using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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

        private void MainForm_Load(object sender, EventArgs e)
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            LocalFiles localFiles = new LocalFiles();
            localFiles.Locations.Add(@"C:\Music");
            localFiles.Slurp();

            stopwatch.Stop();

            XmlSerializer serializer = new XmlSerializer(typeof(LocalFiles));
            TextWriter writer = new StreamWriter(@"C:\Test.xml");
            
            serializer.Serialize(writer, localFiles);
            writer.Close();

            Console.WriteLine("Total time: {0}", stopwatch.Elapsed);
        }
    }
}