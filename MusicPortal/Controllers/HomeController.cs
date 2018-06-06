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
        public IActionResult Index()
        {
            LastFMData a = new LastFMData();
            return View(a.GetTopArtists(1, 10, 2));
        }
        
        [HttpGet]
        public IActionResult ArtistProfile(string Name)
        {
            string name = Request.Query.FirstOrDefault(p => p.Key == "Name").Value;
            LastFMData a = new LastFMData();
            Artist artist = a.GetTopArtists(1, 10, 3).FirstOrDefault(p => p.Name == Name);
            artist.SetBiography(a.GetArtistBiography(Name));
            return View(artist);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
