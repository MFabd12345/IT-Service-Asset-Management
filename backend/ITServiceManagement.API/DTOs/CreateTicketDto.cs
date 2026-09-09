using ITServiceManagement.API.Models;
using System.ComponentModel.DataAnnotations;

namespace ITServiceManagement.API.DTOs;

public class CreateTicketDto
{
    [Required]
    [StringLength(150, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(1000, MinimumLength = 5)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public TicketPriority Priority { get; set; }

    [Required]
    public int EmployeeId { get; set; }

    public int? AssetId { get; set; }
}