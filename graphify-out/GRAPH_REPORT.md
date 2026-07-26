# Graph Report - AgroShop.API  (2026-07-26)

## Corpus Check
- 277 files · ~234,126 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 1532 nodes · 3213 edges · 150 communities (103 shown, 47 thin omitted)
- Extraction: 93% EXTRACTED · 7% INFERRED · 0% AMBIGUOUS · INFERRED: 218 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `7d4d9e63`
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
- .SaveAsync
- 20260726132842_ReworkAttributeSystemWithOptions.Designer.cs
- AddProductDto
- SubCategory
- 20260725201921_ReworkSubCategoryNameAndUniqueness.Designer.cs
- DependencyInjection
- Product
- ProductAttributeValue

## God Nodes (most connected - your core abstractions)
1. `Error` - 217 edges
2. `AgroShop.Core.Entities` - 62 edges
3. `AgroShop.Core.Shared` - 59 edges
4. `AgroShop.Core.ValueObjects` - 39 edges
5. `AgroShop.Persistence.Data.Migrations` - 39 edges
6. `AgroShop.Persistence.Data` - 34 edges
7. `AgroShop.Application.Interfaces` - 33 edges
8. `AgroShop.Core.Interfaces` - 33 edges
9. `Attribute` - 29 edges
10. `User` - 29 edges

## Surprising Connections (you probably didn't know these)
- `Error` --references--> `ErrorType`  [EXTRACTED]
  src/AgroShop.Core/Shared/Error.cs → src/AgroShop.Core/Enums/ErrorType.cs
- `AuthController` --inherits--> `ApplicationController`  [EXTRACTED]
  src/AgroShop.API/Controllers/AuthController.cs → src/AgroShop.API/Controllers/ApplicationController.cs
- `CategoryController` --inherits--> `ApplicationController`  [EXTRACTED]
  src/AgroShop.API/Controllers/CategoryController.cs → src/AgroShop.API/Controllers/ApplicationController.cs
- `ProductAttributeController` --inherits--> `ApplicationController`  [EXTRACTED]
  src/AgroShop.API/Controllers/ProductAttributeController.cs → src/AgroShop.API/Controllers/ApplicationController.cs
- `ProductController` --inherits--> `ApplicationController`  [EXTRACTED]
  src/AgroShop.API/Controllers/ProductController.cs → src/AgroShop.API/Controllers/ApplicationController.cs

## Import Cycles
- None detected.

## Communities (150 total, 47 thin omitted)

### Community 1 - "Controllers, Envelope & Category Interface"
Cohesion: 0.15
Nodes (16): SupplierController, CancellationToken, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult, Task (+8 more)

### Community 2 - "Supplier Feature (Entity/Repo/Service)"
Cohesion: 0.11
Nodes (14): AbstractValidator, AgroShop.Application.Validators.SupplierValidators, AgroShop.Application.Validators.ProductAttributeValidators, AgroShop.Application.Validators.SubCategoryValidators, AgroShop.Application.Dto.SupplierDto, AgroShop.Application.Validators.AuthenticationValidators, AgroShop.Application.Extensions, AddSupplierDto (+6 more)

### Community 3 - "SubCategory Feature (Entity/Repo/Service)"
Cohesion: 0.13
Nodes (10): AgroShop.Persistence.Configurations, IEntityTypeConfiguration, AttributeOptionConfiguration, EntityTypeBuilder, ProductAttributeConfiguration, EntityTypeBuilder, ProductConfiguration, EntityTypeBuilder (+2 more)

### Community 4 - "Category Feature (Entity/Repo/Service)"
Cohesion: 0.44
Nodes (4): UserRepository, CancellationToken, Guid, Task

### Community 5 - "Auth Service, JWT Handler & User Repo"
Cohesion: 0.08
Nodes (19): IQueryable, SupplierMapper, IEnumerable, Supplier, Guid, ICollection, Result, SupplierName (+11 more)

