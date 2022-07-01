using GPSTracker.Persistance.Data.Contexts;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSTracker.Infrastructure.SignalR.Hubs
{
    public class LocationHub:Hub
    {
        private readonly GPSTrackerDbContext _context;

        public LocationHub(GPSTrackerDbContext context)
        {
            _context = context;
        }

        public override Task OnConnectedAsync()
        {
            Clients.All.SendAsync("connected", Context.ConnectionId);
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            Clients.All.SendAsync("disconnected", Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
