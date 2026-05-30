<div align="center">
  <h1>BeyondNet.Aop</h1>
  <p><strong>A high-performance Aspect-Oriented Programming (AOP) framework for .NET</strong></p>

  <p>
    <a href="README.en.md">🇬🇧 English</a> | <a href="README.es.md">🇪🇸 Español</a>
  </p>

  <p>
    <a href="https://www.nuget.org/profiles/BeyondNetCode">
      <img src="https://img.shields.io/badge/NuGet-BeyondNetCode-blue" alt="NuGet" />
    </a>
    <a href="https://github.com/beyondnetcode/Shell.Aop/actions">
      <img src="https://github.com/beyondnetcode/Shell.Aop/workflows/CI%20/%20CD/badge.svg" alt="Build" />
    </a>
  </p>
</div>

---

Welcome to **BeyondNet.Aop**! This framework allows you to implement cross-cutting concerns (such as logging, retries, caching, etc.) cleanly and efficiently using `.NET`'s native `DispatchProxy`, augmented with high-performance caching for reflection and dynamic compilation.

## Installation

### NuGet Packages

```bash
# Core AOP library
dotnet add package BeyondNetCode.Shell.Aop

# Pre-built aspects (retry, logging, etc.)
dotnet add package BeyondNetCode.Shell.Aop.Aspects

# Serilog integration (recommended for observability)
dotnet add package BeyondNetCode.Shell.Aop.Aspects.Logger.Serilog

# Dependency Injection installer
dotnet add package BeyondNetCode.Shell.Aop.DI
```

### Packages Overview

| Package | Description | NuGet |
|---------|-------------|-------|
| `BeyondNetCode.Shell.Aop` | Core AOP abstractions and executor | [link](https://www.nuget.org/packages/BeyondNetCode.Shell.Aop) |
| `BeyondNetCode.Shell.Aop.Aspects` | Pre-built retry, logger, and advice aspects | [link](https://www.nuget.org/packages/BeyondNetCode.Shell.Aop.Aspects) |
| `BeyondNetCode.Shell.Aop.Aspects.Logger` | Common.Logging integration | [link](https://www.nuget.org/packages/BeyondNetCode.Shell.Aop.Aspects.Logger) |
| `BeyondNetCode.Shell.Aop.Aspects.Logger.Serilog` | Serilog integration with structured logging | [link](https://www.nuget.org/packages/BeyondNetCode.Shell.Aop.Aspects.Logger.Serilog) |
| `BeyondNetCode.Shell.Aop.DispatchProxy` | Native DispatchProxy-based proxy creation | [link](https://www.nuget.org/packages/BeyondNetCode.Shell.Aop.DispatchProxy) |
| `BeyondNetCode.Shell.Aop.DI` | Microsoft.Extensions.DependencyInjection extensions | [link](https://www.nuget.org/packages/BeyondNetCode.Shell.Aop.DI) |

## Quick Start

```csharp
// 1. Define your aspect attribute
[AttributeUsage(AttributeTargets.Method)]
public class RetryAttribute : AbstractAspectAttribute
{
    public int MaxRetries { get; set; } = 3;
    public int DelayMs { get; set; } = 1000;
}

// 2. Implement your aspect
public class RetryAspect : OnMethodBoundaryAspect<RetryAttribute>
{
    protected override void OnEntry(IJoinPoint joinPoint)
    {
        Console.WriteLine($"Entering {joinPoint.MethodInfo.Name}");
    }

    protected override void OnException(IJoinPoint joinPoint, Exception ex)
    {
        // Implement retry logic
    }
}

// 3. Apply to your interface
public interface IMyService
{
    [Retry(MaxRetries = 3)]
    Task<string> DoSomethingAsync();
}

// 4. Register in DI
services.AddAopAspects(typeof(IMyService).Assembly);
```

## Documentation

For detailed documentation, see the language-specific README files:
- [English](README.en.md)
- [Español](README.es.md)

## Migration from Ums.Shell.Aop

If you were using `Ums.Shell.Aop`, update your NuGet references:

```bash
# Before (Ums.Shell.Aop)
dotnet add package Ums.Shell.Aop

# After (BeyondNetCode.Shell.Aop)
dotnet add package BeyondNetCode.Shell.Aop
```

Update namespaces in your code:
```csharp
// Before
using Ums.Shell.Aop;

// After
using BeyondNetCode.Shell.Aop;
```

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for GitFlow workflow, commit conventions, and coding standards.

## Versioning

See [VERSIONING.md](VERSIONING.md) for SemVer strategy and release process.

## License

Licensed under the Apache 2.0 License. See [LICENSE](LICENSE) for details.

## Acknowledgments

See [DISCLAIMER.md](DISCLAIMER.md) for original code authorship attribution.