using GPSTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSTracker.Persistance.Data.Contexts
{
    public class GPSTrackerDbContext : DbContext
    {
        public GPSTrackerDbContext(DbContextOptions<GPSTrackerDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
    }
}
