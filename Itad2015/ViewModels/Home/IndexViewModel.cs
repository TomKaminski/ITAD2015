using System.Collections.Generic;
using Itad2015.ViewModels.Base;
using Itad2015.ViewModels.Guest;

namespace Itad2015.ViewModels.Home
{
    public class IndexViewModel
    {
        public RegisterGuestViewModel RegisterGuestViewModel { get; set; }
        public RegisterWorkshopGuestViewModel RegisterWorkshopGuestViewModel { get; set; }
        public List<AlertViewModel> Alerts { get; set; }
    }
}