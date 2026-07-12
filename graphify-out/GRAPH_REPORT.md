# Graph Report - .  (2026-07-12)

## Corpus Check
- Corpus is ~16,290 words - fits in a single context window. You may not need a graph.

## Summary
- 888 nodes · 1702 edges · 57 communities (35 shown, 22 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 55 edges (avg confidence: 0.79)
- Token cost: 45,000 input · 4,281 output

## Community Hubs (Navigation)
- Domain Errors & Validation Results
- Supplier Feature (Entity/Repo/Service)
- SubCategory Feature (Entity/Repo/Service)
- Category Feature (Entity/Repo/Service)
- Validators, DTOs & Action Filters
- Auth Controller & Service Interfaces
- Auth Service Implementation & JWT Handler
- Product Feature (Entity/Repo/Service)
- Category Controller & API Layer
- JWT Token Handler & Refresh Tokens
- Role Entity & DbContext Infrastructure
- Project Structure & NuGet Dependencies
- Application DI & Service Interfaces
- Core Repository Interfaces & Persistence Impls
- Launch Settings (Dev Config)
- Product Attribute & EF Configurations
- Program Entry & Exception Middleware
- User Entity & Configuration
- Password Value Object
- DbContext Model Snapshot (Migration 10)
- FluentValidation Rule Builder Extensions
- ProductAttribute Entity & Config
- API Controllers & Cookie Auth Services
- ProductAttributeValue Entity & Config
- User Repository
- README & Docker Compose
- API Response Envelope
- Email Value Object
- Phone Value Object
- FirstName Value Object
- LastName Value Object
- Patronymic Value Object
- Migration 10 (Up/Down)
- Persistence DI & Migration Runner
- Migration 1 (Initial)
- Migration 2
- Migration 3
- Migration 4
- Migration 5
- Migration 6
- Migration 7
- Migration 8
- Migration 9
- Migration 11
- Product Query Parameters
- Migration 1 Designer
- Migration 2 Designer
- Migration 3 Designer
- Migration 4 Designer
- Migration 5 Designer
- Migration 6 Designer
- Migration 7 Designer
- Migration 8 Designer
- Migration 9 Designer
- Migration 11 Designer
- Paginated Result
- Role Constants

## God Nodes (most connected - your core abstractions)
1. `Error` - 93 edges
2. `AgroShop.Core.Entities` - 46 edges
3. `AgroShop.Core.ValueObjects` - 30 edges
4. `AgroShop.Core.Shared` - 29 edges
5. `SubCategory` - 28 edges
6. `User` - 28 edges
7. `Category` - 26 edges
8. `Supplier` - 26 edges
9. `Product` - 25 edges
10. `AgroShopDbContext` - 24 edges

## Surprising Connections (you probably didn't know these)
- `AgroShop.API (project)` --conceptually_related_to--> `api service (ASP.NET Core container)`  [INFERRED]
  README.md → docker-compose.yml
- `AgroShop.API (project)` --conceptually_related_to--> `db service (PostgreSQL container)`  [INFERRED]
  README.md → docker-compose.yml
- `AuthController` --inherits--> `ApplicationController`  [EXTRACTED]
  src/AgroShop.API/Controllers/AuthController.cs → src/AgroShop.API/Controllers/ApplicationController.cs
- `AuthCookieService` --implements--> `IAuthCookieService`  [EXTRACTED]
  src/AgroShop.API/Services/AuthCookieService.cs → src/AgroShop.API/Services/IAuthCookieService.cs
- `LoginUserDtoValidator` --references--> `LoginUserDto`  [EXTRACTED]
  src/AgroShop.API/Validators/AuthenticationValidators/LoginUserDtoValidator.cs → src/AgroShop.Application/Dto/AuthDto/LoginUserDto.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **AgroShop.API project stack (README + Docker Compose services)** — readme_agroshop_api, docker_compose_api, docker_compose_db [INFERRED 0.70]
- **Shared Postgres credential flow (db env, api connection string, .env secrets)** — docker_compose_db, docker_compose_api, docker_compose_postgres_credentials, docker_compose_defaultconnection [INFERRED 0.85]

## Communities (57 total, 22 thin omitted)

### Community 0 - "Domain Errors & Validation Results"
Cohesion: 0.06
Nodes (19): ActionResult, AgroShop.Core.Enums, ResponseExtensions, ErrorType, Error, string, Authentication, Category (+11 more)

### Community 1 - "Supplier Feature (Entity/Repo/Service)"
Cohesion: 0.06
Nodes (37): ISupplierService, CancellationToken, Func, Guid, IEnumerable, IQueryable, Result, Task (+29 more)

### Community 2 - "SubCategory Feature (Entity/Repo/Service)"
Cohesion: 0.07
Nodes (38): IEntityTypeConfiguration, ISubCategoryService, CancellationToken, Func, Guid, IEnumerable, IQueryable, Result (+30 more)

### Community 3 - "Category Feature (Entity/Repo/Service)"
Cohesion: 0.06
Nodes (35): CategoryService, CancellationToken, Func, IEnumerable, IQueryable, Result, Task, UnitResult (+27 more)

### Community 4 - "Validators, DTOs & Action Filters"
Cohesion: 0.05
Nodes (32): AbstractValidator, ActionExecutingContext, ActionExecutionDelegate, AgroShop.API.Filters, AgroShop.Application.Validators.ProductValidators, AgroShop.Application.Dto.ProductDto, AgroShop.API, AgroShop.API.Validators.CategoryValidators (+24 more)

### Community 5 - "Auth Controller & Service Interfaces"
Cohesion: 0.09
Nodes (27): AllowAnonymous, Authorize, AuthController, CancellationToken, HttpGet, HttpPost, IActionResult, Task (+19 more)

### Community 6 - "Auth Service Implementation & JWT Handler"
Cohesion: 0.10
Nodes (22): CookieOptions, DateTimeOffset, PasswordHasher, AuthCookieService, HttpResponse, int, string, IJwtTokenHandler (+14 more)

### Community 7 - "Product Feature (Entity/Repo/Service)"
Cohesion: 0.10
Nodes (23): Product, Guid, ICollection, IProductRepository, CancellationToken, Expression, Func, Guid (+15 more)

### Community 8 - "Category Controller & API Layer"
Cohesion: 0.11
Nodes (23): ControllerBase, HttpDelete, HttpPut, ApplicationController, IActionResult, Result, UnitResult, CategoryController (+15 more)

### Community 9 - "JWT Token Handler & Refresh Tokens"
Cohesion: 0.10
Nodes (17): IConfigurationSection, JwtTokenHandler, CancellationToken, int, Result, Task, RefreshToken, DateTime (+9 more)

### Community 10 - "Role Entity & DbContext Infrastructure"
Cohesion: 0.08
Nodes (19): DbContext, DbSet, IDesignTimeDbContextFactory, Role, Guid, IRoleRepository, CancellationToken, Guid (+11 more)

### Community 11 - "Project Structure & NuGet Dependencies"
Cohesion: 0.08
Nodes (22): CSharpFunctionalExtensions (3.7.0), FluentValidation (12.1.1), Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9), Microsoft.AspNetCore.OpenApi (10.0.9), Microsoft.EntityFrameworkCore (10.0.9), Microsoft.EntityFrameworkCore.Design (10.0.9), Microsoft.EntityFrameworkCore.Relational (10.0.9), Microsoft.Extensions.Configuration.EnvironmentVariables (10.0.9) (+14 more)

