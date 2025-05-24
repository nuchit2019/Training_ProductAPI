# CRUD Product 
ตัวอย่างระบบ **จัดการข้อมูลสินค้า (CRUD Product)** ด้วย **C# WebAPI (.NET 8)**, เชื่อมต่อ Oracle ผ่าน **Dapper**, และออกแบบตามแนวทาง **Clean Architecture** โดยใช้ **Visual Studio 2022**
# CRUD ย่อมาจาก:

- **C** = **Create** → สร้างข้อมูลใหม่ (เช่น เพิ่มสินค้าใหม่)  
- **R** = **Read** → อ่านหรือดึงข้อมูล (เช่น ดูรายละเอียดสินค้า)  
- **U** = **Update** → แก้ไขข้อมูล (เช่น แก้ชื่อหรือราคาสินค้า)  
- **D** = **Delete** → ลบข้อมูล (เช่น ลบสินค้าที่เลิกขาย)

#
.NET Core เริ่มเปิดตัวครั้งแรกใน **ปี 2016** โดย Microsoft

#

## 🔹 จุดประสงค์หลักของการสร้าง .NET Core คืออะไร?

1. **Cross-platform**  
   - ให้สามารถรันได้บน **Windows**, **Linux**, และ **macOS**  
   - ไม่เหมือน .NET Framework เดิมที่รันได้แค่บน Windows เท่านั้น
   - ทำให้ Deploy ได้บน Docker, Kubernetes, Cloud อย่างอิสระ

2. **Open Source**  
   - Microsoft เปิดซอร์สโค้ดทั้งหมดบน GitHub  
   - ให้นักพัฒนาชุมชนทั่วโลกสามารถมีส่วนร่วมได้

3. **Modular Architecture**  
   - เลือกติดตั้งเฉพาะ package ที่ต้องใช้ (ผ่าน NuGet)  
   - ทำให้ขนาดของแอปเล็กลง และโหลดเร็วขึ้น 

4. **Performance ที่ดีขึ้น**  
   - มีการออกแบบ runtime ใหม่ให้เร็วและใช้ทรัพยากรน้อยลง  
   - เบาและเร็วกว่า .NET Framework
   - เหมาะสำหรับ Microservices ที่ต้องการ scale ได้ดี

5. **Cloud-ready และ Container-friendly**  
   - ถูกออกแบบมาให้ทำงานบน Cloud และ Docker ได้อย่างดี  
   - เหมาะกับยุคของ Microservices และ DevOps
## .NET Core  ออกแบบเพื่อ Microservices
#

## 🔸 Timeline (เวอร์ชันหลัก)

| ปี | เวอร์ชัน | หมายเหตุ |
|----|----------|----------|
| 2016 | .NET Core 1.0 | เปิดตัวครั้งแรก |
| 2017 | .NET Core 2.0 | เริ่มได้รับความนิยม |
| 2019 | .NET Core 3.0 | รองรับ Windows Forms และ WPF |
| 2020 | .NET 5 | รวม .NET Core กับ .NET Framework เป็นเวอร์ชันเดียว (เรียกแค่ ".NET") |
| 2021 | .NET 6 (LTS) | Long Term Support |
| 2022 | .NET 7 | Version ปกติ |
| 2023 | .NET 8 (LTS) | ล่าสุด และเป็นเวอร์ชันที่เสถียรที่สุดในตอนนี้ |

Modern.NET
https://github.com/nuchit2019/Training_ProductAPI/blob/main/Modern%20.NET.md
#
 
# CRUD Product Endpoints

# 1. API Endpoints
รายการ **API Endpoints** ที่ได้จาก Controller `ProductController` ซึ่งรองรับ **CRUD** และ **Enquiry** (ค้นหา) สำหรับข้อมูลสินค้า:

#

## ✅ Product API Endpoints

| Method | URL                     | Description                 | Request Body     | Response              |
|--------|-------------------------|-----------------------------|------------------|------------------------|
| GET    | `/api/products`          | ดึงสินค้าทั้งหมด (Enquiry) | -                | `List<ProductDto>`    |
| GET    | `/api/product/{id}`     | ดึงสินค้าตาม ID            | -                | `ProductDto` หรือ 404 |
| POST   | `/api/product`          | เพิ่มสินค้าใหม่ (Create)    | `ProductDto`     | 200 OK                |
| PUT    | `/api/product`          | แก้ไขสินค้า (Update)        | `ProductDto`     | 200 OK                |
| DELETE | `/api/product/{id}`     | ลบสินค้าตาม ID (Delete)     | -                | 200 OK                |

#

## ✅ Model Request/Response
**ตัวอย่าง Request/Response** สำหรับแต่ละ **Product API Endpoint** ที่ได้จาก `ProductController`:

#

## ✅ 1.1 `GET /api/products`

### 📌 Description: ดึงสินค้าทั้งหมด

**Request:**  
```
GET /api/products
```

**Response:**
```json
[
  {
    "id": 101,
    "name": "Laptop Lenovo",
    "price": 25000.00,
    "category": "Electronics"
  },
  {
    "id": 102,
    "name": "iPhone 15",
    "price": 39900.00,
    "category": "Smartphone"
  }
]
```
#

## ✅ 1.2. `GET /api/product/{id}`

### 📌 Description: ดึงสินค้ารายตัวตาม ID

**Request:**  
```
GET /api/product/101
```

**Response (200 OK):**
```json
{
  "id": 101,
  "name": "Laptop Lenovo",
  "price": 25000.00,
  "category": "Electronics"
}
```

**Response (404 Not Found):**
```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.5",
  "title": "Not Found",
  "status": 404,
  "traceId": "00-ba56f638cec2dcfd65f18585bf20da39-21369e5d800f1552-00"
}
```

