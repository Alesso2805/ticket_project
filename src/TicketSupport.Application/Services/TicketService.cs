using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TicketSupport.Application.DTOs;
using TicketSupport.Application.Interfaces;
using TicketSupport.Domain.Entities;
using TicketSupport.Domain.Exceptions;

namespace TicketSupport.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _repository;

        public TicketService(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task<TicketDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var ticket = await _repository.GetByIdAsync(id, cancellationToken);
            
            if (ticket == null)
            {
                throw new NotFoundException(nameof(Ticket), id);
            }

            return MapToDto(ticket);
        }

        public async Task<IEnumerable<TicketDto>> GetAllAsync(Guid tenantId, CancellationToken cancellationToken = default)
        {
            var tickets = await _repository.GetAllAsync(tenantId, cancellationToken);
            return tickets.Select(MapToDto);
        }

        public async Task<Guid> CreateAsync(CreateTicketDto dto, CancellationToken cancellationToken = default)
        {
            var ticket = new Ticket
            {
                Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority,
                TenantId = dto.TenantId,
                CreatedByUserId = dto.CreatedByUserId,
                Status = Domain.Enums.TicketStatus.Open
            };

            await _repository.AddAsync(ticket, cancellationToken);

            return ticket.Id;
        }

        public async Task UpdateAsync(Guid id, UpdateTicketDto dto, CancellationToken cancellationToken = default)
        {
            var ticket = await _repository.GetByIdAsync(id, cancellationToken);

            if (ticket == null)
            {
                throw new NotFoundException(nameof(Ticket), id);
            }

            ticket.Title = dto.Title;
            ticket.Description = dto.Description;
            ticket.Priority = dto.Priority;
            ticket.Status = dto.Status;
            ticket.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(ticket, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var ticket = await _repository.GetByIdAsync(id, cancellationToken);
            if (ticket == null)
            {
                throw new NotFoundException(nameof(Ticket), id);
            }

            await _repository.DeleteAsync(ticket, cancellationToken);
        }

        private static TicketDto MapToDto(Ticket ticket)
        {
            return new TicketDto(
                ticket.Id,
                ticket.Title,
                ticket.Description,
                ticket.Status,
                ticket.Priority,
                ticket.TenantId,
                ticket.CreatedByUserId,
                ticket.AssignedToUserId,
                ticket.CreatedAt
            );
        }
    }
}
