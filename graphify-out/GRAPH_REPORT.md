# Graph Report - AgroShop.API  (2026-07-26)

## Corpus Check
- 279 files · ~150,022 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 1543 nodes · 3232 edges · 146 communities (96 shown, 50 thin omitted)
- Extraction: 93% EXTRACTED · 7% INFERRED · 0% AMBIGUOUS · INFERRED: 222 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `46e75379`
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
- .IncludeAll
- _10
- AgroShop.Application.Dto.AttributeDto
- .GenerateTokensAsync
- 20260127152943_5.Designer.cs
- .GetUserByIdAsync
- 20260719153341_MakeCategoryImagePathRequired.Designer.cs
- 20260307133355_8.Designer.cs
- 20260725202611_AlignCategoryNameMaxLengthWithDomain.Designer.cs
- RoleConstants.cs
- Program.cs
- HttpGet
- HttpPost
- HttpPut
- IActionResult
- TimeSpan
- ICollection
- int
- .GenerateTokensAsync
- SupplierName
- 20260127152943_5.Designer.cs
- 20260308131958_9.Designer.cs
- 20260725201921_ReworkSubCategoryNameAndUniqueness.Designer.cs
- 20260726111737_ReworkProductAndSupplier.Designer.cs
- Image
- PackageSize
- ISubCategoryService
- StockQuantity
- CategoryName
- MakeProductDescriptionRequired
- 20251026152202_4.Designer.cs
- .AddInfrastructure
- 20260222150331_7.Designer.cs
- 20260711114130_11.Designer.cs
- 20260726130523_AddPackageSizeToProduct.Designer.cs
- Product
- Phone
- .GetRefreshTokenByHashAsync
- EntityTypeBuilder
- ProductDescription
- AlignCategoryNameMaxLengthWithDomain
- ReworkAttributeSystemWithOptions
- SubCategoryName
- AgroShop.Application.Dto.SubCategoryDto
- 20260222150331_7.Designer.cs
- 20260726132842_ReworkAttributeSystemWithOptions.Designer.cs
- SubCategory
- Image
- SubCategory

## God Nodes (most connected - your core abstractions)
1. `Error` - 217 edges
2. `AgroShop.Core.Entities` - 62 edges
3. `AgroShop.Core.Shared` - 59 edges
4. `AgroShop.Persistence.Data.Migrations` - 41 edges
5. `AgroShop.Core.ValueObjects` - 39 edges
6. `AgroShop.Persistence.Data` - 35 edges
7. `AgroShop.Application.Interfaces` - 33 edges
8. `AgroShop.Core.Interfaces` - 33 edges
9. `Attribute` - 29 edges
10. `User` - 29 edges

## Surprising Connections (you probably didn't know these)
- `AttributeController` --inherits--> `ApplicationController`  [EXTRACTED]
  src/AgroShop.API/Controllers/AttributeController.cs → src/AgroShop.API/Controllers/ApplicationController.cs
- `AuthController` --inherits--> `ApplicationController`  [EXTRACTED]
  src/AgroShop.API/Controllers/AuthController.cs → src/AgroShop.API/Controllers/ApplicationController.cs
- `ProductAttributeController` --inherits--> `ApplicationController`  [EXTRACTED]
  src/AgroShop.API/Controllers/ProductAttributeController.cs → src/AgroShop.API/Controllers/ApplicationController.cs
- `ProductController` --inherits--> `ApplicationController`  [EXTRACTED]
  src/AgroShop.API/Controllers/ProductController.cs → src/AgroShop.API/Controllers/ApplicationController.cs
- `SubCategoryController` --inherits--> `ApplicationController`  [EXTRACTED]
  src/AgroShop.API/Controllers/SubCategoryController.cs → src/AgroShop.API/Controllers/ApplicationController.cs

## Import Cycles
- None detected.

## Communities (146 total, 50 thin omitted)

### Community 1 - "Controllers, Envelope & Category Interface"
Cohesion: 0.29
Nodes (9): SupplierController, CancellationToken, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult, Task (+1 more)

### Community 2 - "Supplier Feature (Entity/Repo/Service)"
Cohesion: 0.09
Nodes (9): AgroShop.Application.Validators.SupplierValidators, AgroShop.Core.ValueObjects, AgroShop.Application.Validators.CategoryValidators, AgroShop.Core.Shared, AgroShop.Application.Validators.ProductValidators, AgroShop.Application.Validators.SubCategoryValidators, AgroShop.Application.Extensions, AgroShop.Application.Validators.AttributeOptionValidators (+1 more)

