using System;
using System.Threading;
using System.Threading.Tasks;
using TicketSupport.Domain.Entities;

namespace TicketSupport.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddAsync(User user, CancellationToken cancellationToken);
    }
}
