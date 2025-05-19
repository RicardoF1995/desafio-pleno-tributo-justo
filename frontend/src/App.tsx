import { Routes, Route, Navigate } from "react-router-dom";
import Home from "./pages/Home";
import LoginPage from "./pages/Login";
import Upload from "./pages/Upload";
import Relatorios from "./pages/Relatorios";
import Alertas from "./pages/Alertas";
import Estatisticas from "./pages/Estatisticas";
import ProtectedRoute from "./auth/ProtectedRoute";
import Layout from "./components/Layout";

function App() {
  return (
    <Routes>
      <Route path="/login" element={<LoginPage />} />
      <Route
        element={
          <ProtectedRoute>
            <Layout />
          </ProtectedRoute>
        }
      >
        <Route
          path="/home"
          element={
            <ProtectedRoute>
              <Home />
            </ProtectedRoute>
          }
        />
        <Route
          path="/upload"
          element={
            <ProtectedRoute>
              <Upload />
            </ProtectedRoute>
          }
        />
        <Route
          path="/relatorio"
          element={
            <ProtectedRoute>
              <Relatorios />
            </ProtectedRoute>
          }
        />
        <Route
          path="/alertas"
          element={
            <ProtectedRoute>
              <Alertas />
            </ProtectedRoute>
          }
        />
        <Route
          path="/estatisticas"
          element={
            <ProtectedRoute>
              <Estatisticas />
            </ProtectedRoute>
          }
        />
      </Route>
      <Route path="*" element={<Navigate to="/home" />} />
    </Routes>
  );
}

export default App;
