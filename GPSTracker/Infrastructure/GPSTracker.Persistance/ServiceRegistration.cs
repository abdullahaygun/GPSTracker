using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GPSTracker.Persistance.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GPSTracker.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            ConfigurationManager configurationManager = new();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory()));
            configurationManager.AddJsonFile("appsettings.json");
            services.AddDbContext<GPSTrackerDbContext>(options => options.UseNpgsql(configurationManager.GetConnectionString("PostgreSQL")));
        }
    }
}