### Community 6 - "Product Feature (Entity/Repo/Service)"
Cohesion: 0.33
Nodes (5): IProductRepository, CancellationToken, Guid, IEnumerable, Task

### Community 7 - "Auth & Category Validators (FluentValidation)"
Cohesion: 0.24
Nodes (8): Func, IRuleBuilder, IRuleBuilderOptions, IRuleBuilderOptionsConditions, ValidationExtensions, IFormFile, Result, string

### Community 9 - "JWT Token Handler & Refresh Tokens"
Cohesion: 0.11
Nodes (22): AgroShop.Application.Dto.SubCategoryDto, SubCategoryController, CancellationToken, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+14 more)

### Community 10 - "Project Structure & NuGet Dependencies"
Cohesion: 0.07
Nodes (25): CSharpFunctionalExtensions (3.7.0), FluentValidation (12.1.1), Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9), Microsoft.AspNetCore.OpenApi (10.0.9), Microsoft.EntityFrameworkCore (10.0.9), Microsoft.EntityFrameworkCore.Design (10.0.9), Microsoft.EntityFrameworkCore.Relational (10.0.9), Microsoft.Extensions.Configuration.EnvironmentVariables (10.0.9) (+17 more)

### Community 11 - "Dependency Injection & Program Entry"
Cohesion: 0.15
Nodes (8): AgroShop.Persistence, AgroShop.Application, AgroShop.API, IServiceProvider, Program, DependencyInjection, IConfiguration, IServiceCollection

### Community 12 - "Launch Settings (Dev Config)"
Cohesion: 0.10
Nodes (21): applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, launchUrl, ASPNETCORE_ENVIRONMENT, commandName (+13 more)

### Community 13 - "Core Repository Interfaces & Persistence Impls"
Cohesion: 0.20
Nodes (12): PasswordHasher, IJwtTokenHandler, CancellationToken, Result, Task, AuthResponse, AuthService, CancellationToken (+4 more)

### Community 14 - "Auth Controller Actions"
Cohesion: 0.26
Nodes (10): AllowAnonymous, Authorize, AuthController, CancellationToken, HttpGet, HttpPost, IActionResult, Task (+2 more)

### Community 15 - "Application Service Interfaces & Impls"
Cohesion: 0.21
Nodes (10): IConfigurationSection, JwtTokenHandler, CancellationToken, int, Result, Task, IRefreshTokenRepository, CancellationToken (+2 more)

### Community 16 - "Response Extensions & Validation Filter"
Cohesion: 0.14
Nodes (17): ProductAttributeController, CancellationToken, HttpDelete, HttpGet, HttpPost, IActionResult, Task, AddProductAttributeDto (+9 more)

### Community 17 - "Auth Cookie Service"
Cohesion: 0.29
Nodes (5): Phone, IEnumerable, int, Result, string

### Community 19 - "Category, Subcategory & AttributeValue Entities"
Cohesion: 0.43
Nodes (4): DependencyInjection, IConfiguration, IServiceCollection, string

### Community 20 - "User Service & Response"
Cohesion: 0.19
Nodes (7): AgroShop.Application.Dto.AttributeOptionDto, AgroShop.Application.Validators.AttributeOptionValidators, AddAttributeOptionDto, Guid, UpdateAttributeOptionDto, AddAttributeOptionDtoValidator, UpdateAttributeOptionDtoValidator

### Community 21 - "Password Value Object"
Cohesion: 0.24
Nodes (5): Regex, Password, IEnumerable, int, Result

### Community 22 - "DbContext Model Snapshot (Migration 10)"
Cohesion: 0.11
Nodes (10): AgroShop.Persistence.Data.Migrations, ModelSnapshot, _4, ModelBuilder, _11, ModelBuilder, MakeCategoryImagePathRequired, ModelBuilder (+2 more)

### Community 23 - "Attribute Entity & Configuration"
Cohesion: 0.18
Nodes (14): SubCategoryService, CancellationToken, Guid, IEnumerable, Result, string, Task, TimeSpan (+6 more)

