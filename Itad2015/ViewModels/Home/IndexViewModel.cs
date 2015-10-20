using System.Collections.Generic;
using Itad2015.ViewModels.Base;

namespace Itad2015.ViewModels.Home
{
    public class IndexViewModel
    {
        public IndexGuestModel IndexGuestModel { get; set; }
        public List<AlertViewModel> Alerts { get; set; }
    }
}