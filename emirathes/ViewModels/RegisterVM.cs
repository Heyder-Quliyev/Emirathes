using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace emirathes.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
        [ValidateNever]
        public bool IsRemember { get; set; }

    }
}
