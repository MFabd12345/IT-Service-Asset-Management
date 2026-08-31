using ITServiceManagement.API.Models;

namespace ITServiceManagement.API.DTOs;

public class AssetDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public AssetStatus Status { get; set; }
}