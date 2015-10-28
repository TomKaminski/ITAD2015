using System;
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
                Clients.Client(connection.DeviceConnectionId.ToString()).notifyDeviceConnectedCallback();
                if (connection.DeviceConnectionId != Guid.Empty)
                {
                    Clients.Client(connection.DeviceConnectionId.ToString()).notifyUserConnected();
                }
            }
            else
            {
                Clients.Client(connection.UserConnectionId.ToString()).notifyUserConnectedCallback();
                Clients.Client(connection.DeviceConnectionId.ToString()).notifyUserConnected();
                if (connection.DeviceConnectionId != Guid.Empty)
                {
                    Clients.Client(connection.UserConnectionId.ToString()).notifyDeviceConnected();
                }
            }
        }

        public void SendInfoToUser(string key)
        {
            Clients.Client(Connections.GetConnections(key).UserConnectionId.ToString()).sendInfoToUser();
        }

        public void LockDevice(string key)
        {
            Clients.Client(Connections.GetConnections(key).DeviceConnectionId.ToString()).lockDevice();
        }

        public void UnlockDevice(string key)
        {
            Clients.Client(Connections.GetConnections(key).DeviceConnectionId.ToString()).unlockDevice();
        }

        public void CheckDeviceOnline(string key)
        {
            var connection = Connections.GetConnections(key);
            Clients.Client(connection.UserConnectionId.ToString())
                .deviceOnlineNotification(connection.DeviceConnectionId != Guid.Empty);
        }

        public void CheckUserOnline(string key)
        {
            var connection = Connections.GetConnections(key);
            Clients.Client(connection.DeviceConnectionId.ToString())
                .userOnlineNotification(connection.UserConnectionId != Guid.Empty);
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