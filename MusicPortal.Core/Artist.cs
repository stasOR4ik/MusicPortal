﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPortal.Core
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureLink { get; set; }
        public string Biography { get; set; }
        public string ShortBiography { get; set; }
        public List<Track> Tracks { get; set; }
        public List<Album> Albums { get; set; }
        public List<ArtistSimilarArtist> SimilarArtists { get; set; }

        public Artist() { }

        public Artist(string name)
        {
            Name = name;
            Tracks = new List<Track>();
            Albums = new List<Album>();
            SimilarArtists = new List<ArtistSimilarArtist>();
        }

        public Artist(string name, string shortBiography)
        {
            Name = name;
            Tracks = new List<Track>();
            Albums = new List<Album>();
            SimilarArtists = new List<ArtistSimilarArtist>();
            ShortBiography = shortBiography;
        }

        public Artist(string name, List<Album> albums)
        {
            Name = name;
            Tracks = new List<Track>();
            Albums = albums;
            SimilarArtists = new List<ArtistSimilarArtist>();

        }

        public void SetPictureLink(string picturelink)
        {
            string defultPictureLink = "https://lastfm-img2.akamaized.net/i/u/avatar170s/2a96cbd8b46e442fc41c2b86b821562f";
            PictureLink = picturelink == "" ? defultPictureLink : picturelink;
        }
    }
}
