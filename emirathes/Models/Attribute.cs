using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emirathes.Models
{
    public class Attribute
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Baggage { get; set; }
        public string CabinBaggage { get; set; }
       
        //siralama bele olsun
        //CategoryId sonra foreignkey sonra validate never sonra da hemin id yerlesen table
        //Siralama ferqli olanda tanimir ki hansi uchun deyirsiz foreign key

        [ValidateNever]
        public bool IsAvailable { get; set; } = true;

    }
}
