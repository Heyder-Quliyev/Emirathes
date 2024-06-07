using emirathes.Models;

namespace emirathes.ViewModels
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string FlightNumber { get; set; }
        public string ProductFrom { get; set; }
        public string ProductTo { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public DateTime LandigTime { get; set; }
        public string Baggage { get; set; }
        public string CabinBaggage { get; set; }
        public decimal Total
        {
            get { return Quantity * Price; }
        }

        public CartItem()
        {
        }

        public CartItem(Product product)
        {
            ProductId = product.Id;
            FlightNumber = product.FlightNumber;
            ProductFrom = product.From;
            ProductTo = product.To;
            Price = product.Price;
            
        }
    }
}
