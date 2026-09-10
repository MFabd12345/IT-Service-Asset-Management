import { useEffect, useState } from "react";
import api from "../services/api";

interface Ticket {
  id: number;
  title: string;
  description: string;
  priority: string;
  status: string;
  employeeId: number;
  assetId: number | null;
  createdDate: string;
  resolvedDate: string | null;
}

function Tickets() {
  const [tickets, setTickets] = useState<Ticket[]>([]);
  const [loading, setLoading] = useState(true);

  const loadTickets = async () => {
    try {
      const response = await api.get("/tickets");
      setTickets(response.data);
    } catch (error) {
      console.error("Failed to load tickets:", error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadTickets();
  }, []);

  const startTicket = async (id: number) => {
    try {
      await api.put(`/tickets/${id}/start`);
      await loadTickets();
    } catch (error) {
      console.error("Failed to start ticket:", error);
      alert("Unable to start ticket.");
    }
  };

  const resolveTicket = async (id: number) => {
    try {
      await api.put(`/tickets/${id}/resolve`);
      await loadTickets();
    } catch (error) {
      console.error("Failed to resolve ticket:", error);
      alert("Unable to resolve ticket.");
    }
  };

  const closeTicket = async (id: number) => {
    try {
      await api.put(`/tickets/${id}/close`);
      await loadTickets();
    } catch (error) {
      console.error("Failed to close ticket:", error);
      alert("Unable to close ticket.");
    }
  };

  const priorityClass = (priority: string) => {
    return `badge priority-${priority.toLowerCase()}`;
  };

  const statusClass = (status: string) => {
    return `badge status-${status.toLowerCase()}`;
  };

  const formatDate = (date: string) => {
    return new Date(date).toLocaleDateString();
  };

  return (
    <div>
      <h1>Tickets</h1>
      <p>Manage IT incidents and service requests.</p>

      {loading ? (
        <h3>Loading tickets...</h3>
      ) : tickets.length === 0 ? (
        <div className="empty-state">
          No tickets found.
        </div>
      ) : (
        <table>
          <thead>
            <tr>
              <th>ID</th>
              <th>Title</th>
              <th>Priority</th>
              <th>Status</th>
              <th>Employee</th>
              <th>Asset</th>
              <th>Created</th>
              <th>Actions</th>
            </tr>
          </thead>

          <tbody>
            {tickets.map((ticket) => (
              <tr key={ticket.id}>
                <td>{ticket.id}</td>

                <td>
                  <strong>{ticket.title}</strong>
                </td>

                <td>
                  <span className={priorityClass(ticket.priority)}>
                    {ticket.priority}
                  </span>
                </td>

                <td>
                  <span className={statusClass(ticket.status)}>
                    {ticket.status}
                  </span>
                </td>

                <td>{ticket.employeeId}</td>

                <td>
                  {ticket.assetId ?? "None"}
                </td>

                <td>
                  {formatDate(ticket.createdDate)}
                </td>

                <td>
                  <div className="action-buttons">
                    {ticket.status === "Open" && (
                      <button
                        className="btn"
                        onClick={() => startTicket(ticket.id)}
                      >
                        Start
                      </button>
                    )}

                    {ticket.status === "InProgress" && (
                      <button
                        className="btn"
                        onClick={() => resolveTicket(ticket.id)}
                      >
                        Resolve
                      </button>
                    )}

                    {ticket.status === "Resolved" && (
                      <button
                        className="btn"
                        onClick={() => closeTicket(ticket.id)}
                      >
                        Close
                      </button>
                    )}

                    {ticket.status === "Closed" && (
                      <span className="completed">
                        Completed
                      </span>
                    )}
                  </div>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}

export default Tickets;