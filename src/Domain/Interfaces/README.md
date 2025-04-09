# Interfaces

This directory contains core domain interfaces that define the contracts for services needed by the domain.

## What goes here?

- **Repository Interfaces**: Contracts for data access without implementation details
- **Domain Service Interfaces**: Contracts for domain-level services
- **UnitOfWork Interface**: Contract for transaction management

## Example

```csharp
public interface ICustomerRepository
{
    Task<Customer> GetByIdAsync(int id);
    Task<IEnumerable<Customer>> GetAllAsync();
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(Customer customer);
}
```

These interfaces should be implemented in the Infrastructure layer, following the Dependency Inversion Principle.
