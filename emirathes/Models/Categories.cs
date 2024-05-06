namespace emirathes.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string TicketsClassName { get; set; }
        public bool IsActive { get; set; }

        public List<Products> Products { get; set; }


    }
}
