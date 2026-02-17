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
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Comment?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Comments.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<IEnumerable<Comment>> GetByTicketIdAsync(Guid ticketId, CancellationToken cancellationToken)
        {
            return await _context.Comments
                .AsNoTracking()
                .Where(c => c.TicketId == ticketId)
                .OrderBy(c => c.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Comment comment, CancellationToken cancellationToken)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
