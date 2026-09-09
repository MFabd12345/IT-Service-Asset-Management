using ITServiceManagement.API.Data;
using ITServiceManagement.API.Models;
using Microsoft.EntityFrameworkCore;

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

    public void AddAssets(List<Asset> assets)
    {
        _db.Assets.AddRange(assets);
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
        asset.SerialNumber = updatedAsset.SerialNumber;
        asset.AssetTag = updatedAsset.AssetTag;
        asset.Manufacturer = updatedAsset.Manufacturer;
        asset.Model = updatedAsset.Model;
        asset.PurchaseDate = updatedAsset.PurchaseDate;
        asset.WarrantyExpiry = updatedAsset.WarrantyExpiry;
        asset.Location = updatedAsset.Location;

        _db.SaveChanges();

        return true;
    }

    public bool AssignAsset(int assetId, int employeeId)
    {
        var asset = _db.Assets.Find(assetId);

        if (asset == null)
        {
            return false;
        }

        var employee = _db.Employees.Find(employeeId);

        if (employee == null)
        {
            return false;
        }

        if (asset.EmployeeId != null)
        {
            return false;
        }

        if (asset.Status == AssetStatus.Retired)
        {
            return false;
        }

        asset.EmployeeId = employeeId;
        asset.Status = AssetStatus.Assigned;

        _db.SaveChanges();

        return true;
    }

    public bool ReturnAsset(int assetId)
    {
        var asset = _db.Assets.Find(assetId);

        if (asset == null)
        {
            return false;
        }

        if (asset.EmployeeId == null)
        {
            return false;
        }

        asset.EmployeeId = null;
        asset.Status = AssetStatus.Available;

        _db.SaveChanges();

        return true;
    }

    public List<Asset> GetAssetsByEmployeeId(int employeeId)
    {
        return _db.Assets
            .Where(a => a.EmployeeId == employeeId)
            .ToList();
    }

    public Employee? GetEmployeeByAssetId(int assetId)
    {
        var asset = _db.Assets
            .Include(a => a.Employee)
            .FirstOrDefault(a => a.Id == assetId);

        return asset?.Employee;
    }

}