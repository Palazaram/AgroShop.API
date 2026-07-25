# Graph Report - AgroShop.API  (2026-07-25)

## Corpus Check
- 204 files · ~217,522 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 1020 nodes · 1937 edges · 103 communities (77 shown, 26 thin omitted)
- Extraction: 96% EXTRACTED · 4% INFERRED · 0% AMBIGUOUS · INFERRED: 85 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `3f02f036`
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
- ExceptionHandler
- AuthController IActionResult Type
- AuthController Task Type
- DependencyInjection String Type
- 20251016175310_2.Designer.cs
- 20251026150053_3.Designer.cs
- 20260719153341_MakeCategoryImagePathRequired.Designer.cs
- RedisCacheService
- ProductAttribute
- ProductAttributeValue
- CategoryRepository
- .GetCategoryByIdAsync
- .GetSubCategoryByIdAsync
- _10
- AlignCategoryNameMaxLengthWithDomain
- 20260127152943_5.Designer.cs
- 20260308131958_9.Designer.cs
- 20260719153341_MakeCategoryImagePathRequired.Designer.cs

## God Nodes (most connected - your core abstractions)
1. `Error` - 114 edges
2. `AgroShop.Core.Entities` - 46 edges
3. `AgroShop.Core.Shared` - 34 edges
4. `AgroShop.Persistence.Data.Migrations` - 31 edges
5. `AgroShop.Core.ValueObjects` - 30 edges
6. `User` - 29 edges
7. `Supplier` - 26 edges
8. `AgroShop.Persistence.Data` - 26 edges
9. `Product` - 24 edges
10. `AgroShopDbContext` - 24 edges

## Surprising Connections (you probably didn't know these)
- `Error` --references--> `ErrorType`  [EXTRACTED]
  src/AgroShop.Core/Shared/Error.cs → src/AgroShop.Core/Enums/ErrorType.cs
- `AuthController` --inherits--> `ApplicationController`  [EXTRACTED]
  src/AgroShop.API/Controllers/AuthController.cs → src/AgroShop.API/Controllers/ApplicationController.cs
- `CategoryController` --inherits--> `ApplicationController`  [EXTRACTED]
  src/AgroShop.API/Controllers/CategoryController.cs → src/AgroShop.API/Controllers/ApplicationController.cs
- `AuthCookieService` --implements--> `IAuthCookieService`  [EXTRACTED]
  src/AgroShop.API/Services/AuthCookieService.cs → src/AgroShop.API/Services/IAuthCookieService.cs
- `ImageStorageService` --implements--> `IImageStorageService`  [EXTRACTED]
  src/AgroShop.API/Services/ImageStorageService.cs → src/AgroShop.Application/Interfaces/IImageStorageService.cs

## Import Cycles
- None detected.

## Communities (103 total, 26 thin omitted)

### Community 0 - "Domain Errors & Validation Results"
Cohesion: 0.06
Nodes (17): Error, string, Authentication, Category, CategoryName, Email, Errors, FirstName (+9 more)

### Community 1 - "Controllers, Envelope & Category Interface"
Cohesion: 0.12
Nodes (20): CategoryController, CancellationToken, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult, Task (+12 more)

### Community 2 - "Supplier Feature (Entity/Repo/Service)"
Cohesion: 0.07
Nodes (37): ISupplierService, CancellationToken, Func, Guid, IEnumerable, IQueryable, Result, Task (+29 more)

### Community 3 - "SubCategory Feature (Entity/Repo/Service)"
Cohesion: 0.15
Nodes (13): SubCategoryMapper, IEnumerable, SubCategory, Guid, ICollection, Result, SubCategoryConfiguration, EntityTypeBuilder (+5 more)

### Community 4 - "Category Feature (Entity/Repo/Service)"
Cohesion: 0.15
Nodes (15): IImageStorageService, CancellationToken, IFormFile, Task, CategoryService, CancellationToken, IEnumerable, Result (+7 more)

