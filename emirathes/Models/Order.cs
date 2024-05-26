﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emirathes.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Required]
        public decimal Total { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ProgramUsers User { get; set; }


        public List<OrderItem> OrderItems { get; set; }

    }
}
