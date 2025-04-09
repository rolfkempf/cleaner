# Data Access

This directory contains the data access implementation for the application.

## What goes here?

- **DbContext**: Entity Framework Core DbContext and related configuration
- **Repository Implementations**: Concrete implementations of repository interfaces
- **Entity Configurations**: Mapping between domain entities and database schema
- **Migrations**: Database migration scripts

## Example

```csharp
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        return await _context.Customers.FindAsync(id);
    }

    // Other repository methods
}
```

This layer handles all data access concerns, isolating them from the rest of the application.
