 using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace emirathes.ViewModels
{
    public class LoginVM
    { 
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]

        public string Password { get; set; }
        [Required]
        [Compare("Password")]

        [ValidateNever]
        public bool IsRemember { get; set; }



    }
}
