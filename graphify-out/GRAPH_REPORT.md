# Graph Report - AgroShop.API  (2026-07-25)

## Corpus Check
- 204 files · ~217,908 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 1043 nodes · 1867 edges · 115 communities (80 shown, 35 thin omitted)
- Extraction: 96% EXTRACTED · 4% INFERRED · 0% AMBIGUOUS · INFERRED: 71 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `66609bbd`
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
- .GenerateTokensAsync
- 20260127152943_5.Designer.cs
- 20260308131958_9.Designer.cs
- 20260719153341_MakeCategoryImagePathRequired.Designer.cs
- 20260307133355_8.Designer.cs
- 20260725202611_AlignCategoryNameMaxLengthWithDomain.Designer.cs
- RoleConstants.cs
- HttpDelete
- HttpGet
- HttpPost
- HttpPut
- IActionResult
- TimeSpan
- ICollection
- int

## God Nodes (most connected - your core abstractions)
1. `Error` - 49 edges
2. `AgroShop.Core.Entities` - 46 edges
3. `AgroShop.Core.Shared` - 34 edges
4. `AgroShop.Persistence.Data.Migrations` - 31 edges
5. `AgroShop.Core.ValueObjects` - 30 edges
6. `User` - 29 edges
7. `Supplier` - 26 edges
8. `SubCategory` - 25 edges
9. `Product` - 24 edges
10. `AgroShopDbContext` - 23 edges

## Surprising Connections (you probably didn't know these)
- `AddSubCategoryDtoValidator` --references--> `AddSubCategoryDto`  [EXTRACTED]
  src/AgroShop.Application/Validators/SubCategoryValidators/AddSubCategoryDtoValidator.cs → src/AgroShop.Application/Dto/SubCategoryDto/AddSubCategoryDto.cs
- `UpdateSubCategoryDtoValidator` --references--> `UpdateSubCategoryDto`  [EXTRACTED]
  src/AgroShop.Application/Validators/SubCategoryValidators/UpdateSubCategoryDtoValidator.cs → src/AgroShop.Application/Dto/SubCategoryDto/UpdateSubCategoryDto.cs
- `SubCategoryService` --implements--> `ISubCategoryService`  [EXTRACTED]
  src/AgroShop.Application/Services/SubCategoryService.cs → src/AgroShop.Application/Interfaces/ISubCategoryService.cs
- `Category` --references--> `SubCategory`  [EXTRACTED]
  src/AgroShop.Core/Entities/Category.cs → src/AgroShop.Core/Entities/SubCategory.cs
- `Product` --references--> `SubCategory`  [EXTRACTED]
  src/AgroShop.Core/Entities/Product.cs → src/AgroShop.Core/Entities/SubCategory.cs

## Import Cycles
- None detected.

## Communities (115 total, 35 thin omitted)

### Community 0 - "Domain Errors & Validation Results"
Cohesion: 0.05
Nodes (16): Authentication, Category, CategoryName, Email, Errors, FirstName, General, Image (+8 more)

### Community 1 - "Controllers, Envelope & Category Interface"
Cohesion: 0.06
Nodes (31): ActionResult, AgroShop.Core.Enums, ModelStateDictionary, IActionResult, Result, UnitResult, CategoryController, CancellationToken (+23 more)

### Community 2 - "Supplier Feature (Entity/Repo/Service)"
Cohesion: 0.07
Nodes (33): CancellationToken, Func, Guid, IEnumerable, IQueryable, Result, Task, CancellationToken (+25 more)

### Community 3 - "SubCategory Feature (Entity/Repo/Service)"
Cohesion: 0.06
Nodes (40): AgroShopDbContext, ICacheService, ICategoryRepository, ICollection, IUnitOfWork, Product, ProductAttribute, SubCategoryDto (+32 more)

### Community 4 - "Category Feature (Entity/Repo/Service)"
Cohesion: 0.06
Nodes (36): ICacheService, CancellationToken, Task, TimeSpan, IImageStorageService, CancellationToken, IFormFile, Task (+28 more)

### Community 5 - "Auth Service, JWT Handler & User Repo"
Cohesion: 0.26
Nodes (7): IConfigurationSection, JwtTokenHandler, CancellationToken, int, Result, Task, AuthResponse

### Community 6 - "Product Feature (Entity/Repo/Service)"
Cohesion: 0.13
Nodes (19): Product, Guid, ICollection, IProductRepository, CancellationToken, Expression, Func, Guid (+11 more)

