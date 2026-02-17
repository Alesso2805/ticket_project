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
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var ticket = await _ticketService.GetByIdAsync(id, cancellationToken);
            return Ok(ticket);
        }

        [HttpGet("tenant/{tenantId}")]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetAll(Guid tenantId, CancellationToken cancellationToken)
        {
            if (tenantId == Guid.Empty)
            {
                return BadRequest("TenantId is required.");
            }

            var tickets = await _ticketService.GetAllAsync(tenantId, cancellationToken);
            return Ok(tickets);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateTicketDto dto, CancellationToken cancellationToken)
        {
            var id = await _ticketService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTicketDto dto, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            await _ticketService.UpdateAsync(id, dto, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
             await _ticketService.DeleteAsync(id, cancellationToken);
             return NoContent();
        }
    }
}
