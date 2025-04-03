Project Structure สำหรับระบบ Product API โดยแบ่งออกเป็น 5 Projects ซึ่งอยู่ใน Solution `ProductAPI` รายละเอียดแต่ละ Project มีดังนี้:

#

## ✅ โฟลเดอร์หลัก
### `src/`
เก็บ Source Code หลักของระบบ

#

## 1. **ProductAPI** (Web API Layer)
เป็น Entry Point ของ Application (Web API)

- **Controllers/**  
  - `ProductController.cs`  
    Controller สำหรับรับ HTTP Request เช่น `GET`, `POST`, `PUT`, `DELETE` จากผู้ใช้งาน

- **Program.cs**  
  Entry ของ .NET App — ตั้งค่า DI, Middleware ฯลฯ

- **appsettings.json**  
  เก็บค่าการตั้งค่า เช่น ConnectionString

- **ProductAPI.http**  
  ใช้สำหรับทดสอบ API ด้วย HTTP request ภายใน VSCode/Visual Studio

#

## 2. **ProductAPI.Application** (Application Layer)
เก็บ Logic ทางธุรกิจ (Business Logic)

- **Interfaces/**  
  - `IProductService.cs`  
    Interface ของ Service สำหรับให้ Controller เรียกใช้งาน

- **Services/**  
  - `ProductService.cs`  
    Implement `IProductService` ทำหน้าที่เรียกใช้ Repository และจัดการ Business Logic

#

## 3. **ProductAPI.Domain** (Domain Layer)
เก็บโมเดลหลัก (Entity) และ Interface ของ Repository

- **Entities/**  
  - `Product.cs`  
    Class แทน Product Entity เช่น `Id`, `Name`, `Price`, ฯลฯ

- **Interfaces/**  
  - `IProductRepository.cs`  
    Interface สำหรับการเข้าถึงข้อมูล เช่น `GetAll`, `Add`, `Delete`

#

## 4. **ProductAPI.Infrastructure** (Infrastructure Layer)
เก็บการเชื่อมต่อกับฐานข้อมูล หรือ External Services

- **Repositories/**  
  - `ProductRepository.cs`  
    Implement `IProductRepository` ใช้ Dapper / EF Core / SQL Query ในการดึงข้อมูลจริงจาก DB

#

## 5. **ProductAPI.Test** (Testing Layer)
ใช้สำหรับเขียน Unit Test หรือ Integration Test

#

## 🧠 Flow การทำงานโดยย่อ

```
[Client] 
   ↓ 
[ProductController.cs] ← (รับ HTTP Request)
   ↓ 
[ProductService.cs] ← (Business Logic)
   ↓ 
[ProductRepository.cs] ← (เข้าถึง DB)
   ↓ 
[Oracle / SQL Server / DB]
```

#
