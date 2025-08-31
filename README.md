# Task Management System

A task and issue tracking web application built with **.NET 8.0** following Clean Architecture principles.  
The system allows users to create, assign, and track tasks with priorities, statuses, and deadlines.

---

## Features

- User registration and authentication with **JWT tokens**
- **Email confirmation** and **password reset** using MailKit
- Task/Issue management:
  - Create, update, and delete issues
  - Assign issues to users
  - Set priority, status, and due dates
  - Track completion progress
- Object mapping with **Mapster**
- Service–repository pattern for clean separation of concerns

---

## Domain Models

- **User** – Manages authentication and assigned issues  
- **Issue** – Represents a task with description, due date, completion status  
- **Priority** – Defines task urgency (e.g., Low, Medium, High)  
- **Status** – Defines task progress (e.g., To do, In Progress, Completed)  

---

## Tech Stack

- **Backend**: .NET 8.0 Web API  
- **Architecture**: Clean Architecture, Service–Repository pattern  
- **Authentication**: JWT tokens  
- **Email**: MailKit for confirmation & password reset  
- **Mapping**: Mapster  
- **Logging**: Serilog (optional)  
- **Database**: (SQL Server / SQLite – depending on your setup)  

---

## Getting Started

### Prerequisites
- .NET 8.0 SDK  
- MSSQL Server or SQLite  

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/pixel228s/TaskManagementSystem3.git
   
2.Navigate to the project directory:
cd TaskManagementSystem3

3. Apply migrations:
dotnet ef database update

5. Run the project:
dotnet run
---

Update the appsettings.json file with your database and email configuration.
🛠️ Example appsettings.json (structure may vary)
```json
 {
  "ConnectionStrings": {
    "DefaultConnection": "ConnectionString"
  },

  "AllowedHosts": "*",

  "Serilog": {
    "Using": [ "Serilog.Sinks.Console", "Serilog.Sinks.File" ],
    "MinimumLevel": {
      "Default": "Debug",
      "Override": {
        "Microsoft": "Warning",
        "Microsoft.Hosting.Lifetime": "Information",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "outputTemplate": "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
        }
      },
      {
        "Name": "File",
        "Args": {
          "path": "logs/log-.txt",
          "rollingInterval": "Day",
          "retainedFileCountLimit": 30,
          "outputTemplate": "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
        }
      }
    ],
    "Enrich": [ "FromLogContext", "WithMachineName", "WithThreadId" ],
    "Properties": {
      "Application": "IssueManagement"
    }
  },

  "Authentication": {
    "SecretForKey": "YourSecretKey",
    "Issuer": "Issuer",
    "Audience": "YourAudience"
  },

  "EmailSender": {
    "UserName": "username/email",
    "Password": "yourpassword",
    "SenderAddress": "senderAddress",
    "SenderName": "SenderNAme",
    "SmtpServer": "smtp.gmail.com",
    "Port": 587
  }
}
