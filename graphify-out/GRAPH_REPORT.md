# Graph Report - AgroShop.API  (2026-07-26)

## Corpus Check
- 269 files · ~228,601 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 1449 nodes · 2983 edges · 147 communities (99 shown, 48 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 177 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `1c4a51e1`
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
- EntityTypeBuilder
- MigrationBuilder
- AlignCategoryNameMaxLengthWithDomain
- ReworkAttributeSystemWithOptions
- AddAttributeOptionDto
- Phone
- 20260222150331_7.Designer.cs
- 20260725201921_ReworkSubCategoryNameAndUniqueness.Designer.cs
- 20260726132842_ReworkAttributeSystemWithOptions.Designer.cs
- UpdateAttributeOptionDto
- .ToDto
- Image

## God Nodes (most connected - your core abstractions)
1. `Error` - 212 edges
2. `AgroShop.Core.Entities` - 60 edges
3. `AgroShop.Core.Shared` - 59 edges
4. `AgroShop.Core.ValueObjects` - 39 edges
5. `AgroShop.Persistence.Data.Migrations` - 39 edges
6. `AgroShop.Persistence.Data` - 33 edges
7. `AgroShop.Core.Interfaces` - 31 edges
8. `AgroShop.Application.Interfaces` - 30 edges
9. `Attribute` - 29 edges
10. `User` - 29 edges

## Surprising Connections (you probably didn't know these)
- `AuthController` --inherits--> `ApplicationController`  [EXTRACTED]
  src/AgroShop.API/Controllers/AuthController.cs → src/AgroShop.API/Controllers/ApplicationController.cs
- `SubCategoryController` --inherits--> `ApplicationController`  [EXTRACTED]
  src/AgroShop.API/Controllers/SubCategoryController.cs → src/AgroShop.API/Controllers/ApplicationController.cs
- `SupplierController` --inherits--> `ApplicationController`  [EXTRACTED]
  src/AgroShop.API/Controllers/SupplierController.cs → src/AgroShop.API/Controllers/ApplicationController.cs
- `AuthController` --references--> `IUserService`  [EXTRACTED]
  src/AgroShop.API/Controllers/AuthController.cs → src/AgroShop.Application/Interfaces/IUserService.cs
- `ImageStorageService` --implements--> `IImageStorageService`  [EXTRACTED]
  src/AgroShop.API/Services/ImageStorageService.cs → src/AgroShop.Application/Interfaces/IImageStorageService.cs

## Import Cycles
- None detected.

## Communities (147 total, 48 thin omitted)

### Community 0 - "Domain Errors & Validation Results"
Cohesion: 0.18
Nodes (3): LastName, ProductName, Sku

### Community 1 - "Controllers, Envelope & Category Interface"
Cohesion: 0.06
Nodes (49): ControllerBase, ApplicationController, IActionResult, Result, UnitResult, CategoryController, CancellationToken, HttpDelete (+41 more)

### Community 2 - "Supplier Feature (Entity/Repo/Service)"
Cohesion: 0.16
Nodes (16): AttributeDto, Guid, List, AttributeService, CancellationToken, IEnumerable, Result, string (+8 more)

### Community 3 - "SubCategory Feature (Entity/Repo/Service)"
Cohesion: 0.22
Nodes (11): SubCategoryDto, Guid, SubCategoryService, CancellationToken, Guid, IEnumerable, Result, string (+3 more)

### Community 4 - "Category Feature (Entity/Repo/Service)"
Cohesion: 0.18
Nodes (12): HashSet, User, DateTime, Guid, IReadOnlyCollection, Result, UserConfiguration, EntityTypeBuilder (+4 more)

### Community 5 - "Auth Service, JWT Handler & User Repo"
Cohesion: 0.12
Nodes (20): SupplierController, CancellationToken, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult, Task (+12 more)

### Community 6 - "Product Feature (Entity/Repo/Service)"
Cohesion: 0.18
Nodes (13): ProductAttributeService, CancellationToken, IEnumerable, Result, string, Task, TimeSpan, UnitResult (+5 more)

### Community 7 - "Auth & Category Validators (FluentValidation)"
Cohesion: 0.24
Nodes (8): Func, IRuleBuilder, IRuleBuilderOptions, IRuleBuilderOptionsConditions, ValidationExtensions, IFormFile, Result, string

### Community 8 - "Role Entity & DbContext Infrastructure"
Cohesion: 0.07
Nodes (24): DbContext, DbSet, IDesignTimeDbContextFactory, Role, Guid, IRoleRepository, CancellationToken, Guid (+16 more)

### Community 9 - "JWT Token Handler & Refresh Tokens"
Cohesion: 0.20
Nodes (11): CategoryService, CancellationToken, IEnumerable, Result, string, Task, TimeSpan, UnitResult (+3 more)

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
Nodes (30): AllowAnonymous, Authorize, CookieOptions, DateTimeOffset, AuthController, CancellationToken, HttpGet, HttpPost (+22 more)

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
Cohesion: 0.11
Nodes (5): AgroShop.Core.ValueObjects, AgroShop.Core.Shared, AgroShop.Core.Enums, RoleConstants, Guid

### Community 19 - "Category, Subcategory & AttributeValue Entities"
Cohesion: 0.43
Nodes (4): DependencyInjection, IConfiguration, IServiceCollection, string

### Community 20 - "User Service & Response"
Cohesion: 0.11
Nodes (11): AgroShop.Application.Dto.ProductAttributeDto, AgroShop.Application.Services, AgroShop.Application.Validators.SupplierValidators, AgroShop.Application.Dto.SubCategoryDto, AgroShop.API.Responses, AgroShop.Application.Dto.ProductDto, AgroShop.API.Extensions, AgroShop.Application.Mappers (+3 more)

### Community 21 - "Password Value Object"
Cohesion: 0.24
Nodes (5): Regex, Password, IEnumerable, int, Result

### Community 22 - "DbContext Model Snapshot (Migration 10)"
Cohesion: 0.11
Nodes (10): AgroShop.Persistence.Data.Migrations, ModelSnapshot, _4, ModelBuilder, _11, ModelBuilder, MakeCategoryImagePathRequired, ModelBuilder (+2 more)

### Community 23 - "Attribute Entity & Configuration"
Cohesion: 0.20
Nodes (11): SupplierService, CancellationToken, IEnumerable, Result, string, Task, TimeSpan, UnitResult (+3 more)

### Community 24 - "ProductAttribute Entity & Configuration"
Cohesion: 0.19
Nodes (7): RefreshToken, DateTime, Guid, RefreshTokenConfiguration, EntityTypeBuilder, CancellationToken, Task

### Community 25 - "User Entity & Configuration"
Cohesion: 0.28
Nodes (9): AttributeOptionService, CancellationToken, Guid, IEnumerable, Result, string, Task, TimeSpan (+1 more)

### Community 26 - "User Repository Implementation"
Cohesion: 0.14
Nodes (11): CategoryMapper, IEnumerable, Category, Guid, IReadOnlyCollection, List, Result, ICategoryRepository (+3 more)

### Community 27 - "Email Value Object"
Cohesion: 0.25
Nodes (6): ActionExecutingContext, ActionExecutionDelegate, AgroShop.API.Filters, IAsyncActionFilter, ValidationFilter, Task

### Community 28 - "Phone Value Object"
Cohesion: 0.19
Nodes (12): IImageStorageService, CancellationToken, IFormFile, Task, ProductService, CancellationToken, IEnumerable, Result (+4 more)

### Community 29 - "FirstName Value Object"
Cohesion: 0.22
Nodes (9): AttributeOption, Guid, ICollection, Result, IAttributeOptionRepository, CancellationToken, Guid, IEnumerable (+1 more)

### Community 30 - "20260705114002_10.Designer.cs"
Cohesion: 0.17
Nodes (11): Product, DateTime, Guid, ICollection, Result, PackageUnit, PackageSize, IEnumerable (+3 more)

### Community 31 - "SubCategory"
Cohesion: 0.11
Nodes (23): AbstractValidator, AgroShop.Application.Validators.ProductAttributeValidators, SubCategoryController, CancellationToken, HttpDelete, HttpGet, HttpPost, HttpPut (+15 more)

### Community 34 - "Migration 2 (Up/Down)"
Cohesion: 0.32
Nodes (5): ImageStorageService, CancellationToken, IFormFile, IWebHostEnvironment, Task

### Community 38 - "Migration 6 (Up/Down)"
Cohesion: 0.26
Nodes (8): UpdateAttributeDto, IAttributeService, CancellationToken, IEnumerable, Result, Task, UnitResult, UpdateAttributeDtoValidator

### Community 44 - "ValueObject"
Cohesion: 0.33
Nodes (4): Email, IEnumerable, int, Result

### Community 45 - "Migration 3 Designer Snapshot"
Cohesion: 0.33
Nodes (4): FirstName, IEnumerable, int, Result

### Community 46 - "Migration 4 Designer Snapshot"
Cohesion: 0.22
Nodes (5): Migration, _1, MigrationBuilder, _6, MigrationBuilder

### Community 47 - "Migration 5 Designer Snapshot"
Cohesion: 0.09
Nodes (10): Attribute, Category, Errors, PackageSize, ProductAttribute, ProductAttributeValue, StockQuantity, SubCategory (+2 more)

### Community 49 - "Migration 7 Designer Snapshot"
Cohesion: 0.33
Nodes (4): LastName, IEnumerable, int, Result

### Community 50 - "Migration 8 Designer Snapshot"
Cohesion: 0.13
Nodes (7): AgroShop.Application.Jwt, AgroShop.Application.Responses, AgroShop.Application.Dto.AuthDto, AgroShop.Application.Validators.AuthenticationValidators, AgroShop.API.Services, DependencyInjection, IServiceCollection

### Community 51 - "Migration 9 Designer Snapshot"
Cohesion: 0.33
Nodes (4): Patronymic, IEnumerable, int, Result

### Community 53 - "Paginated Result"
Cohesion: 0.08
Nodes (22): SupplierMapper, IEnumerable, Supplier, Guid, ICollection, Result, ISupplierRepository, CancellationToken (+14 more)

### Community 54 - "LastName"
Cohesion: 0.33
Nodes (5): CategoryRepository, CancellationToken, Guid, IEnumerable, Task

### Community 55 - "MakeCategoryImagePathRequired"
Cohesion: 0.18
Nodes (10): IUserService, CancellationToken, Result, Task, UserResponse, Guid, UserService, CancellationToken (+2 more)

### Community 56 - "AuthController HttpGet Attribute"
Cohesion: 0.10
Nodes (13): AgroShop.Application.Dto.AttributeDto, AgroShop.Application.Validators.AttributeValidators, AddAttributeDto, AttributeMapper, IEnumerable, AddAttributeDtoValidator, Attribute, Guid (+5 more)

### Community 57 - "ExceptionHandler"
Cohesion: 0.07
Nodes (23): AgroShop.Persistence, AgroShop.Infrastructure, AgroShop.Application, AgroShop.API, AgroShop.Infrastructure.Caching, AgroShop.API.Middlewares, Exception, HttpContext (+15 more)

### Community 59 - "AuthController Task Type"
Cohesion: 0.29
Nodes (8): AttributeOptionDto, Guid, IAttributeOptionService, CancellationToken, IEnumerable, Result, Task, UnitResult

### Community 88 - "20260719153341_MakeCategoryImagePathRequired.Designer.cs"
Cohesion: 0.12
Nodes (6): AgroShop.Core.Entities, AgroShop.Persistence.Extensions, AgroShop.Persistence.Configurations, AgroShop.Persistence.Repositories, AgroShop.Core.Interfaces, AgroShop.Persistence.Data

### Community 92 - "RedisCacheService"
Cohesion: 0.11
Nodes (7): ErrorType, Error, string, CategoryName, General, Money, SubCategoryName

### Community 93 - "ProductAttribute"
Cohesion: 0.08
Nodes (25): IQueryable, AddProductAttributeDto, Guid, ProductAttributeDto, Guid, IProductAttributeService, CancellationToken, IEnumerable (+17 more)

### Community 94 - "ProductAttributeValue"
Cohesion: 0.22
Nodes (8): IEntityTypeConfiguration, ProductAttributeValue, Guid, Result, AttributeOptionConfiguration, EntityTypeBuilder, ProductAttributeValueConfiguration, EntityTypeBuilder

### Community 95 - "CategoryRepository"
Cohesion: 0.33
Nodes (5): IProductRepository, CancellationToken, Guid, IEnumerable, Task

### Community 96 - ".GetCategoryByIdAsync"
Cohesion: 0.44
Nodes (4): IUserRepository, CancellationToken, Guid, Task

### Community 97 - ".GetSubCategoryByIdAsync"
Cohesion: 0.19
Nodes (5): AgroShop.Application.Dto.CategoryDto, AgroShop.Application.Validators.CategoryValidators, AgroShop.Application.Validators.ProductValidators, AgroShop.Application.Validators.SubCategoryValidators, AgroShop.Application.Extensions

### Community 99 - "AlignCategoryNameMaxLengthWithDomain"
Cohesion: 0.33
Nodes (4): CategoryName, IEnumerable, int, Result

### Community 102 - "20260308131958_9.Designer.cs"
Cohesion: 0.33
Nodes (5): AttributeOptionRepository, CancellationToken, Guid, IEnumerable, Task

### Community 103 - "20260719153341_MakeCategoryImagePathRequired.Designer.cs"
Cohesion: 0.33
Nodes (5): ProductRepository, CancellationToken, Guid, IEnumerable, Task

### Community 106 - "RoleConstants.cs"
Cohesion: 0.33
Nodes (4): SubCategoryName, IEnumerable, int, Result

### Community 107 - "HttpDelete"
Cohesion: 0.33
Nodes (5): SubCategoryRepository, CancellationToken, Guid, IEnumerable, Task

### Community 108 - "HttpGet"
Cohesion: 0.39
Nodes (4): ICacheService, CancellationToken, Task, TimeSpan

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
Cohesion: 0.39
Nodes (5): ISubCategoryRepository, CancellationToken, Guid, IEnumerable, Task

### Community 120 - "20260726111737_ReworkProductAndSupplier.Designer.cs"
Cohesion: 0.33
Nodes (4): Sku, IEnumerable, int, Result

### Community 121 - "Image"
Cohesion: 0.33
Nodes (4): ActionResult, ModelStateDictionary, ResponseExtensions, ValidationResult

### Community 124 - "StockQuantity"
Cohesion: 0.40
Nodes (3): StockQuantity, IEnumerable, Result

### Community 127 - "20251026152202_4.Designer.cs"
Cohesion: 0.29
Nodes (5): ProductDto, DateTime, Guid, ProductMapper, IEnumerable

### Community 130 - "20260711114130_11.Designer.cs"
Cohesion: 0.29
Nodes (5): AttributeName, IEnumerable, int, Result, ValueObject

### Community 135 - "EntityTypeBuilder"
Cohesion: 0.33
Nodes (4): AttributeOptionValue, IEnumerable, int, Result

### Community 139 - "AddAttributeOptionDto"
Cohesion: 0.50
Nodes (3): AddAttributeOptionDto, Guid, AddAttributeOptionDtoValidator

## Knowledge Gaps
- **40 isolated node(s):** `net10.0`, `FluentValidation.DependencyInjectionExtensions (12.1.1)`, `Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9)`, `Microsoft.AspNetCore.OpenApi (10.0.9)`, `Swashbuckle.AspNetCore (10.2.3)` (+35 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **48 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Error` connect `RedisCacheService` to `Domain Errors & Validation Results`, `Controllers, Envelope & Category Interface`, `Supplier Feature (Entity/Repo/Service)`, `SubCategory Feature (Entity/Repo/Service)`, `Category Feature (Entity/Repo/Service)`, `Auth Service, JWT Handler & User Repo`, `Product Feature (Entity/Repo/Service)`, `Auth & Category Validators (FluentValidation)`, `RoleConstants.cs`, `JWT Token Handler & Refresh Tokens`, `MigrationBuilder`, `.GetRefreshTokenByHashAsync`, `Phone`, `Core Repository Interfaces & Persistence Impls`, `Auth Controller Actions`, `Application Service Interfaces & Impls`, `20260711114130_11.Designer.cs`, `Response Extensions & Validation Filter`, `EF Entity Type Configurations`, `Image`, `Auth Cookie Service`, `Password Value Object`, `Attribute Entity & Configuration`, `User Entity & Configuration`, `User Repository Implementation`, `Product`, `Phone Value Object`, `FirstName Value Object`, `20260705114002_10.Designer.cs`, `SubCategory`, `Migration 1 - Initial (Up/Down)`, `Migration 6 (Up/Down)`, `EntityTypeBuilder`, `ValueObject`, `Migration 3 Designer Snapshot`, `Migration 5 Designer Snapshot`, `Migration 7 Designer Snapshot`, `Migration 9 Designer Snapshot`, `Paginated Result`, `MakeCategoryImagePathRequired`, `AuthController HttpGet Attribute`, `AuthController Task Type`, `ProductAttribute`, `ProductAttributeValue`, `AlignCategoryNameMaxLengthWithDomain`, `.GenerateTokensAsync`, `20260127152943_5.Designer.cs`, `RoleConstants.cs`, `HttpPost`, `HttpPut`, `ICollection`, `int`, `SupplierName`, `20260726111737_ReworkProductAndSupplier.Designer.cs`, `Image`, `Password`, `StockQuantity`?**
  _High betweenness centrality (0.332) - this node is a cross-community bridge._
- **Why does `AgroShop.Persistence.Data` connect `20260719153341_MakeCategoryImagePathRequired.Designer.cs` to `.AddInfrastructure`, `20260222150331_7.Designer.cs`, `20260726130523_AddPackageSizeToProduct.Designer.cs`, `Role Entity & DbContext Infrastructure`, `20260307133355_8.Designer.cs`, `20260725202611_AlignCategoryNameMaxLengthWithDomain.Designer.cs`, `Migration 1 Designer Snapshot`, `20260222150331_7.Designer.cs`, `20260725201921_ReworkSubCategoryNameAndUniqueness.Designer.cs`, `20260726132842_ReworkAttributeSystemWithOptions.Designer.cs`, `Migration 6 Designer Snapshot`, `Email`, `20260127152943_5.Designer.cs`, `DbContext Model Snapshot (Migration 10)`, `20251016175310_2.Designer.cs`, `20251026150053_3.Designer.cs`, `20260308131958_9.Designer.cs`, `DependencyInjection String Type`?**
  _High betweenness centrality (0.215) - this node is a cross-community bridge._
- **Why does `AgroShop.Persistence.Data.Migrations` connect `DbContext Model Snapshot (Migration 10)` to `.AddInfrastructure`, `20260222150331_7.Designer.cs`, `20260726130523_AddPackageSizeToProduct.Designer.cs`, `AlignCategoryNameMaxLengthWithDomain`, `ReworkAttributeSystemWithOptions`, `20260222150331_7.Designer.cs`, `20260725201921_ReworkSubCategoryNameAndUniqueness.Designer.cs`, `20260726132842_ReworkAttributeSystemWithOptions.Designer.cs`, `Migration 10 (Up/Down)`, `Migration 3 (Up/Down)`, `Migration 4 (Up/Down)`, `Role`, `Migration 7 (Up/Down)`, `Migration 8 (Up/Down)`, `Migration 9 (Up/Down)`, `Migration 11 (Up/Down)`, `Migration 1 Designer Snapshot`, `Migration 4 Designer Snapshot`, `Migration 6 Designer Snapshot`, `Email`, `AuthController IActionResult Type`, `DependencyInjection String Type`, `20251016175310_2.Designer.cs`, `20251026150053_3.Designer.cs`, `_10`, `20260307133355_8.Designer.cs`, `20260725202611_AlignCategoryNameMaxLengthWithDomain.Designer.cs`, `IActionResult`, `TimeSpan`, `20260127152943_5.Designer.cs`, `20260308131958_9.Designer.cs`, `20260725201921_ReworkSubCategoryNameAndUniqueness.Designer.cs`, `_2`, `MakeProductDescriptionRequired`?**
  _High betweenness centrality (0.130) - this node is a cross-community bridge._
- **What connects `net10.0`, `FluentValidation.DependencyInjectionExtensions (12.1.1)`, `Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9)` to the rest of the system?**
  _40 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Controllers, Envelope & Category Interface` be split into smaller, more focused modules?**
  _Cohesion score 0.05555555555555555 - nodes in this community are weakly interconnected._
- **Should `Auth Service, JWT Handler & User Repo` be split into smaller, more focused modules?**
  _Cohesion score 0.12310606060606061 - nodes in this community are weakly interconnected._
- **Should `Role Entity & DbContext Infrastructure` be split into smaller, more focused modules?**
  _Cohesion score 0.06685633001422475 - nodes in this community are weakly interconnected._