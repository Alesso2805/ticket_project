using System;
using System.Collections.Generic;
using TicketSupport.Domain.Common;

namespace TicketSupport.Domain.Entities
{
    public class Tenant : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string PlanType { get; set; } = "Basic";
        public bool IsActive { get; set; } = true;

        // Navigation Properties
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
