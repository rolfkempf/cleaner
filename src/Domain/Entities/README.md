# Entities

This directory contains the domain entities that represent the core business objects of the application.

## What goes here?

- **Domain Entities**: These are the core business objects of your application. They encapsulate the most critical business rules and data.
- **Entity Base Classes**: Common functionality shared across entities like `BaseEntity` or `AuditableEntity`.
- **Aggregates**: Groups of entities that are treated as a single unit for data changes.

## Example

```csharp
public class Customer : BaseEntity
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public Address Address { get; private set; }

    // Domain logic and behaviors
    public void UpdateAddress(Address newAddress)
    {
        // Business rules/validation
        Address = newAddress;
    }
}
```

Keep entities focused on domain behavior rather than data persistence concerns.

# Dummy Entity

The `Dummy` entity represents a person with the following properties:

- `Name` (string): The name of the person.
- `Birthday` (DateTime): The date of birth of the person.
- `Gender` (string): The gender of the person.
