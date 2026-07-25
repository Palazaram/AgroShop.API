# Graph Report - AgroShop.API  (2026-07-25)

## Corpus Check
- 192 files · ~130,212 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 956 nodes · 1775 edges · 92 communities (67 shown, 25 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 61 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `893dc1d7`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- Domain Errors & Validation Results
- Controllers, Envelope & Category Interface
- Supplier Feature (Entity/Repo/Service)
- SubCategory Feature (Entity/Repo/Service)
- Category Feature (Entity/Repo/Service)
- Auth Service, JWT Handler & User Repo
- Product Feature (Entity/Repo/Service)
- Auth & Category Validators (FluentValidation)
- Role Entity & DbContext Infrastructure
- JWT Token Handler & Refresh Tokens
- Project Structure & NuGet Dependencies
- Dependency Injection & Program Entry
- Launch Settings (Dev Config)
- Core Repository Interfaces & Persistence Impls
- Auth Controller Actions
- Application Service Interfaces & Impls
- Response Extensions & Validation Filter
- Auth Cookie Service
- EF Entity Type Configurations
- Category, Subcategory & AttributeValue Entities
- User Service & Response
- Password Value Object
- DbContext Model Snapshot (Migration 10)
- Attribute Entity & Configuration
- ProductAttribute Entity & Configuration
- User Entity & Configuration
- User Repository Implementation
- Email Value Object
- Phone Value Object
- FirstName Value Object
- 20260705114002_10.Designer.cs
- SubCategory
- Migration 10 (Up/Down)
- Migration 1 - Initial (Up/Down)
- Migration 2 (Up/Down)
- Migration 3 (Up/Down)
- Migration 4 (Up/Down)
- Role
- Migration 6 (Up/Down)
- Migration 7 (Up/Down)
- Migration 8 (Up/Down)
- Migration 9 (Up/Down)
- Migration 11 (Up/Down)
- Migration 1 Designer Snapshot
- ValueObject
- Migration 3 Designer Snapshot
- Migration 4 Designer Snapshot
- Migration 5 Designer Snapshot
- Migration 6 Designer Snapshot
- Migration 7 Designer Snapshot
- Migration 8 Designer Snapshot
- Migration 9 Designer Snapshot
- Email
- Paginated Result
- LastName
- MakeCategoryImagePathRequired
- AuthController HttpGet Attribute
- AuthController HttpPost Attribute
- AuthController IActionResult Type
- AuthController Task Type
- DependencyInjection String Type
- 20251016175310_2.Designer.cs
- 20251026150053_3.Designer.cs
- 20260719153341_MakeCategoryImagePathRequired.Designer.cs

## God Nodes (most connected - your core abstractions)
1. `Error` - 98 edges
2. `AgroShop.Core.Entities` - 46 edges
3. `AgroShop.Core.Shared` - 32 edges
4. `User` - 29 edges
5. `SubCategory` - 28 edges
6. `AgroShop.Core.ValueObjects` - 28 edges
7. `AgroShop.Persistence.Data.Migrations` - 27 edges
8. `Supplier` - 26 edges
9. `Product` - 24 edges
10. `AgroShop.Persistence.Data` - 24 edges

## Surprising Connections (you probably didn't know these)
- `AuthController` --inherits--> `ApplicationController`  [EXTRACTED]
  src/AgroShop.API/Controllers/AuthController.cs → src/AgroShop.API/Controllers/ApplicationController.cs
- `AuthCookieService` --implements--> `IAuthCookieService`  [EXTRACTED]
  src/AgroShop.API/Services/AuthCookieService.cs → src/AgroShop.API/Services/IAuthCookieService.cs
- `ImageStorageService` --implements--> `IImageStorageService`  [EXTRACTED]
  src/AgroShop.API/Services/ImageStorageService.cs → src/AgroShop.Application/Interfaces/IImageStorageService.cs
- `RegisterUserDtoValidator` --references--> `RegisterUserDto`  [EXTRACTED]
  src/AgroShop.Application/Validators/AuthenticationValidators/RegisterUserDtoValidator.cs → src/AgroShop.Application/Dto/AuthDto/RegisterUserDTO.cs
- `AddCategoryDtoValidator` --references--> `AddCategoryDto`  [EXTRACTED]
  src/AgroShop.Application/Validators/CategoryValidators/AddCategoryDtoValidator.cs → src/AgroShop.Application/Dto/CategoryDto/AddCategoryDto.cs

## Import Cycles
- None detected.

## Communities (92 total, 25 thin omitted)

### Community 0 - "Domain Errors & Validation Results"
Cohesion: 0.06
Nodes (21): ActionResult, AgroShop.Core.Enums, ModelStateDictionary, ResponseExtensions, ErrorType, Error, string, Authentication (+13 more)

### Community 1 - "Controllers, Envelope & Category Interface"
Cohesion: 0.11
Nodes (23): ControllerBase, HttpDelete, HttpPut, ApplicationController, IActionResult, Result, UnitResult, CategoryController (+15 more)

### Community 2 - "Supplier Feature (Entity/Repo/Service)"
Cohesion: 0.07
Nodes (34): CancellationToken, Func, Guid, IEnumerable, IQueryable, Result, Task, CancellationToken (+26 more)

### Community 3 - "SubCategory Feature (Entity/Repo/Service)"
Cohesion: 0.07
Nodes (36): ISubCategoryService, CancellationToken, Func, Guid, IEnumerable, IQueryable, Result, Task (+28 more)

### Community 4 - "Category Feature (Entity/Repo/Service)"
Cohesion: 0.05
Nodes (39): ICacheService, CancellationToken, Task, TimeSpan, IImageStorageService, CancellationToken, IFormFile, Task (+31 more)

### Community 5 - "Auth Service, JWT Handler & User Repo"
Cohesion: 0.20
Nodes (6): RefreshToken, DateTime, Guid, EntityTypeBuilder, CancellationToken, Task

### Community 6 - "Product Feature (Entity/Repo/Service)"
Cohesion: 0.12
Nodes (20): Product, Guid, ICollection, IProductRepository, CancellationToken, Expression, Func, Guid (+12 more)

### Community 7 - "Auth & Category Validators (FluentValidation)"
Cohesion: 0.24
Nodes (8): IRuleBuilder, IRuleBuilderOptions, IRuleBuilderOptionsConditions, ValidationExtensions, Func, IFormFile, Result, string

### Community 8 - "Role Entity & DbContext Infrastructure"
Cohesion: 0.20
Nodes (8): IRoleRepository, CancellationToken, Guid, Task, RoleRepository, CancellationToken, Guid, Task

### Community 9 - "JWT Token Handler & Refresh Tokens"
Cohesion: 0.13
Nodes (14): AgroShop.API.Responses, AgroShop.API.Middlewares, Exception, HttpContext, RequestDelegate, ExceptionHandler, ILogger, IWebHostEnvironment (+6 more)

### Community 10 - "Project Structure & NuGet Dependencies"
Cohesion: 0.07
Nodes (25): CSharpFunctionalExtensions (3.7.0), FluentValidation (12.1.1), Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9), Microsoft.AspNetCore.OpenApi (10.0.9), Microsoft.EntityFrameworkCore (10.0.9), Microsoft.EntityFrameworkCore.Design (10.0.9), Microsoft.EntityFrameworkCore.Relational (10.0.9), Microsoft.Extensions.Configuration.EnvironmentVariables (10.0.9) (+17 more)

