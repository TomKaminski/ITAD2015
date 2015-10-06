using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Itad2015.Contract.DTO.Base;

namespace Itad2015.ViewModels.Guest
{
    public class RegisterGuestViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="Podano zły adres e-mail")]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public Size Size { get; set; }
        public string Info { get; set; }
    }
}