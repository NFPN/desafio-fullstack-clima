# Aplicativo de Previsão do Tempo

Bem-vindo ao Aplicativo de Previsão do Tempo! Este aplicativo full-stack permite que você verifique previsões do tempo por cidade, gerencie locais favoritos e visualize previsões para 5 dias. Ele é desenvolvido com Angular no front-end e .NET 8 no back-end.

## Pré-requisitos

Antes de começar, certifique-se de ter os seguintes itens instalados:
- **Node.js** (v14 ou superior) - [Download](https://nodejs.org/)
- **Angular CLI** (v12 ou superior) - Instale com `npm install -g @angular/cli`
- **.NET 8 SDK**: [Baixe aqui](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server**: LocalDB (incluído no Visual Studio) ou uma instância completa do SQL Server.
- **Chave da API do OpenWeatherMap**: Registre-se em [OpenWeatherMap](https://openweathermap.org/) para obter uma chave gratuita.
- **Git**: Para clonar o repositório.
- **Visual Studio 2022** (opcional, para suporte a IDE) ou qualquer editor de código (ex.: VS Code).

## Instruções de Configuração

Siga estas etapas para configurar e executar o projeto localmente.

### Back-end (.NET 8)
#### ProjetoClima.API

Este é o backend de um aplicativo de previsão do tempo construído com .NET 8 usando Minimal APIs. Ele fornece autenticação de usuários via JWT, recupera dados climáticos do OpenWeatherMap e gerencia cidades favoritas armazenadas em um banco de dados SQL Server utilizando Entity Framework Core. O projeto é projetado para ser modular, seguro e escalável, com serviços que tratam a lógica de negócios e endpoints focados no manuseio de requisições e respostas HTTP.

## Funcionalidades

- **Autenticação**: Registro (`/auth/registro`) e login (`/auth/login`) de usuários baseados em JWT.
- **Dados Climáticos**: Endpoints públicos para clima atual (`/clima/{cidade}`) e previsão de 5 dias (`/previsao/{cidade}`) do OpenWeatherMap.
- **Cidades Favoritas**: Endpoints protegidos para gerenciar cidades favoritas do usuário (`/favoritos`) usando autenticação JWT.
- **Banco de Dados**: SQL Server com migrações do EF Core para gerenciamento do esquema.
- **Desempenho**: Cache de dados climáticos com `IMemoryCache` (TTL de 10 minutos). O cache é automaticamente invalidado após esse período, garantindo que os dados sejam atualizados regularmente. Caso seja necessário um refresh manual, o cache pode ser limpo diretamente no serviço responsável pelo armazenamento.
- **Segurança**: Tokens JWT, CORS e reforço do uso de HTTPS.

---

## Instruções de Configuração

Siga os passos abaixo para clonar e configurar o backend:

### 1. Clonar o Repositório

```bash
git clone https://github.com/seuusuario/ProjetoClima.API.git
cd ProjetoClima.API
```

### 2. Restaurar Dependências

Restaurar os pacotes .NET necessários para o projeto:

```bash
dotnet restore
```

### 3. Configurar Variáveis de Ambiente

A aplicação utiliza variáveis de ambiente para dados sensíveis. Crie um arquivo `.env` (opcional) ou defina essas variáveis no ambiente:

- **Chave da API do OpenWeatherMap**: Necessária para obter os dados climáticos.

  ```bash
  setx OPENWEATHERMAP_API_KEY="sua-chave-da-api"
  ```

  Ou edite `appsettings.json` (não recomendado para produção):

  ```json
  "OpenWeatherMap": {
    "ApiKey": "sua-chave-da-api"
  }
  ```

- **Chave JWT**: Uma chave secreta para assinatura de tokens JWT (pelo menos 16 caracteres).

  ```bash
  setx JWT_KEY="sua-chave-secreta-com-pelo-menos-16-caracteres"
  ```

  Ou edite `appsettings.json`:

  ```json
  "Jwt": {
    "Key": "sua-chave-secreta-com-pelo-menos-16-caracteres",
    "Issuer": "ProjetoClima.API",
    "Audience": "ProjetoClima.Frontend"
  }
  ```

- **Conexão com o Banco de Dados** (opcional): Padrão para LocalDB. Para usar outra instância do SQL Server, defina:

  ```bash
  setx CONNECTIONSTRINGS__DEFAULTCONNECTION="Server=seu-servidor;Database=ProjetoClimaDb;User Id=seu-usuario;Password=sua-senha;"
  ```
  
  Ou edite `appsettings.json`:

  ```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=seu-servidor;Database=ProjetoClimaDb;User Id=seu-usuario;Password=sua-senha;"
  }
  ```

### 4. Aplicar Migrações do Banco de Dados

O projeto usa migrações do EF Core para criar o esquema do banco de dados. Execute:

```bash
dotnet ef database update
```

### 5. Compilar o Projeto

Compile o projeto para verificar se tudo está configurado corretamente:

```bash
dotnet build
```

---

## Executando a Aplicação

### Executar Localmente

Inicie o servidor backend:

```bash
dotnet run --project ProjetoClima.API
```

- A aplicação iniciará em `https://localhost:7018` (ou outra porta definida em `launchSettings.json`).
- Utilize Postman ou curl para testar os endpoints.

---

## Testando os Endpoints

### 1. Registrar um Usuário

- **Endpoint**: `POST /auth/registro`
- **Corpo** (JSON):
  ```json
  {
    "email": "teste@exemplo.com",
    "password": "SenhaForte123!"
  }
  ```

### 2. Fazer Login

- **Endpoint**: `POST /auth/login`
- **Corpo** (JSON):
  ```json
  {
    "email": "teste@exemplo.com",
    "password": "SenhaForte123!"
  }
  ```
- **Resposta**: `200 OK` com um token JWT.
- Utilize o token gerado no login para se autenticar no Swagger. No Swagger UI, clique em "Authorize", insira o token no formato `Bearer <seu-token>` e, em seguida, teste os outros endpoints protegidos. Exemplo de token JWT:
  ````json
  {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
  }
  ``` No Swagger UI, clique em "Authorize", insira o token no formato `<seu-token>` e, em seguida, teste os outros endpoints protegidos.
  ````

---

## Executando Testes Unitários

O projeto inclui testes unitários para `ClimaService` e `FavoritoService` usando xUnit.

### 1. Configurar o Projeto de Testes

```bash
cd ProjetoClima.API.Testes
dotnet restore
```

### 2. Executar os Testes

```bash
dotnet test
```

---
