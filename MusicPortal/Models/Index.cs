using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPortal.Models
{
    public class Index
    {
        public List<Artist> Artists { get; set; }
        public int Page { get; set; }
        public int NumberOfArtistsOnStartPage { get; set; }

        public Index(List<Artist> list) { }

        public Index(List<Artist> artists, int page, int namberOfArtistsOnStartPage)
        {
            Artists = artists;
            Page = page;
            NumberOfArtistsOnStartPage = namberOfArtistsOnStartPage;
        }
    }
}
