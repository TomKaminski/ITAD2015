using Itad2015.Contract.DTO.Base;

namespace Itad2015.Contract.DTO.GetDto
{
    public class UserGetDto : GetBaseDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool SuperAdmin { get; set; }
        public string ConnectedDeviceId { get; set; }
    }
}
