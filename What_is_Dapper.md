**Dapper** คือ **Micro ORM (Object-Relational Mapper)** ที่พัฒนาโดยทีม Stack Overflow ซึ่งใช้กับภาษา C# (หรือ .NET) เพื่อช่วยในการดึงและบันทึกข้อมูลจากฐานข้อมูลเชิงสัมพันธ์ เช่น SQL Server, Oracle, MySQL ฯลฯ โดย **เน้นความเร็วและประสิทธิภาพ**

---

### 🔍 สรุปแบบง่าย ๆ

- ✅ เป็น **ORM เบา ๆ (Micro ORM)**
- ✅ เร็วมาก เพราะใช้ **Raw SQL** แล้วแมปผลลัพธ์กลับมาเป็น Object
- ✅ เหมาะกับคนที่อยากควบคุม SQL เองเต็มที่
- ❌ ไม่จัดการเรื่อง Relation แบบ EF Core เช่น Auto Lazy Load หรือ Change Tracking

---

### 🔧 ตัวอย่างการใช้งาน Dapper

สมมุติว่าเรามีตาราง `Products`

```sql
SELECT * FROM Products WHERE Id = @Id
```

โค้ด C# ด้วย Dapper:

```csharp
using (var connection = new OracleConnection("your_connection_string"))
{
    string sql = "SELECT * FROM Products WHERE Id = :Id";
    var product = connection.QueryFirstOrDefault<Product>(sql, new { Id = 1 });
}
```

---

### ✅ ข้อดีของ Dapper

| ข้อดี | รายละเอียด |
|-------|-------------|
| 🔥 เร็วมาก | เกือบเท่ากับ ADO.NET เพราะไม่มี Overhead |
| 💡 ใช้ง่าย | ใช้ SQL ตรง ๆ ไม่ต้องเรียนรู้ DSL |
| 🔄 Mapping อัตโนมัติ | แมปผลลัพธ์จาก DB ไปยัง C# class ได้ทันที |
| 🧩 Flexible | ควบคุม SQL และ Transaction ได้เต็มที่ |
| 🤝 รองรับหลาย DB | ใช้กับ SQL Server, Oracle, MySQL, PostgreSQL ได้ |

---

### ❌ ข้อจำกัดของ Dapper

| ข้อจำกัด | รายละเอียด |
|-----------|--------------|
| ❌ ไม่ Auto Track Entity | ต้องเขียน SQL เองหมด |
| ❌ ไม่มี Migration | ไม่มีระบบจัดการ Schema แบบ EF Core |
| ❌ ไม่มี Lazy Load | ต้อง JOIN เองหากต้องการดึงข้อมูลหลายตาราง |

---

ถ้าคุณต้องการ ORM ที่เร็วและอยากควบคุม SQL เองทั้งหมด (เหมาะกับระบบ Enterprise หรือ Legacy DB) — **Dapper คือทางเลือกที่ยอดเยี่ยม**  
แต่ถ้าคุณต้องการ ORM ที่ "ดูแลให้หมด" (เช่น EF Core) Dapper อาจจะไม่ตอบโจทย์ครับ

---

อยากดูตัวอย่างโค้ด CRUD แบบเต็ม ๆ ด้วย Dapper ไหม?
