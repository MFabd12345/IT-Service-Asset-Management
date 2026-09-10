import { useEffect, useState } from "react";
import api from "../services/api";

interface Employee {
  id: number;
  employeeId: string;
  name: string;
  email: string;
  department: string;
  designation: string;
}

function Employees() {
  const [employees, setEmployees] = useState<Employee[]>([]);
  const [loading, setLoading] = useState(true);

  const loadEmployees = async () => {
    try {
      const response = await api.get("/employees");
      setEmployees(response.data);
    } catch (error) {
      console.error("Failed to load employees:", error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadEmployees();
  }, []);

  return (
    <div>
      <h1>Employees</h1>
      <p>Manage employees and their IT information.</p>

      {loading ? (
        <h3>Loading employees...</h3>
      ) : (
        <table>
          <thead>
            <tr>
              <th>ID</th>
              <th>Employee ID</th>
              <th>Name</th>
              <th>Email</th>
              <th>Department</th>
              <th>Designation</th>
            </tr>
          </thead>

          <tbody>
            {employees.map((employee) => (
              <tr key={employee.id}>
                <td>{employee.id}</td>
                <td>{employee.employeeId}</td>
                <td>{employee.name}</td>
                <td>{employee.email}</td>
                <td>{employee.department}</td>
                <td>{employee.designation}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}

export default Employees;