using System;
using Itad2015.Contract.DTO.Base;

namespace Itad2015.Contract.DTO.GetDto
{
    public class GuestGetDto : GetBaseDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? CheckInDate { get; set; }
        public Size Size { get; set; }
        public bool Cancelled { get; set; }
        public int? WorkshopGuestId { get; set; }
        public DateTime RegistrationTime { get; set; }
        public DateTime? ConfirmationTime { get; set; }
        public string ConfirmationHash { get; set; }
        public string CancelationHash { get; set; }
        public string Info { get; set; }
        public bool ShirtOrdered { get; set; }
        public bool QrEmailSent { get; set; }
        public bool AgendaEmailSent { get; set; }
        public bool PolicyAccepted { get; set; }
    }


    public class GuestShirtGetDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Size Size { get; set; }
    }
}
