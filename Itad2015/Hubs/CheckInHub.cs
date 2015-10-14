using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR;

namespace Itad2015.Hubs
{
    public class CheckInHub:Hub
    {
        private readonly static Dictionary<string,Connections> InMemoryConnections = new Dictionary<string, Connections>();
    }

    public class Connections
    {
        public Guid DeviceConnectionId { get; set; }
        public Guid UserConnectionId { get; set; }
    }
}