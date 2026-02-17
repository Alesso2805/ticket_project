using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketSupport.Infrastructure.Data;

namespace TicketSupport.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            services.AddScoped<TicketSupport.Application.Interfaces.ITicketRepository, TicketSupport.Infrastructure.Repositories.TicketRepository>();
            services.AddScoped<TicketSupport.Application.Interfaces.ITenantRepository, TicketSupport.Infrastructure.Repositories.TenantRepository>();
            services.AddScoped<TicketSupport.Application.Interfaces.IUserRepository, TicketSupport.Infrastructure.Repositories.UserRepository>();

            return services;
        }
    }
}
