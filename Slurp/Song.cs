using System;

namespace PlaylistPanda.Slurp
{
    /// <summary>
    /// Represents the bare minimum of a song's tag information.
    /// Sanitizes incoming strings for inclusion in the XML cache file.
    /// </summary>
    public class Song : IComparable<Song>
    {
        public string Path { get; set; }

        private string _artist;
        private string _album;
        private string _title;

        public string Artist
        {
            get
            {
                return _artist;
            }
            set
            {
                _artist = value == null ? string.Empty : SanitizeXml.SanitizeXmlString(value);
            }
        }

        public string Album
        {
            get
            {
                return _album;
            }
            set
            {
                _album = value == null ? string.Empty : SanitizeXml.SanitizeXmlString(value);
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value == null ? string.Empty : SanitizeXml.SanitizeXmlString(value);
            }
        }

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

        public override string ToString()
        {
            string artist = string.Empty;
            string album = string.Empty;

            if (!string.IsNullOrEmpty(Artist))
            {
                artist = string.Format("{0} - ", Artist);
            }

            if (!string.IsNullOrEmpty(Album))
            {
                album = string.Format("{0} - ", Album);
            }

            return artist + album + (Title == null ? "<Unknown>" : Title);
        }

        public int CompareTo(Song other)
        {
            return ToString().CompareTo(other.ToString());
        }
    }
}