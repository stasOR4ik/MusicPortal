using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MusicPortal.Core;
using Microsoft.AspNetCore.Mvc;
using MusicPortal.Models;
using Newtonsoft.Json;
using MusicPortal.Repo;
using MusicPortal.Service;

namespace MusicPortal.Controllers
{
    public class HomeController : Controller
    {
        DataElector _data;

        public HomeController(MusicContext musicContext)
        {
            _data = new DataElector(musicContext);
        }

        public IActionResult Index()
        {
            return View(_data.GetTopArtists(1, 12));
        }
        
        public IActionResult ArtistProfile(string name)
        {
            Artist artist = _data.SearchArtist(name);
            return artist == null ? View("ErrorMessage", "This artist doesn't exist.") : View(artist);
        }

        public IActionResult ArtistAlbum(string artistName, string albumName)
        {
            return View(_data.GetArtistAlbum(artistName, albumName));
        }

        public IActionResult ArtistBiography(string name)
        {
            return View(_data.SearchArtist(name));
        }

        [HttpPost]
        public IActionResult PartialArtistsOnStartPage(int page = 1, int numberArtistsOnStartPage = 12)
        {
            return PartialView("_PartialArtistsOnStartPage", _data.GetTopArtists(page, numberArtistsOnStartPage));
        }

        [HttpPost]
        public IActionResult PartialPagination(int page = 1, int numberArtistsOnStartPage = 12)
        {
            return PartialView("_PartialPagination", new Pagination(page, numberArtistsOnStartPage));
        }

        [HttpPost]
        public IActionResult PartialArtistTopTracks(string name)
        {
            return PartialView("_PartialArtistTopTracks", _data.GetArtistTopTracks(name, 1, 15));
        }

        [HttpPost]
        public IActionResult PartialArtistTopAlbums(string name)
        {
            return PartialView("_PartialArtistTopAlbums", new Artist(name, _data.GetArtistTopAlbums(name, 1, 12)));
        }

        [HttpPost]
        public IActionResult PartialArtistSimilarArtists(string name)
        {
            return PartialView("_PartialArtistSimilarArtists", _data.GetSimilarArtists(name, 12));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
