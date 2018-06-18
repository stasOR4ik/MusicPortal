using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Mvc;
using MusicPortal.Models;
using Newtonsoft.Json;
using Repo;

namespace MusicPortal.Controllers
{
    public class HomeController : Controller
    {
        MusicContext _db;
        LastFMData _data;
        int numberOfArtistsOnStartPage = 12;

        public HomeController(MusicContext musicContext)
        {
            _db = musicContext;
            _data = new LastFMData();
        }

        public IActionResult Index(int page = 1, int numberOfArtistsOnIndex = 12)
        {
            numberOfArtistsOnStartPage = numberOfArtistsOnIndex;
            return View(new Index(_data.GetTopArtists(page, numberOfArtistsOnStartPage, 2), page, numberOfArtistsOnStartPage));
        }
        
        public IActionResult ArtistProfile(string name)
        {
            return View(_data.SearchArtist(name, 1, true));
        }

        public IActionResult ArtistAlbum(string artistName, string albumName)
        {
            return View(_data.GetArtistAlbum(artistName, albumName));
        }

        public IActionResult ArtistBiography(string name)
        {
            return View(_data.SearchArtist(name, 1, false));
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
