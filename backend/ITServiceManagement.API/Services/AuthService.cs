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

    public User CreateUser(
        string username,
        string email,
        string password,
        string role = "User")
    {
        var user = new User
        {
            Username = username,
            Email = email,
            Role = role
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