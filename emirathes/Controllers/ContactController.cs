using emirathes.IRepository;
using emirathes.Models;
using Microsoft.AspNetCore.Mvc;

namespace emirathes.Controllers
{
    public class ContactController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ContactUser obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ContactUser.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Your message sent successfully";
                return RedirectToAction("Index");
            }
            return View();

        }
    }
}