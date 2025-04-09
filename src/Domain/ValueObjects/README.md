# Value Objects

This directory contains value objects for the domain layer.

## What goes here?

- **Value Objects**: Immutable objects that describe characteristics of domain entities but have no identity.
- **Complex Values**: Objects that represent concepts with their own rules but don't need unique identification.

## Example

```csharp
public class Money : ValueObject
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}
```

Value objects should be immutable and implement value equality based on their properties rather than identity.
