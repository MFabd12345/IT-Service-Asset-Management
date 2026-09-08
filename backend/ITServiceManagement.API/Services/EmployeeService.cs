using ITServiceManagement.API.Data;
using ITServiceManagement.API.Models;

namespace ITServiceManagement.API.Services;

public class EmployeeService
{
    private readonly AppDbContext _db;

    public EmployeeService(AppDbContext db)
    {
        _db = db;
    }

    public List<Employee> GetEmployees()
    {
        return _db.Employees.ToList();
    }

    public Employee? GetEmployeeById(int id)
    {
        return _db.Employees.Find(id);
    }

    public void AddEmployee(Employee employee)
    {
        _db.Employees.Add(employee);
        _db.SaveChanges();
    }

    public void AddEmployees(List<Employee> employees)
    {
        _db.Employees.AddRange(employees);
        _db.SaveChanges();
    }

    public (bool Success, string? Error) DeleteEmployee(int id)
    {
        var employee = _db.Employees.Find(id);

        if (employee == null)
        {
            return (false, "Employee not found");
        }

        var hasAssignedAssets = _db.Assets
            .Any(a => a.EmployeeId == id);

        if (hasAssignedAssets)
        {
            return (false, "Employee has assets assigned and cannot be deleted");
        }

        _db.Employees.Remove(employee);
        _db.SaveChanges();

        return (true, null);
    }

    public bool UpdateEmployee(int id, Employee updatedEmployee)
    {
        var employee = _db.Employees.Find(id);

        if (employee == null)
        {
            return false;
        }

        employee.EmployeeId = updatedEmployee.EmployeeId;
        employee.Name = updatedEmployee.Name;
        employee.Email = updatedEmployee.Email;
        employee.Department = updatedEmployee.Department;
        employee.Designation = updatedEmployee.Designation;

        _db.SaveChanges();

        return true;
    }
}