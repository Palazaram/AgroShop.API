# Graph Report - AgroShop.API  (2026-07-26)

## Corpus Check
- 234 files · ~229,725 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 1206 nodes · 2379 edges · 135 communities (91 shown, 44 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 137 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `17fcd05a`
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
- Phone
- SupplierName
- 20260127152943_5.Designer.cs
- 20260308131958_9.Designer.cs
- 20260725201921_ReworkSubCategoryNameAndUniqueness.Designer.cs
- 20260726111737_ReworkProductAndSupplier.Designer.cs
- Image
- LastName
- Password
- StockQuantity
- _2
- MakeProductDescriptionRequired
- 20251026152202_4.Designer.cs
- .AddInfrastructure
- 20260222150331_7.Designer.cs
- 20260711114130_11.Designer.cs
- 20260726130523_AddPackageSizeToProduct.Designer.cs
- Product
- RoleConstants.cs
- .GetRefreshTokenByHashAsync

## God Nodes (most connected - your core abstractions)
1. `Error` - 163 edges
2. `AgroShop.Core.Entities` - 48 edges
3. `AgroShop.Core.Shared` - 47 edges
4. `AgroShop.Core.ValueObjects` - 37 edges
5. `AgroShop.Persistence.Data.Migrations` - 37 edges
6. `User` - 29 edges
7. `AgroShop.Persistence.Data` - 29 edges
8. `Product` - 28 edges
9. `AgroShop.Application.Interfaces` - 24 edges
10. `AgroShopDbContext` - 24 edges

## Surprising Connections (you probably didn't know these)
- `AuthController` --inherits--> `ApplicationController`  [EXTRACTED]
  src/AgroShop.API/Controllers/AuthController.cs → src/AgroShop.API/Controllers/ApplicationController.cs
- `SubCategoryController` --inherits--> `ApplicationController`  [EXTRACTED]
  src/AgroShop.API/Controllers/SubCategoryController.cs → src/AgroShop.API/Controllers/ApplicationController.cs
- `SupplierController` --inherits--> `ApplicationController`  [EXTRACTED]
  src/AgroShop.API/Controllers/SupplierController.cs → src/AgroShop.API/Controllers/ApplicationController.cs
- `ImageStorageService` --implements--> `IImageStorageService`  [EXTRACTED]
  src/AgroShop.API/Services/ImageStorageService.cs → src/AgroShop.Application/Interfaces/IImageStorageService.cs
- `AddProductDto` --references--> `PackageUnit`  [EXTRACTED]
  src/AgroShop.Application/Dto/ProductDto/AddProductDto.cs → src/AgroShop.Core/Enums/PackageUnit.cs

## Import Cycles
- None detected.

## Communities (135 total, 44 thin omitted)

### Community 0 - "Domain Errors & Validation Results"
Cohesion: 0.19
Nodes (3): ProductDescription, ProductName, SubCategoryName

### Community 1 - "Controllers, Envelope & Category Interface"
Cohesion: 0.06
Nodes (47): ControllerBase, ApplicationController, IActionResult, Result, UnitResult, CategoryController, CancellationToken, HttpDelete (+39 more)

### Community 2 - "Supplier Feature (Entity/Repo/Service)"
Cohesion: 0.33
Nodes (4): SupplierName, IEnumerable, int, Result

### Community 3 - "SubCategory Feature (Entity/Repo/Service)"
Cohesion: 0.16
Nodes (16): SubCategoryDto, Guid, SubCategoryService, CancellationToken, Guid, IEnumerable, Result, string (+8 more)

### Community 4 - "Category Feature (Entity/Repo/Service)"
Cohesion: 0.18
Nodes (13): CategoryService, CancellationToken, IEnumerable, Result, string, Task, TimeSpan, UnitResult (+5 more)

### Community 5 - "Auth Service, JWT Handler & User Repo"
Cohesion: 0.14
Nodes (18): SupplierController, CancellationToken, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult, Task (+10 more)

### Community 6 - "Product Feature (Entity/Repo/Service)"
Cohesion: 0.25
Nodes (7): IQueryable, Product, DateTime, ICollection, ProductConfiguration, EntityTypeBuilder, QueryableExtensions

### Community 7 - "Auth & Category Validators (FluentValidation)"
Cohesion: 0.24
Nodes (8): Func, IRuleBuilder, IRuleBuilderOptions, IRuleBuilderOptionsConditions, ValidationExtensions, IFormFile, Result, string

### Community 8 - "Role Entity & DbContext Infrastructure"
Cohesion: 0.13
Nodes (12): Role, Guid, IRoleRepository, CancellationToken, Guid, Task, RoleConfiguration, EntityTypeBuilder (+4 more)

### Community 9 - "JWT Token Handler & Refresh Tokens"
Cohesion: 0.33
Nodes (5): ProductRepository, CancellationToken, Guid, IEnumerable, Task

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
Cohesion: 0.07
Nodes (32): AllowAnonymous, Authorize, CookieOptions, DateTimeOffset, AuthController, CancellationToken, HttpGet, HttpPost (+24 more)

### Community 15 - "Application Service Interfaces & Impls"
Cohesion: 0.21
Nodes (10): IConfigurationSection, JwtTokenHandler, CancellationToken, int, Result, Task, IRefreshTokenRepository, CancellationToken (+2 more)

### Community 16 - "Response Extensions & Validation Filter"
Cohesion: 0.40
Nodes (3): Money, IEnumerable, Result

### Community 17 - "Auth Cookie Service"
Cohesion: 0.29
Nodes (5): Phone, IEnumerable, int, Result, string

### Community 18 - "EF Entity Type Configurations"
Cohesion: 0.14
Nodes (3): AgroShop.Core.ValueObjects, AgroShop.Core.Shared, AgroShop.Core.Enums

### Community 19 - "Category, Subcategory & AttributeValue Entities"
Cohesion: 0.43
Nodes (4): DependencyInjection, IConfiguration, IServiceCollection, string

### Community 20 - "User Service & Response"
Cohesion: 0.15
Nodes (8): AgroShop.Application.Services, AgroShop.Application.Dto.CategoryDto, AgroShop.Application.Dto.SubCategoryDto, AgroShop.Application.Dto.ProductDto, AgroShop.Application.Mappers, AgroShop.Application.Dto.SupplierDto, AgroShop.API.Controllers, AgroShop.Application.Interfaces

### Community 21 - "Password Value Object"
Cohesion: 0.24
Nodes (5): Regex, Password, IEnumerable, int, Result

### Community 22 - "DbContext Model Snapshot (Migration 10)"
Cohesion: 0.14
Nodes (8): AgroShop.Persistence.Data.Migrations, ModelSnapshot, _7, ModelBuilder, ReworkSubCategoryNameAndUniqueness, ModelBuilder, AgroShopDbContextModelSnapshot, ModelBuilder

### Community 23 - "Attribute Entity & Configuration"
Cohesion: 0.44
Nodes (4): UserRepository, CancellationToken, Guid, Task

### Community 24 - "ProductAttribute Entity & Configuration"
Cohesion: 0.13
Nodes (13): HashSet, RefreshToken, DateTime, Guid, User, DateTime, Guid, IReadOnlyCollection (+5 more)

### Community 25 - "User Entity & Configuration"
Cohesion: 0.25
Nodes (4): AgroShop.API.Filters, AgroShop.API.Responses, AgroShop.API.Extensions, AgroShop.API.Services

### Community 26 - "User Repository Implementation"
Cohesion: 0.13
Nodes (14): CategoryMapper, IEnumerable, Category, Guid, IReadOnlyCollection, List, Result, CategoryConfiguration (+6 more)

### Community 27 - "Email Value Object"
Cohesion: 0.33
Nodes (5): ActionExecutingContext, ActionExecutionDelegate, IAsyncActionFilter, ValidationFilter, Task

### Community 28 - "Phone Value Object"
Cohesion: 0.15
Nodes (16): ProductDto, DateTime, Guid, ProductService, CancellationToken, IEnumerable, Result, string (+8 more)

### Community 29 - "FirstName Value Object"
Cohesion: 0.21
Nodes (7): DbContext, DbSet, IDesignTimeDbContextFactory, AgroShopDbContext, ModelBuilder, AgroShopDbContextFactory, IConfiguration

### Community 30 - "20260705114002_10.Designer.cs"
Cohesion: 0.27
Nodes (6): Guid, Result, PackageUnit, PackageSize, IEnumerable, Result

### Community 31 - "SubCategory"
Cohesion: 0.10
Nodes (25): AbstractValidator, SubCategoryController, CancellationToken, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+17 more)

