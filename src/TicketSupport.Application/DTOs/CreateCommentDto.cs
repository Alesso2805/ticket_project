using System;
using System.ComponentModel.DataAnnotations;

namespace TicketSupport.Application.DTOs
{
    public record CreateCommentDto(
        [Required] string Content,
        [Required] Guid TicketId,
        [Required] Guid UserId
    );
}
