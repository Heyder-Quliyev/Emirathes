using System.ComponentModel.DataAnnotations;

namespace emirathes.Models
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }
        public string TicketsClassName { get; set; }
        public bool IsActive { get; set; } = true;

        public List<Products> Products { get; set; }


    }
}
