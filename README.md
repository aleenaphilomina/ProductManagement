# ProductManagement

Small, production-minded Product Management API (NET 8) with:
- REST endpoints for CRUD on `Product`
- EF Core persistence (SQLite by default)
- Optimistic concurrency (`RowVersion` / base64 in JSON)
- Swagger UI + minimal browser UI (`wwwroot/index.html`)
- Clean layering: Controllers → Services → Repositories → `ApplicationDbContext`

## Quickstart

Prerequisites
- .NET 8 SDK
- (Optional) `dotnet-ef` tool for migrations: `dotnet tool install --global dotnet-ef`
- (Optional) Visual Studio 2022

Clone and restore
Configuration
- Primary configuration is `appsettings.json`.
- Default SQLite connection string: `ConnectionStrings:DefaultConnection = "Data Source=products.db"`.
- To use In-Memory provider for ephemeral testing, update `Program.cs` to call `UseInMemoryDatabase(...)`.

Database (SQLite)
- Add packages if missing:
- Create and apply migrations (CLI):
- Or use Visual Studio __Package Manager Console__:

## Features
- Add a new `Product`: `POST /api/products`.
- Get all products: `GET /api/products`.
- Get a single product by ID: `GET /api/products/{id}`.
- Update a product by ID: `PUT /api/products/{id}`.
- Delete a product by ID: `DELETE /api/products/{id}`.

## Notes
- For brevity, only `Product` entity and basic CRUD are covered.
- Consider adding pagination, filtering, sorting for scalability.
- Secure API keys/secrets in production; consider using `DotNet Env` or Azure Key Vault.

## Optional Enhancements
- Use Azure SQL Database or other providers by updating connection string and installing necessary EF Core packages.
- Enable CORS in `Startup.cs` for cross-origin requests.
- Implement JWT or API key authentication.
- Integrate with a message broker (e.g., Azure Service Bus, RabbitMQ) for async processing.
- Add a caching layer (e.g., Redis) to cache product data and reduce database load.

## Thoughts?
This README aims to provide a solid foundation. Feel free to suggest improvements, additional features, or any other feedback.

## TODO
- [ ] Implement logging (e.g., Serilog) to track API usage and errors.
- [ ] Set up monitoring and alerts for the API (e.g., Azure Application Insights).
- [ ] Write comprehensive tests covering all aspects of the API.
- [ ] Add CI/CD pipeline for automated testing and deployment.
- [ ] Document API endpoints, request/response formats, and error codes.

## Development notes
- DTOs isolate API surface: `CreateProductDto`, `UpdateProductDto`, `ProductDto`.
- Consider adding `AutoMapper` and `FluentValidation` for scale.
- Add tests under `tests/...` using `WebApplicationFactory<Program>` and an isolated InMemory DB.

## Contributing
- Fork → feature branch → PR with tests and brief description.
- Keep migrations committed (if using SQLite) for deterministic CI setup.

## License
- Check repository root for license file (or add one, e.g. MIT).

If you want, I can prepare:
- A short `Makefile` / `scripts/` for migrations + run.
- A ready `docker-compose.yml` with SQLite or SQL Server.

If you prefer no concurrency token for simpler PUT flows, remove `RowVersion` from `UpdateProductDto` and related logic.
