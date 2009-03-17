using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
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
            localFiles.Locations.Add(@"D:\Music\MP3");
            localFiles.Slurp();

            stopwatch.Stop();

            Console.WriteLine("Total time: {0}", stopwatch.Elapsed);
        }
    }
}