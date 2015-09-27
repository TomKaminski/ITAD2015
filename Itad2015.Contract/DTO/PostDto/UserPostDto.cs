using Itad2015.Contract.DTO.Base;

namespace Itad2015.Contract.DTO.PostDto
{
    public class UserPostDto : PostBaseDto
    {
        public string Email { get; set; }
        public bool SuperAdmin { get; set; }
        public string DeviceCode { get; set; }
    }
}
