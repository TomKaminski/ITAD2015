using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR;

namespace Itad2015.Hubs
{
    public class CheckInHub:Hub
    {
        
    }

    public class Connections
    {
        public Guid DeviceConnectionId { get; set; }
        public Guid UserConnectionId { get; set; }
    }

    public enum ConnectionType
    {
        Device,
        User
    }
}