# 🔐 Easy Lockers Management Project

A basic application for locker rental management (classic CRUD).

This project was created to practice full-stack application development, with a focus on clean architecture and the use of design patterns. The app includes two user roles: **Administrator** and **User**, and implements patterns such as: **Facade/Factory**, **Repository**, **DTO**, **Base Entity**, and **Unit of Work**.

---

## 📅 Changelog

- **2025-03-08**: Backend/Frontend/Database — Initial code upload, database structure, entity-relationship diagram, entities, interfaces, models, and services.
- **2025-03-23**: Added unit tests using **Moq** and **xUnit** packages. Backend/Frontend: Minor bug fixes.
- **2025-04-25**: Version improved by adding popups and HTML table PDF export. Implemented the **Facade/Factory** design pattern on the frontend using the **MatDialog** library.

---

## 🎯 Objective

To gain hands-on experience with:

- **.NET (C#)** and **MySQL**
- **Angular (TypeScript)**
- **Design Patterns**
- **Onion Architecture**
- **Entity Framework / Docker**

---

## 🚀 Features

### 🔧 Backend

- Based on **Onion Architecture**
- Implements multiple **design patterns**:
  - Repository Pattern
  - Unit of Work
  - Base Entity
  - Data Transfer Object (DTO)

- **Key Libraries**:
  - **Encryption**:
    - `BCrypt.Net-Next`
    - `System.Security.Cryptography` (AES-256 encryption)
  - **Logging**:
    - `Serilog`
    - `Serilog.Extensions.Logging`
    - `Serilog.Sinks.File`
  - **ORM**:
    - `Microsoft.EntityFrameworkCore`
    - `Pomelo.EntityFrameworkCore.MySql` (for MySQL)

---

### 💻 Frontend

- Built with **Angular 18.2.14**
- Uses **Angular Material** for modals and UI components:
  - `@angular/material`: 18.2.14
  - `@angular/animations`: 18.2.13
  - `@angular/cdk`: 18.2.14
- Uses **jspdf** and **html2canvas** to export HTML tables to PDF
- Modular project structure

---

### 🗄️ Database

- Uses **MySQL**
- Managed via **Docker Desktop** and **DBeaver**
- Includes:
  - **DDL scripts** for table creation
  - **DML scripts** for sample data
  - **Entity Relationship Diagram**

---

## 🧪 Installation

### ✅ Prerequisites

Make sure the following tools are installed:

- [.NET SDK 9.0.200](https://dotnet.microsoft.com/)
- [Docker Desktop 4.40.0](https://www.docker.com/products/docker-desktop/)
- [DBeaver Community 25.0.3](https://dbeaver.io/download/)
- [Node.js & npm](https://nodejs.org/) (for the frontend)

---

### 🛠️ Setup Instructions

1. Clone the repository:

    ```bash
    git clone https://github.com/waltermillan/Lockers.git
    ```

2. Follow the video guides for full setup:
    - [Version 1 Overview](https://youtu.be/y3lp6_5Npe0)
    - [Version 2 Overview](https://youtu.be/79U6jyvQDDM)

3. Complete the remaining steps as detailed in the project documentation.

---

## 📄 License

**Free and open-source**
