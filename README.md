# RabbitMQ Exchange Types Demo 🐰

An educational **.NET 10 / C#** project demonstrating the three core RabbitMQ exchange types — **Direct**, **Fanout**, and **Topic** — through a practical scenario: user registration triggering SMS and email notifications.

> Goal: learn the difference between RabbitMQ exchange behaviors with simple, runnable code — not a production-ready service.

## 🎯 About

Each folder is a complete Publisher/Consumer scenario for one exchange type:

| Exchange | Scenario | Behavior |
|---|---|---|
| **Direct** | Register → Send SMS | Message routes only to queues bound with the exact routing key |
| **Fanout** | Register → Send SMS + Email | Message is broadcast to every bound queue, routing key ignored |
| **Topic** | Register → Smart routing of SMS/Email | Message routes based on a wildcard pattern (`*`, `#`) on the routing key |

## 🏗️ Project Structure

```
RabbitMq.Structure.slnx
├── Direct-Exchange
│   ├── RegisterUser.Direct      → Publisher
│   └── SendSms.Direct           → Consumer
├── Fanout-Exchange
│   ├── RegisterUser.Fanout      → Publisher
│   ├── SendSms.Fanout           → Consumer
│   └── SendEmail.Fanout         → Consumer
├── Topic-Exchange
│   ├── RegisterUser.Topic       → Publisher
│   ├── SendSms.Topic            → Consumer (binding: *.registered)
│   └── SendEmail.Topic          → Consumer (binding: User.*)
└── Utils                         → Shared User model
```

## ⚙️ Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- A running RabbitMQ instance (easiest via Docker):
```bash
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

The management UI is available at [http://localhost:15672](http://localhost:15672) with `guest`/`guest`.

## 📚 Key Concepts

### Direct Exchange
A message is delivered only to queues bound with a routing key that matches **exactly**.

### Fanout Exchange
The routing key is ignored entirely; the message is delivered to **all** bound queues — useful for broadcasting an event to multiple independent services.

### Topic Exchange
The routing key follows a dot-separated pattern (`word1.word2...`), and bindings can use wildcards:
- `*` matches exactly one word
- `#` matches zero or more words

Example in this repo: a message published with key `User.registered` matches both `User.*` (email service) and `*.registered` (SMS service).

## 🛠️ Tech Stack

- C# / .NET 10
- [RabbitMQ.Client](https://www.nuget.org/packages/RabbitMQ.Client) 7.x (async API)
- Newtonsoft.Json

## ⭐ Like it?

If you liked this project and it helped you, support it by giving it a **star** ⭐ or sharing it with your friends.

## Developer

[Mohammad Amin Nazeri](https://github.com/Mohammad-Amin-Nazeri)

[![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/mohammad-amin-nazeri)
[![GitHub](https://img.shields.io/badge/GitHub-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://github.com/Mohammad-Amin-Nazeri)
[![Telegram](https://img.shields.io/badge/Telegram-2CA5E2?style=for-the-badge&logo=telegram&logoColor=white)](https://t.me/Aminn02)
[![Instagram](https://img.shields.io/badge/Instagram-E4405F?style=for-the-badge&logo=instagram&logoColor=white)](https://www.instagram.com/mohammad_amin_nazeri/)

---
---

# نمونه‌ی آموزشی انواع Exchange در RabbitMQ 🐰

یک پروژه‌ی آموزشی با **.NET 10 / C#** که سه نوع اصلی Exchange در RabbitMQ — **Direct**، **Fanout** و **Topic** — را با یک سناریوی عملی نشان می‌دهد: ثبت‌نام کاربر که باعث ارسال پیامک و ایمیل می‌شود.

> هدف: یادگیری تفاوت رفتار Exchangeهای RabbitMQ با کد ساده و قابل اجرا — نه یک سرویس آماده‌ی production.

## 🎯 درباره‌ی پروژه

هر پوشه یک سناریوی کامل Publisher/Consumer برای یک نوع Exchange است:

| Exchange | سناریو | رفتار |
|---|---|---|
| **Direct** | ثبت‌نام → ارسال پیامک | پیام فقط به صف‌هایی می‌رسد که با Routing Key دقیقاً یکسان بایند شده‌اند |
| **Fanout** | ثبت‌نام → ارسال پیامک + ایمیل | پیام به همه‌ی صف‌های بایند‌شده Broadcast می‌شود؛ Routing Key نادیده گرفته می‌شود |
| **Topic** | ثبت‌نام → مسیر‌دهی هوشمند پیامک/ایمیل | پیام بر اساس یک الگوی wildcard (`*`, `#`) روی Routing Key مسیر‌دهی می‌شود |

## 🏗️ ساختار پروژه

```
RabbitMq.Structure.slnx
├── Direct-Exchange
│   ├── RegisterUser.Direct      → Publisher
│   └── SendSms.Direct           → Consumer
├── Fanout-Exchange
│   ├── RegisterUser.Fanout      → Publisher
│   ├── SendSms.Fanout           → Consumer
│   └── SendEmail.Fanout         → Consumer
├── Topic-Exchange
│   ├── RegisterUser.Topic       → Publisher
│   ├── SendSms.Topic            → Consumer (binding: *.registered)
│   └── SendEmail.Topic          → Consumer (binding: User.*)
└── Utils                         → مدل مشترک User
```

## ⚙️ پیش‌نیازها

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- یک نمونه در حال اجرای RabbitMQ (ساده‌ترین راه با Docker):
```bash
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

پنل مدیریتی روی [http://localhost:15672](http://localhost:15672) با `guest`/`guest` در دسترس است.

## 📚 نکات کلیدی

### Direct Exchange
پیام فقط به صف‌هایی تحویل داده می‌شود که Routing Key آن‌ها دقیقاً مطابقت داشته باشد.

### Fanout Exchange
Routing Key کاملاً نادیده گرفته می‌شود؛ پیام به **همه‌ی** صف‌های بایند‌شده می‌رسد — مناسب برای پخش یک رویداد به چند سرویس مستقل.

### Topic Exchange
Routing Key از الگوی نقطه‌جدا (`word1.word2...`) پیروی می‌کند و binding می‌تواند از wildcard استفاده کند:
- `*` دقیقاً یک کلمه
- `#` صفر یا چند کلمه

مثال در این ریپو: پیامی با کلید `User.registered` هم با `User.*` (سرویس ایمیل) و هم با `*.registered` (سرویس پیامک) مچ می‌شود.

## 🛠️ تکنولوژی‌ها

- C# / .NET 10
- [RabbitMQ.Client](https://www.nuget.org/packages/RabbitMQ.Client) نسخه 7.x (Async API)
- Newtonsoft.Json

## ⭐ خوشتون اومد؟

اگر این پروژه به نظرتون خوب بود و کمکتون کرد، با زدن یک **استار** ⭐ یا معرفی این ریپازیتوری به دوستانتون ازش حمایت کنید.

## توسعه‌دهنده

[محمدامین نظری](https://github.com/Mohammad-Amin-Nazeri)

[![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/mohammad-amin-nazeri)
[![GitHub](https://img.shields.io/badge/GitHub-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://github.com/Mohammad-Amin-Nazeri)
[![Telegram](https://img.shields.io/badge/Telegram-2CA5E2?style=for-the-badge&logo=telegram&logoColor=white)](https://t.me/Aminn02)
[![Instagram](https://img.shields.io/badge/Instagram-E4405F?style=for-the-badge&logo=instagram&logoColor=white)](https://www.instagram.com/mohammad_amin_nazeri/)
