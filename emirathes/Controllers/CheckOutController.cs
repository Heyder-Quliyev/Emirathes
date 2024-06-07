using DocuSign.eSign.Model;
using emirathes.Extensions;
using emirathes.Models;
using emirathes.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using System;

namespace emirathes.Controllers
{
    [Authorize]
    public class CheckOutController : Controller
    {
        //private readonly AppDbContent _context;
        //private readonly UserManager<ProgramUsers> _userManager;
        //public CheckOutController(AppDbContent context, UserManager<ProgramUsers> userManager)
        //{
        //    _context = context;
        //    _userManager = userManager;
        //}
        //private static Order tempOrder;
        //private static decimal total = 0;
        //[HttpPost]
        //public IActionResult Checkout(Order order, string code)
        //{

        //    decimal prcntg = 0;

        //    if (code != null)
        //    {
        //        var proCode = _context.Promotions.FirstOrDefault(p => p.Code == code);
        //        prcntg = proCode == null ? 0 : proCode.DiscountPercentage;
        //    }
        //    tempOrder = order;
        //    var domain = "https://localhost:7032/";
        //    var options = new SessionCreateOptions()
        //    {
        //        SuccessUrl = domain + $"CheckOut/OrderConfirmation",
        //        CancelUrl = domain + "CheckOut/Login",
        //        LineItems = new List<SessionLineItemOptions>(),
        //        Mode = "payment"
        //    };

        //    var sessionListItem = new SessionLineItemOptions
        //    {
        //        PriceData = new SessionLineItemPriceDataOptions
        //        {
        //            UnitAmountDecimal = order.Price * 100 * (1 - prcntg / 100),
        //            Currency = "usd",
        //            ProductData = new SessionLineItemPriceDataProductDataOptions
        //            {
        //                Name = order.FirstName
        //            }
        //        },
        //        Quantity = order.Quantity,
        //    };
        //    total += order.Total * (1 - prcntg / 100);
        //    options.LineItems.Add(sessionListItem);

        //    var service = new SessionService();
        //    Session session = service.Create(options);
        //    TempData["Session"] = session.Id;
        //    Response.Headers.Add("Location", session.Url);
        //    return new StatusCodeResult(303);
        //}
        //[HttpGet]
        //public IActionResult Billing()
        //{
        //    return View();
        //}
        //public async Task<IActionResult> OrderConfirmation()
        //{
        //    var service = new SessionService();
        //    Session session = service.Get(TempData["Session"].ToString());
        //    if (session.PaymentStatus == "paid")
        //    {
        //        var user = await _userManager.GetUserAsync(User);
        //        var random = new Random();
        //        tempOrder.UserId = user.Id;
        //        tempOrder.OrderNumber = random.Next(100000, 1000000);
        //        tempOrder.Date = DateTime.Now;
        //        tempOrder.Total = total;

        //        _context.Orders.Add(tempOrder);
        //        _context.SaveChanges();
        //        var cartList = HttpContext.Session.GetJson<List<CartItem>>("Cart");
        //        foreach (var item in cartList)
        //        {
        //            var orderDetail = new OrderItem
        //            {
        //                OrderId = tempOrder.Id,
        //                TicketId = item.ProductId,
        //                Quantity = item.Quantity,
        //                Subtotal = item.Total,
        //                Baggage = item.Baggage,
        //                CabinBaggage = item.CabinBaggage,
        //                Date = item.Date,
        //                LandingDate = item.LandigTime
        //            };
        //            _context.OrderItems.Add(orderDetail);
        //            var product = _context.Products.FirstOrDefault(p => p.Id == item.ProductId);
        //            product.StockQuantity -= item.Quantity;
        //        }
        //        _context.SaveChanges();
        //        var _order = _context.Orders.Include(x => x.OrderItems).ThenInclude(x => x.Tickets).FirstOrDefault(x => x.Id == tempOrder.Id);
        //        return View("Success", _order);
        //    }
        //    return View("Fail");
        //}


        //public IActionResult Success()
        //{
        //    return View(); // By default, this looks for Views/CheckOut/Success.cshtml
        //}

        private readonly AppDbContent _content;
        private readonly UserManager<ProgramUsers> _userManager;

        public CheckOutController(AppDbContent context, UserManager<ProgramUsers> userManager)
        {
            _content = context;
            _userManager = userManager;
        }

        public IActionResult Checkout(Order order, string code)
        {
              decimal prcntg = 0;

            if (code != null)
            {
                var proCode = _content.Promotions.FirstOrDefault(p => p.Code == code);
                prcntg = proCode == null ? 0 : proCode.DiscountPercentage;
            }

            var domain = "https://localhost:7032/";
            var options = new SessionCreateOptions
            {
                //SuccessUrl = domain + $"Vip/OrderConfirmation?id={id}",
                //CancelUrl = domain + "Vip/Login",
                //LineItems = new List<SessionLineItemOptions>

                SuccessUrl = domain + $"CheckOut/OrderConfirmation",
                CancelUrl = domain + "CheckOut/Login",
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = 1000, // Example amount in cents (10.00 USD)
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Total Price"
                        }
                    },
                    Quantity = 1
                }
            },
                Mode = "payment"
            };

            var service = new SessionService();
            Session session = service.Create(options);
            TempData["Session"] = session.Id;
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        public async Task<IActionResult> OrderConfirmation(int id)
        {
            var service = new SessionService();
            Session session = service.Get(TempData["Session"].ToString());

            if (session.PaymentStatus == "paid")
            {
                var product = await _content.Products.FindAsync(id);
                if (product != null)
                {
                    product.IsAvailable = true;
                    _content.Products.Update(product);
                    await _content.SaveChangesAsync();
                }

                return View("Success");
            }

            return View("Fail");
        }


       






















    }
}
