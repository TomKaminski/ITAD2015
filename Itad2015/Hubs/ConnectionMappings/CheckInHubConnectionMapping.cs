using System;
using System.Collections.Generic;
using System.Linq;

namespace Itad2015.Hubs.ConnectionMappings
{
    public class CheckInHubConnectionMapping
    {
        public class ConnectionMapping
        {
            private readonly static Dictionary<string,Connections> _connections = new Dictionary<string, Connections>();

            public int Count => _connections.Count;

            public void Add(string key, Guid connectionId, ConnectionType connectionType)
            {
                lock (_connections)
                {
                    Connections connection;
                    if (!_connections.TryGetValue(key, out connection))
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
                        _connections.Add(key, connection);
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
                lock (_connections)
                {
                    Connections connection;
                    if (_connections.TryGetValue(key, out connection))
                    {
                        return connection;
                    }
                }

                return new Connections();
            }
        }
    }
}