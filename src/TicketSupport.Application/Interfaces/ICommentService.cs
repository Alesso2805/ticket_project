using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketSupport.Application.DTOs;

namespace TicketSupport.Application.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetByTicketIdAsync(Guid ticketId, CancellationToken cancellationToken = default);
        Task<Guid> CreateAsync(CreateCommentDto dto, CancellationToken cancellationToken = default);
    }
}
