# **SOLID** คือหลักการพื้นฐาน 5 ข้อที่ใช้ในการออกแบบ **Object-Oriented Programming (OOP)** และเป็นหัวใจของ **Clean Code**, **Maintainable Code** และ **Clean Architecture** ที่เราลังใช้ในระบบ `ProductAPI`😎

#

## 🧠 **SOLID คืออะไร?**

**SOLID** เป็นตัวย่อของหลักการ 5 ข้อที่ช่วยให้การเขียนโปรแกรมมีความ:

- ยืดหยุ่น (Flexible)
- ทดสอบง่าย (Testable)
- แก้ไขขยายได้ง่าย (Extensible)
- ไม่ทำให้โค้ดพังทั้งระบบ (Maintainable)

#

## 🔠 ย่อมาจากอะไร?

| อักษร | ชื่อเต็ม | ความหมายหลัก |
|-------|----------|----------------|
| **S** | **Single Responsibility Principle (SRP)** | 1 class ควรมีหน้าที่ 1 อย่าง |
| **O** | **Open/Closed Principle (OCP)** | เปิดให้ขยาย (Extend), ปิดการแก้ไข (Modify) |
| **L** | **Liskov Substitution Principle (LSP)** | ใช้ subclass แทน superclass แล้วต้องยังทำงานได้ถูกต้อง |
| **I** | **Interface Segregation Principle (ISP)** | ไม่ควรบังคับให้ class ใช้ method ที่ไม่จำเป็น |
| **D** | **Dependency Inversion Principle (DIP)** | พึ่งพา abstraction (interface) แทนการพึ่ง implementation ตรง ๆ |

#

## 🔍 อธิบายทีละตัวแบบเข้าใจง่าย

### ✅ S – Single Responsibility Principle  
> **"ทำหน้าที่เดียวเท่านั้น"**

**ผิด:**
```csharp
public class ProductService {
    public void AddProduct() { /* logic */ }
    public void LogAction() { /* logging */ }
}
```

**ถูก:**
```csharp
public class ProductService { public void AddProduct() { /* logic */ } }
public class Logger { public void LogAction() { /* logging */ } }
```

#

### ✅ O – Open/Closed Principle  
> **"เปิดให้ขยาย ปิดการแก้ไข"**

**ผิด:**
```csharp
public class DiscountService {
    public double GetDiscount(string type) {
        if (type == "VIP") return 0.2;
        else if (type == "Normal") return 0.1;
        return 0;
    }
}
```
#
**ถูก (ใช้ Polymorphism):**
```csharp
public interface IDiscountStrategy { double GetDiscount(); }

public class VipDiscount : IDiscountStrategy { public double GetDiscount() => 0.2; }
public class NormalDiscount : IDiscountStrategy { public double GetDiscount() => 0.1; }
```

#

### ✅ L – Liskov Substitution Principle  
> **"Subclass ควรแทนที่ Superclass ได้อย่างสมบูรณ์"**

**ผิด:**
```csharp
public class Bird { public virtual void Fly() {} }
public class Ostrich : Bird { public override void Fly() { throw new NotSupportedException(); } }
```

**ถูก:**
```csharp
public abstract class Bird {}
public class FlyingBird : Bird { public void Fly() {} }
public class Ostrich : Bird { }
```

#

### ✅ I – Interface Segregation Principle  
> **"Interface ต้องเล็กและเฉพาะเจาะจง"**

**ผิด:**
```csharp
public interface IAnimal {
    void Walk();
    void Swim();
    void Fly();
}
```

**ถูก:**
```csharp
public interface IWalker { void Walk(); }
public interface ISwimmer { void Swim(); }
public interface IFlyer  { void Fly(); }
```

#

### ✅ D – Dependency Inversion Principle  
> **"พึ่งพา abstraction ไม่ใช่ concrete class"**

**ผิด:**
```csharp
public class ProductController {
    private ProductService _service = new ProductService();
}
```

**ถูก (ใช้ Interface + DI):**
```csharp
public class ProductController {
    private readonly IProductService _service;
    public ProductController(IProductService service) {
        _service = service;
    }
}
```

#

## 💡 สรุปภาพรวม
```text
S: Class เดียวควรทำหน้าที่เดียว
O: เพิ่มความสามารถโดยไม่แก้โค้ดเดิม
L: Subclass ใช้แทน Superclass ได้
I: Interface ต้องแยกย่อยไม่บังคับของไม่จำเป็น
D: พึ่ง abstraction ไม่พึ่ง implementation ตรง
```
#
หากคุณต้องการ Slide, Infographic หรือ Code Example ที่รวมการใช้ SOLID ทั้ง 5 ข้อในระบบจริง (เช่น ProductService) ผมจัดให้ได้เลยนะครับ 🙌
