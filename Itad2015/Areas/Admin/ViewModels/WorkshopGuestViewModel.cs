using System.Collections.Generic;
using Itad2015.Contract.DTO.GetDto;

namespace Itad2015.Areas.Admin.ViewModels
{
    public class WorkshopGuestViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<AdminGuestViewModel> Guests { get; set; }
    }

    public class WorkshopGuestExtendedViewModel
    {
        public int GuestId { get; set; }
        public string SchoolName { get; set; }
        public int WorkshopId { get; set; }
        public AdminGuestViewModel Guest { get; set; }
    }
}