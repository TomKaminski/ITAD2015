using System;
using Itad2015.Contract.DTO.Base;

namespace Itad2015.Contract.DTO.PostDto
{
    public class WorkshopGuestPostDto : PostBaseDto
    {
        public DateTime RegistrationTime { get; set; }
        public DateTime? ConfirmationTime { get; set; }
        public bool Cancelled { get; set; }
        public int GuestId { get; set; }
    }
}
