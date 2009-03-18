namespace PlaylistPanda.Slurp
{
    /// <summary>
    /// Represents the bare minimum of a song's tag information.
    /// </summary>
    public class Song
    {
        public string Path { get; set; }

        public string Artist { get; set; }
        public string Album { get; set; }
        public string Title { get; set; }

        public Song()
        {
        }

        public Song(string path, string artist, string album, string title)
        {
            Path = path;

            Artist = artist;
            Album = album;
            Title = title;
        }
    }
}