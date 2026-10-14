
namespace ITServiceManagement.API.Models;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string Role { get; set; } = "User";

    // Links an employee login to an employee record.
    public int? EmployeeId { get; set; }

    public Employee? Employee { get; set; }
}