### Community 24 - "ProductAttribute Entity & Configuration"
Cohesion: 0.18
Nodes (7): RefreshToken, DateTime, Guid, RefreshTokenConfiguration, EntityTypeBuilder, CancellationToken, Task

### Community 25 - "User Entity & Configuration"
Cohesion: 0.24
Nodes (8): AgroShop.API.Middlewares, Exception, HttpContext, RequestDelegate, ExceptionHandler, ILogger, IWebHostEnvironment, Task

### Community 26 - "User Repository Implementation"
Cohesion: 0.09
Nodes (18): CategoryMapper, IEnumerable, Category, Guid, IReadOnlyCollection, List, Result, CategoryName (+10 more)

### Community 27 - "Email Value Object"
Cohesion: 0.22
Nodes (7): ActionExecutingContext, ActionExecutionDelegate, AgroShop.API.Filters, AgroShop.API.Extensions, IAsyncActionFilter, ValidationFilter, Task

### Community 28 - "Phone Value Object"
Cohesion: 0.16
Nodes (17): Option, ProductAttributeId, IImageStorageService, CancellationToken, IFormFile, Task, ProductService, CancellationToken (+9 more)

### Community 29 - "FirstName Value Object"
Cohesion: 0.14
Nodes (5): AgroShop.Application.Dto.ProductAttributeDto, AgroShop.Application.Services, AgroShop.Application.Mappers, AgroShop.API.Controllers, AgroShop.Application.Interfaces

### Community 30 - "20260705114002_10.Designer.cs"
Cohesion: 0.16
Nodes (11): ProductMapper, IEnumerable, Product, DateTime, Guid, ICollection, Result, PackageUnit (+3 more)

### Community 31 - "SubCategory"
Cohesion: 0.15
Nodes (16): AttributeService, CancellationToken, IEnumerable, Result, string, Task, TimeSpan, UnitResult (+8 more)

### Community 33 - "Migration 1 - Initial (Up/Down)"
Cohesion: 0.12
Nodes (6): Error, string, Email, General, Patronymic, SupplierName

### Community 34 - "Migration 2 (Up/Down)"
Cohesion: 0.33
Nodes (6): IConnectionMultiplexer, IDatabase, RedisCacheService, CancellationToken, Task, TimeSpan

### Community 38 - "Migration 6 (Up/Down)"
Cohesion: 0.07
Nodes (38): ControllerBase, ApplicationController, IActionResult, Result, UnitResult, AttributeController, CancellationToken, HttpDelete (+30 more)

### Community 40 - "Migration 8 (Up/Down)"
Cohesion: 0.19
Nodes (13): CategoryService, CancellationToken, IEnumerable, Result, string, Task, TimeSpan, UnitResult (+5 more)

### Community 43 - "Migration 1 Designer Snapshot"
Cohesion: 0.44
Nodes (4): IUserRepository, CancellationToken, Guid, Task

### Community 44 - "ValueObject"
Cohesion: 0.33
Nodes (4): Email, IEnumerable, int, Result

### Community 45 - "Migration 3 Designer Snapshot"
Cohesion: 0.33
Nodes (4): FirstName, IEnumerable, int, Result

### Community 46 - "Migration 4 Designer Snapshot"
Cohesion: 0.22
Nodes (5): Migration, _5, MigrationBuilder, _6, MigrationBuilder

### Community 47 - "Migration 5 Designer Snapshot"
Cohesion: 0.10
Nodes (9): Attribute, Category, Errors, PackageSize, ProductAttribute, StockQuantity, SubCategory, Supplier (+1 more)

### Community 49 - "Migration 7 Designer Snapshot"
Cohesion: 0.33
Nodes (4): LastName, IEnumerable, int, Result

### Community 50 - "Migration 8 Designer Snapshot"
Cohesion: 0.19
Nodes (13): SupplierService, CancellationToken, IEnumerable, Result, string, Task, TimeSpan, UnitResult (+5 more)

