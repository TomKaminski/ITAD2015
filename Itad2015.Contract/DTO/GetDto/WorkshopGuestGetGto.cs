using Itad2015.Contract.DTO.Base;

namespace Itad2015.Contract.DTO.GetDto
{
    public class WorkshopGuestGetGto : GetBaseDto
    {
        public int GuestId { get; set; }
        public string SchoolName { get; set; }
        public int WorkshopId { get; set; }
    }

    public class WorkshopGuestExtendedGetDto: WorkshopGuestGetGto
    {
        public GuestGetDto Guest { get; set; }
    }
}
