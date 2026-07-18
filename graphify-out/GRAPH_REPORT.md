# Graph Report - AgroShop.API  (2026-07-18)

## Corpus Check
- 171 files · ~37,421 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 933 nodes · 1573 edges · 82 communities (48 shown, 34 thin omitted)
- Extraction: 99% EXTRACTED · 1% INFERRED · 0% AMBIGUOUS · INFERRED: 19 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `b4b3687a`
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
- Migration 6 (Up/Down)
- Migration 7 (Up/Down)
- Migration 8 (Up/Down)
- Migration 9 (Up/Down)
- Migration 11 (Up/Down)
- Migration 1 Designer Snapshot
- Migration 3 Designer Snapshot
- Migration 4 Designer Snapshot
- Migration 5 Designer Snapshot
- Migration 6 Designer Snapshot
- Migration 7 Designer Snapshot
- Migration 8 Designer Snapshot
- Migration 9 Designer Snapshot
- Paginated Result
- AuthController CancellationToken Param
- AuthController HttpGet Attribute
- AuthController HttpPost Attribute
- AuthController IActionResult Type
- AuthController Task Type
- DependencyInjection String Type

## God Nodes (most connected - your core abstractions)
1. `Error` - 81 edges
2. `AgroShop.Core.Entities` - 46 edges
3. `User` - 30 edges
4. `SubCategory` - 28 edges
5. `Category` - 26 edges
6. `Supplier` - 26 edges
7. `Product` - 24 edges
8. `AgroShopDbContext` - 24 edges
9. `AgroShop.Core.Shared` - 23 edges
10. `AgroShop.Persistence.Data.Migrations` - 23 edges

## Surprising Connections (you probably didn't know these)
- `AgroShopDbContext` --references--> `Attribute`  [EXTRACTED]
  src/AgroShop.Persistence/Data/AgroShopDbContext.cs → src/AgroShop.Core/Entities/Attribute.cs
- `Category` --references--> `SubCategory`  [EXTRACTED]
  src/AgroShop.Core/Entities/Category.cs → src/AgroShop.Core/Entities/SubCategory.cs
- `AgroShopDbContext` --references--> `Category`  [EXTRACTED]
  src/AgroShop.Persistence/Data/AgroShopDbContext.cs → src/AgroShop.Core/Entities/Category.cs
- `Product` --references--> `ProductAttributeValue`  [EXTRACTED]
  src/AgroShop.Core/Entities/Product.cs → src/AgroShop.Core/Entities/ProductAttributeValue.cs
- `Product` --references--> `SubCategory`  [EXTRACTED]
  src/AgroShop.Core/Entities/Product.cs → src/AgroShop.Core/Entities/SubCategory.cs

## Import Cycles
- None detected.

## Communities (82 total, 34 thin omitted)

### Community 0 - "Domain Errors & Validation Results"
Cohesion: 0.07
Nodes (16): AgroShop.Core.Enums, ErrorType, Error, string, Authentication, Category, CategoryName, Email (+8 more)

### Community 1 - "Controllers, Envelope & Category Interface"
Cohesion: 0.07
Nodes (32): ActionExecutingContext, ActionExecutionDelegate, ControllerBase, AgroShop.Application.Dto.CategoryDto, AgroShop.API.Filters, AgroShop.API.Extensions, AgroShop.API.Controllers, HttpDelete (+24 more)

### Community 2 - "Supplier Feature (Entity/Repo/Service)"
Cohesion: 0.06
Nodes (37): ISupplierService, CancellationToken, Func, Guid, IEnumerable, IQueryable, Result, Task (+29 more)

### Community 3 - "SubCategory Feature (Entity/Repo/Service)"
Cohesion: 0.07
Nodes (37): ISubCategoryService, CancellationToken, Func, Guid, IEnumerable, IQueryable, Result, Task (+29 more)