### Community 34 - "Migration 2 (Up/Down)"
Cohesion: 0.32
Nodes (5): ImageStorageService, CancellationToken, IFormFile, IWebHostEnvironment, Task

### Community 44 - "ValueObject"
Cohesion: 0.33
Nodes (4): Email, IEnumerable, int, Result

### Community 45 - "Migration 3 Designer Snapshot"
Cohesion: 0.33
Nodes (4): FirstName, IEnumerable, int, Result

### Community 46 - "Migration 4 Designer Snapshot"
Cohesion: 0.22
Nodes (5): Migration, _1, MigrationBuilder, AlignCategoryNameMaxLengthWithDomain, MigrationBuilder

### Community 47 - "Migration 5 Designer Snapshot"
Cohesion: 0.10
Nodes (9): Category, Errors, Image, Money, PackageSize, StockQuantity, SubCategory, Supplier (+1 more)

### Community 49 - "Migration 7 Designer Snapshot"
Cohesion: 0.33
Nodes (4): LastName, IEnumerable, int, Result

### Community 50 - "Migration 8 Designer Snapshot"
Cohesion: 0.23
Nodes (4): AgroShop.Application.Jwt, AgroShop.Application.Responses, AgroShop.Application.Dto.AuthDto, AgroShop.Application.Validators.AuthenticationValidators

