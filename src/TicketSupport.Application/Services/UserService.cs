using System;
using System.Threading;
using System.Threading.Tasks;
using TicketSupport.Application.DTOs;
using TicketSupport.Application.Interfaces;
using TicketSupport.Domain.Entities;
using TicketSupport.Domain.Exceptions;

namespace TicketSupport.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly ITenantRepository _tenantRepository;

        public UserService(IUserRepository repository, ITenantRepository tenantRepository)
        {
            _repository = repository;
            _tenantRepository = tenantRepository;
        }

        public async Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _repository.GetByIdAsync(id, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), id);
            }

            return new UserDto(user.Id, user.FullName, user.Email, user.Role, user.TenantId);
        }

        public async Task<Guid> CreateAsync(CreateUserDto dto, CancellationToken cancellationToken = default)
        {
            var tenant = await _tenantRepository.GetByIdAsync(dto.TenantId, cancellationToken);
            if (tenant == null)
            {
                throw new NotFoundException(nameof(Tenant), dto.TenantId);
            }

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                // In a real app, we should hash the password properly. For now, simple assignment.
                PasswordHash = dto.Password, 
                Role = dto.Role,
                TenantId = dto.TenantId
            };

            await _repository.AddAsync(user, cancellationToken);
            return user.Id;
        }
    }
}
