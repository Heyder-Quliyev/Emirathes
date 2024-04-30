using emirathes.Models;
using Microsoft.AspNetCore.Mvc;

namespace emirathes.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContent appDbContent;
        public SliderController (AppDbContent _appDbContent)
        {
            appDbContent = _appDbContent;
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

        public IActionResult Create(Tickts tickts)
        {

            if(!ModelState.IsValid)
            {
                return View(tickts);
            }
            appDbContent.Ticktes.Add(tickts);
            appDbContent.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        { 
        if(id == 0)
            {
                return NotFound();
            }
            var tickts = appDbContent.Ticktes.Find(id);
            if(tickts != null)
            {
                appDbContent.Ticktes.Remove(tickts);
                appDbContent.SaveChanges();
            }
            return RedirectToAction("Index");
        
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var model = appDbContent.Ticktes.FirstOrDefault(x=>x.Id == id);
            if (model==null)
            {
                return RedirectToAction("Index");

            }
            return View(model);

        }

        [HttpPost]
        public IActionResult Edit(Tickts tickts)
        {
            if (!ModelState.IsValid)
            {
                return View(tickts);
            }
            appDbContent.Ticktes.Update(tickts);
            appDbContent.SaveChanges();

            return RedirectToAction("Index");

        }









    }
}
