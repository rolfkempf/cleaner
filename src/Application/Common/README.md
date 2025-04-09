# Common Application Components

This directory contains common utilities and behaviors used across the application layer.

## What goes here?

- **Behaviors**: Cross-cutting concerns implemented as pipeline behaviors (logging, validation, etc.)
- **Exceptions**: Application-specific exceptions
- **Mapping Profiles**: Object mapping configuration
- **Validators**: Common validation rules and logic

## Example

```csharp
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Validation logic
        // ...

        return await next();
    }
}
```

These components provide reusable functionality across use cases in the application layer.
