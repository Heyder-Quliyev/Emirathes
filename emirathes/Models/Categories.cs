using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emirathes.Models
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }
        public string TicketsClassName { get; set; }
        public bool IsActive { get; set; } = true;

        [NotMapped]
        public List<Products> Products { get; set; }

    }
}
