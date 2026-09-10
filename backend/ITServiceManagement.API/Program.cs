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
builder.Services.AddOpenApi();
builder.Services.AddScoped<AssetService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<MaintenanceService>();
builder.Services.AddScoped<TicketService>();
builder.Services.AddSwaggerGen();
builder.Services.AddValidation();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection")
        )
    ));

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("FrontendPolicy");

if (app.Environment.IsDevelopment())
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


// ==================== ASSETS ====================

// GET ALL ASSETS
app.MapGet("/api/assets", (AssetService assetService) =>
{
    var assets = assetService.GetAssets();

    var assetDtos = assets.Select(asset => new AssetDto
    {
        Id = asset.Id,
        Name = asset.Name,
        Type = asset.Type,
        Status = asset.Status,
        SerialNumber = asset.SerialNumber,
        AssetTag = asset.AssetTag,
        Manufacturer = asset.Manufacturer,
        Model = asset.Model,
        PurchaseDate = asset.PurchaseDate,
        WarrantyExpiry = asset.WarrantyExpiry,
        Location = asset.Location,
        EmployeeId = asset.EmployeeId
    });

    return Results.Ok(assetDtos);
});


// GET ASSET BY ID
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
        Status = asset.Status,
        SerialNumber = asset.SerialNumber,
        AssetTag = asset.AssetTag,
        Manufacturer = asset.Manufacturer,
        Model = asset.Model,
        PurchaseDate = asset.PurchaseDate,
        WarrantyExpiry = asset.WarrantyExpiry,
        Location = asset.Location,
        EmployeeId = asset.EmployeeId
    };

    return Results.Ok(assetDto);
});


// CREATE ASSET
app.MapPost("/api/assets",
    (CreateAssetDto assetDto, AssetService assetService) =>
    {
        var asset = new Asset
        {
            Name = assetDto.Name,
            Type = assetDto.Type,
            Status = assetDto.Status,
            SerialNumber = assetDto.SerialNumber,
            AssetTag = assetDto.AssetTag,
            Manufacturer = assetDto.Manufacturer,
            Model = assetDto.Model,
            PurchaseDate = assetDto.PurchaseDate,
            WarrantyExpiry = assetDto.WarrantyExpiry,
            Location = assetDto.Location
        };

        assetService.AddAsset(asset);

        var resultDto = new AssetDto
        {
            Id = asset.Id,
            Name = asset.Name,
            Type = asset.Type,
            Status = asset.Status,
            SerialNumber = asset.SerialNumber,
            AssetTag = asset.AssetTag,
            Manufacturer = asset.Manufacturer,
            Model = asset.Model,
            PurchaseDate = asset.PurchaseDate,
            WarrantyExpiry = asset.WarrantyExpiry,
            Location = asset.Location,
            EmployeeId = asset.EmployeeId
        };

        return Results.Created($"/api/assets/{asset.Id}", resultDto);
    });


// CREATE ASSETS IN BULK
app.MapPost("/api/assets/bulk",
    (List<Asset> assets, AssetService assetService) =>
    {
        assetService.AddAssets(assets);

        return Results.Ok(
            $"{assets.Count} assets added successfully"
        );
    });


// DELETE ASSET
app.MapDelete("/api/assets/{id}", (int id, AssetService assetService) =>
{
    var deleted = assetService.DeleteAsset(id);

    if (!deleted)
    {
        return Results.NotFound("Asset not found");
    }

    return Results.Ok("Asset deleted successfully");
});


// UPDATE ASSET
app.MapPut("/api/assets/{id}",
    (int id, UpdateAssetDto assetDto, AssetService assetService) =>
    {
        var updatedAsset = new Asset
        {
            Name = assetDto.Name,
            Type = assetDto.Type,
            Status = assetDto.Status,
            SerialNumber = assetDto.SerialNumber,
            AssetTag = assetDto.AssetTag,
            Manufacturer = assetDto.Manufacturer,
            Model = assetDto.Model,
            PurchaseDate = assetDto.PurchaseDate,
            WarrantyExpiry = assetDto.WarrantyExpiry,
            Location = assetDto.Location
        };

        var updated = assetService.UpdateAsset(id, updatedAsset);

        if (!updated)
        {
            return Results.NotFound("Asset not found");
        }

        return Results.Ok("Asset updated successfully");
    });


