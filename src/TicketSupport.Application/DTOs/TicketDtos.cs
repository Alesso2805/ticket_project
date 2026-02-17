using System;
using TicketSupport.Domain.Enums;

namespace TicketSupport.Application.DTOs
{
    public record TicketDto(
        Guid Id,
        string Title,
        string Description,
        TicketStatus Status,
        TicketPriority Priority,
        Guid TenantId,
        Guid CreatedByUserId,
        Guid? AssignedToUserId,
        DateTime CreatedAt
    );

    public record CreateTicketDto(
        string Title,
        string Description,
        TicketPriority Priority,
        Guid TenantId,
        Guid CreatedByUserId
    );

    public record UpdateTicketDto(
        string Title,
        string Description,
        TicketPriority Priority,
        TicketStatus Status
    );
}
