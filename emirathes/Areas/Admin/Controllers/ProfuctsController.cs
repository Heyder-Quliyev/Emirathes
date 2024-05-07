using emirathes.Extensions;
using emirathes.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace emirathes.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly AppDbContent appDbContent;

        public ProductsController(AppDbContent _appDbContent)
        {
            appDbContent = _appDbContent;
        }
        public IActionResult Index()
        {
            return View(appDbContent.Products.Include(x => x.Category).ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Category = appDbContent.Categories.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Products model)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Category = appDbContent.Categories.ToList();
                return View(model);
            }

            appDbContent.Products.Add(model);
            appDbContent.SaveChanges();
            return RedirectToAction("Index");
        }





        public IActionResult Edit(int id)
        {
            ViewBag.Category = appDbContent.Categories.ToList();
            var model = appDbContent.Products.FirstOrDefault(x => x.Id == id);

            return View(model);
        }


        [HttpPost]
        public IActionResult Edit(Products products)
        {
            ViewBag.Categories = appDbContent.Categories.ToList();
            if (!ModelState.IsValid)
            {
                return View(products);
            }

            appDbContent.Products.Update(products);
            appDbContent.SaveChanges();
            return RedirectToAction("Index");
        }


















        public JsonResult Delete(int id)
        {
            if (id == 0)
            {
                return Json(new
                {
                    status = 400
                });
            }

            var products = appDbContent.Products.Find(id); //axtarib tapiram
            if (products != null)
            {
                if (products.IsAvailable == true)
                {
                    products.IsAvailable = false;
                }
                else
                {
                    products.IsAvailable = true;
                }
                appDbContent.Products.Remove(products);
                appDbContent.SaveChanges();
            }

            return Json(new
            {
                status = 200
            });

        }







    }
}
