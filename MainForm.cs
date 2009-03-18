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

        /// <summary>
        /// Returns false if the saved options aren't valid.
        /// </summary>
        /// <returns></returns>
        public static bool OptionsAreValid()
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

            if (!OptionsAreValid())
            {
                while (new OptionsForm().ShowDialog() != DialogResult.OK)
                {
                    // Keep showing the OptionsForm until they save successfully.
                }
            }

            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.RunWorkerAsync();

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

                // XXX: Make this easier for the user.
                MessageBox.Show("Click OK to continue once you have logged in at Last.FM.");

                session.AuthenticateViaWeb();
            }

            // Authenticate it with a username and password to be able
            // to perform write operations and access private data.
            session.Authenticate(Settings.Default.LastFmUserName, Lastfm.Utilities.md5(Settings.Default.LastFmPassword));

            // Save the session key because it never expires (unless revoked).
            Settings.Default.LastFmSessionKey = session.SessionKey;
            Settings.Default.Save();

            User user = new User(Settings.Default.LastFmUserName, session);

            List<LibraryTrack> tracks = new List<LibraryTrack>();

            for (int page = 1; page <= user.Library.Tracks.GetPageCount(); page++)
            {
                Console.WriteLine("Adding page {0}.", page);

                tracks.AddRange(user.Library.Tracks.GetPage(page));
            }

            //TopTrack[] tracks = user.GetTopTracks(Period.Overall);

            //foreach (TopTrack track in tracks)
            //{
            //    topTracksListBox.Items.Add(string.Format("{0} - {1} - {2}", track.Weight, track.Item.Artist.Name, track.Item.Title));
            //}
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            string localFilesPath = Path.Combine(_path, "LocalFiles.xml");

            XmlSerializer serializer = new XmlSerializer(typeof(LocalFiles));
            LocalFiles localFiles;

            if (File.Exists(localFilesPath))
            {
                // Read and deserialize the cached data if it exists.
                // TODO: Add expiration date logic to re-slurp files.
                TextReader reader = new StreamReader(localFilesPath);

                localFiles = (LocalFiles)serializer.Deserialize(reader);
            }
            else
            {
                // If the file doesn't exist slurp files from the specified locations.
                localFiles = new LocalFiles
                {
                    Locations = Settings.Default.Locations,
                    ThreadsPerProcessor = Settings.Default.ThreadsPerProcessor
                };

                localFiles.Slurp();

                TextWriter writer = new StreamWriter(Path.Combine(_path, "LocalFiles.xml"));

                serializer.Serialize(writer, localFiles);
                writer.Close();
            }

            stopwatch.Stop();

            // TODO: Output debugging data another way.
            Console.WriteLine("Total time: {0}", stopwatch.Elapsed);
            Console.WriteLine("Total songs added: {0}", localFiles.Songs.Count);
        }

        /// <summary>
        /// Show the OptionsForm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionsButton_Click(object sender, EventArgs e)
        {
            OptionsForm optionsForm = new OptionsForm();

            optionsForm.ShowDialog();
        }
    }
}