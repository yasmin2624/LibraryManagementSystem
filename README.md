# LibraryManagementSystem

A **Library Management System** built with **ASP.NET Core 9**, **Entity Framework Core**, **SQL Server**, and **Razor Pages**.

## Features

*  Dashboard with library statistics
*  Book Management
*  Author Management
*  Category Management
*  User Management
*  Borrow & Return Management
*  Payment Management
*  Search and filtering
*  Validation and business rules
*  RESTful Web API
*  Swagger & Postman API testing

## Architecture

The project follows a layered architecture:

```text
LibraryManagementSystem
│
├── LibraryManagementSystem.API
├── LibraryManagementSystem.Application
├── LibraryManagementSystem.Domain
├── LibraryManagementSystem.Infrastructure
└── LibraryManagementSystem.Web
```

* **Domain** – Entities and core models
* **Application** – DTOs, interfaces, and services
* **Infrastructure** – Database and EF Core
* **API** – RESTful API controllers
* **Web** – Razor Pages user interface

## Technologies

* C#
* ASP.NET Core 9
* ASP.NET Core Web API
* Razor Pages
* Entity Framework Core
* SQL Server
* HTML / CSS
* Bootstrap
* Swagger / OpenAPI
* Postman
* Git & GitHub

## Main Entities

* Users
* Books
* Authors
* Categories
* Borrows
* Payments

## Getting Started

### Clone the repository

```bash
git clone https://github.com/yasmin2624/LibraryManagementSystem.git
cd LibraryManagementSystem
```

### Database

Configure the SQL Server connection string in:

```text
LibraryManagementSystem.API/appsettings.json
```

Then apply the migrations:

```bash
dotnet ef database update
```

### Run

Open the solution in **Visual Studio** and run the API and Web projects.

