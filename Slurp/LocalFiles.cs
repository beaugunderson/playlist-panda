using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Linq;

namespace PlaylistPanda.Slurp
{
    class LocalFiles
    {
        public Collection<string> Locations { get; set; }
        public Collection<Song> Songs { get; set; }

        private static readonly string[] _extensions = new[] { ".mp3", ".wma", ".mp4", ".wav", ".ra", ".flac", ".mpc", ".ogg", ".mp2" };

        public LocalFiles()
        {
            Locations = new Collection<string>();
            Songs = new Collection<Song>();
        }

        public void Slurp()
        {
            foreach (string location in Locations)
            {
                if (Directory.Exists(location))
                {
                    RecursiveSlurp(location);
                }
                else
                {
                    Console.WriteLine("Directory did not exist: {0}", location);
                }
            }
        }

        public void RecursiveSlurp(object stateInfo)
        {
            string location = (string)stateInfo;

            Console.WriteLine("Slurping tags from files in {0}", location);

            FileInfo[] files = new DirectoryInfo(location).GetFiles("*", SearchOption.AllDirectories);

            Console.WriteLine("Got {0} files.", files.Length);

            // Divide the list up into chunks
            int workItems = Environment.ProcessorCount * 5;
            int chunkSize = Math.Max(files.Count() / workItems, 1);

            int count = workItems;

            // Use an event to wait for all work items
            using (var mre = new ManualResetEvent(false))
            {
                // Each work item processes appx 1/Nth of the data items
                WaitCallback callback = state =>
                {
                    int iteration = (int)state;
                    int from = chunkSize * iteration;
                    int to = iteration == workItems - 1 ? files.Count() : chunkSize * (iteration + 1);

                    Console.WriteLine("   Sub-tasked {0} files.", to - from);

                    while (from < to)
                    {
                        processFile(files[from++]);
                    }

                    Console.WriteLine("   Filled {0} songs.", Songs.Count);

                    if (Interlocked.Decrement(ref count) == 0)
                    {
                        mre.Set();
                    }
                };

                // The ThreadPool is used to process all but one of the
                // chunks; the current thread is used for that chunk,
                // rather than just blocking.
                for (int i = 0; i < workItems; i++)
                {
                    if (i < workItems - 1)
                    {
                        ThreadPool.QueueUserWorkItem(callback, i);
                    }
                    else
                    {
                        callback(i);
                    }
                }

                // Wait for all work to complete
                mre.WaitOne();
            }
        }

        private void processFile(FileSystemInfo file)
        {
            if (!_extensions.Contains(file.Extension))
            {
                return;
            }

            try
            {
                TagLib.File tagFile = TagLib.File.Create(file.FullName);

                Songs.Add(new Song(file.FullName, tagFile.Tag.JoinedPerformers, tagFile.Tag.Album, tagFile.Tag.Title));
            }
            catch (TagLib.CorruptFileException exception)
            {
                Console.WriteLine("Unable to read file (CorruptFile): " + file.FullName);
            }
            catch (TagLib.UnsupportedFormatException exception)
            {
                Console.WriteLine("Unable to read file (UnsupportedFormat): " + file.FullName);
            }
        }
    }
}