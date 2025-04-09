# Infrastructure Services

This directory contains implementations of services that interact with external systems or provide technical capabilities.

## What goes here?

- **External API Clients**: Service implementations for third-party APIs
- **Email Services**: Email sending functionality
- **File Storage Services**: Services for handling file operations
- **Identity Services**: Authentication and authorization services
- **Caching Services**: Implementation of caching mechanisms

## Example

```csharp
public class EmailService : IEmailService
{
    private readonly SmtpSettings _smtpSettings;

    public EmailService(IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        // Implementation for sending emails via SMTP
    }
}
```

These services implement the interfaces defined in the application layer, providing the concrete technical capabilities needed by the application.
