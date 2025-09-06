# User Management API

A simple web API for managing users.  
This project demonstrates API design, clean architecture, and common backend practices.

---

## How to Run (`dotnet run`)

1. **Clone the repository**
   ```bash
   git clone https://github.com/voquangnha1998/UserManagementApi.git
   cd UserManagementApi

## Design choiced
Repository 

## What I’d Do Next (Longer Take-home)
Code Optimization: Refactor services & repositories for cleaner, more maintainable code
Adding Entity Framework Core with a real database
Implementing JWT authentication & role-based authorization

## Call Endpoints

- Get all users: http://localhost:5000/api/users

## Test Instructions
```bash
dotnet test

Unit tests for controller, services and repositories
- Controller: UsersControllerTests
- Services: AuthServiceTests and UserServiceTests
- Repositories: InMemoryUserRepositoryTests
