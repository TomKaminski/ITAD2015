using System;
using System.ComponentModel.DataAnnotations;

namespace Itad2015.ViewModels.Guest
{
    public class RegisterGuestViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}