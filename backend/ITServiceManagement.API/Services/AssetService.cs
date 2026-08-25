using ITServiceManagement.API.Data;
using ITServiceManagement.API.Models;

namespace ITServiceManagement.API.Services;

public class AssetService
{
    private readonly AppDbContext _db;

    public AssetService(AppDbContext db)
    {
        _db = db;
    }

    public List<Asset> GetAssets()
    {
        return _db.Assets.ToList();
    }

    public Asset? GetAssetById(int id)
{
    return _db.Assets.Find(id);
}

    public void AddAsset(Asset asset)
    {
        _db.Assets.Add(asset);
        _db.SaveChanges();
    }
}