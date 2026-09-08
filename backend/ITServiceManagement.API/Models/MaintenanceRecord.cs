namespace ITServiceManagement.API.Models;

public class MaintenanceRecord
{
    public int Id { get; set; }

    public int AssetId { get; set; }

    public Asset? Asset { get; set; }

    public string Issue { get; set; } = string.Empty;

    public string? Resolution { get; set; }

    public DateTime ReportedDate { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedDate { get; set; }

    public decimal? Cost { get; set; }
}