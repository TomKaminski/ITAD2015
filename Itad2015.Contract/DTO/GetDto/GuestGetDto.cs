using System;
using Itad2015.Contract.DTO.Base;

namespace Itad2015.Contract.DTO.GetDto
{
    public class GuestGetDto:GetBaseDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? CheckInDate { get; set; }
        public bool Cancelled { get; set; }
        public int? WorkshopGuestId { get; set; }
        public DateTime RegistrationTime { get; set; }
        public DateTime? ConfirmationTime { get; set; }
    }
}
