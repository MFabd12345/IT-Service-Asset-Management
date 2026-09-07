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

    public bool DeleteEmployee(int id)
    {
        var employee = _db.Employees.Find(id);

        if (employee == null)
        {
            return false;
        }

        _db.Employees.Remove(employee);
        _db.SaveChanges();

        return true;
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