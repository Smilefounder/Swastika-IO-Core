using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swastika.Domain.Entities.Blog;

// Ref: https://chsakell.com/2016/10/10/real-time-applications-using-asp-net-core-signalr-angular/
namespace Swastika.Hubs
{
    public class Broadcaster : Hub<IBroadcaster>
    {
        public override Task OnConnected()
        {
            // Set connection id for just connected client only
            return Clients.Client(Context.ConnectionId).SetConnectionId(Context.ConnectionId);
        }

        // Server side methods called from client
        public Task Subscribe(int matchId)
        {
            return Groups.Add(Context.ConnectionId, matchId.ToString());
        }

        public Task Unsubscribe(int matchId)
        {
            return Groups.Remove(Context.ConnectionId, matchId.ToString());
        }
    }

    public interface IBroadcaster
    {
        Task SetConnectionId(string connectionId);
        Task AddBlogPost(Post post);
    }
}
