# Web API

This directory contains the Web API presentation layer, which exposes the application's functionality through HTTP endpoints.

## What goes here?

- **Controllers**: API endpoints that handle HTTP requests and responses
- **Middleware**: Custom HTTP request/response processing components
- **Filters**: Action and exception filters for cross-cutting concerns
- **API Models**: Request and response models specific to the API

## Example Structure

```
WebApi/
├── Controllers/
│   ├── CustomersController.cs
│   └── ProductsController.cs
├── Middleware/
│   └── ExceptionHandlingMiddleware.cs
├── Filters/
│   └── ApiExceptionFilterAttribute.cs
├── Models/
│   ├── Requests/
│   └── Responses/
├── Program.cs
└── appsettings.json
```

This layer depends only on the Application layer and should not contain business logic. Its primary responsibility is handling HTTP communication and translating between HTTP and the application's use cases.
