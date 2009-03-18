using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
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

        public static bool ValidateOptions()
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
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }

            if (!ValidateOptions())
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
                Lastfm.Lib.Proxy = new WebProxy("http://webproxy.costco.com:80/", false, null, new NetworkCredential(Settings.Default.ProxyUsername, Settings.Default.ProxyPassword));
            }

            Session session;

            if (!string.IsNullOrEmpty(Settings.Default.LastFmSessionKey))
            {
                session = new Session(_API_KEY, _API_SECRET, Settings.Default.LastFmSessionKey);
            }
            else
            {
                session = new Session(_API_KEY, _API_SECRET);

                Process.Start(session.GetWebAuthenticationURL());

                MessageBox.Show("Click OK to continue once you have logged in at Last.FM.");

                session.AuthenticateViaWeb();
            }

            // Authenticate it with a username and password to be able
            // to perform write operations and access this user's profile
            // private data.
            session.Authenticate(Settings.Default.LastFmUserName, Lastfm.Utilities.md5(Settings.Default.LastFmPassword));

            Settings.Default.LastFmSessionKey = session.SessionKey;
            Settings.Default.Save();

            User u = new User(Settings.Default.LastFmUserName, session);

            TopTrack[] tracks = u.GetTopTracks(Period.Overall);

            foreach (TopTrack track in tracks)
            {
                string format = string.Format("{0} - {1} - {2}", track.Weight, track.Item.Artist.Name, track.Item.Title);

                topTracksListBox.Items.Add(format);
            }
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
                TextReader reader = new StreamReader(localFilesPath);
                localFiles = (LocalFiles)serializer.Deserialize(reader);
            }
            else
            {
                localFiles = new LocalFiles
                {
                    Locations = Settings.Default.Locations
                };

                localFiles.Slurp();

                TextWriter writer = new StreamWriter(Path.Combine(_path, "LocalFiles.xml"));

                serializer.Serialize(writer, localFiles);
                writer.Close();
            }

            stopwatch.Stop();

            Console.WriteLine("Total time: {0}", stopwatch.Elapsed);
            Console.WriteLine("Total songs added: {0}", localFiles.Songs.Count);
        }

        private void optionsButton_Click(object sender, EventArgs e)
        {
            OptionsForm optionsForm = new OptionsForm();

            optionsForm.ShowDialog();
        }
    }
}