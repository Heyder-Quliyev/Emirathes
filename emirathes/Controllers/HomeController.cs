using emirathes.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace emirathes.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContent appDbContent;
        public HomeController(AppDbContent _appDbContent)
        {
            appDbContent = _appDbContent;
        }



        public IActionResult SignUp()
        {
            return View();
        }



        public IActionResult FlightSearch()
        {
            return View(appDbContent.Ticktes.ToList());
        }









        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View(appDbContent.Ticktes.Where(x=>x.IsAvailable !=false).ToList());
        }

      
    }
}
