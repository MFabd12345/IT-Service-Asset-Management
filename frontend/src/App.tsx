import { BrowserRouter, Routes, Route } from "react-router-dom";

import Sidebar from "./components/Sidebar";

import Dashboard from "./pages/Dashboard";
import Assets from "./pages/Assets";
import Employees from "./pages/Employees";
import Maintenance from "./pages/Maintenance";
import Tickets from "./pages/Tickets";

import "./App.css";

function App() {
  return (
    <BrowserRouter>
      <div className="app">
        <Sidebar />

        <main className="main-content">
          <Routes>
            <Route path="/" element={<Dashboard />} />
            <Route path="/assets" element={<Assets />} />
            <Route path="/employees" element={<Employees />} />
            <Route path="/maintenance" element={<Maintenance />} />
            <Route path="/tickets" element={<Tickets />} />
          </Routes>
        </main>
      </div>
    </BrowserRouter>
  );
}

export default App;