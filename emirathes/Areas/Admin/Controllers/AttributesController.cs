using emirathes.Extensions;
using emirathes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace emirathes.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class AttributesController : Controller
    {
        private readonly AppDbContent appDbContent;

        public AttributesController(AppDbContent _appDbContent)
        {
            appDbContent = _appDbContent;
        }
        public IActionResult Index()
        {
            return View(appDbContent.Attributes.ToList());
        }



        public IActionResult Create()
        {
            ViewBag.Category = appDbContent.Categories.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Models.Attribute model)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Category = appDbContent.Categories.ToList();
                return View(model);
            }

            appDbContent.Attributes.Add(model);
            appDbContent.SaveChanges();
            return RedirectToAction("Index");
        }




        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Category = appDbContent.Categories.ToList();
            var model = appDbContent.Attributes.FirstOrDefault(x => x.Id == id);

            return View(model);
        }


        [HttpPost]
        public IActionResult Edit(Models.Attribute products)
        {
            ViewBag.Categories = appDbContent.Categories.ToList();
            if (!ModelState.IsValid)
            {
                return View(products);
            }

            appDbContent.Attributes.Update(products);
            appDbContent.SaveChanges();
            return RedirectToAction("Index");
        }


        //public IActionResult Edit(int id)
        //{
        //    if (id == 0)
        //    {
        //        return View("index");
        //    }
        //    var model = appDbContent.Products.FirstOrDefault(x => x.Id == id);
        //    if (model == null)
        //    {
        //        return RedirectToAction("Index");

        //    }
        //    return View(model);

        //}

        //[HttpPost]
        //public IActionResult Edit(Products products)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return View(products);
        //    }
        //    appDbContent.Products.Update(products);
        //    appDbContent.SaveChanges();
        //    return RedirectToAction("Index");

        //}

        public JsonResult Delete(int id)
        {
            if (id == 0)
            {
                return Json(new
                {
                    status = 400
                });
            }

            var products = appDbContent.Attributes.Find(id); //axtarib tapiram
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
                appDbContent.Attributes.Remove(products);
                appDbContent.SaveChanges();
            }

            return Json(new
            {
                status = 200
            });

        }







    }
}