### Community 7 - "Auth & Category Validators (FluentValidation)"
Cohesion: 0.22
Nodes (8): IRuleBuilder, IRuleBuilderOptions, IRuleBuilderOptionsConditions, ValidationExtensions, Func, IFormFile, Result, string

### Community 8 - "Role Entity & DbContext Infrastructure"
Cohesion: 0.20
Nodes (8): IRoleRepository, CancellationToken, Guid, Task, RoleRepository, CancellationToken, Guid, Task

### Community 9 - "JWT Token Handler & Refresh Tokens"
Cohesion: 0.43
Nodes (3): AgroShop.API.Responses, AgroShop.API.Extensions, AgroShop.API.Controllers

### Community 10 - "Project Structure & NuGet Dependencies"
Cohesion: 0.07
Nodes (25): CSharpFunctionalExtensions (3.7.0), FluentValidation (12.1.1), Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9), Microsoft.AspNetCore.OpenApi (10.0.9), Microsoft.EntityFrameworkCore (10.0.9), Microsoft.EntityFrameworkCore.Design (10.0.9), Microsoft.EntityFrameworkCore.Relational (10.0.9), Microsoft.Extensions.Configuration.EnvironmentVariables (10.0.9) (+17 more)

### Community 11 - "Dependency Injection & Program Entry"
Cohesion: 0.29
Nodes (4): IServiceProvider, DependencyInjection, IConfiguration, IServiceCollection

### Community 12 - "Launch Settings (Dev Config)"
Cohesion: 0.10
Nodes (21): applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, launchUrl, ASPNETCORE_ENVIRONMENT, commandName (+13 more)

### Community 13 - "Core Repository Interfaces & Persistence Impls"
Cohesion: 0.26
Nodes (7): PasswordHasher, AuthService, CancellationToken, ILogger, Result, Task, UnitResult

### Community 14 - "Auth Controller Actions"
Cohesion: 0.07
Nodes (32): AllowAnonymous, Authorize, ControllerBase, ApplicationController, AuthController, CancellationToken, HttpGet, HttpPost (+24 more)

### Community 15 - "Application Service Interfaces & Impls"
Cohesion: 0.13
Nodes (11): RefreshToken, DateTime, Guid, IRefreshTokenRepository, CancellationToken, Task, RefreshTokenConfiguration, EntityTypeBuilder (+3 more)

### Community 16 - "Response Extensions & Validation Filter"
Cohesion: 0.25
Nodes (5): CategoryName, IEnumerable, int, Result, ValueObject

### Community 17 - "Auth Cookie Service"
Cohesion: 0.25
Nodes (5): Phone, IEnumerable, int, Result, string

### Community 18 - "EF Entity Type Configurations"
Cohesion: 0.25
Nodes (4): AgroShop.Persistence, AgroShop.API, AgroShop.API.Middlewares, Program

### Community 19 - "Category, Subcategory & AttributeValue Entities"
Cohesion: 0.43
Nodes (4): DependencyInjection, IConfiguration, IServiceCollection, string

### Community 20 - "User Service & Response"
Cohesion: 0.22
Nodes (8): AgroShop.Application.Services, AgroShop.Application.Dto.CategoryDto, AgroShop.Application.Dto.SubCategoryDto, AgroShop.Core.Shared, AgroShop.Application.Mappers, AgroShop.Application.Interfaces, ISupplierService, SupplierService

### Community 21 - "Password Value Object"
Cohesion: 0.22
Nodes (5): Regex, Password, IEnumerable, int, Result

### Community 22 - "DbContext Model Snapshot (Migration 10)"
Cohesion: 0.14
Nodes (8): AgroShop.Persistence.Data.Migrations, ModelSnapshot, _5, ModelBuilder, ReworkSubCategoryNameAndUniqueness, ModelBuilder, AgroShopDbContextModelSnapshot, ModelBuilder

### Community 23 - "Attribute Entity & Configuration"
Cohesion: 0.44
Nodes (4): UserRepository, CancellationToken, Guid, Task

### Community 25 - "User Entity & Configuration"
Cohesion: 0.31
Nodes (6): CookieOptions, DateTimeOffset, AuthCookieService, HttpResponse, int, string

### Community 26 - "User Repository Implementation"
Cohesion: 0.12
Nodes (11): AgroShop.Persistence.Configurations, IEntityTypeConfiguration, CategoryConfiguration, Category, EntityTypeBuilder, ProductConfiguration, EntityTypeBuilder, SupplierConfiguration (+3 more)

