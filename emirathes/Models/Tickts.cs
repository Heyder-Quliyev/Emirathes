using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emirathes.Models
{
    public class Tickts
    {
        public int Id { get; set; }
       
        [Required]
        public string From { get; set; }
        [Required]
        public string To { get; set; }

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
        public string Stop {get; set; }
        public string? ImgUrl { get; set; }
        [NotMapped]
        [ValidateNever]
        public IFormFile File { get; set; }
        public bool IsAvailable { get; set; } = false;



    }
}
