using System.ComponentModel.DataAnnotations;
using Itad2015.Contract.DTO.Base;

namespace Itad2015.ViewModels.Guest
{
    public class RegisterGuestViewModel
    {
        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress(ErrorMessage ="Podano zły adres e-mail")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Imię jest wymagane")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Wybierz rozmiar koszulki")]
        public Size Size { get; set; }
        public string Info { get; set; }
    }
}