using ITServiceManagement.API.Data;
using ITServiceManagement.API.Models;
using ITServiceManagement.API.Services;
using Microsoft.EntityFrameworkCore;
using ITServiceManagement.API.DTOs;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(
        new JsonStringEnumConverter()
    );
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<AssetService>();
builder.Services.AddScoped<EmployeeService>();
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
    var assets = assetService.GetAssets();

    var assetDtos = assets.Select(asset => new AssetDto
    {
        Id = asset.Id,
        Name = asset.Name,
        Type = asset.Type,
        Status = asset.Status
    });

    return Results.Ok(assetDtos);
});

app.MapGet("/api/assets/{id}", (int id, AssetService assetService) =>
{
    var asset = assetService.GetAssetById(id);

    if (asset == null)
    {
        return Results.NotFound("Asset not found");
    }

    var assetDto = new AssetDto
    {
        Id = asset.Id,
        Name = asset.Name,
        Type = asset.Type,
        Status = asset.Status
    };

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

    var resultDto = new AssetDto
    {
        Id = asset.Id,
        Name = asset.Name,
        Type = asset.Type,
        Status = asset.Status
    };

    return Results.Created($"/api/assets/{asset.Id}", asset);
});

app.MapPost("/api/assets/bulk",
    (List<Asset> assets, AssetService assetService) =>
    {
        assetService.AddAssets(assets);

        return Results.Ok($"{assets.Count} assets added successfully");
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
    var asset = new Asset
    {
        Name = updatedAsset.Name,
        Type = updatedAsset.Type,
        Status = updatedAsset.Status
    };

    var updated = assetService.UpdateAsset(id, updatedAsset);

    if (!updated)
    {
        return Results.NotFound("Asset not found");
    }

    return Results.Ok("Asset updated successfully");
});

app.MapGet("/api/employees", (EmployeeService employeeService) =>
{
    var employees = employeeService.GetEmployees();

    var employeeDtos = employees.Select(employee => new EmployeeDto
    {
        Id = employee.Id,
        EmployeeId = employee.EmployeeId,
        Name = employee.Name,
        Email = employee.Email,
        Department = employee.Department,
        Designation = employee.Designation
    });

    return Results.Ok(employeeDtos);
});

app.MapGet("/api/employees/{id}", (int id, EmployeeService employeeService) =>
{
    var employee = employeeService.GetEmployeeById(id);

    if (employee == null)
    {
        return Results.NotFound("Employee not found");
    }

    var employeeDto = new EmployeeDto
    {
        Id = employee.Id,
        EmployeeId = employee.EmployeeId,
        Name = employee.Name,
        Email = employee.Email,
        Department = employee.Department,
        Designation = employee.Designation
    };

    return Results.Ok(employeeDto);
});

app.MapPost("/api/employees", (CreateEmployeeDto employeeDto, EmployeeService employeeService) =>
{
    var employee = new Employee
    {
        EmployeeId = employeeDto.EmployeeId,
        Name = employeeDto.Name,
        Email = employeeDto.Email,
        Department = employeeDto.Department,
        Designation = employeeDto.Designation
    };

    employeeService.AddEmployee(employee);

    var resultDto = new EmployeeDto
    {
        Id = employee.Id,
        EmployeeId = employee.EmployeeId,
        Name = employee.Name,
        Email = employee.Email,
        Department = employee.Department,
        Designation = employee.Designation
    };

    return Results.Created($"/api/employees/{employee.Id}", resultDto);
});

app.MapPost("/api/employees/bulk",
    (List<Employee> employees, EmployeeService employeeService) =>
    {
        employeeService.AddEmployees(employees);

        return Results.Ok($"{employees.Count} employees added successfully");
    });

app.MapPut("/api/employees/{id}", (int id, CreateEmployeeDto employeeDto, EmployeeService employeeService) =>
{
    var employee = new Employee
    {
        EmployeeId = employeeDto.EmployeeId,
        Name = employeeDto.Name,
        Email = employeeDto.Email,
        Department = employeeDto.Department,
        Designation = employeeDto.Designation
    };

    var updated = employeeService.UpdateEmployee(id, employee);

    if (!updated)
    {
        return Results.NotFound("Employee not found");
    }

    return Results.Ok("Employee updated successfully");
});

app.MapDelete("/api/employees/{id}", (int id, EmployeeService employeeService) =>
{
    var deleted = employeeService.DeleteEmployee(id);

    if (!deleted)
    {
        return Results.NotFound("Employee not found");
    }

    return Results.Ok("Employee deleted successfully");
});

app.MapPut("/api/assets/{assetId}/assign/{employeeId}",
    (int assetId, int employeeId, AssetService assetService) =>
    {
        var assigned = assetService.AssignAsset(assetId, employeeId);

        if (!assigned)
        {
            return Results.BadRequest("Asset or employee not found, or asset is already assigned");
        }

        return Results.Ok("Asset assigned successfully");
    });

app.MapPut("/api/assets/{assetId}/return",
    (int assetId, AssetService assetService) =>
{
    var returned = assetService.ReturnAsset(assetId);

    if (!returned)
    {
        return Results.BadRequest(
            "Asset not found or asset is not currently assigned");
    }

    return Results.Ok("Asset returned successfully");
});

app.MapGet("/api/employees/{employeeId}/assets",
    (int employeeId, AssetService assetService) =>
    {
        var assets = assetService.GetAssetsByEmployeeId(employeeId);

        return Results.Ok(assets);
    });

app.MapGet("/api/assets/{assetId}/employee",
    (int assetId, AssetService assetService) =>
    {
        var employee = assetService.GetEmployeeByAssetId(assetId);

        if (employee == null)
        {
            return Results.NotFound("No employee is assigned to this asset");
        }

        return Results.Ok(employee);
    });

app.Run();
