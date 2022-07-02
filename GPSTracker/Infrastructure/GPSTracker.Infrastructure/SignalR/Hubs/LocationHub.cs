using GPSTracker.Domain.Entities;
using GPSTracker.Persistance.Data.Contexts;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
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

        public async Task Connect(string tag, string lat, string lon)
        {
            var user = await _context.Users.Where(u => u.Name == tag && u.IsActive == true).FirstOrDefaultAsync();
            if (user == null)
            {
                user = new()
                {
                    CreatedDate = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    IsActive = true,
                    Name = tag,
                    ConnectionId = Context.ConnectionId
                };
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                user.ConnectionId = Context.ConnectionId;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }

           await Clients.All.SendAsync("location",tag, lat, lon);
           
        }
    }
}
