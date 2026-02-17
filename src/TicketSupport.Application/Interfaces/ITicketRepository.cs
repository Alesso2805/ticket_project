using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketSupport.Domain.Entities;

namespace TicketSupport.Application.Interfaces
{
    public interface ITicketRepository
    {
        Task<Ticket?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Ticket>> GetAllAsync(Guid tenantId, CancellationToken cancellationToken);
        Task AddAsync(Ticket ticket, CancellationToken cancellationToken);
        Task UpdateAsync(Ticket ticket, CancellationToken cancellationToken);
        Task DeleteAsync(Ticket ticket, CancellationToken cancellationToken);
    }
}
