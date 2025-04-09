# Data Transfer Objects (DTOs)

This directory contains DTOs that are used to transfer data between the application layer and the presentation layer.

## What goes here?

- **Request DTOs**: Objects that represent input data for operations
- **Response DTOs**: Objects that represent output data from operations
- **View Models**: Specialized DTOs that provide data for UI components

## Example

```csharp
public class CustomerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public AddressDto Address { get; set; }
}

public class AddressDto
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
}
```

DTOs help decouple the internal domain model from the external representation, providing a clear contract for data exchange.