### Community 5 - "Auth Service, JWT Handler & User Repo"
Cohesion: 0.21
Nodes (10): IConfigurationSection, JwtTokenHandler, CancellationToken, int, Result, Task, IRefreshTokenRepository, CancellationToken (+2 more)

### Community 6 - "Product Feature (Entity/Repo/Service)"
Cohesion: 0.12
Nodes (21): Product, Guid, ICollection, IProductRepository, CancellationToken, Expression, Func, Guid (+13 more)

### Community 7 - "Auth & Category Validators (FluentValidation)"
Cohesion: 0.22
Nodes (8): IRuleBuilder, IRuleBuilderOptions, IRuleBuilderOptionsConditions, ValidationExtensions, Func, IFormFile, Result, string

### Community 8 - "Role Entity & DbContext Infrastructure"
Cohesion: 0.20
Nodes (8): IRoleRepository, CancellationToken, Guid, Task, RoleRepository, CancellationToken, Guid, Task

### Community 9 - "JWT Token Handler & Refresh Tokens"
Cohesion: 0.28
Nodes (6): AgroShop.API.Responses, Envelope, DateTime, IEnumerable, List, ResponseError

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
Cohesion: 0.19
Nodes (12): PasswordHasher, IJwtTokenHandler, CancellationToken, Result, Task, AuthResponse, AuthService, CancellationToken (+4 more)

### Community 14 - "Auth Controller Actions"
Cohesion: 0.08
Nodes (32): AllowAnonymous, Authorize, AgroShop.Application.Dto.AuthDto, AgroShop.Application.Validators.AuthenticationValidators, AgroShop.Application.Extensions, AuthController, CancellationToken, HttpGet (+24 more)

### Community 15 - "Application Service Interfaces & Impls"
Cohesion: 0.19
Nodes (7): RefreshToken, DateTime, Guid, RefreshTokenConfiguration, EntityTypeBuilder, CancellationToken, Task

### Community 16 - "Response Extensions & Validation Filter"
Cohesion: 0.29
Nodes (5): CategoryName, IEnumerable, int, Result, ValueObject

### Community 17 - "Auth Cookie Service"
Cohesion: 0.29
Nodes (5): Phone, IEnumerable, int, Result, string

### Community 18 - "EF Entity Type Configurations"
Cohesion: 0.29
Nodes (4): AgroShop.Persistence, AgroShop.Application, AgroShop.API.Middlewares, Program

### Community 19 - "Category, Subcategory & AttributeValue Entities"
Cohesion: 0.43
Nodes (4): DependencyInjection, IConfiguration, IServiceCollection, string

### Community 20 - "User Service & Response"
Cohesion: 0.18
Nodes (6): ActionResult, AgroShop.Core.Enums, ModelStateDictionary, ResponseExtensions, ErrorType, ValidationResult

### Community 21 - "Password Value Object"
Cohesion: 0.24
Nodes (5): Regex, Password, IEnumerable, int, Result

### Community 22 - "DbContext Model Snapshot (Migration 10)"
Cohesion: 0.14
Nodes (8): AgroShop.Persistence.Data.Migrations, ModelSnapshot, _8, ModelBuilder, AlignCategoryNameMaxLengthWithDomain, ModelBuilder, AgroShopDbContextModelSnapshot, ModelBuilder

### Community 23 - "Attribute Entity & Configuration"
Cohesion: 0.44
Nodes (4): UserRepository, CancellationToken, Guid, Task

### Community 24 - "ProductAttribute Entity & Configuration"
Cohesion: 0.22
Nodes (11): SubCategoryDto, Guid, SubCategoryService, CancellationToken, Guid, IEnumerable, Result, string (+3 more)

### Community 25 - "User Entity & Configuration"
Cohesion: 0.31
Nodes (6): CookieOptions, DateTimeOffset, AuthCookieService, HttpResponse, int, string

