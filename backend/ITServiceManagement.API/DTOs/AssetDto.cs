using ITServiceManagement.API.Models;

namespace ITServiceManagement.API.DTOs;

public class AssetDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public AssetStatus Status { get; set; }

    public string SerialNumber { get; set; } = string.Empty;

    public string AssetTag { get; set; } = string.Empty;

    public string Manufacturer { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public DateTime? PurchaseDate { get; set; }

    public DateTime? WarrantyExpiry { get; set; }

    public string Location { get; set; } = string.Empty;

    public int? EmployeeId { get; set; }
}