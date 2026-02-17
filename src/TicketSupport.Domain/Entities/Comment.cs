using System;
using TicketSupport.Domain.Common;

namespace TicketSupport.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; } = string.Empty;

        public Guid TicketId { get; set; }
        public Ticket? Ticket { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
