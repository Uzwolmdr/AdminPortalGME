# SampleApiService - Admin Portal Backend

## Overview

SampleApiService is a .NET 8.0 Web API service that serves as the backend for the Admin Portal application. It provides authentication, user profile management, and password change functionality. The service follows a clean architecture pattern with separation of concerns between controllers, services, and data access layers.

## Project Structure

SampleApiService/
├── Controllers/
│   └── LoginController.cs          # API endpoints for authentication and profile operations
├── Services/
│   ├── IProfileService.cs          # Service interface for profile operations
│   └── ProfileService.cs           # Service implementation with business logic
├── Models/
│   ├── LoginRequest.cs             # Request model for login
│   ├── ChangePasswordRequest.cs    # Request model for password change
│   └── User.cs                     # User entity model
├── Program.cs                       # Application entry point and configuration
├── appsettings.json                # Application configuration
└── wwwroot/                        # Static files (React frontend build)

Repository/                          # Separate class library project
├── DapperRepo/
│   ├── IProfileRepository.cs       # Repository interface
│   └── ProfileRepository.cs        # Repository implementation using Dapper
├── Models/
│   ├── GenericResponse.cs          # Generic API response model
│   └── LoginResponse.cs            # Login-specific response model
└── Config/
    └── ConfigurationManager.cs     # Configuration management utility


## Technology Stack

- .NET 8.0 - Target framework
- ASP.NET Core Web API - Web framework
- Dapper (v2.1.66) - Micro ORM for database operations

## Architecture

The project follows a layered architecture pattern:

1. Controller Layer (LoginController.cs)
   - Handles HTTP requests and responses
   - Validates input
   - Delegates business logic to services

2. Service Layer (ProfileService.cs)
   - Contains business logic
   - Validates business rules
   - Handles error scenarios and response codes
   - Acts as an intermediary between controllers and repositories

3. Repository Layer (ProfileRepository.cs)
   - Data access abstraction
   - Uses Dapper for database operations
   - Executes stored procedures and SQL queries
   - Handles database-specific exceptions

## LoginController

The LoginController is the main API controller that exposes endpoints for authentication and user profile management.

### Endpoints

#### 1. POST /api/Login/LoginProcess
Authenticates a user with email, user code, and password.

Request Body:
{
  "email": "user@example.com",
  "userCode": "USER001",
  "password": "password123"
}
```

Response Codes:
- 100 - Successful login
- 101 - Invalid usercode and password
- 102 - Account is not in active state
- 103 - Login failed attempt has exceeded (more than 5 attempts)
- 105 - Internal error occurred

Implementation Flow:
1. Receives LoginRequest from client
2. Calls ProfileService.GetLoginDetailsAsync()
3. Service validates credentials through repository
4. Returns GenericResponse with appropriate response code
5. Validates the Account Status of the client and also ensures the failed login attempt hasn't exceeded a certain threshold

#### 2. GET /api/Login/GetVersion
Returns the product version from the assembly information.

Response: String containing the version (e.g., "4.2.1")

Usage: Displays version in the footer of login, profile, dashboard, and change password pages.

#### 3. POST /api/Login/GetEmail
Retrieves the email address of a user by their user code.

Request Body:
{
  "userCode": "USER001"
}


Response: Email address string or null

Usage: Displays user email on the profile page after successful login.

#### 4. POST /api/Login/ChangePassword
Changes the password for an authenticated user.

Request Body:
{
  "email": "user@example.com",
  "userCode": "USER001",
  "oldPassword": "oldpass123",
  "newPassword": "newpass123"
}

**Response Codes:**
- 100 - Password change successful
- 101 - Change password failed
- 102 - Old password doesn't match
- 103 - Internal error occurred

Implementation Flow:
1. Validates old password first
2. If valid, updates to new password
3. Returns appropriate response code

## Dapper Methods for Asynchronous Operations

The ProfileRepository class uses Dapper for all database operations. Dapper is a lightweight ORM that extends IDbConnection with convenient methods for executing queries and stored procedures.

### Key Dapper Methods Used

#### 1. QueryFirstOrDefaultAsync<T>()
Executes a query asynchronously and returns the first result, or a default value if no results are found.


**How it works:**
- Opens the database connection automatically if closed
- Executes the stored procedure or the inline query with provided parameters
- Maps the result set to mentioned object
- Returns null if no matching record is found
- Asynchronous execution: The thread is released while waiting for the database response, allowing other operations to proceed

### DynamicParameters

All methods use `DynamicParameters` to pass parameters to database operations:

var parameters = new DynamicParameters();
parameters.Add("@Email", email, DbType.String);
parameters.Add("@UserCode", userCode, DbType.String);
parameters.Add("@Password", password, DbType.String);
parameters.Add("@Flag", "LoginDetails", DbType.String);


**Benefits:**
- Type-safe parameter binding
- Prevents SQL injection
- Supports various data types
- Clean and readable code

### Asynchronous Benefits

1. Non-blocking I/O: Database operations don't block the thread
2. Scalability: Server can handle more concurrent requests
3. Resource Efficiency: Threads are released while waiting for database responses
4. Better Performance: Especially under high load scenarios

### Error Handling

All repository methods include try-catch blocks with logging:

catch (Exception ex)
{
    _logger.LogError(ex, "Error message with context");
    throw; // Re-throws to be handled by service layer
}


### Dependency Injection

Services are registered in `Program.cs`:

// Database connection
builder.Services.AddScoped<IDbConnection>(sp => 
    new SqlConnection(connectionString));

// Services
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();


## Database

### Stored Procedure: `SW_PROC_PROFILE`

This stored procedure handles multiple operations based on the `@Flag` parameter:

- LoginDetails: Validates user credentials and returns status and login fail attempt count
- CheckOldPassword: Validates if the provided old password matches
- UpdatePassword: Updates the user's password

### Tables

- UserProfile: Stores user information (Email, UserCode, Password, Status, LoginFailAttempt)
- Error: Logs database errors


## Static Files

The wwwroot folder contains the built React frontend application. The API serves these static files and uses MapFallbackToFile("index.html") for client-side routing support.

## Running the Application

1. Prerequisites:
   - .NET 8.0 SDK
   - SQL Server with database configured

2. Configuration:
   - Update appsettings.json with your database connection string
   - Configure CORS origins if needed


## Response Code Reference

| Code | Description |
|------|-------------|
| 100  | Success |
| 101  | Invalid credentials / Operation failed |
| 102  | Account inactive / Old password mismatch |
| 103  | Login attempts exceeded / Internal error |
| 105  | Internal server error |

## Version Information

Current version: **4.2.1**

Version information is embedded in the assembly and can be retrieved via the `GetVersion` endpoint.


