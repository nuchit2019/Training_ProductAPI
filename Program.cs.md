
# Program.cs และ DI Container (Dependency Injection Container) 
**Loose Coupling** คือหัวใจของการออกแบบ Software ที่ดี โดยเฉพาะกับแนวคิดเช่น **SOLID**, **DI (Dependency Injection)** และ **Clean Architecture** ที่เราใช้อยู่ 😄
#

## 🧩 **Loose Coupling คืออะไร?**

**Loose Coupling** หรือ "การเชื่อมโยงกันอย่างหลวม" คือการที่ **แต่ละส่วนของระบบไม่พึ่งพากันโดยตรงมากเกินไป**  
พูดง่าย ๆ คือ **Component หนึ่งสามารถเปลี่ยนแปลงได้ โดยไม่กระทบกับ Component อื่น**

---

### 🔍 ยกตัวอย่างให้เห็นภาพง่าย ๆ:

#### ❌ Tight Coupling (ผูกติดแน่นเกินไป)
```csharp
public class ProductController
{
    private readonly ProductService _productService = new ProductService(); // ผูกตรง ๆ

    public void GetProducts() => _productService.GetAll();
}
```
- ถ้าเปลี่ยนชื่อ class หรือ logic ใน `ProductService` → ต้องแก้ที่ `ProductController` ด้วย
- Unit Test ทำได้ยาก เพราะไม่สามารถ Mock ได้

---

#### ✅ Loose Coupling (ผูกหลวม ใช้ Interface)
```csharp
public class ProductController
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    public void GetProducts() => _productService.GetAll();
}
```
- `Controller` ไม่รู้ว่าเบื้องหลังคือ `ProductService` หรือของใคร
- แค่รู้ว่าใช้ `IProductService` ก็พอ
- ทำให้สามารถเปลี่ยน implementation ได้อย่างยืดหยุ่น
- ทดสอบง่าย → Inject Mock Service ได้เลย

---

## ✅ ข้อดีของ Loose Coupling

| ข้อดี | คำอธิบาย |
|-------|----------|
| **ดูแลง่าย** | เปลี่ยนหรือแก้โค้ดส่วนหนึ่งโดยไม่กระทบอีกส่วน |
| **ทดสอบง่าย** | Mock หรือ Stub dependencies ได้ |
| **ยืดหยุ่น** | เปลี่ยน Implementation ได้สบาย เช่นจาก Local เป็น Remote |
| **ขยายระบบง่าย** | เพิ่มความสามารถใหม่โดยไม่ต้องรื้อโครงสร้างเดิม |

---

## 🔧 ตัวช่วยที่ทำให้ Loose Coupling เป็นจริง

- **Interface** – ทำให้ไม่ผูกกับ Implementation ตรง ๆ
- **DI Container** – ช่วย inject dependency ให้โดยอัตโนมัติ
- **SOLID Principle** – โดยเฉพาะ "D" = **Dependency Inversion Principle**

---

ถ้าต้องการให้ช่วยวาด Diagram เปรียบเทียบ Tight vs Loose Coupling หรืออธิบายให้ทีมเข้าใจได้ง่าย ผมช่วยจัดให้ได้นะครับ 😄
