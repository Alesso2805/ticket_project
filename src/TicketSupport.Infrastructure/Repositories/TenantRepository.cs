using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketSupport.Application.Interfaces;
using TicketSupport.Domain.Entities;
using TicketSupport.Infrastructure.Data;

namespace TicketSupport.Infrastructure.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly AppDbContext _context;

        public TenantRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Tenant?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Tenants.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task AddAsync(Tenant tenant, CancellationToken cancellationToken)
        {
            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
