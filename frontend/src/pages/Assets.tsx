import { useEffect, useState } from "react";
import api from "../services/api";

interface Asset {
  id: number;
  name: string;
  type: string;
  status: string;
  serialNumber: string;
  assetTag: string;
  manufacturer: string;
  model: string;
  location: string;
  employeeId: number | null;
}

function Assets() {
  const [assets, setAssets] = useState<Asset[]>([]);
  const [loading, setLoading] = useState(true);

  const loadAssets = async () => {
    try {
      const response = await api.get("/assets");
      setAssets(response.data);
    } catch (error) {
      console.error("Failed to load assets:", error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadAssets();
  }, []);

  if (loading) {
    return <h2>Loading assets...</h2>;
  }

  return (
    <div>
      <h2>Assets</h2>
      <p>Manage and monitor IT assets.</p>

      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Type</th>
            <th>Status</th>
            <th>Serial Number</th>
            <th>Asset Tag</th>
            <th>Manufacturer</th>
            <th>Model</th>
            <th>Location</th>
            <th>Employee</th>
          </tr>
        </thead>

        <tbody>
          {assets.map((asset) => (
            <tr key={asset.id}>
              <td>{asset.id}</td>
              <td>{asset.name}</td>
              <td>{asset.type}</td>
              <td>{asset.status}</td>
              <td>{asset.serialNumber}</td>
              <td>{asset.assetTag}</td>
              <td>{asset.manufacturer}</td>
              <td>{asset.model}</td>
              <td>{asset.location}</td>
              <td>{asset.employeeId ?? "Unassigned"}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default Assets;