### Community 4 - "Category Feature (Entity/Repo/Service)"
Cohesion: 0.06
Nodes (38): CategoryName, Func, ICategoryRepository, ICategoryService, IEntityTypeConfiguration, IEnumerable, IQueryable, IUnitOfWork (+30 more)

### Community 5 - "Auth Service, JWT Handler & User Repo"
Cohesion: 0.08
Nodes (25): AgroShop.Application.Dto.AuthDto, IConfigurationSection, LoginUserDto, RegisterUserDto, IAuthService, CancellationToken, Result, Task (+17 more)

### Community 6 - "Product Feature (Entity/Repo/Service)"
Cohesion: 0.09
Nodes (23): Product, Guid, ICollection, IProductRepository, CancellationToken, Expression, Func, Guid (+15 more)

### Community 7 - "Auth & Category Validators (FluentValidation)"
Cohesion: 0.24
Nodes (7): AgroShop.Application.Extensions, IRuleBuilder, IRuleBuilderOptions, IRuleBuilderOptionsConditions, ValidationExtensions, Func, Result

### Community 8 - "Role Entity & DbContext Infrastructure"
Cohesion: 0.06
Nodes (27): DbContext, DbSet, IDesignTimeDbContextFactory, RefreshToken, DateTime, Guid, Role, Guid (+19 more)

### Community 9 - "JWT Token Handler & Refresh Tokens"
Cohesion: 0.12
Nodes (14): AgroShop.API.Responses, AgroShop.API.Middlewares, Exception, HttpContext, IWebHostEnvironment, RequestDelegate, ExceptionHandler, ILogger (+6 more)

### Community 10 - "Project Structure & NuGet Dependencies"
Cohesion: 0.08
Nodes (22): CSharpFunctionalExtensions (3.7.0), FluentValidation (12.1.1), Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9), Microsoft.AspNetCore.OpenApi (10.0.9), Microsoft.EntityFrameworkCore (10.0.9), Microsoft.EntityFrameworkCore.Design (10.0.9), Microsoft.EntityFrameworkCore.Relational (10.0.9), Microsoft.Extensions.Configuration.EnvironmentVariables (10.0.9) (+14 more)

### Community 11 - "Dependency Injection & Program Entry"
Cohesion: 0.06
Nodes (25): AbstractValidator, AgroShop.Persistence, AgroShop.Application, AgroShop.Application.Validators.CategoryValidators, AgroShop.API, AgroShop.Application.Validators.AuthenticationValidators, IConfiguration, IServiceProvider (+17 more)

### Community 12 - "Launch Settings (Dev Config)"
Cohesion: 0.10
Nodes (21): applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, launchUrl, ASPNETCORE_ENVIRONMENT, commandName (+13 more)

### Community 13 - "Core Repository Interfaces & Persistence Impls"
Cohesion: 0.18
Nodes (14): AuthResponse, IJwtTokenHandler, ILogger, IRoleRepository, PasswordHasher, AuthService, CancellationToken, Error (+6 more)

### Community 14 - "Auth Controller Actions"
Cohesion: 0.09
Nodes (26): AllowAnonymous, ApplicationController, Authorize, CancellationToken, HttpGet, HttpPost, IActionResult, IAuthCookieService (+18 more)

### Community 15 - "Application Service Interfaces & Impls"
Cohesion: 0.05
Nodes (25): AgroShop.Core.Entities, AgroShop.Application.Services, AgroShop.Application.Jwt, AgroShop.Core.ValueObjects, AgroShop.Application.Responses, AgroShop.Core.Shared, AgroShop.Persistence.Configurations, AgroShop.API.Services (+17 more)

### Community 16 - "Response Extensions & Validation Filter"
Cohesion: 0.29
Nodes (5): ActionResult, ModelStateDictionary, ResponseExtensions, Error, ValidationResult

