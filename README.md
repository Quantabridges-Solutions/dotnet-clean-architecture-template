# .NET Clean Architecture Template

[![CI](https://github.com/Quantabridges-Solutions/dotnet-clean-architecture-template/actions/workflows/ci.yml/badge.svg)](https://github.com/Quantabridges-Solutions/dotnet-clean-architecture-template/actions/workflows/ci.yml)  
*If your repo is under a different org or name, update the badge URLs above.*

A **production-ready** .NET 10 template with Clean Architecture, CQRS, MediatR, PostgreSQL, Redis, Docker, and CI/CD. Built by [Qbs-Tech](https://qbs-tech.com) to showcase our engineering standards and to give teams a solid starting point for new APIs.

Use this repo as a **reference implementation**, **learning resource**, or **fork it** to kickstart your next .NET API.

---

## Why this template?

- **Clean Architecture** — Domain at the centre; infrastructure and UI are pluggable.
- **CQRS + MediatR** — Clear separation of reads and writes; easy to test and extend.
- **Production-minded** — Health checks, structured logging, global error handling, validation, OpenAPI docs.
- **Testable** — Unit tests with mocks; integration tests with **Testcontainers** (real Postgres/Redis).
- **Ready to ship** — Docker, docker-compose, and GitHub Actions CI included.

---

## Tech stack

| Layer | Technology |
|-------|------------|
| Runtime | .NET 10 |
| API | ASP.NET Core Web API |
| CQRS / Mediator | MediatR |
| Validation | FluentValidation |
| Data | Entity Framework Core, PostgreSQL (code-first) |
| Cache | Redis (StackExchange) |
| API docs | Scalar (OpenAPI) |
| Tests | xUnit, FluentAssertions, Moq, Testcontainers |
| DevOps | Docker, GitHub Actions |

---

## Solution structure

```
src/
├── Domain/           # Entities, interfaces — no dependencies
├── Application/      # Commands, queries, handlers, validators, behaviours
├── Infrastructure/   # EF Core, PostgreSQL, Redis, repositories
└── Api/              # Controllers, filters, middleware, entry point
tests/
└── Tests.csproj      # Unit + integration tests (Testcontainers)
```

See [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md) for a short architecture overview.

---

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Docker (for PostgreSQL, Redis, or full stack)

---

## Quick start

**1. Clone and start dependencies**

```bash
git clone https://github.com/Quantabridges-Solutions/dotnet-clean-architecture-template.git
cd dotnet-clean-architecture-template
docker compose up -d postgres redis
```

**2. Apply migrations (code-first)**

```bash
dotnet ef database update --project src/Infrastructure --startup-project src/Api
```

**3. Run the API**

```bash
dotnet run --project src/Api
```

- **API:** http://localhost:5000  
- **Health:** http://localhost:5000/health  
- **API docs (Scalar):** http://localhost:5000/scalar  

**4. Run tests**

```bash
dotnet test tests/Tests.csproj
```

---

## Run with Docker (full stack)

```bash
docker compose up -d postgres redis
dotnet ef database update --project src/Infrastructure --startup-project src/Api
docker compose up --build api
```

---

## After you fork

1. **Replace connection strings** — Use `appsettings.Development.json` or environment variables; never commit secrets.
2. **Create your first migration** — `dotnet ef migrations add Initial --project src/Infrastructure --startup-project src/Api`
3. **Rename solution/namespaces** — Search for `CleanArchitecture` and your repo name and update to your product name.
4. **Add your domain** — Add entities in `Domain`, commands/queries in `Application`, and repositories in `Infrastructure`.
5. **Enable branch protection** — Require CI to pass on `main` (and optionally use the provided CI workflow).

---

## Commands reference

| Task | Command |
|------|--------|
| Build | `dotnet build CleanArchitecture.slnx -c Release` |
| Test | `dotnet test tests/Tests.csproj` |
| Add migration | `dotnet ef migrations add <Name> --project src/Infrastructure --startup-project src/Api` |
| Update DB | `dotnet ef database update --project src/Infrastructure --startup-project src/Api` |

---

## Features in detail

- **.NET 10** — Current LTS-ready stack.
- **CQRS** — Commands and queries with MediatR; handlers are thin and testable.
- **FluentValidation** — Request validation via pipeline behaviour; consistent error responses.
- **PostgreSQL + EF Core** — Code-first migrations; repository abstraction in Domain.
- **Redis** — Caching abstraction (`ICacheService`); example usage in `GetItemQueryHandler`.
- **Health checks** — `/health` reports API, PostgreSQL, and Redis status.
- **Structured logging** — Serilog with request logging and configurable sinks.
- **Global error handling** — Unhandled exceptions return a consistent problem-details response.
- **Scalar** — Modern OpenAPI documentation at `/scalar`.
- **CI** — GitHub Actions: restore, build, test on push/PR to `main` and `develop`.

---

## Contributing

We welcome issues and pull requests. See [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines. For security concerns, see [SECURITY.md](SECURITY.md).

---

## License

MIT — see [LICENSE](LICENSE). Built by [Qbs-Tech](https://qbs-tech.com).
