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
            Index index = new Index(_data.GetTopArtists(page, numberOfArtistsOnStartPage, 2), page, numberOfArtistsOnStartPage);
            return View(index);
        }
        
        public IActionResult ArtistProfile(string name, int tab = 0)
        {
            Artist artist = _data.SearchArtist(name, 1, true);
            if (tab == 1)
            {
                artist.Albums = _data.GetArtistTopAlbums(name, 1, 12);
            }
            else if (tab == 2)
            {
                //foreach (Artist similarArtist in _data.GetSimilarArtists(name, 12))
                //{
                //    _db.Artists.Add(similarArtist);
                //    _db.SaveChanges();
                //}
                //artist.SimilarArtists.SimilarArtists = _data.GetSimilarArtists(name, 12);
                //_db.Artists.Add(artist);
                //_db.SaveChanges();
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
            Artist artist = _data.SearchArtist(name, 1, false);
            return View(artist);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