#

## ✅ 1.3. `POST /api/product`

### 📌 Description: เพิ่มสินค้าใหม่

**Request:**  
```
POST /api/product
Content-Type: application/json
```

**Body:**
```json
{
  "id": 103,
  "name": "Samsung TV",
  "price": 18990.00,
  "category": "Electronics"
}
```

**Response (200 OK):**
```json
{
  "result": "success"
}
```

 
#

## ✅ 1.4. `PUT /api/product`

### 📌 Description: แก้ไขข้อมูลสินค้า

**Request:**  
```
PUT /api/product
Content-Type: application/json
```

**Body:**
```json
{
  "id": 103,
  "name": "Samsung TV 55-inch",
  "price": 19990.00,
  "category": "Electronics"
}
```

**Response (200 OK):**
```json
{
  "result": "success"
}
```

#

## ✅ 1.5. `DELETE /api/product/{id}`

### 📌 Description: ลบสินค้า

**Request:**  
```
DELETE /api/product/103
```

**Response (200 OK):**
```json
{
  "result": "success"
}
```

#
#
# 2. ขั้นตอน สร้าง Project
## ✅ 2.1 สร้าง Blank Solution Solution ด้วย VS2022
![image](https://github.com/user-attachments/assets/7d65e87c-e8bb-40c3-8f3b-c1457d13152d)


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
#
- Project Structure:
   - https://github.com/nuchit2019/Training_ProductAPI/blob/main/ProjectStructure.md

-  Dapper คืออะไร?
   -  https://github.com/nuchit2019/Training_ProductAPI/blob/main/What_is_Dapper.md
 
- Clean Architecture คืออะไร?
   - https://github.com/nuchit2019/Clean-Architecture
 

## ✅ 2.2 สร้าง Project ด้วย Visual Studio 2022
![image](https://github.com/user-attachments/assets/82ca5156-e379-4914-a21c-6870d224e31d)

2.2.1. เปิด Visual Studio 2022

2.2.2. File → New → Project → เลือก **ASP.NET Core Web API**
   - Project Name: `ProductAPI.API`
   - Framework: `.NET 8`
   - Uncheck: "Use controllers" → (จะใช้ Minimal API หรือ Clean Architecture Style)
     
2.2.3. เพิ่ม Class Library (.NET 8) ทั้งหมด:
   - `ProductAPI.Application`
   - `ProductAPI.Domain`
   - `ProductAPI.Infrastructure`
   - `ProductAPI.Tests` (เลือก xUnit)

#

## ✅ 2.3. ติดตั้ง NuGet Packages
ติดตั้งผ่าน VS 2022

![image](https://github.com/user-attachments/assets/aa372257-dd34-4d1c-9995-ccb9acf04cb1)

![image](https://github.com/user-attachments/assets/ae67837f-ff8c-4a48-9ce3-1c9ce86836e8)

### ✅ ติดตั้งใน Project `Infrastructure` 
ติดตั้งผ่าน .NET CLI (Command Line Interface) 

```bash
dotnet add ProductAPI.Infrastructure package Dapper
dotnet add ProductAPI.Infrastructure package Oracle.ManagedDataAccess.Core
```

### ✅ ติดตั้งใน Project `API`
ติดตั้งผ่าน .NET CLI
```bash
dotnet add ProductAPI.API package Microsoft.Extensions.DependencyInjection.Abstractions
```

#
# Step Coding .......

# ✅ 2.4. เริ่มเขียน Code ตาม Layer

#

## 2.4.1 🧩 Domain Layer (ProductAPI.Domain)

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

###  2.4.2🔸 Interfaces/IProductRepository.cs

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

#

##  2.4.3 🧩 Application Layer (ProductAPI.Application)

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

###  2.4.4 🔸 Services/ProductService.cs

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

#

##  2.4.5 🧩 Infrastructure Layer (ProductAPI.Infrastructure)

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

#

##  2.4.6 🧩 API Layer (ProductAPI.API)

### 🔸 ProductController.cs
```csharp
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Application.Interfaces;
using ProductAPI.Domain.Entities;

namespace ProductAPI.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet("api/products")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllAsync();
            return Ok(products);
        }

        
        [HttpGet("api/product/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            return product is not null ? Ok(product) : NotFound();
        }

       
        [HttpPost("api/product")]
        public async Task<IActionResult> Create(Product product)
        {
            var id = await _service.AddAsync(product);
            return CreatedAtAction(nameof(GetById), new { id }, product);
        }

       
        [HttpPut("api/product")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            product.Id = id;
            var updated = await _service.UpdateAsync(product);
            return updated ? NoContent() : NotFound();
        }

       
        [HttpDelete("api/product/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}

```

###  2.4.7🔸 Program.cs
#### Program.cs และ DI Container

https://github.com/nuchit2019/Training_ProductAPI/blob/main/Program.cs.md

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

###  2.4.8🔸 appsettings.json

```json
{
  "ConnectionStrings": {
    "OracleDb": "User Id=your_user;Password=your_pass;Data Source=your_db"
  }
}
```

#

## ✅ 2.5.  Table `PRODUCTS` (Oracle)

```sql
CREATE TABLE PRODUCTS (
    ID NUMBER GENERATED BY DEFAULT AS IDENTITY PRIMARY KEY,
    NAME VARCHAR2(100),
    DESCRIPTION VARCHAR2(500),
    PRICE NUMBER(10,2),
    CREATED_AT DATE DEFAULT SYSDATE
);
#
```
#

> *  Nuchita-นุชิตะ
> *  2025-04-06
> *  ขอบคุณ ChatBot ทุกตัว