### Community 11 - "Dependency Injection & Program Entry"
Cohesion: 0.33
Nodes (4): IServiceProvider, DependencyInjection, IConfiguration, IServiceCollection

### Community 12 - "Launch Settings (Dev Config)"
Cohesion: 0.10
Nodes (21): applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, launchUrl, ASPNETCORE_ENVIRONMENT, commandName (+13 more)

### Community 13 - "Core Repository Interfaces & Persistence Impls"
Cohesion: 0.09
Nodes (26): IConfigurationSection, PasswordHasher, IJwtTokenHandler, CancellationToken, Result, Task, JwtTokenHandler, CancellationToken (+18 more)

### Community 14 - "Auth Controller Actions"
Cohesion: 0.09
Nodes (26): AllowAnonymous, Authorize, AuthController, CancellationToken, HttpGet, HttpPost, IActionResult, Task (+18 more)

### Community 15 - "Application Service Interfaces & Impls"
Cohesion: 0.06
Nodes (28): AgroShop.Core.Entities, AgroShop.Persistence.Extensions, AgroShop.Core.ValueObjects, AgroShop.Persistence.Configurations, AgroShop.Persistence.Repositories, AgroShop.Core.Interfaces, AgroShop.Persistence.Data, IEntityTypeConfiguration (+20 more)

