using System.ComponentModel.DataAnnotations;
using Itad2015.ViewModels.Guest;

namespace Itad2015.ViewModels
{
    public class RegisterWorkshopGuestViewModel:RegisterGuestViewModel
    {
        [Required]
        public string SchoolName { get; set; }
        [Required]
        public int WorkshopId { get; set; }
    }
}