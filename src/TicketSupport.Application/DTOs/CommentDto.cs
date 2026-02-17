using System;

namespace TicketSupport.Application.DTOs
{
    public record CommentDto(
        Guid Id,
        string Content,
        Guid TicketId,
        Guid UserId,
        DateTime CreatedAt
    );
}
