# ProductController.cs
#

### 🔹 `ProductsController : ControllerBase`...
- `ProductsController` คือ **Controller Class** สำหรับจัดการคำขอ (Request) ที่เกี่ยวข้องกับ "Product"
- `: ControllerBase` หมายถึง **สืบทอดคุณสมบัติ (inherit)** จาก `ControllerBase` ซึ่งเป็นคลาสพื้นฐานที่ใช้สำหรับสร้าง API Controller (ไม่รวม View เหมือน `Controller`)
- ใช้ `ControllerBase` จะมีฟีเจอร์เช่น `Ok()`, `NotFound()`, `BadRequest()` เพื่อคืนค่า HTTP Response แบบต่าง ๆ

#### ControllerBase:

https://github.com/nuchit2019/Training_ProductAPI/blob/main/src/ProductAPI/Controllers/ControllerBase.md

#

### 🔹 `[HttpGet]`...
- เป็น **Attribute** บอกว่า Method นี้จะตอบสนองคำขอ HTTP GET
- ใช้สำหรับดึงข้อมูลทั้งหมด เช่น `GET /api/Products`
```csharp
[HttpGet]
public async Task<IActionResult> GetProducts()
```

#

### 🔹 `[HttpGet("{id}")]`...
- เป็น HTTP GET แบบมี parameter **ส่ง `id` ผ่าน URL**
- เช่น `GET /api/Products/5` จะส่ง id = 5
- `{id}` จะ map เข้ากับ parameter ของ Method โดยอัตโนมัติ
```csharp
[HttpGet("{id}")]
public async Task<IActionResult> GetProduct(int id)
```

#

### 🔹 `[HttpPost]`...
- ระบุว่า Method นี้ใช้กับคำสั่ง **HTTP POST**
- ใช้สำหรับสร้างข้อมูลใหม่ เช่น `POST /api/Products` พร้อม body ที่เป็นข้อมูล Product ใหม่
```csharp
[HttpPost]
public async Task<IActionResult> CreateProduct([FromBody] Product product)
```

#

### 🔹 `[HttpPut("{id}")]`...
- ใช้สำหรับ **HTTP PUT** ซึ่งใช้แก้ไขข้อมูลที่มีอยู่
- URL จะระบุ `id` ของรายการที่จะแก้ไข เช่น `PUT /api/Products/3`
- ใช้ร่วมกับ `product.Id` เพื่อตรวจสอบว่าตรงกันหรือไม่
```csharp
[HttpPut("{id}")]
public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
```

#

### 🔹 `[HttpDelete("{id}")]`...
- ใช้สำหรับ **HTTP DELETE** เพื่อลบข้อมูลตาม `id`
- เช่น `DELETE /api/Products/3` จะลบ Product ที่มี id = 3
```csharp
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteProduct(int id)
```

#

### 🔹 `public async Task<IActionResult>`...

- `public`: เป็น **access modifier** ระบุว่า method นี้เรียกใช้งานได้จากภายนอก
- `async`: เป็น **asynchronous method** ใช้งานร่วมกับ `await` เพื่อรองรับการทำงานแบบไม่บล็อค (non-blocking)
- `Task<IActionResult>`:
  - `Task<>` คือผลลัพธ์ของ method แบบ async
  - `IActionResult` คือผลลัพธ์ที่บอกว่า Controller ควรส่งอะไรกลับ เช่น `Ok()`, `NotFound()`, `BadRequest()`, ฯลฯ

#

### สรุปภาพรวม:

| Attribute         | HTTP Method | ใช้ทำอะไร                          | URL ตัวอย่าง              |
|------------------|-------------|-----------------------------------|----------------------------|
| `[HttpGet]`       | GET         | ดึงข้อมูลทั้งหมด                   | `/api/Products`           |
| `[HttpGet("{id}")]` | GET       | ดึงข้อมูลตาม id                   | `/api/Products/1`         |
| `[HttpPost]`      | POST        | เพิ่มข้อมูลใหม่                    | `/api/Products`           |
| `[HttpPut("{id}")]` | PUT       | อัปเดตข้อมูลตาม id               | `/api/Products/1`         |
| `[HttpDelete("{id}")]` | DELETE | ลบข้อมูลตาม id                    | `/api/Products/1`         |

#😄