### Community 26 - "User Repository Implementation"
Cohesion: 0.14
Nodes (11): CategoryMapper, IEnumerable, Category, Guid, IReadOnlyCollection, List, Result, CategoryConfiguration (+3 more)

### Community 27 - "Email Value Object"
Cohesion: 0.19
Nodes (7): AgroShop.API.Filters, AgroShop.API.Extensions, AgroShop.API, AgroShop.API.Controllers, AgroShop.API.Services, IAsyncActionFilter, ValidationFilter

### Community 28 - "Phone Value Object"
Cohesion: 0.29
Nodes (5): IEntityTypeConfiguration, Role, Guid, RoleConfiguration, EntityTypeBuilder

### Community 29 - "FirstName Value Object"
Cohesion: 0.21
Nodes (7): DbContext, DbSet, IDesignTimeDbContextFactory, AgroShopDbContext, ModelBuilder, AgroShopDbContextFactory, IConfiguration

### Community 30 - "20260705114002_10.Designer.cs"
Cohesion: 0.33
Nodes (4): SubCategoryName, IEnumerable, int, Result

### Community 31 - "SubCategory"
Cohesion: 0.50
Nodes (3): ActionExecutingContext, ActionExecutionDelegate, Task

### Community 32 - "Migration 10 (Up/Down)"
Cohesion: 0.22
Nodes (5): Migration, _2, MigrationBuilder, _4, MigrationBuilder

### Community 33 - "Migration 1 - Initial (Up/Down)"
Cohesion: 0.08
Nodes (30): AbstractValidator, ControllerBase, AgroShop.Application.Dto.SubCategoryDto, AgroShop.Application.Validators.SubCategoryValidators, ApplicationController, IActionResult, Result, UnitResult (+22 more)

### Community 34 - "Migration 2 (Up/Down)"
Cohesion: 0.32
Nodes (5): ImageStorageService, CancellationToken, IFormFile, IWebHostEnvironment, Task

### Community 44 - "ValueObject"
Cohesion: 0.33
Nodes (4): Email, IEnumerable, int, Result

### Community 45 - "Migration 3 Designer Snapshot"
Cohesion: 0.33
Nodes (4): FirstName, IEnumerable, int, Result

### Community 49 - "Migration 7 Designer Snapshot"
Cohesion: 0.33
Nodes (4): LastName, IEnumerable, int, Result

### Community 50 - "Migration 8 Designer Snapshot"
Cohesion: 0.39
Nodes (4): ICacheService, CancellationToken, Task, TimeSpan

### Community 51 - "Migration 9 Designer Snapshot"
Cohesion: 0.33
Nodes (4): Patronymic, IEnumerable, int, Result

### Community 54 - "LastName"
Cohesion: 0.18
Nodes (12): HashSet, User, DateTime, Guid, IReadOnlyCollection, Result, IUserRepository, CancellationToken (+4 more)

### Community 57 - "ExceptionHandler"
Cohesion: 0.31
Nodes (7): Exception, HttpContext, RequestDelegate, ExceptionHandler, ILogger, IWebHostEnvironment, Task

### Community 88 - "20260719153341_MakeCategoryImagePathRequired.Designer.cs"
Cohesion: 0.05
Nodes (24): AgroShop.Core.Entities, AgroShop.Application.Services, AgroShop.Application.Dto.CategoryDto, AgroShop.Persistence.Extensions, AgroShop.Application.Jwt, AgroShop.Core.ValueObjects, AgroShop.Application.Responses, AgroShop.Application.Validators.CategoryValidators (+16 more)

### Community 92 - "RedisCacheService"
Cohesion: 0.15
Nodes (11): AgroShop.Infrastructure, AgroShop.Infrastructure.Caching, IConnectionMultiplexer, IDatabase, RedisCacheService, CancellationToken, Task, TimeSpan (+3 more)

### Community 93 - "ProductAttribute"
Cohesion: 0.36
Nodes (5): ProductAttribute, Guid, ICollection, ProductAttributeConfiguration, EntityTypeBuilder

