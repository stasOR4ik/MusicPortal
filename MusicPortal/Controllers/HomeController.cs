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
            return View(a.GetTopArtists(1,10));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
