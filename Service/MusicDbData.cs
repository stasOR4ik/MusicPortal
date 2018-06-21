using Core;
using Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class MusicDbData : IData
    {
        private MusicContext _context;
        private MusicRepository<Artist> _artistsDb;
        private MusicRepository<Album> _albumsDb;
        private MusicRepository<Track> _tracksDb;

        public MusicDbData(MusicContext context)
        {
            _context = context;
            _albumsDb = new MusicRepository<Album>(_context);
            _artistsDb = new MusicRepository<Artist>(_context);
            _tracksDb = new MusicRepository<Track>(_context);
        }

        public List<Artist> GetTopArtists(int page, int limit)
        {
            return _artistsDb.GetAllBy(p => p.Id > (page - 1) * limit && p.Id <= page * limit).ToList();
        }

        public List<Track> GetArtistTopTracks(string name, int page, int limit)
        {
            Artist artist = _artistsDb.GetBy(p => p.Name == name);
            artist.Tracks = _tracksDb.GetAllBy(p => p.Artist.Id == artist.Id).ToList();
            _artistsDb.Save();
            return artist.Tracks;
        }

        public List<Album> GetArtistTopAlbums(string name, int page, int limit)
        {
            return _artistsDb.GetBy(p => p.Name == name).Albums;
        }

        public List<Artist> GetSimilarArtists(string name, int limit)
        {
            List<Artist> similarArtists = new List<Artist>();
            foreach(ArtistSimilarArtist similarArtist in GetArtistByName(name).SimilarArtists)
            {
                similarArtists.Add(similarArtist.SimilarArtist);
            }
            return similarArtists;
        }

        public Artist SearchArtist(string name, bool isShorBiography)
        {
            return GetArtistByName(name);
        }

        public Album GetArtistAlbum(string artistName, string albumName)
        {
            return GetArtistByName(artistName).Albums.FirstOrDefault(p => p.Name == albumName);
        }

        Artist GetArtistByName(string name)
        {
            return _artistsDb.GetBy(p => p.Name == name);
        }
    }
}
