//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authorization;
//using Stripe.Checkout;
//using emirathes.Models;
//using emirathes.ViewModels;
//using emirathes.Extensions;
////using emirathes.Models;
////using emirathes.Models;
//using Microsoft.AspNetCore.Identity;
//using Stripe.Climate;
//using Microsoft.EntityFrameworkCore;
//using System.Linq;
//using Stripe;


////namespace emirathes.Areas.Admin.Controllers
////{
////    public class SalesController : Controller
////    {
////        public IActionResult Index()
////        {
////            return View();
////        }
////    }
////}






//namespace emirathes.Controllers
//{
//    [Authorize]
//    public class SalesController : Controller
//    {
//        private readonly AppDbContent _appDbContext;
//        private readonly UserManager<ProgramUsers> _userManager;
//        public SalesController(AppDbContent context, UserManager<ProgramUsers> userManager)
//        {
//            _appDbContext = context;
//            _userManager = userManager;
//        }
//        private static Models.Order tempOrder;
//        private static decimal total = 0;
//        [HttpPost]
//        public IActionResult Checkout(int id)
//        {
//            decimal prcntg = 0;
//            if (list == null)
//            {
//                return RedirectToAction("ShopCart", "Cart");
//            }

//            var domain = "https://localhost:7032/";
//            var options = new SessionCreateOptions()
//            {
//                SuccessUrl = domain + $"Shared/OrderConfirmation",
//                CancelUrl = domain + "Shared/Login",
//                LineItems = new List<SessionLineItemOptions>(),
//                Mode = "payment"
//            };
//            var service = new SessionService();
//            Session session = service.Create(options);
//            TempData["Session"] = session.Id;
//            Response.Headers.Add("Location", session.Url);
//            return new StatusCodeResult(303);
//        }





//        //    foreach (var item in list)
//        //    {
//        //        var sessionListItem = new SessionLineItemOptions
//        //        {
//        //            PriceData = new SessionLineItemPriceDataOptions
//        //            {
//        //                UnitAmountDecimal = item.Price * 100 * (1 - prcntg / 100),
//        //                Currency = "usd",
//        //                ProductData = new SessionLineItemPriceDataProductDataOptions
//        //                {
//        //                    Name = item.ProductName
//        //                }
//        //            },
//        //            Quantity = item.Quantity
//        //        };
//        //        total += item.Total * (1 - prcntg / 100);
//        //        options.LineItems.Add(sessionListItem);
//        //    }

//        //}
//        [HttpGet]
//        public IActionResult Billing()
//        {
//            return View();
//        }
//        public async Task<IActionResult> OrderConfirmation()
//        {
//            var service = new SessionService();
//            Session session = service.Get(TempData["Session"].ToString());
//            if (session.PaymentStatus == "paid")
//            {
//                var user = await _userManager.GetUserAsync(User);
//                var random = new Random();
//                tempOrder.UserId = user.Id;
//                tempOrder.OrderNumber = random.Next(100000, 1000000);
//                tempOrder.Date = DateTime.Now;
//                tempOrder.Total = total;

//                _context.Orders.Add(tempOrder);
//                _context.SaveChanges();
//                var cartList = HttpContext.Session.GetJson<List<CartItem>>("Cart");
//                foreach (var item in cartList)
//                {
//                    var orderDetail = new Products
//                    {
//                        CategoryId = tempOrder.Id,
//                        //Id = item.Id,
//                        Title = item.Title,
//                        Baggage = item.Baggage,
//                        CabinBaggage = item.CabinBaggage,

//                    };
//                    _appDbContext.Orders.Add(orderDetail);
//                    var product = _appDbContext.Products.FirstOrDefault(p => p.Id == item.ProductId);
//                    product.Baggage -= item.Baggage;
//                }
//                _appDbContext.SaveChanges();
//                var _order = _appDbContext.Orders.Include(x => x.Orders).ThenInclude(x => x.Product).FirstOrDefault(x => x.Id == tempOrder.Id);
//                return View("Success", _order);
//            }
//            return View("Fail");
//        }



//    }
//}