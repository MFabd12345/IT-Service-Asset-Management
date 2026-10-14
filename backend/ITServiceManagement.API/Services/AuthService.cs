
using ITServiceManagement.API.Data;
using ITServiceManagement.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITServiceManagement.API.Services;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly PasswordHasher<User> _passwordHasher;

    public AuthService(AppDbContext context)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<User>();
    }

    public User CreateEmployeeUser(
        string username,
        string email,
        string password,
        int employeeId)
    {
        if (_context.Users.Any(u => u.Email == email))
        {
            throw new InvalidOperationException(
                "An account with this email already exists.");
        }

        var employee = _context.Employees
            .FirstOrDefault(e => e.Id == employeeId);

        if (employee == null)
        {
            throw new KeyNotFoundException(
                "Employee not found.");
        }

        if (_context.Users.Any(u => u.EmployeeId == employeeId))
        {
            throw new InvalidOperationException(
                "This employee already has a login account.");
        }

        if (!string.Equals(
                employee.Email,
                email,
                StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                "The email must match the employee record.");
        }

        var user = new User
        {
            Username = username,
            Email = email,
            Role = "User",
            EmployeeId = employee.Id
        };

        user.PasswordHash =
            _passwordHasher.HashPassword(user, password);

        _context.Users.Add(user);
        _context.SaveChanges();

        return user;
    }

    public User? ValidateUser(string email, string password)
    {
        var user = _context.Users
            .FirstOrDefault(u => u.Email == email);

        if (user == null)
        {
            return null;
        }

        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            password
        );

        if (result == PasswordVerificationResult.Failed)
        {
            return null;
        }

        return user;
    }
}
