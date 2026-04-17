# Task Manager API

API REST para gerenciamento de tarefas, desenvolvida com .NET 10 seguindo os princípios de Clean Architecture.

## 🏗️ Arquitetura

O projeto é dividido em 4 camadas:

- **Domain** — entidades e regras de negócio
- **Application** — casos de uso com CQRS e MediatR
- **Infrastructure** — acesso a dados com EF Core e SQL Server
- **API** — controllers, Swagger e configuração da aplicação

## 🚀 Tecnologias

- .NET 10
- ASP.NET Core
- Entity Framework Core
- MediatR (CQRS)
- SQL Server / LocalDB
- Swagger / OpenAPI
- xUnit
- Moq
- FluentAssertions

## ⚙️ Como rodar localmente

**Pré-requisitos:** .NET 10 SDK e SQL Server ou LocalDB instalados.

Clone o repositório:
```bash
git clone https://github.com/pedrinhopontes/task-manager-api.git
cd task-manager-api
```

Aplique as migrations:
```bash
dotnet ef database update --project TaskManager.Infrastructure --startup-project TaskManager.API
```

Rode a aplicação:
```bash
dotnet run --project TaskManager.API
```

Acesse o Swagger em `https://localhost:7267/swagger`

## 📋 Endpoints

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | /api/tasks | Lista todas as tarefas |
| POST | /api/tasks | Cria uma nova tarefa |
| PATCH | /api/tasks/{id}/complete | Marca uma tarefa como concluída |

## 🧪 Testes

```bash
dotnet test
```

4 testes unitários cobrindo os handlers de criação e conclusão de tarefas.