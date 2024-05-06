using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace emirathes.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Baggage { get; set; }
        public string CabinBaggage { get; set; }
       
        public double Price { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Categories Category { get; set; }
        //public List<Order> Orders { get; set; }







        public bool IsAvailable { get; set; } = false;

    }
}
