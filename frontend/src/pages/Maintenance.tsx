import { useEffect, useState } from "react";
import api from "../services/api";

interface MaintenanceRecord {
  id: number;
  assetId: number;
  issue: string;
  resolution: string | null;
  reportedDate: string;
  completedDate: string | null;
  cost: number | null;
}

function Maintenance() {
  const [records, setRecords] = useState<MaintenanceRecord[]>([]);
  const [loading, setLoading] = useState(true);

  const loadMaintenance = async () => {
    try {
      const response = await api.get("/maintenance");
      setRecords(response.data);
    } catch (error) {
      console.error("Failed to load maintenance:", error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadMaintenance();
  }, []);

  const formatDate = (date: string | null) => {
    if (!date) return "-";

    return new Date(date).toLocaleDateString();
  };

  return (
    <div>
      <h1>Maintenance</h1>
      <p>Monitor IT asset maintenance records.</p>

      {loading ? (
        <h3>Loading maintenance records...</h3>
      ) : records.length === 0 ? (
        <div className="empty-state">
          No maintenance records found.
        </div>
      ) : (
        <table>
          <thead>
            <tr>
              <th>ID</th>
              <th>Asset ID</th>
              <th>Issue</th>
              <th>Resolution</th>
              <th>Reported Date</th>
              <th>Completed Date</th>
              <th>Cost</th>
            </tr>
          </thead>

          <tbody>
            {records.map((record) => (
              <tr key={record.id}>
                <td>{record.id}</td>
                <td>{record.assetId}</td>
                <td>{record.issue}</td>
                <td>{record.resolution ?? "Pending"}</td>
                <td>{formatDate(record.reportedDate)}</td>
                <td>{formatDate(record.completedDate)}</td>
                <td>
                  {record.cost !== null
                    ? `₹${record.cost}`
                    : "-"}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}

export default Maintenance;