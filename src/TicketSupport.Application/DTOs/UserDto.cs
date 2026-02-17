using System;
using TicketSupport.Domain.Enums;

namespace TicketSupport.Application.DTOs
{
    public record UserDto(Guid Id, string FullName, string Email, UserRole Role, Guid TenantId);
}
