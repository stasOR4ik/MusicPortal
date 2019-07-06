using MusicPortal.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPortal.Models
{
    public class Pagination
    {
        public int Page { get; set; }
        public int NumberOfArtistsOnStartPage { get; set; }

        public Pagination() { }

        public Pagination(int page, int namberOfArtistsOnStartPage)
        {
            Page = page;
            NumberOfArtistsOnStartPage = namberOfArtistsOnStartPage;
        }
    }
}
