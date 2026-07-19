# Graph Report - AgroShop.API  (2026-07-19)

## Corpus Check
- 179 files · ~124,315 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 947 nodes · 1649 edges · 95 communities (61 shown, 34 thin omitted)
- Extraction: 98% EXTRACTED · 2% INFERRED · 0% AMBIGUOUS · INFERRED: 38 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `d90c6b4f`
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
- IQueryable
- UnitResult
- Guid
- IReadOnlyCollection
- List
- EntityTypeBuilder

## God Nodes (most connected - your core abstractions)
1. `AgroShop.Core.Entities` - 46 edges
2. `Error` - 45 edges
3. `AgroShop.Core.Shared` - 32 edges
4. `User` - 29 edges
5. `AgroShop.Persistence.Data.Migrations` - 27 edges
6. `SubCategory` - 27 edges
7. `Category` - 26 edges
8. `Supplier` - 26 edges
9. `AgroShop.Core.ValueObjects` - 25 edges
10. `Product` - 24 edges

## Surprising Connections (you probably didn't know these)
- `ImageStorageService` --implements--> `IImageStorageService`  [EXTRACTED]
  src/AgroShop.API/Services/ImageStorageService.cs → src/AgroShop.Application/Interfaces/IImageStorageService.cs
- `AgroShopDbContext` --references--> `Category`  [EXTRACTED]
  src/AgroShop.Persistence/Data/AgroShopDbContext.cs → src/AgroShop.Core/Entities/Category.cs
- `AuthCookieService` --implements--> `IAuthCookieService`  [EXTRACTED]
  src/AgroShop.API/Services/AuthCookieService.cs → src/AgroShop.API/Services/IAuthCookieService.cs
- `AuthService` --implements--> `IAuthService`  [EXTRACTED]
  src/AgroShop.Application/Services/AuthService.cs → src/AgroShop.Application/Interfaces/IAuthService.cs
- `AuthService` --references--> `IRoleRepository`  [EXTRACTED]
  src/AgroShop.Application/Services/AuthService.cs → src/AgroShop.Core/Interfaces/IRoleRepository.cs

## Import Cycles
- None detected.

## Communities (95 total, 34 thin omitted)

### Community 0 - "Domain Errors & Validation Results"
Cohesion: 0.06
Nodes (14): Authentication, Category, CategoryName, Email, Errors, FirstName, General, Image (+6 more)

### Community 1 - "Controllers, Envelope & Category Interface"
Cohesion: 0.29
Nodes (8): ICategoryService, CancellationToken, Func, IEnumerable, IQueryable, Result, Task, UnitResult

### Community 2 - "Supplier Feature (Entity/Repo/Service)"
Cohesion: 0.07
Nodes (37): ISupplierService, CancellationToken, Func, Guid, IEnumerable, IQueryable, Result, Task (+29 more)

### Community 3 - "SubCategory Feature (Entity/Repo/Service)"
Cohesion: 0.07
Nodes (37): ISubCategoryService, CancellationToken, Func, Guid, IEnumerable, IQueryable, Result, Task (+29 more)

### Community 4 - "Category Feature (Entity/Repo/Service)"
Cohesion: 0.05
Nodes (43): CategoryName, EntityTypeBuilder, Guid, ICategoryRepository, ICategoryService, IEnumerable, IQueryable, IReadOnlyCollection (+35 more)

### Community 5 - "Auth Service, JWT Handler & User Repo"
Cohesion: 0.18
Nodes (8): RefreshToken, DateTime, Guid, RefreshTokenConfiguration, EntityTypeBuilder, RefreshTokenRepository, CancellationToken, Task

### Community 6 - "Product Feature (Entity/Repo/Service)"
Cohesion: 0.11
Nodes (21): Product, Guid, ICollection, IProductRepository, CancellationToken, Expression, Func, Guid (+13 more)

### Community 7 - "Auth & Category Validators (FluentValidation)"
Cohesion: 0.22
Nodes (9): IRuleBuilder, IRuleBuilderOptions, IRuleBuilderOptionsConditions, ValidationExtensions, Error, Func, IFormFile, Result (+1 more)

### Community 8 - "Role Entity & DbContext Infrastructure"
Cohesion: 0.12
Nodes (12): Role, Guid, IRoleRepository, CancellationToken, Guid, Task, RoleConfiguration, EntityTypeBuilder (+4 more)

