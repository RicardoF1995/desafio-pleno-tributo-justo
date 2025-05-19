import { NavLink, useNavigate } from "react-router-dom";
import '../css/Navbar.css';

export default function Navbar() {
  const navigate = useNavigate();

  const handleLogoff = () => {
    localStorage.removeItem("token");
    navigate("/");
  };

  return (
    <nav className="navbar">
      <div className="nav-links">
        <NavLink to="/upload" className="nav-link">Upload</NavLink>
        <NavLink to="/relatorio" className="nav-link">Relatório</NavLink>
        <NavLink to="/estatisticas" className="nav-link">Estatísticas</NavLink>
        <NavLink to="/alertas" className="nav-link">Alertas</NavLink>
      </div>
      <button onClick={handleLogoff} className="logout-button">
        Logoff
      </button>
    </nav>
  );
}
