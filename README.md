# Employee Attendance Tracker

A web-based attendance management system developed using **ASP.NET MVC** and **3-Tier Architecture**. This system allows organizations to manage departments, employees, and attendance records in an organized and efficient way. Built as part of a Code Zone task.

---

## 🏛️ Architecture: 3-Tier Structure

The project follows a clean separation of concerns using **Three-Tier Architecture**:

1. **Presentation Layer (`AttendanceTracker.Presentation`)**
   - ASP.NET MVC UI  
   - Views and Controllers for user interaction

2. **Business Logic Layer (BLL) (`AttendanceTracker.Business`)**
   - DTOs, Interfaces, and Services  
   - Handles validation, calculations (like attendance %), and business rules

3. **Data Access Layer (DAL) (`AttendanceTracker.Data`)**
   - Entity Framework Core (Code-First)  
   - Models and Repositories for interacting with the database

---

## 📋 Features

### 🏢 Department Management
- Add, edit, delete, and list departments  
- Validations: Unique name and 4-letter code  
- Shows employee count per department

### 👨‍💼 Employee Management
- Add, edit, delete, and list employees  
- Validations: Full name (4 parts), email format, uniqueness  
- Department assignment from dropdown  
- Displays current month's attendance summary

### 🕒 Attendance Management
- Mark employee as "Present" or "Absent" for a selected date  
- One attendance per employee per day  
- Cannot mark attendance for future dates  
- Filter attendance records by date, department, or employee

---

## 🛠️ Technologies Used

- ASP.NET MVC (.NET 7)
- Entity Framework Core (Code First)
- In-Memory Database (for simplicity)
- HTML, CSS, Bootstrap, jQuery
- Visual Studio 2022

---

## 🚀 Getting Started

### Prerequisites

- Visual Studio 2022  
- .NET 7 SDK  
- Git (optional)

### Steps

1. Clone the repository:

```bash
git clone https://github.com/mariamalaa98/code_zone_task_MariamAlaa.git