### Community 51 - "Migration 9 Designer Snapshot"
Cohesion: 0.33
Nodes (4): Patronymic, IEnumerable, int, Result

### Community 53 - "Paginated Result"
Cohesion: 0.08
Nodes (28): SupplierDto, Guid, SupplierMapper, IEnumerable, SupplierService, CancellationToken, IEnumerable, Result (+20 more)

### Community 54 - "LastName"
Cohesion: 0.33
Nodes (5): SubCategoryRepository, CancellationToken, Guid, IEnumerable, Task

### Community 55 - "MakeCategoryImagePathRequired"
Cohesion: 0.29
Nodes (6): UserResponse, Guid, UserService, CancellationToken, Result, Task

### Community 56 - "AuthController HttpGet Attribute"
Cohesion: 0.25
Nodes (6): IEntityTypeConfiguration, Attribute, Guid, ICollection, AttributeConfiguration, EntityTypeBuilder

### Community 57 - "ExceptionHandler"
Cohesion: 0.07
Nodes (23): AgroShop.Persistence, AgroShop.Infrastructure, AgroShop.Application, AgroShop.API, AgroShop.Infrastructure.Caching, AgroShop.API.Middlewares, Exception, HttpContext (+15 more)

### Community 59 - "AuthController Task Type"
Cohesion: 0.13
Nodes (6): ErrorType, Error, string, FirstName, General, Sku

### Community 88 - "20260719153341_MakeCategoryImagePathRequired.Designer.cs"
Cohesion: 0.12
Nodes (6): AgroShop.Core.Entities, AgroShop.Persistence.Extensions, AgroShop.Persistence.Configurations, AgroShop.Persistence.Repositories, AgroShop.Core.Interfaces, AgroShop.Persistence.Data

### Community 92 - "RedisCacheService"
Cohesion: 0.33
Nodes (4): ActionResult, ModelStateDictionary, ResponseExtensions, ValidationResult

### Community 93 - "ProductAttribute"
Cohesion: 0.36
Nodes (5): ProductAttribute, Guid, ICollection, ProductAttributeConfiguration, EntityTypeBuilder

### Community 94 - "ProductAttributeValue"
Cohesion: 0.43
Nodes (4): ProductAttributeValue, Guid, ProductAttributeValueConfiguration, EntityTypeBuilder

### Community 95 - "CategoryRepository"
Cohesion: 0.38
Nodes (4): IImageStorageService, CancellationToken, IFormFile, Task

### Community 96 - ".GetCategoryByIdAsync"
Cohesion: 0.44
Nodes (4): IUserRepository, CancellationToken, Guid, Task

### Community 97 - ".GetSubCategoryByIdAsync"
Cohesion: 0.24
Nodes (4): AgroShop.Application.Validators.SupplierValidators, AgroShop.Application.Validators.ProductValidators, AgroShop.Application.Validators.SubCategoryValidators, AgroShop.Application.Extensions

### Community 99 - "AlignCategoryNameMaxLengthWithDomain"
Cohesion: 0.29
Nodes (5): CategoryName, IEnumerable, int, Result, ValueObject

### Community 100 - ".GenerateTokensAsync"
Cohesion: 0.39
Nodes (4): ICacheService, CancellationToken, Task, TimeSpan

### Community 106 - "RoleConstants.cs"
Cohesion: 0.33
Nodes (4): SubCategoryName, IEnumerable, int, Result

### Community 108 - "HttpGet"
Cohesion: 0.40
Nodes (3): IUnitOfWork, CancellationToken, Task

### Community 109 - "HttpPost"
Cohesion: 0.33
Nodes (4): ProductDescription, IEnumerable, int, Result

### Community 110 - "HttpPut"
Cohesion: 0.33
Nodes (4): ProductName, IEnumerable, int, Result

### Community 113 - "ICollection"
Cohesion: 0.21
Nodes (8): SubCategoryMapper, IEnumerable, SubCategory, Guid, ICollection, Result, SubCategoryConfiguration, EntityTypeBuilder