### Community 51 - "Migration 9 Designer Snapshot"
Cohesion: 0.33
Nodes (4): Patronymic, IEnumerable, int, Result

### Community 53 - "Paginated Result"
Cohesion: 0.12
Nodes (12): AgroShop.Application.Jwt, AgroShop.Application.Responses, IUserService, CancellationToken, Result, Task, UserResponse, Guid (+4 more)

### Community 55 - "MakeCategoryImagePathRequired"
Cohesion: 0.33
Nodes (4): ActionResult, ModelStateDictionary, ResponseExtensions, ValidationResult

### Community 56 - "AuthController HttpGet Attribute"
Cohesion: 0.12
Nodes (14): AttributeMapper, IEnumerable, Attribute, Guid, ICollection, Result, AttributeValueType, AttributeConfiguration (+6 more)

### Community 57 - "ExceptionHandler"
Cohesion: 0.25
Nodes (5): AgroShop.Infrastructure, AgroShop.Infrastructure.Caching, DependencyInjection, IConfiguration, IServiceCollection

### Community 59 - "AuthController Task Type"
Cohesion: 0.17
Nodes (16): CategoryController, CancellationToken, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult, Task (+8 more)

### Community 86 - "20251016175310_2.Designer.cs"
Cohesion: 0.21
Nodes (9): AgroShop.Application.Dto.AuthDto, LoginUserDto, RegisterUserDto, IAuthService, CancellationToken, Result, Task, UnitResult (+1 more)

### Community 88 - "20260719153341_MakeCategoryImagePathRequired.Designer.cs"
Cohesion: 0.18
Nodes (7): AgroShop.Core.Entities, AgroShop.Persistence.Extensions, AgroShop.Persistence.Repositories, AgroShop.Core.Interfaces, AgroShop.Persistence.Data, IRoleRepository, RoleRepository

### Community 92 - "RedisCacheService"
Cohesion: 0.18
Nodes (8): AgroShop.Application.Dto.CategoryDto, AgroShop.Application.Validators.CategoryValidators, AddCategoryDto, IFormFile, UpdateCategoryDto, IFormFile, AddCategoryDtoValidator, UpdateCategoryDtoValidator

### Community 93 - "ProductAttribute"
Cohesion: 0.19
Nodes (12): ICacheService, CancellationToken, Task, TimeSpan, ProductAttributeService, CancellationToken, IEnumerable, Result (+4 more)

### Community 94 - "ProductAttributeValue"
Cohesion: 0.12
Nodes (15): ProductAttributeValue, Guid, Result, IProductAttributeValueRepository, CancellationToken, Guid, IEnumerable, Task (+7 more)

### Community 95 - "CategoryRepository"
Cohesion: 0.33
Nodes (4): SubCategoryName, IEnumerable, int, Result

### Community 96 - ".GetCategoryByIdAsync"
Cohesion: 0.22
Nodes (8): HashSet, User, DateTime, Guid, IReadOnlyCollection, Result, UserConfiguration, EntityTypeBuilder

### Community 97 - ".IncludeAll"
Cohesion: 0.21
Nodes (7): DbContext, DbSet, IDesignTimeDbContextFactory, AgroShopDbContext, ModelBuilder, AgroShopDbContextFactory, IConfiguration

### Community 99 - "AgroShop.Application.Dto.AttributeDto"
Cohesion: 0.22
Nodes (6): AgroShop.API.Responses, Envelope, DateTime, IEnumerable, List, ResponseError

### Community 100 - ".GenerateTokensAsync"
Cohesion: 0.11
Nodes (22): ProductController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+14 more)

### Community 101 - "20260127152943_5.Designer.cs"
Cohesion: 0.13
Nodes (10): Role, Guid, CancellationToken, Guid, Task, RoleConfiguration, EntityTypeBuilder, CancellationToken (+2 more)

### Community 103 - "20260719153341_MakeCategoryImagePathRequired.Designer.cs"
Cohesion: 0.33
Nodes (5): ProductRepository, CancellationToken, Guid, IEnumerable, Task

