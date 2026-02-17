using System;
using System.Collections.Generic;
using TicketSupport.Domain.Common;
using TicketSupport.Domain.Enums;

namespace TicketSupport.Domain.Entities
{
    public class Ticket : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TicketStatus Status { get; set; } = TicketStatus.Open;
        public TicketPriority Priority { get; set; } = TicketPriority.Medium;

        public Guid TenantId { get; set; }
        public Tenant? Tenant { get; set; }

        public Guid CreatedByUserId { get; set; }
        public User? CreatedByUser { get; set; }

        public Guid? AssignedToUserId { get; set; }
        public User? AssignedToUser { get; set; }

        // Navigation Properties
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