### Community 17 - "Auth Cookie Service"
Cohesion: 0.21
Nodes (8): CookieOptions, DateTimeOffset, AuthCookieService, HttpResponse, int, string, IAuthCookieService, HttpResponse

### Community 21 - "Password Value Object"
Cohesion: 0.04
Nodes (31): Regex, CategoryName, IEnumerable, int, Result, Email, IEnumerable, int (+23 more)

### Community 22 - "DbContext Model Snapshot (Migration 10)"
Cohesion: 0.14
Nodes (8): AgroShop.Persistence.Data.Migrations, ModelSnapshot, _2, ModelBuilder, _11, ModelBuilder, AgroShopDbContextModelSnapshot, ModelBuilder

### Community 24 - "ProductAttribute Entity & Configuration"
Cohesion: 0.41
Nodes (4): AgroShop.Persistence.Extensions, AgroShop.Persistence.Repositories, AgroShop.Core.Interfaces, AgroShop.Persistence.Data

### Community 26 - "User Repository Implementation"
Cohesion: 0.08
Nodes (24): DateTime, Email, FirstName, HashSet, LastName, Patronymic, Phone, RefreshToken (+16 more)

### Community 34 - "Migration 2 (Up/Down)"
Cohesion: 0.22
Nodes (5): Migration, _2, MigrationBuilder, _5, MigrationBuilder

## Knowledge Gaps
- **39 isolated node(s):** `net10.0`, `FluentValidation.DependencyInjectionExtensions (12.1.1)`, `Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9)`, `Microsoft.AspNetCore.OpenApi (10.0.9)`, `Swashbuckle.AspNetCore (10.2.3)` (+34 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **34 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Error` connect `Domain Errors & Validation Results` to `Controllers, Envelope & Category Interface`, `Supplier Feature (Entity/Repo/Service)`, `SubCategory Feature (Entity/Repo/Service)`, `Auth Service, JWT Handler & User Repo`, `Auth & Category Validators (FluentValidation)`, `Auth Controller Actions`, `Password Value Object`?**
  _High betweenness centrality (0.214) - this node is a cross-community bridge._
- **Why does `AgroShop.Persistence.Data` connect `ProductAttribute Entity & Configuration` to `Role Entity & DbContext Infrastructure`, `Migration 1 Designer Snapshot`, `Migration 3 Designer Snapshot`, `Migration 4 Designer Snapshot`, `Application Service Interfaces & Impls`, `Migration 5 Designer Snapshot`, `Migration 6 Designer Snapshot`, `Migration 7 Designer Snapshot`, `Migration 8 Designer Snapshot`, `Migration 9 Designer Snapshot`, `DbContext Model Snapshot (Migration 10)`, `20260705114002_10.Designer.cs`?**
  _High betweenness centrality (0.177) - this node is a cross-community bridge._
- **Why does `AgroShopDbContext` connect `Role Entity & DbContext Infrastructure` to `Supplier Feature (Entity/Repo/Service)`, `SubCategory Feature (Entity/Repo/Service)`, `Category Feature (Entity/Repo/Service)`, `Auth Service, JWT Handler & User Repo`, `Product Feature (Entity/Repo/Service)`, `Application Service Interfaces & Impls`, `User Repository Implementation`?**
  _High betweenness centrality (0.155) - this node is a cross-community bridge._
- **What connects `net10.0`, `FluentValidation.DependencyInjectionExtensions (12.1.1)`, `Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9)` to the rest of the system?**
  _39 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Domain Errors & Validation Results` be split into smaller, more focused modules?**
  _Cohesion score 0.06564364876385337 - nodes in this community are weakly interconnected._
- **Should `Controllers, Envelope & Category Interface` be split into smaller, more focused modules?**
  _Cohesion score 0.07183673469387755 - nodes in this community are weakly interconnected._
- **Should `Supplier Feature (Entity/Repo/Service)` be split into smaller, more focused modules?**
  _Cohesion score 0.0647307924984876 - nodes in this community are weakly interconnected._