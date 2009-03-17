using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
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
        private static readonly byte[] _entropy = new byte[] { 0x01, 0x02, 0x03, 0x05, 0x07, 0x11 };

        public static void WritePassword(string password)
        {
            byte[] secret = Encoding.Unicode.GetBytes(password);
            byte[] protectedSecret = ProtectedData.Protect(secret, _entropy, DataProtectionScope.CurrentUser);

            Properties.Settings.Default.LastFmPassword = Convert.ToBase64String(protectedSecret, Base64FormattingOptions.None);
        }

        public static string ReadPassword()
        {
            try
            {
                byte[] protectedSecret = Convert.FromBase64String(Properties.Settings.Default.LastFmPassword);
                byte[] secret = ProtectedData.Unprotect(protectedSecret, _entropy, DataProtectionScope.CurrentUser);

                return Encoding.Unicode.GetString(secret);
            }
            catch (CryptographicException cryptographicException)
            {
                Console.WriteLine(cryptographicException);

                return null;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }

            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.RunWorkerAsync();
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
                    Locations = Properties.Settings.Default.Locations
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
    }
}