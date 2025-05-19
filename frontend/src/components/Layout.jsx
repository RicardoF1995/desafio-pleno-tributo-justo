// src/components/Layout.jsx
import Navbar from "./Navbar";
import { Outlet } from "react-router-dom";

const Layout = () => {
  return (
    <>
      <Navbar />
      <main style={{ padding: "1rem" }}>
        <Outlet />
      </main>
    </>
  );
};

export default Layout;
