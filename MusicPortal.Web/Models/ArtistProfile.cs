using MusicPortal.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPortal.Web.Models
{
    public class ArtistProfile
    {
        public Artist Artist { get; set; }
        public int Tab { get; set; }

        public ArtistProfile(Artist artist, int tab)
        {
            Artist = artist;
            Tab = tab;
        }
    }
}
