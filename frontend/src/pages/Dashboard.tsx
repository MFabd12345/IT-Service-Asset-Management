import { useEffect, useState } from "react";
import api from "../services/api";
import StatCard from "../components/StatCard";

interface Asset {
  id: number;
  name: string;
  status: string;
}

interface Employee {
  id: number;
}

interface Ticket {
  id: number;
  status: string;
}

function Dashboard() {
  const [assets, setAssets] = useState<Asset[]>([]);
  const [employees, setEmployees] = useState<Employee[]>([]);
  const [tickets, setTickets] = useState<Ticket[]>([]);

  useEffect(() => {
    const loadDashboard = async () => {
      try {
        const [assetsResponse, employeesResponse, ticketsResponse] =
          await Promise.all([
            api.get("/assets"),
            api.get("/employees"),
            api.get("/tickets"),
          ]);

        setAssets(assetsResponse.data);
        setEmployees(employeesResponse.data);
        setTickets(ticketsResponse.data);
      } catch (error) {
        console.error("Dashboard loading failed:", error);
      }
    };

    loadDashboard();
  }, []);

  const countAssets = (status: string) =>
    assets.filter((asset) => asset.status === status).length;

  const countTickets = (status: string) =>
    tickets.filter((ticket) => ticket.status === status).length;

  return (
    <div>
      <h2>Dashboard</h2>
      <p>IT Service Management Overview</p>

      <div className="stats-grid">
        <StatCard title="Total Assets" value={assets.length} />
        <StatCard title="Available Assets" value={countAssets("Available")} />
        <StatCard title="Assigned Assets" value={countAssets("Assigned")} />
        <StatCard title="Under Repair" value={countAssets("UnderRepair")} />
        <StatCard title="Retired Assets" value={countAssets("Retired")} />
        <StatCard title="Employees" value={employees.length} />
        <StatCard title="Open Tickets" value={countTickets("Open")} />
        <StatCard title="In Progress" value={countTickets("InProgress")} />
        <StatCard title="Resolved Tickets" value={countTickets("Resolved")} />
      </div>
    </div>
  );
}

export default Dashboard;