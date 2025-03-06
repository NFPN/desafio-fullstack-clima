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
  ```json
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
  ```
  No Swagger UI, clique em "Authorize", insira o token no formato `<seu-token>` e, em seguida, teste os outros endpoints protegidos.

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
## Front-end (Angular)
### Projeto Clima App

Uma aplicação web desenvolvida em Angular para consultar o clima de cidades, gerenciar uma lista de cidades favoritas e navegar entre telas de busca e favoritos. Este projeto utiliza uma API backend para autenticação e gerenciamento de favoritos, com armazenamento local no navegador para persistência de dados entre sessões.

## Funcionalidades
- **Autenticação:** Login e cadastro de usuários com JWT.
- **Busca de Clima:** Consulta de clima atual e previsão para cidades específicas.
- **Favoritos:** Adição, remoção e visualização de cidades favoritas, sincronizadas com o backend e persistidas localmente no `localStorage`.
- **Navegação:** Sidebar responsiva para alternar entre busca e favoritos, visível apenas após login.
- **Design:** Interface moderna com Bootstrap e Bootstrap Icons, incluindo cards para exibir dados climáticos e uma sidebar estilizada.

## Requisitos

### Pré-requisitos
- **Node.js**: Versão 18.x ou superior (recomendado: 20.x).
- **npm**: Versão 9.x ou superior (vem com o Node.js).
- **Angular CLI**: Versão 17.x (`npm install -g @angular/cli`).
- **Backend**: Uma API rodando localmente ou remotamente (ex.: ASP.NET Core com endpoints `/auth/login`, `/auth/registro`, `/clima/{cidade}`, `/previsao/{cidade}`, `/favoritos`, `/favorito`).

### Dependências do Projeto
As dependências principais estão listadas no `package.json`. Aqui estão as mais relevantes:
- `@angular/core`: ^17.0.0
- `@angular/cdk`: ^17.0.0 (para `BreakpointObserver` na sidebar responsiva)
- `bootstrap`: ^5.3.0 (framework de estilo)
- `bootstrap-icons`: ^1.11.0 (ícones usados na sidebar e botões)
- `rxjs`: ^7.8.0 (para manipulação de Observables)

---

## Como Rodar o Projeto

### 1. Instalar Dependências
Instale as dependências listadas no `package.json`:
```bash
npm install
```

- **Nota:** Se encontrar erros relacionados a `@angular/cdk`, reinstale com:
  ```bash
  npm install @angular/cdk@^17.0.0
  ```

### 2. Configurar o Backend
O projeto depende de uma API backend para autenticação, dados climáticos e gerenciamento de favoritos. Certifique-se de que o backend esteja rodando e acessível.

- **URL da API:** Edite o arquivo `src/environments/environment.ts` para apontar para o endereço do seu backend:
  ```typescript
  export const environment = {
    production: false,
    apiUrl: 'http://localhost:5000' // Ajuste para o endereço do seu backend
  };
  ```
- **Endpoints Esperados:**
  - `POST /auth/login`: Autentica o usuário e retorna um token JWT.
  - `POST /auth/registro`: Registra um novo usuário.
  - `GET /clima/{cidade}`: Retorna o clima atual da cidade.
  - `GET /previsao/{cidade}`: Retorna a previsão do tempo da cidade.
  - `GET /favoritos`: Lista as cidades favoritas do usuário autenticado.
  - `POST /favorito`: Adiciona uma cidade aos favoritos.
  - `DELETE /favorito/{cidade}`: Remove uma cidade dos favoritos.

- **Exemplo de Backend:** Veja o código fornecido para o backend em C# (ASP.NET Core) para configurar os endpoints.

### 3. Rodar a Aplicação
Inicie o servidor de desenvolvimento do Angular:
```bash
ng serve
```
- Acesse a aplicação em `http://localhost:4200`.

- **Nota:** Use `ng serve --open` para abrir automaticamente no navegador.

---

## Estrutura do Projeto

