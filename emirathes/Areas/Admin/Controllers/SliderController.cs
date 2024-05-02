﻿using emirathes.Extensions;
using emirathes.Models;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var tickts = appDbContent.Ticktes.Find(id);
            if (tickts != null)
            {
                appDbContent.Ticktes.Remove(tickts);
                appDbContent.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        //[HttpGet]
        //public JsonResult Edit(int id)
        //{
        //    if (id == 0)
        //    {
        //        return Json;
        //    }
        //    var model = appDbContent.Ticktes.FirstOrDefault(x => x.Id == id);
        //    if (model == null)
        //    {
        //        return RedirectToAction("Index");

        //    }
        //    return View(model);

        //}






        [HttpGet]
        public JsonResult Edit(int id)
        {
            if (id == 0)
            {
                return Json(new
                {
                    status = 400
                });
            }
            var model = appDbContent.Ticktes.FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return Json(new
                {
                    status = 400
                });
            }
            return Json(model);
        }




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
            oldSlider.Destiantion = tickts.Destiantion;
            oldSlider.Way = tickts.Way;
            oldSlider.Classes = tickts.Classes;
            oldSlider.FlightNumber = tickts.FlightNumber;
            oldSlider.Price = tickts.Price;
            oldSlider.Date = tickts.Date;
            oldSlider.LandigTime = tickts.LandigTime;
            oldSlider.Time = tickts.Time;
            oldSlider.Stop = tickts.Stop;



            appDbContent.SaveChanges();
            return RedirectToAction("Index");
        }









    }
}
