using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TicketSupport.Application.Interfaces;
using TicketSupport.Application.Services;
using TicketSupport.Application.Validators;

namespace TicketSupport.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IUserService, UserService>();
            services.AddValidatorsFromAssemblyContaining<CreateTicketValidator>();
            
            return services;
        }
    }
}
