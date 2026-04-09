# Bruno

A vehicle booking management system built with ASP.NET Core and Blazor WebAssembly.

---

## Architecture

The solution follows **Clean Architecture** with a clear separation of concerns across four backend layers and a Blazor WASM frontend.

```
solid-bruno/
├── backend/
│   ├── Bruno.Domain/          # Entities, interfaces, value objects, exceptions
│   ├── Bruno.Application/     # CQRS handlers, DTOs, validators, domain events
│   ├── Bruno.Infrastructure/  # EF Core, repositories, data seeding
│   ├── Bruno.API/             # ASP.NET Core controllers, middleware, Swagger
│   ├── Bruno.Tests/           # xUnit unit tests
│   └── Bruno/                 # Blazor WASM frontend
```

### Backend Layers

**Domain (`Bruno.Domain`)**
Contains the core business model with no external dependencies: `Vehicle`, `Customer`, and `Booking` entities, repository interfaces (`IUnitOfWork`, `IVehicleRepository`, etc.), value objects (`DateRange`), domain exceptions, and enums (`BookingStatus`).

**Application (`Bruno.Application`)**
Implements CQRS via MediatR — every use case is a `Command` or `Query` with a dedicated handler. FluentValidation runs as a MediatR pipeline behaviour so all input is validated before reaching a handler. Domain events (e.g. `BookingCreatedEvent`, `VehicleDeletedEvent`) are published through MediatR after successful operations.

**Infrastructure (`Bruno.Infrastructure`)**
Houses the EF Core `BrunoContext`, concrete repository implementations, the `UnitOfWork`, and the `DataSeeder` that seeds development data on startup.

**API (`Bruno.API`)**
Thin ASP.NET Core layer: three controllers (`VehicleController`, `CustomerController`, `BookingController`) that dispatch commands/queries through MediatR. Custom exception handlers translate domain and validation exceptions to RFC 9110 Problem Details responses. API key authentication is enforced by `ApiKeyMiddleware` on every request. Swagger/OpenAPI is enabled in development.

### Frontend (`Bruno`)

Blazor WebAssembly app organised by feature (`Vehicle`, `Customer`, `Booking`). Each feature owns its pages, components, models, API client interface, and a lightweight state class used for in-memory caching and reactive UI updates. A shared `ApiClient` wrapper handles HTTP calls and propagates the `X-Api-Key` header.

---

## How to Run

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL 15+ running locally

### 1. Database setup

Create a PostgreSQL database named `brunodb` and ensure the connection string in `backend/Bruno.API/appsettings.Development.json` matches your local credentials.

Apply migrations:

```bash
cd backend
dotnet ef database update --project Bruno.API
```

> The database is seeded automatically with sample vehicles, customers, and bookings the first time the API starts in the `Development` environment.

### 2. Run the API

```bash
cd backend/Bruno.API
dotnet run
```

The API will be available at `https://localhost:7134`. Swagger UI is available at `https://localhost:7134/swagger`.

### 3. Run the Blazor frontend

```bash
cd backend/Bruno
dotnet run
```

The frontend will be available at `https://localhost:7037`.

### 4. Run the tests

```bash
cd backend
dotnet test
```

---

## Assumptions

- **.NET 10 SDK** is installed on the development machine.
- **PostgreSQL** (v15+) is running locally on port `5432` with a database named `brunodb`, accessible via the credentials in `appsettings.Development.json`.
- The API is hosted at `https://localhost:7134` during development (matches the Blazor client's `appsettings.json`).
- The Blazor WASM client is hosted at `https://localhost:7037` / `http://localhost:5188` during development (these origins are allow-listed in the API's CORS policy).
- All requests to the API require the `X-Api-Key` header matching the value in `appsettings.json`. The default development key is `bruno-api-key-2026` — this **must** be rotated before any production deployment and set via an environment variable (`ApiKey`).
- Soft-deleted vehicles are excluded from all queries via a global EF Core query filter; there is no hard-delete for vehicles.
- Bookings cannot be deleted if they have a future active status — only cancellation via status update is supported.
- The application seeds initial data (vehicles, customers, bookings) automatically when running in the `Development` environment and the database is empty.
- No authentication beyond the shared API key is implemented; role-based access control is out of scope.

### EF Migrations reference

To add a new migration:

```bash
cd backend
dotnet ef migrations add <MigrationName> --project Bruno.API
dotnet ef database update --project Bruno.API
```
