# Clean Architecture .NET Solution

This is a .NET solution template following Clean Architecture principles.

## Project Structure

- **Domain**: Contains enterprise business rules and entities

  - Entities: Core business objects
  - Value Objects: Immutable objects without identity
  - Interfaces: Core abstractions and contracts

- **Application**: Contains application business rules

  - Use Cases: Application-specific business rules
  - DTOs: Data Transfer Objects
  - Interfaces: Application service abstractions

- **Infrastructure**: Contains frameworks, drivers and tools

  - Data: Database-related implementations
  - Services: External service implementations

- **Presentation**: Contains UI and API components
  - WebApi: REST API endpoints
  - ImportService: CLI tool for data imports

## Getting Started

1. Clone this repository
2. Navigate to the solution directory
3. Run `dotnet build` to build the solution
4. Run `dotnet run --project src/Presentation/WebApi` to start the Web API
5. Run `dotnet run --project src/Presentation/ImportService` to use the Import CLI

## Adding New Features

1. Start by defining domain entities and business rules in the Domain project
2. Create use cases in the Application project
3. Implement required infrastructure in the Infrastructure project
4. Expose functionality via the Presentation layer (WebApi or ImportService)
