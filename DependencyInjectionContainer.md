# Register Service

ใน `Program.cs` หรือ `Startup.cs` ของ ASP.NET Core WebAPI (หรือ .NET ทั่วไป) เวลา Register Service กับ Dependency Injection (DI) Container ผ่าน `services.AddXXX<T>()` จะมี **3 แบบหลักๆ** ได้แก่:

#

## ✅ 1. `AddSingleton<TInterface, TImplementation>()`

### 🔁 Lifetime: ตลอดอายุของแอป (Application Lifetime)

- ถูกสร้าง **แค่ครั้งเดียว** ตั้งแต่แอปรันครั้งแรก
- ใช้ instance เดิม **ทุกๆ request ทุกๆ thread**

### 🟢 เหมาะกับ:
- Class ที่ไม่มี state (Stateless)
- Class ที่ต้องการแชร์ cache, config, หรือ static data
- เช่น: `ILogger`, `IConfiguration`, `HttpClientFactory`

### 🔴 ไม่เหมาะกับ:
- Class ที่ต้องมี state per-user หรือ per-request
- ใช้งานกับ Database Context (เช่น EF Core DbContext) เพราะอาจเกิด Race Condition

#

## ✅ 2. `AddScoped<TInterface, TImplementation>()`

### 🔁 Lifetime: **ต่อ 1 Request**

- จะสร้าง instance ใหม่ทุกครั้งที่มี HTTP request ใหม่
- แต่ภายใน 1 request เดียวกัน จะใช้ instance เดิม

### 🟢 เหมาะกับ:
- Service ที่ต้องการ context เฉพาะ user เช่น DbContext
- ใช้ใน WebAPI controller ทั่วไป
- ทำงานกับข้อมูล request เช่น `User`, `Token`, หรือ `RequestId`

### 🔴 ไม่เหมาะกับ:
- Background Task ที่รันอยู่นอก Request (เช่น Worker Service)

#

## ✅ 3. `AddTransient<TInterface, TImplementation>()`

### 🔁 Lifetime: **ใหม่ทุกครั้งที่มีการเรียกใช้**

- ทุกครั้งที่มีการ `resolve` จะสร้าง instance ใหม่เสมอ

### 🟢 เหมาะกับ:
- Lightweight Service ที่ไม่ต้องแชร์ state
- Service ที่มี logic เฉพาะงาน เช่น EmailSender, SMSNotifier

### 🔴 ไม่เหมาะกับ:
- Class ที่ต้องใช้งานซ้ำ หรือมีค่าคงที่ (อาจทำให้เปลือง resource)
- Service ที่ต้องแชร์ connection หรือ context เช่น DB

#

## 🔍 ตัวอย่างการใช้:

```csharp
services.AddSingleton<IConfigService, ConfigService>();
services.AddScoped<IUserService, UserService>();
services.AddTransient<IEmailSender, EmailSender>();
```

#

## 🔄 ตารางสรุป:

| Register Type | Lifetime | สร้างใหม่เมื่อ | เหมาะกับ |
|---------------|----------|----------------|-----------|
| `Singleton`   | ตลอดอายุแอป | ครั้งแรกที่เรียก | Config, Logger, Cache |
| `Scoped`      | ต่อ Request | ทุก HTTP Request | DBContext, Business Logic |
| `Transient`   | ทุกครั้ง | ทุก `resolve` | Lightweight, Stateless Services |
#
