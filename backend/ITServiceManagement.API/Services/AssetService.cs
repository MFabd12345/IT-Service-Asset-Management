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

    public bool DeleteAsset(int id)
    {
        var asset = _db.Assets.Find(id);

        if (asset == null)
        {
            return false;
        }

        _db.Assets.Remove(asset);
        _db.SaveChanges();

        return true;
    }

    public bool UpdateAsset(int id, Asset updatedAsset)
    {
        var asset = _db.Assets.Find(id);

        if (asset == null)
        {
            return false;
        }

        asset.Name = updatedAsset.Name;
        asset.Type = updatedAsset.Type;
        asset.Status = updatedAsset.Status;

        _db.SaveChanges();

        return true;
    }
}