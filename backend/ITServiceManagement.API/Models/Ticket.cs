namespace ITServiceManagement.API.Models;

public class Ticket
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public TicketPriority Priority { get; set; }

    public TicketStatus Status { get; set; } = TicketStatus.Open;

    public int EmployeeId { get; set; }

    public Employee? Employee { get; set; }

    public int? AssetId { get; set; }

    public Asset? Asset { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime? ResolvedDate { get; set; }
}