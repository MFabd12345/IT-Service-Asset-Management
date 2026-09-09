using ITServiceManagement.API.Data;
using ITServiceManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ITServiceManagement.API.Services;

public class TicketService
{
    private readonly AppDbContext _db;

    public TicketService(AppDbContext db)
    {
        _db = db;
    }

    public List<Ticket> GetTickets()
    {
        return _db.Tickets
            .Include(t => t.Employee)
            .Include(t => t.Asset)
            .ToList();
    }

    public Ticket? GetTicketById(int id)
    {
        return _db.Tickets
            .Include(t => t.Employee)
            .Include(t => t.Asset)
            .FirstOrDefault(t => t.Id == id);
    }

    public (bool Success, string? Error, Ticket? Ticket) AddTicket(
        Ticket ticket)
    {
        var employee = _db.Employees.Find(ticket.EmployeeId);

        if (employee == null)
        {
            return (false, "Employee not found", null);
        }

        if (ticket.AssetId != null)
        {
            var asset = _db.Assets.Find(ticket.AssetId);

            if (asset == null)
            {
                return (false, "Asset not found", null);
            }

            if (asset.EmployeeId != ticket.EmployeeId)
            {
                return (
                    false,
                    "Asset is not assigned to this employee",
                    null
                );
            }
        }

        _db.Tickets.Add(ticket);
        _db.SaveChanges();

        return (true, null, ticket);
    }

    public bool UpdateTicket(int id, Ticket updatedTicket)
    {
        var ticket = _db.Tickets.Find(id);

        if (ticket == null)
        {
            return false;
        }

        ticket.Title = updatedTicket.Title;
        ticket.Description = updatedTicket.Description;
        ticket.Priority = updatedTicket.Priority;

        _db.SaveChanges();

        return true;
    }

    public bool DeleteTicket(int id)
    {
        var ticket = _db.Tickets.Find(id);

        if (ticket == null)
        {
            return false;
        }

        _db.Tickets.Remove(ticket);
        _db.SaveChanges();

        return true;
    }

    public bool StartTicket(int id)
    {
        var ticket = _db.Tickets.Find(id);

        if (ticket == null)
        {
            return false;
        }

        if (ticket.Status != TicketStatus.Open)
        {
            return false;
        }

        ticket.Status = TicketStatus.InProgress;

        _db.SaveChanges();

        return true;
    }

    public bool ResolveTicket(int id)
    {
        var ticket = _db.Tickets.Find(id);

        if (ticket == null)
        {
            return false;
        }

        if (ticket.Status != TicketStatus.InProgress)
        {
            return false;
        }

        ticket.Status = TicketStatus.Resolved;
        ticket.ResolvedDate = DateTime.UtcNow;

        _db.SaveChanges();

        return true;
    }

    public bool CloseTicket(int id)
    {
        var ticket = _db.Tickets.Find(id);

        if (ticket == null)
        {
            return false;
        }

        if (ticket.Status != TicketStatus.Resolved)
        {
            return false;
        }

        ticket.Status = TicketStatus.Closed;

        _db.SaveChanges();

        return true;
    }
}