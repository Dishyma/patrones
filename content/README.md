# Patrones de Comportamiento en C#

Implementación de dos patrones de diseño **comportamentales** en C# (.NET 8), con el código organizado por capas (modelos, interfaces y componentes de cada patrón).

## Estructura

- **`CadenaResponsabilidad/`** — Solución del patrón **Chain of Responsibility**.
  Valida solicitudes de crédito encadenando validadores independientes.
- **`Command/`** — Solución del patrón **Command**.
  Control remoto de domótica que convierte acciones en objetos comando, con soporte de deshacer.

Cada carpeta contiene su propia solución (`.sln`) y su proyecto dentro de una subcarpeta, lista para abrir en Visual Studio o compilar desde la línea de comandos.

## Cómo ejecutar

Desde la raíz de cada solución:

```bash
# Patrón Cadena de Responsabilidad
cd CadenaResponsabilidad/CadenaResponsabilidad
dotnet run

# Patrón Command
cd Command/Command
dotnet run
```

Requiere el SDK de .NET 8 (o superior compatible).

## Organización por rol de patrón

### Cadena de Responsabilidad
- `Interfaces/IValidador.cs` — interfaz del handler.
- `Validators/ValidadorBase.cs` — handler base con la lógica de encadenamiento.
- `Validators/Validador*.cs` — handlers concretos.
- `Models/SolicitudCredito.cs` — objeto request que viaja por la cadena.
- `Program.cs` — cliente que arma y dispara la cadena.

### Command
- `Interfaces/IComando.cs` — interfaz del command.
- `Commands/Comando*.cs` — comandos concretos (invocan al receptor).
- `Models/Luz.cs`, `Models/Persiana.cs` — receptores con la lógica de negocio.
- `Invokers/ControlRemoto.cs` — invocador que dispara comandos y guarda historial.
- `Program.cs` — cliente que crea receptores, comandos e invocador.
