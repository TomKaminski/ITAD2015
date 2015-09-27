using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Itad2015.Areas.Admin.ViewModels
{
    public class LoginViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [PasswordPropertyText]
        public string Password { get; set; }

        [Required]
        public bool RemeberMe { get; set; }
    }
}