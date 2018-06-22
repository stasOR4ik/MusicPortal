using Core;
using Repo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
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

        public List<Artist> GetTopArtists(int page, int limit) => _artistsDb.GetAllBy(p => p.Id > (page - 1) * limit && p.Id <= page * limit).ToList();

        public List<Track> GetArtistTopTracks(string name, int page, int limit) => _tracksDb.GetAllBy(p => p.Artist.Id == GetArtistByName(name).Id).ToList();

        public List<Album> GetArtistTopAlbums(string name, int page, int limit) => _albumsDb.GetAllBy(p => p.Artist.Id == GetArtistByName(name).Id).ToList();

        public List<Artist> GetSimilarArtists(string name, int limit)
        {
            List<Artist> artists = new List<Artist>();
            List<ArtistSimilarArtist> similarArtists = _context.SimilarArtists.Include("SimilarArtist").Where(p => p.Artist == GetArtistByName(name)).ToList();
            foreach (ArtistSimilarArtist similarArtist in similarArtists)
            {
                artists.Add(similarArtist.SimilarArtist);
            }
            return artists;
        }

        public Artist SearchArtist(string name) => GetArtistByName(name);

        public Album GetArtistAlbum(string artistName, string albumName)
        {
            Album album = _albumsDb.GetBy(p => p.Name == albumName);
            album.Tracks = _tracksDb.GetAllBy(p => p.Album == album).ToList();
            return album.Tracks == null || album.Artist == null ? null : album;
        }

        Artist GetArtistByName(string name) => _artistsDb.GetBy(p => p.Name == name);
    }
}
