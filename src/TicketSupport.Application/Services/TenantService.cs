using System;
using System.Threading;
using System.Threading.Tasks;
using TicketSupport.Application.DTOs;
using TicketSupport.Application.Interfaces;
using TicketSupport.Domain.Entities;
using TicketSupport.Domain.Exceptions;

namespace TicketSupport.Application.Services
{
    public class TenantService : ITenantService
    {
        private readonly ITenantRepository _repository;

        public TenantService(ITenantRepository repository)
        {
            _repository = repository;
        }

        public async Task<TenantDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var tenant = await _repository.GetByIdAsync(id, cancellationToken);
            if (tenant == null)
            {
                throw new NotFoundException(nameof(Tenant), id);
            }

            return new TenantDto(tenant.Id, tenant.Name, tenant.PlanType, tenant.IsActive);
        }

        public async Task<Guid> CreateAsync(CreateTenantDto dto, CancellationToken cancellationToken = default)
        {
            var tenant = new Tenant
            {
                Name = dto.Name,
                PlanType = dto.PlanType,
                IsActive = true
            };

            await _repository.AddAsync(tenant, cancellationToken);
            return tenant.Id;
        }
    }
}
