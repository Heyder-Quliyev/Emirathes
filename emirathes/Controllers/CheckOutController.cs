using emirathes.Extensions;
using emirathes.Models;
using emirathes.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;

namespace emirathes.Controllers
{
    [Authorize]
    public class CheckOutController : Controller
    {
        private readonly AppDbContent _context;
        private readonly UserManager<ProgramUsers> _userManager;
        public CheckOutController(AppDbContent context, UserManager<ProgramUsers> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private static Order tempOrder;
        private static decimal total = 0;
        [HttpPost]
        public IActionResult Checkout(Order order, string code)
        {
            var list = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            decimal prcntg = 0;
            if (list == null)
            {
                return RedirectToAction("ShopCart", "Cart");
            }
            if (code != null)
            {
                var proCode = _context.Promotions.FirstOrDefault(p => p.Code == code);
                prcntg = proCode == null ? 0 : proCode.DiscountPercentage;
            }
            tempOrder = order;
            var domain = "https://localhost:7032/";
            var options = new SessionCreateOptions()
            {
                SuccessUrl = domain + $"CheckOut/OrderConfirmation",
                CancelUrl = domain + "CheckOut/Login",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment"
            };
            foreach (var item in list)
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmountDecimal = item.Price * 100 * (1 - prcntg / 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.FlightNumber
                        }
                    },
                    Quantity = item.Quantity
                };
                total += item.Total * (1 - prcntg / 100);
                options.LineItems.Add(sessionListItem);
            }
            var service = new SessionService();
            Session session = service.Create(options);
            TempData["Session"] = session.Id;
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
        [HttpGet]
        public IActionResult Billing()
        {
            return View();
        }
        public async Task<IActionResult> OrderConfirmation()
        {
            var service = new SessionService();
            Session session = service.Get(TempData["Session"].ToString());
            if (session.PaymentStatus == "paid")
            {
                var user = await _userManager.GetUserAsync(User);
                var random = new Random();
                tempOrder.UserId = user.Id;
                tempOrder.OrderNumber = random.Next(100000, 1000000);
                tempOrder.Date = DateTime.Now;
                tempOrder.Total = total;

                _context.Orders.Add(tempOrder);
                _context.SaveChanges();
                var cartList = HttpContext.Session.GetJson<List<CartItem>>("Cart");
                foreach (var item in cartList)
                {
                    var orderDetail = new OrderItem
                    {
                        OrderId = tempOrder.Id,
                        TicketId = item.ProductId,
                        Quantity = item.Quantity,
                        Subtotal = item.Total,
                        Baggage = item.Baggage,
                        CabinBaggage = item.CabinBaggage,
                        Date = item.Date,
                        LandingDate = item.LandigTime
                    };
                    _context.OrderItems.Add(orderDetail);
                    var product = _context.Products.FirstOrDefault(p => p.Id == item.ProductId);
                    product.StockQuantity -= item.Quantity;
                }
                _context.SaveChanges();
                var _order = _context.Orders.Include(x => x.OrderItems).ThenInclude(x => x.Tickets).FirstOrDefault(x => x.Id == tempOrder.Id);
                return View("Success", _order);
            }
            return View("Fail");
        }



    }
}
