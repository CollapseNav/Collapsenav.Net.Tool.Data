# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Test Commands

```bash
# Build the entire solution (multi-targeted: net8.0, net6.0, netstandard2.1, netstandard2.0)
dotnet build Collapsenav.Net.Tool.Data.sln

# Run all tests
dotnet test Collapsenav.Net.Tool.Data.sln

# Run a specific test file's tests
dotnet test Collapsenav.Net.Tool.Data.sln --filter "FullyQualifiedName~CrudRepositoryTest"

# Run a single test method
dotnet test Collapsenav.Net.Tool.Data.sln --filter "FullyQualifiedName~CrudRepositoryTest.CrudRepositoryQueryTest"
```

Tests use xUnit with a custom `TestCaseOrderer` (`TestOrders`). Tests run against SQLite databases (`Test.db`). The test project targets `net8.0` and `net6.0`.

## Solution Structure

| Project | Purpose |
|---|---|
| `Collapsenav.Net.Tool.Data.Core` | Interfaces, entities, repository abstractions, and base implementations. No EF Core dependency beyond `IDB`. |
| `Collapsenav.Net.Tool.Data` | EF Core implementation (`EFDB<Context>`), DI registration extensions, auto-transaction support, read/write context separation. Depends on `Data.Core` and `Collapsenav.Module`. |
| `Collapsenav.Net.Tool.Data.Mssql` | SQL Server `Conn` subclass and DI extensions (`AddSqlServer<T>`). |
| `Collapsenav.Net.Tool.Data.Mysql` | MySQL and MariaDB `Conn` subclasses and DI extensions (`AddMysql<T>`, `AddMariaDb<T>`). |
| `Collapsenav.Net.Tool.Data.Pgsql` | PostgreSQL `Conn` subclass and DI extensions (`AddPgSql<T>`). Overrides `BaseEntity.GetNow` to use `DateTime.UtcNow`. |
| `Test/` | xUnit test project using SQLite. |
| `Demo/` | Demo apps (`AspnetDbDemo`, `NewTest`) and shared entity library (`EntityLib`). |

All library projects import `version.props` for shared build configuration (version `1.3.1`, LGPL-3.0-or-later license, `LangVersion=preview`).

## Architecture

### Entity Hierarchy

`IEntity` defines the core contract: `Init()`, `Update()`, `SoftDelete()`, `KeyType()`, `KeyProp()`, `SetKeyValue()`.

`IBaseEntity` extends `IEntity` with audit fields: `IsDeleted`, `CreationTime`, `LastModificationTime`. `IBaseEntity<TKey>` adds `CreatorId` and `LastModifierId`.

Concrete base classes use a 3-tier inheritance:
- `Entity` / `Entity<TKey>` / `AutoIncrementEntity<TKey>` — key + lifecycle methods, no audit fields
- `BaseEntity` / `BaseEntity<TKey>` / `AutoIncrementBaseEntity<TKey>` — adds soft delete and timestamps, `GetNow` static func (overridable per DB, e.g. PGSQL sets to UTC)

Entities with `DatabaseGeneratedOption.None` (user-assigned keys) use a static `GetKey` func to auto-generate IDs. `AddDefaultIdGenerator()` wires SnowFlake IDs for `long`/`long?` and `Guid.NewGuid()` for `Guid`/`Guid?`.

### IDB Abstraction

`IDB` is the central ORM abstraction — a single interface that wraps EF Core's `DbContext` operations (CRUD, transaction, query). This allows repository implementations to be ORM-agnostic. The concrete implementation is `EFDB<Context>` which takes a `DbContext` and delegates to EF Core + `Z.EntityFramework.Plus.EFCore` for bulk operations (`BulkUpdate`, `DeleteFromQuery`, `UpdateFromQuery`).

### Repository Pattern

**Two parallel interface/implementation trees:**

1. **NoConstraints** (no generic constraint beyond `class`): `INoConstraintsRepository<T>` → `INoConstraintsQueryRepository<T>` / `INoConstraintsModifyRepository<T>` → `INoConstraintsCrudRepository<T>`

2. **Constrained** (requires `T : class, IEntity`): `IRepository<T>` → `IQueryRepository<T>` / `IModifyRepository<T>` → `ICrudRepository<T>`. Each inherits from both its NoConstraints counterpart AND `IRepository<T>`.

**Implementation layering:**
- `NoConstraintsXxxRepository` classes implement the actual EF Core logic via `_db` (the `IDB` instance)
- The constrained `ModifyRepository<T>` overrides add/update/delete to call entity lifecycle hooks (`entity.Init()`, `entity.Update()`, `entity.SoftDelete()`)
- `CrudRepository<T>` uses **read/write separation via composition**: takes separate `IQueryRepository<T>` and `IModifyRepository<T>` instances and delegates queries to read + mutations to write

**DI Registration:** `services.AddRepository()` registers all 8 open-generic repository types as scoped services, plus default ID generators.

### Read/Write Context Separation

`ReadContext` and `WriteContext` are marker subclasses of `DbContext`. `DataInitModule` auto-discovers concrete DbContext subclasses and registers them differently: `WriteContext` subclasses via `AddWriteContext<T>`, `ReadContext` subclasses via `AddReadContext<T>`, and plain `DbContext` subclasses via standard `AddDbContext<T>`. This makes `ReadContext` and `WriteContext` injectable directly.

### Auto-Transaction

`TransManager` tracks `IDB` instances with a reference count. When `AutoCommit` is enabled (via `UseAutoCommit()` or `app.UseAutoCommit()` middleware), each repository registration increments the count, and disposal decrements it. When the count reaches zero (all repositories for that context are disposed), `SaveChanges()` is called automatically. An ASP.NET Core exception handler middleware (`AutoTransactionErrorHandler`) sets `HasError` to prevent commit on errors.

### Database Connection Auto-Discovery

`DataInitModule.Init()` reads the `Connection` section from `appsettings.json` (`ConnectionString` and `DbType`), finds the matching `Conn` subclass (by name prefix match against the `DbType` enum), and auto-registers all discovered `DbContext` subclasses with the resolved connection string.

### Join Support

`GroupJoinResult<T1..Tn>` provides a fluent, type-safe chained Join/LeftJoin API supporting up to 9 table joins. Started via `CreateJoin<T>()` or `StartJoin<T>()` extension methods on `INoConstraintsRepository<T>`.

## Key External Dependencies

- **Collapsenav.Net.Tool** (via `Collapsenav.Net.Tool.Base`) — utility extensions and helpers
- **Collapsenav.Module** — `InitModule` base class for auto-configuration
- **Z.EntityFramework.Plus.EFCore** — bulk update/delete operations (version matched to EF Core version per TFM)
- **EF Core** — version floats per TFM (3.x for netstandard2.0, 5.x for netstandard2.1, 6.x for net6.0, 8.x for net8.0)
