# BeyondNet.Aop

<p>
  <a href="README.en.md">English</a> | <strong>Español</strong>
</p>

BeyondNet.Aop es un framework de Programacion Orientada a Aspectos (AOP) de alto rendimiento para .NET 10. Te permite separar limpiamente la logica transversal (como logging, manejo de errores, reintentos, etc.) de la logica central de negocio utilizando el `DispatchProxy` nativo de `.NET` combinado con fuertes optimizaciones de cache.

## Caracteristicas

- **Alto Rendimiento**: Usa `ConcurrentDictionary` para cachear llamadas de reflexion y compilacion dinamica de expresiones (busquedas en O(1)).
- **Codigo Limpio**: Convenciones de nombrado estrictas y excepciones semanticas.
- **.NET Nativo**: Construido sobre `System.Reflection.DispatchProxy` sin requerir herramientas complejas de compilacion post-procesada.
- **Extensible**: Facil de conectar a librerias de logging o motores de evaluacion personalizados.
- **Observabilidad**: Integracion nativa con Serilog con logging estructurado y propagacion de contexto de ejecucion.
- **Integracion DI**: Integracion seamless con Microsoft.Extensions.DependencyInjection.

## Arquitectura

El framework esta organizado en paquetes modulares:

```
BeyondNetCode.Shell.Aop              # Abstracciones core (IAspect, IJoinPoint, AspectExecutor)
BeyondNetCode.Shell.Aop.Aspects      # Aspectos pre-construidos (Retry, Logger, Advice)
BeyondNetCode.Shell.Aop.DispatchProxy # Creacion de proxies usando DispatchProxy
BeyondNetCode.Shell.Aop.Aspects.Logger # Integracion con Common.Logging
BeyondNetCode.Shell.Apect.Aspects.Logger.Serilog # Integracion con Serilog y logging estructurado
BeyondNetCode.Shell.Aop.DI           # Extensiones para Dependency Injection
```

## Instalacion

```bash
# Libreria core
dotnet add package BeyondNetCode.Shell.Aop

# Aspectos pre-construidos
dotnet add package BeyondNetCode.Shell.Aop.Aspects

# Integracion Serilog (recomendado)
dotnet add package BeyondNetCode.Shell.Aop.Aspects.Logger.Serilog

# Instalador DI
dotnet add package BeyondNetCode.Shell.Aop.DI
```

## Inicio Rapido

### Paso 1: Define un Atributo de Aspecto

```csharp
[AttributeUsage(AttributeTargets.Method)]
public class MyLogAttribute : AbstractAspectAttribute
{
    public string Message { get; set; }
}
```

### Paso 2: Implementa el Aspecto

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

### Paso 3: Aplica a Metodos de Interfaz

```csharp
public interface IMyService
{
    [MyLog(Message = "Starting operation")]
    Task<Result> DoWorkAsync();
}
```

### Paso 4: Registra en DI

```csharp
services.AddAopAspects(typeof(IMyService).Assembly);
```

## Temas Avanzados

### Pointcuts Personalizados

Implementa `IPointCut` para controlar cuando se aplican los aspectos:

```csharp
public class MyPointCut : IPointCut
{
    public bool CanApply(IJoinPoint joinPoint, Type aspectType)
    {
        // Logica personalizada
    }
}
```

### Orden de Aspectos

Usa la propiedad `Order` en los atributos para controlar el orden de ejecucion:

```csharp
[MyAspect(Order = 1)]
[AnotherAspect(Order = 2)]
void MyMethod() { }
```

### Reintento con Exponential Backoff

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

## Contribuir

Ver [CONTRIBUTING.md](CONTRIBUTING.md) para el flujo de GitFlow y estandares de codificacion.

## Versionado

Ver [VERSIONING.md](VERSIONING.md) para estrategia de SemVer.

## Licencia

Apache 2.0 - Ver [LICENSE](LICENSE)

## Reconocimientos

Ver [DISCLAIMER.md](DISCLAIMER.md) para atribucion de autoria del codigo original.