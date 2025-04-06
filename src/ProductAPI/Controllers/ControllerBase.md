  
# 🔍 `ControllerBase` 

`ControllerBase` เป็นคลาสพื้นฐานสำหรับ API Controller ใน ASP.NET Core  
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

#
แน่นอนครับ! 🔥  
นี่คือตัวอย่างที่ชัดเจนของ `[FromBody]`, `[FromQuery]`, และ `[FromRoute]` พร้อมคำอธิบายว่าแต่ละตัวดึงข้อมูลจากส่วนไหนของ HTTP Request:

---

## ✅ 3.1. `[FromBody]` — รับข้อมูลจาก **Request Body**
> ใช้กับ HTTP POST/PUT/DELETE ที่ส่งข้อมูลแบบ JSON

### 🎯 ตัวอย่าง:
```csharp
[HttpPost("create")]
public IActionResult CreateProduct([FromBody] ProductDto product)
{
    return Ok($"Created: {product.Name} ราคา {product.Price} บาท");
}
```

### 📦 ตัวอย่าง Request:
```
POST /api/products/create
Content-Type: application/json

{
  "name": "iPhone 15",
  "price": 45900
}
```

---

## ✅ 3.2. `[FromQuery]` — รับข้อมูลจาก **Query String**
> ใช้กับ HTTP GET ที่ส่งข้อมูลผ่าน URL query เช่น `?key=value`

### 🎯 ตัวอย่าง:
```csharp
[HttpGet("search")]
public IActionResult Search([FromQuery] string keyword, [FromQuery] int page = 1)
{
    return Ok($"Search: {keyword}, Page: {page}");
}
```

### 🔍 ตัวอย่าง URL:
```
GET /api/products/search?keyword=iphone&page=2
```

---

## ✅ 3.3. `[FromRoute]` — รับข้อมูลจาก **Route Parameter**
> ใช้กับ URL ที่มี path parameter เช่น `/products/{id}`

### 🎯 ตัวอย่าง:
```csharp
[HttpGet("{id}")]
public IActionResult GetProductById([FromRoute] int id)
{
    return Ok($"Product ID: {id}");
}
```

### 🌐 ตัวอย่าง URL:
```
GET /api/products/101
```

---

## 🔁 รวมตัวอย่างใช้หลายแบบพร้อมกัน
```csharp
[HttpPut("{id}")]
public IActionResult UpdateProduct(
    [FromRoute] int id,
    [FromBody] ProductDto product,
    [FromQuery] string updatedBy)
{
    return Ok($"Update Product {id} by {updatedBy}, New Name: {product.Name}");
}
```

🧾 URL:
```
PUT /api/products/101?updatedBy=admin
```

🧾 Body:
```json
{
  "name": "iPhone 15 Pro Max",
  "price": 55900
}
```

 😄
#

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
