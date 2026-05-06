# GitGardens

GitGardens is a production-grade **ASP.NET Core MVC web application** designed to demonstrate modern software engineering principles, scalable architecture, and intelligent agritech data management. The system enables users to **create, manage, and monitor multiple gardens**, while capturing environmental metrics to support future analytics and smart decision-making.

---

## Features

### User Authentication & Identity
- **Secure Authentication**: Login and registration system with role-based access (**Admin / User**).
- **Session-Based Authorization**: Cookie authentication with protected routes using `[Authorize]`.
- **Data Ownership Enforcement**: Each user can only access and manage their own gardens.
- **Live Weather Data:** Real time weather conditions fetched from OpenWeatherMap API.
- **Smart Garden Tips:** Gardening tips generated based on current weather conditions.

---

### Garden Management (Core CRUD)
- **Create Garden**: Users can create multiple gardens with names and descriptions.
- **View Gardens**: Card-based dashboard displaying all user-owned gardens.
- **Edit Garden**: Pre-populated forms allowing secure updates to garden details.
- **User Isolation**: All garden operations are scoped to the authenticated user.

---

### Garden Metrics Tracking
- **Slider-Based Input UI**: Intuitive range sliders for environmental data capture:
  - Moisture
  - pH Levels
  - Temperature
  - Humidity
  - Sunlight
  - Nitrogen
- **Time-Series Data Storage**: Each metric entry is stored as a new record (no overwrites).
- **Secure Metric Assignment**: Metrics can only be added to gardens owned by the user.

---

### UI / UX Experience
- **Calm Agricultural Theme**:
  - Primary: Forest Green
  - Accent: Warning Orange
  - Error: Critical Red
- **Responsive Design**: Built with **Bootstrap 5** for cross-device compatibility.
- **Card-Based Layout**: Clean dashboard experience for garden visualization.
- **Minimalist Data-Driven UI**: Focused on clarity, usability, and productivity.

---

## Technical Architecture

The application follows **Clean Architecture principles** with strict adherence to **SOLID design patterns**, ensuring scalability, maintainability, and testability.

---

### Layered Architecture

- **Presentation Layer (MVC)**  
  Handles user interaction via Controllers and Razor Views.

- **Service Layer**  
  Contains business logic and enforces rules such as user ownership and validation.

- **Repository Layer**  
  Abstracts data access using interfaces and dependency injection.

- **Data Layer**  
  Uses **Entity Framework Core** (Database-First) to interact with SQL Server.

---

### Design Patterns

- **Repository Pattern**: Decouples data access logic from business logic.
- **Dependency Injection (DI)**: Promotes loose coupling and testability.
- **ViewModels**: Prevent overposting and enforce UI-specific data binding.
- **Separation of Concerns**: Clear boundaries between layers.

---

## Smart Garden Logic (Foundation)

GitGardens is designed to evolve into an **intelligent agritech platform**.

### Planned Health Index System
A weighted scoring model based on environmental inputs:

| Metric       | Weight |
|-------------|--------|
| Moisture    | 30%    |
| pH          | 25%    |
| Temperature | 15%    |
| Humidity    | 10%    |
| Sunlight    | 10%    |
| Nitrogen    | 10%    |

### Ideal Ranges
- **pH**: 6.0 – 7.5  
- **Temperature**: 18°C – 26°C  
- (Additional ranges to be enforced in future phases)

---

## Tech Stack

| Category | Technology |
| :--- | :--- |
| **Framework** | ASP.NET Core MVC (.NET 9) |
| **Language** | C# |
| **ORM** | Entity Framework Core (Database-First) |
| **Database** | SQL Server |
| **Frontend** | Razor Views + Bootstrap 5 |
| **Authentication** | Identity |
| **Architecture** | Clean Architecture + SOLID |
| **Design Pattern** | Repository Pattern |

---

## Implementation Highlights

### Garden Ownership Enforcement
- Every query filters by `UserID`
- Prevents unauthorized access and data leakage

### Secure Model Binding
- ViewModels used instead of Entities in forms
- Protects against **overposting attacks**

### Scalable Data Model
- One User → Many Gardens  
- One Garden → Many Metrics  

### Time-Series Ready
- Metrics are stored as historical records
- Enables future analytics and AI-driven insights

---

## Database Design

### Core Tables
- **Users**
- **Roles**
- **Gardens**
- **GardenMetrics**

### Relationships
- User → Gardens (One-to-Many)
- Garden → Metrics (One-to-Many)

---

## Setup & Installation

1. **Clone the repository**
2. **Database Setup**:
   - Open SQL Server
   - Run the provided DDL scripts to create tables
3. **Connection String**:
   - Update `appsettings.json` with your SQL Server connection
4. **Run Application**:
   - Open in **Visual Studio 2022+**
   - Build and run the project
5. **Authentication**:
   - Register a new user or use seeded data

---

## Development Roadmap

- [x] Project Architecture Setup (MVC + Clean Architecture)
- [x] Authentication & Role Management
- [x] Garden CRUD (Create, Read, Update)
- [x] Garden List Dashboard (Card UI)
- [x] Add Garden Metrics (Slider UI)
- [x] Garden Health Index Dashboard
- [ ] Mobile Companion App (Future Expansion)

---

## Vision

GitGardens is more than a CRUD application — it is the foundation of a **data-driven agritech platform**.

Future iterations aim to:
- Provide **predictive analytics**
- Enable **smart farming decisions**
- Integrate **IoT sensor data**
- Deliver **AI-powered recommendations**

---

## Demo / Walkthrough

📺 **YouTube Video Demonstration**
(https://youtu.be/ggBbungCqbw?si=pmI-3iYhpI1UzfKA)

---

## Author

**GitHugs** 
