using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml.Serialization;
using Lastfm.Services;
using PlaylistPanda.Properties;
using PlaylistPanda.Slurp;

namespace PlaylistPanda
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private static readonly string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PlaylistPanda");

        private const string _API_KEY = "7c54e029ae58a00acb284990211777c7";
        private const string _API_SECRET = "f49bb9cd3afb30e99b7dbd749626c44d";

        private LocalFiles _localFiles;
        private readonly List<LibraryTrack> _tracks = new List<LibraryTrack>();
        private User _user;
        private bool _lastFmDone;
        private bool _slurpDone;

        /// <summary>
        /// Returns false if the saved options aren't valid.
        /// </summary>
        /// <returns></returns>
        private static bool optionsAreValid()
        {
            if (string.IsNullOrEmpty(Settings.Default.LastFmUserName) ||
                string.IsNullOrEmpty(Settings.Default.LastFmPassword) ||
                    (Settings.Default.Locations == null ||
                     Settings.Default.Locations.Count == 0))
            {
                return false;
            }

            return true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Create our application data directory if it doesn't
            // exist; this is where we store our cached data.
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }

            if (!optionsAreValid())
            {
                new OptionsForm().ShowUntilDialogResultOk();
            }

            BackgroundWorker slurpWorker = new BackgroundWorker();

            slurpWorker.DoWork += backgroundWorker_DoWork;
            slurpWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;

            slurpWorker.RunWorkerAsync();

            if (Settings.Default.ProxyEnabled && !string.IsNullOrEmpty(Settings.Default.ProxyUri))
            {
                Lastfm.Lib.Proxy = new WebProxy(Settings.Default.ProxyUri, false, null, new NetworkCredential(Settings.Default.ProxyUsername, Settings.Default.ProxyPassword));
            }

            Session session;

            if (!string.IsNullOrEmpty(Settings.Default.LastFmSessionKey))
            {
                // We have a cached SessionKey, use it.
                session = new Session(_API_KEY, _API_SECRET, Settings.Default.LastFmSessionKey);
            }
            else
            {
                session = new Session(_API_KEY, _API_SECRET);

                // Open the Last.FM authorization page in the default web browser.
                Process.Start(session.GetWebAuthenticationURL());

                // TODO: Make this easier for the user?
                MessageBox.Show("Click OK to continue once you have logged in at Last.FM.");

                session.AuthenticateViaWeb();
            }

            // Authenticate it with a username and password to be able
            // to perform write operations and access private data.
            session.Authenticate(Settings.Default.LastFmUserName, Lastfm.Utilities.md5(Settings.Default.LastFmPassword));

            // Save the session key because it never expires (unless revoked).
            Settings.Default.LastFmSessionKey = session.SessionKey;
            Settings.Default.Save();

            _user = new User(Settings.Default.LastFmUserName, session);

            BackgroundWorker lastFmWorker = new BackgroundWorker();

            lastFmWorker.DoWork += lastFmWorker_DoWork;
            lastFmWorker.RunWorkerCompleted += lastFmWorker_RunWorkerCompleted;

            lastFmWorker.RunWorkerAsync();
        }

        void lastFmWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Sort ourselves (descending by Playcount) in case Last.FM no longer sorts the data for us
            _tracks.Sort((x, y) => y.Playcount.CompareTo(x.Playcount));

            foreach (LibraryTrack track in _tracks)
            {
                topTracksListBox.Items.Add(string.Format("{0} - {1} - {2}", track.Playcount, track.Track.Artist.Name, track.Track.Title));
            }

            _lastFmDone = true;

            enableMatchButton();
        }

        void lastFmWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int maxTracks = Settings.Default.PlaylistTracks;

            int totalPages = _user.Library.Tracks.GetPageCount();

            for (int page = 1; page <= totalPages; page++)
            {
                LibraryTrack[] tracks = _user.Library.Tracks.GetPage(page);

                Trace.WriteLine(string.Format("Added page {0}.", page));

                if (_tracks.Count + tracks.Length > maxTracks)
                {
                    _tracks.AddRange(tracks.Slice(0, maxTracks - _tracks.Count));

                    break;
                }

                _tracks.AddRange(tracks);
            }

            Trace.WriteLine(string.Format("Added {0} tracks.", _tracks.Count));

            // TODO: Blend in GetTopTracks data for other periods.
            //TopTrack[] _tracks = user.GetTopTracks(Period.Overall);        
        }

        /// <summary>
        /// Enable the Match button only if slurping and Last.FM data gathering is done.
        /// </summary>
        private void enableMatchButton()
        {
            if (_slurpDone && _lastFmDone)
            {
                matchButton.Enabled = true;
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _slurpDone = true;

            enableMatchButton();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            string localFilesPath = Path.Combine(_path, "LocalFiles.xml");

            XmlSerializer serializer = new XmlSerializer(typeof(LocalFiles));

            if (File.Exists(localFilesPath))
            {
                // Read and deserialize the cached data if it exists.
                // TODO: Add expiration date logic to re-slurp files.
                TextReader reader = new StreamReader(localFilesPath);

                try
                {
                    _localFiles = (LocalFiles)serializer.Deserialize(reader);
                    reader.Close();
                }
                catch (InvalidOperationException unused)
                {
                    MessageBox.Show("There was an error reading the cache file. Recreating it now.", "Error reading settings!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    reader.Close();

                    // If the file was unreadable slurp the files from the specified locations.
                    slurpFiles(serializer);
                }
            }
            else
            {
                // If the file doesn't exist slurp files from the specified locations.
                slurpFiles(serializer);
            }

            stopwatch.Stop();

            Trace.WriteLine(string.Format("Total time: {0}", stopwatch.Elapsed));
            Trace.WriteLine(string.Format("Total songs added: {0}", _localFiles.Songs.Count));
        }

        private void slurpFiles(XmlSerializer serializer)
        {
            _localFiles = new LocalFiles
            {
                Locations = Settings.Default.Locations,
                ThreadsPerProcessor = Settings.Default.ThreadsPerProcessor,
                Extensions = Settings.Default.Extensions
            };

            _localFiles.Slurp();

            TextWriter writer = new StreamWriter(Path.Combine(_path, "LocalFiles.xml"));

            serializer.Serialize(writer, _localFiles);
            writer.Close();
        }

        /// <summary>
        /// Show the <see cref="OptionsForm"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionsButton_Click(object sender, EventArgs e)
        {
            OptionsForm optionsForm = new OptionsForm();

            optionsForm.ShowDialog();
        }

        private void matchButton_Click(object sender, EventArgs e)
        {
            matchButton.Enabled = false;

            Dictionary<LibraryTrack, List<Song>> possibleMatches = new Dictionary<LibraryTrack, List<Song>>();

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            foreach (LibraryTrack track in _tracks)
            {
                foreach (Song song in _localFiles.Songs)
                {
                    if (string.IsNullOrEmpty(song.Artist) ||
                        string.IsNullOrEmpty(song.Title))
                    {
                        // TODO: Compare based on the file name.
                        continue;
                    }

                    if (LevenshteinDistance.ComputeDistance(song.Artist, track.Track.Artist.Name) >= 3 ||
                        LevenshteinDistance.ComputeDistance(song.Title, track.Track.Title) >= 3)
                    {
                        continue;
                    }

                    if (possibleMatches.ContainsKey(track))
                    {
                        possibleMatches[track].Add(song);
                    }
                    else
                    {
                        possibleMatches.Add(track, new List<Song>());

                        possibleMatches[track].Add(song);
                    }
                }
            }

            stopwatch.Stop();

            Trace.WriteLine(string.Format("Levenshtein comparisons done in {0}", stopwatch.Elapsed));

            foreach (KeyValuePair<LibraryTrack, List<Song>> kvp in possibleMatches)
            {
                Trace.WriteLine(string.Format("{0} - {1}:", kvp.Key.Track.Artist, kvp.Key.Track.Title));
                Trace.Indent();

                foreach (Song song in kvp.Value)
                {
                    Trace.WriteLine(string.Format("{2} ({0} - {1})", song.Artist, song.Title, song.Path));
                }

                Trace.Unindent();
            }

            matchButton.Enabled = true;
        }
    }
}