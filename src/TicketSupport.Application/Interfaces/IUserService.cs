using System;
using System.Threading;
using System.Threading.Tasks;
using TicketSupport.Application.DTOs;

namespace TicketSupport.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Guid> CreateAsync(CreateUserDto dto, CancellationToken cancellationToken = default);
    }
}
