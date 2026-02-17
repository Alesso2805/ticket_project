using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketSupport.Application.Interfaces;
using TicketSupport.Domain.Entities;
using TicketSupport.Infrastructure.Data;

namespace TicketSupport.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Ticket?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Tickets
                .AsNoTracking() // Optional: tracking might be needed for updates if not careful
                // For updates, tracking is better, but here we use UpdateAsync explicitly
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync(Guid tenantId, CancellationToken cancellationToken)
        {
            return await _context.Tickets
                .AsNoTracking()
                .Where(x => x.TenantId == tenantId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Ticket ticket, CancellationToken cancellationToken)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Ticket ticket, CancellationToken cancellationToken)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Ticket ticket, CancellationToken cancellationToken)
        {
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