### Community 9 - "JWT Token Handler & Refresh Tokens"
Cohesion: 0.08
Nodes (22): ActionExecutingContext, ActionExecutionDelegate, AgroShop.API.Filters, AgroShop.API.Responses, AgroShop.API.Extensions, AgroShop.API.Controllers, AgroShop.API.Middlewares, Exception (+14 more)

### Community 10 - "Project Structure & NuGet Dependencies"
Cohesion: 0.08
Nodes (22): CSharpFunctionalExtensions (3.7.0), FluentValidation (12.1.1), Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9), Microsoft.AspNetCore.OpenApi (10.0.9), Microsoft.EntityFrameworkCore (10.0.9), Microsoft.EntityFrameworkCore.Design (10.0.9), Microsoft.EntityFrameworkCore.Relational (10.0.9), Microsoft.Extensions.Configuration.EnvironmentVariables (10.0.9) (+14 more)

### Community 11 - "Dependency Injection & Program Entry"
Cohesion: 0.33
Nodes (4): IServiceProvider, DependencyInjection, IConfiguration, IServiceCollection

### Community 12 - "Launch Settings (Dev Config)"
Cohesion: 0.10
Nodes (21): applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, launchUrl, ASPNETCORE_ENVIRONMENT, commandName (+13 more)

### Community 13 - "Core Repository Interfaces & Persistence Impls"
Cohesion: 0.07
Nodes (28): IConfigurationSection, PasswordHasher, IJwtTokenHandler, CancellationToken, Result, Task, JwtTokenHandler, CancellationToken (+20 more)

### Community 14 - "Auth Controller Actions"
Cohesion: 0.09
Nodes (30): AllowAnonymous, Authorize, ControllerBase, ApplicationController, IActionResult, Result, UnitResult, AuthController (+22 more)

### Community 15 - "Application Service Interfaces & Impls"
Cohesion: 0.05
Nodes (29): AbstractValidator, AgroShop.Core.Entities, AgroShop.Application.Services, AgroShop.Application.Dto.CategoryDto, AgroShop.Application.Jwt, AgroShop.Core.ValueObjects, AgroShop.Application, AgroShop.Application.Responses (+21 more)

### Community 16 - "Response Extensions & Validation Filter"
Cohesion: 0.29
Nodes (5): CategoryName, IEnumerable, int, Result, ValueObject

### Community 17 - "Auth Cookie Service"
Cohesion: 0.29
Nodes (5): Phone, IEnumerable, int, Result, string

### Community 18 - "EF Entity Type Configurations"
Cohesion: 0.26
Nodes (10): ApplicationController, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult, CategoryController, CancellationToken (+2 more)

### Community 20 - "User Service & Response"
Cohesion: 0.18
Nodes (4): AgroShop.Core.Enums, ErrorType, Error, string

### Community 21 - "Password Value Object"
Cohesion: 0.24
Nodes (5): Regex, Password, IEnumerable, int, Result

### Community 22 - "DbContext Model Snapshot (Migration 10)"
Cohesion: 0.11
Nodes (10): AgroShop.Persistence.Data.Migrations, ModelSnapshot, _9, ModelBuilder, _10, ModelBuilder, MakeCategoryImagePathRequired, ModelBuilder (+2 more)

### Community 23 - "Attribute Entity & Configuration"
Cohesion: 0.44
Nodes (4): UserRepository, CancellationToken, Guid, Task

### Community 25 - "User Entity & Configuration"
Cohesion: 0.25
Nodes (6): IEntityTypeConfiguration, Attribute, Guid, ICollection, AttributeConfiguration, EntityTypeBuilder

### Community 26 - "User Repository Implementation"
Cohesion: 0.24
Nodes (8): HashSet, User, DateTime, Guid, IReadOnlyCollection, Result, UserConfiguration, EntityTypeBuilder

### Community 27 - "Email Value Object"
Cohesion: 0.36
Nodes (5): ProductAttribute, Guid, ICollection, ProductAttributeConfiguration, EntityTypeBuilder

### Community 29 - "FirstName Value Object"
Cohesion: 0.21
Nodes (7): DbContext, DbSet, IDesignTimeDbContextFactory, AgroShopDbContext, ModelBuilder, AgroShopDbContextFactory, IConfiguration

