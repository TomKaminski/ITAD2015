using System;
using Itad2015.Contract.DTO.Base;

namespace Itad2015.Contract.DTO.GetDto
{
    public class WorkshopGuestGetGto : GetBaseDto
    {
        public DateTime RegistrationTime { get; set; }
        public DateTime? ConfirmationTime { get; set; }
        public bool Cancelled { get; set; }
        public int GuestId { get; set; }
        public string SchoolName { get; set; }
        public int WorkshopId { get; set; }
    }
}
