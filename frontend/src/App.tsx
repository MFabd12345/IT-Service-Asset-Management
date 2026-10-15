
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Sidebar from "./components/Sidebar";
import ProtectedRoute from "./components/ProtectedRoute";
import Login from "./pages/Login";
import Dashboard from "./pages/Dashboard";
import Assets from "./pages/Assets";
import Employees from "./pages/Employees";
import Maintenance from "./pages/Maintenance";
import Tickets from "./pages/Tickets";
import "./App.css";

function AppLayout() {
  return (
    <div className="app">
      <Sidebar />
      <main className="main-content">
        <header className="page-header">
          <h1>IT Service Management</h1>
        </header>

        <div className="page-content">
          <Routes>
            <Route path="/" element={<Dashboard />} />

            <Route
              path="/assets"
              element={
                <ProtectedRoute allowedRoles={["Admin"]}>
                  <Assets />
                </ProtectedRoute>
              }
            />

            <Route
              path="/employees"
              element={
                <ProtectedRoute allowedRoles={["Admin"]}>
                  <Employees />
                </ProtectedRoute>
              }
            />

            <Route
              path="/maintenance"
              element={
                <ProtectedRoute allowedRoles={["Admin"]}>
                  <Maintenance />
                </ProtectedRoute>
              }
            />

            <Route path="/tickets" element={<Tickets />} />
            <Route path="*" element={<Navigate to="/" replace />} />
          </Routes>
        </div>
      </main>
    </div>
  );
}

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route
          path="/*"
          element={
            <ProtectedRoute>
              <AppLayout />
            </ProtectedRoute>
          }
        />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
