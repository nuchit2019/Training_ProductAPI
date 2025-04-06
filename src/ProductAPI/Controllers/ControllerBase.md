  
# 🔍 `ControllerBase` เป็นคลาสพื้นฐานสำหรับ API Controller ใน ASP.NET Core  
โดยไม่รวม View (เหมือน `Controller` สำหรับ MVC)  
เน้นตอบสนอง **Web API (JSON / XML)** อย่างเดียว

#

### ✅ ฟีเจอร์เด่นของ `ControllerBase`

#### 1. **Helper Methods สำหรับส่งผลลัพธ์กลับ**
ใช้สำหรับสร้าง `HTTP Response` ได้สะดวก เช่น:

| Method                 | คำอธิบาย                                          |
|------------------------|--------------------------------------------------|
| `Ok(object value)`     | คืนค่า HTTP 200 OK พร้อมข้อมูล                   |
| `Created(...)`         | คืนค่า HTTP 201 Created                          |
| `BadRequest(...)`      | คืนค่า HTTP 400 Bad Request พร้อม error message |
| `NotFound(...)`        | คืนค่า HTTP 404 Not Found                        |
| `NoContent()`          | คืนค่า HTTP 204 No Content (เช่นหลังจากลบสำเร็จ)|
| `Unauthorized()`       | คืนค่า HTTP 401 Unauthorized                     |
| `Forbid()`             | คืนค่า HTTP 403 Forbidden                        |

#

#### 2. **ModelState Validation**
เช็คความถูกต้องของข้อมูลที่ส่งมาใน `Request Body`
```csharp
if (!ModelState.IsValid)
{
    return BadRequest(ModelState);
}
```

#

#### 3. **[FromBody], [FromQuery], [FromRoute]**
ใช้ใน parameter ของ method เพื่อ map ค่าจาก HTTP request

| Attribute      | รับค่าจากไหน            |
|----------------|-------------------------|
| `[FromBody]`   | จาก JSON ใน body        |
| `[FromQuery]`  | จาก query string        |
| `[FromRoute]`  | จาก URL route parameter |
| `[FromHeader]` | จาก HTTP header         |
| `[FromForm]`   | จาก `multipart/form-data`|

---

#### 4. **Access HTTP Context**
เข้าถึง `HttpContext`, `Request`, `Response`, `User` ได้ทันที เช่น:
```csharp
var userId = HttpContext.User.Identity.Name;
```

#

#### 5. **ActionResult<T> (Generic Return Type)**
ใช้กับ ASP.NET Core 2.1+ เพื่อให้การตอบกลับมี strongly-typed และยืดหยุ่น
```csharp
public ActionResult<Product> GetProduct(int id)
{
    var product = _service.Get(id);
    if (product == null)
        return NotFound();
    return product;
}
```

#

### 📌 เปรียบเทียบ:
| ชื่อคลาส        | ใช้ในอะไร             | มี View Engine ไหม |
|-----------------|------------------------|---------------------|
| `ControllerBase`| Web API (RESTful)      | ❌ ไม่มี View       |
| `Controller`    | MVC (Web Application)  | ✅ มี View (.cshtml)|

#
😎
