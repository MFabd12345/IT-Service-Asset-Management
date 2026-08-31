namespace ITServiceManagement.API.Models;

public class Asset
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public AssetStatus Status { get; set; }
}