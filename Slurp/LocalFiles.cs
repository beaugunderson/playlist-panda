using System;
using System.Collections.Generic;
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
        public List<Song> Songs { get; set; }

        public int ThreadsPerProcessor { get; set; }

        private static readonly string[] _extensions = new[] { ".mp3", ".wma", ".mp4", ".wav", ".ra", ".flac", ".mpc", ".ogg", ".mp2" };

        private readonly object _locker = new object();

        public LocalFiles()
        {
            Locations = new StringCollection();
            Songs = new List<Song>();
        }

        public void Slurp()
        {
            foreach (string location in Locations)
            {
                if (Directory.Exists(location))
                {
                    slurpLocation(location);
                }
                else
                {
                    Console.WriteLine("Directory did not exist: {0}", location);
                }
            }

            Songs.Sort();
        }

        private void slurpLocation(object stateInfo)
        {
            string location = (string)stateInfo;

            Console.WriteLine("Slurping tags from files in {0}", location);

            // Get every file underneath the path given
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

                    // Create a per-thread list to add to the main list at the end.
                    List<Song> songs = new List<Song>();

                    // Iterate through our thread's slice of the whole list.
                    while (from < to)
                    {
                        Song song = songFromFileSystemInfo(files[from++]);

                        if (song != null)
                        {
                            songs.Add(song);
                        }
                    }

                    Console.WriteLine("   Filled {0} songs.", songs.Count);

                    lock (_locker)
                    {
                        Songs.AddRange(songs);
                    }

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
        /// <param name="file">The <see cref="FileSystemInfo"/> of an audio file to process with <see cref="TagLib"/>.</param>
        private static Song songFromFileSystemInfo(FileSystemInfo file)
        {
            if (!_extensions.Contains(file.Extension))
            {
                return null;
            }

            TagLib.File tagFile = null;

            try
            {
                tagFile = TagLib.File.Create(file.FullName);
            }
            catch (TagLib.CorruptFileException)
            {
                Console.WriteLine("Unable to read file (CorruptFile): {0}", file.FullName);
            }
            catch (TagLib.UnsupportedFormatException)
            {
                Console.WriteLine("Unable to read file (UnsupportedFormat): {0}", file.FullName);
            }

            return tagFile == null ? new Song(file.FullName, string.Empty, string.Empty, string.Empty) :
                                     new Song(file.FullName, tagFile.Tag.JoinedPerformers, tagFile.Tag.Album, tagFile.Tag.Title);
        }
    }
}