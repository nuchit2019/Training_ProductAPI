ตัวอย่างระบบ **จัดการข้อมูลสินค้า (Product Management)** ด้วย **C# WebAPI (.NET 8)**, เชื่อมต่อ Oracle ผ่าน **Dapper**, และออกแบบตามแนวทาง **Clean Architecture** โดยใช้ **Visual Studio 2022**

---

## ✅ 1. สร้าง Solution & Project (VS2022)

### 🧱 โครงสร้าง Solution (Clean Architecture)

```
ProductAPI
├── src
│   ├── ProductAPI             # WebAPI
│   ├── ProductAPI.Application     # Business Logic (UseCases)
│   ├── ProductAPI.Domain          # Entities, Interfaces
│   └── ProductAPI.Infrastructure  # Dapper + Oracle
└── tests
    └── ProductAPI.Tests           # Unit Test
```

---

## ✅ 2. สร้าง Project ด้วย Visual Studio 2022

1. เปิด Visual Studio 2022
2. File → New → Project → เลือก **ASP.NET Core Web API**
   - Project Name: `ProductAPI.API`
   - Framework: `.NET 8`
   - Uncheck: "Use controllers" → (จะใช้ Minimal API หรือ Clean Architecture Style)
3. เพิ่ม Class Library (.NET 8) ทั้งหมด:
   - `ProductAPI.Application`
   - `ProductAPI.Domain`
   - `ProductAPI.Infrastructure`
   - `ProductAPI.Tests` (เลือก xUnit)

---

## ✅ 3. ติดตั้ง NuGet Packages

### ✅ ติดตั้งใน Project `Infrastructure`

```bash
dotnet add ProductAPI.Infrastructure package Dapper
dotnet add ProductAPI.Infrastructure package Oracle.ManagedDataAccess.Core
```

### ✅ ติดตั้งใน Project `API`

```bash
dotnet add ProductAPI.API package Microsoft.Extensions.DependencyInjection.Abstractions
```

---

## ✅ 4. เริ่มเขียน Code ตาม Layer

---

## 🧩 Domain Layer (ProductAPI.Domain)

### 🔸 Entities/Product.cs

```csharp
namespace ProductAPI.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

### 🔸 Interfaces/IProductRepository.cs

```csharp
namespace ProductAPI.Domain.Interfaces;

using ProductAPI.Domain.Entities;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<int> AddAsync(Product product);
    Task<bool> UpdateAsync(Product product);
    Task<bool> DeleteAsync(int id);
}
```

---

## 🧩 Application Layer (ProductAPI.Application)

### 🔸 Interfaces/IProductService.cs

```csharp
using ProductAPI.Domain.Entities;

namespace ProductAPI.Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<int> AddAsync(Product product);
    Task<bool> UpdateAsync(Product product);
    Task<bool> DeleteAsync(int id);
}
```

### 🔸 Services/ProductService.cs

```csharp
using ProductAPI.Application.Interfaces;
using ProductAPI.Domain.Entities;
using ProductAPI.Domain.Interfaces;

namespace ProductAPI.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Product>> GetAllAsync() => _repository.GetAllAsync();
    public Task<Product?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
    public Task<int> AddAsync(Product product) => _repository.AddAsync(product);
    public Task<bool> UpdateAsync(Product product) => _repository.UpdateAsync(product);
    public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
}
```

---

## 🧩 Infrastructure Layer (ProductAPI.Infrastructure)

### 🔸 Repositories/ProductRepository.cs

```csharp

 using Oracle.ManagedDataAccess.Client;
using ProductAPI.Domain.Entities;
using System.Data;
using Dapper;
using ProductAPI.Domain.Interfaces;

namespace ProductAPI.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection Connection => new OracleConnection(_connectionString);

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var sql = "SELECT * FROM TNIRPT.TEST_PRODUCTS";
            using var conn = Connection;
            return await conn.QueryAsync<Product>(sql);
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM TNIRPT.TEST_PRODUCTS WHERE ID = :Id";
            using var conn = Connection;
            return await conn.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });
        }

        public async Task<int> AddAsync(Product product)
        {
            var sql = @"
                    INSERT INTO TNIRPT.TEST_PRODUCTS (NAME, DESCRIPTION, PRICE, CREATED_AT)
                    VALUES (:Name, :Description, :Price, SYSDATE)
                    RETURNING ID INTO :Id";

            using var conn = Connection;
            var parameters = new DynamicParameters();
            parameters.Add("Name", product.Name);
            parameters.Add("Description", product.Description);
            parameters.Add("Price", product.Price);
            parameters.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await conn.ExecuteAsync(sql, parameters);
            return parameters.Get<int>("Id");
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            var sql = @"UPDATE TNIRPT.TEST_PRODUCTS SET NAME = :Name, DESCRIPTION = :Description, PRICE = :Price
                    WHERE ID = :Id";
            using var conn = Connection;
            var rows = await conn.ExecuteAsync(sql, product);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DELETE FROM TNIRPT.TEST_PRODUCTS WHERE ID = :Id";
            using var conn = Connection;
            var rows = await conn.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}


```

---

## 🧩 API Layer (ProductAPI.API)

### 🔸 Program.cs

```csharp
using ProductAPI.Application.Interfaces;
using ProductAPI.Application.Services;
using ProductAPI.Domain.Interfaces;
using ProductAPI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("OracleDb")!;
builder.Services.AddScoped<IProductRepository>(_ => new ProductRepository(connectionString));
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();

```

### 🔸 appsettings.json

```json
{
  "ConnectionStrings": {
    "OracleDb": "User Id=your_user;Password=your_pass;Data Source=your_db"
  }
}
```

---

## ✅ 5. ตัวอย่าง Table `PRODUCTS` (Oracle)

```sql
CREATE TABLE PRODUCTS (
    ID NUMBER GENERATED BY DEFAULT AS IDENTITY PRIMARY KEY,
    NAME VARCHAR2(100),
    DESCRIPTION VARCHAR2(500),
    PRICE NUMBER(10,2),
    CREATED_AT DATE DEFAULT SYSDATE
);
```

---