### Community 27 - "Email Value Object"
Cohesion: 0.25
Nodes (6): ActionExecutingContext, ActionExecutionDelegate, AgroShop.API.Filters, IAsyncActionFilter, ValidationFilter, Task

### Community 28 - "Phone Value Object"
Cohesion: 0.29
Nodes (4): Role, Guid, RoleConfiguration, EntityTypeBuilder

### Community 29 - "FirstName Value Object"
Cohesion: 0.21
Nodes (7): DbContext, DbSet, IDesignTimeDbContextFactory, AgroShopDbContext, ModelBuilder, AgroShopDbContextFactory, IConfiguration

### Community 30 - "20260705114002_10.Designer.cs"
Cohesion: 0.25
Nodes (5): int, SubCategoryName, Error, IEnumerable, Result

### Community 31 - "SubCategory"
Cohesion: 0.17
Nodes (10): AbstractValidator, AgroShop.Application.Validators.CategoryValidators, AgroShop.Application.Validators.AuthenticationValidators, AgroShop.Application.Extensions, LoginUserDto, RegisterUserDto, LoginUserDtoValidator, RegisterUserDtoValidator (+2 more)

### Community 33 - "Migration 1 - Initial (Up/Down)"
Cohesion: 0.13
Nodes (20): ApplicationController, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult, SubCategoryController, CancellationToken (+12 more)

### Community 34 - "Migration 2 (Up/Down)"
Cohesion: 0.32
Nodes (5): ImageStorageService, CancellationToken, IFormFile, IWebHostEnvironment, Task

### Community 38 - "Migration 6 (Up/Down)"
Cohesion: 0.40
Nodes (3): Migration, _6, MigrationBuilder

### Community 43 - "Migration 1 Designer Snapshot"
Cohesion: 0.22
Nodes (5): AgroShop.Persistence.Data, _1, ModelBuilder, _9, ModelBuilder

### Community 44 - "ValueObject"
Cohesion: 0.29
Nodes (4): Email, IEnumerable, int, Result

### Community 45 - "Migration 3 Designer Snapshot"
Cohesion: 0.29
Nodes (4): FirstName, IEnumerable, int, Result

### Community 49 - "Migration 7 Designer Snapshot"
Cohesion: 0.29
Nodes (4): LastName, IEnumerable, int, Result

### Community 50 - "Migration 8 Designer Snapshot"
Cohesion: 0.25
Nodes (4): AgroShop.Application.Jwt, AgroShop.Application.Responses, AgroShop.Application.Dto.AuthDto, AgroShop.API.Services

### Community 51 - "Migration 9 Designer Snapshot"
Cohesion: 0.29
Nodes (4): Patronymic, IEnumerable, int, Result

### Community 54 - "LastName"
Cohesion: 0.42
Nodes (4): IUserRepository, CancellationToken, Guid, Task

### Community 56 - "AuthController HttpGet Attribute"
Cohesion: 0.24
Nodes (5): Attribute, Guid, ICollection, AttributeConfiguration, EntityTypeBuilder

### Community 57 - "ExceptionHandler"
Cohesion: 0.31
Nodes (7): Exception, HttpContext, RequestDelegate, ExceptionHandler, ILogger, IWebHostEnvironment, Task

### Community 88 - "20260719153341_MakeCategoryImagePathRequired.Designer.cs"
Cohesion: 0.24
Nodes (4): AgroShop.Core.Entities, AgroShop.Persistence.Extensions, AgroShop.Persistence.Repositories, AgroShop.Core.Interfaces

### Community 92 - "RedisCacheService"
Cohesion: 0.33
Nodes (6): IConnectionMultiplexer, IDatabase, RedisCacheService, CancellationToken, Task, TimeSpan

### Community 93 - "ProductAttribute"
Cohesion: 0.27
Nodes (5): ProductAttribute, Guid, ICollection, ProductAttributeConfiguration, EntityTypeBuilder

### Community 94 - "ProductAttributeValue"
Cohesion: 0.31
Nodes (4): ProductAttributeValue, Guid, ProductAttributeValueConfiguration, EntityTypeBuilder

### Community 95 - "CategoryRepository"
Cohesion: 0.25
Nodes (5): AgroShop.Infrastructure, AgroShop.Infrastructure.Caching, DependencyInjection, IConfiguration, IServiceCollection

### Community 96 - ".GetCategoryByIdAsync"
Cohesion: 0.29
Nodes (6): HashSet, User, DateTime, Guid, IReadOnlyCollection, Result

