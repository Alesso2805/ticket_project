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
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;

        public CommentService(ICommentRepository repository, ITicketRepository ticketRepository, IUserRepository userRepository)
        {
            _repository = repository;
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<CommentDto>> GetByTicketIdAsync(Guid ticketId, CancellationToken cancellationToken = default)
        {
            // Verify ticket exists
            var ticket = await _ticketRepository.GetByIdAsync(ticketId, cancellationToken);
            if (ticket == null)
            {
                throw new NotFoundException(nameof(Ticket), ticketId);
            }

            var comments = await _repository.GetByTicketIdAsync(ticketId, cancellationToken);
            return comments.Select(c => new CommentDto(c.Id, c.Content, c.TicketId, c.UserId, c.CreatedAt));
        }

        public async Task<Guid> CreateAsync(CreateCommentDto dto, CancellationToken cancellationToken = default)
        {
            // Verify ticket exists
            var ticket = await _ticketRepository.GetByIdAsync(dto.TicketId, cancellationToken);
            if (ticket == null)
            {
                throw new NotFoundException(nameof(Ticket), dto.TicketId);
            }

            // Verify user exists
            var user = await _userRepository.GetByIdAsync(dto.UserId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), dto.UserId);
            }

            var comment = new Comment
            {
                Content = dto.Content,
                TicketId = dto.TicketId,
                UserId = dto.UserId,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(comment, cancellationToken);
            return comment.Id;
        }
    }
}
