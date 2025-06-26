# DIRS21 Dynamic Mapping System

[![.NET 8](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## Table of Contents

1. [Overview](#overview)
2. [Features](#features)
3. [Architecture](#architecture)
4. [Getting Started](#getting-started)
5. [Extending the System](#extending-the-system)
6. [Error Handling](#error-handling1)
7. [Design Patterns & SOLID Principles](#design-patterns--solid-principles)

# Overview <a name="overview"></a>

A .NET 8-based dynamic mapping system that converts between DIRS21's internal data models and partner-specific formats (Google, Expedia, etc.). Designed for:

- **Extensibility**: Add new partners without core changes
- **Maintainability**: Clean Architecture separation
- **Robustness**: Comprehensive validation and error handling

# Features <a name="features"></a>

✅ Bidirectional mapping (DIRS21 ↔ Partner)  
✅ Automatic dependency registration  
✅ Type-safe validation  
✅ Meaningful error messages  
✅ over 80% unit test coverage

# Architecture <a name="architecture"></a>


![cleanArchitecture](./assets/cleanArch.png)

## Getting Started <a name="getting-started"></a>

## Layered Responsibilities

| Layer              | Responsibility                   | Key Components               |
| ------------------ | -------------------------------- | ---------------------------- |
| **Domain**         | Business models and interfaces   | `Reservation`, `IMapper<,>`  |
| **Application**    | Mapping logic                    | `MapHandler`, `TypeResolver` |
| **Infrastructure** | Partner-specific implementations | `GoogleReservationMapper`    |
| **Presentation**   | API/Entry point (optional)       | `MappingController`          |

# Key Classes & Methods

| Class                                | Method                        | Description                           |
| ------------------------------------ | ----------------------------- | ------------------------------------- |
| `MapHandler`                         | `Map(object, string, string)` | Orchestrates mapping with validation  |
| `TypeResolver`                       | `GetType(string)`             | Converts string aliases to .NET types |
| `IMappingProvider<TSource, TTarget>` | `Map(TSource)`                | Contract for all mappers              |

```csharp
// Google Reservation Mapper
public class GoogleReservationProvider :
    IMapper<Reservation, GoogleReservation>,
    IMapper<GoogleReservation, Reservation>
{
    public GoogleReservation Map(Reservation source)
    {
        // Validation and conversion logic
    }
}
```

## Design Patterns & SOLID Principles<a name="design-patterns--solid-principles"></a>

### Design Patterns

| Pattern   | Implementation                          | Benefit                                  |
| --------- | --------------------------------------- | ---------------------------------------- |
| Strategy  | `IMappingProvider<,>` implementations   | Swappable mapping algorithms per partner |
| Factory   | DI container auto-registration          | Decouples mapper creation                |
| Composite | `TypeResolver` aggregates type mappings | Uniform type handling                    |

### SOLID Compliance

| Principle             | How We Achieved It                                            |
| --------------------- | ------------------------------------------------------------- |
| Single Responsibility | Each mapper handles one conversion.                           |
| Open/Closed           | New partners added without modifying `MapHandler`.            |
| Liskov Substitution   | All mappers adhere to `IMappingProvider<,>`.                  |
| Interface Segregation | `IMappingProvider<,>` has only one method.                    |
| Dependency Inversion  | `MapHandler` depends on abstractions (`IMappingProvider<,>`). |

## Extending the System

### Adding a New Partner (e.g., TripAdvisor)

## 1. Create models

```csharp
// Infrastructure/TripAdvisor/Models/TripAdvisorReservation.cs
public class TripAdvisorReservation
{
    // TripAdvisor-specific fields
}
```

## 2. Implement mapper

```csharp
// Infrastructure/TripAdvisor/Mappers/TripAdvisorReservationProvider.cs
public class TripAdvisorReservationProvider :
    IMapper<Reservation, TripAdvisorReservation>
{
    public TripAdvisorReservation Map(Reservation source)
    {
        // Mapping logic here
    }
}
```

## 3. Register type

```csharp
// Application/Helper/TypeResolver.cs
// In TypeResolver initialization
 ["TripAdvisor.Reservation"] = typeof(TripAdvisorReservation),
```

## 4. No DI changes needed!

**Mappers auto-register via:**

```csharp
services.Scan(scan => scan
    .FromApplicationDependencies()
    .AddClasses(c => c.AssignableTo(typeof(IMappingProvider<,>)))
    .AsImplementedInterfaces()
    .WithTransientLifetime());
```

![cleanArchitecture](./assets/errorhandling.png)

## 5. Error Handling <a name="error-handling1"></a>

| Scenario            | Exception                    | Example Message                                              |
| ------------------- | ---------------------------- | ------------------------------------------------------------ |
| Invalid source type | `TypeMismatchException`      | "Expected Reservation but got Restaurant"                    |
| Missing mapper      | `MapperNotFoundException`    | "No mapper for Model.Reservation → Google.GoogleReservation" |
| Validation failure  | `MappingValidationException` | "Reservation ID is required"                                 |

---

## 6. Assumptions & Limitations <a name="limitations"></a>

### Assumptions

- Partner/model identifiers (e.g., `"Google.GoogleReservation"`) are known at compile-time.
- Mappings are primarily **1:1 transformations**.

## 7. Limitations

| Limitation                   | Mitigation Strategy              |
| ---------------------------- | -------------------------------- |
| No runtime type registration | Restart app to add new types.    |
| Basic performance profiling  | Add `Stopwatch` in `MapHandler`. |

---

## 8. Setup & Execution

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

## Running the API

### Run the Application

```bash
cd src/DIRS21.Mapper.Presentation
dotnet run
```

**Sample Request**

```json
{
  "data": { "Id": "RES-123", "GuestName": "Tochi" },
  "sourceType": "Model.Reservation",
  "targetType": "Google.GoogleReservation"
}
```

## 9. Testing <a name="testing"></a>

### Run All Tests

```bash
dotnet test
```

### Coverage

    •	✅ over 80% unit test coverage

### Validates

    •	Type resolution
    •	Mapper correctness
    •	Error scenarios

## 10. Evaluation Criteria

| Criteria      | How We Achieved It                                  |
| ------------- | --------------------------------------------------- |
| Code Quality  | SOLID compliance, test coverage, clean abstractions |
| Design        | Clean Architecture, Strategy pattern, extensible DI |
| Functionality | Handles all required mappings with validation       |
| Documentation | Clear structure, examples, and rationale            |