### Community 31 - "SubCategory"
Cohesion: 0.43
Nodes (4): ProductAttributeValue, Guid, ProductAttributeValueConfiguration, EntityTypeBuilder

### Community 40 - "Migration 8 (Up/Down)"
Cohesion: 0.22
Nodes (5): Migration, _5, MigrationBuilder, _8, MigrationBuilder

### Community 44 - "ValueObject"
Cohesion: 0.33
Nodes (4): Email, IEnumerable, int, Result

### Community 45 - "Migration 3 Designer Snapshot"
Cohesion: 0.33
Nodes (4): FirstName, IEnumerable, int, Result

### Community 49 - "Migration 7 Designer Snapshot"
Cohesion: 0.33
Nodes (4): LastName, IEnumerable, int, Result

### Community 51 - "Migration 9 Designer Snapshot"
Cohesion: 0.33
Nodes (4): Patronymic, IEnumerable, int, Result

### Community 52 - "Email"
Cohesion: 0.29
Nodes (4): ActionResult, ModelStateDictionary, ResponseExtensions, ValidationResult

### Community 53 - "Paginated Result"
Cohesion: 0.38
Nodes (5): AgroShop.Persistence, AgroShop.Persistence.Extensions, AgroShop.Persistence.Repositories, AgroShop.Core.Interfaces, AgroShop.Persistence.Data

### Community 54 - "LastName"
Cohesion: 0.08
Nodes (18): CookieOptions, AgroShop.API, AgroShop.API.Services, DateTimeOffset, IConfiguration, IServiceCollection, IWebHostEnvironment, DependencyInjection (+10 more)

## Knowledge Gaps
- **40 isolated node(s):** `Errors`, `net10.0`, `FluentValidation.DependencyInjectionExtensions (12.1.1)`, `Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9)`, `Microsoft.AspNetCore.OpenApi (10.0.9)` (+35 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **34 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `AgroShop.Core.Shared` connect `Application Service Interfaces & Impls` to `Domain Errors & Validation Results`, `JWT Token Handler & Refresh Tokens`, `Core Repository Interfaces & Persistence Impls`, `Email`, `User Service & Response`, `Paginated Result`?**
  _High betweenness centrality (0.238) - this node is a cross-community bridge._
- **Why does `AgroShop.Persistence.Data` connect `Paginated Result` to `Migration 1 Designer Snapshot`, `Migration 4 Designer Snapshot`, `Application Service Interfaces & Impls`, `Migration 5 Designer Snapshot`, `Migration 6 Designer Snapshot`, `Migration 8 Designer Snapshot`, `20251016175310_2.Designer.cs`, `20251026150053_3.Designer.cs`, `DbContext Model Snapshot (Migration 10)`, `AuthController Task Type`, `Phone Value Object`, `FirstName Value Object`?**
  _High betweenness centrality (0.208) - this node is a cross-community bridge._
- **Why does `AgroShopDbContext` connect `FirstName Value Object` to `Supplier Feature (Entity/Repo/Service)`, `SubCategory Feature (Entity/Repo/Service)`, `Category Feature (Entity/Repo/Service)`, `Auth Service, JWT Handler & User Repo`, `Product Feature (Entity/Repo/Service)`, `Role Entity & DbContext Infrastructure`, `Core Repository Interfaces & Persistence Impls`, `Application Service Interfaces & Impls`, `Attribute Entity & Configuration`, `User Entity & Configuration`, `User Repository Implementation`, `Email Value Object`, `SubCategory`?**
  _High betweenness centrality (0.168) - this node is a cross-community bridge._
- **What connects `Errors`, `net10.0`, `FluentValidation.DependencyInjectionExtensions (12.1.1)` to the rest of the system?**
  _40 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Domain Errors & Validation Results` be split into smaller, more focused modules?**
  _Cohesion score 0.05819209039548023 - nodes in this community are weakly interconnected._
- **Should `Supplier Feature (Entity/Repo/Service)` be split into smaller, more focused modules?**
  _Cohesion score 0.06641604010025062 - nodes in this community are weakly interconnected._
- **Should `SubCategory Feature (Entity/Repo/Service)` be split into smaller, more focused modules?**
  _Cohesion score 0.06704260651629072 - nodes in this community are weakly interconnected._