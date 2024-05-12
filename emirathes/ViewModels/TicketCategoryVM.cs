using emirathes.Models;

namespace emirathes.ViewModels
{
    public class TicketCategoryVM
    {
        public Tickts Tickts { get; set; }
        public List<Categories> Categories { get; set; }
        public List<Products> Products { get; set; }
        public List<Passengers> Passengers { get; set; }

    }
}
