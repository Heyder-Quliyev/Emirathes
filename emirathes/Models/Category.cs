using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emirathes.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string TicketsClassName { get; set; }
        public bool IsActive { get; set; } = true;

        //public int? ParentId { get; set; }

        //[ForeignKey("ParentId")]
        //public Category ParentCategory { get; set; }

        public List<Product> Products { get; set; }

    }
}
