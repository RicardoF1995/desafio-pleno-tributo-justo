import React, { useEffect, useState } from "react";
import axios from "axios";
import "../css/Relatorios.css";
import LoadingSpinner from "../components/LoadingSpinner";
import Mensagem from "../components/Mensagem";

export default function Alertas() {
    const [expanded, setExpanded] = useState({});
    const [dados, setDados] = useState([]);
    const [isLoading, setIsLoading] = useState(false);
    const [tipoMensagem, setTipoMensagem] = useState("");
    const [mensagemTexto, setMensagemTexto] = useState("");
    const [filtroTexto, setFiltroTexto] = useState("");
    const [dataInicio, setDataInicio] = useState("");
    const [dataFim, setDataFim] = useState("");

    useEffect(() => {
        const fetchData = async () => {
            try {
                setIsLoading(true);
                const res = await axios.get("/api/alertas", {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem("token")}`,
                    },
                });
                setDados(res.data);
            } catch (err) {
                setTipoMensagem("erro");
                setMensagemTexto("Erro ao carregar relatório.");
                console.error("Erro:", err);
            } finally {
                setIsLoading(false);
            }
        };

        fetchData();
    }, []);

    const toggleExpand = (empresaId) => {
        setExpanded((prev) => ({
            ...prev,
            [empresaId]: !prev[empresaId],
        }));
    };

    const filtrarNotas = (notas) => {
        return notas.filter((nota) => {
            const data = new Date(nota.dataEmissao);
            const inicio = dataInicio ? new Date(dataInicio) : null;
            const fim = dataFim ? new Date(dataFim) : null;

            if (inicio && data < inicio) return false;
            if (fim && data > fim) return false;

            return true;
        });
    };

    return (
        <div className="tabela-container">
            {isLoading && <LoadingSpinner />}
            <Mensagem tipo={tipoMensagem} texto={mensagemTexto} />

            <h1>Alertas (notas com diferença de 50%)</h1>

            <div className="filtros-container">
                <h2>Filtros</h2>
                <div className="filtros-relatorio">
                    <div className="filtro-item">
                        <label htmlFor="filtroTexto">CNPJ / Razão Social</label>
                        <input
                            type="text"
                            id="filtroTexto"
                            placeholder="CNPJ ou Razão Social"
                            value={filtroTexto}
                            onChange={(e) => setFiltroTexto(e.target.value)}
                        />
                    </div>

                    <div className="filtro-item">
                        <label>Data Início</label>
                        <input
                            type="date"
                            value={dataInicio}
                            onChange={(e) => setDataInicio(e.target.value)}
                        />
                    </div>

                    <div className="filtro-item">
                        <label>Data Fim</label>
                        <input
                            type="date"
                            value={dataFim}
                            onChange={(e) => setDataFim(e.target.value)}
                        />
                    </div>
                </div>
            </div>

            <table className="tabela-relatorio">
                <thead>
                    <tr>
                        <th>CNPJ / Razão Social</th>
                        <th>Número Nota</th>
                        <th>Data Emissão</th>
                        <th>Valor Total (R$)</th>
                        <th>Imposto Recolhido (R$)</th>
                        <th>Diferença (R$)</th>
                        <th>Diferença (%)</th>
                    </tr>
                </thead>
                <tbody>
                    {dados
                        .filter((empresa) => {
                            const termo = filtroTexto.toLowerCase();
                            return (
                                empresa.razaoSocial.toLowerCase().includes(termo) ||
                                empresa.cnpj.includes(termo)
                            );
                        })
                        .map((empresa) => {
                            const notasFiltradas = filtrarNotas(empresa.lstNotasFiscais);
                            if (notasFiltradas.length === 0) return null;

                            return (
                                <React.Fragment key={empresa.id}>
                                    <tr
                                        className="grupo-empresa"
                                        onClick={() => toggleExpand(empresa.id)}
                                    >
                                        <td colSpan="7">
                                            {expanded[empresa.id] ? "▼" : "►"}{" "}
                                            <strong>{empresa.cnpj}</strong> - {empresa.razaoSocial}
                                        </td>
                                    </tr>

                                    {expanded[empresa.id] &&
                                        notasFiltradas.map((nota) => (
                                            <tr key={nota.id} className="linha-nota">
                                                <td></td>
                                                <td>{nota.numeroNota}</td>
                                                <td>{new Date(nota.dataEmissao).toLocaleDateString()}</td>
                                                <td>{nota.valorTotal.toLocaleString("pt-BR", { style: "currency", currency: "BRL" })}</td>
                                                <td>{nota.impostoRecolhido.toLocaleString("pt-BR", { style: "currency", currency: "BRL" })}</td>
                                                <td>{nota.diferenca.toLocaleString("pt-BR", { style: "currency", currency: "BRL" })}</td>
                                                <td>{nota.diferencaPercentual.toFixed(2).replace('.', ',')}%</td>
                                            </tr>
                                        ))}
                                </React.Fragment>
                            );
                        })}
                </tbody>
            </table>
        </div>
    );
}