### Community 94 - "ProductAttributeValue"
Cohesion: 0.43
Nodes (4): ProductAttributeValue, Guid, ProductAttributeValueConfiguration, EntityTypeBuilder

### Community 95 - "CategoryRepository"
Cohesion: 0.33
Nodes (5): CategoryRepository, CancellationToken, Guid, IEnumerable, Task

### Community 96 - ".GetCategoryByIdAsync"
Cohesion: 0.33
Nodes (5): ICategoryRepository, CancellationToken, Guid, IEnumerable, Task

### Community 97 - ".GetSubCategoryByIdAsync"
Cohesion: 0.39
Nodes (5): ISubCategoryRepository, CancellationToken, Guid, IEnumerable, Task

## Knowledge Gaps
- **39 isolated node(s):** `net10.0`, `FluentValidation.DependencyInjectionExtensions (12.1.1)`, `Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9)`, `Microsoft.AspNetCore.OpenApi (10.0.9)`, `Swashbuckle.AspNetCore (10.2.3)` (+34 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **26 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Error` connect `Domain Errors & Validation Results` to `Controllers, Envelope & Category Interface`, `Supplier Feature (Entity/Repo/Service)`, `SubCategory Feature (Entity/Repo/Service)`, `Category Feature (Entity/Repo/Service)`, `Auth Service, JWT Handler & User Repo`, `Auth & Category Validators (FluentValidation)`, `Core Repository Interfaces & Persistence Impls`, `Auth Controller Actions`, `Response Extensions & Validation Filter`, `Auth Cookie Service`, `User Service & Response`, `Password Value Object`, `ProductAttribute Entity & Configuration`, `User Repository Implementation`, `20260705114002_10.Designer.cs`, `Migration 1 - Initial (Up/Down)`, `ValueObject`, `Migration 3 Designer Snapshot`, `Migration 7 Designer Snapshot`, `Migration 9 Designer Snapshot`, `LastName`?**
  _High betweenness centrality (0.272) - this node is a cross-community bridge._
- **Why does `AgroShop.Persistence.Data` connect `20260719153341_MakeCategoryImagePathRequired.Designer.cs` to `20260127152943_5.Designer.cs`, `20260308131958_9.Designer.cs`, `20260719153341_MakeCategoryImagePathRequired.Designer.cs`, `Migration 1 Designer Snapshot`, `Migration 4 Designer Snapshot`, `Migration 5 Designer Snapshot`, `Migration 6 Designer Snapshot`, `Email`, `20251016175310_2.Designer.cs`, `20251026150053_3.Designer.cs`, `DbContext Model Snapshot (Migration 10)`, `AuthController HttpGet Attribute`, `AuthController Task Type`, `DependencyInjection String Type`, `FirstName Value Object`?**
  _High betweenness centrality (0.216) - this node is a cross-community bridge._
- **Why does `AgroShop.Core.Shared` connect `20260719153341_MakeCategoryImagePathRequired.Designer.cs` to `Domain Errors & Validation Results`, `Category Feature (Entity/Repo/Service)`, `Auth & Category Validators (FluentValidation)`, `User Service & Response`, `Email Value Object`?**
  _High betweenness centrality (0.178) - this node is a cross-community bridge._
- **What connects `net10.0`, `FluentValidation.DependencyInjectionExtensions (12.1.1)`, `Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9)` to the rest of the system?**
  _39 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Domain Errors & Validation Results` be split into smaller, more focused modules?**
  _Cohesion score 0.062456140350877196 - nodes in this community are weakly interconnected._
- **Should `Controllers, Envelope & Category Interface` be split into smaller, more focused modules?**
  _Cohesion score 0.12310606060606061 - nodes in this community are weakly interconnected._
- **Should `Supplier Feature (Entity/Repo/Service)` be split into smaller, more focused modules?**
  _Cohesion score 0.06818181818181818 - nodes in this community are weakly interconnected._