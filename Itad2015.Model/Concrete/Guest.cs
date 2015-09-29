using System;
using Itad2015.Model.Common;

namespace Itad2015.Model.Concrete
{
    public class Guest: Entity
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? CheckInDate { get; set; }

        public string ConfirmationHash { get; set; }
        public string CancelationHash { get; set; }
        public DateTime RegistrationTime { get; set; }
        public DateTime? ConfirmationTime { get; set; }

        public bool Cancelled { get; set; }

        public int? WorkshopGuestId { get; set; }

        public WorkshopGuest WorkshopGuest { get; set; }
    }
}