### Community 12 - "Application DI & Service Interfaces"
Cohesion: 0.18
Nodes (9): AgroShop.Application.Services, AgroShop.Application.Dto.CategoryDto, AgroShop.Application.Jwt, AgroShop.Application.Responses, AgroShop.Core.Shared, AgroShop.Application.Dto.AuthDto, AgroShop.Application.Interfaces, DependencyInjection (+1 more)

### Community 13 - "Core Repository Interfaces & Persistence Impls"
Cohesion: 0.23
Nodes (5): AgroShop.Core.Entities, AgroShop.Persistence.Extensions, AgroShop.Persistence.Repositories, AgroShop.Core.Interfaces, AgroShop.Persistence.Data

### Community 14 - "Launch Settings (Dev Config)"
Cohesion: 0.10
Nodes (21): applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, launchUrl, ASPNETCORE_ENVIRONMENT, commandName (+13 more)

### Community 15 - "Product Attribute & EF Configurations"
Cohesion: 0.14
Nodes (7): AgroShop.Core.ValueObjects, AgroShop.Persistence.Configurations, Attribute, Guid, ICollection, AttributeConfiguration, EntityTypeBuilder

### Community 16 - "Program Entry & Exception Middleware"
Cohesion: 0.15
Nodes (11): AgroShop.Persistence, AgroShop.Application, AgroShop.API.Middlewares, Exception, HttpContext, IWebHostEnvironment, RequestDelegate, ExceptionHandler (+3 more)

### Community 17 - "User Entity & Configuration"
Cohesion: 0.22
Nodes (7): HashSet, User, DateTime, Guid, IReadOnlyCollection, UserConfiguration, EntityTypeBuilder

### Community 18 - "Password Value Object"
Cohesion: 0.22
Nodes (5): Regex, Password, IEnumerable, int, Result

### Community 19 - "DbContext Model Snapshot (Migration 10)"
Cohesion: 0.20
Nodes (6): AgroShop.Persistence.Data.Migrations, ModelSnapshot, _10, ModelBuilder, AgroShopDbContextModelSnapshot, ModelBuilder

### Community 20 - "FluentValidation Rule Builder Extensions"
Cohesion: 0.31
Nodes (6): IRuleBuilder, IRuleBuilderOptions, IRuleBuilderOptionsConditions, ValidationExtensions, Func, Result

### Community 21 - "ProductAttribute Entity & Config"
Cohesion: 0.27
Nodes (5): ProductAttribute, Guid, ICollection, ProductAttributeConfiguration, EntityTypeBuilder