### Community 106 - "RoleConstants.cs"
Cohesion: 0.24
Nodes (6): AgroShop.Application.Dto.AttributeDto, AgroShop.Application.Validators.AttributeValidators, AddAttributeDto, UpdateAttributeDto, AddAttributeDtoValidator, UpdateAttributeDtoValidator

### Community 107 - "Program.cs"
Cohesion: 0.22
Nodes (6): AgroShop.API.Services, ImageStorageService, CancellationToken, IFormFile, IWebHostEnvironment, Task

### Community 108 - "HttpGet"
Cohesion: 0.31
Nodes (6): CookieOptions, DateTimeOffset, AuthCookieService, HttpResponse, int, string

### Community 109 - "HttpPost"
Cohesion: 0.33
Nodes (4): ProductDescription, IEnumerable, int, Result

### Community 110 - "HttpPut"
Cohesion: 0.33
Nodes (4): ProductName, IEnumerable, int, Result

### Community 113 - "ICollection"
Cohesion: 0.31
Nodes (6): SubCategoryMapper, IEnumerable, SubCategory, Guid, ICollection, Result

### Community 114 - "int"
Cohesion: 0.25
Nodes (6): ProductAttributeMapper, IEnumerable, ProductAttribute, Guid, ICollection, Result

### Community 115 - ".GenerateTokensAsync"
Cohesion: 0.33
Nodes (5): IProductAttributeRepository, CancellationToken, Guid, IEnumerable, Task

### Community 116 - "SupplierName"
Cohesion: 0.33
Nodes (5): ProductAttributeRepository, CancellationToken, Guid, IEnumerable, Task

### Community 120 - "20260726111737_ReworkProductAndSupplier.Designer.cs"
Cohesion: 0.33
Nodes (4): Sku, IEnumerable, int, Result

### Community 121 - "Image"
Cohesion: 0.12
Nodes (4): AgroShop.Core.ValueObjects, AgroShop.Core.Shared, AgroShop.Core.Enums, ErrorType

### Community 124 - "StockQuantity"
Cohesion: 0.40
Nodes (3): StockQuantity, IEnumerable, Result

### Community 130 - "20260711114130_11.Designer.cs"
Cohesion: 0.17
Nodes (8): AttributeName, IEnumerable, int, Result, Money, IEnumerable, Result, ValueObject

### Community 132 - "Product"
Cohesion: 0.15
Nodes (4): AttributeName, AttributeOption, Image, ProductName

### Community 134 - ".GetRefreshTokenByHashAsync"
Cohesion: 0.33
Nodes (5): SubCategoryRepository, CancellationToken, Guid, IEnumerable, Task

### Community 135 - "EntityTypeBuilder"
Cohesion: 0.08
Nodes (28): AttributeOptionMapper, IEnumerable, AttributeOptionService, CancellationToken, Guid, IEnumerable, Result, string (+20 more)

### Community 144 - "AddProductDto"
Cohesion: 0.10
Nodes (17): AgroShop.Application.Validators.ProductValidators, AgroShop.Application.Dto.ProductDto, AddProductDto, Guid, IFormFile, List, ProductFilterGroupDto, Guid (+9 more)

