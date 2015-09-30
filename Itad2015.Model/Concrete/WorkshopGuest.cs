using Itad2015.Model.Common;

namespace Itad2015.Model.Concrete
{
    public class WorkshopGuest:Entity
    {
        public string SchoolName { get; set; }
        public int GuestId { get; set; }
        public Guest Guest { get; set; }
        public int WorkshopId { get; set; }
        public Workshop Workshop { get; set; }
    }
}
