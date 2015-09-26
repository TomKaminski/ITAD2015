using System;
using System.ComponentModel.DataAnnotations.Schema;
using Itad2015.Model.Common;

namespace Itad2015.Model.Concrete
{
    public class WorkshopGuest:Entity
    {
        public string ConfirmationHash { get; set; }
        public string CancelationHash { get; set; }
        public DateTime RegistrationTime { get; set; }
        public DateTime? ConfirmationTime { get; set; }

        public bool Cancelled { get; set; }

        public int GuestId { get; set; }
        public Guest Guest { get; set; }
    }
}
