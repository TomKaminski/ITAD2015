using Itad2015.Contract.DTO.Base;

namespace Itad2015.Contract.DTO.PostDto
{
    public class ConnectedDevicePostDto : PostBaseDto
    {
        public string UserSignalRId { get; set; }
        public string DeviceSignalRId { get; set; }
        public int UserId { get; set; }
    }
}
