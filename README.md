# User Management API

A simple web API for managing users.  
This project demonstrates API design, clean architecture, and common backend practices.

---

## How to Run

1. **Clone the repository**
   ```bash
   git clone https://github.com/voquangnha1998/UserManagementApi.git
   cd UserManagementApi
   ```
   ```bash
   dotnet run
   ```
or you can choice UserManagement and click run
- The API will start at:
      https://localhost:5001 - http://localhost:5000
## Design choiced
Repository 

## What I’d Do Next (Longer Take-home)
- Code Optimization: Refactor services & repositories for cleaner, more maintainable code
- Adding Entity Framework Core with a real database
- Implementing JWT authentication & role-based authorization

## Call Endpoints
- SignIn: [POST]https://localhost:5001/api/auth/sign-in
- Get all users: [GET] https://localhost:5001/api/users
- Get user by ID: [GET] https://localhost:5001/api/users/{id}
- Create a new user: [POST] https://localhost:5001/api/users
- Update a user: [PUT] https://localhost:5001/api/users/{id}
- Delete a user: [DELETE] http://localhost:5000/api/users/{id}
### --> When you run project: Swagger provides detailed descriptions of all endpoints, request/response formats, and allows you to try out requests directly from the browser.
## Test Instructions
```bash
dotnet test
```
Unit tests for controller, services and repositories
- Controller: UsersControllerTests
- Services: AuthServiceTests and UserServiceTests
- Repositories: InMemoryUserRepositoryTests
