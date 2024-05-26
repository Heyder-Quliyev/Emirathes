using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emirathes.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }
        public string Baggage { get; set; }
        public string CabinBaggage { get; set; }
        public DateTime Date {  get; set; }
        public DateTime LandingDate { get; set; }
        public int TicketId { get; set; }
        [ForeignKey("TicketId")]
        [ValidateNever]
        public Product Tickets { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        [ValidateNever]
        public Order Order { get; set; }
    }
}
