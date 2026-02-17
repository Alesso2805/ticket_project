using System;
using System.Threading;
using System.Threading.Tasks;
using TicketSupport.Application.DTOs;

namespace TicketSupport.Application.Interfaces
{
    public interface ITenantService
    {
        Task<TenantDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Guid> CreateAsync(CreateTenantDto dto, CancellationToken cancellationToken = default);
    }
}