// ==================== EMPLOYEES ====================

// GET ALL EMPLOYEES
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


// GET EMPLOYEE BY ID
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


// CREATE EMPLOYEE
app.MapPost("/api/employees",
    (CreateEmployeeDto employeeDto, EmployeeService employeeService) =>
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

        return Results.Created(
            $"/api/employees/{employee.Id}",
            resultDto
        );
    });


// CREATE EMPLOYEES IN BULK
app.MapPost("/api/employees/bulk",
    (List<Employee> employees, EmployeeService employeeService) =>
    {
        employeeService.AddEmployees(employees);

        return Results.Ok(
            $"{employees.Count} employees added successfully"
        );
    });


// UPDATE EMPLOYEE
app.MapPut("/api/employees/{id}",
    (int id,
     CreateEmployeeDto employeeDto,
     EmployeeService employeeService) =>
    {
        var employee = new Employee
        {
            EmployeeId = employeeDto.EmployeeId,
            Name = employeeDto.Name,
            Email = employeeDto.Email,
            Department = employeeDto.Department,
            Designation = employeeDto.Designation
        };

        var updated = employeeService.UpdateEmployee(
            id,
            employee
        );

        if (!updated)
        {
            return Results.NotFound("Employee not found");
        }

        return Results.Ok("Employee updated successfully");
    });


// DELETE EMPLOYEE
app.MapDelete("/api/employees/{id}",
    (int id, EmployeeService employeeService) =>
    {
        var result = employeeService.DeleteEmployee(id);

        if (!result.Success)
        {
            if (result.Error == "Employee not found")
            {
                return Results.NotFound(result.Error);
            }

            return Results.Conflict(result.Error);
        }

        return Results.Ok("Employee deleted successfully");
    });


// ==================== ASSET ASSIGNMENT ====================

// ASSIGN ASSET TO EMPLOYEE
app.MapPut("/api/assets/{assetId}/assign/{employeeId}",
    (int assetId,
     int employeeId,
     AssetService assetService) =>
    {
        var assigned = assetService.AssignAsset(
            assetId,
            employeeId
        );

        if (!assigned)
        {
            return Results.BadRequest(
                "Asset or employee not found, or asset is already assigned"
            );
        }

        return Results.Ok("Asset assigned successfully");
    });


// RETURN ASSET
app.MapPut("/api/assets/{assetId}/return",
    (int assetId, AssetService assetService) =>
    {
        var returned = assetService.ReturnAsset(assetId);

        if (!returned)
        {
            return Results.BadRequest(
                "Asset not found or asset is not currently assigned"
            );
        }

        return Results.Ok("Asset returned successfully");
    });


// GET ALL ASSETS OF AN EMPLOYEE
app.MapGet("/api/employees/{employeeId}/assets",
    (int employeeId, AssetService assetService) =>
    {
        var assets = assetService.GetAssetsByEmployeeId(
            employeeId
        );

        return Results.Ok(assets);
    });


// GET EMPLOYEE ASSIGNED TO AN ASSET
app.MapGet("/api/assets/{assetId}/employee",
    (int assetId, AssetService assetService) =>
    {
        var employee = assetService.GetEmployeeByAssetId(
            assetId
        );

        if (employee == null)
        {
            return Results.NotFound(
                "No employee is assigned to this asset"
            );
        }

        return Results.Ok(employee);
    });

app.MapGet("/api/maintenance",
    (MaintenanceService service) =>
    {
        return Results.Ok(service.GetAll());
    });

app.MapGet("/api/maintenance/{id}",
    (int id, MaintenanceService service) =>
    {
        var record = service.GetById(id);

        if (record == null)
        {
            return Results.NotFound("Maintenance record not found");
        }

        return Results.Ok(record);
    });

app.MapPost("/api/assets/{assetId}/maintenance",
    (int assetId, MaintenanceRecord record, MaintenanceService service) =>
    {
        var added = service.Add(assetId, record);

        if (!added)
        {
            return Results.NotFound("Asset not found");
        }

        return Results.Ok("Maintenance record created successfully");
    });