### Community 115 - "Phone"
Cohesion: 0.33
Nodes (3): AgroShop.Application.Validators.CategoryValidators, DependencyInjection, IServiceCollection

### Community 120 - "20260726111737_ReworkProductAndSupplier.Designer.cs"
Cohesion: 0.33
Nodes (4): Sku, IEnumerable, int, Result

### Community 124 - "StockQuantity"
Cohesion: 0.40
Nodes (3): StockQuantity, IEnumerable, Result

## Knowledge Gaps
- **39 isolated node(s):** `net10.0`, `FluentValidation.DependencyInjectionExtensions (12.1.1)`, `Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9)`, `Microsoft.AspNetCore.OpenApi (10.0.9)`, `Swashbuckle.AspNetCore (10.2.3)` (+34 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **44 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Error` connect `AuthController Task Type` to `Domain Errors & Validation Results`, `Controllers, Envelope & Category Interface`, `Supplier Feature (Entity/Repo/Service)`, `SubCategory Feature (Entity/Repo/Service)`, `Category Feature (Entity/Repo/Service)`, `Auth Service, JWT Handler & User Repo`, `Product`, `Auth & Category Validators (FluentValidation)`, `Core Repository Interfaces & Persistence Impls`, `Auth Controller Actions`, `Application Service Interfaces & Impls`, `Response Extensions & Validation Filter`, `Auth Cookie Service`, `EF Entity Type Configurations`, `Password Value Object`, `ProductAttribute Entity & Configuration`, `User Repository Implementation`, `Phone Value Object`, `20260705114002_10.Designer.cs`, `SubCategory`, `Migration 1 - Initial (Up/Down)`, `ValueObject`, `Migration 3 Designer Snapshot`, `Migration 5 Designer Snapshot`, `Migration 7 Designer Snapshot`, `Migration 9 Designer Snapshot`, `Paginated Result`, `MakeCategoryImagePathRequired`, `RedisCacheService`, `AlignCategoryNameMaxLengthWithDomain`, `20260127152943_5.Designer.cs`, `20260308131958_9.Designer.cs`, `RoleConstants.cs`, `HttpPost`, `HttpPut`, `ICollection`, `int`, `SupplierName`, `20260726111737_ReworkProductAndSupplier.Designer.cs`, `Image`, `LastName`, `Password`, `StockQuantity`?**
  _High betweenness centrality (0.347) - this node is a cross-community bridge._
- **Why does `AgroShop.Persistence.Data` connect `20260719153341_MakeCategoryImagePathRequired.Designer.cs` to `.AddInfrastructure`, `20260222150331_7.Designer.cs`, `20260711114130_11.Designer.cs`, `20260726130523_AddPackageSizeToProduct.Designer.cs`, `20260719153341_MakeCategoryImagePathRequired.Designer.cs`, `20260307133355_8.Designer.cs`, `20260725202611_AlignCategoryNameMaxLengthWithDomain.Designer.cs`, `Migration 1 Designer Snapshot`, `Migration 6 Designer Snapshot`, `Email`, `20260127152943_5.Designer.cs`, `20251016175310_2.Designer.cs`, `20251026150053_3.Designer.cs`, `DbContext Model Snapshot (Migration 10)`, `20260308131958_9.Designer.cs`, `DependencyInjection String Type`, `FirstName Value Object`, `20251026152202_4.Designer.cs`?**
  _High betweenness centrality (0.182) - this node is a cross-community bridge._
- **Why does `AgroShop.Core.Shared` connect `EF Entity Type Configurations` to `.GetSubCategoryByIdAsync`, `RoleConstants.cs`, `HttpGet`, `Migration 5 Designer Snapshot`, `Migration 8 Designer Snapshot`, `User Service & Response`, `20260719153341_MakeCategoryImagePathRequired.Designer.cs`, `User Entity & Configuration`?**
  _High betweenness centrality (0.161) - this node is a cross-community bridge._
- **What connects `net10.0`, `FluentValidation.DependencyInjectionExtensions (12.1.1)`, `Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9)` to the rest of the system?**
  _39 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Controllers, Envelope & Category Interface` be split into smaller, more focused modules?**
  _Cohesion score 0.05555555555555555 - nodes in this community are weakly interconnected._
- **Should `Auth Service, JWT Handler & User Repo` be split into smaller, more focused modules?**
  _Cohesion score 0.13793103448275862 - nodes in this community are weakly interconnected._
- **Should `Role Entity & DbContext Infrastructure` be split into smaller, more focused modules?**
  _Cohesion score 0.1323529411764706 - nodes in this community are weakly interconnected._