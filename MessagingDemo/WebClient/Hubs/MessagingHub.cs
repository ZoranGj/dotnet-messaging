using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClient.Hubs
{
    public class MessagingHub : Hub
    {
        public void ReadQueueMessages(string name, string message)
        {
            Clients.All.NewMessage(name, message);
        }
    }
}