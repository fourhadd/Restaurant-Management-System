# 🍔 FoodieRestaurant - Full-Stack E-Ticarət Arxitekturası

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-6.0%2B-512bd4)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.0-7952b3)
![SQLite](https://img.shields.io/badge/SQLite-3.0-003b57)
![Architecture](https://img.shields.io/badge/Arxitektura-Clean%20MVC-green)

Bu layihə **ASP.NET Core MVC** platformasında hazırlanmış, müasir və yüksək performanslı yemək sifarişi platformasıdır. Layihə sadəcə vizual yeniləmə deyil, həm də mühəndislik prinsiplərinə əsaslanan arxitektur təkamülü təmsil edir.

---

## 🚀 Əsas Mühəndislik Üstünlükləri

### 1. Arxitektur Struktur (Backend)
- **Layered Architecture:** Məsuliyyətlərin bölünməsi prinsipinə (Separation of Concerns) uyğun olaraq **Controller-Service-Repository** strukturu tətbiq edilib.
- **Dependency Injection (DI):** Kodun asılılığını azaltmaq və test edilə bilməsini təmin etmək üçün ASP.NET Core-un daxili DI konteynerindən istifadə olunub.
- **Generic Repository Pattern:** CRUD əməliyyatlarını abstraksiya edərək kod təkrarı minimuma endirilib.
- **Identity və Təhlükəsizlik:** İstifadəçi qeydiyyatı və rol əsaslı giriş (Admin/Müştəri) üçün **ASP.NET Core Identity** inteqrasiya edilib.

### 2. UI/UX və Frontend Dayanıqlığı
- **Defensive Rendering:** Razor View-larda "Fallback Strategy" tətbiq edilib. Əgər şəklin linki xarabdırsa və ya yoxdursa, sistem avtomatik kateqoriyaya uyğun default şəkillər təyin edir.
- **Design System:** Saytın bütün rəng və vizual sistemi **CSS Custom Properties (:root)** vasitəsilə mərkəzləşdirilib.
- **Sticky Footer Arxitekturası:** **Flexbox** (`min-vh-100`) istifadə edilərək, səhifə məzmunu az olsa belə footer-in ekranın aşağısında sabit qalması təmin edilib.
- **Responsive Dizayn:** **Bootstrap 5** grid sistemi ilə saytın mobil və desktop cihazlarında qüsursuz görünməsi təmin edilib.

### 3. Məlumat Axını və Təhlükəsizlik
- **Strongly-Typed ViewModels:** Təhlükəsizliyi artırmaq üçün birbaşa bazadakı modellər deyil, spesifik **ViewModel**-lər (məs: `CartViewModel`, `PagedFoodListViewModel`) istifadə edilib.
- **LINQ Aggregations:** Səbət məbləği (`TotalPrice`) və sətir cəmi (`LineTotal`) kimi hesablamalar model səviyyəsində avtomatlaşdırılıb.

---

## 🛠 Texnologiya Stack-i

| Lay | Texnologiya |
| :--- | :--- |
| **Framework** | ASP.NET Core MVC (C#) |
| **Database** | SQLite (Entity Framework Core) |
| **Frontend** | Razor Views, Bootstrap 5, Vanilla JS |
| **Auth** | ASP.NET Core Identity |
| **Arxitektura** | Repository Pattern, Service Layer, MVVM |

---

## 📂 Layihənin Strukturu

* `Program.cs`: Tətbiqin giriş nöqtəsi; Middleware, DI və Identity konfiqurasiyaları.
* `ViewModels/`: UI dayanıqlığını təmin edən məlumat transfer obyektləri.
* `Services/ & Repositories/`: Biznes məntiqi və verilənlər bazası ilə əlaqə layları.
* `Views/Shared/_Layout.cshtml`: Flexbox strukturu və ana şablon.

---

## ⚙️ Quruluş və Quraşdırma

1.  **Reponu klonlayın:**
    ```bash
    git clone https://github.com/fourhadd/Restaurant-Management-System.git
    ```
2.  **Visual Studio-da (2022+) açın**

---

## 👨‍💻 Mühəndislik Dəyəri
> "Bu layihə sadəcə bir yemək proqramı deyil; o, müdafiəli renderinq məntiqi, idarə oluna bilən frontend arxitekturası və müasir dizayn sisteminə malik bir sistemdir."
