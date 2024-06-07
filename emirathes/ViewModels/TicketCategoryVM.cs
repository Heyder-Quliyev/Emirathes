using emirathes.Models;
namespace emirathes.ViewModels
{
    public class TicketCategoryVM
    {
        public Product Tickts { get; set; }
        public List<Category> Categories { get; set; }
        public List<Models.Attribute> Products { get; set; }
        public List<Passengers> Passengers { get; set; }
        public Category Category { get; set; }



    }
}
