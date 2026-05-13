# 📦 Software Version Manager

API REST para gerenciamento de softwares e suas versões, desenvolvida utilizando **.NET 10**, **Entity Framework Core** e **SQL Server**, com suporte a execução via **Docker**.

---

## 🏗️ Arquitetura

O projeto segue uma arquitetura em camadas inspirada em **Clean Architecture**, separando responsabilidades em projetos distintos:

```
software-version-manager/
├── SoftwareVersionManager            # Camada de apresentação (API, Controllers)
├── SoftwareVersionManager.Domain     # Regras de negócio (Services, Interfaces)
├── SoftwareVersionManager.Data       # Acesso a dados (Repositories, DbContext, Migrations)
└── SoftwareVersionManager.Entities   # Modelos de domínio (Entidades)
```

---

## 🚀 Como executar

### Pré-requisitos
- [Docker](https://www.docker.com/) e Docker Compose instalados

### Subindo o projeto

```bash
# Clone o repositório
git clone https://github.com/seu-usuario/software-version-manager.git
cd software-version-manager

# Suba os containers (API + SQL Server)
docker-compose up --build
```

A API estará disponível em `http://localhost:8080`.  
A documentação Swagger estará em `http://localhost:8080/swagger`.

> **Nota:** As migrations são aplicadas automaticamente na inicialização da API. Não é necessário rodar nenhum comando adicional para criar o banco de dados.

---

## 📋 Endpoints

### Software

| Método | Rota | Descrição |
|--------|------|-----------|
| `GET` | `/api/Software` | Lista todos os softwares |
| `GET` | `/api/Software/{id}` | Busca um software pelo ID |
| `POST` | `/api/Software` | Cadastra um novo software |
| `PUT` | `/api/Software` | Atualiza um software existente |
| `DELETE` | `/api/Software/{id}` | Remove um software |

#### Exemplo de payload (POST/PUT)
```json
{
  "name": "VS Code",
  "description": "Editor de código da Microsoft",
  "author": "Microsoft"
}
```

---

### SoftwareVersion

| Método | Rota | Descrição |
|--------|------|-----------|
| `GET` | `/api/SoftwareVersion` | Lista todas as versões |
| `GET` | `/api/SoftwareVersion/{id}` | Busca uma versão pelo ID |
| `POST` | `/api/SoftwareVersion` | Cadastra uma nova versão |
| `PUT` | `/api/SoftwareVersion` | Atualiza uma versão existente |
| `DELETE` | `/api/SoftwareVersion/{id}` | Remove uma versão |

#### Exemplo de payload (POST/PUT)
```json
{
  "versionNumber": "1.85.0",
  "releaseDate": "2024-01-15T00:00:00",
  "softwareId": 1,
  "isDeprecated": false,
  "description": "Versão estável com suporte a extensões de IA"
}
```

---

## 🗄️ Banco de Dados

- **SGBD:** SQL Server 2022 (via Docker)
- **ORM:** Entity Framework Core 10 (Code First)
- **Conexão padrão:** `Server=sqlserver,1433;Database=SoftwareVersionManagerDb;User Id=sa;Password=Admin123!`

---

## 🛠️ Tecnologias

| Tecnologia | Versão |
|------------|--------|
| .NET | 10 |
| ASP.NET Core | 10 |
| Entity Framework Core | 10 |
| SQL Server | 2022 |
| Swashbuckle (Swagger) | 10 |
| Docker / Docker Compose | - |
