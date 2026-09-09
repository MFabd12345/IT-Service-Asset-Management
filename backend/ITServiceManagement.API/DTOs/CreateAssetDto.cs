using ITServiceManagement.API.Models;
using System.ComponentModel.DataAnnotations;

namespace ITServiceManagement.API.DTOs;

public class CreateAssetDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Type { get; set; } = string.Empty;

    [Required]
    public AssetStatus Status { get; set; }

    [Required]
    [StringLength(100)]
    public string SerialNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string AssetTag { get; set; } = string.Empty;

    [StringLength(100)]
    public string Manufacturer { get; set; } = string.Empty;

    [StringLength(100)]
    public string Model { get; set; } = string.Empty;

    public DateTime? PurchaseDate { get; set; }

    public DateTime? WarrantyExpiry { get; set; }

    [StringLength(100)]
    public string Location { get; set; } = string.Empty;
}