using System.ComponentModel.DataAnnotations;

namespace Itad2015.Areas.Admin.ViewModels
{
    public class UserListViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool SuperAdmin { get; set; }
    }

    public class UserCreateViewModel
    {
        [Required]
        public string Email { get; set; }

        public bool SuperAdmin { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Compare("Password")]
        public string RepeatPassword { get; set; }
    }

    public class UserEditViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }

        public bool SuperAdmin { get; set; }

        [MinLength(8)]
        public string Password { get; set; }
        [Compare("Password")]
        public string RepeatPassword { get; set; }
    }
}