app.MapPut("/api/maintenance/{id}/complete",
    (int id, string resolution, decimal? cost, MaintenanceService service) =>
    {
        var completed = service.Complete(id, resolution, cost);

        if (!completed)
        {
            return Results.NotFound("Maintenance record not found");
        }

        return Results.Ok("Maintenance completed successfully");
    });

app.MapGet("/api/tickets", (TicketService ticketService) =>
{
    var tickets = ticketService.GetTickets();

    var ticketDtos = tickets.Select(ticket => new TicketDto
    {
        Id = ticket.Id,
        Title = ticket.Title,
        Description = ticket.Description,
        Priority = ticket.Priority,
        Status = ticket.Status,
        EmployeeId = ticket.EmployeeId,
        AssetId = ticket.AssetId,
        CreatedDate = ticket.CreatedDate,
        ResolvedDate = ticket.ResolvedDate
    });

    return Results.Ok(ticketDtos);
});

app.MapGet("/api/tickets/{id}",
    (int id, TicketService ticketService) =>
    {
        var ticket = ticketService.GetTicketById(id);

        if (ticket == null)
        {
            return Results.NotFound("Ticket not found");
        }

        var ticketDto = new TicketDto
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Description = ticket.Description,
            Priority = ticket.Priority,
            Status = ticket.Status,
            EmployeeId = ticket.EmployeeId,
            AssetId = ticket.AssetId,
            CreatedDate = ticket.CreatedDate,
            ResolvedDate = ticket.ResolvedDate
        };

        return Results.Ok(ticketDto);
    });

app.MapPost("/api/tickets",
    (CreateTicketDto ticketDto, TicketService ticketService) =>
    {
        var ticket = new Ticket
        {
            Title = ticketDto.Title,
            Description = ticketDto.Description,
            Priority = ticketDto.Priority,
            EmployeeId = ticketDto.EmployeeId,
            AssetId = ticketDto.AssetId,
            Status = TicketStatus.Open
        };

        var result = ticketService.AddTicket(ticket);

        if (!result.Success)
        {
            return Results.BadRequest(result.Error);
        }

        var resultDto = new TicketDto
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Description = ticket.Description,
            Priority = ticket.Priority,
            Status = ticket.Status,
            EmployeeId = ticket.EmployeeId,
            AssetId = ticket.AssetId,
            CreatedDate = ticket.CreatedDate,
            ResolvedDate = ticket.ResolvedDate
        };

        return Results.Created($"/api/tickets/{ticket.Id}", resultDto);
    });

app.MapPut("/api/tickets/{id}",
    (int id, UpdateTicketDto ticketDto, TicketService ticketService) =>
    {
        var ticket = new Ticket
        {
            Title = ticketDto.Title,
            Description = ticketDto.Description,
            Priority = ticketDto.Priority
        };

        var updated = ticketService.UpdateTicket(id, ticket);

        if (!updated)
        {
            return Results.NotFound("Ticket not found");
        }

        return Results.Ok("Ticket updated successfully");
    });

app.MapDelete("/api/tickets/{id}",
    (int id, TicketService ticketService) =>
    {
        var deleted = ticketService.DeleteTicket(id);

        if (!deleted)
        {
            return Results.NotFound("Ticket not found");
        }

        return Results.Ok("Ticket deleted successfully");
    });

app.MapPut("/api/tickets/{id}/start",
    (int id, TicketService ticketService) =>
    {
        var started = ticketService.StartTicket(id);

        if (!started)
        {
            return Results.BadRequest(
                "Ticket not found or ticket is not currently open");
        }

        return Results.Ok("Ticket started successfully");
    });

app.MapPut("/api/tickets/{id}/resolve",
    (int id, TicketService ticketService) =>
    {
        var resolved = ticketService.ResolveTicket(id);

        if (!resolved)
        {
            return Results.BadRequest(
                "Ticket not found or ticket is not currently in progress");
        }

        return Results.Ok("Ticket resolved successfully");
    });

app.MapPut("/api/tickets/{id}/close",
    (int id, TicketService ticketService) =>
    {
        var closed = ticketService.CloseTicket(id);

        if (!closed)
        {
            return Results.BadRequest(
                "Ticket not found or ticket is not currently resolved");
        }

        return Results.Ok("Ticket closed successfully");
    });

app.Run();