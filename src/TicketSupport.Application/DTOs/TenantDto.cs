using System;

namespace TicketSupport.Application.DTOs
{
    public record TenantDto(Guid Id, string Name, string PlanType, bool IsActive);
}
