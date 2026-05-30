# BeyondNet.Aop

<p>
  <strong>English</strong> | <a href="README.es.md">Español</a>
</p>

BeyondNet.Aop is a high-performance Aspect-Oriented Programming (AOP) framework for .NET 10. It allows you to cleanly separate cross-cutting concerns (like logging, error handling, retries, etc.) from your core business logic using native `.NET` `DispatchProxy` with heavy caching optimizations.

## Features

- **High Performance**: Uses `ConcurrentDictionary` to cache reflection calls and dynamic expression compilation (O(1) lookups).
- **Clean Code**: Strict naming conventions and semantic exceptions.
- **Native .NET**: Built on top of `System.Reflection.DispatchProxy` without requiring complex post-compilation weaving tools.
- **Extensible**: Easily plug in custom loggers or evaluation engines.
- **Observability**: Built-in Serilog integration with structured logging and execution context propagation.
- **DI Integration**: Seamless integration with Microsoft.Extensions.DependencyInjection.

## Architecture

The framework is organized into modular packages:

```
BeyondNetCode.Shell.Aop              # Core abstractions (IAspect, IJoinPoint, AspectExecutor)
BeyondNetCode.Shell.Aop.Aspects      # Pre-built aspects (Retry, Logger, Advice)
BeyondNetCode.Shell.Aop.DispatchProxy # Proxy creation using DispatchProxy
BeyondNetCode.Shell.Aop.Aspects.Logger # Common.Logging integration
BeyondNetCode.Shell.Aop.Aspects.Logger.Serilog # Serilog integration with structured logging
BeyondNetCode.Shell.Aop.DI           # Dependency Injection extensions
```

## Installation

```bash
# Core library
dotnet add package BeyondNetCode.Shell.Aop

# Pre-built aspects
dotnet add package BeyondNetCode.Shell.Aop.Aspects

# Serilog integration (recommended)
dotnet add package BeyondNetCode.Shell.Aop.Aspects.Logger.Serilog

# DI installer
dotnet add package BeyondNetCode.Shell.Aop.DI
```

## Quick Start

### Step 1: Define an Aspect Attribute

```csharp
[AttributeUsage(AttributeTargets.Method)]
public class MyLogAttribute : AbstractAspectAttribute
{
    public string Message { get; set; }
}
```

### Step 2: Implement the Aspect

```csharp
public class MyLogAspect : OnMethodBoundaryAspect<MyLogAttribute>
{
    protected override void OnEntry(IJoinPoint joinPoint)
    {
        var attr = GetAttribute(joinPoint);
        Console.WriteLine($"Method {joinPoint.MethodInfo.Name}: {attr.Message}");
    }
}
```

### Step 3: Apply to Interface Methods

```csharp
public interface IMyService
{
    [MyLog(Message = "Starting operation")]
    Task<Result> DoWorkAsync();
}
```

### Step 4: Register in DI

```csharp
services.AddAopAspects(typeof(IMyService).Assembly);
```

## Advanced Topics

### Custom Pointcuts

Implement `IPointCut` to control when aspects are applied:

```csharp
public class MyPointCut : IPointCut
{
    public bool CanApply(IJoinPoint joinPoint, Type aspectType)
    {
        // Custom logic
    }
}
```

### Aspect Ordering

Use the `Order` property on attributes to control execution order:

```csharp
[MyAspect(Order = 1)]
[AnotherAspect(Order = 2)]
void MyMethod() { }
```

### Retry with Exponential Backoff

```csharp
public class RetryAttribute : AbstractAspectAttribute
{
    public int MaxRetries { get; set; } = 3;
    public int BaseDelayMs { get; set; } = 1000;
}

public class RetryAspect : OnRetryAspect<RetryAttribute>
{
    protected override bool CanRetry(IJoinPoint joinPoint, Exception ex)
    {
        return GetAttribute(joinPoint) is { } attr &&
               attr.MaxRetries > 0;
    }
}
```

## Testing

```bash
dotnet test
```

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for GitFlow workflow and coding standards.

## Versioning

See [VERSIONING.md](VERSIONING.md) for SemVer strategy.

## License

Apache 2.0 - See [LICENSE](LICENSE)

## Acknowledgments

See [DISCLAIMER.md](DISCLAIMER.md) for original code authorship.