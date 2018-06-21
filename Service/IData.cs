using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    interface IData
    {
        List<Artist> GetTopArtists(int page, int limit);

        List<Track> GetArtistTopTracks(string name, int page, int limit);

        List<Album> GetArtistTopAlbums(string name, int page, int limit);

        List<Artist> GetSimilarArtists(string name, int limit);

        Artist SearchArtist(string name, bool isShorBiography);

        Album GetArtistAlbum(string artistName, string albumName);
    }
}
