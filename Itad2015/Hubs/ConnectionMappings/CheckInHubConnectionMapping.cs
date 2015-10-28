﻿using System;
using System.Collections.Generic;

namespace Itad2015.Hubs.ConnectionMappings
{
    public class CheckInHubConnectionMapping
    {
        private readonly static Dictionary<string, Connections> Connections = new Dictionary<string, Connections>();

        public int Count => Connections.Count;

        public void Add(string key, Guid connectionId, ConnectionType connectionType)
        {
            lock (Connections)
            {
                Connections connection;
                if (!Connections.TryGetValue(key, out connection))
                {
                    connection = new Connections();
                    if (connectionType == ConnectionType.Device)
                    {
                        connection.DeviceConnectionId = connectionId;
                    }
                    else
                    {
                        connection.UserConnectionId = connectionId;
                    }
                    Connections.Add(key, connection);
                }

                lock (connection)
                {
                    if (connectionType == ConnectionType.Device)
                    {
                        connection.DeviceConnectionId = connectionId;
                    }
                    else
                    {
                        connection.UserConnectionId = connectionId;
                    }
                }
            }
        }

        public Connections GetConnections(string key)
        {
            lock (Connections)
            {
                Connections connection;
                if (Connections.TryGetValue(key, out connection))
                {
                    return connection;
                }
            }

            return new Connections();
        }
    }

}