```
weather-app/
├── src/
│   ├── app/
│   │   ├── nav/                    # Sidebar de navegação
│   │   ├── login/                  # Tela de login
│   │   ├── register/               # Tela de cadastro
│   │   ├── weather-search/         # Tela de busca de clima
│   │   ├── favorites/              # Tela de favoritos
│   │   ├── forecast/               # Tela de previsão detalhada
│   │   ├── services/               # Serviços (auth, favorites, weather)
│   │   ├── models/                 # Modelos TypeScript
│   │   ├── app.component.ts        # Componente raiz
│   │   ├── app.routes.ts           # Definição de rotas
│   │   └── auth.guard.ts           # Guarda de autenticação
│   ├── environments/               # Configurações de ambiente
│   ├── main.ts                     # Ponto de entrada
│   └── styles.css                  # Estilos globais
├── angular.json                    # Configuração do Angular
└── package.json                    # Dependências e scripts
```

---

## Testando a Aplicação

### Passos para Teste
1. **Iniciar o Backend:**
   - Certifique-se de que o backend está rodando (ex.: `dotnet run` se usar ASP.NET Core em `http://localhost:5000`).

2. **Acessar a Tela de Login:**
   - Abra `http://localhost:4200`.
   - Você será redirecionado para `/login` (rota padrão).

3. **Fazer Login:**
   - Use credenciais válidas (ex.: email: `teste@teste.com`, senha: `123456`).
   - Após login, você será levado para `/search`.

4. **Testar a Busca:**
   - Na tela de busca (`/search`), digite uma cidade (ex.: "São Paulo") e pressione "Enter" ou clique em "Buscar".
   - Verifique se o clima atual aparece em um card com temperatura à direita.

5. **Gerenciar Favoritos:**
   - Clique no ícone de coração para adicionar a cidade aos favoritos (coração preenchido em vermelho).
   - Clique novamente para remover (coração vazio).
   - Os favoritos são salvos no `localStorage` e sincronizados com o backend.

6. **Navegar para Favoritos:**
   - Use a sidebar à esquerda para clicar em "Favoritos" (`/favorites`).
   - Veja a lista de cidades favoritas com dados climáticos.

7. **Testar a Previsão:**
   - Clique em "Ver Previsão" em qualquer card para ir a `/forecast/{cidade}` e ver a previsão detalhada.

8. **Testar Logout:**
   - Na sidebar, clique em "Sair" (botão vermelho claro).
   - Verifique se você é redirecionado para `/login` e os favoritos são limpos do `localStorage`.

9. **Testar Múltiplos Usuários:**
   - Faça login com um usuário, adicione favoritos, faça logout.
   - Faça login com outro usuário e confirme que os favoritos do usuário anterior não aparecem.

---

## Notas Importantes

### Configuração do Backend
- O backend deve usar autenticação JWT e incluir o token no header `Authorization` para requisições protegidas (ex.: `/favoritos`).
- O `auth.interceptor.ts` adiciona o token automaticamente às requisições para `/favorito`.

### Persistência Local
- Os favoritos são salvos no `localStorage` com a chave `favorites`.
- Ao fazer login ou logout, o cache local é limpo e recarregado do backend para refletir o usuário atual.

### Sidebar Responsiva
- Em telas grandes (≥992px), a sidebar é visível por padrão e pode ser escondida/mostrada com botões.
- Em telas pequenas (<992px), ela começa escondida, com um botão toggler externo para abri-la.

---

## Resolução de Problemas

### Erros de Compilação
- **Dependências Ausentes:** Execute `npm install` novamente ou reinstale com `rm -rf node_modules package-lock.json && npm install`.
- **TypeScript Errors:** Verifique os tipos em `models.ts` e ajuste conforme a resposta do backend.

### Backend Não Responde
- Confirme que o backend está rodando e a URL em `environment.ts` está correta.
- Verifique os logs do servidor para erros (ex.: CORS, autenticação).

### Favoritos Não Atualizam
- Inspecione o `localStorage` (DevTools > Application) e as requisições de rede (DevTools > Network) para diagnosticar.

---
