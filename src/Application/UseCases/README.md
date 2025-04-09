# Use Cases

This directory contains application use cases that implement the business logic of the application.

## What goes here?

- **Commands**: Operations that modify state (create, update, delete)
- **Queries**: Operations that retrieve data without modifying state
- **Command/Query Handlers**: Implementation of command and query processing logic
- **Use Case Specific Models**: DTOs specific to particular use cases

## Structure

Organize use cases by feature or domain concept:

```
UseCases/
├── Customers/
│   ├── Commands/
│   │   ├── CreateCustomer/
│   │   │   ├── CreateCustomerCommand.cs
│   │   │   └── CreateCustomerCommandHandler.cs
│   │   └── UpdateCustomer/
│   │       ├── UpdateCustomerCommand.cs
│   │       └── UpdateCustomerCommandHandler.cs
│   └── Queries/
│       ├── GetCustomerById/
│       │   ├── GetCustomerByIdQuery.cs
│       │   └── GetCustomerByIdQueryHandler.cs
│       └── GetCustomersList/
│           ├── GetCustomersListQuery.cs
│           └── GetCustomersListQueryHandler.cs
└── Products/
    ├── Commands/
    └── Queries/
```

This organization makes it easy to locate use cases and understand the available system operations.

# Dummy Use Case

This use case retrieves a list of `Dummy` entities.
