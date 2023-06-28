
using GrennyWebApplication.Database;
using Microsoft.EntityFrameworkCore;

namespace GrennyWebApplication.Infrastructure.Configurations
{
    public static class DatabaseConfigurations
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("RamilPc"));
            });
        }
    }
}
