import { useState } from "react";
import axios from "axios";
import "../css/Upload.css";
import LoadingSpinner from "../components/LoadingSpinner";
import Mensagem from "../components/Mensagem";

export default function Upload() {
  const [isLoading, setIsLoading] = useState(false);
  const [file, setFile] = useState(null);
  const [tipoMensagem, setTipoMensagem] = useState("");
  const [mensagemTexto, setMensagemTexto] = useState("");

  const exibirMensagem = (tipo, texto) => {
    setTipoMensagem(tipo);
    setMensagemTexto(texto);
    setTimeout(() => setMensagemTexto(""), 5000);
  };

  const handleFileChange = (e) => {
    setFile(e.target.files[0]);
    setTipoMensagem("");
    setMensagemTexto("");
  };

  const handleUpload = async () => {
    if (!file) {
      exibirMensagem("erro", "Selecione um arquivo CSV.");
      return;
    }

    const formData = new FormData();
    formData.append("file", file);

    try {
      setIsLoading(true);
      await axios.post("/api/uploadarquivo", formData, {
        headers: {
          "Content-Type": "multipart/form-data",
          Authorization: `Bearer ${localStorage.getItem("token")}`,
        },
      });
      exibirMensagem("sucesso", "Arquivo enviado com sucesso!");
      setFile(null);
    } catch (err) {
      exibirMensagem("erro", "Erro ao enviar o arquivo.");
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="upload-container">
      <h2>Upload de Arquivo CSV</h2>
      {isLoading && <LoadingSpinner />}

      <Mensagem tipo={tipoMensagem} texto={mensagemTexto} />

      <div className="upload-box">
        <input
          type="file"
          accept=".csv"
          id="file-input"
          onChange={handleFileChange}
        />
        {file && <p className="file-name">📄 {file.name}</p>}

        <button onClick={handleUpload}>Enviar</button>
      </div>
    </div>
  );
}