### Community 22 - "API Controllers & Cookie Auth Services"
Cohesion: 0.31
Nodes (4): AgroShop.API.Responses, AgroShop.API.Extensions, AgroShop.API.Controllers, AgroShop.API.Services

### Community 23 - "ProductAttributeValue Entity & Config"
Cohesion: 0.31
Nodes (4): ProductAttributeValue, Guid, ProductAttributeValueConfiguration, EntityTypeBuilder

### Community 24 - "User Repository"
Cohesion: 0.44
Nodes (4): UserRepository, CancellationToken, Guid, Task

### Community 25 - "README & Docker Compose"
Cohesion: 0.36
Nodes (8): agroshop_data volume, api service (ASP.NET Core container), db service (PostgreSQL container), ConnectionStrings__DefaultConnection, Dockerfile (api build context), JWT_SECRET_KEY secret, Postgres credentials (POSTGRES_DB / POSTGRES_USER / POSTGRES_PASSWORD), AgroShop.API (project)

### Community 26 - "API Response Envelope"
Cohesion: 0.29
Nodes (5): Envelope, DateTime, IEnumerable, List, ResponseError

### Community 27 - "Email Value Object"
Cohesion: 0.25
Nodes (5): Email, IEnumerable, int, Result, ValueObject

### Community 28 - "Phone Value Object"
Cohesion: 0.25
Nodes (5): Phone, IEnumerable, int, Result, string

### Community 29 - "FirstName Value Object"
Cohesion: 0.29
Nodes (4): FirstName, IEnumerable, int, Result

### Community 30 - "LastName Value Object"
Cohesion: 0.29
Nodes (4): LastName, IEnumerable, int, Result

### Community 31 - "Patronymic Value Object"
Cohesion: 0.29
Nodes (4): Patronymic, IEnumerable, int, Result

### Community 32 - "Migration 10 (Up/Down)"
Cohesion: 0.40
Nodes (3): Migration, _10, MigrationBuilder

### Community 33 - "Persistence DI & Migration Runner"
Cohesion: 0.40
Nodes (3): IServiceProvider, DependencyInjection, IServiceCollection

### Community 44 - "Product Query Parameters"
Cohesion: 0.50
Nodes (3): AgroShop.Application.QueryParameters, ProductQueryParameters, Guid

## Knowledge Gaps
- **40 isolated node(s):** `net10.0`, `FluentValidation.DependencyInjectionExtensions (12.1.1)`, `Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9)`, `Microsoft.AspNetCore.OpenApi (10.0.9)`, `Swashbuckle.AspNetCore (10.2.3)` (+35 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **22 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Error` connect `Domain Errors & Validation Results` to `Supplier Feature (Entity/Repo/Service)`, `SubCategory Feature (Entity/Repo/Service)`, `Category Feature (Entity/Repo/Service)`, `Auth Controller & Service Interfaces`, `Auth Service Implementation & JWT Handler`, `Category Controller & API Layer`, `JWT Token Handler & Refresh Tokens`, `Password Value Object`, `FluentValidation Rule Builder Extensions`, `Email Value Object`, `Phone Value Object`, `FirstName Value Object`, `LastName Value Object`, `Patronymic Value Object`?**
  _High betweenness centrality (0.258) - this node is a cross-community bridge._
- **Why does `AgroShop.Persistence.Data` connect `Core Repository Interfaces & Persistence Impls` to `Role Entity & DbContext Infrastructure`, `Migration 1 Designer`, `Migration 2 Designer`, `Migration 3 Designer`, `Product Attribute & EF Configurations`, `Migration 4 Designer`, `Migration 5 Designer`, `Migration 6 Designer`, `Migration 7 Designer`, `Migration 8 Designer`, `Migration 9 Designer`, `DbContext Model Snapshot (Migration 10)`, `Migration 11 Designer`?**
  _High betweenness centrality (0.199) - this node is a cross-community bridge._
- **Why does `AgroShopDbContext` connect `Role Entity & DbContext Infrastructure` to `Supplier Feature (Entity/Repo/Service)`, `SubCategory Feature (Entity/Repo/Service)`, `Category Feature (Entity/Repo/Service)`, `Product Feature (Entity/Repo/Service)`, `JWT Token Handler & Refresh Tokens`, `Product Attribute & EF Configurations`, `User Entity & Configuration`, `ProductAttribute Entity & Config`, `ProductAttributeValue Entity & Config`, `User Repository`?**
  _High betweenness centrality (0.153) - this node is a cross-community bridge._
- **What connects `net10.0`, `FluentValidation.DependencyInjectionExtensions (12.1.1)`, `Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9)` to the rest of the system?**
  _40 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Domain Errors & Validation Results` be split into smaller, more focused modules?**
  _Cohesion score 0.05960755275823769 - nodes in this community are weakly interconnected._
- **Should `Supplier Feature (Entity/Repo/Service)` be split into smaller, more focused modules?**
  _Cohesion score 0.0647307924984876 - nodes in this community are weakly interconnected._
- **Should `SubCategory Feature (Entity/Repo/Service)` be split into smaller, more focused modules?**
  _Cohesion score 0.06704260651629072 - nodes in this community are weakly interconnected._