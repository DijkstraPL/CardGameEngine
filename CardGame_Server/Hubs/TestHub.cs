using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardGame_Server.Hubs
{
    public class TestHub : Hub
    {
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override Task OnConnectedAsync()
        {

            Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public async Task Greetings(string name)
        {
            await Clients.All.SendAsync("Greetings", "Hello from " + name);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
