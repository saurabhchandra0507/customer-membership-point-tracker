# 🏆 Customer Membership Point Tracker

A comprehensive customer loyalty system built using **.NET Core Web API** and **C#**.  
Track points, manage rewards, and gain insights with a robust backend system.

---

## ✨ Features

### 👤 Customer
- 📝 Registration, login & profile management  
- 💰 Real-time point balance tracking  
- 🏅 Tier system: Bronze, Silver, Gold, Platinum  
- 🎁 Redeem rewards using earned points  
- ⏳ Point expiration notifications  

### 🛠️ Admin
- 👥 Manage customers and user roles  
- 🛒 Add/update products with point values  
- 📢 Launch promotional campaigns  
- 📊 View analytics dashboard and customer stats  

---

## 🧰 Technical Highlights
- 💻 **.NET Core 8.0**
- 🧠 **C#**
- 🗄️ **Entity Framework Core** with SQL Server  
- 🔐 JWT authentication & role-based authorization  
- 🔁 AutoMapper  
- 🧪 FluentValidation  
- 📝 Swagger / OpenAPI docs  
- 📜 Serilog for logging  

---

## 🗂️ Project Structure

SupermarketLoyalty/
├── SupermarketLoyalty.API/ # API controllers and startup
├── SupermarketLoyalty.Core/ # Domain models, DTOs, interfaces
├── SupermarketLoyalty.Infrastructure/ # EF DbContext and services


---

## 🚀 Getting Started

### 🔧 Prerequisites
- ✅ .NET 8 SDK  
- ✅ SQL Server (LocalDB for development)

### 📥 Installation

```bash
git clone <repository-url>
cd SupermarketLoyalty
dotnet restore
cd SupermarketLoyalty.API
# Update DB connection string in appsettings.json
dotnet ef database update
dotnet run
👤 Default Accounts
🛡️ Admin
📧 admin@supermart.com

🔐 Admin123!

🙋 Customers (test users)
Gold, Silver, and Bronze level accounts pre-seeded

📡 Key API Endpoints
POST /api/auth/login – 🔐 Login

GET /api/customers – 👥 Get all customers (Admin)

POST /api/transactions – 💸 Create a transaction

POST /api/rewards/redeem – 🎁 Redeem reward