### Community 3 - "SubCategory Feature (Entity/Repo/Service)"
Cohesion: 0.11
Nodes (10): AgroShop.Persistence, AgroShop.Application, AgroShop.API.Responses, AgroShop.API.Extensions, AgroShop.API, AgroShop.API.Services, AgroShop.API.Middlewares, Program (+2 more)

### Community 4 - "Category Feature (Entity/Repo/Service)"
Cohesion: 0.44
Nodes (4): UserRepository, CancellationToken, Guid, Task

### Community 5 - "Auth Service, JWT Handler & User Repo"
Cohesion: 0.17
Nodes (11): Supplier, Guid, ICollection, Result, ISupplierRepository, CancellationToken, Guid, IEnumerable (+3 more)

### Community 6 - "Product Feature (Entity/Repo/Service)"
Cohesion: 0.26
Nodes (9): AttributeDto, Guid, List, IAttributeService, CancellationToken, IEnumerable, Result, Task (+1 more)

### Community 7 - "Auth & Category Validators (FluentValidation)"
Cohesion: 0.24
Nodes (8): Func, IRuleBuilder, IRuleBuilderOptions, IRuleBuilderOptionsConditions, ValidationExtensions, IFormFile, Result, string

### Community 8 - "Role Entity & DbContext Infrastructure"
Cohesion: 0.13
Nodes (5): AttributeName, Email, Phone, ProductAttributeValue, ProductName

### Community 9 - "JWT Token Handler & Refresh Tokens"
Cohesion: 0.32
Nodes (8): AttributeController, CancellationToken, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult, Task

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
Cohesion: 0.05
Nodes (40): AllowAnonymous, Authorize, CookieOptions, DateTimeOffset, AuthController, CancellationToken, HttpGet, HttpPost (+32 more)

### Community 15 - "Application Service Interfaces & Impls"
Cohesion: 0.21
Nodes (10): IConfigurationSection, JwtTokenHandler, CancellationToken, int, Result, Task, IRefreshTokenRepository, CancellationToken (+2 more)

### Community 16 - "Response Extensions & Validation Filter"
Cohesion: 0.17
Nodes (15): ProductAttributeController, CancellationToken, HttpDelete, HttpGet, HttpPost, IActionResult, Task, AddProductAttributeDto (+7 more)

### Community 17 - "Auth Cookie Service"
Cohesion: 0.29
Nodes (5): Phone, IEnumerable, int, Result, string

### Community 18 - "EF Entity Type Configurations"
Cohesion: 0.18
Nodes (14): AttributeOptionDto, Guid, AttributeOptionService, CancellationToken, Guid, IEnumerable, Result, string (+6 more)

### Community 19 - "Category, Subcategory & AttributeValue Entities"
Cohesion: 0.43
Nodes (4): DependencyInjection, IConfiguration, IServiceCollection, string

### Community 21 - "Password Value Object"
Cohesion: 0.24
Nodes (5): Regex, Password, IEnumerable, int, Result

### Community 22 - "DbContext Model Snapshot (Migration 10)"
Cohesion: 0.11
Nodes (10): AgroShop.Persistence.Data.Migrations, ModelSnapshot, _4, ModelBuilder, _10, ModelBuilder, MakeCategoryImagePathRequired, ModelBuilder (+2 more)

### Community 23 - "Attribute Entity & Configuration"
Cohesion: 0.05
Nodes (49): SubCategoryController, CancellationToken, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult, Task (+41 more)

### Community 24 - "ProductAttribute Entity & Configuration"
Cohesion: 0.19
Nodes (7): RefreshToken, DateTime, Guid, RefreshTokenConfiguration, EntityTypeBuilder, CancellationToken, Task

### Community 25 - "User Entity & Configuration"
Cohesion: 0.15
Nodes (11): AgroShop.Infrastructure, AgroShop.Infrastructure.Caching, IConnectionMultiplexer, IDatabase, RedisCacheService, CancellationToken, Task, TimeSpan (+3 more)

### Community 26 - "User Repository Implementation"
Cohesion: 0.31
Nodes (7): Exception, HttpContext, RequestDelegate, ExceptionHandler, ILogger, IWebHostEnvironment, Task

