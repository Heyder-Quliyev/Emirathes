using emirathes.Models;
using emirathes.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
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
            return View(appDbContent.Ticktes.Where(x => x.IsAvailable != false).ToList());

        }


        //public IActionResult FlightSingle(int id)
        //{
        //    //ViewBag.Category = appDbContent.Categories.ToList();
        //    ViewBag.Types = appDbContent.Passengers.ToList();
        //    return View(appDbContent.Ticktes.Find(id));
        //}

        public IActionResult FlightSingle(int id)
        {
            TicketCategoryVM views = new()
            {
                Tickts = appDbContent.Ticktes.FirstOrDefault(x => x.Id == id),
                Categories = appDbContent.Categories.ToList(),
                Products = appDbContent.Products.ToList(),
                Passengers = appDbContent.Passengers.ToList()
            };

            return View(views);
        }


        public IActionResult FlightBooking()
        {
            return View();

        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View(appDbContent.Ticktes.Where(x => x.IsAvailable != false).ToList());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }






    }
}
