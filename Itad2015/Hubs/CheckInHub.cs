using System;
using System.Threading.Tasks;
using Itad2015.Contract.DTO.Api;
using Itad2015.Hubs.ConnectionMappings;
using Microsoft.AspNet.SignalR;

namespace Itad2015.Hubs
{
    public class CheckInHub : Hub
    {
        private static readonly CheckInHubConnectionMapping Connections = new CheckInHubConnectionMapping();

        public async Task Connect(string key, Guid connectionId, ConnectionType type)
        {
            Connections.Add(key, connectionId, type);
            var connection = Connections.GetConnections(key);
            if (type == ConnectionType.Device)
            {
                await Clients.Client(connection.UserConnectionId.ToString()).notifyDeviceConnected();
            }
            else
            {
                await Clients.Client(connection.UserConnectionId.ToString()).notifyUserConnectedCallback();
                if (connection.DeviceConnectionId != Guid.Empty)
                {
                    await Clients.Client(connection.UserConnectionId.ToString()).notifyDeviceConnected();
                }
            }
        }

        public async Task Disconnect(string key, ConnectionType type)
        {
            Connections.Remove(key, type);
            var connection = Connections.GetConnections(key);
            if (type == ConnectionType.Device)
            {
                await Clients.Client(connection.UserConnectionId.ToString()).notifyDeviceDisconnected();
            }
        }

        public async Task LockDevice(string key, GuestApiDto data)
        {
            var connection = Connections.GetConnections(key);
            await Clients.Client(connection.UserConnectionId.ToString()).lockDevice(data);
        }

        public async Task UnlockDevice(string key)
        {
            var connection = Connections.GetConnections(key);
            await Clients.Client(connection.DeviceConnectionId.ToString()).unlockDevice();
        }

        public async Task UnlockDeviceUserCallback(string key)
        {
            var connection = Connections.GetConnections(key);
            await Clients.Client(connection.UserConnectionId.ToString()).unlockDeviceUserCallback();
        }

        public async Task UserRecievedMessageCallback(string key)
        {
            var connection = Connections.GetConnections(key);
            await Clients.Client(connection.DeviceConnectionId.ToString()).userRecievedMessageCallback();
        }

        public async Task CheckAppIsWaiting(string key)
        {
            var connection = Connections.GetConnections(key);
            await Clients.Client(connection.DeviceConnectionId.ToString()).checkAppIsWaiting();
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

