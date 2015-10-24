using System.Collections.Generic;

namespace Itad2015.Areas.Admin.ViewModels
{
    public class WorkshopGuestViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<WorkshopGuestExtendedItemViewModel> Guests { get; set; }
    }

    public class WorkshopGuestExtendedViewModel
    {
        public int GuestId { get; set; }
        public string SchoolName { get; set; }
        public int WorkshopId { get; set; }
        public AdminGuestViewModel Guest { get; set; }

    }

    public class WorkshopGuestExtendedItemViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? WorkshopGuestId { get; set; }
        public string SchoolName { get; set; }
    }
}