using System;
using Itad2015.Contract.DTO.Base;

namespace Itad2015.Contract.DTO.GetDto
{
    public class ConnectedDeviceGetDto : GetBaseDto
    {
        public int Id { get; set; }
        public string UserSignalRId { get; set; }
        public string DeviceSignalRId { get; set; }
        public int UserId { get; set; }
    }
}
