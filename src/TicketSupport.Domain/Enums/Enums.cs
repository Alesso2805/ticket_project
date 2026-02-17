namespace TicketSupport.Domain.Enums
{
    public enum TicketStatus
    {
        Open,
        InProgress,
        Resolved,
        Closed
    }

    public enum TicketPriority
    {
        Low,
        Medium,
        High,
        Critical
    }

    public enum UserRole
    {
        Admin,
        Agent,
        Customer
    }
}
