using ITServiceManagement.API.Data;
using ITServiceManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ITServiceManagement.API.Services;

public class MaintenanceService
{
    private readonly AppDbContext _db;

    public MaintenanceService(AppDbContext db)
    {
        _db = db;
    }

    public List<MaintenanceRecord> GetAll()
    {
        return _db.MaintenanceRecords
            .Include(m => m.Asset)
            .ToList();
    }

    public MaintenanceRecord? GetById(int id)
    {
        return _db.MaintenanceRecords
            .Include(m => m.Asset)
            .FirstOrDefault(m => m.Id == id);
    }

    public bool Add(int assetId, MaintenanceRecord record)
    {
        var asset = _db.Assets.Find(assetId);

        if (asset == null)
        {
            return false;
        }

        record.AssetId = assetId;
        asset.Status = AssetStatus.UnderRepair;

        _db.MaintenanceRecords.Add(record);
        _db.SaveChanges();

        return true;
    }

    public bool Complete(int id, string resolution, decimal? cost)
    {
        var record = _db.MaintenanceRecords.Find(id);

        if (record == null)
        {
            return false;
        }

        record.Resolution = resolution;
        record.Cost = cost;
        record.CompletedDate = DateTime.UtcNow;

        var asset = _db.Assets.Find(record.AssetId);

        if (asset != null)
        {
            asset.Status = AssetStatus.Available;
        }

        _db.SaveChanges();

        return true;
    }
}