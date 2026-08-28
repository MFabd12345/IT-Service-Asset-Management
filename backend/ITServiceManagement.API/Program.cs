using ITServiceManagement.API.Data;
using ITServiceManagement.API.Models;
using ITServiceManagement.API.Services;
using Microsoft.EntityFrameworkCore;
using ITServiceManagement.API.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<AssetService>();
builder.Services.AddSwaggerGen();
builder.Services.AddValidation();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection")
        )
    ));

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();

    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.MapGet("/", () =>
{
    return "IT Service Asset Management API is running!";
});

app.MapGet("/api/assets", (AssetService assetService) =>
{
    return assetService.GetAssets();
});

app.MapGet("/api/assets/{id}", (int id, AssetService assetService) =>
{
    var asset = assetService.GetAssetById(id);

    if (asset == null)
    {
        return Results.NotFound("Asset not found");
    }

    return Results.Ok (asset);
});

app.MapPost("/api/assets", (CreateAssetDto assetDto, AssetService assetService) =>
{
    var asset = new Asset
    {
        Name = assetDto.Name,
        Type = assetDto.Type,
        Status = assetDto.Status
    };

    assetService.AddAsset(asset);
    return Results.Created($"/api/assets/{asset.Id}", asset);
});

app.MapDelete("/api/assets/{id}", (int id, AssetService assetService) =>
{
    var deleted = assetService.DeleteAsset(id);

    if (!deleted)
    {
        return Results.NotFound("Asset not found");
    }

    return Results.Ok("Asset deleted successfully");
});

app.MapPut("/api/assets/{id}", (int id, Asset updatedAsset, AssetService assetService) =>
{
    var updated = assetService.UpdateAsset(id, updatedAsset);

    if (!updated)
    {
        return Results.NotFound("Asset not found");
    }

    return Results.Ok("Asset updated successfully");
});

app.Run();
