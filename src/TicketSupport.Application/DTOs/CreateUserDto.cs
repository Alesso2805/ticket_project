using System;
using System.ComponentModel.DataAnnotations;
using TicketSupport.Domain.Enums;

namespace TicketSupport.Application.DTOs
{
    public record CreateUserDto(
        [Required] string FullName,
        [Required, EmailAddress] string Email,
        [Required] string Password,
        [Required] Guid TenantId,
        UserRole Role = UserRole.Customer
    );
}
