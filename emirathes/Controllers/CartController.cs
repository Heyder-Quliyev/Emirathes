﻿using emirathes.Extensions;
using emirathes.Models;
using emirathes.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace emirathes.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContent _context;
        public CartController(AppDbContent context)
        {
            _context = context;
        }
        public IActionResult ShopCart()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel cartVM = new()
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Quantity * x.Price)
            };

            return View(cartVM);
        }
        [HttpPost]
        public async Task<IActionResult> Add(CartItem cartItem)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == cartItem.ProductId);

            if (product.StockQuantity < cartItem.Quantity)
            {
                return RedirectToAction("ProductDetails", "Ticket", new { id = product.Id });
            }
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartItem item = cart.FirstOrDefault(c => c.ProductId == cartItem.ProductId);

            if (item == null)
            {
                var newItem = new CartItem(product);
                newItem.Quantity = cartItem.Quantity;
                newItem.Baggage = cartItem.Baggage;
                newItem.CabinBaggage = cartItem.CabinBaggage;
                newItem.Date = cartItem.Date;
                newItem.LandigTime = cartItem.LandigTime;

                cart.Add(newItem);
            }
            else
            {
                item.Quantity += cartItem.Quantity;
            }

            HttpContext.Session.SetJson("Cart", cart);

            TempData["Success"] = "The ticket has been added!";
            //return Json(new { success = true });
            return RedirectToAction("ProductDetails", "Ticket", new { id = product.Id });
        }
        [HttpPost]
        public async Task<IActionResult> Increase(int id)
        {
            var product = await _context.Products.FindAsync(id);

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();

            if (cartItem == null)
            {
                cart.Add(new CartItem(product));
            }
            else
            {
                cartItem.Quantity += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);

            TempData["Success"] = "The ticket has been added!";
            return Json(new { success = true });
            //return RedirectToAction("ShopCart");
        }
        public IActionResult Decrease(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(p => p.ProductId == id);
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "The ticket has been removed!";
            return Json(new { success = true });

            //return RedirectToAction("ShopCart");
        }

        public async Task<IActionResult> Remove(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            cart.RemoveAll(p => p.ProductId == id);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "The ticket has been removed!";
            return Json(new { success = true });
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");
            return Json(new { success = true });
        }
    }
}
