using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicPortal.Models;
using Newtonsoft.Json;

namespace MusicPortal.Controllers
{
    public class HomeController : Controller
    {
        LastFMData _data;
        int numberOfArtistsOnStartPage = 12;

        public HomeController()
        {
            _data = new LastFMData();
        }

        public IActionResult Index(int page = 1, int numberOfArtistsOnIndex = 12)
        {
            numberOfArtistsOnStartPage = numberOfArtistsOnIndex;
            return View(new Index(_data.GetTopArtists(page, numberOfArtistsOnStartPage, 2), page, numberOfArtistsOnStartPage));
        }
        
        public IActionResult ArtistProfile(string name, int tab = 0)
        {
            Artist artist = _data.SearchArtist(name, 1);
            artist.SetShortBiography(_data.GetArtistBiography(name, "summary"));
            if (tab == 1)
            {
                artist.Albums = _data.GetArtistTopAlbums(name, 1, 12);
            }
            else if (tab == 2)
            {
                artist.SimilarArtists = _data.GetSimilarArtists(name, 12);
            }
            else
            {
                artist.Tracks = _data.GetArtistTopTracks(name, 1, 15);
            }
            return View(new ArtistProfile(artist, tab));
        }

        public IActionResult ArtistAlbum(string artistName, string albumName)
        {
            return View(_data.GetArtistAlbum(artistName, albumName));
        }

        public IActionResult ArtistBiography(string name)
        {
            Artist artist = _data.SearchArtist(name, 1);
            artist.SetBiography(_data.GetArtistBiography(name, "content"));
            return View(artist);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
