using Core;
using Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DataElector : IData
    {
        LastFMData _lastFMData;
        MusicDbData _musicDbData;

        public DataElector(MusicContext context)
        {
            _lastFMData = new LastFMData(context);
            _musicDbData = new MusicDbData(context);
        }

        public List<Artist> GetTopArtists(int page, int limit)
        {
            List<Artist> artists = _musicDbData.GetTopArtists(page, limit);
            return artists.Count == 0 ? _lastFMData.GetTopArtists(page, limit) : artists;
        }

        public List<Track> GetArtistTopTracks(string name, int page, int limit)
        {
            List<Track> tracks = _musicDbData.GetArtistTopTracks(name, page, limit);
            return tracks.Count == 0 ? _lastFMData.GetArtistTopTracks(name, page, limit) : tracks;
        }

        public List<Album> GetArtistTopAlbums(string name, int page, int limit)
        {
            List<Album> albums = _musicDbData.GetArtistTopAlbums(name, page, limit);
            return albums.Count == 0 ? _lastFMData.GetArtistTopAlbums(name, page, limit) : albums;
        }

        public List<Artist> GetSimilarArtists(string name, int limit)
        {
            List<Artist> artists = _musicDbData.GetSimilarArtists(name, limit);
            return artists.Count == 0 ? _lastFMData.GetSimilarArtists(name, limit) : artists;
        }

        public Artist SearchArtist(string name)
        {
            Artist artist = _musicDbData.SearchArtist(name);
            return artist?.ShortBiography == null ? _lastFMData.SearchArtist(name) : artist;
        }

        public Album GetArtistAlbum(string artistName, string albumName)
        {
            Album album = _musicDbData.GetArtistAlbum(artistName, albumName);
            return album == null ? _lastFMData.GetArtistAlbum(artistName, albumName) : album;
        }
    }
}
