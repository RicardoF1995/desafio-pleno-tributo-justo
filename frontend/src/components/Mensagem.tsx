import React from 'react';
import '../css/Mensagem.css';

interface MensagemProps {
  tipo: 'sucesso' | 'erro';
  texto: string;
}

const Mensagem: React.FC<MensagemProps> = ({ tipo, texto }) => {
  if (!texto) return null;

  return (
    <div className={`mensagem ${tipo}`}>
      {texto}
    </div>
  );
};

export default Mensagem;