### Community 27 - "Email Value Object"
Cohesion: 0.25
Nodes (6): ActionExecutingContext, ActionExecutionDelegate, AgroShop.API.Filters, IAsyncActionFilter, ValidationFilter, Task

### Community 28 - "Phone Value Object"
Cohesion: 0.16
Nodes (17): Option, ProductAttributeId, ProductService, CancellationToken, Guid, IEnumerable, List, Result (+9 more)

### Community 29 - "FirstName Value Object"
Cohesion: 0.10
Nodes (10): AgroShop.Application.Dto.AttributeOptionDto, AgroShop.Application.Dto.ProductAttributeDto, AgroShop.Application.Services, AgroShop.Application.Dto.CategoryDto, AgroShop.Application.Dto.SubCategoryDto, AgroShop.Application.Mappers, AgroShop.Application.Dto.SupplierDto, AgroShop.Application.Dto.AttributeDto (+2 more)

### Community 30 - "20260705114002_10.Designer.cs"
Cohesion: 0.24
Nodes (6): Guid, Result, PackageUnit, PackageSize, IEnumerable, Result

### Community 31 - "SubCategory"
Cohesion: 0.26
Nodes (8): AttributeService, CancellationToken, IEnumerable, Result, string, Task, TimeSpan, UnitResult

### Community 34 - "Migration 2 (Up/Down)"
Cohesion: 0.18
Nodes (10): SupplierDto, Guid, UpdateSupplierDto, IFormFile, CancellationToken, IEnumerable, Result, Task (+2 more)

### Community 38 - "Migration 6 (Up/Down)"
Cohesion: 0.06
Nodes (47): AbstractValidator, ControllerBase, AgroShop.Application.Validators.ProductAttributeValidators, ApplicationController, IActionResult, Result, UnitResult, AttributeOptionController (+39 more)

### Community 40 - "Migration 8 (Up/Down)"
Cohesion: 0.06
Nodes (38): ImageStorageService, CancellationToken, IFormFile, IWebHostEnvironment, Task, CategoryDto, Guid, IImageStorageService (+30 more)

### Community 43 - "Migration 1 Designer Snapshot"
Cohesion: 0.44
Nodes (4): IUserRepository, CancellationToken, Guid, Task

### Community 44 - "ValueObject"
Cohesion: 0.33
Nodes (4): Email, IEnumerable, int, Result

### Community 45 - "Migration 3 Designer Snapshot"
Cohesion: 0.33
Nodes (4): FirstName, IEnumerable, int, Result

### Community 47 - "Migration 5 Designer Snapshot"
Cohesion: 0.10
Nodes (9): Attribute, Category, Errors, Money, PackageSize, ProductAttribute, StockQuantity, Supplier (+1 more)

### Community 49 - "Migration 7 Designer Snapshot"
Cohesion: 0.33
Nodes (4): LastName, IEnumerable, int, Result

### Community 50 - "Migration 8 Designer Snapshot"
Cohesion: 0.29
Nodes (8): SupplierService, CancellationToken, IEnumerable, Result, string, Task, TimeSpan, UnitResult

### Community 51 - "Migration 9 Designer Snapshot"
Cohesion: 0.33
Nodes (4): Patronymic, IEnumerable, int, Result

### Community 52 - "Email"
Cohesion: 0.33
Nodes (5): IAttributeRepository, CancellationToken, Guid, IEnumerable, Task

### Community 55 - "MakeCategoryImagePathRequired"
Cohesion: 0.33
Nodes (4): ActionResult, ModelStateDictionary, ResponseExtensions, ValidationResult

### Community 56 - "AuthController HttpGet Attribute"
Cohesion: 0.15
Nodes (11): AddAttributeDto, AttributeMapper, IEnumerable, AddAttributeDtoValidator, Attribute, Guid, ICollection, Result (+3 more)

### Community 57 - "ExceptionHandler"
Cohesion: 0.33
Nodes (5): AttributeRepository, CancellationToken, Guid, IEnumerable, Task

### Community 59 - "AuthController Task Type"
Cohesion: 0.33
Nodes (5): SupplierRepository, CancellationToken, Guid, IEnumerable, Task

### Community 88 - "20260719153341_MakeCategoryImagePathRequired.Designer.cs"
Cohesion: 0.12
Nodes (6): AgroShop.Core.Entities, AgroShop.Persistence.Extensions, AgroShop.Persistence.Configurations, AgroShop.Persistence.Repositories, AgroShop.Core.Interfaces, AgroShop.Persistence.Data

