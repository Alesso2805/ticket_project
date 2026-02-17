using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketSupport.Domain.Entities;

namespace TicketSupport.Application.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Comment>> GetByTicketIdAsync(Guid ticketId, CancellationToken cancellationToken);
        Task AddAsync(Comment comment, CancellationToken cancellationToken);
    }
}
