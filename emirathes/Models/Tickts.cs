using System.ComponentModel.DataAnnotations;

namespace emirathes.Models
{
    public class Tickts
    {
        public int Id { get; set; }
        [Required]
        public string Destiantion { get; set; }
        [Required]
        public string Way { get; set; }
        [Required]
        public string Classes { get; set; }
        [Required]
        public string FlightNumber { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime LandigTime { get; set; }

        [Required]
        public DateTime Time { get; set; }
        public string? ImgUrl { get; set; }

        public bool IsAvailable { get; set; } = false;

       //public IFormFile FormFile { get; set; }  
    }
}