## Knowledge Gaps
- **40 isolated node(s):** `net10.0`, `FluentValidation.DependencyInjectionExtensions (12.1.1)`, `Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9)`, `Microsoft.AspNetCore.OpenApi (10.0.9)`, `Swashbuckle.AspNetCore (10.2.3)` (+35 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **47 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Error` connect `Migration 1 - Initial (Up/Down)` to `Domain Errors & Validation Results`, `Controllers, Envelope & Category Interface`, `20260711114130_11.Designer.cs`, `Product`, `Auth Service, JWT Handler & User Repo`, `Phone`, `Auth & Category Validators (FluentValidation)`, `EntityTypeBuilder`, `JWT Token Handler & Refresh Tokens`, `Role Entity & DbContext Infrastructure`, `ProductDescription`, `SubCategoryName`, `Core Repository Interfaces & Persistence Impls`, `Application Service Interfaces & Impls`, `Response Extensions & Validation Filter`, `Auth Cookie Service`, `Product`, `ProductAttributeValue`, `Password Value Object`, `Attribute Entity & Configuration`, `User Repository Implementation`, `Phone Value Object`, `20260705114002_10.Designer.cs`, `SubCategory`, `Migration 6 (Up/Down)`, `Migration 8 (Up/Down)`, `ValueObject`, `Migration 3 Designer Snapshot`, `Migration 5 Designer Snapshot`, `Migration 7 Designer Snapshot`, `Migration 8 Designer Snapshot`, `Migration 9 Designer Snapshot`, `Paginated Result`, `LastName`, `MakeCategoryImagePathRequired`, `AuthController HttpGet Attribute`, `AuthController Task Type`, `20251016175310_2.Designer.cs`, `ProductAttribute`, `ProductAttributeValue`, `CategoryRepository`, `.GetCategoryByIdAsync`, `.GenerateTokensAsync`, `.GetUserByIdAsync`, `HttpPost`, `HttpPut`, `ICollection`, `int`, `20260726111737_ReworkProductAndSupplier.Designer.cs`, `Image`, `PackageSize`, `ISubCategoryService`, `StockQuantity`, `CategoryName`?**
  _High betweenness centrality (0.357) - this node is a cross-community bridge._
- **Why does `AgroShop.Persistence.Data` connect `20260719153341_MakeCategoryImagePathRequired.Designer.cs` to `.AddInfrastructure`, `20260222150331_7.Designer.cs`, `20260726130523_AddPackageSizeToProduct.Designer.cs`, `Dependency Injection & Program Entry`, `AgroShop.Application.Dto.SubCategoryDto`, `20260222150331_7.Designer.cs`, `20260726132842_ReworkAttributeSystemWithOptions.Designer.cs`, `SubCategory`, `20260725201921_ReworkSubCategoryNameAndUniqueness.Designer.cs`, `DbContext Model Snapshot (Migration 10)`, `Migration 6 Designer Snapshot`, `Email`, `DependencyInjection String Type`, `20251026150053_3.Designer.cs`, `.IncludeAll`, `20260307133355_8.Designer.cs`, `20260725202611_AlignCategoryNameMaxLengthWithDomain.Designer.cs`, `20260127152943_5.Designer.cs`, `20260308131958_9.Designer.cs`?**
  _High betweenness centrality (0.228) - this node is a cross-community bridge._
- **Why does `AgroShop.Core.Shared` connect `Image` to `Supplier Feature (Entity/Repo/Service)`, `AgroShop.Application.Dto.AttributeDto`, `20260127152943_5.Designer.cs`, `JWT Token Handler & Refresh Tokens`, `RoleConstants.cs`, `Dependency Injection & Program Entry`, `Migration 5 Designer Snapshot`, `AddProductDto`, `int`, `EF Entity Type Configurations`, `User Service & Response`, `Paginated Result`, `20251016175310_2.Designer.cs`, `20260719153341_MakeCategoryImagePathRequired.Designer.cs`, `RedisCacheService`, `FirstName Value Object`, `ProductAttributeValue`?**
  _High betweenness centrality (0.138) - this node is a cross-community bridge._
- **What connects `net10.0`, `FluentValidation.DependencyInjectionExtensions (12.1.1)`, `Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9)` to the rest of the system?**
  _40 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Supplier Feature (Entity/Repo/Service)` be split into smaller, more focused modules?**
  _Cohesion score 0.11333333333333333 - nodes in this community are weakly interconnected._
- **Should `SubCategory Feature (Entity/Repo/Service)` be split into smaller, more focused modules?**
  _Cohesion score 0.13071895424836602 - nodes in this community are weakly interconnected._
- **Should `Auth Service, JWT Handler & User Repo` be split into smaller, more focused modules?**
  _Cohesion score 0.08064516129032258 - nodes in this community are weakly interconnected._