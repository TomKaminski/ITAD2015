using System.Collections.Generic;
using Itad2015.ViewModels.Base;

namespace Itad2015.ViewModels.Home
{
    public class IndexViewModel
    {
        public IndexWorkshopGuestModel IndexWorkshopGuestModel { get; set; }
        public IndexGuestModel IndexGuestModel { get; set; }
        public List<AlertViewModel> Alerts { get; set; }
    }
}