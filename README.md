
---

# 🚀 CareerLens API — .NET 8 (FIAP Advanced Business Development)

API RESTful construída em **.NET 8**, seguindo arquitetura em camadas, boas práticas REST, versionamento, EF Core com Oracle, observabilidade, HATEOAS e testes automatizados com xUnit.  
Desenvolvida como parte da disciplina **Advanced Business Development with .NET — FIAP**.

---

# 📌 Sumário
1. [Arquitetura](#arquitetura)
2. [Tecnologias](#tecnologias)
3. [Módulos Implementados](#módulos-implementados)
4. [Versionamento da API](#versionamento-da-api)
5. [Boas Práticas REST](#boas-práticas-rest)
6. [Banco Oracle](#banco-oracle)
7. [Migrations](#migrations)
8. [Observabilidade](#observabilidade)
9. [Como Executar](#como-executar)
10. [Testes Automatizados](#testes-automatizados)
11. [Status Final](#status-final)

---

# 🏗️ Arquitetura

A solução segue o padrão **Clean Architecture**, garantindo separação de responsabilidade e alta testabilidade:

```

CareerLens/
┣ Domain/
┃ ┣ Entities
┃ ┗ Interfaces
┣ Application/
┃ ┣ Dtos
┃ ┣ Mapper
┃ ┣ Interfaces
┃ ┗ UseCases
┣ Infrastructure/
┃ ┣ Data (DbContext)
┃ ┗ Repositories
┗ Presentation/
┗ Controllers (v1 e v2)

```

**Benefícios:**
- Baixo acoplamento  
- Fácil manutenção  
- Pronto para escalar e versionar  

---

# 🛠 Tecnologias

| Tecnologia | Uso |
|-----------|-----|
| **.NET 8 Web API** | Core da aplicação |
| **Entity Framework Core 8** | ORM |
| **Oracle Managed Provider** | Banco de Dados |
| **Swagger** | Documentação |
| **API Versioning** | v1 e v2 |
| **OpenTelemetry** | Logging + Tracing |
| **HealthChecks** | Observabilidade |
| **xUnit + Moq** | Testes automatizados |

---

# 🎯 Módulos Implementados

Todos os módulos estão completos:

- **Users**
- **Resumes**
- **Skills**
- **LearningResources**
- **ResumeSkills**
- **JobAnalyses**
- **AnalysisResults**

Cada módulo possui:

✔ DTOs separados  
✔ Mappers atualizados  
✔ UseCases com OperationResult  
✔ Repositórios Oracle com EF  
✔ Controllers REST + HATEOAS  
✔ Paginação correta  
✔ Versionamento completo  

---

# 🔀 Versionamento da API

A API usa versionamento via URL:

### ▶ Versão 1 (completa)
```

/api/v1/users
/api/v1/resumes
/api/v1/skills
/api/v1/job-analyses
...

```

### ▶ Versão 2 (em evolução)
```

/api/v2/users

````

Configuração de v2:

```csharp
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/users")]
[ApiExplorerSettings(GroupName = "v2")]
````

Swagger exibe grupos **v1** e **v2** separadamente.

---

# 📚 Boas Práticas REST

✔ Paginação padrão em todos os GET
✔ HATEOAS nos recursos
✔ Status Codes corretos
✔ Verbos respeitados
✔ Responses padronizados com OperationResult

Exemplo:

```json
{
  "data": [...],
  "pagination": {
    "page": 1,
    "pageSize": 10,
    "totalItems": 42
  },
  "links": {
    "self": "/api/v1/users?page=1&pageSize=10"
  }
}
```

---

# 🗄 Banco Oracle

Conexão:

```json
"ConnectionStrings": {
  "Oracle": "User Id=RMxxxxx;Password=xxxx;Data Source=oracle.fiap.com.br:1521/ORCL"
}
```

Repositórios usam EF Core + Oracle Managed Provider.

---

# 🧱 Migrations

Gerar:

```
dotnet ef migrations add InitialMigration --project Infrastructure
```

Aplicar:

```
dotnet ef database update --project Infrastructure
```

---

# 📈 Observabilidade

A API implementa **100% dos requisitos**:

### 🟢 HealthCheck

Endpoints:

```
/health
/health/db
```

### 🟠 Logging

Logs automáticos ASP.NET + EF Core
Tracking completo das queries Oracle

### 🔵 Tracing (OpenTelemetry)

Instrumentações:

* AspNetCore
* HttpClient
* EF Core
* Exporter Console

Exemplo real:

```
TraceId: 4f5a1d178d9b2c18
Span: GET /api/v1/users
Status: Success (200)
```

---

# ▶ Como Executar

### 1️⃣ Restaurar pacotes

```
dotnet restore
```

### 2️⃣ Subir API

```
dotnet run
```

### 3️⃣ Acessar Swagger

```
https://localhost:{porta}/swagger
```

---

# 🧪 Testes Automatizados

A solução inclui o projeto:

```
CareerLens.Test/
```

### ✔ Testes unitários com Moq

Cobrem cenários negativos para garantir estabilidade dos UseCases.

### ✔ 8 testes implementados (todos passando)

| Módulo               | Testado |
| -------------------- | ------- |
| Users                | ✔       |
| Resumes              | ✔       |
| Skills               | ✔       |
| AnalysisResults      | ✔       |
| JobAnalyses          | ✔       |
| ResumeSkills         | ✔       |
| LearningResources    | ✔       |
| OperationResult flow | ✔       |

### Executar testes:

```
dotnet test
```

Saída real:

```
Total: 8
Succeeded: 8
Failed: 0
```

---

# 🏁 Status Final

| Requisito FIAP         | Status       |
| ---------------------- | ------------ |
| REST + HATEOAS         | ✔ 100%       |
| Paginação              | ✔            |
| Versionamento v1/v2    | ✔            |
| Oracle + EF Core       | ✔            |
| Migrations             | ✔            |
| HealthCheck            | ✔            |
| Logging & Tracing      | ✔            |
| Testes Automatizados   | ✔ (8 testes) |
| Documentação Swagger   | ✔            |
| Código limpo e modular | ✔            |

---