### Community 16 - "Response Extensions & Validation Filter"
Cohesion: 0.25
Nodes (5): CategoryName, IEnumerable, int, Result, ValueObject

### Community 17 - "Auth Cookie Service"
Cohesion: 0.25
Nodes (5): Phone, IEnumerable, int, Result, string

### Community 18 - "EF Entity Type Configurations"
Cohesion: 0.23
Nodes (7): AgroShop.Application.Services, AgroShop.Application.Jwt, AgroShop.Application.Responses, AgroShop.Core.Shared, AgroShop.Application.Interfaces, ISupplierService, SupplierService

### Community 19 - "Category, Subcategory & AttributeValue Entities"
Cohesion: 0.43
Nodes (4): DependencyInjection, IConfiguration, IServiceCollection, string

### Community 20 - "User Service & Response"
Cohesion: 0.18
Nodes (10): AbstractValidator, AgroShop.Application.Validators.CategoryValidators, AgroShop.Application.Dto.AuthDto, AgroShop.Application.Validators.AuthenticationValidators, AgroShop.Application.Extensions, LoginUserDto, LoginUserDtoValidator, RegisterUserDtoValidator (+2 more)

### Community 21 - "Password Value Object"
Cohesion: 0.22
Nodes (5): Regex, Password, IEnumerable, int, Result

### Community 22 - "DbContext Model Snapshot (Migration 10)"
Cohesion: 0.14
Nodes (8): AgroShop.Persistence.Data.Migrations, ModelSnapshot, _7, ModelBuilder, _9, ModelBuilder, AgroShopDbContextModelSnapshot, ModelBuilder

### Community 23 - "Attribute Entity & Configuration"
Cohesion: 0.44
Nodes (4): UserRepository, CancellationToken, Guid, Task

### Community 24 - "ProductAttribute Entity & Configuration"
Cohesion: 0.32
Nodes (5): ImageStorageService, CancellationToken, IFormFile, IWebHostEnvironment, Task

### Community 25 - "User Entity & Configuration"
Cohesion: 0.21
Nodes (5): AgroShop.API.Filters, AgroShop.API.Extensions, AgroShop.API, AgroShop.API.Controllers, AgroShop.API.Services

### Community 26 - "User Repository Implementation"
Cohesion: 0.22
Nodes (7): HashSet, User, DateTime, Guid, IReadOnlyCollection, Result, EntityTypeBuilder

### Community 27 - "Email Value Object"
Cohesion: 0.33
Nodes (6): IConnectionMultiplexer, IDatabase, RedisCacheService, CancellationToken, Task, TimeSpan

### Community 28 - "Phone Value Object"
Cohesion: 0.33
Nodes (4): AgroShop.Application.Dto.CategoryDto, AgroShop.Application.Mappers, UpdateCategoryDto, IFormFile

### Community 29 - "FirstName Value Object"
Cohesion: 0.21
Nodes (7): DbContext, DbSet, IDesignTimeDbContextFactory, AgroShopDbContext, ModelBuilder, AgroShopDbContextFactory, IConfiguration

### Community 30 - "20260705114002_10.Designer.cs"
Cohesion: 0.33
Nodes (4): Role, Guid, RoleConfiguration, EntityTypeBuilder

### Community 31 - "SubCategory"
Cohesion: 0.33
Nodes (5): ActionExecutingContext, ActionExecutionDelegate, IAsyncActionFilter, ValidationFilter, Task

### Community 42 - "Migration 11 (Up/Down)"
Cohesion: 0.40
Nodes (3): Migration, _11, MigrationBuilder

