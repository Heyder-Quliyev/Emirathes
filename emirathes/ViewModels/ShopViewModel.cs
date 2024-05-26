using emirathes.Models;
using System.Drawing;
using X.PagedList;

namespace emirathes.ViewModels
{
    public class ShopViewModel
    {
        public IPagedList<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<Models.Attribute> Attributes { get; set; }
        public decimal MaxPrice { get; set; }
        public bool HasPreviousPage => !Products.IsFirstPage;
        public bool HasNextPage => !Products.IsLastPage;
    }
}
