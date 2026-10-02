import { NavLink, useNavigate } from "react-router-dom";

function Sidebar() {
  const navigate = useNavigate();

  const username = localStorage.getItem("username") || "Admin";
  const role = localStorage.getItem("role") || "Admin";

  const handleLogout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("username");
    localStorage.removeItem("email");
    localStorage.removeItem("role");

    navigate("/login", { replace: true });
  };

  return (
    <aside className="sidebar">
      <div className="logo">
        <h2>ITSM</h2>
        <span>IT Service Management</span>
      </div>

      <div className="user-info">
        <strong>{username}</strong>
        <span>{role}</span>
      </div>

      <nav>
        <NavLink to="/" end>
          Dashboard
        </NavLink>

        <NavLink to="/assets">
          Assets
        </NavLink>

        <NavLink to="/employees">
          Employees
        </NavLink>

        <NavLink to="/maintenance">
          Maintenance
        </NavLink>

        <NavLink to="/tickets">
          Tickets
        </NavLink>
      </nav>

      <button className="logout-button" onClick={handleLogout}>
        Logout
      </button>
    </aside>
  );
}

export default Sidebar;