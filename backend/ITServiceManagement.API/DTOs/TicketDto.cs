using ITServiceManagement.API.Models;

namespace ITServiceManagement.API.DTOs;

public class TicketDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public TicketPriority Priority { get; set; }

    public TicketStatus Status { get; set; }

    public int EmployeeId { get; set; }

    public int? AssetId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ResolvedDate { get; set; }
}