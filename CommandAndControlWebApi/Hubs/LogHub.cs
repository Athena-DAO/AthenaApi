using CommandAndControlWebApi.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAndControlWebApi.Hubs
{
    public class LogHub : Hub
    {

        public async Task StartTransmission(string id)
        {
            LoggingService.RegisterUser(id, Context.ConnectionId);
            string targetFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", id) + ".txt";
            if (File.Exists(targetFilePath))
            {
                string contents = File.ReadAllText(targetFilePath);
                await Clients.All.SendAsync("Log", contents);
            }
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            LoggingService.RemoveUser(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
