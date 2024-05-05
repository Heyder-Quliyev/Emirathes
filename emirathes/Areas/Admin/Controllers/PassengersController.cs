using emirathes.Extensions;
using emirathes.Migrations;
using emirathes.Models;
using Microsoft.AspNetCore.Mvc;

namespace emirathes.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PassengersController : Controller
    {


        private readonly AppDbContent appDbContent;

        public PassengersController(AppDbContent _appDbContent)
        {
            appDbContent = _appDbContent;
           
        }








        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Passengers passengers)
        {

            //if (!ModelState.IsValid)
            //{
            //    return View(passengers);
            //}

            appDbContent.Passengers.Add(passengers);
            appDbContent.SaveChanges();
            return RedirectToAction("Index");
        }



        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var Passengers = appDbContent.Passengers.Find(id);
            if (Passengers != null)
            {
                appDbContent.Passengers.Remove(Passengers);
                appDbContent.SaveChanges();
            }
            return RedirectToAction("Index");

        }






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
            var model = appDbContent.Passengers.FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return Json(new
                {
                    status = 400
                });
            }
            return Json(model);
        }








        public IActionResult Index()
        {
            return View(appDbContent.Passengers.ToList());
        }



















    }
}
