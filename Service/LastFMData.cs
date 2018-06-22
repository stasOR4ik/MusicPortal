﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Core;
using Repo;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Service
{
    public class LastFMData : IData
    {
        private string commonUrl = "http://ws.audioscrobbler.com/2.0/?api_key=6406cb88807bffe5b2492343145f8451&format=json&";
        private MusicContext _context;
        private MusicRepository<Artist> _artistsDb;
        private MusicRepository<Album> _albumsDb;
        private MusicRepository<Track> _tracksDb;

        public LastFMData(MusicContext context)
        {
            _context = context;
            _albumsDb = new MusicRepository<Album>(_context);
            _artistsDb = new MusicRepository<Artist>(_context);
            _tracksDb = new MusicRepository<Track>(_context);
        }

        public List<Artist> GetTopArtists(int page, int limit)
        {
            string artistsUrl = commonUrl + "method=chart.gettopartists&page=" + page + "&limit=" + limit;
            List<Artist> artists = new List<Artist>();
            foreach (JToken singer in TakeJObjectFromLastFM(artistsUrl)["artists"]["artist"])
            {
                string artistName = singer.SelectToken("name").ToString();
                Artist artist = new Artist(artistName);
                artist.SetPictureLink(singer["image"][2].SelectToken("#text").ToString());
                _artistsDb.Create(artist);
                _artistsDb.Save();
                artists.Add(artist);
            }
            
            return artists;
        }

        public List<Track> GetArtistTopTracks(string name, int page, int limit)
        {
            string tracksUrl = commonUrl + "method=artist.gettoptracks&page=" + page + "&limit=" + limit + "&artist=" + name;
            List<Track> tracks = new List<Track>();
            Artist artist = _artistsDb.GetBy(p => p.Name == name);
            foreach (JToken song in TakeJObjectFromLastFM(tracksUrl).SelectToken("toptracks")["track"])
            {
                string trackName = song.SelectToken("name").ToString();
                Track track = new Track(trackName);
                track.SetPictureLink(GetTrackImage(name, track.Name));
                track.SetDurationInMilliseconds(GetTrackDurationInMilliseconds(name, track.Name));
                track.Artist = artist;
                _tracksDb.Create(track);
                tracks.Add(track);
            }
            _tracksDb.Save();
            return tracks;
        }

        public List<Album> GetArtistTopAlbums(string name, int page, int limit)
        {
            string albumsUrl = commonUrl + "method=artist.gettopalbums&page=" + page + "&limit=" + limit + "&artist=" + name;
            List<Album> albums = new List<Album>();
            foreach (JToken _album in TakeJObjectFromLastFM(albumsUrl).SelectToken("topalbums")["album"])
            {
                Album album = new Album(_album.SelectToken("name").ToString());
                album.SetPictureLink(GetAlbumImage(name, album.Name));
                albums.Add(album);
            }
            return albums;
        }

        public List<Artist> GetSimilarArtists(string name, int limit)
        {
            string artistsUrl = commonUrl + "method=artist.getsimilar&limit=" + limit + "&artist=" + name;
            List<Artist> artists = new List<Artist>();
            foreach (JToken singer in TakeJObjectFromLastFM(artistsUrl).SelectToken("similarartists")["artist"])
            {
                Artist artist = new Artist(singer.SelectToken("name").ToString());
                artist.SetPictureLink(singer["image"][2].SelectToken("#text").ToString());
                artists.Add(artist);
            }
            return artists;
        }

        public Artist SearchArtist(string name)
        {
            string artistUrl = commonUrl + "method=artist.search&limit=1&artist=" + name;
            JToken jsonData = TakeJObjectFromLastFM(artistUrl).SelectToken("results").SelectToken("artistmatches").SelectToken("artist")[0];
            Artist searchArtist = _artistsDb.GetBy(p => p.Name == name);
            Artist artist = searchArtist != null ? searchArtist : new Artist(jsonData.SelectToken("name").ToString());
            (string, string) biographies = GetArtistBiographies(name);
            artist.ShortBiography = biographies.Item1;
            artist.Biography = biographies.Item2;
            artist.SetPictureLink(jsonData.SelectToken("image")[2].SelectToken("#text").ToString());
            if (searchArtist == null)
            {
                _artistsDb.Create(artist);
            }
            else
            {
                _artistsDb.Update(artist);
            }
            _artistsDb.Save();
            return artist;
        }

        public Album GetArtistAlbum(string artistName, string albumName)
        {
            string albumUrl = commonUrl + "method=album.getinfo&artist=" + artistName + "&album=" + albumName;
            JObject jsonData = TakeJObjectFromLastFM(albumUrl);
            Album album = new Album(albumName);
            album.SetPictureLink(jsonData.SelectToken("album")["image"][3].SelectToken("#text").ToString());
            album.Artist = SearchArtist(artistName);
            foreach (JToken song in jsonData.SelectToken("album").SelectToken("tracks")["track"])
            {
                Track track = new Track(song.SelectToken("name").ToString());
                track.SetPictureLink(jsonData.SelectToken("album")["image"][0].SelectToken("#text").ToString());
                track.SetDurationInMilliseconds(GetTrackDurationInMilliseconds(artistName, track.Name));
                album.Tracks.Add(track);
            }
            return album;
        }

        public string GetTrackDurationInMilliseconds(string artistName, string trackName)
        {
            string trackUrl = commonUrl + "method=track.getInfo&artist=" + artistName + "&track=" + trackName;
            JObject jsonData = TakeJObjectFromLastFM(trackUrl);
            return jsonData.SelectToken("track")?.SelectToken("duration")?.ToString() ?? "0";
        }

        public string GetTrackImage(string artistName, string trackName)
        {
            string trackUrl = commonUrl + "method=track.getInfo&artist=" + artistName + "&track=" + trackName;
            JObject jsonData = TakeJObjectFromLastFM(trackUrl);
            return jsonData.SelectToken("track")?.SelectToken("album")?.SelectToken("image")?[0]?.SelectToken("#text")?.ToString() ?? "";
        }

        public string GetAlbumImage(string artistName, string albumName)
        {
            string albumUrl = commonUrl + "method=album.getInfo&artist=" + artistName + "&album=" + albumName;
            JObject jsonData = TakeJObjectFromLastFM(albumUrl);
            return jsonData.SelectToken("album")?.SelectToken("image")?[2]?.SelectToken("#text")?.ToString() ?? "";
        }

        public (string, string) GetArtistBiographies(string artistName)
        {
            string artistUrl = commonUrl + "method=artist.getinfo&" + "artist=" + artistName;
            JObject jsonBiography = TakeJObjectFromLastFM(artistUrl);
            string shortBiography = jsonBiography.SelectToken("artist")["bio"].SelectToken("summary").ToString();
            string biography = jsonBiography.SelectToken("artist")["bio"].SelectToken("content").ToString();
            return (ParsingBiography(shortBiography), ParsingBiography(biography));
        }

        public string ParsingBiography(string biography)
        {
            bool IsRun = true;
            while (IsRun)
            {
                int beginLinkIndex = biography.IndexOf("[http");
                int endLinkIndex = biography.IndexOf("]");
                if (beginLinkIndex != -1 && endLinkIndex > beginLinkIndex)
                {
                    biography = biography.Remove(beginLinkIndex, endLinkIndex - beginLinkIndex + 1);
                }
                else if (beginLinkIndex == -1)
                {
                    IsRun = false;
                }
            }
            biography = biography.Split(new[] { "http" }, StringSplitOptions.None)[0];
            biography = biography.Split(new[] { " <a href" }, StringSplitOptions.None)[0];
            return biography;
        }

        public JObject TakeJObjectFromLastFM(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string jsonFromLastFM = new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();
            return JObject.Parse(jsonFromLastFM);
        }
    }
}