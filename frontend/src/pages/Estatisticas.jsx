import React, { useEffect, useState } from "react";
import axios from "axios";
import "../css/Estatisticas.css";
import LoadingSpinner from "../components/LoadingSpinner";

export default function Estatisticas() {
    const [kpis, setKpis] = useState(null);
    const [isLoading, setIsLoading] = useState(false);

    useEffect(() => {
        const fetchKpis = async () => {
            try {
                setIsLoading(true);
                const res = await axios.get("/api/estatisticas", {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem("token")}`,
                    },
                });
                setKpis(res.data);
            } catch (err) {
                console.error("Erro ao carregar KPIs:", err);
            } finally {
                setIsLoading(false);
            }
        };

        fetchKpis();
    }, []);

    if (isLoading || !kpis) return <LoadingSpinner />;

    return (
        <div className="kpis-container">
            <h1>Estatísticas</h1>
            <div className="kpi-cards">
                <div className="kpi-card">
                    <h2>Total de Empresas</h2>
                    <p>{kpis.totalEmpresas}</p>
                </div>
                <div className="kpi-card">
                    <h2>Total de Notas</h2>
                    <p>{kpis.totalNotasFiscais}</p>
                </div>
                <div className="kpi-card">
                    <h2>Total de Itens</h2>
                    <p>{kpis.totalItens}</p>
                </div>
                <div className="kpi-card">
                    <h2>Valor Total das Notas</h2>
                    <p>{kpis.valorTotalNotas?.toLocaleString("pt-BR", { style: "currency", currency: "BRL" })}</p>
                </div>
                <div className="kpi-card">
                    <h2>Total de Imposto Recolhido</h2>
                    <p>{kpis.totalImpostos?.toLocaleString("pt-BR", { style: "currency", currency: "BRL" })}</p>
                </div>
            </div>
        </div>
    );
}