### Community 44 - "ValueObject"
Cohesion: 0.29
Nodes (4): Email, IEnumerable, int, Result

### Community 45 - "Migration 3 Designer Snapshot"
Cohesion: 0.29
Nodes (4): FirstName, IEnumerable, int, Result

### Community 49 - "Migration 7 Designer Snapshot"
Cohesion: 0.29
Nodes (4): LastName, IEnumerable, int, Result

### Community 51 - "Migration 9 Designer Snapshot"
Cohesion: 0.29
Nodes (4): Patronymic, IEnumerable, int, Result

### Community 53 - "Paginated Result"
Cohesion: 0.15
Nodes (8): AgroShop.Persistence, AgroShop.Infrastructure, AgroShop.Application, AgroShop.Infrastructure.Caching, Program, DependencyInjection, IConfiguration, IServiceCollection

### Community 54 - "LastName"
Cohesion: 0.31
Nodes (6): CookieOptions, DateTimeOffset, AuthCookieService, HttpResponse, int, string

## Knowledge Gaps
- **39 isolated node(s):** `net10.0`, `FluentValidation.DependencyInjectionExtensions (12.1.1)`, `Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9)`, `Microsoft.AspNetCore.OpenApi (10.0.9)`, `Swashbuckle.AspNetCore (10.2.3)` (+34 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **25 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Error` connect `Domain Errors & Validation Results` to `Controllers, Envelope & Category Interface`, `Supplier Feature (Entity/Repo/Service)`, `SubCategory Feature (Entity/Repo/Service)`, `Category Feature (Entity/Repo/Service)`, `Auth & Category Validators (FluentValidation)`, `ValueObject`, `Core Repository Interfaces & Persistence Impls`, `Auth Controller Actions`, `Migration 3 Designer Snapshot`, `Response Extensions & Validation Filter`, `Migration 7 Designer Snapshot`, `Auth Cookie Service`, `Migration 9 Designer Snapshot`, `Password Value Object`, `User Repository Implementation`?**
  _High betweenness centrality (0.262) - this node is a cross-community bridge._
- **Why does `AgroShop.Persistence.Data` connect `Application Service Interfaces & Impls` to `Migration 1 Designer Snapshot`, `Migration 4 Designer Snapshot`, `Migration 5 Designer Snapshot`, `Migration 6 Designer Snapshot`, `Migration 8 Designer Snapshot`, `Email`, `20251016175310_2.Designer.cs`, `20251026150053_3.Designer.cs`, `DbContext Model Snapshot (Migration 10)`, `AuthController HttpGet Attribute`, `AuthController Task Type`, `DependencyInjection String Type`, `FirstName Value Object`?**
  _High betweenness centrality (0.209) - this node is a cross-community bridge._
- **Why does `AgroShop.Core.Shared` connect `EF Entity Type Configurations` to `Domain Errors & Validation Results`, `Category Feature (Entity/Repo/Service)`, `ValueObject`, `Migration 3 Designer Snapshot`, `Application Service Interfaces & Impls`, `Response Extensions & Validation Filter`, `Migration 7 Designer Snapshot`, `Auth Cookie Service`, `Migration 9 Designer Snapshot`, `User Service & Response`, `Password Value Object`, `20260719153341_MakeCategoryImagePathRequired.Designer.cs`, `User Entity & Configuration`, `User Repository Implementation`, `Phone Value Object`?**
  _High betweenness centrality (0.143) - this node is a cross-community bridge._
- **What connects `net10.0`, `FluentValidation.DependencyInjectionExtensions (12.1.1)`, `Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9)` to the rest of the system?**
  _39 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Domain Errors & Validation Results` be split into smaller, more focused modules?**
  _Cohesion score 0.055501460564751706 - nodes in this community are weakly interconnected._
- **Should `Controllers, Envelope & Category Interface` be split into smaller, more focused modules?**
  _Cohesion score 0.11428571428571428 - nodes in this community are weakly interconnected._
- **Should `Supplier Feature (Entity/Repo/Service)` be split into smaller, more focused modules?**
  _Cohesion score 0.06848357791754019 - nodes in this community are weakly interconnected._