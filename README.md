# 📦 Payment Integration API

Bu proje, .NET 8 tabanlı bir ödeme yönetim sistemidir.  
Ürünleri listeler, sipariş işlemlerini gerçekleştirir ve harici bir sistem üzerinden bakiyeleri yönetir.

---

## 🚀 Kullanılan Teknolojiler

- .NET 8  
- PostgreSQL 16  
- Entity Framework Core  
- Serilog (loglama)  
- Katmanlı mimari (API, App, Data, DataAccess, External, Helper)

---

## 🛠️ PostgreSQL Gereksinimi

Proje, PostgreSQL 16 ile çalışacak şekilde yapılandırılmıştır. Daha düşük sürümler desteklenmemektedir.

### Veritabanı ve Kullanıcı Oluşturma

Aşağıdaki komutlar PostgreSQL üzerinde gerekli kullanıcıyı ve veritabanını oluşturur:

```sql
CREATE USER payment_user WITH PASSWORD 'yourpassword';
CREATE DATABASE paymentdb OWNER payment_user;
GRANT ALL PRIVILEGES ON DATABASE paymentdb TO payment_user;
```

> Veritabanı bağlantı bilgileri, proje içerisindeki yapılandırma dosyası ile eşleşmelidir.

---

## ▶️ Projeyi Çalıştırma

1. PostgreSQL 16 kurulu olmalıdır.  
2. Bağlantı ayarlarını yapılandırın.  
3. Aşağıdaki komutla projeyi çalıştırın:

```bash
dotnet run --project PaymentIntegration.API
```

> Uygulama çalıştığında veritabanı tabloları otomatik olarak oluşturulur.

---

## 📂 Proje Katmanları

| Katman       | Açıklama                                                |
|--------------|---------------------------------------------------------|
| API          | HTTP endpoint'lerini içerir                            |
| App          | İş mantığını içeren servis katmanı                     |
| Data         | Domain entity’leri, DbContext ve migration'lar         |
| DataAccess   | Veritabanı işlemlerini içeren servis katmanı           |
| External     | Harici servislerle (örneğin Balance API) entegrasyon   |
| Helper       | Response modelleri, middleware'lar ve ortak yardımcılar|
| Tests        | Unit test projeleri (xUnit, Moq)                       |

---

## 🧪 Testler

`PaymentIntegration.Tests` projesinde servis seviyesinde unit testler bulunmaktadır.  
xUnit, Moq ve FluentAssertions kullanılmaktadır.

---

## 📌 Notlar

- API token ile korunmaktadır (`Authorization` header).  
- Veritabanı bağlantısı uygulama başlarken otomatik migration ile yapılandırılır.
