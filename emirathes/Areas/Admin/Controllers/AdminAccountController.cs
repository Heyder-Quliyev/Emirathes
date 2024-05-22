using Microsoft.AspNetCore.Mvc;

namespace emirathes.Areas.Admin.Controllers
{
    public class AdminAccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
