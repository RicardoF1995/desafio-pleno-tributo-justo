import { useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import '../css/Login.css';
import Mensagem from "../components/Mensagem";

export default function Login() {
  const [isCadastro, setIsCadastro] = useState(false);
  const [nomeUsuario, setNomeUsuario] = useState("");
  const [senha, setSenha] = useState("");
  const navigate = useNavigate();

  const [tipoMensagem, setTipoMensagem] = useState("");
  const [mensagemTexto, setMensagemTexto] = useState("");

  const exibirMensagem = (tipo, texto) => {
    setTipoMensagem(tipo);
    setMensagemTexto(texto);
    setTimeout(() => setMensagemTexto(""), 5000);
  };

  const validarCampos = () => {
    if (!nomeUsuario || nomeUsuario.length < 5 || nomeUsuario.length > 50) {
      return "O nome deve ter entre 5 e 50 caracteres.";
    }
    if (!senha || senha.length < 5 || senha.length > 100) {
      return "A senha deve ter entre 5 e 100 caracteres.";
    }
    return null;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const erroValidacao = validarCampos();
    if (erroValidacao) {
      exibirMensagem("erro", erroValidacao);
      return;
    }

    try {
      if (isCadastro) {
        await axios.post("/api/auth/register", { nomeUsuario, senha });
        exibirMensagem("sucesso", "Cadastro realizado com sucesso!");
        setIsCadastro(false);
      } else {
        const res = await axios.post("/api/auth/login", { nomeUsuario, senha });
        localStorage.setItem("token", res.data.token);
        navigate("/home");
      }
    } catch (err) {
      if (err.response && err.response.status === 401) {
        exibirMensagem("erro", "Usuário ou senha inválidos.");
      } else {
        const mensagemErro = isCadastro
          ? "Erro ao cadastrar usuário!"
          : "Erro ao efetuar login!";
        exibirMensagem("erro", mensagemErro);
      }
    }
  
  };

  return (
    <div className="container">
      <img
        src="https://tributojusto.com.br/wp-content/uploads/2024/11/Logo-1.svg"
        alt="Logo da empresa"
        style={{ width: "150px", marginBottom: "20px" }}
      />
      <Mensagem tipo={tipoMensagem} texto={mensagemTexto} />
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
