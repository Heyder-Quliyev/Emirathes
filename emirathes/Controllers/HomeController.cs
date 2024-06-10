using emirathes.Models;
using emirathes.ViewModels;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using NETCore.MailKit.Core;
using System.Diagnostics;
using MailKit.Net.Smtp;
using System.Net.Mail;
using emirathes.IRepository;

namespace emirathes.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly AppDbContent appDbContent;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(AppDbContent _appDbContent, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            appDbContent = _appDbContent;
            _configuration = configuration;
            _unitOfWork = unitOfWork;

        }

        public IActionResult SignUp()
        {
            return View();
        }



        public IActionResult FlightSearch()
        {
            return View(appDbContent.Products.Where(x => x.IsAvailable != false).ToList());

        }


        //public IActionResult FlightSingle(int id)
        //{
        //    ViewBag.Category = appDbContent.Categories.ToList();
        //    ViewBag.Types = appDbContent.Passengers.ToList();
        //    return View(appDbContent.Products.Find(id));
        //}

        public IActionResult FlightSingle(int id)
        {
            TicketCategoryVM views = new()
            {
            Tickts = appDbContent.Products.FirstOrDefault(x => x.Id == id),
                Categories = appDbContent.Categories.ToList(),
                Products = appDbContent.Attributes.ToList(),
                Passengers = appDbContent.Passengers.ToList()
            };

            return View(views);
        }


        public IActionResult FlightBooking(int id)
        {
            return View();
        }



        public IActionResult Index()
        {
            return View();
        }


        public IActionResult About()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }


        public IActionResult Confirmation()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View(appDbContent.Products.Where(x => x.IsAvailable != false).ToList());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }






    }
}
