using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketSupport.Application.DTOs;

namespace TicketSupport.Application.Interfaces
{
    public interface ITicketService
    {
        Task<TicketDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TicketDto>> GetAllAsync(Guid tenantId, CancellationToken cancellationToken = default); // Simplified pagination for now
        Task<Guid> CreateAsync(CreateTicketDto ticket, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid id, UpdateTicketDto ticket, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
