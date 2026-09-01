# Guía de Estudio Completa: Principios SOLID y Patrones de Diseño Creacionales

> **Basada en las presentaciones del MSc. César Augusto López Gallego** (Profesor Facultad Ingeniería en TIC – UPB, Coordinador Área de Programación, Computación y Desarrollo de Software — cesar.lopezg@upb.edu.co)
>
> Contenido: 53 diapositivas de *Principios SOLID* + 46 diapositivas de *Patrones Creacionales*, con todos los ejemplos, diagramas descritos, códigos en C#, casos de estudio y conceptos previos necesarios para entender todo sin vacíos.

---

## Tabla de Contenidos

1. [Conceptos Previos Indispensables](#1-conceptos-previos-indispensables)
2. [¿Qué es SOLID y para qué existe?](#2-qué-es-solid-y-para-qué-existe)
3. [S — Principio de Responsabilidad Única (SRP)](#3-s--principio-de-responsabilidad-única-srp)
4. [O — Principio Abierto/Cerrado (OCP)](#4-o--principio-abiertocerrado-ocp)
5. [L — Principio de Sustitución de Liskov (LSP)](#5-l--principio-de-sustitución-de-liskov-lsp)
6. [I — Principio de Segregación de Interfaces (ISP)](#6-i--principio-de-segregación-de-interfaces-isp)
7. [D — Principio de Inversión de Dependencia (DIP) e Inyección de Dependencias](#7-d--principio-de-inversión-de-dependencia-dip-e-inyección-de-dependencias)
8. [Caso de Estudio 1: Proyecto del Concesionario](#8-caso-de-estudio-1-proyecto-del-concesionario)
9. [Caso de Estudio 2: Sistema de Tránsito (análisis clase a clase)](#9-caso-de-estudio-2-sistema-de-tránsito-análisis-clase-a-clase)
10. [Introducción a los Patrones de Diseño](#10-introducción-a-los-patrones-de-diseño)
11. [Patrón Factory Method (Método de Fábrica)](#11-patrón-factory-method-método-de-fábrica)
12. [Patrón Abstract Factory (Fábrica Abstracta)](#12-patrón-abstract-factory-fábrica-abstracta)
13. [Patrón Builder (Constructor)](#13-patrón-builder-constructor)
14. [Patrón Prototype (Prototipo)](#14-patrón-prototype-prototipo)
15. [Patrón Singleton](#15-patrón-singleton)
16. [Relación entre SOLID y los Patrones Creacionales](#16-relación-entre-solid-y-los-patrones-creacionales)
17. [Tablas Resumen (Cheat Sheets)](#17-tablas-resumen-cheat-sheets)
18. [Preguntas de Autoevaluación](#18-preguntas-de-autoevaluación)
19. [Glosario Rápido](#19-glosario-rápido)
20. [Bibliografía y Cibergrafía](#20-bibliografía-y-cibergrafía)

---

## 1. Conceptos Previos Indispensables

Antes de entrar en SOLID y en los patrones, hay conceptos de **Programación Orientada a Objetos (OOP)** y de diseño que se asumen conocidos en las diapositivas. Aquí se explican todos con ejemplos.

### 1.1 Clase, Objeto, Atributo y Método

- **Clase**: plantilla o "molde" que define cómo serán los objetos (qué datos guardan y qué pueden hacer).
- **Objeto**: instancia concreta creada a partir de una clase (con el operador `new`).
- **Atributo**: dato que guarda el objeto (su estado).
- **Método**: comportamiento que el objeto puede ejecutar.

```csharp
public class Automovil              // Clase (el molde)
{
    public string Placa;            // Atributo (estado)
    public void Arrancar()          // Método (comportamiento)
    {
        Console.WriteLine("Arrancando...");
    }
}

Automovil miAuto = new Automovil(); // Objeto (instancia)
miAuto.Arrancar();
```

### 1.2 Los 4 pilares de la OOP

| Pilar | Idea | Ejemplo sencillo |
|---|---|---|
| **Abstracción** | Modelar solo lo esencial de algo, ocultando lo irrelevante | Una clase `Automovil` tiene `Arrancar()`; al conductor no le importa la mezcla de combustible interna |
| **Encapsulamiento** | Proteger los datos internos; solo se accede por vías controladas (propiedades/métodos) | Un atributo `private int velocidad` que solo cambia mediante `Acelerar()` con validaciones |
| **Herencia** | Una clase hija reutiliza y extiende a una clase padre (relación "ES UN") | `Deportivo : Automovil` → un deportivo **es un** automóvil |
| **Polimorfismo** | El mismo mensaje (método) se comporta distinto según el objeto que lo reciba | Una lista de `Automovil` donde cada subclase ejecuta su propio `Arrancar()` |

### 1.3 Interfaz vs Clase Abstracta (¡muy usado en las diapositivas!)

- **Interfaz** (`interface`): contrato puro. Solo declara firmas de métodos, sin implementación. Una clase puede implementar **varias** interfaces.
- **Clase abstracta** (`abstract class`): puede tener métodos implementados y métodos abstractos (sin cuerpo). No se puede instanciar directamente. Una clase solo hereda de **una** clase abstracta (herencia simple en C#).

```csharp
// Interfaz: solo el contrato
public interface IMotor
{
    void Encender();   // sin cuerpo, sin implementación
}

// Clase abstracta: contrato + algo de implementación
public abstract class Automovil
{
    public abstract void Conducir();              // abstracto: las hijas deben implementarlo
    public void AbrirPuerta()                     // concreto: ya viene implementado
    {
        Console.WriteLine("Puerta abierta");
    }
}

public class MotorCombustion : IMotor             // "Implementa" la interfaz
{
    public void Encender() => Console.WriteLine("Motor de combustión encendido.");
}
```

> **Regla práctica que usa el curso**: cuando solo necesitas definir *qué* debe hacer algo → interfaz. Cuando además quieres compartir código común entre hijas → clase abstracta.

### 1.4 Acoplamiento y Cohesión

- **Acoplamiento**: grado de dependencia entre clases/módulos.
  - *Alto acoplamiento* ❌: cambiar una clase obliga a cambiar muchas otras. Ejemplo: `Automovil` haciendo `new Motor()` internamente.
  - *Bajo acoplamiento* ✅: las clases dependen de **abstracciones**, no de implementaciones concretas.
- **Cohesión**: qué tan enfocada está una clase en una sola tarea.
  - *Alta cohesión* ✅: la clase hace una sola cosa y bien.
  - *Baja cohesión* ❌: la clase hace de todo ("clase Dios").

> Todos los principios SOLID persiguen lo mismo: **bajo acoplamiento + alta cohesión**.

### 1.5 "ES UN" (herencia) vs "TIENE UN" (composición)

- **Herencia ("ES UN")**: `Gasolina ES UN Automovil`.
- **Composición ("TIENE UN")**: `Automovil TIENE UN Motor`.

Las diapositivas insisten: si la relación "ES UN" no es clara, probablemente sea mejor usar **composición** (un objeto contiene a otro) en lugar de herencia. Esto se retoma en LSP.

### 1.6 UML básico para leer los diagramas del curso

| Símbolo | Significado |
|---|---|
| Rectángulo con 3 compartimentos | Clase: nombre / atributos / métodos |
| `«interface»` | Es una interfaz |
| `+` / `-` | Público / Privado |
| Flecha continua con triángulo vacío | Herencia ("Es una") |
| Flecha punteada con triángulo vacío | Implementación de interfaz ("Implementa") |
| Rombo vacío `◇` | Agregación ("tiene", débil) |
| Rombo lleno `◆` | Composición ("es parte de", fuerte) |
| Flecha punteada simple | Dependencia ("Depende de" / "Usa") |

### 1.7 ¿Qué es un patrón de diseño?

Un **patrón de diseño** es una solución probada y reutilizable a un problema recurrente de diseño de software. No es código listo para copiar: es una *estructura* que se adapta a cada caso.

Los patrones clásicos (catálogo GoF — *Gang of Four*, 1994) se clasifican en 3 familias:

1. **Creacionales** → cómo se **crean** los objetos. (Los de esta guía: Factory Method, Abstract Factory, Builder, Prototype, Singleton.)
2. **Estructurales** → cómo se **componen** clases y objetos entre sí.
3. **De comportamiento** → cómo se **comunican** y reparten responsabilidades los objetos.

### 1.8 Conceptos transversales que aparecen en las diapositivas

- **Refactorización / rediseño**: modificar la estructura interna del código sin cambiar lo que hace, para mejorar su calidad.
- **Inyección de dependencias (DI)**: darle a una clase sus dependencias desde afuera (por ejemplo por el constructor) en lugar de que ella las cree con `new`. Se explica a fondo en la sección 7.
- **Value Object**: objeto pequeño que representa un valor con reglas propias (ej.: una `Placa` que se valida a sí misma) en lugar de un simple `string`.
- **Patrón Observer**: mecanismo de eventos donde un objeto *publica* y otros *se suscriben*. Se menciona como alternativa a lanzar excepciones para notificar.
- **Pruebas unitarias**: pruebas a clases/métodos aislados. Requieren poder *simular* (mockear) las dependencias — imposible si la clase crea sus dependencias con `new`.
- **Hardcodear**: dejar valores fijos escritos en el código (ej. `cant_sal_min = 3`) en vez de leerlos de configuración.

---

## 2. ¿Qué es SOLID y para qué existe?

**SOLID** es un acrónimo de 5 principios de diseño promovidos por **Robert C. Martin ("Uncle Bob")**.

> **Objetivo SOLID**: promover buenas prácticas de diseño de software, en particular en programación orientada a objetos (OOP), abordando problemas comunes que enfrentan los desarrolladores a medida que los sistemas de software crecen en tamaño y complejidad.

| Letra | Principio | En una frase |
|---|---|---|
| **S** | Single Responsibility (SRP) | Una clase, una sola razón para cambiar |
| **O** | Open/Closed (OCP) | Abierto a extensión, cerrado a modificación |
| **L** | Liskov Substitution (LSP) | Una subclase debe poder reemplazar a su superclase sin romper nada |
| **I** | Interface Segregation (ISP) | Interfaces pequeñas y específicas, no una gigante |
| **D** | Dependency Inversion (DIP) | Depende de abstracciones, no de clases concretas |

**Beneficio general de aplicarlos** (aparece transversalmente en todo el mazo):
- Reduce la complejidad.
- Mejora la legibilidad y comprensión del código.
- Facilita mantener y ampliar el software.
- Baja el acoplamiento.
- Hace el código más fácil de probar.

---

## 3. S — Principio de Responsabilidad Única (SRP)

### 3.1 Definición (diapositiva 4)

> **"Una clase debe tener solo una razón (o preocupación) para cambiar."**

- Debe tener solo una tarea simple y bien definida dentro del software.

**Beneficios**:
- Reduce la complejidad.
- Mejora la legibilidad y la comprensión.
- Facilita mantener y ampliar el código.

**¿Cómo?** → **Dividir las clases en unidades más pequeñas y más específicas.**

### 3.2 Ejemplo 1: Employee y el reporte de horas (diapositiva 5, de *Dive into Design Patterns*)

**ANTES — una clase con DOS preocupaciones:**

```
┌─────────────────────────┐
│        Employee         │
├─────────────────────────┤
│ - name                  │
├─────────────────────────┤
│ + getName()             │
│ + printTimeSheetReport()│
└─────────────────────────┘
```

- **1ª preocupación**: la gestión de los datos del empleado cuando estos cambian. **Esta es la principal función de esta clase.**
- **2ª preocupación**: ¿qué pasa si el formato de tiempo cambia? El reporte tendría que modificarse... y con él la clase `Employee`.

El problema: si cambia el formato del reporte, hay que tocar `Employee`, aunque los datos del empleado no hayan cambiado. **Dos razones para cambiar = violación de SRP.**

**DESPUÉS — se dividen las clases en unidades más pequeñas y más específicas:**

```
┌──────────────────┐        ┌──────────────┐
│  TimeSheetReport │───────▶│   Employee   │
├──────────────────┤        ├──────────────┤
│ ...              │        │ - name       │
├──────────────────┤        ├──────────────┤
│ + print(employee)│        │ + getName()  │
└──────────────────┘        └──────────────┘
```

- `Employee` solo gestiona datos del empleado.
- `TimeSheetReport` solo se ocupa de imprimir reportes.

### 3.3 Ejemplo 2: User, EmailSender y UserService (diapositiva 6, de FreeCodeCamp)

**ANTES — la clase `User` registra usuarios Y envía correos:**

```csharp
public class User
{
    public string Username { get; set; }   // (1)
    public string Email { get; set; }      // (1)

    public void Register()                 // (2)
    {
        // Register user logic, e.g. save to database...

        // Send email notification
        EmailSender emailSender = new EmailSender();
        emailSender.SendEmail("Welcome to our platform!", Email);
    }
}
```

Preguntas guía de la diapositiva:
- **¿Cuáles son los cambios que preocupan a la clase?** → (1) los datos del usuario y (2) la lógica de registro + notificación.
- **¿EmailSender es una preocupación?** → Sí: enviar correos es una responsabilidad distinta a "ser un usuario".

**DESPUÉS — cada clase con su única responsabilidad:**

```csharp
// Responsabilidad 1: enviar correos
public class EmailSender
{
    public void SendEmail(string message, string recipient)
    {
        // Email sending logic
        Console.WriteLine($"Sending email to {recipient}: {message}");
    }
}

// Responsabilidad 2: solo datos del usuario
public class User
{
    public string Username { get; set; }
    public string Email { get; set; }
}

// Responsabilidad 3: la lógica de registro
public class UserService
{
    public void RegisterUser(User user)
    {
        // Register user logic...

        EmailSender emailSender = new EmailSender();
        emailSender.SendEmail("Welcome to our platform!", user.Email);
    }
}
```

**Moraleja SRP**: pregúntate *"¿cuántas razones distintas podrían obligarme a editar esta clase?"*. Si la respuesta es más de una, divide.

---

## 4. O — Principio Abierto/Cerrado (OCP)

### 4.1 Definición (diapositiva 7)

> **"Las clases deben estar abiertas para extensión pero cerradas para modificación."**

- **Objetivo**: evitar que el código existente colapse cuando se implementan nuevas características.
- **Una clase está ABIERTA** si se puede extender: hacer lo que se quiera con ella — agregar nuevos métodos o campos, anular un comportamiento, etc.
- **Una clase está CERRADA** si está 100% lista para ser utilizada por otras clases: su interfaz está claramente definida y no se cambiará en el futuro.

**Aclaración importante de las diapositivas:**
> Si la clase principal tiene un error, esta es la que se debe corregir. **NO aplica la creación de una subclase para resolver el error.** Una clase secundaria no debería ser responsable de los problemas de la clase principal.

- OCP **fomenta el uso de la abstracción y el polimorfismo**, permitiendo que el código se extienda a través de la **herencia o la composición**.
- OCP invita a diseñar el software de modo que **para agregar nuevas funcionalidades, la vía sea agregar código nuevo** (no editar el viejo). Esto crea software fácil de mantener y con **acoplamiento bajo**.

### 4.2 Ejemplo 1: Order y los envíos (diapositiva 8, de *Dive into Design Patterns*)

**ANTES**: la clase `Order` calcula los costos de envío con condicionales por tipo:

```
┌──────────────────────┐
│        Order         │
├──────────────────────┤
│ - lineItems          │
│ - shipping           │
├──────────────────────┤
│ + getTotal()         │
│ + getTotalWeight()   │
│ + setShippingType(st)│
│ + getShippingCost()  │──▶ if (shipping == "ground") { ... return max(10, getTotalWeight() * 1.5) }
│ + getShippingDate()  │    if (shipping == "air")    { ... return max(20, getTotalWeight() * 3) }
└──────────────────────┘
```

**Problema**: ¿llega un nuevo medio de envío (barco, dron)? Hay que **modificar** la clase `Order` agregando otro `if`. Cada cambio arriesga romper lo que ya funciona.

**DESPUÉS — se extrae la interfaz `Shipping`:**

```
┌──────────────────────┐        ┌──────────────────────────┐
│        Order         │ ◇─────▶│     «interface»          │
├──────────────────────┤        │        Shipping          │
│ - lineItems          │        ├──────────────────────────┤
│ - shipping: Shipping │        │ + getCost(order)         │
├──────────────────────┤        │ + getDate(order)         │
│ + getTotal()         │        └───────────▲──────────────┘
│ + getTotalWeight()   │                    │ implementan
│ + setShippingType()  │        ┌───────────┴───────────┐
│ + getShippingCost()  │        │                       │
│   return shipping.   │   ┌────┴─────┐           ┌─────┴───┐
│        getCost(this) │   │  Ground  │           │   Air   │
└──────────────────────┘   ├──────────┤           ├─────────┤
                           │+getCost()│           │+getCost()│
                           │+getDate()│           │+getDate()│
                           └──────────┘           └─────────┘
```

Cada clase de envío (`Ground`, `Air`) implementa su propia lógica (ej. `Ground`: gratis si el pedido supera 100; si no, `max(10, peso * 1.5)`). **Para agregar un nuevo tipo de envío se crea una clase NUEVA que implementa `Shipping`; `Order` no se toca.**

### 4.3 Ejemplo 2: Shape, Circle y Rectangle (diapositiva 9, de FreeCodeCamp)

**ANTES — una sola clase con `switch`:**

```csharp
public enum ShapeType
{
    Circle,
    Rectangle
}

public class Shape
{
    public ShapeType Type { get; set; }
    public double Radius { get; set; }
    public double Length { get; set; }
    public double Width { get; set; }

    public double CalculateArea()
    {
        switch (Type)
        {
            case ShapeType.Circle:
                return Math.PI * Math.Pow(Radius, 2);
            case ShapeType.Rectangle:
                return Length * Width;
            default:
                throw new InvalidOperationException("Unsupported shape type.");
        }
    }
}
```

**Problema**: cada nueva figura obliga a **editar** `CalculateArea()` y el enum.

**DESPUÉS — clase abstracta + subclases (extensión sin modificación):**

```csharp
public abstract class Shape
{
    public abstract double CalculateArea();
}

public class Circle : Shape
{
    public double Radius { get; set; }

    public override double CalculateArea()
    {
        return Math.PI * Math.Pow(Radius, 2);
    }
}

public class Rectangle : Shape
{
    public double Length { get; set; }
    public double Width { get; set; }

    public override double CalculateArea()
    {
        return Length * Width;
    }
}
```

¿Nueva figura `Triangle`? Se crea la clase nueva y ya. `Shape`, `Circle` y `Rectangle` quedan intactas → **abierto a extensión, cerrado a modificación.**

---

## 5. L — Principio de Sustitución de Liskov (LSP)

### 5.1 Definición (diapositiva 10)

> **"Los objetos de una clase derivada deben poder sustituir a los objetos de su clase base sin alterar el comportamiento esperado del programa."**

- Este principio **nos obliga a pensar muy bien el diseño de la herencia**.
- Al extender una clase, se debe poder pasar objetos de la subclase en lugar de objetos de la superclase **sin romper el código del cliente**.
- **La subclase debe seguir siendo compatible con el comportamiento de la superclase.**

### 5.2 ¿Qué tener en cuenta al diseñar la herencia? (diapositiva 11)

- 🔍 **Evitar herencias incorrectas**: no forzar subclases que no cumplen el mismo contrato que la superclase.
- 🔍 **Aplicar interfaces**: en lugar de heredar métodos innecesarios, definir interfaces específicas.
- 🔍 **Usar composición en lugar de herencia**: si una relación "ES UN" no es clara, quizás sea mejor que un objeto "TENGA UN" otro objeto.

### 5.3 Ejemplo UML: Document y ReadOnlyDocument (diapositiva 12)

**ANTES (viola LSP)**:

```
┌──────────────┐          ┌─────────────────────┐
│   Document   │◆────────▶│       Project       │
├──────────────┤          ├─────────────────────┤
│ - data       │          │ - documents         │
│ - filename   │          ├─────────────────────┤
├──────────────┤          │ + openAll()         │
│ + open()     │          │ + saveAll()         │
│ + save()     │          └─────────────────────┘
└──────▲───────┘
       │
┌──────┴───────────┐   save() → throw new Exception("Can't save a read-only document.")
│ ReadOnlyDocument │
├──────────────────┤   saveAll() tiene que preguntar:
│ + save()  ⌀      │   if (doc instanceof ReadOnlyDocument) → saltarlo
└──────────────────┘
```

**Problema**: `ReadOnlyDocument` hereda `save()` pero no puede guardar → o lanza excepción, o el cliente (`Project.saveAll()`) tiene que revisar tipos con `instanceof`. **La subclase rompe el comportamiento esperado.**

**SOLUCIÓN** (según la diapositiva): *"La clase Document es un documento readonly y la clase WritableDocument extiende Document e implementa el comportamiento save."*

```
┌──────────────┐          ┌──────────────────────┐
│   Document   │◆────────▶│       Project        │
├──────────────┤          ├──────────────────────┤
│ - data       │          │ - allDocs            │
│ - filename   │          │ - writableDocs       │
├──────────────┤          ├──────────────────────┤
│ + open()     │          │ + openAll()  → foreach (doc in allDocs) doc.open()
└──────▲───────┘          │ + saveAll()  → foreach (doc in writableDocs) doc.save()
       │                  └──────────────────────┘
┌──────┴───────────┐
│ WritableDocument │
├──────────────────┤
│ + save()         │
└──────────────────┘
```

Se invierte la jerarquía: lo común y seguro (`open`) queda en la base; lo que no todos pueden hacer (`save`) queda en la subclase. Así **ningún objeto recibe un mensaje que no pueda atender**.

### 5.4 Ejemplo de código completo: Automóviles eléctricos y de gasolina (diapositivas 13–15)

**VIOLACIÓN DE LSP (diapositiva 13):**

```csharp
abstract class Automovil
{
    // Método genérico para cargar energía o combustible
    public abstract void Repostar();
}

class Electrico : Automovil
{
    public override void Repostar()
    {
        throw new NotImplementedException("Los autos eléctricos no pueden repostar gasolina.");
    }
}

class Gasolina : Automovil
{
    public override void Repostar()
    {
        Console.WriteLine("Cargando gasolina...");
    }
}

class Program
{
    static void Main()
    {
        List<Automovil> autos = new List<Automovil> {
            new Gasolina(),
            new Electrico()  // 🚨 Este fallará si se llama a Repostar()
        };

        foreach (var auto in autos)
        {
            auto.Repostar(); // ❌ Violación del LSP: Electrico NO puede ejecutar este método
        }
    }
}
```

**¿Por qué viola LSP?** El cliente trabaja con `Automovil` y espera que **cualquier** `Automovil` pueda `Repostar()`. Pero `Electrico` no puede → la sustitución rompe el programa (excepción en tiempo de ejecución).

**CORRECCIÓN (diapositiva 14) — interfaces específicas:**

```csharp
abstract class Automovil
{
    public abstract void Conducir();
}

// Interfaz para autos que usan gasolina
interface ICombustible
{
    void Repostar();
}

// Interfaz para autos eléctricos
interface IElectrico
{
    void CargarBateria();
}

class Gasolina : Automovil, ICombustible
{
    public override void Conducir()
    {
        Console.WriteLine("Conduciendo un auto de gasolina...");
    }

    public void Repostar()
    {
        Console.WriteLine("Cargando gasolina...");
    }
}

class Electrico : Automovil, IElectrico
{
    public override void Conducir()
    {
        Console.WriteLine("Conduciendo un auto eléctrico...");
    }

    public void CargarBateria()
    {
        Console.WriteLine("Cargando batería...");
    }
}
```

**Uso correcto (diapositiva 15):**

```csharp
class Program
{
    static void Main()
    {
        List<Automovil> autos = new List<Automovil> {
            new Gasolina(),
            new Electrico()
        };

        foreach (var auto in autos)
        {
            auto.Conducir(); // ✅ Ambos pueden conducir sin problemas
        }

        // Lista de autos a gasolina
        List<ICombustible> autosGasolina = new List<ICombustible> {
            new Gasolina()
        };
        foreach (var auto in autosGasolina)
        {
            auto.Repostar(); // ✅ Solo los de gasolina pueden repostar
        }

        // Lista de autos eléctricos
        List<IElectrico> autosElectricos = new List<IElectrico> {
            new Electrico()
        };

        foreach (var auto in autosElectricos)
        {
            auto.CargarBateria(); // ✅ Solo los eléctricos pueden cargar batería
        }
    }
}
```

**Cierre del ejemplo (diapositiva 16):**
- 📌 **Error común**: heredar métodos que no tienen sentido en algunas subclases.
- 📌 **Solución**: usar interfaces y dividir correctamente las responsabilidades.
- 📌 **Resultado**: código más limpio, flexible y fácil de mantener.

### 5.5 Los 7 requisitos formales de LSP (diapositivas 17–24)

La diapositiva 17 lista los requisitos formales para las subclases (y específicamente para sus métodos), y cada uno se ejemplifica con código:

---

**✅ Regla 1: Los tipos de parámetros en un método de una subclase deben coincidir o ser más abstractos** *(diapositiva 18)*

> 📌 Una subclase **no debe restringir** los tipos de parámetros. Puede aceptar un tipo **más general**, pero no más específico.

```csharp
// ❌ MAL — el parámetro se volvió más específico (Auto < Vehiculo)
class Taller {
    public virtual void Reparar(Vehiculo vehiculo) {
        Console.WriteLine("Reparando vehículo...");
    }
}

class TallerEspecializado : Taller {
    public override void Reparar(Auto auto) { // ❌ Error: más específico que Vehiculo
        Console.WriteLine("Reparando auto...");
    }
}

// ✅ BIEN — mismo tipo (o más abstracto)
class TallerEspecializado : Taller {
    public override void Reparar(Vehiculo vehiculo) { // ✅ Tipo igual o más abstracto
        Console.WriteLine("Reparando vehículo en taller especializado...");
    }
}
```

*¿Por qué?* Si el cliente llama `Reparar(miMoto)` donde `Moto : Vehiculo`, la subclase mala no podría atenderla → se rompe la sustitución.

---

**✅ Regla 2: El tipo de retorno debe coincidir o ser un subtipo** *(diapositiva 19)*

> 📌 El método en la subclase puede devolver un tipo **más específico** que la superclase.

```csharp
// ❌ MAL — cambió el tipo de retorno a algo incompatible
class Fabrica {
    public virtual Vehiculo CrearVehiculo() {
        return new Vehiculo();
    }
}

class FabricaAutos : Fabrica {
    public override string CrearVehiculo() { // ❌ Error: cambio de tipo de retorno
        return "Auto creado";
    }
}

// ✅ BIEN — Auto es subtipo de Vehiculo
class FabricaAutos : Fabrica {
    public override Auto CrearVehiculo() { // ✅ En este caso Auto:Vehiculo. Tipo de retorno más específico.
        return new Auto();
    }
}
```

*Lógica*: el cliente espera un `Vehiculo`; recibir un `Auto` (que ES UN Vehiculo) nunca lo sorprende.

---

**✅ Regla 3: Una subclase no debe generar tipos de excepciones inesperadas** *(diapositiva 20)*

> 📌 Si la superclase no lanza excepciones, la subclase tampoco debería lanzar excepciones inesperadas.

```csharp
// ❌ MAL
class Vehiculo {
    public virtual void Arrancar() {
        Console.WriteLine("Vehículo arrancando...");
    }
}

class Auto : Vehiculo {
    public override void Arrancar() {
        throw new InvalidOperationException("Fallo en el arranque"); // ❌ Error inesperado
    }
}

// ✅ BIEN
class Auto : Vehiculo {
    public override void Arrancar() {
        Console.WriteLine("Auto arrancando...");
    }
}
```

---

**✅ Regla 4: Una subclase no debe reforzar las condiciones previas (precondiciones)** *(diapositiva 22)*

> 📌 Las condiciones previas en la subclase deben ser **iguales o más débiles** que en la superclase.

```csharp
// ❌ MAL — agrega una exigencia que la base no tenía
class Vehiculo {
    public virtual void CargarCombustible(int litros) {
        Console.WriteLine($"Cargando {litros} litros de combustible...");
    }
}

class Auto : Vehiculo {
    public override void CargarCombustible(int litros) {
        if (litros < 10) {
            throw new ArgumentException("Se deben cargar al menos 10 litros"); // ❌ Más restrictivo
        }
        Console.WriteLine($"Cargando {litros} litros en el auto...");
    }
}

// ✅ BIEN — sin restricciones adicionales
class Auto : Vehiculo {
    public override void CargarCombustible(int litros) {
        Console.WriteLine($"Cargando {litros} litros en el auto...");
    }
}
```

*Idea*: lo que la superclase aceptaba, la subclase también debe aceptarlo.

---

**✅ Regla 5: Una subclase no debe debilitar las condiciones posteriores (postcondiciones)** *(diapositiva 21)*

> 📌 Si un método en la superclase garantiza un estado después de ejecutarse, la subclase **no puede romper esa garantía**.

```csharp
// ❌ MAL
class Vehiculo {
    public virtual int ObtenerVelocidadMaxima() {
        return 200;
    }
}

class Bicicleta : Vehiculo {
    public override int ObtenerVelocidadMaxima() {
        return -10; // ❌ No tiene sentido una velocidad negativa
    }
}

// ✅ BIEN
class Bicicleta : Vehiculo {
    public override int ObtenerVelocidadMaxima() {
        return 50; // ✅ Mantiene la condición esperada (velocidad positiva)
    }
}
```

---

**✅ Regla 6: Las invariantes de una superclase deben conservarse** *(diapositiva 23)*

> 📌 Las reglas que definen un objeto deben seguir siendo válidas en sus subclases. (Una **invariante** es una condición que siempre se cumple durante la vida del objeto, ej.: "la velocidad inicia en 0".)

```csharp
// ❌ MAL
class Vehiculo {
    protected int velocidad;

    public Vehiculo() {
        velocidad = 0; // 🚗 Siempre inicia en 0  ← invariante
    }
}

class Auto : Vehiculo {
    public Auto() {
        velocidad = -100; // ❌ No tiene sentido que un auto inicie con velocidad negativa
    }
}

// ✅ BIEN
class Auto : Vehiculo {
    public Auto() {
        velocidad = 0; // ✅ Mantiene la invariante de la superclase
    }
}
```

---

**✅ Regla 7: Una subclase no debe cambiar los valores de los campos privados de la superclase** *(diapositiva 24)*

> 📌 Los atributos privados de la superclase no deben modificarse directamente en la subclase.

```csharp
// ❌ MAL
class Vehiculo {
    private int velocidad = 100; // 🚗 Configuración interna
}

class Auto : Vehiculo {
    public Auto() {
        this.velocidad = 200; // ❌ No se puede modificar un campo privado
    }
}

// ✅ BIEN — se usa un campo "protected" en lugar de "private" (vía controlada)
class Vehiculo {
    protected int velocidad = 100;
}

class Auto : Vehiculo {
    public Auto() {
        this.velocidad = 150; // ✅ Se usa un campo "protected" en lugar de "private"
    }
}
```

---

### 5.6 Ventajas de aplicar LSP (diapositiva 25)

- ✅ **Código más robusto y mantenible** 🛠️ — evita errores por comportamientos inesperados de subclases.
- ✅ **Mayor reutilización de código** 🔄 — diseñar correctamente la jerarquía permite extender funcionalidades sin problemas.
- ✅ **Facilita la escalabilidad** 📈 — se pueden agregar nuevas subclases sin romper el código existente.

---

## 6. I — Principio de Segregación de Interfaces (ISP)

### 6.1 Definición (diapositiva 26)

> **"Una interfaz no debe obligar a una clase a implementar métodos que no usa."**

- Es mejor tener **varias interfaces pequeñas y específicas** en lugar de una única interfaz grande y genérica que fuerce a las clases a implementar métodos innecesarios.

### 6.2 Ejemplo UML: CloudProvider (diapositiva 26)

**ANTES — interfaz "gorda":**

```
┌───────────────────────────┐
│    «interface»            │
│     CloudProvider         │
├───────────────────────────┤
│ + storeFile(name)         │
│ + getFile(name)           │
│ + createServer(region)    │
│ + listServers(region)     │
│ + getCDNAddress()         │
└─────────────▲─────────────┘
       ┌──────┴──────┐
┌──────┴──────┐ ┌────┴─────────┐
│   Amazon    │ │   Dropbox    │
├─────────────┤ ├──────────────┤
│ (implementa │ │ storeFile()  │
│  todo)      │ │ getFile()    │
└─────────────┘ │ createServer()  ⌀  ← Not implemented
                │ listServers()   ⌀  ← Not implemented
                │ getCDNAddress() ⌀  ← Not implemented
                └──────────────┘
```

Dropbox (almacenamiento puro) se ve **obligado** a implementar métodos de servidores y CDN que no usa → quedan vacíos o lanzando excepciones.

**DESPUÉS — interfaces segregadas:**

```
┌──────────────────────┐ ┌──────────────────┐ ┌──────────────────────┐
│ «interface»          │ │ «interface»      │ │ «interface»          │
│ CloudHostingProvider │ │  CDNProvider     │ │ CloudStorageProvider │
├──────────────────────┤ ├──────────────────┤ ├──────────────────────┤
│ + createServer(reg)  │ │ + getCDNAddress()│ │ + storeFile(name)    │
│ + listServers(reg)   │ └────────▲─────────┘ │ + getFile(name)      │
└────────▲─────────────┘          │           └──────────▲───────────┘
         └──────────────┬─────────┴──────────────────────┘
                        │ implementa las 3
                ┌───────┴────────┐              ┌────────┴─────────┐
                │    Amazon      │              │     Dropbox      │
                ├────────────────┤              ├──────────────────┤
                │ storeFile()    │              │ storeFile(name)  │
                │ getFile()      │              │ getFile(name)    │
                │ createServer() │              └──────────────────┘
                │ listServers()  │   ← solo implementa CloudStorageProvider
                │ getCDNAddress()│
                └────────────────┘
```

### 6.3 Ventajas y recomendaciones de diseño (diapositiva 27)

**Ventajas:**
- ✔ **Evita clases sobrecargadas**: no se obliga a una clase a implementar métodos que no necesita.
- ✔ **Mejora la flexibilidad**: se pueden cambiar o agregar funcionalidades sin afectar clases que no las usan.
- ✔ **Facilita la mantenibilidad**: el código es más claro y fácil de entender.

**🛠 ¿Qué se debe tener en cuenta al diseñar para cumplir ISP?**
1. **Evitar interfaces muy generales**: si una interfaz tiene demasiados métodos, puede estar mal diseñada.
2. **Agrupar métodos relacionados**: crear interfaces específicas con métodos afines.
3. **Aplicar el principio "Solo lo que necesito"**: cada clase debe implementar solo las interfaces que usa.
4. **Usar interfaces pequeñas y reutilizables**: esto mejora la flexibilidad y el mantenimiento.

---

## 7. D — Principio de Inversión de Dependencia (DIP) e Inyección de Dependencias

### 7.1 Definición (diapositiva 28)

> **"Las clases de alto nivel (tienen implementada la lógica del negocio) no deberían depender de las clases de bajo nivel (tienen implementaciones específicas). Ambas deberían depender de abstracciones."**
>
> **"Las abstracciones no deberían depender de los detalles. Los detalles deberían depender de abstracciones (interfaces o clases abstractas)."**

- Cuando se cumple lo anterior, se puede decir que hay una **inversión de dependencia**.
- Este principio promueve un código más flexible mediante:
  - **El desacoplamiento entre módulos (clases)**.
  - **El uso de interfaces o clases abstractas para definir dependencias**.

### 7.2 Inyección de Dependencias en el Constructor (diapositivas 29–30)

> Es la forma **más común** de Inversión de Dependencia (DI).

- **Patrón de diseño** en el que las dependencias de una clase se **proporcionan desde el exterior** en lugar de ser creadas dentro de la clase.
- Consiste en proporcionar las dependencias **a través del constructor** de la clase.

**Ventaja principal**: **reducción del acoplamiento** → la clase no crea sus dependencias directamente, sino que las recibe en su constructor.

**Código completo de la diapositiva 30:**

```csharp
// La abstracción (interfaz)
public interface IMotor
{
    void Encender();
}

// Implementaciones de bajo nivel
public class MotorCombustion : IMotor
{
    public void Encender()
    {
        Console.WriteLine("Motor de combustión encendido.");
    }
}

public class MotorElectrico : IMotor
{
    public void Encender()
    {
        Console.WriteLine("Motor eléctrico encendido.");
    }
}

// Clase de alto nivel: recibe la dependencia por el constructor ← INYECCIÓN
public class Automovil
{
    private readonly IMotor _motor;

    public Automovil(IMotor motor)     // ← aquí ocurre la inyección
    {
        _motor = motor;
    }

    public void Arrancar()
    {
        _motor.Encender();
    }
}

// Uso
IMotor motorCombustion = new MotorCombustion();
Automovil auto1 = new Automovil(motorCombustion);
auto1.Arrancar(); // Salida: Motor de combustión encendido.

IMotor motorElectrico = new MotorElectrico();
Automovil auto2 = new Automovil(motorElectrico);
auto2.Arrancar(); // Salida: Motor eléctrico encendido.
```

### 7.3 Recomendaciones para aplicar DIP (diapositiva 31)

1. Es necesario **describir las interfaces para las operaciones de bajo nivel** en las que se basan las clases de alto nivel, preferiblemente **en términos comerciales** (del negocio).
2. Diseñar de manera que **las clases de alto nivel dependan de esas interfaces**, en lugar de clases concretas de bajo nivel.
3. Una vez que las clases de bajo nivel implementan estas interfaces, se vuelven dependientes del nivel de lógica empresarial, lo que **invierte la dirección de la dependencia original**.

> 📌 **DIP va de la mano con el principio Open/Closed.**

### 7.4 Ejemplo UML: BudgetReport y la base de datos (diapositiva 32)

**ANTES:**

```
   High level                Low level
┌───────────────┐          ┌────────────────┐
│ BudgetReport  │─────────▶│ MySQLDatabase  │
├───────────────┤          ├────────────────┤
│ - database    │          │ + insert()     │
├───────────────┤          │ + update()     │
│ + open(date)  │          │ + delete()     │
│ + save()      │          └────────────────┘
└───────────────┘
```

- La clase `BudgetReport` utiliza la clase `MySQLDatabase` de bajo nivel para leer y conservar sus datos.
- **Un cambio en `MySQLDatabase` podría alterar el funcionamiento de `BudgetReport`.**

**DESPUÉS — se crea una interfaz de alto nivel que describe las operaciones de lectura/escritura:**

```
        High level                        Abstraction
┌───────────────┐                  ┌────────────────────┐
│ BudgetReport  │─────────────────▶│   «interface»      │
├───────────────┤                  │     Database       │
│ - database    │                  ├────────────────────┤
├───────────────┤                  │ + insert()         │
│ + open(date)  │                  │ + update()         │
│ + save()      │                  │ + delete()         │
└───────────────┘                  └─────────▲──────────┘
                                    ┌────────┴────────┐
                              ┌─────┴─────┐     ┌─────┴─────┐
                              │   MySQL   │     │  MongoDB  │   ← Low level
                              ├───────────┤     ├───────────┤
                              │ + insert()│     │ + insert()│
                              │ + update()│     │ + update()│
                              │ + delete()│     │ + delete()│
                              └───────────┘     └───────────┘
```

- La clase `BudgetReport` utiliza la interfaz, en lugar de conectarse con las clases de bajo nivel.
- La clase de bajo nivel original se puede **cambiar o ampliar implementando la nueva interfaz** de lectura/escritura declarada por la lógica de negocio.

### 7.5 Ejemplo de código: Automovil acoplado al Motor (diapositiva 33)

**🚨 VIOLA DIP:**

```csharp
public class Automovil          // Clase de alto nivel
{
    private Motor _motor;

    public Automovil()
    {
        _motor = new Motor();   // ❌ dependencia directa (new interno)
    }

    public void Arrancar()
    {
        _motor.Encender();
    }
}

public class Motor              // Clase de bajo nivel
{
    public void Encender()
    {
        Console.WriteLine("Motor encendido.");
    }
}
```

```
┌────────────┐
│ Automovil  │
└─────┬──────┘
      │  VIOLA DIP: dependencia directa de una implementación concreta
      ▼
┌────────────┐
│   Motor    │
└────────────┘
```

**Problemas:**
- La clase `Automovil` está **directamente acoplada** a la implementación concreta de `Motor`.
- Si se quiere cambiar `Motor` por `MotorElectrico`, se tendría que **modificar `Automovil`**, lo que rompe el principio de abierto/cerrado (OCP).
- **No se pueden realizar pruebas unitarias** de `Automovil` fácilmente porque depende de una implementación específica de `Motor`.

**CORRECCIÓN (diapositiva 34):**

```
┌────────────┐        ┌──────────────────┐
│ Automovil  │───────▶│   «interface»    │
└────────────┘        │      IMotor      │
                      └────────▲─────────┘
                               │ Implementa
                        ┌──────┴───────┐
                        │MotorCombustion│
                        └──────────────┘
```

```csharp
public interface IMotor
{
    void Encender();
}

public class MotorCombustion : IMotor
{
    public void Encender()
    {
        Console.WriteLine("Motor de combustión encendido.");
    }
}

public class Automovil
{
    private readonly IMotor _motor;

    public Automovil(IMotor motor)   // ← Se inyecta la dependencia en el constructor
    {
        _motor = motor;
    }

    public void Arrancar()
    {
        _motor.Encender();
    }
}
```

**✅ Beneficios:**
- **Desacoplamiento**: `Automovil` no depende de ninguna implementación concreta de `IMotor`.
- **Facilidad de cambio**: se puede cambiar `MotorCombustion` por `MotorElectrico` **sin modificar `Automovil`**.

### 7.6 🔥 Errores comunes al violar DIP (diapositiva 35)

1. **Acoplamiento fuerte entre clases**: cuando una clase de alto nivel crea instancias de clases de bajo nivel (`new` directamente dentro de la clase).
2. **Uso de implementaciones en lugar de abstracciones**: si un método espera un `MotorCombustion` en lugar de un `IMotor`, el código no es extensible.
3. **Abuso de contenedores de inyección de dependencia**: aunque la inyección de dependencias es útil, si se abusa creando demasiadas interfaces innecesarias, el código se vuelve complejo sin beneficio real.

---

## 8. Caso de Estudio 1: Proyecto del Concesionario

*(Diapositivas 36–37)* — Aplicación de los 5 principios a un proyecto real del curso:

1. **SRP (Responsabilidad Única)**: se separaron las validaciones en la clase `Validaciones`. Su única responsabilidad es hacer las validaciones.
2. **OCP (Abierto/Cerrado)**: `Automovil` es más extensible sin modificarla directamente. Si cambia la validación de algún atributo, ya no se tiene que cambiar la implementación dentro de `Automovil`.
3. **LSP (Sustitución de Liskov)**: se revisaron herencias para evitar conflictos en el comportamiento.
4. **ISP (Segregación de Interfaces)**: se mantuvo `IMantenimiento`, pero podría segmentarse aún más si es necesario.
5. **DIP (Inversión de Dependencias)**: `Automovil` accedía a atributos definidos en `Concesionario`, como `valor_minimo_nuevo`, lo que generaba acoplamiento innecesario. Para romper el acoplamiento entre `Automovil` y `Validacion`: se diseña una arquitectura donde `Automovil` **no dependa directamente de `Validaciones`**, sino que **reciba una instancia de un servicio de validación a través de inyección de dependencias (DIP)**.

---

## 9. Caso de Estudio 2: Sistema de Tránsito (análisis clase a clase)

*(Diapositivas 38–48)* — Análisis estructural de un sistema de multas de tránsito, revisando qué principios cumple y cuáles no cada clase, y qué hacer al respecto.

### 9.1 Estructura general (diapositivas 38 y 40 — diagramas UML)

El sistema incluye:

- **`Transito`**: `direccion: string`, `telefono: ulong`, `l_multas: Multa`, `Transito(telefono, direccion)`, `Totalizar_multas(): ulong`. Relación 1 a muchos: *impone* multas. Depende de `Interceptor_Envio_Mensajes`.
- **`Multa`** (base abstracta): `conductor: Conductor`, `vehiculo: Vehiculo`, `valor: ulong`, `fecha_hora: DateTime`, `nro: ulong`, `Calcular_sancion(): ulong`. Implementa `ISancionEconomica`.
- **`Menor : Multa`**: `l_infracciones_menores` (enum), `infraccion_menor`, `Restar_puntos(): uint`, `Calcular_sancion(): ulong`. Implementa `IRestaPuntos`.
- **`Mayor : Multa`**: `l_inf_mayores` (enum), `infraccion_mayor`, `pub_anu_lic: Publ_Anul_Lic`, `Anular_Licencia(): string`, `Asignar_Trabajo_Social(): string`, `Calcular_Sancion(): ulong`. Implementa `IAnulaLicencia` (`Anular_Licencia(): string`) e `ITrabajoSocial` (`Asignar_Trabajo_Social(): string`).
- **`Publ_Anul_Lic`**: delegado y evento `dele_anul_lic` + `Informar_Anula_Lic(conductor): string` (publicador de evento de anulación de licencia).
- **Mensajería**: interfaz `IMensajeria` con `EnviarMensajeMulta(conductor: Conductor)`; clase abstracta `Medio`; implementaciones `Email`, `WhatsApp`, `Sms` ("Es un" Medio). `MensajeFactory` con `CrearEmail()`, `CrearSMS()`, `CrearWhatsApp()`, `CrearMensajePorTipo(tipo): IMensajeria` (¡ya usa una fábrica!).
- **`Interceptor_Envio_Mensajes`**: implementa `IInterceptor` (de CastleProxy) e implementa el patrón de inyección con `IHttpContextAccessor` (interfaz de `Microsoft.AspNetCore.Http`). Depende de `IMensajeria` y `MensajeFactory`.
- **`Vehiculo`**: `placa: string`, `marca: string`, `modelo: string`, `ano: ushort` + constructor.
- **`Conductor`**: `tipo_id: l_tipos_id`, `id: string`, `nombre: string`, `edad: byte`, `telefono: ulong`, `correo: string`, `estado_lic: l_estados_lic`, `total_ptos: uint` + constructor.
- **`ReglaNegocio`**: constantes del negocio.

### 9.2 Clase Transito (diapositiva 39)

**✅ Principios cumplidos:**
- **SRP**: maneja la gestión de multas y datos de tránsito. Validaciones correctas en propiedades.

**🚫 Principios NO cumplidos:**
- **SRP: violación parcial** — tiene dos responsabilidades:
  1. Gestión de datos de entidad tránsito.
  2. Cálculo de totalización de multas.
- **DIP (Dependency Inversion)**: depende directamente de clases concretas (`Mayor`, `Menor`).
- **ISP (Interface Segregation)**: no utiliza interfaces para el cálculo.

**👷 ¿Qué hacer?**
- Extraer la lógica de totalización a un servicio `CalculadoraMultas`.
- Usar interfaces en lugar de tipos concretos en `Totalizar_multas()`.
- **Inyectar dependencias** en lugar de crearlas internamente.

### 9.3 Clase Multa (diapositiva 41)

**✅ Principios cumplidos:**
- **SRP**: bien definida como clase base abstracta.
- **OCP**: diseñada para extensión a través de herencia.
- **LSP**: estructurada para que `Mayor` y `Menor` sean sustituibles.

**🚫 Principios NO cumplidos:**
- **ISP**: implementa `ISancionEconomica` directamente, pero no todas las multas podrían necesitar esta interfaz en el futuro.

**👷 ¿Qué hacer?**
- Considerar rediseño por **composición en lugar de herencia** para mayor flexibilidad.
- Separar responsabilidades de valor y cálculo.

### 9.4 Clase Menor (diapositiva 42)

**✅ Principios cumplidos:**
- **LSP**: correctamente sustituible por `Multa`.
- **SRP**: maneja solo infracciones menores.

**🚫 Principios NO cumplidos:**
- **DIP**: depende directamente de `Transito.val_sal_minimo`.
- **Valores hardcodeados**: `cant_sal_min = 3` debería venir de configuración.

**👷 ¿Qué hacer?**
- Inyectar valores de configuración en el constructor.
- Extraer el cálculo a una **estrategia** para cambiar fórmulas fácilmente.

### 9.5 Clase Mayor (diapositiva 43)

**✅ Principios cumplidos:** *(ninguno destacado)*

**🚫 Principios NO cumplidos:**
- **SRP violado** — tiene múltiples responsabilidades:
  - Cálculo de sanción.
  - Asignación de trabajo social.
  - Anulación de licencia.
  - Manejo de eventos.
- **OCP**: difícil de extender con nuevas funcionalidades.
- **DIP**: maneja directamente eventos y lógica de negocio.
- **Control de flujo confuso**: lanza excepciones como mecanismo de notificación.

**👷 ¿Qué hacer?**
- Separar en múltiples servicios:
  - `ServicioCalculoMayor`
  - `ServicioTrabajoSocial`
  - `ServicioLicencias`
- Usar **patrón Observer** para eventos en lugar de excepciones.
- Inyectar dependencias para cada responsabilidad.

### 9.6 Clase Vehiculo (diapositiva 45)

**✅ Principios cumplidos:**
- **SRP**: maneja solo la lógica de un vehículo.
- **Cumplimiento de contratos**: validaciones consistentes en propiedades.

**🚫 Principios NO cumplidos:**
- **OCP**: no está diseñada para extensión. Si se necesitan nuevos tipos de vehículos, habría que modificar la clase.

**👷 ¿Qué hacer?**
- Crear interfaz `IVehiculo` para permitir diferentes implementaciones.
- Mover constantes de año a configuración (`appsettings` o clase de constantes).
- Considerar usar **Value Objects** para propiedades como `Placa`.

### 9.7 Clase Conductor (diapositiva 47)

**✅ Principios cumplidos:**
- **SRP**: maneja solo datos y validaciones del conductor.
- **Encapsulamiento**: correcto con validaciones en propiedades.

**🚫 Principios NO cumplidos:**
- **OCP**: si necesitamos nuevos tipos de identificación o estados, hay que modificar enums.
- **Responsabilidad de generación**: genera su propio número de licencia.

**👷 ¿Qué hacer?**
- Extraer la generación de licencia a un servicio `GeneradorLicencias`.
- Considerar usar *pattern matching* para validaciones complejas (no es mejora SOLID).
- Mover enums a archivos separados si crecen (no es mejora SOLID).

### 9.8 Clase ReglaNegocio (diapositiva 48)

**✅ Principios cumplidos:**
- **SRP**: solo contiene constantes de negocio.

**⚠️ Alertas:**
- **OCP**: está cerrada para modificación... ¡pero también para extensión!
- **DIP**: depende de valores fijos.

**👷 ¿Qué hacer?**
- Convertir en interfaz para permitir diferentes configuraciones.
- Mover a configuración externa (JSON, base de datos).
- Agrupar constantes relacionadas en clases específicas.

---

## 10. Introducción a los Patrones de Diseño

Las diapositivas de patrones cubren los **5 patrones creacionales** clásicos:

| Patrón | Responde a la pregunta |
|---|---|
| **Factory Method** | ¿Quién decide exactamente qué clase instanciar? → una subclase "fábrica" |
| **Abstract Factory** | ¿Cómo crear **familias completas** de objetos compatibles entre sí? |
| **Builder** | ¿Cómo construir un objeto complejo **paso a paso**? |
| **Prototype** | ¿Cómo crear objetos **clonando** uno existente? |
| **Singleton** | ¿Cómo garantizar **una única instancia** global? |

Todos comparten el mismo espíritu que SOLID: **quitar al código cliente la responsabilidad de crear objetos concretos con `new`**, para desacoplar la creación del uso.

---

## 11. Patrón Factory Method (Método de Fábrica)

### 11.1 Definición (diapositiva 2)

> **"El patrón Factory Method es un patrón creacional que proporciona una interfaz para crear objetos en una superclase, pero permite a las subclases alterar el tipo de objetos que se crearán."**

### 11.2 El problema (diapositiva 3)

**Situación**: se tiene una app para la gestión logística que solo maneja **transporte en camiones**; todo el código de la aplicación está acoplado a estas clases.

- ¿Si nos piden incluir **transporte en barcos**? → Sería un cambio mayor para todo el código.
- ¿Y si después piden incluir otro medio de transporte? Por ejemplo, **aéreo** → Otro cambio mayor...

Cada nuevo tipo de transporte obliga a modificar código que ya funcionaba (¡rompe OCP!).

### 11.3 La solución que sugiere el patrón (diapositiva 4)

> Reemplazar las **llamadas directas de construcción de objetos** (usando el operador `new`) con llamadas a un **método de fábrica especial**.

- Los objetos **se siguen creando** a través del operador `new`, pero se le llama **desde dentro del método de fábrica**.
- Los objetos devueltos por un método de fábrica se suelen denominar **"productos"**.
- Se podría **sobrescribir el método de fábrica en una subclase** y cambiar la clase de productos que se crean mediante el método.

### 11.4 Estructura (diapositivas 5–6)

```
                 ┌─────────────────────────┐         ┌──────────────────────┐
                 │        Creator          │         │    «interface»       │
                 ├─────────────────────────┤         │       Product        │  (1)
                 │ + someOperation()       │────────▶├──────────────────────┤
                 │ + createProduct():Prod. │         │ + doStuff()          │
                 └───────────▲─────────────┘         └──────────▲───────────┘
            ┌────────────────┴───────────────┐         ┌────────┴────────┐
   ┌────────┴─────────┐          ┌───────────┴──────┐  │                 │
   │ ConcreteCreatorA │          │ ConcreteCreatorB │  │                 │
   ├──────────────────┤          ├──────────────────┤  │                 │
   │+ createProduct() │          │+ createProduct() │  │                 │
   │ return new       │          │ return new       │  │                 │
   │   ConcreteProd.A │          │   ConcreteProd.B │  │                 │
   └──────────────────┘          └──────────────────┘  │                 │
          (4)                          (4)       ┌─────┴──────┐  ┌───────┴─────┐
                                                 │ Concrete   │  │ Concrete    │
                                                 │ ProductA   │  │ ProductB    │  (2)
                                                 └────────────┘  └─────────────┘
```

1. **La interfaz `Product`** declara un producto, que es común a todos los objetos que pueden ser producidos por el `Creator` y sus subclases.
2. **Las clases concretas** (`ConcreteProductA/B`) son diferentes implementaciones de la interfaz del producto.
3. **La clase `Creator`** declara el método de fábrica `createProduct()` que devuelve nuevos objetos de producto. **El tipo de retorno de este método debe coincidir con la interfaz del producto.**
4. **Las implementaciones concretas** (`ConcreteCreatorA/B`) anulan el método de fábrica base para que devuelva un tipo de producto diferente.

**Notas de la diapositiva:**
- **N1**: puede declarar el método de fábrica como **abstracto** para obligar a todas las subclases a implementar sus propias versiones del método.
- **N2**: la creación del producto **no es la responsabilidad principal** de `Creator` (normalmente tiene lógica de negocio propia, como `someOperation()`).
- **N3**: el método de fábrica ayuda a **desacoplar** las clases de producto concretas.

**Estructura aplicada a automóviles (diapositiva 6):**

```
┌────────────────────────┐ "Depende" ┌──────────────────┐
│   AutomovilFactory     │──────────▶│   IAutomovil     │
├────────────────────────┤           ├──────────────────┤
│ + CrearAutomovil():    │           │ MostrarDetalles()│
│   IAutomovil           │           └────────▲─────────┘
└───────────▲────────────┘      "Implementa" │
      "Es una"│                    ┌─────────┼──────────┐
   ┌──────────┼──────────┐         │         │          │
┌──┴───────────┐ ┌───────┴────┐ ┌──┴───┐ ┌───┴──┐ ┌─────┴────┐
│CamionetaFact.│ │ TaxiFact.* │ │Camio.│ │ Taxi │ │Deportivo │
│+CrearAutomov.│ │+CrearAutom.│ │neta  │ │      │ │          │
└──────────────┘ └────────────┘ └──────┘ └──────┘ └──────────┘
```
*(Nota: en la diapositiva las tres fábricas concretas aparecen rotuladas como "CamionetaFactory"; corresponden a una fábrica por cada producto: Camioneta, Taxi y Deportivo.)*

### 11.5 Ventajas de Factory Method (diapositiva 7)

1. **Desacoplamiento**: elimina la dependencia directa entre el código cliente y las clases concretas. El cliente usa interfaces o clases abstractas sin preocuparse por cómo se crean los objetos reales.
2. **Facilita la extensión**: agregar nuevos tipos de productos (como un nuevo tipo de automóvil) es fácil. Solo necesitas crear una nueva fábrica **sin modificar el código existente**, siguiendo el principio **Open/Closed**.
3. **Reutilización de código**: permite reutilizar la lógica de creación en varias partes de la aplicación. Cada subclase de fábrica encapsula cómo crear un tipo específico de objeto.
4. **Centraliza la creación de objetos**: la lógica de creación no está dispersa en el código. Esto facilita cambios futuros, como modificar el proceso de creación en un solo lugar.
5. **Promueve la cohesión**: cada clase tiene una única responsabilidad — las fábricas crean objetos, los objetos realizan sus propias funciones. Diseño más limpio y mantenible.
6. **Soporte para productos complejos**: si la creación de un objeto es compleja o tiene múltiples pasos, el Factory Method ayuda a encapsular esos detalles.
7. **Mejora las pruebas unitarias**: al trabajar con interfaces, puedes simular objetos más fácilmente durante las pruebas.

### 11.6 Desventajas de Factory Method (diapositiva 8)

- Puede **añadir complejidad** si no se necesita tanta flexibilidad. El código puede volverse más complicado ya que es necesario **introducir muchas subclases nuevas** para implementar el patrón.
- Involucra **más clases y abstracciones**, lo que puede ser excesivo en proyectos muy simples.

### 11.7 ¿Cómo lo usa un arquitecto de software profesionalmente? (diapositiva 9)

Un arquitecto de software, al diseñar la estructura de un sistema, define los **puntos de extensión** y las **interfaces** que otros desarrolladores implementarán. Usa el Factory Method para:

- **Establecer un punto central de creación de objetos**, evitando que múltiples partes del sistema creen instancias de clases directamente.
- **Asegurar flexibilidad** al permitir que el sistema evolucione sin modificar grandes cantidades de código.
- **Facilitar la prueba y el mantenimiento** al crear objetos a través de interfaces bien definidas.

### 11.8 ¿Cuándo usarlo? (diapositiva 10)

1. El método Factory **separa el código de construcción del producto del código que realmente utiliza el producto**. Por lo tanto, es más fácil extender el código de construcción del producto independientemente del resto del código.
2. **Cuando no conozcas de antemano los tipos y dependencias exactos** de los objetos con los que debería trabajar tu código. Por ejemplo, para agregar un nuevo tipo de producto a la aplicación, solo necesitarás crear una nueva subclase de creador y anular el método Factory en ella.
3. **Cuando desees proporcionar a los usuarios de tu biblioteca o framework una forma de extender sus componentes internos.** La solución es reducir el código que construye componentes en todo el framework a un único método de fábrica y permitir que cualquiera reemplace este método, además de extender el componente en sí.
4. **Cuando desees ahorrar recursos del sistema reutilizando objetos existentes** en lugar de reconstruirlos cada vez. Esta necesidad es frecuente cuando se trabaja con objetos grandes que consumen muchos recursos, como **conexiones de bases de datos, sistemas de archivos y recursos de red**.

### 11.9 ¿Cómo funciona en un equipo de desarrollo? (diapositiva 11)

En un equipo, el arquitecto puede decir:

> *"Todas las clases de vehículos deben crearse usando un método fábrica. No creen objetos con `new` directamente."*

Los desarrolladores, al seguir esta guía, implementan clases concretas **sin modificar el núcleo del sistema**. Así, si mañana el cliente pide un nuevo tipo de automóvil (como un eléctrico), solo crean **una nueva fábrica** sin tocar el resto del código.

### 11.10 Preguntas que un equipo resuelve con Factory Method (diapositiva 12)

| Pregunta | Respuesta |
|---|---|
| *"¿Qué pasa si necesitamos agregar un nuevo tipo de producto sin afectar el código existente?"* | Usamos Factory Method para agregar nuevos productos sin modificar el núcleo del sistema. |
| *"¿Cómo podemos asegurarnos de que todos los objetos se crean de manera consistente?"* | Implementamos un método fábrica central para manejar la creación de objetos. |
| *"¿Qué hacemos si diferentes partes del sistema necesitan crear objetos, pero con ligeras diferencias?"* | Usamos subclases de fábrica para que cada una cree su versión específica del objeto. |
| *"¿Cómo evitamos duplicar código de creación de objetos en diferentes módulos?"* | Encapsulamos la lógica de creación en un Factory Method reutilizable. |

### 11.11 Ejemplos en la vida real del desarrollo de software (diapositiva 13)

- **Aplicaciones móviles**: cuando un equipo desarrolla una app multiplataforma (iOS y Android), un arquitecto puede definir un Factory Method para crear **botones, menús o notificaciones según la plataforma**.
- **Juegos**: crear diferentes tipos de **enemigos, personajes o armas** mediante un Factory Method que facilita agregar nuevos elementos al juego sin modificar el código base.
- **Sistemas financieros**: un arquitecto podría definir un Factory Method para crear **objetos de transacciones que se adapten a diferentes bancos o monedas**, permitiendo agregar nuevas entidades financieras sin cambiar el sistema central.

> **Beneficio clave para un equipo**: un arquitecto de software sabe que **los requerimientos siempre cambian**. Con el Factory Method, el equipo está preparado para el futuro sin rehacer el sistema entero. Se logra un desarrollo más **ágil, flexible y mantenible**.

### 11.12 Ejemplo de código de referencia (C#)

```csharp
// (1) Producto (interfaz)
public interface IAutomovil
{
    void MostrarDetalles();
}

// (2) Productos concretos
public class Camioneta : IAutomovil
{
    public void MostrarDetalles() => Console.WriteLine("Soy una camioneta");
}
public class Taxi : IAutomovil
{
    public void MostrarDetalles() => Console.WriteLine("Soy un taxi");
}
public class Deportivo : IAutomovil
{
    public void MostrarDetalles() => Console.WriteLine("Soy un deportivo");
}

// (3) Creator (clase abstracta con el método de fábrica)
public abstract class AutomovilFactory
{
    public abstract IAutomovil CrearAutomovil();   // Factory Method

    // N2: el Creator puede tener lógica de negocio adicional
    public void EntregarVehiculo()
    {
        IAutomovil auto = CrearAutomovil();
        auto.MostrarDetalles();
        Console.WriteLine("Vehículo entregado al cliente.");
    }
}

// (4) Creators concretos: cada uno decide qué producto crear
public class CamionetaFactory : AutomovilFactory
{
    public override IAutomovil CrearAutomovil() => new Camioneta();
}
public class TaxiFactory : AutomovilFactory
{
    public override IAutomovil CrearAutomovil() => new Taxi();
}
public class DeportivoFactory : AutomovilFactory
{
    public override IAutomovil CrearAutomovil() => new Deportivo();
}

// Cliente
AutomovilFactory fabrica = new DeportivoFactory();
fabrica.EntregarVehiculo();   // El cliente nunca hace "new Deportivo()"
```

---

## 12. Patrón Abstract Factory (Fábrica Abstracta)

### 12.1 Definición (diapositiva 14)

> **"Abstract Factory es un patrón creacional que proporciona una interfaz para producir familias de objetos relacionados sin especificar sus clases concretas."**

- Es ideal cuando un sistema debe ser **independiente de cómo se crean, componen y representan sus productos**.

### 12.2 La idea intuitiva: armar un carro (diapositiva 15)

> **Armar un carro = Ensamblar la Carrocería + Instalar el Motor**

- Carrocería Eléctrico + Motor Diésel = 👎 **Los objetos NO se relacionan** (¡combinación incompatible!)
- Carrocería Eléctrico + Motor Eléctrico = 👍 **Los objetos se relacionan** (familia coherente)

**Lección clave**: cuando los productos vienen en *familias* que deben ser compatibles entre sí, no basta con crear cada objeto por separado — hay que garantizar que **toda la familia sea de la misma variante**.

### 12.3 Relación con Factory Method (diapositiva 16)

- El patrón de fábrica abstracta es **casi igual que el patrón de fábrica** y se considera como **otra capa de abstracción sobre el patrón de fábrica**.
- Los patrones abstractos de fábrica funcionan alrededor de una **superfábrica que crea otras fábricas**.
- **En tiempo de ejecución**, la fábrica abstracta se acopla con cualquier fábrica concreta deseada que pueda crear objetos del tipo deseado.

### 12.4 Estructura (diapositiva 17)

```
 ┌──────────────────────────────┐        ┌────────────────────────────┐
 │   ConcreteFactory1           │        │          Client            │
 │ + createProductA(): ProductA │        ├────────────────────────────┤
 │ + createProductB(): ProductB │        │ - factory: AbstractFactory │
 └───────────────▲──────────────┘        │ + Client(f: AbstractFact.) │
                 │ implementa             │ + someOperation()          │
 ┌───────────────┴──────────────┐        └──────────────┬─────────────┘
 │   «interface»                │◀──────────────────────┘
 │    AbstractFactory           │   ProductA pa = factory.createProductA()
 │ + createProductA(): ProductA │
 │ + createProductB(): ProductB │
 └───────────────▲──────────────┘
                 │ implementa
 ┌───────────────┴──────────────┐
 │   ConcreteFactory2           │
 │ + createProductA(): ProductA │ → return new ConcreteProductA2()
 │ + createProductB(): ProductB │
 └──────────────────────────────┘

 Productos:   AbstractProductA  ◀── ConcreteProductA1, ConcreteProductA2
              AbstractProductB  ◀── ConcreteProductB1, ConcreteProductB2
```

**Participantes:**
- **Productos Abstractos**: definen las interfaces para los productos.
- **Productos Concretos**: implementaciones concretas.
- **Fábrica Abstracta**: define los métodos para crear productos.
- **Fábricas Concretas**: crean productos **de una misma familia**.
- **Cliente**: utiliza la fábrica para un producto **sin saber qué tipo específico está creando**.

### 12.5 Implementación del curso: fábrica de autos (diapositiva 18)

```
                        ┌───────────────────────────┐
                        │   FabricaAutosElectricos  │
                        │ + CrearCarroceria():      │──Depende de──▶ CarroceriaElectrico (+Ensamblar())
                        │   ICarroceria             │──Depende de──▶ MotorElectrico (+Instalar())
                        │ + CrearMotor(): IMotor    │
                        └─────────────▲─────────────┘
                                      │ Implementa
┌──────────────┐  ┌──────────┐  ┌─────┴─────────────┐     ┌──────────────────────────────┐
│ ICarroceria  │  │  IMotor  │  │  IFabricaAutos    │◀────│     ClienteFabricaAutos      │
│ Ensamblar()  │  │Instalar()│  │ CrearCarroceria() │Utiliza│ - carroceria: ICarroceria    │
└──────▲───────┘  └────▲─────┘  │ CrearMotor()      │     │ - motor: IMotor              │
       │Implementa     │Implem. └─────────▲─────────┘     │ + ClienteFabricaAutos(       │
┌──────┴───────┐ ┌─────┴──────┐           │ Implementa    │   fabrica: IAutoFabrica)     │
│Carroceria    │ │ Motor      │  ┌────────┴─────────────┐ │ + ArmarAuto(): void          │
│  Gasolina    │ │  Gasolina  │◀─│ FabricaAutosGasolina │ │ {carroceria: readonly}       │
│+Ensamblar()  │ │ +Instalar()│  │ + CrearCarroceria()  │ │ {motor: readonly}            │
└──────────────┘ └────────────┘  │ + CrearMotor()       │ └──────────────────────────────┘
                                 └──────────────────────┘
```

- **Familia "Gasolina"**: `FabricaAutosGasolina` crea `CarroceriaGasolina` + `MotorGasolina`.
- **Familia "Eléctricos"**: `FabricaAutosElectricos` crea `CarroceriaElectrico` + `MotorElectrico`.
- El **cliente** recibe una `IFabricaAutos` (readonly) y arma el auto **sin saber** qué familia concreta está usando → imposible mezclar carrocería eléctrica con motor diésel.

**Código de referencia:**

```csharp
// Productos abstractos
public interface ICarroceria { void Ensamblar(); }
public interface IMotor      { void Instalar(); }

// Productos concretos — familia gasolina
public class CarroceriaGasolina : ICarroceria
{
    public void Ensamblar() => Console.WriteLine("Ensamblando carrocería de gasolina");
}
public class MotorGasolina : IMotor
{
    public void Instalar() => Console.WriteLine("Instalando motor de gasolina");
}

// Productos concretos — familia eléctrica
public class CarroceriaElectrico : ICarroceria
{
    public void Ensamblar() => Console.WriteLine("Ensamblando carrocería eléctrica");
}
public class MotorElectrico : IMotor
{
    public void Instalar() => Console.WriteLine("Instalando motor eléctrico");
}

// Fábrica abstracta
public interface IFabricaAutos
{
    ICarroceria CrearCarroceria();
    IMotor CrearMotor();
}

// Fábricas concretas (una por familia)
public class FabricaAutosGasolina : IFabricaAutos
{
    public ICarroceria CrearCarroceria() => new CarroceriaGasolina();
    public IMotor CrearMotor()           => new MotorGasolina();
}
public class FabricaAutosElectricos : IFabricaAutos
{
    public ICarroceria CrearCarroceria() => new CarroceriaElectrico();
    public IMotor CrearMotor()           => new MotorElectrico();
}

// Cliente
public class ClienteFabricaAutos
{
    private readonly ICarroceria carroceria;
    private readonly IMotor motor;

    public ClienteFabricaAutos(IFabricaAutos fabrica)
    {
        carroceria = fabrica.CrearCarroceria();
        motor = fabrica.CrearMotor();
    }

    public void ArmarAuto()
    {
        carroceria.Ensamblar();
        motor.Instalar();
    }
}

// Uso: la familia completa se decide en UN solo punto
var cliente = new ClienteFabricaAutos(new FabricaAutosElectricos());
cliente.ArmarAuto();   // Garantizado: carrocería eléctrica + motor eléctrico
```

### 12.6 Beneficios (diapositiva 19)

- El patrón Abstract Factory **separa la creación de objetos**, por lo que los clientes no necesitan conocer clases específicas.
- Los clientes interactúan con los objetos a través de **interfaces abstractas**, manteniendo los nombres de clase **ocultos** del código del cliente.
- **Cambiar la fábrica permite diferentes configuraciones de producto**, ya que todos los productos relacionados cambian juntos.
- El patrón **garantiza que una aplicación utilice objetos de una sola familia a la vez** para una mejor compatibilidad.

### 12.7 Cuidados (diapositiva 20)

- Puede **agregar complejidad innecesaria** a proyectos más simples con múltiples fábricas e interfaces.
- **Agregar nuevos tipos de productos** puede requerir cambios **tanto en las fábricas concretas como en la interfaz de la fábrica abstracta**, lo que afecta el código existente. *(Ej.: agregar `CrearCajaCambios()` a `IFabricaAutos` obliga a tocar TODAS las fábricas concretas.)*
- La introducción de más fábricas y familias de productos puede **aumentar rápidamente la cantidad de clases**, dificultando la gestión del código en proyectos más pequeños.
- **Puede violar el principio de inversión de dependencia (DIP)** si el código del cliente depende directamente de fábricas concretas en lugar de interfaces abstractas.

### 12.8 Cuándo utilizarlo (diapositiva 21)

- Cuando el sistema requiere **múltiples familias de productos relacionados** y desea **garantizar la compatibilidad** entre ellos.
- Cuando necesita **flexibilidad y extensibilidad**, permitiendo agregar nuevas variantes de productos sin cambiar el código del cliente existente.
- Cuando desea **encapsular la lógica de creación**, facilitando la modificación o ampliación del proceso de creación de objetos sin afectar al cliente.
- Cuando se pretende **mantener la coherencia** entre diferentes familias de productos, se garantiza una **interfaz uniforme** para los productos.

### 12.9 Cuándo NO utilizarlo (diapositiva 22)

- Cuando es **poco probable que las familias de productos cambien**, ya que esto puede agregar una complejidad innecesaria.
- Cuando su aplicación **solo requiere objetos únicos e independientes** y no se ocupa de familias de productos relacionados.
- Cuando los **costos adicionales** de mantener varias fábricas **superan los beneficios**, particularmente en aplicaciones más pequeñas.
- Cuando existen **soluciones más simples**, como el **Factory Method** o el patrón **Builder**, que satisfacen sus necesidades sin agregar la complejidad del patrón Abstract Factory.

### 12.10 Preguntas que resuelve un arquitecto con Abstract Factory (diapositiva 23)

| Pregunta | Respuesta del patrón |
|---|---|
| ¿Cómo desacoplar la creación de objetos del cliente? | Permite crear objetos sin que el cliente sepa qué implementaciones específicas se están utilizando. |
| ¿Cómo asegurar consistencia en productos relacionados? | Todas las implementaciones de una misma familia son creadas por una sola fábrica. |
| ¿Cómo facilitar el mantenimiento y la expansión? | Agregar una nueva familia de productos (por ejemplo, automóviles híbridos) solo requiere crear nuevas clases y una nueva fábrica, sin modificar el código existente. |
| ¿Cómo apoyar el desarrollo ágil y modular? | Facilita el trabajo en equipos al permitir que diferentes equipos trabajen en diferentes fábricas o productos sin interferir entre sí. |

### 12.11 El arquitecto en el plano profesional (diapositiva 24)

Un arquitecto de software utiliza el patrón Abstract Factory para:

- **Asegurar la consistencia entre productos relacionados**: por ejemplo, garantizar que un sistema de automóviles eléctricos no utilice un motor de combustión interna.
- **Facilitar el mantenimiento y escalabilidad**: permite agregar nuevas variantes de productos (nuevos tipos de automóviles) sin modificar el código existente.
- **Resolver problemas de desacoplamiento**: el arquitecto se asegura de que el sistema no dependa de implementaciones concretas, facilitando los cambios futuros.

---

## 13. Patrón Builder (Constructor)

### 13.1 El problema motivador: la pizzería (diapositiva 25)

> ¿Cómo comenzamos a diseñar la arquitectura de una aplicación para una pizzería?

Opciones que se plantean:
- ¿**Una clase por cada pizza**? (¿`PizzaHawaiana`, `PizzaMexicana`, `PizzaVegetariana`...?)
- ¿**Una clase con una cantidad de constructores sobrecargados**?
- ¿Herencia, polimorfismo?

### 13.2 Definición (diapositiva 26)

> **"Builder Design es un patrón creacional que permite construir objetos complejos paso a paso, donde el proceso de construcción puede cambiar en función del tipo de producto que se esté construyendo."**

- **El proceso de construcción depende del producto.**

### 13.3 Estructura (diapositiva 27)

```
                 ┌──────────────────────────────┐
                 │            Client            │
                 └───────────────┬──────────────┘
        b = new ConcreteBuilder1()              │
        d = new Director(b)                     ▼
        d.make()                    ┌────────────────────────┐
        Product1 p = b.getResult()  │        Director        │ ─ Define la SECUENCIA
                                    ├────────────────────────┤   de construcción de
                                    │ - builder: Builder     │   diferentes tipos de
                                    │ + Director(builder)    │   productos
                                    │ + changeBuilder(build.)│
                                    │ + make(type)           │
                                    └───────────┬────────────┘
                                                │ usa
                 ┌──────────────────────────────▼─────────────┐
                 │              «interface» Builder           │ ─ Define los PASOS
                 ├────────────────────────────────────────────┤   para construir el
                 │ + reset()                                   │   producto
                 │ + buildStepA()                              │
                 │ + buildStepB()                              │
                 │ + buildStepZ()                              │
                 └───────────────┬───────────────▲────────────┘
                                 │ implementa    │
            ┌────────────────────┴───┐  ┌────────┴────────────┐
            │    ConcreteBuilder1    │  │   ConcreteBuilder2  │ ─ Constructor Concreto:
            ├────────────────────────┤  ├─────────────────────┤   clase concreta que
            │ - result: Product1     │  │ - result: Product2  │   implementa la interfaz
            │ + reset()              │  │ + reset()           │
            │ + buildStepA/B/Z()     │  │ + buildStepA/B/Z()  │
            │ + getResult(): Product1│  │ + getResult(): Prod2│
            └───────────┬────────────┘  └──────────┬──────────┘
                        ▼                          ▼
                 ┌────────────┐             ┌────────────┐
                 │  Product1  │             │  Product2  │ ─ Producto: el objeto
                 └────────────┘             └────────────┘   que se está construyendo
```

**Participantes:**
1. **Producto**: representa el objeto que se está construyendo.
2. **Interfaz Builder**: define los pasos para construir el producto.
3. **Constructor Concreto**: es la clase concreta que implementa la interfaz.
4. **Director**: define la **secuencia de construcción** de diferentes tipos de productos.

Lógica del Director (de la diapositiva):

```
builder.reset()
if (type == "simple") {
    builder.buildStepA()
} else {
    builder.buildStepB()
    builder.buildStepZ()
}
```

Y la obtención del resultado: `result = new Product2(); result.setFeatureB(); return this.result`.

### 13.4 Implementación del curso: la pizzería (diapositiva 28)

```
┌────────────────┐   "Usa"    ┌───────────┐
│ IPizzaBuilder  │◀- - - - - -│ Pizzeria  │ ← Director
└───────▲────────┘            └───────────┘
        │ "Implementa"
┌───────┴────────┐
│  PizzaBuilder  │ ← Constructor Concreto
└───────┬────────┘
        │ "Fabrica" (1 pizza; la relación con Calzone está marcada con ✗)
   ┌────┴─────┐
   ▼          ▼
┌───────┐  ┌─────────┐
│ Pizza │  │ Calzone │ ← Productos concretos
└───────┘  └─────────┘
```

Lección de diseño visible en el diagrama: el `PizzaBuilder` fabrica `Pizza`; la asociación directa con `Calzone` está marcada como incorrecta (✗) — para un producto distinto corresponde **otro constructor concreto**, no que un mismo builder fabrique productos no relacionados.

### 13.5 ¿Cuándo se usa el patrón Builder? (diapositiva 29)

1. **Construcción de objetos complejos**: cuando se tiene un objeto con muchos componentes o configuraciones opcionales y deseas proporcionar una separación clara entre el proceso de construcción y la representación real del objeto.
2. **Construcción paso a paso**: cuando la construcción de un objeto implica un proceso paso a paso donde es necesario establecer diferentes configuraciones u opciones en diferentes etapas.
3. **Cómo evitar constructores con múltiples parámetros**: cuando la cantidad de parámetros en un constructor se vuelve demasiado grande y el uso de **constructores telescópicos** (constructores con múltiples parámetros) se vuelve difícil de manejar y propenso a errores.
4. **Creación de objetos configurables**: cuando necesita crear objetos con diferentes configuraciones o variaciones y desea una forma más flexible y legible de especificar estas configuraciones.
5. **Interfaz común para múltiples representaciones**: cuando desea proporcionar una interfaz común para construir diferentes representaciones de un objeto.

### 13.6 ¿Cuándo NO se usa el patrón Builder? (diapositiva 30)

1. **Construcción de objetos simples**: no usar para objetos con pocos parámetros o configuraciones simples.
2. **Preocupaciones sobre el rendimiento**: la sobrecarga adicional que introduce el patrón Builder puede ser un problema en aplicaciones con rendimiento crítico. Puede afectar el rendimiento si la construcción de objetos es frecuente.
3. **Objetos inmutables con campos `final` o `const`**: no usar si va a trabajar con un lenguaje que admite objetos inmutables con campos finales y la estructura del objeto es relativamente simple.
4. **Cuando se quiere evitar introducir mayor complejidad del código**: la introducción de una clase constructora para cada objeto complejo puede generar un aumento en la complejidad del código.
5. **Acoplamiento estrecho con el producto**: si el constructor está estrechamente vinculado al producto que construye, y los cambios en el producto requieren modificaciones correspondientes en el constructor, podría reducir la flexibilidad y la capacidad de mantenimiento del código.

### 13.7 Preguntas que resuelve un arquitecto con Builder (diapositiva 31)

| Pregunta | Respuesta del patrón |
|---|---|
| ¿Cómo simplificar la construcción de objetos con múltiples parámetros? | Se evita el uso de constructores largos con demasiados parámetros. |
| ¿Cómo hacer que la construcción de objetos sea más flexible? | Se pueden crear distintas configuraciones de un objeto sin modificar su estructura. |
| ¿Cómo desacoplar el proceso de construcción de la representación final del objeto? | El director define los pasos de construcción sin saber detalles de la implementación específica. |
| ¿Cómo hacer que el código sea más mantenible y extensible? | Si se agregan nuevas opciones, solo se modifican los métodos del Builder, sin afectar el cliente. |
| ¿Cómo permitir diferentes representaciones de un objeto sin modificar su código? | Se pueden agregar más builders para diferentes tipos de automóviles (híbridos, eléctricos, etc.). |

### 13.8 Ejemplo de código de referencia (C#)

```csharp
// Producto
public class Pizza
{
    public string Masa { get; set; }
    public string Salsa { get; set; }
    public List<string> Ingredientes { get; set; } = new();
    public void Mostrar() =>
        Console.WriteLine($"Pizza: masa {Masa}, salsa {Salsa}, ingredientes: {string.Join(", ", Ingredientes)}");
}

// Interfaz Builder: los pasos
public interface IPizzaBuilder
{
    void Reset();
    void PrepararMasa();
    void AgregarSalsa();
    void AgregarIngredientes();
    Pizza ObtenerPizza();
}

// Constructor concreto
public class PizzaBuilder : IPizzaBuilder
{
    private Pizza pizza = new();

    public void Reset() => pizza = new Pizza();
    public void PrepararMasa() => pizza.Masa = "delgada";
    public void AgregarSalsa() => pizza.Salsa = "tomate";
    public void AgregarIngredientes() => pizza.Ingredientes.AddRange(new[] { "queso", "pepperoni" });
    public Pizza ObtenerPizza()
    {
        Pizza resultado = pizza;
        Reset();                     // deja el builder listo para otra pizza
        return resultado;
    }
}

// Director: define la secuencia
public class Pizzeria
{
    private IPizzaBuilder builder;
    public Pizzeria(IPizzaBuilder builder) => this.builder = builder;

    public void HacerPizzaCompleta()
    {
        builder.Reset();
        builder.PrepararMasa();
        builder.AgregarSalsa();
        builder.AgregarIngredientes();
    }

    public void HacerPizzaSoloQueso()
    {
        builder.Reset();
        builder.PrepararMasa();
        builder.AgregarSalsa();
        // sin ingredientes extra: el proceso CAMBIA según el producto
    }
}

// Uso
var builder = new PizzaBuilder();
var pizzeria = new Pizzeria(builder);
pizzeria.HacerPizzaCompleta();
Pizza miPizza = builder.ObtenerPizza();
miPizza.Mostrar();
```

---

## 14. Patrón Prototype (Prototipo)

### 14.1 Definición (diapositiva 32)

> **"Prototype es un patrón creacional que permite clonar objetos existentes sin acoplar un código a sus clases específicas."**

- El objeto se clona a partir de **una instancia ya existente**, lo que permite una mayor flexibilidad y facilidad de mantenimiento.
- **No habría dependencia directa de los constructores** ni de la implementación concreta de una clase.

### 14.2 ¿Por qué se quisiera clonar un objeto? (diapositiva 33)

Puede ocurrir que el **costo computacional y de recursos para generar una nueva instancia es alto** en comparación al costo de realizar una copia de una instancia ya existente. Ejemplos de la diapositiva:

1. **Instanciar un objeto cuyos datos han sido obtenidos previamente**: debería volver a usar los recursos para obtener los datos nuevamente y asignarlos al objeto recientemente instanciado. **Sale mejor clonarlo.**
2. **Un sistema de encuestas** permite generar formularios personalizados con base en plantillas predefinidas. En lugar de construir cada formulario desde cero, se usa un prototipo (`FormularioBase`) que se clona y se personaliza.
3. **Un sistema empresarial** que maneja objetos costosos (en recursos) de inicializar (conexiones a bases de datos, configuraciones de usuario). Se mantiene un objeto **en caché** y, cuando se necesita, se clona en lugar de instanciarlo de nuevo.
4. **En sistemas de IA o Machine Learning**, se requiere generar múltiples copias de un modelo de datos para experimentos o simulaciones. Se crea un modelo base (`ModeloBase`) y se clona varias veces con parámetros distintos, **en lugar de reentrenarlo desde cero**.

### 14.3 Estructura (diapositiva 34)

```
┌──────────┐            ┌───────────────────────────┐
│  Client  │───────────▶│    «interface» Prototype  │ ← Interface que define la
└──────────┘            ├───────────────────────────┤   operación de clonado.
 copy = existing.clone()│    + clone(): Prototype   │   Normalmente firma un
                        └─────────────▲─────────────┘   solo método clone
                                      │ implementa
                        ┌─────────────┴─────────────┐
                        │    ConcretePrototype      │ ← Prototipo Concreto:
                        ├───────────────────────────┤   implementa la interface
                        │ - field1                  │   y su método Clone() para
                        │ + ConcretePrototype(prot.)│   proceder al clonado.
                        │ + clone(): Prototype      │   → this.field1 = prototype.field1
                        └─────────────▲─────────────┘   → return new ConcretePrototype(this)
                                      │
                        ┌─────────────┴─────────────┐
                        │    SubclassPrototype      │
                        ├───────────────────────────┤
                        │ - field2                  │
                        │ + SubclassPrototype(prot.)│   → super(prototype)
                        │ + clone(): Prototype      │     this.field2 = prototype.field2
                        └───────────────────────────┘   → return new SubclassPrototype(this)
```

**Participantes:**
- **Interface**: define la operación de clonado. Normalmente firma un solo método `clone`.
- **Prototipo Concreto**: implementa la interface y su método `Clone()` para proceder al clonado del objeto.
- **Cliente**: la clase que solicita al prototipo que se clone.

### 14.4 Tipos de clonación (diapositiva 35)

**Contexto**: `Vehiculo` es un **todo** compuesto por `Carroceria` y `Rueda` (sus **partes**).

#### Clonación superficial (*shallow copy*)

```
   Vehiculo1 ────▶ Rueda ◀──── Vehiculo2
          ────▶ Carroceria ◀──
```

- Se clona **bit a bit**, por lo tanto **lo único que se clona es el todo** y **los clones comparten las partes**.
- Los clones del todo **son instancias independientes**: si se cambia un valor de un atributo (del todo), no afecta a los otros.
- **Las partes son las mismas para los clones del todo**: por lo tanto, **un cambio en los atributos de las partes cambia para los clones relacionados**. *(Si a `Vehiculo2` le cambian la rueda, `Vehiculo1` también "ve" el cambio, porque ambos apuntan al mismo objeto `Rueda`.)*

#### Clonación profunda (*deep copy*)

```
   Vehiculo1 ──▶ Rueda          Vehiculo2 ──▶ Rueda2
             ──▶ Carroceria                ──▶ Carroceria2
```

- En esta clonación, **se clona la estructura Todo-Partes completa**: el clon tiene su propia `Rueda2` y su propia `Carroceria2`. Nada queda compartido.

> **Regla práctica**: ¿el objeto tiene referencias a otros objetos mutables? → define conscientemente si necesitas *shallow* (rápida, comparte partes) o *deep* (totalmente independiente). Referencia de la diapositiva: https://danielggarcia.wordpress.com/

### 14.5 Implementación del curso: facturación (diapositiva 36)

```
┌────────────┐                            ┌──────────┐
│ IPrototype │                            │ Factura  │
└─────▲──────┘                            └────▲─────┘
      │ "Implementa"                   "Es parte de"│1
      │         ┌───────────┐               │        │ cliente
      │    ┌────┤ Articulo  │◀──────────────┤        ▼ 1
      └────┤    └───────────┘  l_articulos  │   ┌─────────┐
   "Implementa"                             └──▶│ Cliente │
                                                └─────────┘
```

`Factura`, `Articulo` y `Cliente` implementan `IPrototype` (cada uno sabe clonarse); una factura contiene artículos y un cliente → clonar una factura implica decidir si los artículos y el cliente se clonan también (deep) o se comparten (shallow).

**Código de referencia:**

```csharp
// Interfaz de clonado
public interface IPrototype<T>
{
    T Clonar();
}

public class Articulo : IPrototype<Articulo>
{
    public string Nombre { get; set; }
    public double Precio { get; set; }

    public Articulo Clonar() => (Articulo)this.MemberwiseClone(); // shallow
}

public class Factura : IPrototype<Factura>
{
    public int Numero { get; set; }
    public List<Articulo> Articulos { get; set; } = new();

    // Clonación SUPERFICIAL: la lista de artículos se comparte
    public Factura ClonarSuperficial() => (Factura)this.MemberwiseClone();

    // Clonación PROFUNDA: también se clonan las partes
    public Factura ClonarProfunda()
    {
        Factura copia = (Factura)this.MemberwiseClone();
        copia.Articulos = this.Articulos.Select(a => a.Clonar()).ToList();
        return copia;
    }
}

// Cliente
var original = new Factura { Numero = 1 };
original.Articulos.Add(new Articulo { Nombre = "Llanta", Precio = 350000 });

Factura copia = original.ClonarProfunda();
copia.Numero = 2;
copia.Articulos[0].Precio = 400000;   // NO afecta a la factura original (deep)
```

### 14.6 Un arquitecto de software usa Prototype cuando... (diapositiva 37)

- Se requiere la **creación eficiente de objetos similares** sin pasar por un proceso costoso de configuración.
- Se necesita **evitar el uso de `new` repetidamente**, lo cual mejora la modularidad y la capacidad de prueba del código.
- Se requiere **flexibilidad para duplicar objetos sin conocer sus clases exactas**.
- Se trabaja con **configuraciones complejas** donde es más sencillo copiar un prototipo en lugar de configurar uno nuevo desde cero.

### 14.7 Preguntas clave antes de usar Prototype (diapositiva 38)

| Pregunta | Qué aporta Prototype |
|---|---|
| ¿Los objetos son costosos de crear? | El uso de Prototype puede ahorrar recursos. |
| ¿Necesitamos muchas variaciones de un mismo objeto con pequeñas diferencias? | Prototype permite clonar y modificar solo lo necesario. |
| ¿Los objetos contienen estructuras complejas o referencias anidadas? | Se debe definir si la clonación debe ser superficial o profunda. |
| ¿Es un problema usar `new` directamente en múltiples lugares del código? | Si se quiere evitar la creación repetitiva y mejorar el mantenimiento, Prototype es una buena solución. |
| ¿Queremos reducir la dependencia de clases concretas y mejorar la flexibilidad del código? | Con Prototype, el código depende menos de los constructores y más de la clonación de instancias ya configuradas. |

---

## 15. Patrón Singleton

### 15.1 Definición (diapositiva 39)

> **"Singleton es un patrón creacional que asegura que una clase tenga una única instancia y proporciona un punto global de acceso a esa instancia."**

Es muy útil para situaciones que requieren un **control centralizado**, como por ejemplo:
- La **gestión de conexiones de bases de datos**.
- Una clase **Configuración** que todos la deben usar.
- Una clase que **recibe los datos de varios sensores** y todos pueden acceder a esta a consultarlos.

Reglas clave:
- **No se puede usar un constructor normal** (el constructor se hace privado).
- **Evita que otro objeto sobrescriba la instancia.**

### 15.2 Estructura (diapositiva 40)

```
                        ┌─────────────────────────────────────┐
                        │            CenDiagnAuto             │
                        │         (se "Provee instancia"      │
                        │            a sí misma, relación 1)  │
                        ├─────────────────────────────────────┤
   Atributo Estático →  │ + totalautomoviles: static int      │
                        │ - instancia: CentroDiagnAuto        │ ← En C# se usa el delegado
                        ├─────────────────────────────────────┤   Lazy<T>, permite la
                        │ - CentroDiagnAuto()                 │   creación de objetos de
                        │ + Instancia(): instancia  ← get     │   forma diferida
                        │ + RegistrarAutomovil(modelo:string) │
                        └─────────────────────────────────────┘
```

Elementos anotados en la diapositiva:
- **Atributo estático** que guarda la única instancia.
- En C# se usa el delegado **`Lazy<T>`**, que permite la creación de objetos de forma **diferida** (perezosa).
- **Accesor de tipo `get`** que entrega siempre la misma instancia.
- **Constructor privado** (el `-` en `-CentroDiagnAuto()`).

**Código de referencia (C#):**

```csharp
public class CenDiagnAuto
{
    // Atributo estático con inicialización perezosa (Lazy<T>)
    private static readonly Lazy<CenDiagnAuto> _instancia =
        new Lazy<CenDiagnAuto>(() => new CenDiagnAuto());

    public static int totalautomoviles;   // atributo estático compartido

    // Constructor privado: nadie puede hacer "new CenDiagnAuto()"
    private CenDiagnAuto() { }

    // Punto global de acceso (accesor get)
    public static CenDiagnAuto Instancia => _instancia.Value;

    public void RegistrarAutomovil(string modelo)
    {
        totalautomoviles++;
        Console.WriteLine($"Registrado {modelo}. Total: {totalautomoviles}");
    }
}

// Uso: siempre es EL MISMO objeto
CenDiagnAuto.Instancia.RegistrarAutomovil("Mazda 3");
CenDiagnAuto.Instancia.RegistrarAutomovil("Renault Logan");
```

### 15.3 Ventajas de Singleton (diapositiva 41)

- El patrón Singleton **garantiza que solo haya una instancia** con un identificador único, lo que ayuda a prevenir problemas de nombres.
- Este patrón admite tanto la **inicialización ansiosa** (crear la instancia cuando se carga la clase) como la **inicialización perezosa** (crearla cuando se necesita por primera vez), lo que proporciona adaptabilidad en función del caso de uso.
- Cuando se implementa correctamente, un Singleton puede ser **seguro para subprocesos** (*thread-safe*), lo que garantiza que varios subprocesos no creen accidentalmente instancias duplicadas. *(En C#, `Lazy<T>` ya maneja esto por defecto.)*
- Al mantener solo una instancia, el patrón Singleton puede ayudar a **reducir el uso de memoria** en aplicaciones donde los recursos son limitados.

### 15.4 Desventajas de Singleton (diapositiva 42)

- Los singletons pueden **dificultar las pruebas unitarias**, ya que introducen un **estado global**; su estado puede influir en los resultados de las pruebas.
- En entornos multiproceso, el proceso de creación e inicialización de un Singleton puede generar **"alta concurrencia"** si varios subprocesos intentan crearlo simultáneamente.
- Si más adelante descubre que **necesita varias instancias** o desea modificar la forma en que se crean las instancias, es posible que se requieran **cambios importantes en el código**.
- El patrón Singleton crea una **dependencia global**, lo que puede complicar el reemplazo del Singleton con una implementación diferente o el uso de **inyección de dependencia**.
- **Crear subclases de un Singleton puede ser complicado**, ya que el constructor suele ser privado. Esto requiere un manejo cuidadoso y puede no ajustarse a las prácticas de herencia estándar.

### 15.5 Singleton y atributos estáticos (diapositiva 43)

Cuando una clase Singleton tiene atributos estáticos en C#, estos atributos se **comparten en toda la aplicación** y son accesibles **sin necesidad de instanciar el Singleton**.

**Comportamiento de atributos estáticos en un Singleton:**

1. **Pertenecen a la clase, no a la instancia**: los atributos estáticos se almacenan en memoria una sola vez y no dependen de la instancia del Singleton.
2. **Se inicializan en el primer acceso**: en C#, los atributos estáticos se inicializan la primera vez que se accede a la clase, y su valor se mantiene a lo largo de toda la ejecución del programa.
3. **Independencia de la instancia del Singleton**: aunque un Singleton garantiza que solo hay una instancia, los atributos estáticos existen independientemente de la instancia.

### 15.6 Uso profesional en software automotriz (diapositiva 44)

En el contexto de desarrollo de software automotriz, un arquitecto de software podría usar el Singleton en:

- **Un módulo de diagnóstico de vehículos**: centraliza la recopilación de datos de sensores de todos los automóviles en producción.
- **Un gestor de configuración del sistema**: evita que múltiples instancias configuren parámetros críticos de los sistemas electrónicos.
- **Un controlador de acceso a una base de datos de vehículos**: asegura que todas las consultas pasen por un único punto de acceso.

---

## 16. Relación entre SOLID y los Patrones Creacionales

Los patrones creacionales **materializan** varios principios SOLID. Esta tabla conecta los dos mazos de diapositivas:

| Patrón | Principios SOLID que refuerza | ¿Cómo? |
|---|---|---|
| **Factory Method** | OCP, SRP, DIP | Agregar un producto = crear una fábrica nueva, sin tocar el núcleo (OCP). La creación queda en la fábrica, no dispersa (SRP). El cliente depende de la interfaz del producto (DIP). |
| **Abstract Factory** | OCP, DIP, ISP | Nueva familia = nueva fábrica sin modificar código (OCP). El cliente solo conoce interfaces de productos y de fábrica (DIP/ISP). |
| **Builder** | SRP, OCP | Separa la construcción de la representación (SRP). Nuevas variantes = nuevos builders (OCP). |
| **Prototype** | DIP | El cliente clona a través de la interfaz `IPrototype` sin conocer clases concretas ni usar `new`. |
| **Singleton** | ⚠️ En tensión con DIP | Es útil pero crea dependencia global y dificulta pruebas e inyección de dependencias; usar con moderación (ver desventajas). |

**Idea central compartida**: tanto SOLID como los patrones creacionales buscan que el código cliente **no haga `new` de clases concretas por todas partes**, sino que la creación quede **centralizada, encapsulada y detrás de abstracciones**.

---

## 17. Tablas Resumen (Cheat Sheets)

### 17.1 SOLID en una página

| Letra | Nombre | Regla de oro | Síntoma de violación | Curación típica |
|---|---|---|---|---|
| **S** | Responsabilidad Única | 1 clase = 1 razón para cambiar | La clase hace varias cosas (datos + reportes + correos) | Extraer clases/servicios por responsabilidad |
| **O** | Abierto/Cerrado | Extender sin modificar | `if/switch` por tipo que crece con cada requisito | Interfaz/clase abstracta + polimorfismo |
| **L** | Sustitución de Liskov | La subclase respeta el contrato de la base | `throw new NotImplementedException()` en un override | Interfaces específicas / composición |
| **I** | Segregación de Interfaces | Interfaces pequeñas y específicas | Métodos implementados vacíos o que lanzan excepción | Dividir la interfaz grande en varias |
| **D** | Inversión de Dependencia | Depender de abstracciones | `new ClaseConcreta()` dentro de la clase de negocio | Interfaz + inyección por constructor |

### 17.2 Las 7 reglas formales de LSP

| # | Regla | Permitido | Prohibido |
|---|---|---|---|
| 1 | Tipos de parámetros | Iguales o **más abstractos** | Más específicos |
| 2 | Tipo de retorno | Igual o **subtipo** (más específico) | Tipo incompatible/más general |
| 3 | Excepciones | Las mismas o subtipos esperados | Excepciones inesperadas |
| 4 | Precondiciones | Iguales o **más débiles** | Reforzarlas (exigir más) |
| 5 | Postcondiciones | Iguales o **más fuertes** | Debilitarlas (garantizar menos) |
| 6 | Invariantes | Conservarlas siempre | Romper reglas internas de la base |
| 7 | Campos privados de la base | No tocarlos (usar `protected`/métodos) | Modificarlos directamente |

### 17.3 Patrones creacionales en una página

| Patrón | Problema que resuelve | Participantes clave | Úsalo cuando... | Evítalo cuando... |
|---|---|---|---|---|
| **Factory Method** | El cliente no debe saber qué clase concreta instanciar | Product (interfaz), ConcreteProduct, Creator, ConcreteCreator | No conoces de antemano los tipos exactos; quieres puntos de extensión | El proyecto es muy simple (demasiadas subclases) |
| **Abstract Factory** | Crear **familias** de objetos compatibles entre sí | AbstractFactory, ConcreteFactory, AbstractProduct, ConcreteProduct, Client | Hay variantes (eléctrico/gasolina) que no deben mezclarse | Las familias casi no cambian; solo hay objetos independientes |
| **Builder** | Constructores telescópicos / objetos complejos paso a paso | Builder (interfaz), ConcreteBuilder, Director, Product | Muchos parámetros opcionales; proceso por etapas | Objetos simples; rendimiento crítico; inmutables simples |
| **Prototype** | Crear objetos es costoso; mejor clonar | Prototype (interfaz `clone`), ConcretePrototype, Client | Objetos costosos de inicializar; muchas variaciones parecidas | Hay que definir bien shallow vs deep copy |
| **Singleton** | Se necesita exactamente UNA instancia global | Constructor privado, atributo estático, accesor (`Lazy<T>`) | Configuración global, conexión BD, diagnóstico centralizado | Dificulta pruebas unitarias; crea dependencia global |

---

## 18. Preguntas de Autoevaluación

**SOLID**
1. Una clase `Factura` guarda en base de datos, calcula totales e imprime PDF. ¿Qué principio viola y cómo lo corriges?
2. Tu método tiene `if (tipo == "terrestre") ... else if (tipo == "aereo") ...`. ¿Qué principio viola? ¿Qué estructura lo soluciona?
3. `Pinguino : Ave` hereda `Volar()` pero lanza excepción. ¿Qué principio viola? ¿Cómo lo rediseñas con interfaces?
4. ¿Cuál es la diferencia entre precondiciones y postcondiciones en LSP? Da un ejemplo de cada violación.
5. ¿Por qué una interfaz grande con 15 métodos es un problema? ¿Cómo se relaciona con los métodos "Not implemented"?
6. Explica con tus palabras la "inversión" en DIP: ¿quién dependía de quién antes y quién depende de quién después?
7. ¿Por qué la inyección por constructor facilita las pruebas unitarias?
8. En el caso de Tránsito: ¿qué principio viola la clase `Mayor` y qué servicios proponía la solución?

**Patrones creacionales**
9. ¿Qué diferencia hay entre Factory Method y Abstract Factory? (Pista: producto individual vs familia de productos.)
10. En Abstract Factory, ¿por qué agregar un nuevo *tipo de producto* es costoso, pero agregar una nueva *familia* es fácil?
11. ¿Qué problema resuelve el Director en Builder? ¿Se puede usar Builder sin Director?
12. ¿Cuándo es preferible Prototype sobre un constructor? Da dos ejemplos de las diapositivas.
13. Explica shallow copy vs deep copy con el ejemplo de `Vehiculo`, `Carroceria` y `Rueda`.
14. ¿Qué hace `Lazy<T>` en el Singleton de C#? ¿Qué problema de concurrencia evita?
15. ¿Por qué se dice que Singleton está en tensión con el principio DIP?

---

## 19. Glosario Rápido

| Término | Definición |
|---|---|
| **Acoplamiento** | Grado de dependencia entre clases/módulos. Objetivo: bajo. |
| **Cohesión** | Qué tan enfocada está una clase en una sola tarea. Objetivo: alta. |
| **Contrato** | Conjunto de métodos/comportamientos que una clase promete cumplir (interfaz). |
| **Hardcodear** | Dejar valores fijos en el código en lugar de externalizarlos a configuración. |
| **Inyección de dependencias** | Entregar las dependencias desde afuera (usualmente por el constructor) en vez de crearlas dentro. |
| **Invariante** | Condición que siempre debe cumplirse en un objeto durante su vida. |
| **Precondición** | Lo que debe cumplirse ANTES de ejecutar un método. |
| **Postcondición** | Lo que se garantiza DESPUÉS de ejecutar un método. |
| **Producto** | Objeto devuelto por un método de fábrica. |
| **Shallow copy** | Clonación superficial: solo se copia el objeto; las referencias internas se comparten. |
| **Deep copy** | Clonación profunda: se copia el objeto y toda su estructura de partes. |
| **Constructor telescópico** | Antipatrón: muchas sobrecargas de constructor con combinaciones de parámetros. |
| **`Lazy<T>`** | Tipo de C# que crea el objeto la primera vez que se usa (inicialización diferida), de forma thread-safe. |
| **Value Object** | Objeto pequeño que encapsula un valor y sus reglas de validación (ej. `Placa`). |
| **Patrón Observer** | Mecanismo de eventos: un publicador notifica a suscriptores (alternativa a notificar con excepciones). |
| **Thread-safe** | Seguro para uso concurrente: varios hilos no pueden corromper el estado ni duplicar instancias. |

---

## 20. Bibliografía y Cibergrafía

### Bibliografía (diapositiva 49 / 45)

1. Shvets, Alexander. *Dive into Design Patterns*. Refactoring.guru. 2019.
2. Perera, Srinath. *Software Architecture and Decision-Making: Leveraging Leadership, Technology, and Product Management to Build Great Products*. Addison-Wesley. 2024.
3. Pacheco, Diego & Sgro, Sam. *Principles of Software Architecture Modernization: Delivering engineering excellence with the art of fixing microservices, monoliths...* BPB Publications. 2024.
4. Baptista, Gabriel & Abbruzzese, Francesco. *Software Architecture with C# 12 and .NET 8 — Fourth Edition: Build enterprise applications using microservices, DevOps, EF Core*. 2024.
5. Goldman, Oliver. *Effective Software Architecture: Building Better Software Faster*. Addison-Wesley Professional. 2024.
6. Gilbert, John. *Software Architecture Patterns for Serverless Systems: Architecting for innovation with event-driven microservices*. 2024.

### Cibergrafía (diapositivas 50 / 46)

- FreeCodeCamp (freecodecamp)
- OpenWebinars (openwebinars.net)
- YouTube: *Software Architecture Monday*
- refactoring.guru
- GitHub
- Coursera: *"Software Architecture"* — University of Alberta
- GeeksforGeeks (geeksforgeeks.org)
- https://danielggarcia.wordpress.com/ (tipos de clonación)

---

> **Tip final de estudio**: si solo tienes poco tiempo, memoriza la sección 17 (cheat sheets) y repasa los 5 ejemplos "antes/después" de SOLID (Employee, Order, Automovil eléctrico/gasolina, CloudProvider, BudgetReport) — con eso cubres el núcleo de ambos temas.
