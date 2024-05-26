using emirathes.Models;
using emirathes.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace emirathes.Controllers
{
    public class TicketController : Controller
    {
        private readonly AppDbContent _context;
        private readonly UserManager<ProgramUsers> _userManager;
        public TicketController(AppDbContent context, UserManager<ProgramUsers> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 12;
            var model = new ShopViewModel()
            {
                Products = await _context.Products.Include(u=>u.Attributes).Where(u=>u.IsAvailable).Include(x => x.Category).Where(p => p.IsAvailable).ToPagedListAsync(pageNumber, pageSize),
                Categories = _context.Categories.ToList(),
                Attributes = _context.Attributes.ToList(),
                MaxPrice = await _context.Products.MaxAsync(p => p.Price)
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> GetProducts(int min, int max)
        {

            var query = _context.Products.Include(p => p.Category).Include(p => p.Attributes).Where(p => p.IsAvailable == true);

           
            if (max != 0)
            {
                query = query.Where(p => (p.Price <= max && p.Price >= min));
            }
            var products = await query.ToListAsync();
            return PartialView("_ProductList", products);
        }

        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await _context.Products.Include(u=>u.Category).Include(a => a.Attributes).FirstOrDefaultAsync(p => p.Id == id);
            var user = await _userManager.GetUserAsync(User);
            var model = new ProductDetailsVM()
            {
                Product = product,
                RelatedProducts = await _context.Products.Where(p => p.CategoryId == product.Id && p.Id != product.Id).Take(5).ToListAsync(),
                NextProductId = _context.Products.Where(p => p.Id > id && p.IsAvailable).Min(p => (int?)p.Id),
                PreviousProductId = _context.Products.Where(p => p.Id < id && p.IsAvailable).Max(p => (int?)p.Id)
                //ReviewCount = reviewCount,
                //Rating = rating
            };

            return View(model);
        }


        //[HttpPost]
        //public IActionResult AddReview(Review review)
        //{
        //    int id = review.ProductId;
        //    review.Date = DateTime.Now;
        //    _context.Reviews.Add(review);
        //    _context.SaveChanges();
        //    return Json("Added.");

        //    //return RedirectToAction("ProductDetails", new { id = id });
        //}




    }

}
