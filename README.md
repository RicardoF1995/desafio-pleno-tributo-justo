# 📊 Desafio Tributo Justo - Pleno

Sistema desenvolvido para o desafio técnico da Tributo Justo, com autenticação, gestão de usuários e visualização de relatórios fiscais.

---

# ✅ Pré-requisitos

- [Node.js](https://nodejs.org/) instalado
- [VSCode](https://code.visualstudio.com/) instalado
- [.NET SDK 8.0](https://dotnet.microsoft.com/en-us/download) instalado
- SQLite baixado e extraído na pasta `backend` do projeto para utilização do banco de dados

---

# 🚀 Instruções de Execução

- Abra o terminal do VSCode
- Execute o comando 'cd backend'
- E em seguida execute 'dotnet run' para rodar o backend, automaticamente instalará os pacotes
- Depois abra um novo terminal e execute o comando 'cd frontend'
- E em seguida execute 'npm install', isso instalará todos os pacotes utilizados no frontend
- Para rodar o front, execute o comando 'npm start', deverá abrir no navegador

# 🧪 Como testar

- Efetue o login com seu usuário e senha.
- Caso não possua um, podera clicar no botão da tela de login para abrir o formulário de cadastro, insira seu usuário e senha.
- Após efetuar login, será redirecionado para a home, onde há algumas informações sobre as tecnologias usadas.
- Acima da página há um menu onde poderá navegar para as páginas de Home, Upload, Relatório, Estatísticas e Alertas e no canto direito há um botão para executar logoff.
- Na página Upload poderá adicionar um arquivo .csv para salvar as informações no banco de dados.
- Na página Relatório permite visualizar os dados das notas salvas a partir do arquivo .csv, tais informações como:
CNPJ e razão social das empresas, números das notas e valores monetários relacionados a mesma.
- Na página Estatísticas haverá cards com informações gerais dos dados das notas que estão no banco, como somatória de empresas, notas totais, itens totais, valores totais e total de imposto recolhido.
- Na página Alertas, aparecerão os dados semelhante aos encontrados na página Relatório com o diferencial que são notas onde a diferença (valor nota x valor imposto recolhido) ultrapasse os 50% de valor da nota.
- No botão Logoff, você será desconectado do sistema e redirecionado para a página de login, tendo que efetua-lo novamente caso queira visualizar as páginas.


# 🧠 Decisões técnicas

🔹 Organização do Projeto
O projeto foi dividido em duas partes: frontend (React) e backend (.NET), mantendo uma separação clara entre as responsabilidades da interface do usuário e da lógica de negócio/API.

O backend segue uma estrutura limpa, com as camadas:
Controllers: ponto de entrada das requisições.
Business: camada intermediária com regras de negócio.
Repositories: acesso direto ao banco de dados.
DTOs: transferência segura de dados entre frontend e backend.
Models: representam as entidades do sistema.

🔹 Banco de Dados
Foi escolhido o SQLite por ser leve, simples de configurar e suficiente para os objetivos do projeto (aplicação local e pequena escala).

🔹 Autenticação
Implementação de autenticação via JWT (JSON Web Token), garantindo segurança e controle de acesso às rotas protegidas do sistema.
A senha é criptografada com BCrypt antes de ser salva, aumentando a segurança dos dados do usuário.

🔹 Upload e Leitura de Arquivo CSV
Utilização da biblioteca CsvHelper para parsing do arquivo .csv, convertendo os dados para objetos C# .

🔹 Front-end
Criado com React + TypeScript,por ser uma stack com a qual já possuo familiaridade.
Axios foi utilizado para facilitar a comunicação com a API.
O JWT também é armazenado localmente no browser e incluído nas requisições para rotas autenticadas.

🔹 Validação
As validações de entrada do usuário foram feitas tanto no front-end (React) quanto no back-end (.NET com DataAnnotations), garantindo integridade e experiência do usuário.

🔹 Estilo e Layout
O layout foi desenvolvido com CSS puro, com foco em responsividade simples e clareza visual.
Cards, tabelas e filtros foram utilizados para tornar a visualização de dados mais amigável e organizada.