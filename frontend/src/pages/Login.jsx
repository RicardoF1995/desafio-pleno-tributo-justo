import { useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import '../css/Login.css';

export default function Login() {
  const [isCadastro, setIsCadastro] = useState(false);
  const [nomeUsuario, setNomeUsuario] = useState("");
  const [senha, setSenha] = useState("");
  const navigate = useNavigate();

  const [tipoMensagem, setTipoMensagem] = useState("");
  const [mensagemSucesso, setMensagemSucesso] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (isCadastro) {
        await axios.post("/api/auth/register", { nomeUsuario, senha });
        setTipoMensagem("sucesso");
        setMensagemSucesso("Cadastro realizado com sucesso!");
        setTimeout(() => setMensagemSucesso(""), 5000);
        setIsCadastro(false);
      } else {
        const res = await axios.post("/api/auth/login", { nomeUsuario, senha });
        localStorage.setItem("token", res.data.token);
        navigate("/upload");
      }
    } catch (err) {
      setTipoMensagem("erro");
      setMensagemSucesso("Erro ao cadastrar usuário!");
      setTimeout(() => setMensagemSucesso(""), 5000);
      setIsCadastro(false);
    }
  };

  return (
    <div className="container">
      <img
        src="https://tributojusto.com.br/wp-content/uploads/2024/11/Logo-1.svg"
        alt="Logo da empresa"
        style={{ width: "150px", marginBottom: "20px" }}
      />
      {mensagemSucesso && (
        <div className={`mensagem-sucesso ${tipoMensagem}`}>
         {mensagemSucesso}
       </div>
      )}
      <h1 className="title">{isCadastro ? "Cadastre-se" : "Login"}</h1>
      <form className="form" onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="Usuário"
          value={nomeUsuario}
          onChange={e => setNomeUsuario(e.target.value)}
          required
          className="input"
        />
        <input
          type="password"
          placeholder="Senha"
          value={senha}
          onChange={e => setSenha(e.target.value)}
          required
          className="input"
        />
        <button type="submit" className="button">
          {isCadastro ? "Cadastrar" : "Entrar"}
        </button>
      </form>
      <button 
        onClick={() => setIsCadastro(!isCadastro)} 
        className="switchButton"
      >
        {isCadastro ? "Já tem uma conta? Faça login" : "Não tem conta? Cadastre-se"}
      </button>
    </div>
  );
}
