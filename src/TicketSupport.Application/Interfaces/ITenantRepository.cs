using System;
using System.Threading;
using System.Threading.Tasks;
using TicketSupport.Domain.Entities;

namespace TicketSupport.Application.Interfaces
{
    public interface ITenantRepository
    {
        Task<Tenant?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddAsync(Tenant tenant, CancellationToken cancellationToken);
    }
}
