import { useState } from "react";
import axios from "axios";
import "../css/Upload.css";

export default function Upload() {
  const [file, setFile] = useState(null);
  const [mensagem, setMensagem] = useState("");
  const [erro, setErro] = useState("");

  const handleFileChange = (e) => {
    setFile(e.target.files[0]);
    setMensagem("");
    setErro("");
  };

  const handleUpload = async () => {
    if (!file) {
      setErro("Selecione um arquivo CSV.");
      return;
    }

    const formData = new FormData();
    formData.append("file", file);

    try {
      await axios.post("/api/uploadarquivo", formData, {
        headers: {
          "Content-Type": "multipart/form-data",
          Authorization: `Bearer ${localStorage.getItem("token")}`,
        },
      });
      setMensagem("Arquivo enviado com sucesso!");
      setErro("");
      setFile(null);
    } catch (err) {
      setErro("Erro ao enviar o arquivo.");
      setMensagem("");
    }
  };

  return (
    <div className="upload-container">
      <h2>Upload de Arquivo CSV</h2>

      <div className="upload-box">
        <input
          type="file"
          accept=".csv"
          id="file-input"
          onChange={handleFileChange}
        />
        {file && <p className="file-name">📄 {file.name}</p>}

        <button onClick={handleUpload}>Enviar</button>

        {mensagem && <p className="mensagem sucesso">{mensagem}</p>}
        {erro && <p className="mensagem erro">{erro}</p>}
      </div>
    </div>
  );
}
