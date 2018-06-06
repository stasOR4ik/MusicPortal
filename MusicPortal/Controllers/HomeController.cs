using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicPortal.Models;

namespace MusicPortal.Controllers
{
    public class HomeController : Controller
    {
        LastFMData _data;
        int _numberOfArtistsOnStartPage = 50;

        public HomeController()
        {
            _data = new LastFMData();
        }

        public IActionResult Index()
        {
            return View(_data.GetTopArtists(1, _numberOfArtistsOnStartPage, 2));
        }
        
        [HttpGet]
        public IActionResult ArtistProfile(string Name)
        {
            Artist artist = _data.GetTopArtists(1, _numberOfArtistsOnStartPage, 3).FirstOrDefault(p => p.Name == Name);
            artist.SetShortBiography(_data.GetArtistBiography(Name, "summary"));
            return View(artist);
        }

        public IActionResult ArtistBiography(string Name)
        {
            Artist artist = _data.GetTopArtists(1, _numberOfArtistsOnStartPage, 3).FirstOrDefault(p => p.Name == Name);
            artist.SetBiography(_data.GetArtistBiography(Name, "content"));
            return View(artist);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
