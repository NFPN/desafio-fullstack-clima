# Aplicativo de Previsão do Tempo

Bem-vindo ao Aplicativo de Previsão do Tempo! Este aplicativo full-stack permite que você verifique previsões do tempo por cidade, gerencie locais favoritos e visualize previsões para 5 dias. Ele é desenvolvido com Angular no front-end e .NET 8 no back-end.

## Pré-requisitos

Antes de começar, certifique-se de ter os seguintes itens instalados:
- **Node.js** (v14 ou superior) - [Download](https://nodejs.org/)
- **Angular CLI** (v12 ou superior) - Instale com `npm install -g @angular/cli`
- **.NET 8 SDK** - [Download](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server** (LocalDB ou outra instância) - Disponível com o Visual Studio ou instalado separadamente

## Instruções de Configuração

Siga estas etapas para configurar e executar o projeto localmente.

### Back-end (.NET 8)
1. Abra um terminal e navegue até a pasta `backend`:
   ```bash
   cd backend
   ```
2. Atualize o arquivo `appsettings.json` com sua string de conexão do SQL Server. Exemplo para LocalDB:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=WeatherDb;Trusted_Connection=True;"
   }
   ```
3. Instale as dependências do .NET:
   ```bash
   dotnet restore
   ```
4. Configure o banco de dados (assumindo o uso do Entity Framework Core):
   ```bash
   dotnet ef database update
   ```
5. Inicie a API do back-end:
   ```bash
   dotnet run
   ```
   A API rodará em `http://localhost:5000` (verifique `launchSettings.json` caso a porta seja diferente).

### Front-end (Angular)
1. Abra um novo terminal e navegue até a pasta `frontend`:
   ```bash
   cd frontend
   ```
2. Instale as dependências do Angular:
   ```bash
   npm install
   ```
3. Inicie o servidor de desenvolvimento do front-end:
   ```bash
   ng serve
   ```
   O aplicativo estará disponível em `http://localhost:4200`.

## Executando e Testando o Aplicativo

1. Certifique-se de que a API do back-end está rodando (`http://localhost:5000`).
2. Abra seu navegador e acesse `http://localhost:4200`.
3. Teste o aplicativo:
   - Pesquise a previsão do tempo de uma cidade.
   - Adicione cidades aos seus favoritos.
   - Visualize a previsão para os próximos 5 dias.

## Funcionalidades do Projeto
- **Busca por Clima**: Insira o nome de uma cidade para obter dados climáticos atuais.
- **Favoritos**: Salve e gerencie suas cidades favoritas.
- **Previsão de 5 Dias**: Veja previsões do tempo para os próximos cinco dias.

## Solução de Problemas
- **A API não está conectando?** Verifique se o back-end está rodando e se a porta corresponde às chamadas da API do front-end.
- **Erros no banco de dados?** Confira sua string de conexão e certifique-se de que `dotnet ef database update` foi executado com sucesso.

