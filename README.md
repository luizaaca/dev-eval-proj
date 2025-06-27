# Developer Evaluation Project

## Overview

This project is a Sales Management API built with .NET, following DDD principles. It provides a complete CRUD for sales, including business rules for discounts and item limits, and supports event logging for key sale actions.

---

## Table of Contents

- [Features](#features)
- [Business Rules](#business-rules)
- [Tech Stack](#tech-stack)
- [How to Configure](#how-to-configure)
- [How to Run](#how-to-run)
- [How to Test](#how-to-test)
- [API Endpoints](#api-endpoints)
- [Project Structure](#project-structure)

---

## Features

- Full CRUD for sales (Create, Read, Update, Delete)
- Pagination, ordering, and filtering for sales listing
- Business rules for discounts and item limits
- Domain event logging (SaleCreated, SaleModified, SaleCancelled, ItemCancelled)
- In-memory or PostgreSQL database support (configurable)
- Unit tests for handlers and validators

---

## Business Rules

- **Discount Tiers:**
  - 4+ identical items: 10% discount
  - 10-20 identical items: 20% discount
- **Restrictions:**
  - Maximum: 20 items per product
  - No discounts for less than 4 items

---

## Tech Stack

- **Backend:** .NET 8, ASP.NET Core Web API
- **ORM:** Entity Framework Core
- **Database:** InMemoryDB (for dev)
- **Testing:** xUnit, FluentAssertions, NSubstitute
- **API Docs:** Swagger (OpenAPI)

---

## How to Configure

1. **Clone the repository:**
   ```sh
   git clone https://github.com/luizaaca/dev-eval-proj.git
   cd /dev-eval-proj
   ```
2. **Run the application:**
   ```sh
   dotnet run
   ```
4. **Access the API:**
   - Swagger UI: `https://localhost:7181/swagger`

---

## How to Run

- **Development:**
  1. Restore dependencies:
     ```sh
     dotnet restore
     ```
  2. Run the application:
     ```sh
     dotnet run --project src/Ambev.DeveloperEvaluation.WebApi
     ```
  3. Use `appsettings.Development.json` for development settings.
  4. Access Swagger UI at:
     ```
     http://localhost:5000/swagger
     ```
     *(or the port shown in the console)*

- **Production:**
  1. Publish the application:
     ```sh
     dotnet publish -c Release -o ./publish
     ```
  2. Configure the production database and settings in `appsettings.json`.
  3. Run the published application:
     ```sh
     dotnet ./publish/Ambev.DeveloperEvaluation.WebApi.dll
     ```

---

## How to Test

- **Unit Tests:**
  1. Run all tests:
     ```sh
     dotnet test
     ```
  2. Tests are located in the `tests` directory.

---

## API Endpoints

- **Sales:**
  - `GET /api/sales`: Get all sales
  - `GET /api/sales/{id}`: Get a specific sale by ID
  - `POST /api/sales`: Create a new sale
  - `PUT /api/sales/{id}`: Update an existing sale
  - `DELETE /api/sales/{id}`: Delete a sale

---

## Project Structure

- **/src:** Source code for the backend application
- **/tests:** Unit tests for the application