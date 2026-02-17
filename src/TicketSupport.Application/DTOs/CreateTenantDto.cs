using System.ComponentModel.DataAnnotations;

namespace TicketSupport.Application.DTOs
{
    public record CreateTenantDto([Required] string Name, string PlanType = "Basic");
}
