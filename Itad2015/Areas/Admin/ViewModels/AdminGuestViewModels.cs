using System.ComponentModel.DataAnnotations;
using Itad2015.Contract.DTO.Base;

namespace Itad2015.Areas.Admin.ViewModels
{
    public class AdminGuestViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public string Size { get; set; }
        public bool IsCheckIn { get; set; }
        public bool ShirtOrdered { get; set; }
        public bool QrEmailSent { get; set; }
        public int? WorkshopGuestId { get; set; }
    }

    public class AdminWorkshopGuestViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public string Size { get; set; }
        public bool IsCheckIn { get; set; }
        public bool ShirtOrdered { get; set; }
        public bool QrEmailSent { get; set; }
        public int? WorkshopGuestId { get; set; }
    }


    public class AdminCreateGuestViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public Size Size { get; set; }
    }
}