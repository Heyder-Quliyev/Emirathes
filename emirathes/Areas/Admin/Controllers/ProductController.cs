using emirathes.Extensions;
using emirathes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace emirathes.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize (Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContent appDbContent;
        private readonly IWebHostEnvironment _env;
        public ProductController(AppDbContent _appDbContent, IWebHostEnvironment env)
        {
            appDbContent = _appDbContent;
            _env = env;
        }


        public IActionResult Index()
        {
            return View(appDbContent.Products.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories=appDbContent.Categories.ToList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product tickts)
        {

            //if (!ModelState.IsValid)
            //{
            //    return View(tickts);
            //}

            if (!tickts.File.IsImage())
            {
                ModelState.AddModelError("Photo", "Image type is not valid");
                return View(tickts);
            }

            string filename = await tickts.File.SaveFileAsync(_env.WebRootPath, "uploadSlider");

            tickts.ImgUrl = filename;

            appDbContent.Products.Add(tickts);
            appDbContent.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult Delete(int id)
        {
            if (id == 0)
            {
                return Json(new
                {
                    status = 400
                });
            }

            var tickts = appDbContent.Products.Find(id); //axtarib tapiram
            if (tickts != null)
            {
                appDbContent.Products.Remove(tickts);
                appDbContent.SaveChanges();
                return Json(new
                {
                    status = 200
                });
            }

            return Json(new
            {
                status = 400
            });

        }

        ///BU isaviable uchun qals;n


        //public JsonResult Delete(int id)
        //{
        //    if (id == 0)
        //    {
        //        return Json(new
        //        {
        //            status = 400
        //        });
        //    }

        //    var slider = appDbContent.Ticktes.Find(id); //axtarib tapiram
        //    if (slider != null)
        //    {
        //        if (slider.IsAvailable == true)
        //        {
        //            slider.IsAvailable = false;
        //        }
        //        else
        //        {
        //            slider.IsAvailable = true;
        //        }
        //        appDbContent.Update(slider)
        //        appDbContent.SaveChanges();
        //    }

        //    return Json(new
        //    {
        //        status = 200
        //    });







        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return View("index");
            }
            var model = appDbContent.Products.FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return RedirectToAction("Index");

            }
            return View(model);

        }


        //public IActionResult Edit(Tickts tickts)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return View(tickts);
        //    }
        //    appDbContent.Ticktes.Update(tickts);
        //    appDbContent.SaveChanges();

        //    return RedirectToAction("Index");

        //}




        [HttpPost]
        public async Task<IActionResult> EditAsync(Product tickts)
        {
            var oldSlider = appDbContent.Products.Find(tickts.Id);
            //if (!ModelState.IsValid)
            //{
            //    return View(slider);
            //}
            if (tickts.File != null)
            {

                if (!tickts.File.IsImage())
                {
                    ModelState.AddModelError("Photo", "Image type is not valid");
                    return View(tickts);
                }
                string filename = await tickts.File.SaveFileAsync(_env.WebRootPath, "uploadSlider");

                oldSlider.ImgUrl = filename;
            }
            oldSlider.From = tickts.From;
            oldSlider.To = tickts.To;
            oldSlider.Way = tickts.Way;
            oldSlider.Classes = tickts.Classes;
            oldSlider.FlightNumber = tickts.FlightNumber;
            oldSlider.Price = tickts.Price;
            oldSlider.Date = tickts.Date;
            oldSlider.LandigTime = tickts.LandigTime;
            oldSlider.TotalTime = tickts.TotalTime;
            oldSlider.Stop = tickts.Stop;
            oldSlider.IsAvailable = tickts.IsAvailable;



            appDbContent.SaveChanges();
            return RedirectToAction("Index");
        }









    }
}
