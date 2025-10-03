🎮 Desafio TicTacToe

Imagens das telas em funcionamento estão na pasta (telas)

Projeto completo Front-end + Back-end, desenvolvido com React.js, .NET e PostgreSQL (via Entity Framework Core).
O projeto foi estruturado seguindo os princípios de S.O.L.I.D e Clean Code, garantindo boa organização e manutenibilidade.

🚀 Tecnologias Utilizadas

Front-end: React.js

Back-end: ASP.NET Core (.NET)

Banco de Dados: PostgreSQL + Entity Framework Core

Testes Unitários: xUnit + EF InMemory

Documentação da API: Swagger

📂 Estrutura do Projeto

Models → Representam as tabelas do banco de dados.

ViewModels → Objetos usados para entrada e saída de dados (request/response).

Helpers → Realizam a conversão entre Model ↔ ViewModel, mantendo o código limpo.

Enum → Responsável por tratar status dos jogadores em formato enumerado.

Data → Contém o AppDbContext, responsável por mapear as entidades no banco.

Controllers → Endpoints documentados com exemplos de entrada/saída e explicações.

Desafio_TicTacToe_Teste → Projeto de testes unitários utilizando xUnit.

⚙️ Configuração do Banco de Dados

Antes de rodar o projeto, configure sua conexão no arquivo:

// appsettings.json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=SeuBanco;Username=seuUsuario;Password=suaSenha"
}

📖 Funcionalidades

✅ Dois jogadores jogam alternadamente
✅ Registro de histórico de partidas (nomes, vencedor/empate, data e hora)
✅ Persistência do vencedor via API
✅ Tela com gráfico de vitórias por jogador (Torres)
✅ Entrada de nomes personalizados para os jogadores
✅ Destaque visual para o jogador atual durante a partida
✅ Tela de ranking e listagem dos últimos vencedores
✅ Registro de logs das jogadas realizadas na partida
✅ Exibição das estratégias mais vencedoras

🧪 Testes

Os testes unitários foram implementados utilizando xUnit, com banco InMemory para simulação de dados:

Teste de gravação de partidas

Teste de histórico de partidas

Teste de vencedores recentes

Teste de ranking de vencedores

Para rodar os testes:

dotnet test ou clique no menu Teste-> Executar todos os testes.

Para rodar o projeto:

clique no icone de debug.
