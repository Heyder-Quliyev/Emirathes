using emirathes.Extensions;
using emirathes.Models;
using Microsoft.AspNetCore.Mvc;
using emirathes.Controllers;

using System;

namespace emirathes.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContent appDbContent;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContent _appDbContent, IWebHostEnvironment env)
        {
            appDbContent = _appDbContent;
            _env = env;
        }


        public IActionResult Index()
        {
            return View(appDbContent.Ticktes.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Tickts tickts)
        {

            if (!ModelState.IsValid)
            {
                return View(tickts);
            }
            if (!tickts.File.IsImage())
            {
                ModelState.AddModelError("Photo", "Image type is not valid");
                return View(tickts);
            }
            string filename = await tickts.File.SaveFileAsync(_env.WebRootPath, "uploadSlider");

            tickts.ImgUrl = filename;

            appDbContent.Ticktes.Add(tickts);
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

                var tickts = appDbContent.Ticktes.Find(id); //axtarib tapiram
                if (tickts != null)
                {
                    appDbContent.Ticktes.Remove(tickts);
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
            var model = appDbContent.Ticktes.FirstOrDefault(x => x.Id == id);
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
        public async Task<IActionResult> EditAsync(Tickts tickts)
        {
            var oldSlider = appDbContent.Ticktes.Find(tickts.Id);
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
            oldSlider.Time = tickts.Time;
            oldSlider.Stop = tickts.Stop;
            oldSlider.IsAvailable = tickts.IsAvailable;



            appDbContent.SaveChanges();
            return RedirectToAction("Index");
        }









    }
}
