using System.ComponentModel.DataAnnotations;
using Itad2015.ViewModels.Guest;

namespace Itad2015.ViewModels
{
    public class RegisterWorkshopGuestViewModel : RegisterGuestViewModel
    {
        [Required(ErrorMessage = "Podaj nazwę szkoły")]
        public string SchoolName { get; set; }
        [Required(ErrorMessage = "Wybierz warsztat w którym chciałbyś uczestniczyć")]
        public int WorkshopId { get; set; }
    }
}