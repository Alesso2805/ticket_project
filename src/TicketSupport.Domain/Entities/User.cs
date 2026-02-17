using System;
using System.Collections.Generic;
using TicketSupport.Domain.Common;
using TicketSupport.Domain.Enums;

namespace TicketSupport.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; }

        public Guid TenantId { get; set; }
        public Tenant? Tenant { get; set; }

        // Navigation Properties
        public ICollection<Ticket> CreatedTickets { get; set; } = new List<Ticket>();
        public ICollection<Ticket> AssignedTickets { get; set; } = new List<Ticket>();
    }
}
