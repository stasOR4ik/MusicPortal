using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.Core
{
    public class ArtistSimilarArtist
    {
        public int  Id { get; set; }
        
        public Artist Artist { get; set; }

        public Artist SimilarArtist { get; set; }

        public ArtistSimilarArtist()
        {
            Artist = new Artist();
            SimilarArtist = new Artist();
        }

        public ArtistSimilarArtist(Artist artists, Artist similarArtists)
        {
            Artist = artists;
            SimilarArtist = similarArtists;
        }
    }
}
