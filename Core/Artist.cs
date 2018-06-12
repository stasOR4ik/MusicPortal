using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private string PictureLink { get; set; }
        private string Biography { get; set; }
        private string ShortBiography { get; set; }
        public List<Track> Tracks { get; set; }
        public List<Album> Albums { get; set; }
        public List<Artist> SimilarArtists { get; set; }

        public Artist() { }

        public Artist(string name)
        {
            Name = name;
            Tracks = new List<Track>();
            Albums = new List<Album>();
            SimilarArtists = new List<Artist>();
        }

        public void SetPictureLink(string picturelink)
        {
            string defultPictureLink = "https://lastfm-img2.akamaized.net/i/u/avatar170s/2a96cbd8b46e442fc41c2b86b821562f";
            PictureLink = picturelink == "" ? defultPictureLink : picturelink;
        }

        public string GetPictureLink()
        {
            return PictureLink;
        }

        public void SetBiography(string biography)
        {
            Biography = biography;
        }

        public string GetBiography()
        {
            return Biography;
        }

        public void SetShortBiography(string shortBiography)
        {
            ShortBiography = shortBiography;
        }

        public string GetShortBiography()
        {
            return ShortBiography;
        }
    }
}