### Community 92 - "RedisCacheService"
Cohesion: 0.40
Nodes (3): StockQuantity, IEnumerable, Result

### Community 93 - "ProductAttribute"
Cohesion: 0.18
Nodes (13): ProductAttributeService, CancellationToken, IEnumerable, Result, string, Task, TimeSpan, UnitResult (+5 more)

### Community 94 - "ProductAttributeValue"
Cohesion: 0.05
Nodes (35): DbContext, DbSet, IDesignTimeDbContextFactory, IQueryable, ProductAttribute, Guid, ICollection, Result (+27 more)

### Community 95 - "CategoryRepository"
Cohesion: 0.33
Nodes (4): SubCategoryName, IEnumerable, int, Result

### Community 96 - ".GetCategoryByIdAsync"
Cohesion: 0.24
Nodes (8): HashSet, User, DateTime, Guid, IReadOnlyCollection, Result, UserConfiguration, EntityTypeBuilder

### Community 99 - "AgroShop.Application.Dto.AttributeDto"
Cohesion: 0.23
Nodes (4): AgroShop.Application.Jwt, AgroShop.Application.Responses, AgroShop.Application.Dto.AuthDto, AgroShop.Application.Validators.AuthenticationValidators

### Community 100 - ".GenerateTokensAsync"
Cohesion: 0.07
Nodes (37): ProductController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+29 more)

### Community 101 - "20260127152943_5.Designer.cs"
Cohesion: 0.10
Nodes (15): IEntityTypeConfiguration, Role, Guid, IRoleRepository, CancellationToken, Guid, Task, AttributeOptionConfiguration (+7 more)

### Community 103 - "20260719153341_MakeCategoryImagePathRequired.Designer.cs"
Cohesion: 0.16
Nodes (12): ProductMapper, IEnumerable, Product, DateTime, ICollection, ProductConfiguration, EntityTypeBuilder, ProductRepository (+4 more)

### Community 109 - "HttpPost"
Cohesion: 0.33
Nodes (4): ProductDescription, IEnumerable, int, Result

### Community 110 - "HttpPut"
Cohesion: 0.33
Nodes (4): ProductName, IEnumerable, int, Result

### Community 113 - "ICollection"
Cohesion: 0.50
Nodes (3): AddSupplierDto, IFormFile, AddSupplierDtoValidator

### Community 114 - "int"
Cohesion: 0.33
Nodes (4): ProductAttributeDto, Guid, ProductAttributeMapper, IEnumerable

### Community 120 - "20260726111737_ReworkProductAndSupplier.Designer.cs"
Cohesion: 0.33
Nodes (4): Sku, IEnumerable, int, Result

### Community 123 - "ISubCategoryService"
Cohesion: 0.39
Nodes (4): ICacheService, CancellationToken, Task, TimeSpan

### Community 124 - "StockQuantity"
Cohesion: 0.33
Nodes (4): AttributeName, IEnumerable, int, Result

### Community 127 - "20251026152202_4.Designer.cs"
Cohesion: 0.33
Nodes (4): CategoryName, IEnumerable, int, Result

### Community 130 - "20260711114130_11.Designer.cs"
Cohesion: 0.20
Nodes (7): AttributeOptionValue, IEnumerable, int, Money, IEnumerable, Result, ValueObject

### Community 133 - "Phone"
Cohesion: 0.33
Nodes (4): SupplierName, IEnumerable, int, Result

### Community 135 - "EntityTypeBuilder"
Cohesion: 0.11
Nodes (17): AttributeOptionMapper, IEnumerable, AttributeOption, Guid, ICollection, Result, IAttributeOptionRepository, CancellationToken (+9 more)

### Community 137 - "AlignCategoryNameMaxLengthWithDomain"
Cohesion: 0.22
Nodes (5): Migration, _2, MigrationBuilder, AlignCategoryNameMaxLengthWithDomain, MigrationBuilder

### Community 139 - "SubCategoryName"
Cohesion: 0.11
Nodes (7): ErrorType, Error, string, AttributeOption, General, ProductDescription, SubCategoryName

