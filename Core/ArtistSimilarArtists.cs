using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ArtistSimilarArtist
    {
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

        public int SimilarArtistId { get; set; }
        public List<Artist> SimilarArtists { get; set; }

        public ArtistSimilarArtist()
        {
            Artist = new Artist();
            SimilarArtists = new List<Artist>();
        }

        public ArtistSimilarArtist(Artist artists, List<Artist> similarArtists)
        {
            Artist = artists;
            SimilarArtists = similarArtists;
        }
    }
}
