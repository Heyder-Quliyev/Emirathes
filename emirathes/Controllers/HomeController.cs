using emirathes.Models;
using emirathes.ViewModels;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using NETCore.MailKit.Core;
using System.Diagnostics;
using MailKit.Net.Smtp;
using System.Net.Mail;

namespace emirathes.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly AppDbContent appDbContent;
        private readonly IConfiguration _configuration;
       public HomeController(AppDbContent _appDbContent, IConfiguration configuration)
        {
            appDbContent = _appDbContent;
            _configuration = configuration;
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
        //    //ViewBag.Category = appDbContent.Categories.ToList();
        //    ViewBag.Types = appDbContent.Passengers.ToList();
        //    return View(appDbContent.Ticktes.Find(id));
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


        public IActionResult FlightBooking()
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
        public IActionResult Support()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task <ActionResult> Support(ContactModel model, object smtpServer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            ConfigurationSection emailSettings = (ConfigurationSection)_configuration.GetSection("EmailSettings");
        //            string smtServer = emailSettings.GetValue<string>("Email");
        //            int emtpPort = emailSettings.GetValue<int>("EmailPort");
        //            string userName = emailSettings.GetValue<string>("EmailAddress");
        //            string password = emailSettings.GetValue<string>("EmailPassword");

        //            //create the email message
        //            MimeMessage message = new MimeMessage();
        //            message.From.Add(new MailboxAddress(" ", "heyderquliyev30@gmail.com"));
        //            message.To.Add(new MailboxAddress(" ", "heyderquliyev30@gmail.com"));
        //            message.Subject = "New From Submission";

        //            //Build themail body
        //            BodyBuilder builder = new BodyBuilder();
        //            builder.TextBody = $"Name: {model.SenderName} {Environment.NewLine} Email:{model.SenderEmail}{Environment.NewLine}Message:{model.Message}";

        //            message.Body = builder.ToMessageBody();

        //            //configure the email client

        //            using (var client = new MailKit.Net.Smtp.SmtpClient())
        //            {
        //                await client.ConnectAsync(smtpServer, smtpPort, SecureSocketOptions.StartTls);
        //                await client.DisconnectAsync(true);

        //            }



        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //    }
        //    return View(model);
        //}

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