## Knowledge Gaps
- **40 isolated node(s):** `net10.0`, `FluentValidation.DependencyInjectionExtensions (12.1.1)`, `Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9)`, `Microsoft.AspNetCore.OpenApi (10.0.9)`, `Swashbuckle.AspNetCore (10.2.3)` (+35 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **50 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Error` connect `SubCategoryName` to `Domain Errors & Validation Results`, `20260711114130_11.Designer.cs`, `Product`, `Auth Service, JWT Handler & User Repo`, `Product Feature (Entity/Repo/Service)`, `Auth & Category Validators (FluentValidation)`, `EntityTypeBuilder`, `Role Entity & DbContext Infrastructure`, `Phone`, `AgroShop.Application.Dto.SubCategoryDto`, `Core Repository Interfaces & Persistence Impls`, `Auth Controller Actions`, `Application Service Interfaces & Impls`, `Response Extensions & Validation Filter`, `Auth Cookie Service`, `EF Entity Type Configurations`, `User Service & Response`, `Password Value Object`, `Image`, `Attribute Entity & Configuration`, `SubCategory`, `Phone Value Object`, `20260705114002_10.Designer.cs`, `SubCategory`, `Migration 1 - Initial (Up/Down)`, `Migration 2 (Up/Down)`, `Migration 6 (Up/Down)`, `Migration 8 (Up/Down)`, `ValueObject`, `Migration 3 Designer Snapshot`, `Migration 5 Designer Snapshot`, `Migration 7 Designer Snapshot`, `Migration 8 Designer Snapshot`, `Migration 9 Designer Snapshot`, `Paginated Result`, `LastName`, `MakeCategoryImagePathRequired`, `AuthController HttpGet Attribute`, `20251016175310_2.Designer.cs`, `RedisCacheService`, `ProductAttribute`, `ProductAttributeValue`, `CategoryRepository`, `.GetCategoryByIdAsync`, `.GenerateTokensAsync`, `Program.cs`, `HttpPost`, `HttpPut`, `20260726111737_ReworkProductAndSupplier.Designer.cs`, `StockQuantity`, `CategoryName`, `20251026152202_4.Designer.cs`?**
  _High betweenness centrality (0.355) - this node is a cross-community bridge._
- **Why does `AgroShop.Persistence.Data` connect `20260719153341_MakeCategoryImagePathRequired.Designer.cs` to `.AddInfrastructure`, `20260222150331_7.Designer.cs`, `20260726130523_AddPackageSizeToProduct.Designer.cs`, `.GetRefreshTokenByHashAsync`, `20260222150331_7.Designer.cs`, `20260726132842_ReworkAttributeSystemWithOptions.Designer.cs`, `DbContext Model Snapshot (Migration 10)`, `Migration 6 Designer Snapshot`, `DependencyInjection String Type`, `20251026150053_3.Designer.cs`, `ProductAttributeValue`, `20260307133355_8.Designer.cs`, `20260725202611_AlignCategoryNameMaxLengthWithDomain.Designer.cs`, `HttpGet`, `.GenerateTokensAsync`, `SupplierName`, `20260127152943_5.Designer.cs`, `20260308131958_9.Designer.cs`, `PackageSize`?**
  _High betweenness centrality (0.231) - this node is a cross-community bridge._
- **Why does `AgroShop.Core.Shared` connect `Supplier Feature (Entity/Repo/Service)` to `AgroShop.Application.Dto.AttributeDto`, `SubCategory Feature (Entity/Repo/Service)`, `Migration 5 Designer Snapshot`, `EF Entity Type Configurations`, `User Service & Response`, `20260719153341_MakeCategoryImagePathRequired.Designer.cs`, `Image`, `FirstName Value Object`?**
  _High betweenness centrality (0.150) - this node is a cross-community bridge._
- **What connects `net10.0`, `FluentValidation.DependencyInjectionExtensions (12.1.1)`, `Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9)` to the rest of the system?**
  _40 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Supplier Feature (Entity/Repo/Service)` be split into smaller, more focused modules?**
  _Cohesion score 0.08599033816425121 - nodes in this community are weakly interconnected._
- **Should `SubCategory Feature (Entity/Repo/Service)` be split into smaller, more focused modules?**
  _Cohesion score 0.11428571428571428 - nodes in this community are weakly interconnected._
- **Should `Role Entity & DbContext Infrastructure` be split into smaller, more focused modules?**
  _Cohesion score 0.12666666666666668 - nodes in this community are weakly interconnected._