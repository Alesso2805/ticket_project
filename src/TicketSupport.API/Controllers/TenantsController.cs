using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketSupport.Application.DTOs;
using TicketSupport.Application.Interfaces;

namespace TicketSupport.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenantsController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantsController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TenantDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var tenant = await _tenantService.GetByIdAsync(id, cancellationToken);
            return Ok(tenant);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateTenantDto dto, CancellationToken cancellationToken)
        {
            var id = await _tenantService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }
    }
}
