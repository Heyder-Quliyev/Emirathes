﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emirathes.Models
{
    public class Passengers
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public double Percentage { get; set; }
        public bool IsPassenger { get; set; } = true;


    }
}