### Community 97 - ".GetSubCategoryByIdAsync"
Cohesion: 0.33
Nodes (4): AgroShop.Core.ValueObjects, AgroShop.Application.Validators.SubCategoryValidators, AddSubCategoryDtoValidator, UpdateSubCategoryDtoValidator

### Community 100 - ".GenerateTokensAsync"
Cohesion: 0.48
Nodes (4): IJwtTokenHandler, CancellationToken, Result, Task

### Community 101 - "20260127152943_5.Designer.cs"
Cohesion: 0.40
Nodes (3): AgroShop.Application, DependencyInjection, IServiceCollection

## Knowledge Gaps
- **39 isolated node(s):** `Errors`, `net10.0`, `FluentValidation.DependencyInjectionExtensions (12.1.1)`, `Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9)`, `Microsoft.AspNetCore.OpenApi (10.0.9)` (+34 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **35 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `AgroShop.Core.Shared` connect `User Service & Response` to `Domain Errors & Validation Results`, `Controllers, Envelope & Category Interface`, `SubCategory Feature (Entity/Repo/Service)`, `Category Feature (Entity/Repo/Service)`, `Auth & Category Validators (FluentValidation)`, `JWT Token Handler & Refresh Tokens`, `Dependency Injection & Program Entry`, `Response Extensions & Validation Filter`, `Auth Cookie Service`, `Password Value Object`, `User Repository Implementation`, `Phone Value Object`, `20260705114002_10.Designer.cs`, `SubCategory`, `ValueObject`, `Migration 3 Designer Snapshot`, `Migration 7 Designer Snapshot`, `Migration 8 Designer Snapshot`, `Migration 9 Designer Snapshot`, `.GetCategoryByIdAsync`, `RoleConstants.cs`?**
  _High betweenness centrality (0.236) - this node is a cross-community bridge._
- **Why does `AgroShop.Persistence.Data` connect `Migration 1 Designer Snapshot` to `20260719153341_MakeCategoryImagePathRequired.Designer.cs`, `20260307133355_8.Designer.cs`, `20260719153341_MakeCategoryImagePathRequired.Designer.cs`, `Dependency Injection & Program Entry`, `Migration 4 Designer Snapshot`, `Migration 5 Designer Snapshot`, `Migration 6 Designer Snapshot`, `Email`, `20251016175310_2.Designer.cs`, `20251026150053_3.Designer.cs`, `DbContext Model Snapshot (Migration 10)`, `User Repository Implementation`, `AuthController Task Type`, `DependencyInjection String Type`, `FirstName Value Object`?**
  _High betweenness centrality (0.222) - this node is a cross-community bridge._
- **Why does `AgroShop.Persistence.Data.Migrations` connect `DbContext Model Snapshot (Migration 10)` to `Migration 10 (Up/Down)`, `Migration 3 (Up/Down)`, `Migration 4 (Up/Down)`, `Role`, `Migration 6 (Up/Down)`, `Migration 7 (Up/Down)`, `Migration 8 (Up/Down)`, `Migration 9 (Up/Down)`, `Migration 11 (Up/Down)`, `Migration 1 Designer Snapshot`, `Migration 4 Designer Snapshot`, `Migration 5 Designer Snapshot`, `Migration 6 Designer Snapshot`, `Email`, `Paginated Result`, `MakeCategoryImagePathRequired`, `AuthController IActionResult Type`, `AuthController Task Type`, `DependencyInjection String Type`, `20251016175310_2.Designer.cs`, `20251026150053_3.Designer.cs`, `_10`, `AlignCategoryNameMaxLengthWithDomain`, `20260308131958_9.Designer.cs`, `20260719153341_MakeCategoryImagePathRequired.Designer.cs`, `20260307133355_8.Designer.cs`, `20260725202611_AlignCategoryNameMaxLengthWithDomain.Designer.cs`?**
  _High betweenness centrality (0.148) - this node is a cross-community bridge._
- **What connects `Errors`, `net10.0`, `FluentValidation.DependencyInjectionExtensions (12.1.1)` to the rest of the system?**
  _39 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Domain Errors & Validation Results` be split into smaller, more focused modules?**
  _Cohesion score 0.050724637681159424 - nodes in this community are weakly interconnected._
- **Should `Controllers, Envelope & Category Interface` be split into smaller, more focused modules?**
  _Cohesion score 0.06493506493506493 - nodes in this community are weakly interconnected._
- **Should `Supplier Feature (Entity/Repo/Service)` be split into smaller, more focused modules?**
  _Cohesion score 0.07239819004524888 - nodes in this community are weakly interconnected._