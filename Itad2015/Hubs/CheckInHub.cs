using System;
using Itad2015.Contract.DTO.Api;
using Itad2015.Hubs.ConnectionMappings;
using Microsoft.AspNet.SignalR;

namespace Itad2015.Hubs
{
    public class CheckInHub : Hub
    {
        private static readonly CheckInHubConnectionMapping Connections = new CheckInHubConnectionMapping();

        public void Connect(string key, Guid connectionId, ConnectionType type)
        {
            Connections.Add(key,connectionId,type);
            var connection = Connections.GetConnections(key);
            if (type == ConnectionType.Device)
            {
                Clients.Client(connection.UserConnectionId.ToString()).notifyDeviceConnected();
                if (connection.DeviceConnectionId != Guid.Empty)
                {
                    Clients.Client(connection.DeviceConnectionId.ToString()).notifyUserConnected();
                }
            }
            else
            {
                Clients.Client(connection.UserConnectionId.ToString()).notifyUserConnectedCallback();
                if (connection.DeviceConnectionId != Guid.Empty)
                {
                    Clients.Client(connection.UserConnectionId.ToString()).notifyDeviceConnected();
                }
            }
        }

        public void LockDevice(string key, GuestApiDto data)
        {
            var connection = Connections.GetConnections(key);
            Clients.Client(connection.UserConnectionId.ToString()).lockDevice(data);
        }

        public void UnlockDevice(string key)
        {
            var connection = Connections.GetConnections(key);
            Clients.Client(connection.DeviceConnectionId.ToString()).unlockDevice();
        }

        public void UnlockDeviceUserCallback(string key)
        {
            var connection = Connections.GetConnections(key);
            Clients.Client(connection.UserConnectionId.ToString()).unlockDeviceUserCallback();

        }
    }

    public class Connections
    {
        public Guid DeviceConnectionId { get; set; }
        public Guid UserConnectionId { get; set; }
    }

    public enum ConnectionType
    {
        Device = 0,
        User = 1
    }
}