import { NavLink } from "react-router-dom";

function Sidebar() {
  return (
    <aside className="sidebar">
      <div className="logo">
        <h2>ITSM</h2>
        <span>IT Service Management</span>
      </div>

      <nav>
        <NavLink to="/">
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
    </aside>
  );
}

export default Sidebar;