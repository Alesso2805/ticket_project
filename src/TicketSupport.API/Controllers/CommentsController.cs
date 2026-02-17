using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketSupport.Application.DTOs;
using TicketSupport.Application.Interfaces;

namespace TicketSupport.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("ticket/{ticketId}")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetByTicketId(Guid ticketId, CancellationToken cancellationToken)
        {
            var comments = await _commentService.GetByTicketIdAsync(ticketId, cancellationToken);
            return Ok(comments);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateCommentDto dto, CancellationToken cancellationToken)
        {
            var id = await _commentService.CreateAsync(dto, cancellationToken);
            return Ok(id); // Using Ok instead of CreatedAtAction because we don't have a GetById endpoint for comments exposed
        }
    }
}
