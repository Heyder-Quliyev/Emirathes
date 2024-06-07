﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emirathes.Models
{
    public class ForgotPassword
    {
        [Required(ErrorMessage = "Not Empty")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Please, Write Correctly")]
        public string? Email { get; set; }
    }
}