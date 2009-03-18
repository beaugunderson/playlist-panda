using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Threading;
using System.Linq;
using System.Xml.Serialization;

namespace PlaylistPanda.Slurp
{
    public class LocalFiles
    {
        [XmlArrayItem("Location", typeof(string))]
        public StringCollection Locations { get; set; }

        [XmlArrayItem("Song", typeof(Song))]
        public ArrayList Songs { get; set; }

        public int ThreadsPerProcessor { get; set; }

        private static readonly string[] _extensions = new[] { ".mp3", ".wma", ".mp4", ".wav", ".ra", ".flac", ".mpc", ".ogg", ".mp2" };

        public LocalFiles()
        {
            Locations = new StringCollection();
            Songs = new ArrayList();
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
            int workItems = Environment.ProcessorCount * ThreadsPerProcessor;
            int chunkSize = Math.Max(files.Count() / workItems, 1);

            int count = workItems;

            // Use an event to wait for all work items
            using (var manualResetEvent = new ManualResetEvent(false))
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
                        manualResetEvent.Set();
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
                manualResetEvent.WaitOne();
            }
        }

        /// <summary>
        /// Add tag information from a file to the Songs collection.
        /// </summary>
        /// <param name="file"></param>
        private void processFile(FileSystemInfo file)
        {
            if (!_extensions.Contains(file.Extension))
            {
                return;
            }

            try
            {
                TagLib.File tagFile = TagLib.File.Create(file.FullName);

                // XXX: Is adding to an ArrayList thread-safe?
                Songs.Add(new Song(file.FullName, tagFile.Tag.JoinedPerformers, tagFile.Tag.Album, tagFile.Tag.Title));
            }
            catch (TagLib.CorruptFileException)
            {
                Console.WriteLine("Unable to read file (CorruptFile): " + file.FullName);
            }
            catch (TagLib.UnsupportedFormatException)
            {
                Console.WriteLine("Unable to read file (UnsupportedFormat): " + file.FullName);
            }
        }
    }
}