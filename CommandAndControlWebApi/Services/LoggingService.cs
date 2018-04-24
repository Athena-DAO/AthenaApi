using CommandAndControlWebApi.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAndControlWebApi.Services
{
    public class LoggingService
    {
        private static ConcurrentDictionary<string, string> users = new ConcurrentDictionary<string, string>();
        private static ConcurrentDictionary<string, string> reverseUsers = new ConcurrentDictionary<string, string>();

        public static void RegisterUser(string id, string connectionId)
        {
            users.TryAdd(id, connectionId);
            reverseUsers.TryAdd(connectionId, id);
        }

        public static string GetUser(string id)
        {
            users.TryGetValue(id, out string connectionId);
            return connectionId;
        }

        public static void RemoveUser(string connectionId)
        {
            reverseUsers.TryRemove(connectionId, out string pipelineId);
            users.TryRemove(pipelineId, out _);
        }
    }
}
