# Curso Intensivo de 4 Días — Arquitectura de Software y Fundamentos de POO

**Preparado a partir de 16 documentos oficiales del curso** (presentación del curso, repaso de POO y paradigmas, introducción a arquitectura, principios SOLID, clases básicas, accesibilidad/constructores/accesores, relaciones entre clases, herencia, polimorfismo, clase Object, programación orientada a eventos, programación orientada a aspectos, paradigma funcional, codificación funcional, introducción a SOA e introducción a ASP.NET Core MVC).

> ⚠️ **Nota metodológica importante (léela antes de empezar):**
> Analicé el contenido real de tus 16 archivos. El curso, tal como está diseñado por tu profesor, está construido **100% sobre C# / .NET** (constructores con `PUBLICO`, `virtual`/`override`, delegados, LINQ, `ViewBag`, ASP.NET Core MVC, etc.). No hay una sola diapositiva en Java. Esto es relevante porque **tu examen muy probablemente tendrá sintaxis de C#**, no de Java.
>
> Por eso, en este curso vas a encontrar:
> - **Java** en los conceptos que son 100% portables y donde Java te da una sintaxis más limpia para entender la idea (clases, encapsulamiento, herencia, polimorfismo, interfaces, SOLID, patrones de diseño). Siempre te muestro el **equivalente exacto en C#** al lado, porque así está en tus diapositivas y así puede pedírtelo el profesor.
> - **Solo C# / .NET**, con nota explícita de "esto no existe igual en Java", en los temas que son específicos de la plataforma: delegados, expresiones lambda con `Func`/`Action`, LINQ, tipos anónimos, tuplas, eventos (`event`, `EventHandler`), `virtual`/`override`/`new` (en Java esto se resuelve distinto), Aspect-Oriented Programming vía `DispatchProxy`/Castle DynamicProxy, y ASP.NET Core MVC. En cada uno de estos te explico qué existe en Java como concepto equivalente (interfaces funcionales, Streams, oyentes de eventos, AspectJ/Spring AOP, Spring MVC) para que no te quedes con un vacío conceptual, aunque la sintaxis no sea trasplantable.
>
> Así tienes lo mejor de los dos mundos: fidelidad total con lo que el profesor va a preguntar, y comprensión profunda que no depende de un lenguaje específico.

> 📌 **Inconsistencia detectada entre documentos:** el deck "Repaso POO" (slide 54) y el deck "Polimorfismo" (slide 15) definen "interfaz" de forma casi idéntica pero uno agrega la frase "es una forma de implementar herencia múltiple" y el otro dice literalmente "es un machetazo para tratar de simular herencia múltiple". Son la misma idea con distinto tono; la unifiqué como: *"Una interfaz es el mecanismo de C#/Java para lograr algo parecido a la herencia múltiple de comportamiento, sin heredar de dos clases a la vez."*
>
> 📌 **Concepto agregado como contexto complementario** (no aparece explícitamente en tus PDFs pero es indispensable para entender el material): la diferencia entre **enlace estático (static binding)** y **enlace dinámico (dynamic binding)**, que es la razón técnica de por qué el polimorfismo de sobrescritura (`override`) funciona en tiempo de ejecución y la sobrecarga (`overload`) se resuelve en tiempo de compilación. También agregué la notación completa de multiplicidad UML con ejemplos visuales adicionales, y el patrón "Strategy", "Factory" y "Observer" en detalle porque tus diapositivas los mencionan de pasada en SOLID pero no los desarrollan — y probablemente el profesor los da por sabidos.

---

# FASE 1 — Análisis Global del Contenido

## 1.1 Inventario de documentos y qué aporta cada uno

| # | Documento | Rol en el curso | Profundidad |
|---|---|---|---|
| 0 | Presentación del Curso | Logística (evaluación, acuerdos). Sin contenido técnico de examen. | Bajo (no cae en examen) |
| 1 | Repaso POO y Paradigmas NF | **Repaso panorámico** de casi todo: clases, atributos, constructores, accesores, `this`, sobrecarga, pilares OOP, cohesión/acoplamiento, accesibilidad, relaciones entre clases, herencia, polimorfismo, interfaces, paradigma funcional completo | Alta (es el resumen maestro) |
| 2 | Conceptos Básicos de Clases | Profundiza: abstracción, modularidad, extensibilidad, complejidad, objeto, clase, atributos derivados/restricciones, UML básico, reuso, encapsulamiento, cohesión/acoplamiento | Alta |
| 3 | Accesibilidad, Constructores, Accesores, Instanciación | Profundiza: ocultamiento/modificadores de acceso, constructores, accesores get/set, `this`, sobrecarga, instanciación paso a paso | Alta |
| 4 | Relaciones entre Clases | Profundiza: asociación, multiplicidad, navegabilidad, rol, clase de asociación, agregación, composición | Alta |
| 5 | Herencia | Profundiza: tipos de herencia, ES-UN, clases abstractas, ocultamiento en la herencia | Alta |
| 6 | Polimorfismo | Profundiza: overriding, virtual/override/abstract, atributos estáticos, ocultamiento con `new`, interfaces, overload | Alta |
| 7 | Object As Is | Clase `Object`, operadores `is`/`as` | Media (corto pero cae en examen) |
| 8 | Programación Orientada a Eventos | Publisher/Subscriber, delegados aplicados a eventos | Media |
| 9 | Paradigma Orientado a Aspectos | AOP: aspecto, advice, pointcut, join point, proxies dinámicos | Media |
| — | Paradigma Funcional (Del_Mut) | Profundiza paradigma funcional: delegados, Func/Action, inmutabilidad | Alta |
| — | 1_1 Codificación Funcional | Profundiza: ternario, lambdas, predicados, funciones de orden superior, LINQ | Alta |
| — | Intro a Programación Orientada a Servicios | SOA, microservicios, Singleton | Media |
| — | Intro Proyecto ASP.NET Core MVC | MVC, verbos HTTP, ViewBag/ViewData/TempData | Media |
| — | 2 Intro Arquitectura | Definiciones de arquitectura, vistas (4+1), estilos arquitectónicos (capas, monolito, MVC, cliente-servidor, SOA, microservicios, DDD, Clean Architecture, hexagonal, event-driven, agéntica) | Alta |
| — | 3 Principios SOLID | Los 5 principios a fondo + caso de estudio real (Concesionario, Tránsito) — **es tu primer trabajo, vale 25%** | Muy alta |

## 1.2 Temas repetidos (unificados en este curso, sin duplicar)

Los siguientes temas aparecen en **más de un documento** casi textualmente. Los unifiqué en una sola explicación definitiva para que no estudies lo mismo dos veces:

- **Clase/Objeto/Abstracción** → aparece en "Repaso POO" y en "Conceptos Básicos de Clases". Unificado en Día 1.
- **Constructores, accesores, `this`, sobrecarga** → aparece en "Repaso POO" y en "Accesibilidad...". Unificado en Día 1.
- **Relaciones entre clases (asociación/agregación/composición)** → aparece en "Repaso POO" y en "Relaciones entre Clases" (con el mismo ejercicio del parque de diversiones). Unificado en Día 1.
- **Herencia (tipos, ES-UN, clases abstractas)** → aparece en "Repaso POO" y en "Herencia". Unificado en Día 2.
- **Polimorfismo, interfaces, overload** → aparece en "Repaso POO" y en "Polimorfismo". Unificado en Día 2.
- **Paradigma funcional completo (ternario, lambdas, delegados, Func/Action, LINQ, tipos anónimos)** → aparece en **tres** documentos distintos ("Repaso POO" slides 63-107, "Paradigma Funcional Del_Mut" completo, y "1_1 Codificación Funcional" completo). Unificado en Día 3 como un solo desarrollo profundo.
- **Cohesión/Acoplamiento** → aparece en "Repaso POO" y "Conceptos Básicos". Unificado en Día 1.
- **El ejercicio del "Concesionario de Automóviles"** aparece en Herencia, Polimorfismo y SOLID — es, de hecho, el hilo conductor real de tu curso. Por eso lo usamos como **Proyecto Integrador**.
- **El ejercicio del "Parque de Diversiones"** aparece en Relaciones entre Clases y se retoma en Programación Orientada a Eventos (la taquilla sin boletas dispara un evento). Lo integramos como proyecto secundario de refuerzo en UML y eventos.

## 1.3 Temas relacionados (dependencias fuertes)

- UML (clases, relaciones, herencia, interfaces) es el **lenguaje** que describe TODO lo demás → debe aprenderse en paralelo con cada concepto de POO, no aislado.
- SOLID **no se puede entender sin** dominar antes: clases, encapsulamiento, herencia, polimorfismo, interfaces e inversión de control básica.
- La Programación Orientada a Aspectos **no se entiende sin** haber entendido antes por qué SRP (Single Responsibility) es deseable — el AOP existe precisamente para resolver lo que SRP no puede resolver solo con herencia/interfaces.
- Los Eventos **no se entienden sin** Delegados (paradigma funcional) — un evento es, técnicamente, un delegado especial.
- ASP.NET Core MVC **no se entiende sin** el patrón arquitectónico MVC general, ni sin Inyección de Dependencias (que es DIP aplicado).
- SOA/Microservicios **no se entienden sin** el concepto de acoplamiento bajo / cohesión alta (Día 1) y sin interfaces (Día 2).

## 1.4 Conceptos implícitos (necesarios pero no siempre explicados a fondo en las diapositivas)

- **Enlace estático vs. dinámico** (agregado como contexto complementario) — explica por qué `override` sí es polimorfismo real y `overload`/`new` no lo son de la misma manera.
- **Value Objects** (mencionado de pasada en SOLID slide 45, "Considerar usar Value Objects para Placa") — lo explico en el Día 4.
- **Inyección de Dependencias como patrón**, más allá de DIP como principio — se explica en Día 4.
- **Patrones de diseño** Strategy, Factory, Observer, Decorator, Adapter, Facade, Template Method — se nombran en SOLID pero no se explican; los desarrollo en el Día 4 porque las instrucciones de estudio los piden explícitamente.

## 1.5 Vacíos del material (cosas que el examen podría asumir que ya sabes, pero que no están en los PDFs)

- No hay una explicación formal de **tipos de datos primitivos en C#** — se asume que ya los manejas (lo repasamos brevemente en Día 1).
- No hay diagrama de **secuencia** ni de **casos de uso** desarrollado (solo se nombran como tipos de diagramas UML). Si tu examen incluye UML, es casi seguro que será diagrama de clases (es el único desarrollado a fondo).
- No se explica formalmente qué es una **excepción** en C# (aparece usada, ej. `DivideByZeroException`, `NotImplementedException`) — la repasamos brevemente donde aparece.
- El documento de Arquitectura menciona 11 estilos arquitectónicos pero **no profundiza en ninguno con código** — los explico con ejemplos conceptuales adicionales.

## 1.6 Conceptos fundamentales (alta probabilidad de examen)

Clase, Objeto, Atributo, Método, Encapsulamiento, Abstracción, Herencia, Polimorfismo, Constructor, Accesor (get/set), Sobrecarga (Overload), Sobrescritura (Override), Clase abstracta, Interfaz, Asociación, Agregación, Composición, Dependencia, Multiplicidad, Navegabilidad, los 5 principios SOLID, Delegado, Evento, Lambda, LINQ, Inmutabilidad, MVC, SOA, Microservicio.

## 1.7 Conceptos secundarios (menor probabilidad, pero mencionados)

AOP (aspecto/advice/pointcut), Singleton, Clean Architecture, Arquitectura Hexagonal, Domain Driven Design, Arquitectura Agéntica, ViewBag/ViewData/TempData, verbos HTTP.

## 1.8 Preguntas típicas de examen que detecté (patrones repetidos en tus decks)

1. "Diseñe en UML y luego implemente en C#..." (aparece en Clases Básicas, Relaciones, Herencia, Polimorfismo) — **el formato de examen más probable es: enunciado de negocio → diagrama de clases → código C#**.
2. "¿Qué principio SOLID se viola aquí y cómo lo corrige?" (aparece explícitamente en el caso de estudio de Tránsito/Multas del deck SOLID).
3. Preguntas de "¿es composición, agregación o asociación?" con casos ambiguos (el propio deck de Relaciones ya incluye un caso donde el profesor corrige una respuesta "incorrecta": la relación Venta-Cliente-Vendedor-Automóvil).
4. Identificar qué línea de código viola LSP (aparece con el ejemplo de `Automovil`/`Electrico`/`Gasolina`).
5. Explicar la diferencia entre `virtual`/`override` vs. `new` (hiding) — el deck insiste "Ojo: no utilizar new y override al mismo tiempo, son excluyentes", frase que suena a pregunta de examen.

---

# FASE 2 — Mapa de Conocimiento (ruta de dependencias)

```
NIVEL 0 (Base absoluta)
Clase → Objeto → Atributo/Método → Abstracción
        │
        ▼
NIVEL 1 (Estructura interna de una clase)
Encapsulamiento/Accesibilidad → Constructores → Accesores (get/set) → this → Sobrecarga (Overload) → Instanciación
        │
        ▼
NIVEL 2 (Cómo se relacionan las clases entre sí — UML)
Asociación → Multiplicidad → Navegabilidad/Rol → Clase de Asociación → Agregación → Composición → Dependencia
        │
        ▼
NIVEL 3 (Jerarquías)
Herencia (simple/multinivel) → Clases Abstractas → Sobrescritura (Override) → Métodos Virtuales/Abstractos →
        Ocultamiento con "new" → Interfaces (Realización) → Polimorfismo (unifica override + interfaces + overload)
        │
        ▼
NIVEL 4 (La clase raíz y utilidades)
Clase Object (Equals, GetHashCode, GetType, ToString) → Operadores is/as
        │
        ▼
NIVEL 5 (Otro paradigma: funcional, se apoya en Nivel 0-1)
Expresión ternaria → Lambdas → Predicados → Delegados → Delegados Genéricos → Func/Action →
        Funciones de orden superior → LINQ → Tipos Anónimos → Inmutabilidad
        │
        ▼
NIVEL 6 (Paradigmas que se apoyan en Nivel 3 + Nivel 5)
Programación Orientada a Eventos (necesita Delegados) ── y ── Programación Orientada a Aspectos (necesita entender por qué SRP no basta)
        │
        ▼
NIVEL 7 (Calidad de diseño — el gran examen de fondo del curso)
Cohesión/Acoplamiento (Nivel 0) + Herencia/Polimorfismo/Interfaces (Nivel 3) + Delegados (Nivel 5)
        ⇒ PRINCIPIOS SOLID (SRP, OCP, LSP, ISP, DIP)
        ⇒ Patrones de diseño (Strategy, Factory, Observer, Decorator, Adapter, Facade, Template Method, Singleton, DI)
        │
        ▼
NIVEL 8 (Arquitectura — el "para qué" de todo lo anterior)
Definición de Arquitectura → Vistas (4+1) → Estilos arquitectónicos (Capas, Monolito, Cliente-Servidor,
        MVC, SOA, Microservicios, DDD, Clean Architecture, Hexagonal, Event-Driven, Agéntica)
        │
        ▼
NIVEL 9 (Aplicaciones concretas de arquitectura)
Programación Orientada a Servicios (SOA/Microservicios, depende de Nivel 2 + Nivel 7)
        ── y ──
ASP.NET Core MVC (depende de Nivel 8-MVC + DIP de Nivel 7)
```

**Lectura del mapa:** si aprendes el Nivel 0 y 1 perfectamente, entiendes Nivel 2 (UML) sin esfuerzo, porque UML solo es la forma de *dibujar* lo que ya entendiste en código. Si dominas Nivel 3 (herencia/polimorfismo/interfaces), entonces SOLID (Nivel 7) deja de ser "reglas para memorizar" y se vuelve "consecuencia lógica" de los problemas que Nivel 3 puede causar si se usa mal. Por eso el orden de este curso **no sigue el orden de tus PDFs**, sino esta cadena de dependencias.

---

# FASE 3 — Índice Maestro del Curso (reorganizado pedagógicamente)

**DÍA 1 — Cimientos: Clases, Objetos y sus Relaciones**
1. Paradigmas de programación: dónde encaja POO
2. Clase, Objeto, Abstracción (con las 6 propiedades: cohesión, acoplamiento, reuso, complejidad, modularidad, extensibilidad)
3. Atributos (de dato, de estado, de proyecto, derivados, restricciones)
4. UML — Diagrama de clases: notación compacta y extendida
5. Accesibilidad / Ocultamiento (modificadores de acceso)
6. Constructores (`this`, sobrecarga de constructores)
7. Accesores (get/set) y Encapsulamiento
8. Instanciación
9. UML — Relaciones entre clases: Asociación, Multiplicidad, Navegabilidad, Rol, Clase de Asociación
10. UML — Agregación y Composición (Todo-Partes)
11. UML — Dependencia

**DÍA 2 — Jerarquías: Herencia, Polimorfismo, Interfaces**
12. Herencia (simple, múltiple, multinivel), relación ES-UN
13. Clases y métodos abstractos
14. UML — Generalización/Herencia
15. Polimorfismo: Sobrescritura (Overriding), `virtual`/`override`
16. Ocultamiento de miembros con `new` (y por qué NO es polimorfismo)
17. Interfaces: contrato, herencia de interfaces, segregación
18. UML — Realización (Interfaces)
19. Sobrecarga (Overload): paramétrica y "polimorfismo de sobrecarga"
20. La clase `Object`: `Equals`, `GetHashCode`, `GetType`, `ToString`
21. Operadores `is` / `as`
22. Comparativa completa: Asociación vs. Dependencia vs. Agregación vs. Composición vs. Herencia vs. Realización

**DÍA 3 — Paradigma Funcional, Eventos, Aspectos y Arquitectura**
23. Expresión condicional ternaria
24. Expresiones Lambda
25. Predicados (`Predicate<T>`)
26. Delegados (simples, con parámetros, genéricos, multicast)
27. `Func<>` y `Action<>`
28. Funciones de orden superior y LINQ (Map/Filter/Fold)
29. Tipos Anónimos
30. Mutabilidad e Inmutabilidad
31. Programación Orientada a Eventos (Publisher/Subscriber)
32. Programación Orientada a Aspectos (Aspecto, Advice, Pointcut, Join point, Proxies)
33. Introducción a la Arquitectura de Software (definiciones, con/sin arquitectura, vistas, modelo 4+1)
34. Estilos arquitectónicos (Capas, Monolito, Cliente-Servidor, MVC, SOA, Microservicios, DDD, Clean Architecture, Hexagonal, Event-Driven, Agéntica)
35. Programación Orientada a Servicios (SOA, Microservicios, Singleton)
36. ASP.NET Core MVC (Modelo-Vista-Controlador, verbos HTTP, ViewBag/ViewData/TempData)

**DÍA 4 — SOLID a fondo + Patrones + Integración**
37. SRP — Principio de Responsabilidad Única
38. OCP — Principio Abierto/Cerrado
39. LSP — Principio de Sustitución de Liskov
40. ISP — Principio de Segregación de Interfaces
41. DIP — Principio de Inversión de Dependencias
42. Patrones de diseño asociados a SOLID (Strategy, Factory, Observer, Decorator, Adapter, Facade, Template Method, Singleton, Inyección de Dependencias)
43. Proyecto Integrador — versión final refactorizada con SOLID
44. Examen Final


---

# FASE 4 — Plan Intensivo de 4 Días

> Asumiendo jornadas de estudio realistas de **8-9 horas** (con descansos de 10 min cada hora y almuerzo de 1h). Si tienes menos tiempo, prioriza lo marcado como 🔴 (fundamental para examen) sobre lo marcado 🟡 (secundario).

## Día 1 — Cimientos de POO (≈8.5 h)
| Bloque | Tema | Tiempo | Prioridad |
|---|---|---|---|
| 1 | Paradigmas + Clase/Objeto/Abstracción + propiedades OO | 1.5 h | 🔴 |
| 2 | Atributos + UML clase (notación) | 1 h | 🔴 |
| 3 | Accesibilidad, Constructores, Accesores, `this`, sobrecarga | 2 h | 🔴 |
| 4 | Instanciación + Mini-proyecto (versión 1) | 1 h | 🔴 |
| 5 | UML Relaciones: Asociación, Multiplicidad, Navegabilidad, Rol | 1.5 h | 🔴 |
| 6 | UML Agregación, Composición, Dependencia | 1 h | 🔴 |
| 7 | Repaso + Mini examen Día 1 | 0.5 h | 🔴 |

## Día 2 — Herencia, Polimorfismo, Interfaces (≈8.5 h)
| Bloque | Tema | Tiempo | Prioridad |
|---|---|---|---|
| 1 | Herencia + UML Generalización + clases abstractas | 2 h | 🔴 |
| 2 | Polimorfismo: override/virtual/new | 1.5 h | 🔴 |
| 3 | Interfaces + UML Realización + ISP intro | 1.5 h | 🔴 |
| 4 | Sobrecarga (overload) + polimorfismo de sobrecarga | 1 h | 🟡 |
| 5 | Clase Object + is/as | 1 h | 🔴 |
| 6 | Tabla comparativa de todas las relaciones UML | 1 h | 🔴 |
| 7 | Mini examen Día 2 + Ejercicios finales de UML (bloque de 50) | 0.5 h + estudio libre | 🔴 |

## Día 3 — Funcional, Eventos, Aspectos, Arquitectura (≈9 h)
| Bloque | Tema | Tiempo | Prioridad |
|---|---|---|---|
| 1 | Ternario, Lambdas, Predicados | 1 h | 🔴 |
| 2 | Delegados, Func/Action, funciones de orden superior | 1.5 h | 🔴 |
| 3 | LINQ + Tipos anónimos + Inmutabilidad | 1.5 h | 🔴 |
| 4 | Eventos (Publisher/Subscriber) | 1 h | 🟡 |
| 5 | AOP (aspecto/advice/pointcut/proxy) | 1 h | 🟡 |
| 6 | Arquitectura: definiciones, vistas 4+1, estilos arquitectónicos | 1.5 h | 🔴 |
| 7 | SOA + ASP.NET Core MVC | 1 h | 🟡 |
| 8 | Mini examen Día 3 | 0.5 h | 🔴 |

## Día 4 — SOLID + Integración + Examen Final (≈9 h)
| Bloque | Tema | Tiempo | Prioridad |
|---|---|---|---|
| 1 | SRP (problema→intuición→código→refactor→UML) | 1.3 h | 🔴 |
| 2 | OCP | 1.3 h | 🔴 |
| 3 | LSP | 1.3 h | 🔴 |
| 4 | ISP | 1 h | 🔴 |
| 5 | DIP + Patrones de diseño asociados | 1.5 h | 🔴 |
| 6 | Proyecto Integrador — refactor SOLID completo | 1.3 h | 🔴 |
| 7 | Examen SOLID (40 preguntas) | 0.8 h | 🔴 |
| 8 | Examen Final completo | 1 h | 🔴 |

---

# FASE 5 — Desarrollo del Curso

# 📅 DÍA 1 — Cimientos: Clases, Objetos y sus Relaciones

## Tema 1.1 — Paradigmas de Programación y dónde encaja POO

#### Explicación

Antes de hablar de clases, necesitas ubicar mentalmente **qué problema resuelve la Programación Orientada a Objetos (POO)** frente a otras formas de programar.

Imagina que quieres construirle una casa a un cliente. Hay varias "filosofías" para organizar el trabajo:
- **Paradigma imperativo/estructurado**: le das al obrero una lista de instrucciones paso a paso, en orden estricto ("primero pon el ladrillo, luego la mezcla, luego el siguiente ladrillo…"). Es como escribir una receta de cocina larguísima. Funciona, pero si la casa crece, la receta se vuelve inmanejable.
- **Paradigma orientado a objetos (POO)**: en lugar de una receta gigante, divides el problema en "roles" que colaboran: un `Albañil`, un `Electricista`, un `Plomero`. Cada uno sabe hacer su trabajo (comportamiento) y tiene sus propias herramientas y materiales (atributos). Tú solo coordinas quién hace qué y cuándo.
- **Paradigma funcional**: en lugar de roles con estado, piensas en **transformaciones puras**: "dame ladrillos crudos, te devuelvo un muro" — sin que nada "recuerde" un estado intermedio. Lo veremos a fondo el Día 3.
- **Paradigma orientado a eventos**: los roles no actúan por instrucción directa, sino que "reaccionan" cuando algo pasa ("cuando llegue el camión de cemento, el albañil empieza"). Día 3.
- **Paradigma orientado a aspectos**: hay preocupaciones que **cruzan** a todos los roles por igual (por ejemplo, "todos deben firmar un registro de seguridad antes de trabajar") — en vez de repetir esa instrucción en cada rol, la extraes aparte. Día 3.
- **Paradigma orientado a servicios**: en vez de roles/personas, piensas en **unidades de negocio independientes** que se ofrecen unas a otras a través de contratos claros (interfaces/APIs), sin que a nadie le importe cómo lo hacen por dentro. Día 3.

**Definición formal de POO** (de tu material): consiste en envolver datos (atributos) y comportamientos relacionados con esos datos, en paquetes especiales llamados **objetos**, construidos a partir de "planos" llamados **clases**.

Este curso, aunque se llama "Arquitectura de Software", en realidad tiene una estructura de embudo: primero refuerza POO (porque toda arquitectura moderna se construye sobre objetos bien diseñados), luego añade otros paradigmas, y finalmente enseña cómo **organizar** todo eso a gran escala (arquitectura, SOLID). Por eso empezamos por POO.

#### Ejemplos

**Java:**
```java
// Paradigma imperativo/estructurado (sin objetos, todo en un método)
public class ConstruirCasaImperativo {
    public static void main(String[] args) {
        System.out.println("Colocando ladrillo 1");
        System.out.println("Aplicando mezcla");
        // ... 300 líneas más, todo mezclado, sin roles claros
    }
}

// Paradigma orientado a objetos: roles con datos + comportamiento propio
class Albanil {
    private String nombre;
    Albanil(String nombre) { this.nombre = nombre; }
    void construirMuro() { System.out.println(nombre + " está construyendo el muro"); }
}
```

**C# (equivalente exacto, como lo vería tu profesor):**
```csharp
class Albanil {
    private string nombre;
    public Albanil(string nombre) { this.nombre = nombre; }
    public void ConstruirMuro() => Console.WriteLine($"{nombre} está construyendo el muro");
}
```

#### Errores comunes
- Pensar que "paradigma" es sinónimo de "lenguaje de programación". Un mismo lenguaje (C#, Java, Python) puede soportar varios paradigmas al mismo tiempo — de hecho, C# es multiparadigma (POO + funcional + orientado a eventos), y este curso te lo demuestra todo el tiempo.
- Creer que un paradigma "reemplaza" a otro. En la práctica, un programa profesional mezcla POO (para estructurar el dominio) con funcional (para manipular colecciones con LINQ) y con eventos (para la interfaz de usuario). No son excluyentes.

#### Relaciones
Este tema es la "carpeta contenedora" de absolutamente todo el curso: cada uno de los otros 43 temas es una pieza de alguno de estos paradigmas. Sin este mapa mental, memorizas piezas sueltas; con él, sabes *para qué* sirve cada pieza.

#### Resumen
- POO envuelve datos + comportamiento en objetos, construidos desde clases.
- Existen otros paradigmas (funcional, eventos, aspectos, servicios) que **complementan**, no reemplazan, a POO.
- Este curso sigue la cadena: POO sólido → otros paradigmas → cómo organizar todo (SOLID + Arquitectura).

#### Ejercicios
1. **(Conceptual)** Explica con tus palabras la diferencia entre pensar en "instrucciones paso a paso" y pensar en "objetos que colaboran".
2. **(V/F)** Un programa en C# solo puede usar un paradigma a la vez. → Falso.
3. **(Razonamiento)** ¿Por qué crees que el profesor decidió enseñar Programación Orientada a Aspectos *después* de SOLID en el índice original, pero en este curso lo movimos *antes*? (Pista: revisa el Mapa de Conocimiento de la Fase 2).

---

## Tema 1.2 — Clase, Objeto y Abstracción

#### Explicación

**Intuición primero.** Piensa en la palabra "Casa". Cuando alguien dice "una casa", tú no piensas en una casa específica con una dirección exacta — piensas en el **concepto general** de casa: tiene paredes, techo, puertas, ventanas. Ese concepto general, que ignora los detalles particulares de una casa concreta (el color exacto, el barrio, el número de la calle), es una **abstracción**.

- La **Clase** es ese concepto general, formalizado como un plano/plantilla: "toda Casa tiene N habitaciones, M metros cuadrados, un color".
- El **Objeto** es una casa real y concreta construida siguiendo ese plano: "la casa de la Calle 10 #45-30, con 3 habitaciones, 120 m², color blanco".

**Definición formal:**
- **Objeto** (del latín *objectus*: "algo que se puede arrojar/poner delante"): en la práctica, es *cualquier cosa que tenga estructura (atributos) y comportamiento (métodos)*. Los objetos corresponden a sustantivos, se nombran en singular, y existen siempre **en el contexto de un problema** (una "Guitarra" tiene un contexto distinto en un taller de reparación que en una banda de rock — sus atributos relevantes cambian según el contexto).
- **Clase**: una descripción de un conjunto de objetos que comparten los mismos atributos, operaciones, relaciones y semántica. Es el "agrupador" de objetos del mismo tipo. Por convención: **nombre en singular, comienza en mayúscula** (`Bicicleta`, no `bicicletas` ni `Bicicletas`).

**Abstracción** es el proceso de excluir todas las características que *no* importan para el problema que estás resolviendo. Cuanto más alto el nivel de abstracción, menos elementos necesitas para representar el sistema completo, y más fácil es manejar la complejidad. El nivel más alto describe la aplicación (clases/objetos); el más bajo describe los detalles (atributos/métodos).

**Las 6 propiedades que hacen "bueno" un diseño orientado a objetos** (de tu deck de Conceptos Básicos):

| Propiedad | Qué es | Ejemplo |
|---|---|---|
| **Modularidad** | Dividir la solución en partes que se integran perfectamente para un objetivo común. Se pueden agregar/quitar componentes sin romper el todo. | Un sistema de facturación dividido en módulo `Inventario`, módulo `Clientes`, módulo `Facturación`. |
| **Extensibilidad** | Facilidad de modificar la solución en el tiempo. Cambios *externos* a un objeto repercuten en toda la solución; cambios *internos* solo afectan al objeto. | Si cambias cómo `Factura` calcula el IVA internamente, nada fuera de `Factura` debería enterarse. |
| **Complejidad** | Se reduce subiendo el nivel de abstracción; si un objeto tiene más características de las necesarias, se vuelve difícil de usar. | Una clase `Empleado` con 40 atributos es más difícil de manejar que una bien acotada. |
| **Reuso** | Definir una vez las propiedades comunes evita reprogramarlas para cada caso similar. | Una clase `Validador` genérica reutilizada en 10 formularios distintos. |
| **Cohesión** | Qué tan estrechamente relacionados están los componentes de algo para un fin común. | Alta cohesión: todos los métodos de `CalculadoraImpuestos` hablan de impuestos. |
| **Acoplamiento** | Qué tanto dependen los módulos entre sí. | Bajo acoplamiento: `Factura` no necesita saber los detalles internos de `Cliente` para funcionar. |

> 🔑 **Fundamental para el examen:** *Cohesión alta + Acoplamiento bajo* es el objetivo de **todo** el diseño orientado a objetos, y es la vara de medir que usarás en el Día 4 para juzgar si un diseño cumple SOLID o no. Grábate esta frase: **"Queremos que cada clase haga una sola cosa bien (cohesión alta) y que dependa lo menos posible de los detalles internos de las demás (acoplamiento bajo)."**

#### Ejemplos

**Java:**
```java
// La CLASE es el plano
public class Bicicleta {
    // Atributos (estructura)
    private String marca;
    private String color;

    // Métodos (comportamiento)
    public void acelerar() { System.out.println("La bicicleta acelera"); }
}

public class Main {
    public static void main(String[] args) {
        // Los OBJETOS son instancias concretas del plano
        Bicicleta miBici = new Bicicleta();   // objeto 1
        Bicicleta biciDeAna = new Bicicleta(); // objeto 2 — independiente de miBici
    }
}
```

**C#:**
```csharp
public class Bicicleta {
    private string marca;
    private string color;
    public void Acelerar() => Console.WriteLine("La bicicleta acelera");
}

class Program {
    static void Main() {
        Bicicleta miBici = new Bicicleta();
        Bicicleta biciDeAna = new Bicicleta();
    }
}
```

#### Errores comunes

- **Nombrar clases en plural.** `Automóvil` es una clase correcta; `Automóviles` no lo es — una clase describe UN concepto, no una colección. `Motores` no es una clase; "son una cantidad de clases [instancias]".
- **Nombrar clases/objetos con partes del todo sin sentido propio.** "Lado Izquierdo" no es una clase por sí sola (no tiene identidad ni comportamiento independiente reconocible en la mayoría de los dominios).
- **Llamar a una clase "Dato" o "Información".** Esto es un error clásico de principiantes: una clase debe representar un concepto del dominio (`Factura`, `Estudiante`), no una categoría abstracta de programación como "Dato".
- **Confundir clase con objeto en el habla cotidiana.** Decir "voy a crear la clase `miBici`" es incorrecto: `miBici` es un objeto (instancia); `Bicicleta` es la clase.
- **Diseñar clases que son puramente "entradas o salidas"** (ej. una clase `ImprimirReporte` que solo envuelve un `Console.WriteLine`) — esto generalmente indica que falta identificar el verdadero concepto del dominio.

#### Relaciones
Este es el Nivel 0 del mapa de conocimiento: **todo** lo demás en el curso (atributos, UML, herencia, SOLID) es una elaboración sobre "clase" y "objeto". No puedes entender un diagrama UML de clases si no tienes clarísimo que la caja representa la *clase* (el plano) y no un objeto específico.

#### Resumen
- **Clase** = plano/plantilla; **Objeto** = instancia concreta construida desde ese plano.
- **Abstracción** = quedarse solo con lo relevante para el problema, ignorando el resto.
- Las 6 propiedades de un buen diseño OO: Modularidad, Extensibilidad, Complejidad (controlada), Reuso, Cohesión (alta), Acoplamiento (bajo).
- Nombrar clases: sustantivo, singular, mayúscula inicial.

#### Ejercicios

1. **(Conceptual)** ¿Cuál es la diferencia exacta entre un objeto y una clase? Da un ejemplo propio (no de bicicleta ni casa).
2. **(V/F)** "Motores" es un nombre válido para una clase. → Falso, es plural.
3. **(V/F)** La cohesión alta y el acoplamiento bajo son ambos deseables simultáneamente. → Verdadero.
4. **(Selección múltiple)** ¿Cuál de las siguientes NO es una de las 6 propiedades vistas?
   a) Modularidad  b) Herencia  c) Cohesión  d) Extensibilidad
   *(Respuesta: b — Herencia es un pilar de POO, no una de estas 6 propiedades de diseño)*
5. **(Ejercicio de programación corto)** Diseña (solo los nombres de atributos, sin código aún) una clase `Estudiante` para un sistema universitario. Verifica que el nombre esté en singular y que ningún atributo se llame "Dato" o "Información".
6. **(Abierta)** Explica con tus propias palabras por qué "cuanto más alto el nivel de abstracción, menor la complejidad percibida".

#### Mini examen (Tema 1.1 + 1.2) — no mires las respuestas hasta intentarlo
1. Define clase y objeto y da un ejemplo de cada uno.
2. ¿Qué problema resuelve la abstracción?
3. Explica con tus palabras cohesión alta y acoplamiento bajo, y por qué se buscan juntos.
4. Da un ejemplo de un mal nombre de clase y explica por qué está mal.
5. Nombra los 5 paradigmas mencionados en este curso además de POO.


## Tema 1.3 — Atributos: tipos, derivados y restricciones

#### Explicación

Un atributo define la **estructura** de la clase (y de sus objetos). Corresponde a un sustantivo; su valor puede ser un sustantivo o un adjetivo (`Color: Rojo`, `Edad: 25`). El nombre de un atributo es único dentro de una clase (no puede haber dos atributos llamados `color` — si necesitas dos, se llaman `colorFondo` y `colorFrente`).

Tu material clasifica los atributos en tres tipos según su origen (esto es clave para el examen porque no es obvio):

1. **Atributos de dato**: el usuario final los cambia a través de mecanismos definidos para ello (formularios, setters). Ejemplo: el nombre de un cliente, la potencia del motor.
2. **Atributos de estado**: solo los cambian los métodos de la propia clase, de manera exclusiva. Ejemplo: `cambioActual` de una bicicleta, `abierto` de una taquilla.
3. **Atributos de proyecto** (o de negocio): los definen las reglas de negocio de la organización, no el usuario ni un método interno arbitrario. Ejemplo: el valor de entrada a un parque, el porcentaje de descuento VIP.

**Atributos derivados:** dependen del valor de otros atributos (básicos o incluso de otros derivados). En notación UML se identifican con un `/` antes del nombre: `/masaCorporal`. No se almacenan directamente; se calculan.

**Restricciones de atributos:** limitan los valores válidos que puede tomar un atributo. En UML se escriben fuera de la clase, entre llaves `{ }`.

```
Clase: Persona
Atributos: peso, altura, /masaCorporal
Restricción: { masaCorporal = peso / altura² }
```

#### Ejemplos

**Java:**
```java
public class Persona {
    private double peso;   // atributo de dato (kg)
    private double altura; // atributo de dato (m)

    public Persona(double peso, double altura) {
        this.peso = peso;
        this.altura = altura;
    }

    // Atributo DERIVADO: no se guarda, se calcula siempre a partir de los otros
    public double getMasaCorporal() {
        return peso / (altura * altura);
    }
}
```

**C#:**
```csharp
public class Persona {
    public double Peso { get; set; }
    public double Altura { get; set; }

    // Propiedad derivada: se calcula, no se guarda como campo aparte
    public double MasaCorporal => Peso / (Altura * Altura);
}
```

#### Errores comunes
- Confundir un atributo *derivado* con uno *normal*: si guardas `masaCorporal` como un campo aparte y lo actualizas manualmente cada vez que cambian `peso`/`altura`, corres el riesgo de que queden desincronizados. La forma correcta es calcularlo siempre al vuelo (propiedad computada / método).
- Olvidar las restricciones al diseñar: en tus ejercicios (ver el ejercicio de la "Lámpara" y el "Concesionario"), las restricciones (`marca debe tener más de 6 caracteres`, `año no puede ser 2 años mayor al actual`) **son parte de la nota del examen** — normalmente se implementan validando dentro del *setter* (accesor), tema que viene a continuación.
- Pensar que un atributo de "estado" puede ser modificado libremente desde fuera de la clase — por definición, solo los métodos internos deben tocarlo.

#### Relaciones
Los atributos son la mitad de la "estructura" de una clase (la otra mitad son los métodos, vistos como parte de accesores/constructores). Las restricciones de atributos son la semilla de lo que luego se convertirá en **validación dentro de accesores** (Tema 1.7) y, mucho más adelante, en el **principio de responsabilidad única (SRP)** cuando esa validación se vuelve tan compleja que merece su propia clase (como en tu caso de estudio real del Concesionario, donde `Validaciones` se separó de `Automovil`).

#### Resumen
- Atributo = estructura; tres tipos: de dato, de estado, de proyecto.
- Atributo derivado: se calcula, se marca con `/` en UML, no se almacena como valor independiente.
- Restricción: se escribe entre `{ }` fuera de la clase en UML; en código se aplica normalmente en el accesor.

#### Ejercicios
1. Clasifica estos atributos de una clase `CuentaBancaria` en dato/estado/proyecto: `titular`, `saldo`, `tasaInteresPreferencial` (definida por el banco).
2. Diseña el atributo derivado `/edad` a partir de `fechaNacimiento` para una clase `Persona`, y escribe la restricción correspondiente en notación UML.
3. **(V/F)** Un atributo derivado se guarda físicamente en la base de datos igual que uno normal. → Falso (conceptualmente se recalcula; en la práctica de bases de datos a veces se "materializa" por rendimiento, pero conceptualmente en OO no se guarda como independiente).
4. **(Programación corta)** En C#, escribe una propiedad `NombreCompleto` derivada de `Nombre` y `Apellido`.

---

## Tema 1.4 — UML: Representación de una clase (notación compacta y extendida)

#### Explicación

**Intuición.** Ya sabes qué es una clase conceptualmente. UML (*Unified Modeling Language* — Lenguaje de Modelado Unificado) es simplemente el **idioma visual** que usan los ingenieros de software para dibujar clases y sus relaciones antes de escribir código, del mismo modo que un arquitecto dibuja un plano antes de construir una casa.

**Definición formal:** UML es un lenguaje de modelado con varios tipos de diagramas:
- **Estructurales:** Clases, Componentes, Despliegue, Objetos, Paquetes.
- **De comportamiento:** Actividades, Casos de Uso, Estado.
- **De interacción:** Comunicación, Secuencia, Tiempos.

De todos estos, **el diagrama de clases es, por lejos, el más desarrollado en tu material y el más probable en tu examen.**

**Cómo se dibuja una clase:** un rectángulo dividido en 3 compartimentos:

```
┌──────────────────────────────┐
│          Bicicleta           │  ← Nombre de la clase (singular, mayúscula inicial)
├──────────────────────────────┤
│ + marca: String               │
│ + tamañoMarco: float          │  ← Atributos: [visibilidad] nombre: Tipo
│ - cambio: int                 │
├──────────────────────────────┤
│ + Bicicleta()                 │
│ + subirUnCambio(): int         │  ← Métodos: [visibilidad] nombre(parámetros): TipoRetorno
│ + acelerar(velFinal: int): void│
└──────────────────────────────┘
```

- **Notación compacta:** solo el nombre de la clase (útil cuando el diagrama tiene muchas clases relacionadas y no necesitas ver el detalle de cada una).
- **Notación extendida:** los 3 compartimentos completos, con atributos y métodos.
- **Símbolos de visibilidad:** `+` público, `-` privado, `#` protegido, `~` (o sin símbolo) paquete/interno. Se explican a fondo en el Tema 1.5.
- **Atributos:** `nombre: Tipo` (sustantivo descriptivo).
- **Métodos:** `nombre(parámetros): TipoRetorno` (verbo).

#### Ejemplos

Clase `Bicicleta` completa en notación extendida (de tu propio material):

```
┌───────────────────────────────────────┐
│              Bicicleta                 │
├───────────────────────────────────────┤
│ + marca: String                        │
│ + tamañoMarco: float                   │
│ + tamañoLlanta: float                  │
│ + material: String                     │
│ + numeroCambios: int                   │
│ + color: String                        │
│ - cambio: int                          │
│ - velocidad: int                       │
├───────────────────────────────────────┤
│ + Bicicleta()                          │
│ + Bicicleta(marca, marco, ...): void   │
│ + subirUnCambio(): int                 │
│ + bajarUnCambio(): int                 │
│ + acelerar(velFinal: int): void        │
│ + desacelerar(velFinal: int): void     │
└───────────────────────────────────────┘
```

**Java** (traducción directa del diagrama):
```java
public class Bicicleta {
    private String marca;
    private float tamañoMarco;
    private float tamañoLlanta;
    private String material;
    private int numeroCambios;
    private String color;
    private int cambio;
    private int velocidad;

    public Bicicleta() { }
    public Bicicleta(String marca, float marco, float llanta, String material, int cambios, String color) {
        this.marca = marca; this.tamañoMarco = marco; this.tamañoLlanta = llanta;
        this.material = material; this.numeroCambios = cambios; this.color = color;
    }
    public int subirUnCambio() { cambio++; return cambio; }
    public int bajarUnCambio() { cambio--; return cambio; }
    public void acelerar(int velFinal) { velocidad = velFinal; }
    public void desacelerar(int velFinal) { velocidad = velFinal; }
}
```

**C#** (como aparece literalmente en tu deck):
```csharp
public class Bicicleta {
    public string Marca { get; set; }
    public float TamañoMarco { get; set; }
    public float TamañoLlanta { get; set; }
    public string Material { get; set; }
    public int NumeroCambios { get; set; }
    public string Color { get; set; }
    private int cambio;
    private int velocidad;

    public Bicicleta() { }
    public Bicicleta(string marca, float marco, float llanta, string material, int cambios, string color) {
        Marca = marca; TamañoMarco = marco; TamañoLlanta = llanta;
        Material = material; NumeroCambios = cambios; Color = color;
    }
    public int SubirUnCambio() => ++cambio;
    public int BajarUnCambio() => --cambio;
    public void Acelerar(int velFinal) => velocidad = velFinal;
    public void Desacelerar(int velFinal) => velocidad = velFinal;
}
```

#### Errores comunes
- Escribir los atributos como verbos y los métodos como sustantivos (al revés de la convención).
- Omitir el tipo de retorno de un método, incluso cuando es `void` — en UML **siempre** se especifica.
- Usar notación extendida en diagramas grandes donde solo interesa ver las relaciones (ahí es preferible la compacta, para no saturar visualmente).

#### Relaciones
El diagrama de clases es el "contenedor visual" donde luego (Temas 1.9, 1.10 y todo el Día 2) dibujaremos las relaciones y la herencia. Sin dominar cómo se ve una clase sola, es imposible entender un diagrama con 5 clases relacionadas.

#### Resumen
- UML: lenguaje visual para diseñar antes de programar.
- Diagrama de clases: 3 compartimentos (nombre / atributos / métodos).
- Notación compacta (solo nombre) vs. extendida (todo el detalle).
- `+` público, `-` privado, `#` protegido.

#### Ejercicios
1. Dibuja (en texto/ASCII) el diagrama de clases en notación extendida para una clase `Libro` con atributos `titulo` (público), `precio` (privado) y métodos `calcularDescuento(porcentaje): double` (público).
2. **(V/F)** La notación compacta muestra atributos y métodos. → Falso, solo el nombre.
3. Traduce tu diagrama del ejercicio 1 a código Java y a código C#.


## Tema 1.5 — Accesibilidad / Ocultamiento (Modificadores de Acceso)

#### Explicación

**Intuición.** Piensa en un cajero automático (ATM). Tú puedes pedir dinero, consultar tu saldo, cambiar tu clave — esas son las operaciones "públicas" que el banco te expone. Pero no puedes abrir la máquina y tocar el mecanismo interno que cuenta los billetes: eso está "oculto" para protegerlo de mal uso o de que se dañe. Esa protección voluntaria es exactamente lo que hace la **accesibilidad/ocultamiento** en POO.

**Definición formal:** el ocultamiento protege el acceso a los componentes de una clase (atributos y métodos) y contribuye directamente al **encapsulamiento**. Se implementa mediante **modificadores de acceso**.

| Modificador | Símbolo UML | Quién puede acceder |
|---|---|---|
| **Público** (`public`) | `+` | Cualquier clase, desde cualquier parte del programa |
| **Privado** (`private`) | `-` | Solo la propia clase |
| **Protegido** (`protected`) | `#` | La propia clase y sus subclases (herencia — Día 2) |
| **Paquete/interno** (sin modificador o `internal` en C#) | `~` | Solo clases en la misma ubicación (mismo paquete/ensamblado) |

**Pregunta clave para decidir la visibilidad de cada miembro** (viene textual de tu material): *¿Qué atributos de la clase son modificables desde otras clases? ¿Qué métodos son visibles para otras clases?* — la respuesta depende del problema, no hay una regla universal, pero **la buena práctica por defecto es: atributos privados, métodos según se necesiten (constructor y accesores públicos, lógica interna privada)**.

#### Ejemplos

**Java:**
```java
public class Bicicleta {
    public String marca;       // + público: cualquiera puede leerlo/escribirlo directamente
    private int cambio;        // - privado: solo Bicicleta puede tocarlo directamente
    protected int velocidadMax; // # protegido: Bicicleta y sus subclases

    // método público de la interfaz "hacia afuera"
    public int subirUnCambio() {
        cambio++;             // acceso interno permitido
        return cambio;
    }
}
```

**C#:**
```csharp
public class Bicicleta {
    public string Marca;
    private int cambio;
    protected int velocidadMax;

    public int SubirUnCambio() => ++cambio;
}
```

#### Errores comunes
- Hacer **todos** los atributos públicos "para no complicarse". Esto rompe el encapsulamiento: cualquier clase externa podría poner el atributo `cambio` en `-500`, un valor sin sentido, sin pasar por ninguna validación.
- Confundir `protected` con `private`: una subclase **sí** puede acceder a miembros `protected` de su superclase, pero **no** a los `private` (ver Tema 2.1, Herencia).
- Olvidar que en C# el modificador por defecto (sin escribir nada) para miembros de clase es `private`, mientras que para la clase misma (a nivel de archivo) es `internal`.

#### Relaciones
La accesibilidad es la base técnica del **Encapsulamiento** (uno de los 4 pilares clásicos de POO, que veremos formalmente completado en el Tema 1.7 junto con los accesores). También es la base de por qué existen los **accesores get/set**: si todo fuera público, no necesitarías un método `get`/`set` — simplemente accederías al atributo directo. Además, será clave en Herencia (protected) y en SOLID (el ISP y el DIP dependen de exponer solo lo necesario).

#### Resumen
- 4 niveles de acceso: público (+), privado (-), protegido (#), paquete/interno (~).
- Regla de oro: atributos privados por defecto, exponer solo lo estrictamente necesario.
- La visibilidad depende del problema, no hay receta universal — pero la buena práctica es "todo cerrado excepto lo que necesita estar abierto".

#### Ejercicios
1. **(V/F)** Un miembro protegido puede ser usado por cualquier clase del programa. → Falso, solo por la propia clase y sus subclases.
2. Diseña la visibilidad correcta para una clase `CuentaBancaria` con atributos `saldo`, `numeroCuenta`, `titular` y un método `retirar(monto)`.
3. **(Selección múltiple)** ¿Cuál es la mejor práctica por defecto? a) todos los atributos públicos b) todos privados y exponer solo lo necesario c) todo protegido d) no aplica ninguna regla → **(b)**

---

## Tema 1.6 — Constructores

#### Explicación

**Intuición.** Cuando compras un carro de fábrica, existe una línea de ensamblaje que garantiza que el carro nunca sale de la planta sin motor, sin llantas, sin volante. El **constructor** es esa línea de ensamblaje: garantiza que un objeto nunca "nazca" en un estado incompleto o inválido.

**Definición formal (textual de tu material):**
- Es un **método** que se crea con el **mismo nombre de la clase**.
- Es **obligatorio** para instanciar (crear) objetos de una clase.
- **No tiene tipo de retorno**, ni siquiera `void`.
- Su función es permitir la creación de un nuevo objeto, cuando se usa la palabra reservada `new`.
- Se pueden declarar **varios constructores** para una misma clase — a esto se le llama **sobrecarga** (overload), tema que profundizamos en el Día 2, pero que ya empiezas a ver aquí.

En el constructor puedes:
- Asignar valores a los atributos (todos o solo algunos).
- Validar la información antes de asignarla (si los atributos no son públicos — lo normal).

> 🔑 Tu material remarca explícitamente: "los valores para inicializar se pueden enviar como parámetros al constructor **o se pueden leer dentro del método** (no se recomienda, porque vamos a evitar escribir/leer directamente en los métodos de negocio)". Esto es una buena práctica importante: separar la lógica de construcción de la lógica de entrada/salida (I/O).

#### Ejemplos

**Java** — dos constructores sobrecargados:
```java
public class Bicicleta {
    private String marca;
    private float tamañoMarco;

    // Constructor vacío (constructor por defecto)
    public Bicicleta() { }

    // Constructor con parámetros
    public Bicicleta(String marca, float tamañoMarco) {
        this.marca = marca;             // this distingue el atributo del parámetro
        this.tamañoMarco = tamañoMarco;
    }
}
```

**C#** (idéntico, tal como en tu deck):
```csharp
public class Bicicleta {
    public string Marca { get; set; }
    public float TamañoMarco { get; set; }

    public Bicicleta() { }

    public Bicicleta(string marca, float tamañoMarco) {
        Marca = marca;
        TamañoMarco = tamañoMarco;
    }
}
```

**La palabra `this`** — usos exactos que pide tu material:
1. Cuando el nombre del atributo es igual al del parámetro y hay que diferenciarlos: `this.color = color;` (el primero es el atributo, el segundo el parámetro).
2. Para acceder explícitamente a un atributo del objeto actual: `return this.cambio;`

#### Errores comunes
- Ponerle un tipo de retorno al constructor (incluso `void`) — deja de ser un constructor y se convierte en un método normal que "coincidentemente" se llama igual que la clase (esto compila en algunos lenguajes pero es un error conceptual grave).
- Leer datos con `Console.ReadLine()`/`Scanner` **dentro** del constructor como práctica habitual — tu propio material lo desaconseja porque mezcla responsabilidades (I/O con construcción de objetos). Es un anticipo directo del **SRP** que verás el Día 4.
- Olvidar `this` cuando el parámetro se llama igual que el atributo, causando que el atributo nunca se actualice (el compilador no marca error, pero el bug es real: el parámetro "se asigna a sí mismo").
- Pensar que un objeto puede existir sin haber pasado por ningún constructor — es imposible en C#/Java, siempre se ejecuta un constructor (si no escribes ninguno, el compilador genera uno vacío automáticamente, el "constructor por defecto").

#### Relaciones
El constructor es el punto de entrada de la **Instanciación** (Tema 1.8). Su necesidad de "no tener I/O dentro" es el primer indicio, muy temprano en el curso, de por qué el **Principio de Responsabilidad Única (SRP)** existirá más adelante: ya en el Día 1 se te enseña a no mezclar responsabilidades.

#### Resumen
- Constructor: mismo nombre que la clase, sin tipo de retorno, obligatorio, se ejecuta con `new`.
- Puede haber varios (sobrecarga).
- `this` desambigua atributo vs. parámetro, o accede explícitamente al objeto actual.
- Evita hacer I/O dentro del constructor.

#### Ejercicios
1. Escribe en Java un constructor sobrecargado (vacío + con parámetros) para una clase `Estudiante(nombre, edad)`.
2. **(V/F)** Un constructor puede retornar `void` explícitamente. → Falso.
3. **(Trampa)** Un compañero escribe: `public Bicicleta(String marca) { marca = marca; }`. ¿Qué pasa y cómo lo arreglas? *(El parámetro se asigna a sí mismo; el atributo de la clase nunca cambia. Se arregla con `this.marca = marca;`.)*

---

## Tema 1.7 — Accesores (get/set) y Encapsulamiento

#### Explicación

**Intuición.** Sigamos con el cajero automático: tú no metes la mano directamente a la bóveda para tomar dinero (eso sería un atributo público sin protección). En cambio, usas un "canal controlado" — el cajero valida que tengas fondos, que tu clave sea correcta, y *entonces* te entrega el dinero. Ese canal controlado es el **accesor**.

**Definición formal:** los accesores ayudan a implementar protección sobre los atributos de una clase. Hay dos por cada atributo:
- **Set** (Modificar): cambia el valor del atributo — puede (y debe) **validar** antes de asignar.
- **Get** (Obtener): lee el valor del atributo — puede transformar el valor antes de entregarlo (ej. "entregar siempre en mayúsculas", como pide el ejercicio del Parque de Diversiones).

Se puede usar solo `get`, solo `set`, o ambos, según lo que el diseño necesite. Si otra clase quiere acceder a un atributo privado, **debe** hacerlo a través de los accesores — nunca accediendo directamente al campo privado (eso sería imposible de todas formas, por eso es privado).

Esto completa el pilar de **Encapsulamiento**: agrupar atributos + comportamiento bajo una misma unidad de programación, funcionando como una "caja negra" — se conoce el **qué** (los métodos públicos disponibles) pero no el **cómo** (la implementación interna).

#### Ejemplos

**Java** (accesor con validación, exactamente el patrón que pide el ejercicio de la Lámpara/Parque):
```java
public class Parque {
    private String nombre; // privado: protegido

    public void setNombre(String nombre) {
        if (nombre != null && nombre.length() > 8) {
            this.nombre = nombre.toUpperCase();   // se guarda siempre en mayúscula
        } else {
            throw new IllegalArgumentException("El nombre debe tener más de 8 caracteres");
        }
    }

    public String getNombre() {
        return this.nombre.toUpperCase();          // se entrega siempre en mayúscula
    }
}
```

**C#** (usando propiedades, la forma idiomática de .NET — equivalente exacto a get/set):
```csharp
public class Parque {
    private string nombre;

    public string Nombre {
        get => nombre.ToUpper();
        set {
            if (value != null && value.Length > 8)
                nombre = value.ToUpper();
            else
                throw new ArgumentException("El nombre debe tener más de 8 caracteres");
        }
    }
}
```
> Nota: en C#, la propiedad automática `public string Nombre { get; set; }` (sin validación) es un atajo cuando NO necesitas validar. En cuanto necesitas validar (como aquí), se expande a la forma completa con campo privado de respaldo (`nombre`), tal como en tu deck ("Accesor público con validación").

#### Errores comunes
- Crear un `get` y un `set` "por costumbre" para **todos** los atributos, incluso los de estado que solo deberían cambiar internamente (rompe el principio de exponer solo lo necesario).
- Poner la validación en el lugar equivocado: validar en el `Main`/controlador en lugar de en el `set` — esto permite que otras partes del código "se olviden" de validar y corrompan el objeto.
- Olvidar que un `get` también puede (y a veces debe) transformar el dato antes de entregarlo (no es un simple "devolver tal cual").

#### Relaciones
Los accesores son el **mecanismo concreto** que implementa la Accesibilidad (Tema 1.5) sobre los Atributos (Tema 1.3), y son el segundo gran anticipo del Encapsulamiento como pilar de POO (junto con Herencia, Polimorfismo y Abstracción, que veremos formalmente reunidos al cierre del Día 2).

#### Resumen
- `get` lee (y puede transformar); `set` escribe (y debe validar).
- El encapsulamiento = atributos protegidos + comportamiento agrupado, expuesto como "caja negra".
- Acceso externo a un atributo privado SIEMPRE pasa por el accesor correspondiente.

#### Ejercicios
1. Implementa en C# la propiedad `Marca` de una `Lampara`, que debe guardarse siempre en mayúscula, ser mayor de 6 caracteres y no nula ni en blanco (ejercicio real de tu curso).
2. **(V/F)** Un atributo puede tener `get` sin tener `set`. → Verdadero (atributo de solo lectura).
3. **(Razonamiento)** ¿Por qué la validación debe ir en el `set` y no en el programa principal que usa la clase?

## Tema 1.8 — Instanciación

#### Explicación

**Intuición.** Si la clase `Homero Simpson` es el plano, cada clon de Homero que ves en pantalla es una instancia: comparten el mismo "molde" (mismos atributos posibles, mismos comportamientos posibles), pero cada uno tiene su propio estado independiente en memoria — si un clon se pinta de azul, los demás siguen amarillos.

**Definición formal:** instanciar es el proceso mediante el cual se crean objetos de una misma clase. Cada objeto instanciado:
- Es **independiente** de los demás objetos de la misma clase.
- Tiene una **representación propia en memoria**.
- Toma **todas** las propiedades definidas por la clase (su "forma" es igual, sus "valores" pueden diferir).
- Se le puede: asignar valores a sus atributos y ejecutar sus operaciones.

**Sintaxis:** `NombreClase nombreObjeto = new NombreClase(parámetros);` — el constructor invocado determina qué parámetros hay que pasar (o ninguno, si usas el constructor vacío).

#### Ejemplos

**Java:**
```java
Bicicleta biciDeJuan = new Bicicleta("Trek", 54.0f, 26.0f, "Aluminio", 21, "Rojo");
Bicicleta biciDeAna  = new Bicicleta("Giant", 50.0f, 24.0f, "Carbono", 18, "Azul");
// Ambos objetos son independientes: cambiar biciDeJuan no afecta a biciDeAna
biciDeJuan.acelerar(30);
```

**C#:**
```csharp
Bicicleta biciDeJuan = new Bicicleta("Trek", 54.0f, 26.0f, "Aluminio", 21, "Rojo");
Bicicleta biciDeAna  = new Bicicleta("Giant", 50.0f, 24.0f, "Carbono", 18, "Azul");
biciDeJuan.Acelerar(30);
```

#### Errores comunes
- Creer que dos objetos instanciados de la misma clase "comparten" sus atributos de dato/estado — **no** es así (los atributos *estáticos* sí se comparten, pero eso es una excepción especial que veremos en el Tema 2.2).
- Confundir la variable de referencia (`biciDeJuan`) con el objeto en sí — la variable es solo una "etiqueta" que apunta al objeto real en memoria.
- Intentar instanciar una clase abstracta directamente (`new Automovil()` si `Automovil` es abstracta) — error de compilación; se profundiza en el Tema 2.1.

#### Relaciones
Este tema cierra el "ciclo de vida básico" de un objeto: Clase (plano) → Constructor (línea de ensamblaje) → Instanciación (el objeto ya existe en memoria) → Accesores (interacción controlada con ese objeto). Es también el primer punto donde puedes empezar a construir el **Proyecto Integrador** (ver más abajo, versión Día 1).

#### Resumen
- Instanciar = crear un objeto concreto a partir de una clase, con `new`.
- Cada objeto es independiente en memoria.
- Toma la "forma" de la clase, pero sus valores pueden diferir de otros objetos de la misma clase.

#### Ejercicios
1. Instancia en C# dos objetos de tu clase `Lampara` del Tema 1.7, con marcas distintas, y demuestra que son independientes cambiando el estado de uno solo.
2. **(V/F)** Todos los objetos de una misma clase deben tener siempre los mismos valores de atributos. → Falso.

#### 🧪 Mini examen — Bloques 3 y 4 del Día 1 (Accesibilidad, Constructores, Accesores, Instanciación)
*No mires las respuestas hasta intentarlo.*
1. ¿Qué diferencia hay entre `private` y `protected`?
2. ¿Por qué un constructor no puede tener tipo de retorno?
3. Explica qué hace `this` en dos contextos distintos.
4. ¿Qué validación harías en el `set` de un atributo `Edad` de una clase `Persona`, y por qué no en el `Main`?
5. Da un ejemplo de dos objetos de la misma clase con estado distinto y explica por qué eso es posible.


## Tema 1.9 — UML: Relación de Asociación (Multiplicidad, Navegabilidad, Rol, Clase de Asociación)

> A partir de aquí empezamos el tratamiento especial de UML que pediste: intuición antes que símbolos, y una decisión razonada antes que memorización.

### 1. Intuición

Piensa en estos ejemplos de la vida real:
- Una **Persona** *posee* un **Automóvil**.
- Un **Estudiante** *está matriculado en* una **Universidad**.
- Un **Médico** *atiende* a un **Paciente**.

En los tres casos hay **dos objetos que existen de forma independiente**, pero que en algún momento se relacionan porque uno **usa, conoce o interactúa** con el otro. Ninguno de los dos "vive dentro" del otro — si la Persona deja de existir, el Automóvil sigue existiendo (quizás lo hereda alguien más). Esa relación de "conocer/usar sin poseer" es la **Asociación**.

### 2. Definición formal

Una **asociación** es una relación estructural entre dos clases donde un objeto **usa o interactúa** con otro. Se usa para representar que una clase tiene, como atributo, una referencia a otra clase.

### 3. Cómo reconocerla — preguntas guía

- ¿Puede existir A sin B, y B sin A, de forma completamente independiente? → Sí ⇒ probablemente Asociación.
- ¿Quién "ve" a quién (quién guarda una referencia hacia el otro)? Eso determina la **navegabilidad**.
- ¿Es una relación permanente y estructural (no un simple préstamo momentáneo)? Si la respuesta es sí, es Asociación (no Dependencia, que veremos en el Tema 1.11 y es más débil/temporal).

### 4. Cómo dibujarla — símbolo, flecha, multiplicidad, dirección

```
Persona  ────────────────  Automovil
              Posee
```

- **Línea continua simple** entre las dos clases (sin rombos, sin triángulos).
- Un verbo o etiqueta sobre la línea que describe la relación (ej. "Posee").
- **Multiplicidad**: un número o rango en cada extremo de la línea, indicando cuántos objetos de esa clase participan en la relación:

| Símbolo | Significado |
|---|---|
| `1` | Uno a uno |
| `0..1` | Cero o uno |
| `1..n` | De 1 a n |
| `0..*` | De cero a muchos |
| `1..*` | De uno a muchos |
| `2` | Exactamente dos (o cualquier entero positivo fijo) |
| `5..11` | De 5 a 11 |
| `5, 11` | Cinco u once (uno de los dos, no un rango continuo) |

```
Persona  1 ────────Posee──────── 0..*  Automovil
```
*(Se lee: "una Persona posee entre 0 y muchos Automóviles".)*

- **Navegabilidad**: una flecha `>` hacia la clase que es "vista" (la que se convierte en atributo de la otra). Si Persona "ve" a Automóvil, la flecha apunta hacia Automóvil, y `Automóvil` se convierte en un atributo (lista) dentro de `Persona`.

```
Persona                                    Automovil
 - id: String                    1  Posee  0..*
 - nombre: String        ───────────────────────>
 - l_automoviles: Automovil[]
```

- **Rol de asociación**: el nombre que recibe ese atributo generado por la navegabilidad (`l_automoviles` en el ejemplo).
- **Clase de asociación**: a veces la relación en sí misma tiene datos propios que no pertenecen ni a A ni a B. Se dibuja como una clase adicional conectada por una **línea punteada** al medio de la línea de asociación. Ejemplo de tu material: la relación "Persona posee Automóvil" puede tener una clase adicional `TarjetaPropiedad` con atributos propios de esa relación (fecha de traspaso, notaría, etc.) que no son ni de Persona ni de Automóvil.

```
Persona  1 ──Posee── 0..*  Automovil
                │
           (línea punteada)
                │
        TarjetaPropiedad
```

### 5. Cómo se implementa en Java (Diagrama → Código → Diseño)

```java
public class Persona {
    private String id;
    private String nombre;
    private List<Automovil> lAutomoviles; // <- rol de asociación, generado por la navegabilidad

    public Persona(String id, String nombre) {
        this.id = id;
        this.nombre = nombre;
        this.lAutomoviles = new ArrayList<>();
    }

    public void agregarAutomovil(Automovil a) { lAutomoviles.add(a); }
}

public class Automovil {
    private String placa;
    // Automovil NO tiene referencia a Persona: la navegabilidad es unidireccional (Persona -> Automovil)
}
```

**C#:**
```csharp
public class Persona {
    public string Id { get; set; }
    public string Nombre { get; set; }
    public List<Automovil> LAutomoviles { get; set; } = new List<Automovil>();
}

public class Automovil {
    public string Placa { get; set; }
}
```

### 6. Errores comunes

- Confundir Asociación con Herencia porque ambas se dibujan con líneas: la Asociación **nunca** lleva triángulo, y no implica "ES-UN", implica "TIENE UNA REFERENCIA A" o "USA A".
- Dibujar la flecha de navegabilidad al revés: la flecha apunta hacia la clase que se **convierte en atributo**, no hacia la clase que "actúa". (En "Persona posee Automóvil", es Persona quien tiene la lista de autos, por eso la flecha va desde Persona *hacia* Automóvil.)
- Olvidar la multiplicidad — un diagrama de asociación sin multiplicidad está incompleto para efectos de examen.
- Confundir Rol de Asociación con el nombre de la relación (la etiqueta "Posee" describe la relación en general; "l_automoviles" es el nombre específico del atributo generado en un extremo).

### 7. Comparaciones

- **Asociación vs. Dependencia:** la Asociación es una relación *permanente/estructural* (se guarda como atributo); la Dependencia es *temporal* (normalmente un parámetro de un método, se usa y se olvida). Ver Tema 1.11.
- **Asociación vs. Agregación/Composición:** la Asociación es la relación *más débil* de la familia "una clase conoce a otra"; Agregación y Composición (Tema 1.10) son asociaciones **especiales** con semántica de "Todo-Partes" y un acoplamiento más fuerte.

### 8. Ejercicio visual (piensa antes de bajar a la corrección)

Escenario: **Sistema de Biblioteca**. Un `Libro` puede ser prestado a varios `Usuario`s a lo largo del tiempo, pero en un momento dado, un libro está prestado a un único usuario (o a ninguno). Un `Usuario` puede tener varios libros prestados simultáneamente.

**Pregunta:** ¿Qué multiplicidad pondrías en cada extremo de la relación `Usuario` — `Libro`? ¿Hacia dónde pondrías la navegabilidad?

### 9. Corrección (razonada, no solo la respuesta)

Multiplicidad correcta: `Usuario (1) ── PrestaA ── (0..*) Libro` **en el sentido de "en un momento dado"** — un usuario puede tener 0 a muchos libros prestados *ahora mismo*; un libro está prestado a 0 o 1 usuario *ahora mismo* (`0..1` del lado de Usuario visto desde Libro). La navegabilidad más natural es `Usuario -> Libro` (el sistema típicamente pregunta "¿qué libros tiene prestados este usuario?" más que al revés), por lo que `Usuario` tendría el atributo `List<Libro> librosPrestados`.

> Si pusiste la multiplicidad como si un libro pudiera estar prestado a varios usuarios *a la vez* (`Libro 0..* — Usuario`), revisa: eso solo sería correcto si hablamos del **historial** de préstamos (una clase de asociación `Prestamo` con fecha), no del préstamo actual.

### 10. Casos difíciles (ambigüedad real)

- **Un `Profesor` "dicta" un `Curso`.** ¿Es asociación simple o hay algo más? Si solo necesitas saber qué profesor dicta qué curso, es asociación simple. Pero si necesitas guardar **cuándo empezó a dictarlo, en qué salón, en qué horario** — esos datos no pertenecen ni a `Profesor` ni a `Curso` exclusivamente: necesitas una **clase de asociación** (`Dictado`).
- **Un `Cliente` "compra" un `Producto`.** Parece asociación simple, pero casi cualquier sistema real necesita guardar cantidad, fecha, precio pagado (que puede diferir del precio actual del producto) — eso apunta, de nuevo, a una clase de asociación (`ItemCompra` o `DetalleVenta`).

### 11. Reglas prácticas — Cómo decidir rápidamente

> - Si una clase **usa o conoce** a otra de forma permanente (se guarda como atributo) → probablemente es **Asociación**.
> - Si además la relación en sí misma tiene **datos propios** que no pertenecen a ninguna de las dos clases → agrega una **Clase de Asociación**.
> - Si no sabes hacia dónde va la flecha de navegabilidad, pregúntate: **¿quién necesita "preguntarle" al otro?** Esa clase es la que tiene el atributo (rol), y la flecha apunta hacia la clase "vista".

### 12. (El ejercicio final de 50 casos de análisis de UML está consolidado al cierre del Día 2, Tema 2.11, cuando ya conoces TODAS las relaciones — así puedes practicar decidiendo entre todas ellas, que es la verdadera dificultad de examen.)

---

## Tema 1.10 — UML: Agregación y Composición (relaciones Todo-Partes)

### 1. Intuición

- **Agregación** — piensa en un **Equipo de fútbol** y sus **Jugadores**. El equipo está compuesto por jugadores, pero si el equipo se disuelve, los jugadores **siguen existiendo** (pueden fichar por otro equipo). Las partes tienen vida propia fuera del todo.
- **Composición** — piensa en una **Casa** y sus **Habitaciones**. Si la casa se demuele, las habitaciones **dejan de existir** — no tiene sentido que una "habitación" exista flotando sin la casa a la que pertenece. Las partes NO tienen vida propia fuera del todo.

Ambas son relaciones "Todo-Partes" ("es parte de"), pero la diferencia está en **qué tan fuerte es el vínculo de existencia**.

### 2. Definición formal

- **Agregación:** acoplamiento más fuerte que la asociación simple. Una clase representa el **Todo**, las demás las **Partes**. La(s) Parte(s) **pueden existir independientemente** del Todo.
- **Composición:** un tipo especial (más restrictivo) de agregación. Cada parte pertenece **a un único todo** y no tiene sentido fuera de él. Si el objeto completo se borra o se copia, sus partes se borran o se copian con él.

### 3. Cómo reconocerla — preguntas guía (las de tus instrucciones)

- **"¿Puede existir A sin B?"** → Si la Parte puede existir sin el Todo, es **Agregación**. Si no puede (deja de tener sentido), es **Composición**.
- **"¿Quién crea a quién?"** → En Composición, normalmente el Todo **crea** sus partes en su propio constructor (como en tu ejemplo del Parque, que "carga automáticamente la lista con 1000 manillas" en su constructor).
- **"¿Quién es dueño de quién?"** → El Todo es "dueño" en ambos casos, pero en Composición la propiedad es **exclusiva** (una Parte no puede pertenecer a dos Todos simultáneamente).

### 4. Cómo dibujarla — símbolo, rombo, dirección

```
Agregación:                          Composición:
  ClaseTodo ◇──────── ClasePartes       ClaseTodo ◆──────── ClasePartes
  (rombo BLANCO/hueco en el Todo)       (rombo RELLENO/sólido en el Todo)
```

Ejemplo real de tu material (Álbum de figuritas):
```
     Album  ◆────1───670───  Lamina
       │
       ◆
       1
       │
     Tapa
```
*(El álbum "es compuesto por" 1 tapa y hasta 670 láminas — si el álbum se destruye, tapa y láminas dejan de tener sentido como partes de ESE álbum.)*

Ejemplo de Agregación (Todo-Partes con vida propia):
```
    ClaseTodo
        │◇
    ┌───┼───┐
ClaseParte ClaseParte ClaseParte
```

### 5. Cómo se implementa en Java

**Agregación** (las partes se pasan/asignan desde afuera, tienen vida propia):
```java
public class Jugador {
    private String nombre;
    public Jugador(String nombre) { this.nombre = nombre; }
}

public class Equipo {
    private List<Jugador> jugadores = new ArrayList<>();

    // El jugador YA EXISTÍA antes de entrar al equipo, y seguirá existiendo si lo saco
    public void ficharJugador(Jugador j) { jugadores.add(j); }
    public void liberarJugador(Jugador j) { jugadores.remove(j); } // j sigue vivo fuera del equipo
}
```

**Composición** (el Todo crea y controla el ciclo de vida completo de sus partes):
```java
public class Habitacion {
    private double metrosCuadrados;
    Habitacion(double m2) { this.metrosCuadrados = m2; }
}

public class Casa {
    private List<Habitacion> habitaciones = new ArrayList<>();

    public Casa(int numHabitaciones) {
        // La Casa CREA sus propias habitaciones en su constructor: no existen fuera de ella
        for (int i = 0; i < numHabitaciones; i++) {
            habitaciones.add(new Habitacion(20.0));
        }
    }
    // No hay un método "sacarHabitacion()" que la deje viva fuera de la casa
}
```

**C# (Composición, ejemplo del Parque, fiel a tu material):**
```csharp
public class Manilla {
    public string Id { get; private set; }
    public int SaldoPuntos { get; private set; } = 0;
    public Manilla() { Id = Guid.NewGuid().ToString(); }
}

public class Parque {
    private List<Manilla> manillas = new List<Manilla>();

    public Parque() {
        // El Parque CREA sus 1000 manillas en su propio constructor (composición)
        for (int i = 0; i < 1000; i++) manillas.Add(new Manilla());
    }
}
```

### 6. Errores comunes

- Usar Composición cuando en realidad las partes tienen vida independiente (ej. modelar `Empleado` como composición de `Empresa` — un empleado sigue existiendo, como persona, si lo despiden; ahí lo correcto es Agregación o incluso solo Asociación).
- Confundir el rombo blanco (agregación) con el relleno (composición) al dibujar — es un error puramente de notación pero muy penalizado en examen.
- Pensar que Composición implica que las partes se crean **necesariamente** en el constructor del Todo — en general así es lo más común y semánticamente más limpio, pero lo esencial de la definición es **el ciclo de vida atado**, no el momento exacto de creación.
- **El caso real que tu propio profesor corrige en el deck de Herencia**: en el ejercicio del Concesionario, una `Venta` tiene un `Cliente`, un `Vendedor` y un `Automovil`. Un estudiante podría pensar "Venta es composición de Cliente" — pero **es incorrecto**, porque el Cliente sigue existiendo si se borra la venta. La relación correcta es: `Venta` tiene **Agregación** (o simple Asociación) hacia `Cliente`, `Vendedor` y `Automovil` (todos siguen vivos fuera de la venta), mientras que `Concesionario` sí tiene **Composición** hacia `Venta` (si el concesionario cierra, sus ventas históricas como registros internos dejan de tener sentido como entidad independiente en ese sistema).

### 7. Comparaciones

| | Asociación | Agregación | Composición |
|---|---|---|---|
| Rombo | Ninguno | Blanco/hueco | Relleno/sólido |
| ¿La parte vive sin el todo? | N/A (no hay "todo") | Sí | No |
| Fuerza del acoplamiento | Más débil | Media | Más fuerte |
| ¿Quién crea a la parte? | Cualquiera | Externo, normalmente | El propio Todo |
| Ejemplo | Persona-Automóvil | Equipo-Jugador | Casa-Habitación |

### 8. Ejercicio visual

Escenario: **Sistema de Hospital**. Un `Hospital` tiene varios `Departamento`s (Cardiología, Pediatría...). Un `Departamento` tiene varios `Medico`s asignados.

**Pregunta:** ¿La relación Hospital-Departamento es agregación o composición? ¿Y Departamento-Médico?

### 9. Corrección

- **Hospital-Departamento → Composición.** Un "Departamento de Cardiología" solo tiene sentido como parte de ESE hospital específico; si el hospital cierra, ese departamento (como entidad organizacional) deja de existir.
- **Departamento-Médico → Agregación (o incluso solo Asociación).** Un médico sigue existiendo como persona/profesional si lo trasladan a otro departamento o renuncia — su existencia no depende del departamento.

### 10. Casos difíciles

- **`Universidad` y `Facultad`**: parece composición (si la universidad cierra, la facultad como entidad administrativa deja de existir), pero si el modelo de negocio permite que una Facultad se "traspase" a otra universidad (fusiones), entonces sería más correcto modelarlo como Agregación. **La respuesta correcta depende del enunciado del problema, no hay una única verdad universal** — esto es intencional en tu examen: debes justificar tu elección con el contrato de negocio dado, no con "sentido común" genérico.
- **`Pedido` y `LineaDePedido`**: casi siempre Composición (una línea de pedido sin su pedido no tiene sentido ni identidad), pero si el sistema requiere "mover una línea de un pedido a otro" (poco común, pero existe en sistemas de facturación complejos), técnicamente dejaría de ser composición pura.

### 11. Reglas prácticas — Cómo decidir rápidamente

> - Si un objeto puede existir sin el otro… → probablemente **NO es composición** (revisa si es agregación o asociación simple).
> - Si una clase **almacena permanentemente** otra como parte de su estructura interna, pero esa parte tiene sentido por sí sola → **Agregación**.
> - Si el Todo **crea y destruye** sus partes, y las partes no tienen identidad fuera de él → **Composición**.

### 12. (Ejercicio final consolidado al cierre del Día 2.)


## Tema 1.11 — UML: Dependencia

### 1. Intuición

Piensa en un **Mesero** que necesita una **Calculadora** para calcular la propina de una mesa. El mesero **usa** la calculadora un momento, para una tarea puntual, y luego la suelta — no la carga consigo como parte de su "equipo permanente" (eso sería asociación). Mañana podría usar otra calculadora distinta, o ninguna. Esa relación de "uso momentáneo, no permanente" es la **Dependencia**.

### 2. Definición formal

La dependencia es un tipo de asociación **más débil**: significa que **no hay una relación permanente** entre las clases. Frecuentemente aparece cuando una clase recibe a otra como **parámetro de un método** (o crea una instancia local temporal dentro de un método), sin guardarla como atributo.

### 3. Cómo reconocerla

- ¿La clase B aparece **solo dentro de la firma o el cuerpo de un método** de A, sin ser un atributo de A? → Dependencia.
- ¿Si eliminas ese método, desaparece toda relación entre A y B? → Si sí, confirma que es Dependencia (no Asociación, que persistiría como atributo).

### 4. Cómo dibujarla

```
ClaseA - - - - - -> ClaseB     (línea PUNTEADA con flecha, a diferencia de la asociación que es línea CONTINUA)
```

### 5. Cómo se implementa en Java

```java
public class Impresora {
    // Impresora "depende" de Documento solo durante la ejecución de este método
    // NO tiene un atributo Documento — no lo guarda
    public void imprimir(Documento doc) {
        System.out.println("Imprimiendo: " + doc.getContenido());
    }
}
```
**C#:**
```csharp
public class Impresora {
    public void Imprimir(Documento doc) => Console.WriteLine($"Imprimiendo: {doc.Contenido}");
}
```

### 6. Errores comunes
- Confundir Dependencia con Asociación cuando un parámetro de método es en realidad una clase que **luego se guarda** como atributo dentro del objeto (ej. un constructor que recibe un objeto y lo asigna a `this.algo = parametro` — eso ya es Asociación, no Dependencia, porque se vuelve permanente).
- No reconocer la Dependencia por Inyección de Dependencias (Día 4, DIP): cuando una clase recibe una interfaz por el constructor y **sí** la guarda como atributo, en realidad es **Asociación hacia una interfaz** (más fuerte que dependencia), aunque coloquialmente se hable de "inyectar una dependencia".

### 7. Comparaciones
**Asociación vs. Dependencia** — la pregunta de examen más frecuente de esta familia:

| | Asociación | Dependencia |
|---|---|---|
| ¿Se guarda como atributo? | Sí | No (normalmente parámetro de método o variable local) |
| Duración de la relación | Permanente | Momentánea/puntual |
| Línea UML | Continua | Punteada |
| Ejemplo | `Persona` tiene `List<Automovil>` | `Impresora.imprimir(Documento doc)` |

### 8. Ejercicio visual
Una clase `ServicioDeCorreo` tiene un método `enviar(Mensaje m)`. `Mensaje` no se guarda en ningún atributo de `ServicioDeCorreo`. ¿Qué relación hay?

### 9. Corrección
Es **Dependencia**: `Mensaje` solo existe dentro del método `enviar`, no hay atributo permanente.

### 10. Casos difíciles
Una clase `Reporte` recibe un `ProveedorDeDatos` en su constructor y lo asigna a un atributo privado para usarlo en varios métodos. Aunque suena a "solo se usa para construir el reporte", **al guardarse como atributo, ya es Asociación**, no Dependencia — el hecho de que solo se use "una vez lógicamente" (para construir el reporte) no importa; lo que importa es si el vínculo persiste en el tiempo de vida del objeto.

### 11. Reglas prácticas
> - Si una clase solamente **usa** otra de forma temporal (parámetro de método, variable local) → probablemente es **Dependencia**.
> - Si esa misma clase pasa a **guardarla como atributo** → deja de ser dependencia y se vuelve **Asociación**.

### 12. (Consolidado al cierre del Día 2.)

---

### 🧪 Mini examen — Día 1 completo (no mires las respuestas hasta intentarlo)

1. Explica con tus propias palabras la diferencia entre Clase y Objeto.
2. ¿Cuáles son las 6 propiedades de un buen diseño OO vistas hoy?
3. Clasifica un atributo `saldo` de `CuentaBancaria` según su tipo (dato/estado/proyecto) y justifica.
4. Dibuja (en texto) el diagrama UML extendido de una clase `Producto` con: `nombre` (público), `precio` (privado, con validación >0), `stock` (privado), método `venderUnidad(cantidad): boolean`.
5. ¿Qué diferencia hay entre un constructor vacío y uno con parámetros? ¿Cuándo usarías cada uno?
6. Escribe en C# el accesor `Precio` con validación (no puede ser negativo).
7. Da un ejemplo de Asociación, uno de Agregación, uno de Composición y uno de Dependencia, DISTINTOS a los usados en este documento.
8. Un estudiante modela `Factura` con Composición hacia `Cliente`. ¿Está bien? Justifica.
9. ¿Cuál es la diferencia visual (símbolo) entre Agregación y Composición en UML?
10. En el ejemplo `Persona -> Automovil` con navegabilidad, ¿qué atributo se genera y en qué clase?


---

# 📅 DÍA 2 — Jerarquías: Herencia, Polimorfismo e Interfaces

## Tema 2.1 — Herencia

#### Explicación

**Intuición.** Piensa en una **Biblioteca**. Tienes `Libro`, `Revista`, `DVD` — todos son "Material Bibliográfico": todos tienen un título, un código, se pueden prestar y devolver. En lugar de escribir esas características tres veces (una por cada tipo), las escribes **una vez** en un concepto general (`MaterialBibliografico`) y dejas que `Libro`, `Revista` y `DVD` las **hereden** automáticamente, agregando solo lo que las hace especiales (`Libro` tiene `numeroPaginas`, `DVD` tiene `duracionMinutos`).

**Definición formal:** la herencia es la propiedad para (1) compartir atributos y métodos entre clases y (2) definir nuevas clases usando como base clases ya existentes.
- La clase que se hereda se llama **superclase** o **clase padre**.
- La clase que hereda se llama **subclase** o **clase derivada**.
- La subclase hereda los atributos y comportamientos **específicos** de la clase existente, y puede agregar los suyos propios.
- Contribuye a un software: fiable, comprensible, de bajo costo, adaptable y reutilizable.

**La relación ES-UN:** "Libro **es un** Material Bibliográfico". Esta es la prueba de sentido común más importante para saber si la herencia es correcta: **si no puedes decir naturalmente "X es un Y", probablemente no debería ser herencia.**

**Sintaxis (C#):**
```csharp
class NombreClaseDerivada : NombreClaseBase { }
class Libro : MaterialBibliografico { }
```
**Java:**
```java
class Libro extends MaterialBibliografico { }
```

**Tipos de herencia:**
| Tipo | Descripción | ¿Soportado? |
|---|---|---|
| **Simple** | Una sola clase base, una o más derivadas. | Sí, en C# y Java |
| **Múltiple** | Una clase deriva de **dos o más** clases base directamente. | **No** en C# ni en Java (sí en C++). Se simula con interfaces. |
| **Multinivel** | Una clase derivada sirve, a su vez, de base para otra derivada (A → B → C). | Sí, en C# y Java, sin límite de niveles |

**Clases abstractas:** no se pueden instanciar directamente; existen para servir de base a la herencia. Se marcan con la palabra `abstract`. En UML se escriben en **cursiva** (tanto el nombre de la clase como los métodos abstractos). Un método abstracto **debe** ser implementado en cada subclase concreta.

**Accesibilidad en la herencia:**
- Una subclase **NO puede** acceder a miembros `private` de su superclase.
- Por eso, para exponer algo a las subclases (pero no al resto del mundo), se usa `protected`.
- Los miembros `public` están disponibles para subclases y para todo el mundo.

#### Ejemplos

**Java:**
```java
public abstract class MaterialBibliografico {
    protected String titulo;
    protected String codigo;

    public MaterialBibliografico(String titulo, String codigo) {
        this.titulo = titulo;
        this.codigo = codigo;
    }

    public abstract void mostrarFicha(); // método abstracto: obligatorio implementarlo en subclases
}

public class Libro extends MaterialBibliografico {
    private int numeroPaginas;

    public Libro(String titulo, String codigo, int numeroPaginas) {
        super(titulo, codigo);           // invoca al constructor de la superclase
        this.numeroPaginas = numeroPaginas;
    }

    @Override
    public void mostrarFicha() {
        System.out.println(titulo + " - " + numeroPaginas + " páginas");
    }
}
```

**C# (equivalente exacto, sintaxis de tu curso):**
```csharp
public abstract class MaterialBibliografico {
    protected string titulo;
    protected string codigo;

    public MaterialBibliografico(string titulo, string codigo) {
        this.titulo = titulo;
        this.codigo = codigo;
    }

    public abstract void MostrarFicha();
}

public class Libro : MaterialBibliografico {
    private int numeroPaginas;

    public Libro(string titulo, string codigo, int numeroPaginas) : base(titulo, codigo) {
        this.numeroPaginas = numeroPaginas;
    }

    public override void MostrarFicha() =>
        Console.WriteLine($"{titulo} - {numeroPaginas} páginas");
}
```

**Herencia multinivel** (Vehículo → Automóvil → Deportivo):
```csharp
public abstract class Vehiculo { protected string placa; }
public abstract class Automovil : Vehiculo { protected int cilindraje; }
public class Deportivo : Automovil { public bool esCoupe; }
// Desde el Main, se invocan directamente las clases heredadas concretas:
Deportivo miAuto = new Deportivo();
```

#### Errores comunes
- Intentar **herencia múltiple de clases** en C#/Java (`class X : A, B` con A y B siendo clases) — no compila. La forma correcta de "heredar de dos cosas" es heredar de **una clase** y **implementar una o más interfaces**.
- Redefinir **atributos** en la subclase — tu material lo señala explícitamente: "los atributos no deberían ser redefinidos (esto daría a pensar que el hijo no es realmente un hijo de ese padre)". Si sientes esa necesidad, repiensa si la herencia es el modelo correcto.
- Intentar acceder desde una subclase a un miembro `private` de la superclase — no compila; hay que usar `protected`.
- Instanciar directamente una clase abstracta (`new MaterialBibliografico()`) — error de compilación.
- Aplicar herencia cuando la relación correcta era Composición/Agregación — el error de diseño más común y más preguntado en exámenes de POO: "Camioneta ES-UN Automóvil" (herencia, correcto) vs. "Motor ES-UN Automóvil" (falso — un motor no es un tipo de automóvil, un motor **es parte de** un automóvil → Composición).

#### Relaciones
La Herencia es el mecanismo que hace posible el **Polimorfismo de sobrescritura** (Tema 2.3) — sin una jerarquía de herencia, `override` no tiene sentido. También es la base del primer error de diseño que motiva el **Principio de Sustitución de Liskov (LSP)** en el Día 4: usar mal la herencia (forzar un "ES-UN" que en realidad no se cumple del todo) es exactamente lo que LSP viene a prevenir.

#### Resumen
- Herencia: compartir atributos/métodos + definir clases nuevas sobre clases existentes.
- Superclase/padre vs. Subclase/derivada; relación ES-UN.
- Simple, múltiple (no soportada), multinivel (sí soportada).
- Clases y métodos abstractos: no instanciables, itálica en UML, obligatorio implementar en subclases concretas.
- `protected` es el modificador clave para exponer algo solo a subclases.

#### Ejercicios
1. **(V/F)** En C# una clase puede heredar directamente de dos clases distintas. → Falso.
2. Diseña (en texto) una jerarquía de 3 niveles para `Empleado` → `EmpleadoAdministrativo` → `Gerente`.
3. **(Trampa)** Un estudiante redefine el atributo `color` en una subclase `CarroDeportivo` porque "quiere que sea siempre rojo". ¿Es buena práctica? *(No — mejor usar un valor por defecto asignado en el constructor de la subclase, no redefinir el atributo heredado.)*
4. Explica con tus palabras por qué una subclase no puede tocar los atributos `private` de su padre, y qué modificador soluciona eso.
5. **(Programación)** Implementa en Java una clase abstracta `Figura` con método abstracto `calcularArea(): double`, y dos subclases concretas `Circulo` y `Rectangulo`.

## Tema 2.2 — UML: Generalización (Herencia)

### 1. Intuición
Ya la tienes del Tema 2.1: "Libro es un Material Bibliográfico". La Generalización en UML es, literalmente, la forma de **dibujar** esa frase.

### 2. Definición formal
La Generalización es la relación UML que representa herencia: una clase (subclase) especializa a otra (superclase), heredando su estructura y comportamiento y añadiendo o modificando lo que le es propio.

### 3. Cómo reconocerla
- ¿Puedes decir "A es un B" de forma natural y sin forzar el lenguaje? → Generalización.
- ¿A tiene TODO lo que tiene B, más algo adicional específico? → Generalización.
- Si tu respuesta suena forzada ("un Motor es un Automóvil" — no tiene sentido), **no es herencia**, busca Composición/Agregación.

### 4. Cómo dibujarla
```
        MaterialBibliografico
                 △
                 │   (línea continua + triángulo/flecha hueca, apunta a la SUPERCLASE)
         ┌───────┴───────┐
       Libro            DVD
```
El triángulo (o flecha hueca) siempre apunta **hacia arriba, hacia la superclase**. Es el único símbolo UML que se ve así (a diferencia del rombo de Agregación/Composición).

### 5. Cómo se implementa en Java/C#
Ya lo viste completo en el Tema 2.1 (`extends` en Java, `:` en C#). Diagrama → Código:
```
MaterialBibliografico (abstracta, itálica)     →     abstract class MaterialBibliografico { ... }
        △
      Libro                                     →     class Libro : MaterialBibliografico { ... }
```

### 6. Errores comunes
- Dibujar el triángulo apuntando hacia la subclase (al revés). El triángulo **siempre** apunta al padre.
- Confundir el triángulo de Generalización con el rombo de Composición — son símbolos completamente distintos con significados completamente distintos (jerarquía "ES-UN" vs. "TIENE-UN/ES-PARTE-DE").
- Modelar como herencia una relación que en realidad es de "capacidad" (ej. "Pato ES-UN Volador" — no, un Pato **puede volar**, eso es una capacidad/comportamiento, mejor modelado con una **interfaz** `IVolador`, no con herencia de una clase `Volador`).

### 7. Comparaciones
**Herencia vs. Composición** (pregunta de examen frecuentísima): usa Herencia cuando modelas **qué ES** algo (relación taxonómica, "es un tipo de"); usa Composición cuando modelas **de qué ESTÁ HECHO** algo (relación estructural, "tiene un/está compuesto por"). Regla mnemotécnica: **"Favorece la composición sobre la herencia"** cuando tengas dudas — es un principio de diseño ampliamente aceptado (lo retomamos en LSP, Día 4) porque la herencia crea un acoplamiento muy fuerte y rígido entre clases.

### 8. Ejercicio visual
Un `Empleado` puede ser `EmpleadoDeTiempoCompleto` o `EmpleadoPorHoras`. Ambos comparten `nombre`, `cedula`, pero calculan el salario de forma completamente distinta. ¿Herencia o interfaz? ¿Por qué?

### 9. Corrección
**Herencia**, porque comparten **estado** (atributos: nombre, cédula) además de comportamiento — una interfaz solo define comportamiento (firmas de métodos), no puede compartir atributos. Si solo necesitaras compartir el "contrato" de un método `calcularSalario()` sin compartir estado, una interfaz bastaría; aquí, como también comparten estructura, la herencia (con un método abstracto `calcularSalario()`) es más apropiada.

### 10. Casos difíciles
- **`Cuadrado` hereda de `Rectangulo`?** Matemáticamente "un cuadrado ES un rectángulo especial" — pero en código, si `Rectangulo` tiene `setAncho()` y `setAlto()` independientes, un `Cuadrado` que hereda debe forzar que ambos cambien juntos, lo que **rompe el comportamiento esperado** de la superclase (romper LSP — lo verás formalmente en el Día 4). Este es el ejemplo clásico de por qué "ES-UN" en lenguaje natural no siempre garantiza que la herencia sea la decisión de diseño correcta.

### 11. Reglas prácticas
> - Si puedes decir "A es un tipo especializado de B" **sin forzar el idioma**, y además comparten **estado**, → Generalización/Herencia.
> - Si la relación es sobre una **capacidad** que varias clases no relacionadas entre sí pueden tener (volar, nadar, imprimirse) → probablemente Interfaz (Tema 2.5), no Herencia.

### 12. (Consolidado más abajo, Tema 2.9.)


## Tema 2.3 — Polimorfismo (Sobrescritura, Virtual/Override, Ocultamiento con `new`, Atributos Estáticos)

#### Explicación

**Intuición.** Piensa en el verbo "pintar". `Casa.pintar()`, `Auto.pintar()`, `Carretera.pintar()` — el mismo **nombre** de acción, pero cada objeto lo ejecuta de forma **completamente distinta** (pintar una casa implica brochas y paredes; pintar una carretera implica líneas viales). Eso es **polimorfismo**: la capacidad de un programa de detectar la clase real de un objeto en tiempo de ejecución y llamar a **su propia** implementación de un método.

**Definición formal:** "poli" (muchas) + "morfismo" (formas). El polimorfismo se implementa mediante tres mecanismos: **Sobrescritura (Overriding)**, **Interfaces**, y **Sobrecarga (Overload)** — aunque, como veremos, solo el primero y el segundo son polimorfismo "verdadero" en el sentido de resolverse en tiempo de ejecución.

**🔑 Concepto complementario agregado (no está explícito así en tus PDFs, pero es la explicación técnica real):** existe **enlace estático (compile-time binding)** y **enlace dinámico (runtime binding)**.
- El **Overload** se resuelve en tiempo de **compilación** (el compilador decide, según los tipos de los argumentos, cuál método sobrecargado llamar). Por eso, aunque tus diapositivas lo llaman "polimorfismo de sobrecarga", **no es polimorfismo en el sentido estricto de la POO** — no depende de qué objeto real hay en memoria en tiempo de ejecución.
- El **Override** se resuelve en tiempo de **ejecución** (el runtime mira el tipo real del objeto, no el tipo de la variable, y llama a la versión correspondiente). **Este es el polimorfismo "de verdad"**, el que hace que `Casa.pintar()` ejecute el código de `Casa` incluso si la variable está declarada como un tipo más general.

**Overriding (Sobrescritura):** una subclase B hereda de A pero **redefine** un método heredado de A. Los atributos, en cambio, no deberían redefinirse (Tema 2.1).

**Métodos virtuales:**
- Se declaran con `virtual` en la clase base; **pueden** ser sobrescritos (no es obligatorio) usando `override` en la subclase.
- Solo puedes usar `override` si el método base está marcado `virtual`, `abstract` u `override` (en una cadena de herencia de más de dos niveles).
- El método `override` debe mantener el **mismo nivel de acceso** que el `virtual` correspondiente.

**Métodos abstractos:** se declaran con `abstract`; a diferencia de `virtual`, un método `abstract` **debe** ser sobrescrito obligatoriamente en la subclase (no tiene cuerpo/implementación en la clase base).

```csharp
// Clase Base
public class Vehiculo {
    public virtual void Acelerar() => Console.WriteLine("Acelerando genérico");
}
// SubClase
public class Moto : Vehiculo {
    public override void Acelerar() => Console.WriteLine("La moto acelera rápido");
}
```

**Ocultamiento con `new` (¡ojo, examen frecuente!):** cuando usas `new` como modificador de un miembro (no de instanciación), **ocultas explícitamente** un miembro heredado, en lugar de sobrescribirlo. La diferencia es sutil pero crítica:
- `override` → polimorfismo real: si tienes una variable del tipo base apuntando a un objeto derivado, se ejecuta la versión de la **subclase** (enlace dinámico).
- `new` (ocultamiento) → **NO** es polimorfismo: si tienes una variable del tipo base apuntando a un objeto derivado, se ejecuta la versión de la **base** (enlace estático, se decide por el tipo de la variable, no el objeto real).

> ⚠️ **Regla textual de tu material, cásate con ella para el examen:** *"Ojo: no utilizar `new` y `override` al mismo tiempo, son excluyentes."*

**Atributos estáticos:** tu material aclara explícitamente: *"No existen atributos abstractos, existen atributos ESTÁTICOS."* Un atributo estático se **comparte entre todos los objetos** instanciados de una clase (a diferencia de los atributos normales, que son independientes por objeto — Tema 1.8). Se usa típicamente para numeración consecutiva (ej. número de boleta, número de factura).

#### Ejemplos

**Java** — Overriding real:
```java
public class Vehiculo {
    public void acelerar() { System.out.println("Vehículo acelera de forma genérica"); }
}
public class Moto extends Vehiculo {
    @Override
    public void acelerar() { System.out.println("La moto acelera rápido"); }
}

Vehiculo v = new Moto();   // variable de tipo Vehiculo, objeto real Moto
v.acelerar();              // Imprime "La moto acelera rápido" -> POLIMORFISMO REAL (enlace dinámico)
```

**C# — comparando `override` vs. `new` (ocultamiento) lado a lado:**
```csharp
public class Vehiculo {
    public virtual void Acelerar() => Console.WriteLine("Vehículo acelera genérico");
}

public class MotoOverride : Vehiculo {
    public override void Acelerar() => Console.WriteLine("Moto (override) acelera rápido");
}

public class MotoNew : Vehiculo {
    public new void Acelerar() => Console.WriteLine("Moto (new/ocultamiento) acelera rápido");
}

// Main:
Vehiculo v1 = new MotoOverride();
v1.Acelerar(); // "Moto (override) acelera rápido"  <- polimorfismo real, mira el objeto

Vehiculo v2 = new MotoNew();
v2.Acelerar(); // "Vehículo acelera genérico"        <- NO es polimorfismo, mira el TIPO DE LA VARIABLE
```

**Atributos estáticos (C#):**
```csharp
public class Factura {
    private static int consecutivo = 0;    // COMPARTIDO por todas las Facturas
    public int numero;

    public Factura() {
        consecutivo++;
        numero = consecutivo;  // cada factura toma el siguiente número disponible
    }
}
```

#### Errores comunes
- El error de examen #1 de este tema: **confundir `new` (ocultamiento) con `override`** y esperar comportamiento polimórfico de un método oculto con `new`. Si el profesor te muestra un código con `new` y te pregunta "¿qué imprime?", la trampa es asumir que se comporta como `override`.
- Marcar un método como `override` sin que el método base sea `virtual`/`abstract`/`override` — no compila.
- Usar `virtual` y `abstract` indistintamente: `virtual` tiene cuerpo por defecto y es opcional sobrescribirlo; `abstract` NO tiene cuerpo y es obligatorio sobrescribirlo.
- Confundir atributo estático con atributo abstracto (no existe tal cosa como "atributo abstracto" — tu propio material corrige explícitamente esta confusión común).

#### Relaciones
El Polimorfismo es el pilar OOP que **usa** la Herencia (Tema 2.1) para dar comportamiento distinto según el objeto real. Es también la base técnica del **Principio Open/Closed (OCP)** en el Día 4: gracias al polimorfismo, puedes **extender** comportamiento (agregando nuevas subclases con su propio `override`) sin **modificar** el código que ya usa la clase base.

#### Resumen
- Polimorfismo = mismo método, comportamiento distinto según el objeto real (en tiempo de ejecución).
- `virtual`+`override` = polimorfismo real (enlace dinámico).
- `new` = ocultamiento, NO polimorfismo (enlace estático, mira el tipo de la variable).
- `abstract` = obligatorio sobrescribir; `virtual` = opcional sobrescribir.
- Atributo estático = compartido entre todos los objetos de la clase (no existen "atributos abstractos").

#### Ejercicios
1. **(Trampa, la clásica de examen)** Dado el código de `MotoNew` de arriba, ¿qué imprime `v2.Acelerar()` si `v2` es de tipo `Vehiculo` pero apunta a un objeto `MotoNew`? *(Imprime el mensaje de Vehiculo, NO el de MotoNew, porque `new` no es polimorfismo.)*
2. **(V/F)** Se puede usar `override` y `new` al mismo tiempo sobre el mismo miembro. → Falso, son excluyentes.
3. Diseña una clase base `Empleado` con método `virtual CalcularBono()` y una subclase `Gerente` que lo sobrescriba con `override`.
4. **(Conceptual)** Explica, usando tus palabras, la diferencia entre enlace estático y enlace dinámico.
5. Implementa en C# un atributo estático `contador` para una clase `Boleta`, que asigne automáticamente un número consecutivo a cada boleta creada.

## Tema 2.4 — Interfaces

#### Explicación

**Intuición.** Piensa en un **contrato de arrendamiento**: especifica *qué* debe hacer el arrendatario (pagar el arriendo, no hacer ruido después de las 10pm) pero no *cómo* lo hace exactamente (de dónde saca el dinero es asunto suyo). Una **interfaz** en programación es exactamente ese contrato: dice **qué** debe poder hacer una clase, sin decir **cómo**.

**Definición formal (de tu material):**
- Es el **qué** debería hacer una clase (todo lo que se puede hacer con ella), sin especificar el **cómo**.
- Es una especie de "clase abstracta pura" con métodos abstractos **públicos**, sin código.
- En la interfaz no se maneja visibilidad (todos sus miembros son, implícitamente, públicos — es el contrato completo).
- Es una estructura de datos que muestra únicamente las **firmas** de los métodos.
- Se etiqueta **"Implementa"** en UML (a diferencia de "Extiende"/Generalización).
- Nota unificada de tus dos documentos: **es la forma de simular herencia múltiple de comportamiento en lenguajes que no la permiten entre clases** — una clase puede implementar varias interfaces, aunque solo puede heredar de una clase.

**Herencia entre interfaces:** una interfaz puede heredar de otra (usando `:` en C# o `extends` en Java entre interfaces), agregando más métodos al contrato:
```csharp
interface IVehiculo {
    void Acelerar(int kmh);
    void Frenar();
}
interface IVehiculoVolador : IVehiculo {
    void Despegar();
    void Aterrizar();
}
```

**Cuándo usar interfaces:** cuando tienes más de una clase que hace "lo mismo" conceptualmente (mismo contrato), pero de forma distinta internamente.

**Segregar interfaz (adelanto de ISP, Día 4):** dividir los métodos de una interfaz grande en varias interfaces pequeñas, porque no todas las clases que implementarían la interfaz necesitan **todos** sus métodos.

#### Ejemplos

**Escenario de tu material:** una universidad tiene estudiantes de pregrado y posgrado. Todos `estudian` y `exponen`; solo los de posgrado `escribenTesis` y `sustentanTesis`.

**Java:**
```java
interface IEstudiante {
    void estudiar();
    void exponer();
}

class EstudiantePregrado implements IEstudiante {
    public void estudiar() { System.out.println("Estudiando para el parcial"); }
    public void exponer() { System.out.println("Exponiendo un trabajo"); }
}

interface IEstudiantePosgrado extends IEstudiante {
    void escribirTesis();
    void sustentarTesis();
}

class EstudiantePosgrado implements IEstudiantePosgrado {
    public void estudiar() { System.out.println("Estudiando temas avanzados"); }
    public void exponer() { System.out.println("Exponiendo un paper"); }
    public void escribirTesis() { System.out.println("Escribiendo la tesis"); }
    public void sustentarTesis() { System.out.println("Sustentando la tesis"); }
}
```

**C#:**
```csharp
interface IEstudiante {
    void Estudiar();
    void Exponer();
}
interface IEstudiantePosgrado : IEstudiante {
    void EscribirTesis();
    void SustentarTesis();
}

class EstudiantePregrado : IEstudiante {
    public void Estudiar() => Console.WriteLine("Estudiando para el parcial");
    public void Exponer() => Console.WriteLine("Exponiendo un trabajo");
}

class EstudiantePosgrado : IEstudiantePosgrado {
    public void Estudiar() => Console.WriteLine("Estudiando temas avanzados");
    public void Exponer() => Console.WriteLine("Exponiendo un paper");
    public void EscribirTesis() => Console.WriteLine("Escribiendo la tesis");
    public void SustentarTesis() => Console.WriteLine("Sustentando la tesis");
}
```

> Nota: en este ejemplo real de tu deck, si `EstudiantePregrado` y `EstudiantePosgrado` **también comparten atributos** (nombre, cédula, programa), lo correcto es combinar herencia (para el estado y comportamiento común) **con** interfaces (para el contrato adicional de posgrado): una clase abstracta `Estudiante` de la que ambos heredan, y `EstudiantePosgrado` que además implementa `IEstudiantePosgrado`. Esta combinación herencia+interfaces es exactamente "Cómo se diseña" que muestra tu material.

#### Errores comunes
- Crear **una sola interfaz gigante** con 15 métodos que obliga a todas las clases a implementar métodos que no necesitan — es la violación de ISP que verás formalmente en el Día 4 (aquí ya la puedes detectar intuitivamente).
- Pensar que una interfaz puede tener atributos con estado (campos) — no puede; solo puede tener firmas de métodos (y, en C# moderno, propiedades sin campo de respaldo, o métodos con implementación por defecto en casos especiales, pero esto es avanzado y no aparece en tu material).
- Confundir "implementa" (interfaz) con "hereda/extiende" (clase) en el diagrama UML — son etiquetas y símbolos distintos (Tema 2.5).

#### Relaciones
Las interfaces son la base técnica de **ISP** (Segregación de Interfaces, Día 4) y de **DIP** (Inversión de Dependencias, Día 4) — casi todo SOLID depende de programar "contra interfaces, no contra implementaciones concretas". También es clave para SOA (Día 3): un "servicio" es, en esencia, un contrato (interfaz) que oculta su implementación.

#### Resumen
- Interfaz = contrato público, solo firmas de métodos, sin implementación ni estado.
- Se etiqueta "Implementa" en UML; permite simular herencia múltiple de comportamiento.
- Segregar interfaz = dividir una interfaz grande en varias pequeñas y específicas.
- Se puede combinar herencia (para estado/comportamiento común) con interfaces (para contratos adicionales).

#### Ejercicios
1. **(V/F)** Una interfaz puede tener atributos con valores. → Falso.
2. Diseña `IVolador` e `INadador` para modelar un `Pato` que puede hacer ambas cosas (y una `Aguila` que solo `IVolador`).
3. **(Trampa)** Un estudiante diseña una interfaz `IAnimal` con 10 métodos (`volar`, `nadar`, `correr`, `ladrar`, `maullar`...) y hace que TODAS las clases de animales la implementen. ¿Qué está mal? *(Viola segregación de interfaces — un `Pez` tendría que implementar `ladrar()` sin sentido.)*


## Tema 2.5 — UML: Realización (Interfaces)

### 1. Intuición
Ya viste el "contrato" en el Tema 2.4. La Realización es cómo se **dibuja** ese contrato: distinto de la herencia (que hereda estado + comportamiento de una clase concreta), la realización solo hereda un **compromiso de comportamiento** de una interfaz.

### 2. Definición formal
La Realización es la relación UML entre una clase y una interfaz, donde la clase se compromete a implementar todos los métodos definidos por la interfaz.

### 3. Cómo reconocerla
- ¿La clase "cumple un contrato" definido en otra parte (una interfaz) sin heredar estado de ella? → Realización.
- ¿Distintas clases sin relación de parentesco entre sí (un `Pato` y un `Avion`, por ejemplo) necesitan compartir solo un **comportamiento** (`volar()`) sin compartir estructura? → Interfaz + Realización.

### 4. Cómo dibujarla
```
      «interface»
        IVolador
            △
            ┊  (línea PUNTEADA + triángulo hueco, a diferencia de la línea CONTINUA de Generalización)
            ┊
          Pato
```
El símbolo es el mismo triángulo hueco de la Generalización, pero con **línea punteada** en vez de continua — esa es la única diferencia visual, y es una trampa de examen frecuente.

### 5. Cómo se implementa
Ya visto en el Tema 2.4 (`implements` en Java, `:` en C# para interfaces — en C# se usa el mismo símbolo `:` tanto para heredar de una clase como para implementar una interfaz; la diferencia la da el compilador según si lo que sigue es `class` o `interface`).

```csharp
public class Pato : Ave, IVolador, INadador {  // Ave es herencia (Generalización); IVolador/INadador son Realización
    public void Volar() => Console.WriteLine("El pato vuela");
    public void Nadar() => Console.WriteLine("El pato nada");
}
```

### 6. Errores comunes
- Dibujar Realización con línea continua (confundiéndola con Generalización) — son visualmente parecidas pero semánticamente muy distintas.
- Olvidar el estereotipo `«interface»` sobre el nombre en el rectángulo de la interfaz.
- Implementar solo *algunos* métodos de la interfaz — no compila; una clase concreta que implementa una interfaz debe implementar el 100% de sus métodos (o ella misma debe declararse `abstract`).

### 7. Comparaciones
**Generalización vs. Realización** — ambas usan triángulo hueco, se diferencian SOLO por el tipo de línea (continua vs. punteada) y por lo que hay al otro lado (una clase vs. una interfaz).

**Clase abstracta vs. Interface** (comparación pedida explícitamente en tus instrucciones):

| | Clase abstracta | Interfaz |
|---|---|---|
| ¿Puede tener atributos con estado? | Sí | No |
| ¿Puede tener métodos con implementación? | Sí (mezcla de abstractos y concretos) | No (en las versiones clásicas de C#/Java que usa tu curso) |
| ¿Cuántas puede "heredar/implementar" una clase? | Solo 1 (herencia simple) | Varias a la vez |
| ¿Cuándo usarla? | Cuando hay estado y comportamiento común real entre las subclases | Cuando solo necesitas garantizar un contrato de comportamiento, sin importar el parentesco |

### 8. Ejercicio visual
Un `Robot`, un `Humano` y un `Perro` pueden todos `moverse()`, pero de formas totalmente distintas y sin compartir ningún atributo entre sí. ¿Interfaz o herencia?

### 9. Corrección
**Interfaz** `IMovible` con el método `mover()`. No comparten estado ni un ancestro común natural (un Robot no "es un" Humano ni viceversa), solo comparten la **capacidad**.

### 10. Casos difíciles
Un `Documento` de solo lectura y un `DocumentoEditable` (que además permite `guardar()`). ¿Herencia de `Documento` a `DocumentoEditable`, o interfaz `IEditable` aparte? — este es exactamente el ejemplo que tu material usa para **LSP** en el Día 4 (spoiler): la solución que aparece en tus diapositivas de SOLID es que `DocumentoEditable` **extiende** `Documento` y agrega el comportamiento `save()` — es decir, aquí sí se prefiere herencia porque hay una relación ES-UN clara y el comportamiento adicional no rompe lo que el padre garantiza.

### 11. Reglas prácticas
> - Línea **continua** + triángulo hueco → Generalización (herencia de clase).
> - Línea **punteada** + triángulo hueco → Realización (implementación de interfaz).
> - Si dudas entre "clase abstracta" o "interfaz": ¿hay estado/atributos compartidos reales? → clase abstracta. ¿Solo un contrato de comportamiento entre clases sin parentesco? → interfaz.

### 12. (Ejercicio final de UML — a continuación, Tema 2.9.)

## Tema 2.6 — Sobrecarga (Overload): Paramétrica y "Polimorfismo de Sobrecarga"

#### Explicación

**Definición formal:** la sobrecarga es la posibilidad de que **dos o más métodos** dentro de la misma clase, incluyendo el constructor, **compartan el mismo nombre**, diferenciándose en la **declaración de sus parámetros** (cantidad, tipo, u orden). Cuando invocas el método/constructor, el compilador elige automáticamente cuál versión ejecutar, **según los argumentos que le pasaste** (enlace estático — se resuelve en compilación, tal como vimos en el Tema 2.3).

**Tipos de sobrecarga paramétrica** (de tu material):
1. **Sobrecarga paramétrica tipo 1:** mismo método, diferentes parámetros, **mismo tipo** (ej. `sumar(int a, int b)` vs. `sumar(int a, int b, int c)`).
2. **Sobrecarga paramétrica tipo 2:** mismo método, diferentes parámetros, **diferentes tipos** (ej. `sumar(int a, int b)` vs. `sumar(double a, double b)`).

**"Polimorfismo de sobrecarga"** (terminología textual de tu deck, aunque técnicamente NO es polimorfismo en tiempo de ejecución): el mismo **nombre de método** en **diferentes clases no relacionadas por herencia**, donde cada clase hace algo completamente distinto. Ejemplo: `repararMotor()` existe en `Vehiculo`, `Electrodomestico` y `Elevador` — cada uno "repara motor" de forma distinta, pero no hay herencia entre ellos, ni override real: cada clase simplemente define su propio método con ese nombre, sin relación entre sí.

> ⚠️ Distinción de examen: no confundas "polimorfismo de sobrecarga" (mismo nombre en clases NO relacionadas) con el **overriding** real del Tema 2.3 (mismo nombre en una jerarquía de herencia, resuelto en tiempo de ejecución).

#### Ejemplos

**Java** — sobrecarga de método y de constructor:
```java
public class Calculadora {
    // Sobrecarga tipo 1: mismo tipo, distinta cantidad de parámetros
    public int sumar(int a, int b) { return a + b; }
    public int sumar(int a, int b, int c) { return a + b + c; }

    // Sobrecarga tipo 2: distinto tipo de parámetros
    public double sumar(double a, double b) { return a + b; }
}
```
**C#:**
```csharp
public class PerroCaliente {
    public void Preparar() => Console.WriteLine("Pan + salchicha");
    public void Preparar(string salsa1, string salsa2) => Console.WriteLine($"Pan + salchicha + {salsa1} + {salsa2}");
    public void Preparar(string salsa1, string salsa2, bool conTocineta) =>
        Console.WriteLine($"Pan + salchicha + {salsa1} + {salsa2}" + (conTocineta ? " + tocineta" : ""));
}
```

#### Errores comunes
- Intentar sobrecargar dos métodos que solo difieren en el **tipo de retorno** (sin cambiar los parámetros) — **no compila**, ni en C# ni en Java. La firma que distingue sobrecargas es el nombre + los parámetros, nunca el tipo de retorno solo.
- Confundir sobrecarga (overload, tiempo de compilación) con sobrescritura (override, tiempo de ejecución) — es el error conceptual #1 de todo el Día 2, y muy probablemente una pregunta directa de examen ("¿cuál es la diferencia entre overload y override?").

#### Relaciones
La sobrecarga ya la usaste sin saberlo desde el Tema 1.6 (constructores sobrecargados). Aquí se formaliza como concepto general aplicable a cualquier método, y se contrasta explícitamente con el Overriding del Tema 2.3 para que la diferencia quede blindada de cara al examen.

#### Resumen
- Overload = mismo nombre, distinta firma de parámetros, se resuelve en compilación.
- "Polimorfismo de sobrecarga" (según tu material) = mismo nombre en clases no relacionadas, cada una con su propia implementación independiente.
- Distinto de Override (mismo nombre, misma firma, jerarquía de herencia, se resuelve en ejecución).

#### Ejercicios
1. **(V/F)** Se puede sobrecargar un método cambiando solo el tipo de retorno. → Falso.
2. Escribe 3 sobrecargas del método `crear()` de una clase `Figura`: sin parámetros, con un `int lado` (cuadrado), con `int base, int altura` (rectángulo).
3. **(Conceptual, examen típico)** Explica la diferencia entre Overload y Override en una tabla de 2 columnas.

## Tema 2.7 — La clase `Object`: Equals, GetHashCode, GetType, ToString; operadores `is`/`as`

#### Explicación

**Intuición.** En C#, **absolutamente todo hereda de `Object`**, incluso si nunca escribes `: Object` explícitamente — es como el "Adán" de todas las clases. Por eso, cualquier objeto que crees automáticamente "sabe" hacer 4 cosas básicas, aunque tú no se las hayas enseñado.

**Definición formal:**
- `Object` es la clase superior de todas las clases en C# (equivalente exacto a `Object` en Java — mismo concepto, mismo nombre).
- Una variable de tipo `object` puede apuntar a un objeto de **cualquier** tipo (es la abstracción máxima).
- Pero una variable `object` **no permite llamar métodos específicos** de la clase real (solo los 4 genéricos de `Object`), a menos que hagas una conversión.
- Los 4 métodos genéricos que todo objeto tiene por herencia de `Object`:

| Método | Qué hace |
|---|---|
| `Equals(obj)` | Compara si dos objetos son "iguales" (por defecto, compara referencias — mismo objeto en memoria; se puede sobrescribir para comparar por valor) |
| `GetHashCode()` | Devuelve un código numérico usado en estructuras hash (diccionarios, sets) |
| `GetType()` | Devuelve el tipo real del objeto en tiempo de ejecución (¡muy usado para depurar polimorfismo!) |
| `ToString()` | Devuelve una representación en texto del objeto (por defecto, el nombre completo de la clase; se sobrescribe casi siempre) |

**Operador `is`:** verifica si el tipo de un objeto, en tiempo de ejecución, es compatible con otro tipo dado. Devuelve `true`/`false`.

**Operador `as`:** realiza conversiones entre tipos compatibles — es similar a un *cast*, pero **si la conversión falla, devuelve `null` en lugar de lanzar una excepción** (a diferencia de un cast clásico `(Tipo)objeto`, que sí lanza `InvalidCastException` si falla).

#### Ejemplos

**C#:**
```csharp
object obj = new Moto();

// is: verificación de tipo
if (obj is Moto) {
    Console.WriteLine("Es una Moto");
}

// as: conversión segura (null si falla, no lanza excepción)
Moto m = obj as Moto;
if (m != null) {
    m.Acelerar();
}

// GetType(): tipo real en tiempo de ejecución
Console.WriteLine(obj.GetType().Name); // imprime "Moto", aunque la variable sea de tipo object

// ToString() sobrescrito
public class Moto {
    public override string ToString() => "Soy una Moto";
}
Console.WriteLine(obj.ToString()); // "Soy una Moto" en vez del nombre de la clase por defecto
```

**Java (equivalente conceptual):**
```java
Object obj = new Moto();

if (obj instanceof Moto) {          // equivalente a "is"
    Moto m = (Moto) obj;            // Java no tiene un "as" que devuelva null; usa cast clásico
    m.acelerar();
}

System.out.println(obj.getClass().getSimpleName()); // equivalente a GetType().Name
System.out.println(obj.toString());                  // equivalente a ToString()
```
> Nota: Java sí permite un patrón similar a `as` desde Java 14+ con *pattern matching* (`if (obj instanceof Moto m) { m.acelerar(); }`), pero no es exactamente igual al `as` de C#. Para tu examen (que es en C#), quédate con `is`/`as` tal como se explicó arriba.

#### Errores comunes
- Usar un *cast* clásico `(Moto) obj` cuando `obj` podría no ser realmente una `Moto` — lanza `InvalidCastException` en tiempo de ejecución. El operador `as` es más seguro para estos casos (siempre y cuando luego verifiques `!= null`).
- Olvidar sobrescribir `ToString()` y sorprenderse de que `Console.WriteLine(miObjeto)` imprime algo como `MiNamespace.Moto` en lugar de información útil.
- Confundir `Equals()` (compara objetos) con `==` (que, por defecto en clases, también compara referencia, pero puede sobrecargarse de forma distinta a `Equals` — tema avanzado que no está en tu material, pero vale la pena saber que existen ambos).

#### Relaciones
`Object` es el "techo" de toda jerarquía de herencia — cierra conceptualmente el Nivel 3 del mapa de conocimiento (todo lo que viste en Herencia, Polimorfismo e Interfaces, en el fondo, hereda de `Object`). El operador `is`/`as` es una herramienta práctica que usarás constantemente al trabajar con colecciones polimórficas (ej. una `List<Vehiculo>` que en realidad contiene `Moto`s y `Carro`s mezclados).

#### Resumen
- Todo hereda de `Object`: `Equals`, `GetHashCode`, `GetType`, `ToString`.
- `is` verifica compatibilidad de tipo (bool); `as` convierte de forma segura (null si falla).
- `GetType()` da el tipo REAL en tiempo de ejecución, útil para depurar polimorfismo.

#### Ejercicios
1. **(V/F)** El operador `as` lanza una excepción si la conversión falla. → Falso, devuelve `null`.
2. Sobrescribe `ToString()` para una clase `Estudiante` que muestre `"Nombre - Cédula"`.
3. Dada una `List<object>` con mezcla de `Perro` y `Gato`, escribe un `foreach` que use `is` para llamar al método correcto de cada uno.


## Tema 2.8 — Tabla Comparativa Completa: Todas las Relaciones UML

Esta es la tabla que debes tener memorizada al 100% para el examen. Resume TODO lo visto en el Día 1 y el Día 2.

| Relación | Símbolo | Línea | Pregunta clave | Ejemplo |
|---|---|---|---|---|
| **Dependencia** | Flecha simple abierta | Punteada | ¿La uso solo un momento (parámetro de método) sin guardarla? | `Impresora.imprimir(Documento d)` |
| **Asociación** | Flecha simple abierta o línea simple | Continua | ¿La guardo como atributo, permanentemente, ambos con vida propia? | `Persona` tiene `List<Automovil>` |
| **Agregación** | Rombo hueco/blanco en el "todo" | Continua | ¿Es Todo-Partes, pero la parte puede vivir sin el todo? | `Equipo` ◇— `Jugador` |
| **Composición** | Rombo relleno/sólido en el "todo" | Continua | ¿Es Todo-Partes, y la parte NO tiene sentido sin el todo? | `Casa` ◆— `Habitacion` |
| **Generalización (Herencia)** | Triángulo hueco apuntando al padre | Continua | ¿Puedo decir "A es un B" y comparten estado? | `Libro` △→ `MaterialBibliografico` |
| **Realización (Interfaz)** | Triángulo hueco apuntando a la interfaz | **Punteada** | ¿Cumplo un contrato de comportamiento sin compartir estado? | `Pato` ┄△→ `«interface» IVolador` |

**Orden de "fuerza" del acoplamiento, de menor a mayor:**
```
Dependencia  <  Asociación  <  Agregación  <  Composición  <  Herencia
   (más débil, más flexible)                      (más fuerte, más rígido)
```
> 🔑 Esta escala es un argumento de diseño real: cuando dudes entre dos relaciones para modelar algo, **prefiere siempre la más débil que resuelva el problema** — esto minimiza el acoplamiento (recuerda el Tema 1.2) y hace tu diseño más flexible ante cambios futuros. Este mismo argumento reaparecerá, formalizado, en el LSP y el DIP del Día 4 ("preferir composición sobre herencia").

---

## Tema 2.9 — Ejercicio Final de UML: 40 Casos de Análisis

> Instrucciones: para cada caso, decide **tipo de relación**, **multiplicidad** (si aplica), **navegabilidad** (si aplica) e **implementación en C#** (declaración de la(s) clase(s) involucradas, sin desarrollar todo el cuerpo). Al final de la lista tienes la corrección razonada de TODOS. No mires la corrección antes de intentarlo tú mismo — el valor está en el intento, no en leer la respuesta.

1. Una `Universidad` tiene varias `Facultad`es; si la universidad cierra, las facultades (como entidades administrativas) desaparecen.
2. Un `Carro` tiene un `Motor`; el motor no tiene sentido fuera de ese carro específico.
3. Un `Taxi` **es un** `Automovil` con un atributo adicional `numeroTarjetaOperacion`.
4. Un `Metodo_de_pago` es usado (como parámetro) por un servicio `ProcesadorPago.cobrar(MetodoPago mp)`, sin guardarlo.
5. Un `Cliente` tiene una lista de `Pedido`s históricos; un pedido sigue existiendo como registro aunque el cliente se dé de baja (archivo histórico).
6. Un `Pais` tiene varias `Ciudad`es; una ciudad puede, en teoría, cambiarse de país (redistritación), aunque es raro.
7. Un `Perro` y un `Avion` ambos `pueden moverse()`, sin relación de parentesco entre sí.
8. Una `OrdenDeCompra` tiene varias `LineaOrden` (detalle de productos); una línea de orden no existe sin su orden.
9. Un `Gerente` **es un** `Empleado` con bono adicional.
10. Un `ServicioDeEnvio` recibe un objeto `Paquete` en el método `enviar(Paquete p)` y no lo guarda para nada más.
11. Un `Equipo de Baloncesto` tiene `Jugador`es; un jugador puede ser transferido a otro equipo.
12. Un `Album musical` tiene `Cancion`es; una canción sin su álbum (en este modelo específico de negocio, donde solo importa el álbum físico) no tiene identidad.
13. Un `Circulo` y un `Cuadrado` ambos `calculanArea()`, ambos heredan de una clase abstracta `Figura` con atributo común `color`.
14. Un `Banco` ofrece `CuentaAhorro` y `CuentaCorriente`, ambas ES-UN `Cuenta`.
15. Una `Factura` referencia a un `Cliente` (para saber a quién facturar) de forma permanente, guardándolo como atributo.
16. Un método `Reporte.generar(Formato f)` recibe un objeto `Formato` (PDF/Excel) solo para esa ejecución, sin guardarlo.
17. Un `Auto` tiene 4 `Llanta`s; las llantas no tienen sentido fuera de ESE auto (en este modelo de negocio de un taller que solo repara autos completos).
18. Una interfaz `IDescargable` es implementada por `Pdf`, `Imagen` y `Video`, sin relación de herencia entre ellos.
19. Un `Estudiante` **es un** `Persona`.
20. Una `Sucursal Bancaria` tiene `Cajero`s (empleados); un cajero puede ser trasladado a otra sucursal.
21. Un `Pedido` tiene un `Repartidor` asignado (uno a la vez), y el repartidor sigue existiendo con o sin ese pedido.
22. Una clase de asociación `Matricula` conecta `Estudiante` con `Curso`, guardando `nota` y `fechaMatricula` — datos que no pertenecen ni a Estudiante ni a Curso.
23. Una `Bicicleta` tiene un `Cuadro` (frame); en este taller de bicicletas artesanales, el cuadro se fabrica exclusivamente para ESA bicicleta.
24. Un `Robot` y un `Ventilador` ambos implementan `IEncendible` (con métodos `encender()`/`apagar()`).
25. Un `ClienteVIP` **es un** `Cliente` con descuento adicional.
26. Un `GeneradorDeReportes.imprimir(Impresora imp)` — la impresora es solo un parámetro puntual.
27. Un `Colegio` tiene `Salon`es; un salón (como espacio físico numerado dentro de ESE colegio) no tiene sentido fuera de él.
28. Una `Aerolinea` tiene `Avion`es; un avión puede venderse a otra aerolínea y seguir existiendo.
29. Una interfaz `IComparable` es implementada por `Producto` para poder ordenar una lista de productos.
30. Un `Rectangulo` hereda de una clase abstracta `FiguraGeometrica` con método abstracto `area()`.
31. Un `Restaurante` tiene un `Menu`; el menú (esa colección específica de platos con esos precios) no existe conceptualmente fuera de ese restaurante.
32. Una `Mascota` tiene un `Dueño` (Persona); la persona existe independientemente de tener o no una mascota.
33. Un `ControladorMVC` (Día 3) usa una interfaz `IServicioUsuario` inyectada por constructor y la guarda como atributo para usarla en varios métodos de acción.
34. Un `Empleado` calcula su salario con `calcularSalario()`; `EmpleadoPorHoras` y `EmpleadoFijo` heredan y sobrescriben ese método.
35. Una función `Validador.validar(Formulario f)` valida un formulario recibido como parámetro, sin persistir la referencia.
36. Un `Hospital` tiene `Ala`s (Ala Norte, Ala Sur); un ala no tiene sentido como entidad fuera de ESE hospital.
37. Un `Conductor` tiene varias `Multa`s asociadas a lo largo del tiempo; las multas pueden consultarse como historial incluso si el conductor pierde la licencia.
38. Una interfaz `IVolador` es heredada por otra interfaz más específica `IVoladorSupersonico` que agrega `activarPostcombustion()`.
39. Un `Escritorio` tiene `Cajon`es; los cajones (en un modelo de mueblería artesanal a la medida) se fabrican solo para ESE escritorio.
40. Un `SistemaDeNotificaciones.enviar(Usuario u, Mensaje m)` recibe ambos objetos solo para ejecutar el envío, sin guardarlos en ningún atributo.

### ✅ Corrección razonada (los 40 casos)

1. **Composición** — Universidad ◆— Facultad (la facultad como entidad administrativa depende de la universidad).
2. **Composición** — Carro ◆— Motor.
3. **Generalización/Herencia** — Taxi △→ Automovil.
4. **Dependencia** — parámetro de método, sin persistencia.
5. **Agregación** (o Asociación simple con multiplicidad `1 — 0..*`) — el pedido sobrevive al cliente como registro histórico, por tanto NO es composición pura; el vínculo es más débil.
6. **Agregación** — Pais ◇— Ciudad (la ciudad puede, en teoría, existir/trasladarse independientemente).
7. **Realización** de una interfaz común `IMovible`, sin herencia entre Perro y Avion.
8. **Composición** — OrdenDeCompra ◆— LineaOrden.
9. **Generalización/Herencia** — Gerente △→ Empleado.
10. **Dependencia** — `enviar(Paquete p)` sin persistir.
11. **Agregación** — Equipo ◇— Jugador (el jugador sobrevive al equipo).
12. **Composición** — Album ◆— Cancion (en este modelo específico de negocio dado).
13. **Generalización/Herencia** (ambos heredan de `Figura` abstracta, que además define un atributo común `color`, lo que descarta la opción de solo-interfaz).
14. **Generalización/Herencia** — CuentaAhorro y CuentaCorriente △→ Cuenta.
15. **Asociación** (permanente, guardado como atributo).
16. **Dependencia** — parámetro puntual.
17. **Composición** — Auto ◆— Llanta (según el enunciado específico dado).
18. **Realización** — Pdf, Imagen, Video ┄△→ «interface» IDescargable.
19. **Generalización/Herencia** — Estudiante △→ Persona.
20. **Agregación** — Sucursal ◇— Cajero (el cajero sobrevive al traslado).
21. **Asociación** (multiplicidad `1 — 0..1`, el repartidor tiene vida propia).
22. **Clase de Asociación** — Matricula conecta Estudiante—Curso con datos propios de la relación.
23. **Composición** — Bicicleta ◆— Cuadro (fabricado exclusivamente para ella, según el enunciado).
24. **Realización** — Robot, Ventilador ┄△→ «interface» IEncendible.
25. **Generalización/Herencia** — ClienteVIP △→ Cliente.
26. **Dependencia** — parámetro puntual de `imprimir`.
27. **Composición** — Colegio ◆— Salon.
28. **Agregación** — Aerolinea ◇— Avion (puede venderse y seguir existiendo).
29. **Realización** — Producto ┄△→ «interface» IComparable.
30. **Generalización/Herencia** — Rectangulo △→ FiguraGeometrica (abstracta).
31. **Composición** — Restaurante ◆— Menu (según el enunciado dado, específico de ESE restaurante).
32. **Asociación** — Mascota — Dueño (la persona vive independientemente).
33. **Asociación** (hacia una interfaz) — es Inyección de Dependencias (DIP, Día 4): se guarda como atributo, por lo tanto es Asociación, aunque coloquialmente se llame "dependencia inyectada".
34. **Generalización/Herencia + Polimorfismo (override)** — EmpleadoPorHoras, EmpleadoFijo △→ Empleado, cada uno sobrescribe `calcularSalario()`.
35. **Dependencia** — parámetro puntual de `validar`.
36. **Composición** — Hospital ◆— Ala.
37. **Agregación** (o Asociación) — las multas persisten como historial independiente del estado de la licencia del conductor.
38. **Realización entre interfaces (herencia de interfaces)** — IVoladorSupersonico : IVolador.
39. **Composición** — Escritorio ◆— Cajon (a la medida, según enunciado).
40. **Dependencia** — parámetro puntual de `enviar`, sin persistir ninguno de los dos objetos.

> 💡 **Patrón que deberías notar tras resolver los 40 casos:** la palabra clave del enunciado casi siempre delata la relación — *"depende de", "usa temporalmente", "recibe como parámetro"* → Dependencia; *"tiene una lista permanente de", "guarda una referencia a"* → Asociación; *"se compone de", "no tiene sentido sin", "se crea junto con"* → Composición; *"agrupa", "puede existir independientemente"* → Agregación; *"es un tipo de"* → Herencia; *"puede hacer X", "cumple el contrato de"*, sin parentesco → Interfaz/Realización. **Pero cuidado:** en el examen real, el profesor espera que justifiques con el enunciado específico, no que apliques la palabra clave mecánicamente — varios de los 40 casos de arriba son deliberadamente ambiguos para que practiques la justificación, no la memorización de palabras gatillo.

---

### 🧪 Mini examen — Día 2 completo

1. ¿Cuál es la diferencia entre `virtual` y `abstract`?
2. Explica por qué `new` (ocultamiento) NO es polimorfismo, con un ejemplo de código.
3. Dibuja en texto la diferencia visual entre Generalización y Realización.
4. **(V/F)** Una clase puede heredar de dos clases a la vez en C#. → Falso.
5. Define ES-UN y da un ejemplo correcto y uno incorrecto.
6. ¿Qué diferencia hay entre Overload y "polimorfismo de sobrecarga" según tu material?
7. Explica qué hace `GetType()` y en qué se diferencia de simplemente mirar el tipo declarado de una variable.
8. ¿Cuándo usarías una interfaz en vez de una clase abstracta? Da un ejemplo propio.
9. Clasifica: `Perro` **ladra**; `Perro` **tiene** un `Dueño`; `Perro` **es un** `Animal`; `Perro` **usa temporalmente** un `Veterinario` en una consulta.
10. ¿Qué error de compilación obtendrías si marcas `override` un método cuya clase base no lo marcó `virtual`, `abstract` ni `override`?


---

# 📅 DÍA 3 — Paradigma Funcional, Eventos, Aspectos y Arquitectura

## Tema 3.1 — Expresión Condicional Ternaria

#### Explicación

**Intuición.** Es un "if-else" escrito en una sola línea, para cuando la decisión es simple y se usa dentro de una expresión (por ejemplo, para inicializar una variable).

**Definición formal:** el operador `?:` evalúa una expresión booleana y devuelve el resultado de una de dos expresiones, según si la condición es `true` (consecuente) o `false` (alternativa).

```
condicion ? consecuente : alternativa
```

#### Ejemplos

**C#:**
```csharp
bool sw1 = false;
string estadoTxt = !sw1 ? "Apagado" : "Encendido";   // "Encendido"
```
**Java (idéntico):**
```java
boolean sw1 = false;
String estadoTxt = !sw1 ? "Apagado" : "Encendido";
```

#### Errores comunes
- Anidar demasiados ternarios (`a ? b : c ? d : e`) — funciona, pero se vuelve ilegible; tu material lo recomienda solo para simplificar un `if` **simple**.
- Usar el ternario para ejecutar acciones con efectos secundarios complejos (debería usarse para **devolver un valor**, no para "hacer cosas").

#### Relaciones
El ternario es el primer paso hacia el pensamiento funcional: es una **expresión** (devuelve un valor) en vez de una **instrucción** (que ejecuta pasos). Esta distinción "expresión vs. instrucción" es la base de las lambdas de expresión que vienen a continuación.

#### Resumen
- `condicion ? consecuente : alternativa`.
- Útil para simplificar ifs simples y dentro de lambdas.

#### Ejercicios
1. Reescribe con ternario: `if (edad >= 18) { categoria = "Adulto"; } else { categoria = "Menor"; }`.
2. **(V/F)** El ternario puede usarse dentro de una expresión lambda. → Verdadero.

## Tema 3.2 — Expresiones Lambda

#### Explicación

**Intuición.** Una lambda es una forma de escribir una **función sin nombre** ("anónima"), de manera muy compacta, cuando esa función se va a usar una sola vez o se va a pasar como argumento a otra función.

**Definición formal:** es una forma concisa de definir métodos anónimos. Puede contener expresiones o instrucciones.

| Elementos de una función/método tradicional | Elementos de una expresión lambda |
|---|---|
| Tipo de retorno | (a veces inferido por el contexto) |
| Nombre | (no tiene — es anónima) |
| Lista de parámetros | Lista de parámetros |
| Cuerpo con instrucciones | Expresión o cuerpo con instrucciones |

El operador principal es `=>` (tiene la misma precedencia que la asignación `=`).

**Sintaxis (dos formas):**
```csharp
(lista_de_parametros) => expresion                 // lambda de EXPRESIÓN, no usa "return"
(lista_de_parametros) => { secuencia; return ...; } // lambda de INSTRUCCIÓN (necesita delegado)
```
Los paréntesis son opcionales si hay un único parámetro. Se deben poner `()` vacíos si no recibe parámetros.

#### Ejemplos

**C#:**
```csharp
// Lambda de expresión
Func<int, int> alCuadrado = (a) => a * a;

// Lambda de instrucción
Func<int, int, double> potencia = (a, b) => { return Math.Pow(a, b); };

// Con condicional ternario dentro
Func<long, bool> cumplePpto = vtas => vtas > 180000 ? true : false;

// Con Random
Random alea = new Random();
Func<int, int> productoAleatorio = a => a * alea.Next(10, 20);
```

**Java (equivalente con interfaces funcionales):**
```java
import java.util.function.Function;
import java.util.function.BiFunction;

Function<Integer, Integer> alCuadrado = a -> a * a;
BiFunction<Integer, Integer, Double> potencia = (a, b) -> Math.pow(a, b);
```
> Nota: en Java, para usar una lambda necesitas una **interfaz funcional** (una interfaz con un solo método abstracto), como `Function<T,R>` o `BiFunction<T,U,R>` (parte del paquete `java.util.function`, equivalente conceptual a los delegados `Func`/`Action` de C# que veremos en el Tema 3.4).

#### Errores comunes
- Olvidar que una lambda de **instrucción** (con `{ }` y `return`) necesita estar tipada con un **delegado** compatible (`Func`/`Action` en C#) — no puedes asignarla a `var` sin contexto.
- Confundir el operador `=>` de las lambdas con el operador de "flecha" de otros lenguajes con significados distintos.

#### Relaciones
Las lambdas son el corazón del paradigma funcional en C# y la base de: Predicados (3.3), Delegados/Func/Action (3.4), y LINQ (3.5) — casi todo lo que sigue en este Día 3 usa lambdas como "pegamento" sintáctico.

#### Resumen
- Lambda = función anónima y compacta; operador `=>`.
- Dos formas: de expresión (sin `return`) y de instrucción (con `{ }` y `return`, necesita delegado).

#### Ejercicios
1. Escribe una lambda de expresión que reciba un `int` y devuelva si es par.
2. Escribe una lambda de instrucción que reciba dos `double` y devuelva su promedio, validando que no sean negativos (lanzando excepción si lo son).
3. **(V/F)** Toda lambda necesita un nombre. → Falso, son anónimas.

## Tema 3.3 — Predicados

#### Explicación

**Definición formal:** un predicado es una expresión que evalúa si se cumple o no una condición, devolviendo siempre un `bool`. En C#, la sintaxis típica es `Predicate<T>`, donde `T` es el tipo del parámetro que recibe. Se usa comúnmente con lambdas y, si se aplica sobre una colección (`enumerable`), el parámetro representa cada elemento de esa colección al momento de evaluar.

#### Ejemplos

**C#:**
```csharp
var numeros = new List<int> { 3, 7, 10, 15, 20, 22, 25 };

Predicate<int> esImpar = x => x % 2 != 0;
Predicate<int> esMayorDiez = x => x > 10;

var impares = numeros.FindAll(esImpar);        // [3, 7, 15, 25]
var mayores10 = numeros.FindAll(esMayorDiez);  // [15, 20, 22, 25]
```

> Nota de tu propio material: si necesitas un predicado que devuelva algo más elaborado, o que se combine con otras operaciones (no solo filtrar), ya "necesitas usar una lambda con delegado `Func`" — lo veremos en el Tema 3.4.

**Java (equivalente con `Predicate<T>` del paquete `java.util.function`, mismo nombre, mismo concepto):**
```java
import java.util.function.Predicate;
import java.util.List;
import java.util.stream.Collectors;

List<Integer> numeros = List.of(3, 7, 10, 15, 20, 22, 25);
Predicate<Integer> esImpar = x -> x % 2 != 0;
List<Integer> impares = numeros.stream().filter(esImpar).collect(Collectors.toList());
```

#### Errores comunes
- Confundir `Predicate<T>` con `Func<T, bool>` — son conceptualmente equivalentes (ambos devuelven bool), pero `Predicate<T>` está pensado específicamente para "evaluar una condición sobre un elemento", mientras que `Func` es más general.
- Intentar usar un predicado para transformar datos (no es su propósito — solo evalúa verdadero/falso, no transforma).

#### Relaciones
Los predicados son el primer paso hacia las operaciones de **Filtrar** de LINQ (Tema 3.5, método `Where`) — de hecho, son casi lo mismo conceptualmente.

#### Resumen
- `Predicate<T>` evalúa una condición sobre un valor de tipo `T`, siempre devuelve `bool`.
- Muy usado con colecciones para filtrar (`FindAll`).

#### Ejercicios
1. Escribe un `Predicate<string>` que verifique si un string tiene más de 5 caracteres.
2. Usa `FindAll` con ese predicado sobre una lista de nombres.

## Tema 3.4 — Delegados (simples, con parámetros, genéricos, multicast) y Func/Action

#### Explicación

**Intuición.** Un delegado es como un **control remoto universal**: en vez de guardar un botón físico conectado a un aparato específico, guardas una "referencia apuntadora" que puede apuntar a **cualquier método** (de cualquier clase accesible) que tenga la misma firma (mismos parámetros, mismo tipo de retorno). Apretar el botón del control remoto ejecuta lo que sea que esté "apuntado" en ese momento.

**Definición formal:** es un concepto de programación funcional fundamental antes de abordar otros paradigmas (tu material insiste: "los controladores de eventos no son más que métodos que se invocan a través de delegados" — anticipo directo del Tema 3.6). Es un **tipo** que representa referencias a métodos con una lista de parámetros y un tipo de retorno determinados. Al crear una instancia del delegado, puedes asociarle cualquier método compatible.

**Sintaxis de declaración:**
```
[privacidad] delegate [TipoRetorno] [NombreDelegado]([Parámetros])
```
```csharp
internal delegate string DelegadoS(string mensaje);
public delegate int DelegadoI(int val1, int val2);
```
> Nota de tu material: el modificador de accesibilidad de un delegado NO puede ser `private` ni `protected` a nivel de declaración de tipo de primer nivel.

**Delegado genérico:** utiliza una firma genérica (con `T`), donde el tipo se fija cuando se asigna el método concreto:
```csharp
delegate T NombreDelegado<T>(T parametro);
```

**Multicast:** un delegado puede apuntar a **más de un método a la vez** (encadenados con `+=`), y al invocarlo se ejecutan todos en orden.

**`Func<>` y `Action<>`** son delegados **genéricos predefinidos** por .NET, para no tener que declarar tu propio `delegate` cada vez:
- **`Action<T...>`**: representa una lambda que **NO devuelve valor** (equivalente a `void`). `Action<T>` recibe un parámetro; `Action<T1,T2>` recibe dos, etc.
- **`Func<T..., TResult>`**: representa una lambda que **SÍ devuelve un valor** de tipo `TResult`. `Func<TResult>` no recibe parámetros; `Func<T,TResult>` recibe uno; así hasta 16 parámetros en C#.

#### Ejemplos

**C# — delegado simple apuntando a un método normal:**
```csharp
delegate bool VerificadorPrimo(int numero);

class Program {
    static void Main() {
        VerificadorPrimo dEsPrimo = EsPrimo;   // el delegado APUNTA a la función EsPrimo
        int n = 17;
        Console.WriteLine($"{n} es primo? {dEsPrimo(n)}");
    }

    static bool EsPrimo(int num) {
        if (num <= 1) return false;
        if (num == 2) return true;
        if (num % 2 == 0) return false;
        for (int i = 3; i * i <= num; i += 2)
            if (num % i == 0) return false;
        return true;
    }
}
```

**C# — Action (no retorna valor):**
```csharp
Action<bool> establecerEstado = es => Console.WriteLine("Se estableció el estado a: " + es);
establecerEstado(true);
```

**C# — Func (retorna valor), incluyendo con dos parámetros:**
```csharp
Func<int, bool> esPrimoLambda = n => {
    if (n <= 1) return false;
    if (n == 2) return true;
    if (n % 2 == 0) return false;
    for (int i = 3; i * i <= n; i += 2) if (n % i == 0) return false;
    return true;
};

Func<double, double, double> sumar = (x, y) => x + y;
Func<double, double, double> dividir = (x, y) => {
    if (y == 0) throw new DivideByZeroException("No se puede dividir por cero.");
    return x / y;
};
```

**C# — Func que recibe una lista y retorna una tupla (dos valores):**
```csharp
Func<List<int>, (int suma, float promedio)> calcular = lista => {
    int suma = lista.Sum();
    float promedio = lista.Count > 0 ? (float)suma / lista.Count : 0;
    return (suma, promedio);
};
var resultado = calcular(new List<int> { 10, 20, 30, 40, 50 });
Console.WriteLine($"Suma: {resultado.suma}, Promedio: {resultado.promedio}");
```

**Multicast:**
```csharp
Action saludo = () => Console.WriteLine("Hola");
saludo += () => Console.WriteLine("¿Cómo estás?");
saludo();  // imprime AMBAS líneas, en orden
```

**Java (equivalente conceptual usando interfaces funcionales):**
```java
import java.util.function.Function;
import java.util.function.BiFunction;
import java.util.function.Consumer;

// "Func<T,TResult>" equivale a Function<T,R>; "Action<T>" equivale a Consumer<T>
Function<Integer, Boolean> esPrimo = n -> { /* misma lógica */ return true; };
BiFunction<Double, Double, Double> sumar = (x, y) -> x + y;
Consumer<Boolean> establecerEstado = es -> System.out.println("Se estableció el estado a: " + es);
```
> ⚠️ **Importante:** Java **no tiene delegados** como tipo de lenguaje (no existe la palabra clave `delegate`). Lo más parecido conceptualmente son las **interfaces funcionales** (`Function`, `BiFunction`, `Consumer`, `Supplier`, `Runnable`) combinadas con **referencias a métodos** (`Clase::metodo`). No hay "multicast" nativo como en C# — para encadenar acciones en Java se usan métodos como `andThen()` en `Consumer`/`Function`.

#### Errores comunes
- No entender qué significa "apuntar a un método": el delegado **no ejecuta nada por sí solo** al declararse; solo ejecuta cuando lo **invocas** con `()`, como en `dEsPrimo(n)`.
- Confundir `Func<int,int>` (recibe 1 parámetro `int`, devuelve `int` — dos genéricos porque el último es SIEMPRE el retorno) con `Func<int,int,int>` (recibe 2 parámetros `int`, devuelve `int`).
- Usar `Action` cuando la lambda sí devuelve algo (no compila; `Action` es exclusivamente para `void`).
- El ejemplo trampa de tu propio material: una lambda de instrucción que **captura una variable externa** (`j`) y la modifica dentro de la lambda — el valor de `j` cambia realmente fuera de la lambda también, porque las lambdas en C# capturan variables **por referencia** (closures), no por copia. Esto sorprende a muchos estudiantes.

```csharp
int j = 6;
Func<int, int> funcion = i => {
    j = j * j;        // esto MODIFICA la variable externa j
    return 100 + i + j;
};
int resultado = funcion(50); // j ahora vale 36 permanentemente, fuera de la lambda también
```

#### Relaciones
Los delegados son la base técnica de: los **Eventos** (Tema 3.6, un evento es literalmente un delegado especial con restricciones de uso), y son el mecanismo que hace posible pasar comportamiento como si fuera un dato (esto es la esencia del **Principio Open/Closed** del Día 4, cuando en vez de usar herencia usas **inyección de una función/estrategia** para extender comportamiento sin modificar código).

#### Resumen
- Delegado = tipo que referencia métodos con firma compatible; se invoca con `()`.
- `Action<>` = no retorna valor; `Func<>` = sí retorna valor (último genérico = tipo de retorno).
- Multicast: varios métodos encadenados con `+=`, se ejecutan todos al invocar.
- Las lambdas capturan variables externas por referencia (closures) — pueden modificarlas permanentemente.

#### Ejercicios
1. Declara un delegado `Operacion` que reciba dos `int` y devuelva `int`; asígnale una lambda de suma y otra de multiplicación (en variables distintas) y pruébalas.
2. **(Trampa)** ¿Qué imprime este código?
```csharp
int contador = 0;
Action incrementar = () => contador++;
incrementar(); incrementar(); incrementar();
Console.WriteLine(contador);
```
*(Imprime 3 — la lambda capturó `contador` por referencia y lo modificó cada vez.)*
3. Usa `Func<int,int,(int suma, int resta)>` para devolver dos resultados a partir de dos números.
4. **(V/F)** `Action<int,int>` puede devolver un valor `int`. → Falso, `Action` nunca devuelve valor.

## Tema 3.5 — Funciones de Orden Superior y LINQ

#### Explicación

**Definición formal:** una función de orden superior es una función que **acepta otra función como parámetro** y/o **devuelve una función como resultado**. Es el pilar de la programación funcional. El término se refiere básicamente a tres funciones: **Map** (transformar cada elemento), **Filter** (quedarse con los que cumplen una condición) y **Fold/Reduce** (combinar todos los elementos en un solo resultado, ej. una suma total).

**LINQ (Language Integrated Query):** en .NET, es una extensión que provee métodos que siguen el paradigma funcional, simplificando el trabajo con distintas fuentes de datos: cualquier objeto que implemente `IEnumerable<T>` (arreglos, listas), SQL, XML.

**Categorías de operaciones LINQ (de tu material):**

| Categoría | Métodos |
|---|---|
| Cuantificar | `All`, `Any`, `Contains` |
| Filtrar (Filter) | `Where`, `OfType` |
| Transformar (Map) | `Select`, `Zip` |
| Criterios de conjunto | `Distinct`, `Except`, `Intersect`, `Union` |
| Ordenamiento | `OrderBy`, `OrderByDescending`, `ThenBy`, `ThenByDescending`, `Reverse` |
| Agregación (Fold) | `Aggregate`, `Average`, `Count`, `LongCount`, `Max`, `Min`, `Sum` |
| Partir/Unir | `Skip`, `SkipWhile`, `Take`, `TakeWhile`, `Join`, `GroupJoin` |
| Agrupamiento | `GroupBy`, `ToLookup` |

#### Ejemplos

**C# — LINQ con listas de tipos primitivos:**
```csharp
List<int> numeros = new List<int> { 3, 7, 10, 15, 20, 22, 25 };

var pares      = numeros.Where(n => n % 2 == 0).ToList();      // Filter: [10, 20, 22]
var cuadrados  = numeros.Select(n => n * n).ToList();          // Map: [9, 49, 100, ...]
var suma       = numeros.Sum();                                 // Fold: 102
var ordenados  = numeros.OrderByDescending(n => n).ToList();    // [25, 22, 20, 15, 10, 7, 3]
var mayoresA10 = numeros.Count(n => n > 10);                    // Cuantificar: 4
```

**C# — LINQ con clases/objetos (ejemplo real de tu curso, clase `Paciente`):**
```csharp
class Paciente {
    internal string name;
    internal int edad;
    internal int saldo;
    public Paciente(string name, int edad, int saldo) {
        this.name = name; this.edad = edad; this.saldo = saldo;
    }
}

List<Paciente> lPac = new List<Paciente> {
    new Paciente("Juan", 39, 5000000),
    new Paciente("Pedro", 86, 800000),
    new Paciente("Ana", 18, 700000),
    new Paciente("Elena", 16, 4890000),
};

var sumSaldos = lPac.Sum(elem => elem.saldo);
Console.WriteLine("Total de saldos: " + sumSaldos);

var pacXEdad = lPac.OrderBy(elem => elem.edad).ToList();
Console.WriteLine("Lista de nombres ordenados por edad:");
pacXEdad.ForEach(elem => Console.WriteLine(elem.name));

var consultaEdad = lPac.Where(w => w.edad >= 18);            // mayores de edad
var sumMayores = lPac.Where(elem => elem.edad >= 18).Sum(elem => elem.saldo);

Action<Paciente> imprimirFicha = (Paciente arg) =>
    Console.WriteLine($"Paciente {arg.name} Edad {arg.edad}");
pacXEdad.ForEach(elem => imprimirFicha(elem));
```

**Java (equivalente con Streams — el "LINQ de Java"):**
```java
List<Integer> numeros = List.of(3, 7, 10, 15, 20, 22, 25);

List<Integer> pares = numeros.stream().filter(n -> n % 2 == 0).collect(Collectors.toList());
List<Integer> cuadrados = numeros.stream().map(n -> n * n).collect(Collectors.toList());
int suma = numeros.stream().mapToInt(Integer::intValue).sum();
List<Integer> ordenadosDesc = numeros.stream().sorted(Comparator.reverseOrder()).collect(Collectors.toList());
```
> Nota: Java `Stream` cubre exactamente el mismo rol funcional que LINQ (`filter`≈`Where`, `map`≈`Select`, `sorted`≈`OrderBy`, `reduce`≈`Aggregate`, `collect`≈materializar a lista). La diferencia de fondo es que LINQ en C# también sabe traducirse a **consultas SQL** (LINQ to SQL/Entity Framework), mientras que Streams de Java es solo para colecciones en memoria.

#### Errores comunes
- Olvidar `.ToList()` (o `.collect()` en Java) al final de una cadena LINQ/Stream — sin materializar, tienes una consulta "perezosa" (`IEnumerable`) que se re-ejecuta cada vez que la recorres, lo cual puede ser ineficiente o dar resultados inesperados si la fuente de datos cambió entre medias.
- Encadenar demasiadas operaciones LINQ en una sola línea ilegible — se recomienda dividir en pasos con nombres descriptivos (como en el ejemplo de `Paciente` arriba).
- Confundir `Where` (filtra, devuelve varios) con `First`/`Single` (devuelve exactamente uno, lanza excepción si no cumple las condiciones esperadas).

#### Relaciones
LINQ es la aplicación práctica y más usada de las Funciones de Orden Superior + Delegados (`Func`) + Lambdas — es donde todo lo del Día 3 hasta ahora **converge** en herramientas de uso diario real. También es la base de por qué el paradigma funcional es tan valorado en arquitectura moderna (Día 3, más abajo): favorece la ejecución paralela porque no hay estado compartido mutando.

#### Resumen
- Funciones de orden superior: Map, Filter, Fold.
- LINQ: extensión de .NET con esas funciones (y más), para cualquier `IEnumerable<T>`, SQL, XML.
- Categorías: cuantificar, filtrar, transformar, criterios, ordenamiento, agregación, partir/unir, agrupamiento.

#### Ejercicios
1. Con la lista de `Paciente`s de arriba, escribe LINQ para obtener el nombre del paciente con mayor saldo.
2. Escribe LINQ para agrupar pacientes en "mayores de edad" y "menores de edad" usando `GroupBy`.
3. **(Java)** Traduce el ejercicio 1 usando Streams.

## Tema 3.6 — Tipos Anónimos e Inmutabilidad

#### Explicación

**Tipos anónimos:** son clases simples que se crean "al vuelo" (sin declarar una clase formal) para almacenar un conjunto de valores. Se crean con `new` seguido de `{ }`, especificando los valores dentro. Se usan comúnmente junto con LINQ (por ejemplo, para proyectar solo algunos campos de un objeto complejo con `Select`).

```csharp
var libro = new { titulo = "Cien Años de Soledad", valor = 45000 };
var libros = new[] {
    new { Nombre = "Cien Años de Soledad", valor = 45000, editorial = "Oveja Negra" },
    new { Nombre = "C# Libro de Referencia", valor = 60000, editorial = "McKensey" },
};
```
> En Java no existen los tipos anónimos con esta sintaxis; el equivalente más cercano son los `record` (Java 16+) o clases internas anónimas, pero no son "al vuelo" de la misma manera. Para tu examen (C#), la sintaxis de arriba es la que debes dominar.

**Mutabilidad vs. Inmutabilidad:**
- **Mutabilidad** (comportamiento por defecto de POO clásica): los objetos de una clase **pueden cambiar** una vez creados; sus valores se actualizan. Son más difíciles de mantener y depurar (errores frecuentes: "variables que deberían tener un valor y tienen otro, o no lo tienen").
- **Inmutabilidad** (Programación Funcional Estricta): los objetos **no pueden cambiar** una vez creados. Si necesitas "cambiar" un valor, en realidad **creas una copia nueva** con el valor actualizado (el objeto original permanece intacto). En C#, hasta ahora habías usado `const` y `readonly` como acercamientos parciales a esto.

**Beneficios de la inmutabilidad:** más fácil de entender y mantener; fácil de probar; código más seguro; alta reducción de problemas de sincronización entre hilos (concurrencia); controla efectos colaterales.

**Desventaja:** sobrecosto de generar objetos nuevos cada vez que "cambia" un atributo — puede degradar el rendimiento si se abusa en objetos grandes o de alta frecuencia de cambio.

**Cuándo usarla:**
- **Sí usar:** objetos simples/pequeños, fáciles de duplicar; programación concurrente (hilos).
- **No usar:** objetos grandes donde no vale la pena retornar una instancia completa por un solo cambio; objetos que se "van poblando" gradualmente (ej. llenado de sillas de un avión, uno por uno).

#### Ejemplos

**C# — clase mutable (ejemplo real de tu curso):**
```csharp
public class Cuenta {
    public ulong Saldo { get; set; }
    public void Depositar(ulong valor) { Saldo += valor; }
}
// main
Cuenta cta = new Cuenta();
cta.Saldo = 300000;
Console.WriteLine(cta.GetHashCode());
cta.Depositar(1000000);
Console.WriteLine(cta.GetHashCode());
// Es la MISMA instancia (mismo hash) en ambos casos: cta cambió sus valores internamente.
```

**C# — la misma clase, versión inmutable:**
```csharp
public class Cuenta {
    public ulong Saldo { get; }                     // solo get, no set
    public Cuenta(ulong saldo) { Saldo = saldo; }
    public Cuenta Depositar(ulong valor) => new Cuenta(Saldo + valor);  // devuelve OBJETO NUEVO
}
// main
Cuenta cta = new Cuenta(30000);
Console.WriteLine(cta.GetHashCode());
cta = cta.Depositar(1000000);      // cta ahora APUNTA a un objeto NUEVO y distinto
Console.WriteLine(cta.GetHashCode());  // el hash cambia: es otra instancia
```

**Java (equivalente inmutable, usando `final`):**
```java
public final class Cuenta {
    private final long saldo;
    public Cuenta(long saldo) { this.saldo = saldo; }
    public long getSaldo() { return saldo; }
    public Cuenta depositar(long valor) { return new Cuenta(saldo + valor); } // objeto nuevo
}
```

#### Errores comunes
- Modificar un objeto "inmutable" reasignando la variable, pero seguir creyendo que es "el mismo objeto" — conceptualmente son objetos distintos en memoria (aunque la variable tenga el mismo nombre).
- Usar inmutabilidad en un objeto que se llena gradualmente campo por campo (anti-patrón según tu propio material) — generarías cientos de copias innecesarias.
- Olvidar el sobrecosto de rendimiento al aplicar inmutabilidad indiscriminadamente en sistemas de alta frecuencia de actualización.

#### Relaciones
La inmutabilidad es clave para entender por qué el paradigma funcional favorece la **ejecución paralela** (Tema 3.1 de la introducción): si un objeto no puede cambiar, nunca hay una "condición de carrera" entre dos hilos que intenten modificarlo simultáneamente. Es también una idea que reaparecerá cuando hablemos de **Value Objects** (concepto complementario, Día 4, LSP).

#### Resumen
- Tipos anónimos: clases "al vuelo" con `new { }`, usados con LINQ.
- Mutabilidad: el objeto cambia sus propios valores (mismo hash/instancia).
- Inmutabilidad: "cambiar" implica crear una copia nueva (hash/instancia distinta); ideal para concurrencia y objetos simples.

#### Ejercicios
1. Convierte esta clase mutable a inmutable: `public class Punto { public int X {get;set;} public int Y {get;set;} }`.
2. **(V/F)** En una clase inmutable, el método que "modifica" un valor debe devolver una nueva instancia. → Verdadero.
3. Crea un tipo anónimo en C# que represente un `Empleado` con `nombre` y `salario`.

### 🧪 Mini examen — Bloques 1-3 del Día 3 (Funcional)
1. ¿Qué es una lambda y cuál es su operador principal?
2. Diferencia entre `Predicate<T>` y `Func<T,bool>`.
3. ¿Qué es un delegado y cómo se relaciona con los eventos (adelanto)?
4. Explica qué es multicast en delegados.
5. ¿Qué hace `Select` en LINQ? ¿Y `Where`? ¿Y `Sum`?
6. Da un ejemplo propio de clase inmutable en C#.
7. **(Trampa)** ¿Por qué una lambda que modifica una variable externa puede sorprender a un programador que no conoce "closures"?


## Tema 3.7 — Programación Orientada a Eventos

#### Explicación

**Intuición.** Piensa en un timbre de la puerta. Tú (el objeto "Casa") no estás constantemente preguntando "¿tocaron el timbre? ¿tocaron el timbre? ¿tocaron el timbre?" (eso sería *polling*, ineficiente). En cambio, **te suscribes** al evento "alguien tocó el timbre", y cuando pasa, **reaccionas** automáticamente (vas a abrir la puerta). Ese patrón de "avisar cuando algo pasa, en vez de estar preguntando todo el tiempo" es la Programación Orientada a Eventos.

**Definición formal:** paradigma de programación en el que el flujo del programa está determinado por sucesos (eventos): acciones del usuario, sensores, o mensajes de otros programas/hilos. Se apoya en el patrón **Publisher/Subscriber** (Publicador/Suscriptor):
- El **Publisher** (Publicador) es quien **declara y dispara (`invoke`)** el evento cuando algo relevante sucede.
- El **Subscriber** (Suscriptor) es quien **se suscribe** a ese evento y define qué hacer cuando ocurre (el "manejador" o *handler*).

Un evento en C# es, técnicamente, un **delegado especial** (recuerda el Tema 3.4: "los controladores de eventos no son más que métodos que se invocan a través de delegados"), con la palabra clave `event`, que restringe cómo se puede usar desde fuera de la clase (solo se puede suscribir `+=`/desuscribir `-=`, **no** se puede invocar ni reasignar directamente desde fuera de la clase publicadora — esto protege la integridad del mecanismo).

#### Ejemplos

**Escenario real de tu material: la Taquilla de un Parque de Diversiones que se queda sin boletas dispara un evento.**

**C#:**
```csharp
// 1. Se declara el delegado que define la "forma" del evento
public delegate void SinBoletasHandler(string mensaje);

public class Taquilla {
    private int boletasDisponibles;

    // 2. Se declara el EVENTO usando ese delegado
    public event SinBoletasHandler SinBoletas;

    public Taquilla(int boletasIniciales) { boletasDisponibles = boletasIniciales; }

    public void VenderBoleta() {
        if (boletasDisponibles > 0) {
            boletasDisponibles--;
            Console.WriteLine("Boleta vendida. Quedan: " + boletasDisponibles);
        }
        if (boletasDisponibles == 0) {
            // 3. Se DISPARA el evento (Publisher avisando)
            SinBoletas?.Invoke("¡Se agotaron las boletas!");
        }
    }
}

public class Administrador {
    // 4. El Subscriber define el "manejador" (handler)
    public void NotificarReposicion(string mensaje) {
        Console.WriteLine("[Administrador] " + mensaje + " -> Voy a pedir más boletas.");
    }
}

// main
Taquilla taquilla = new Taquilla(2);
Administrador admin = new Administrador();
taquilla.SinBoletas += admin.NotificarReposicion;  // SUSCRIPCIÓN
taquilla.VenderBoleta();
taquilla.VenderBoleta(); // dispara el evento -> se ejecuta NotificarReposicion automáticamente
```

**Java (equivalente conceptual — patrón Observer/Listener manual, porque Java no tiene la palabra clave `event`):**
```java
interface SinBoletasListener { void onSinBoletas(String mensaje); }

class Taquilla {
    private int boletasDisponibles;
    private List<SinBoletasListener> listeners = new ArrayList<>();

    Taquilla(int inicial) { boletasDisponibles = inicial; }
    void suscribir(SinBoletasListener l) { listeners.add(l); }

    void venderBoleta() {
        boletasDisponibles--;
        if (boletasDisponibles == 0) {
            for (SinBoletasListener l : listeners) l.onSinBoletas("¡Se agotaron las boletas!");
        }
    }
}
```
> ⚠️ Java **no tiene** `event` como construcción del lenguaje. El equivalente conceptual es el **patrón de diseño Observer** implementado manualmente (como arriba), o el uso de librerías/frameworks (ej. `ActionListener` en Swing, o Spring `ApplicationEventPublisher` en aplicaciones empresariales). Para tu examen (C#), usa la sintaxis `event`/`+=` mostrada arriba.

#### Errores comunes
- Intentar **invocar** un evento (`taquilla.SinBoletas("mensaje")`) desde **fuera** de la clase publicadora — no compila; solo la propia clase puede invocar su evento (`SinBoletas?.Invoke(...)`), por diseño, para que nadie externo pueda "falsificar" el disparo del evento.
- Olvidar el operador `?.` antes de `Invoke` — si nadie se ha suscrito, el evento es `null`, y `SinBoletas.Invoke(...)` sin el `?.` lanzaría `NullReferenceException`.
- Confundir Publisher con Subscriber: el Publisher **declara y dispara**; el Subscriber **reacciona**.

#### Relaciones
Los Eventos son una aplicación directa de los **Delegados** (Tema 3.4) — de hecho, técnicamente `event` es "un delegado con superpoderes de protección". Este patrón es también la base conceptual del estilo arquitectónico **Event-Driven Architecture** que verás más abajo (Tema 3.10) y de arquitecturas reactivas modernas.

#### Resumen
- Publisher declara y dispara el evento; Subscriber se suscribe y reacciona.
- Un evento es un delegado especial (`event`), protegido: solo se suscribe/desuscribe desde fuera, solo se invoca desde dentro.
- Java no tiene `event` nativo; se simula con el patrón Observer/Listener.

#### Ejercicios
1. Diseña un evento `StockAgotado` para una clase `Inventario`, con un método `VenderProducto()` que lo dispare cuando el stock llegue a 0.
2. **(V/F)** Cualquier clase externa puede invocar directamente un evento declarado en otra clase. → Falso.
3. **(Conceptual)** Explica la relación entre delegados y eventos con tus palabras.

## Tema 3.8 — Programación Orientada a Aspectos (AOP)

#### Explicación

**Intuición.** Imagina que en TODOS los métodos de TODAS las clases de tu sistema necesitas: registrar un log ("se llamó a este método a esta hora"), verificar seguridad ("¿el usuario tiene permiso?"), y medir el tiempo que tarda. Si escribes ese código **dentro** de cada método, uno por uno, terminarás con la misma lógica repetida cientos de veces, mezclada con la lógica de negocio real — ensuciando SRP (Día 4). AOP existe para **extraer** esas preocupaciones que "cruzan" transversalmente a toda la aplicación, en un solo lugar.

**Definición formal:** paradigma de programación que busca la modularización de preocupaciones que afectan a otros módulos (preocupaciones transversales o *cross-cutting concerns*), como logging, transacciones o seguridad, separándolas de la lógica de negocio principal.

**Vocabulario clave (de tu material):**
- **Aspecto (Aspect):** módulo que encapsula una preocupación transversal (ej. `AspectoLogging`).
- **Advice (Consejo):** la acción concreta que toma un aspecto en un punto específico (ej. "antes de ejecutar el método, imprime el log"). Puede ser `Before`, `After`, `Around`.
- **Punto de Corte (Pointcut):** una expresión que define **dónde** se debe aplicar un advice (ej. "todos los métodos públicos que empiecen con `Guardar`").
- **Punto de Unión (Join point):** un punto específico durante la ejecución del programa (una llamada a un método, el manejo de una excepción) donde un aspecto puede insertarse.
- **Tejido (Weaving):** el proceso de combinar los aspectos con el código principal (puede ser en tiempo de compilación, de carga o de ejecución).

**Cómo se implementa en C# (mecanismo real):** C# no tiene AOP nativo como palabra clave del lenguaje. Se logra mediante **Proxies dinámicos** — un objeto "intermediario" que se interpone entre quien llama al método y el método real, ejecutando el advice antes/después/alrededor de la llamada real. Se implementa típicamente con `DispatchProxy` (incluido en .NET) o librerías como Castle DynamicProxy.

#### Ejemplos

**C# — usando `DispatchProxy` (mecanismo real de .NET para AOP):**
```csharp
public interface ICalculadora { int Sumar(int a, int b); }

public class Calculadora : ICalculadora {
    public int Sumar(int a, int b) => a + b;
}

// El "Aspecto" de logging, implementado como Proxy
public class LoggingProxy<T> : DispatchProxy {
    private T _decorado;

    protected override object Invoke(System.Reflection.MethodInfo targetMethod, object[] args) {
        Console.WriteLine($"[LOG] Antes de llamar a {targetMethod.Name}");   // Advice "Before"
        var resultado = targetMethod.Invoke(_decorado, args);                // Join point real
        Console.WriteLine($"[LOG] Después de llamar a {targetMethod.Name}, resultado: {resultado}"); // Advice "After"
        return resultado;
    }

    public static T Crear(T decorado) {
        object proxy = Create<T, LoggingProxy<T>>();
        ((LoggingProxy<T>)proxy)._decorado = decorado;
        return (T)proxy;
    }
}

// main
ICalculadora calc = LoggingProxy<ICalculadora>.Crear(new Calculadora());
int resultado = calc.Sumar(3, 4);   // se registra automáticamente el log antes y después, SIN tocar Calculadora
```

**Java (equivalente con frameworks reales — Spring AOP / AspectJ, el estándar de la industria en Java):**
```java
@Aspect
@Component
public class LoggingAspect {
    @Around("execution(* com.app.Calculadora.sumar(..))")   // Pointcut
    public Object logAlrededor(ProceedingJoinPoint pjp) throws Throwable {
        System.out.println("[LOG] Antes de: " + pjp.getSignature());
        Object resultado = pjp.proceed();  // ejecuta el método real (join point)
        System.out.println("[LOG] Después de: " + resultado);
        return resultado;
    }
}
```
> En Java, AOP **sí** es un paradigma consolidado en frameworks empresariales (Spring AOP, AspectJ) — de hecho, es MÁS común verlo en Java/Spring que en C#. Para tu examen (que usa C#), el concepto vale igual, pero la implementación mostrada con `DispatchProxy` es la más fiel a lo que tu profesor esperaría en .NET puro.

#### Errores comunes
- Confundir AOP con Herencia/Interfaces como forma de compartir comportamiento — AOP resuelve un problema distinto: preocupaciones que no encajan naturalmente en una jerarquía de clases porque **cruzan** muchas clases no relacionadas entre sí (logging le aplica a `Calculadora`, `Factura`, `Usuario`... clases sin parentesco).
- Meter lógica de negocio dentro de un aspecto — un aspecto debe ser una preocupación *técnica transversal* (logging, seguridad, transacciones, cache), no reglas de negocio del dominio.
- Pensar que el Pointcut "modifica" el código fuente original — no lo hace; el tejido ocurre en tiempo de compilación/ejecución, sin tocar el archivo fuente del método real.

#### Relaciones
AOP es la solución arquitectónica al problema que el **Principio de Responsabilidad Única (SRP)**, por sí solo (usando solo herencia/composición), no puede resolver del todo: cuando una preocupación transversal (logging, seguridad) aplicaría a **decenas** de clases no relacionadas, ni la herencia ni la composición simple bastan de forma limpia — se necesita un mecanismo ortogonal como AOP. Es el ejemplo perfecto de por qué "reorganizamos" tu índice para que AOP viniera justo antes de SOLID conceptualmente (aunque en este documento SOLID quedó en el Día 4 por razones de profundidad).

#### Resumen
- AOP modulariza preocupaciones transversales (logging, seguridad, transacciones) separándolas de la lógica de negocio.
- Vocabulario: Aspecto, Advice (Before/After/Around), Pointcut, Join point, Weaving (tejido).
- En C# se implementa con Proxies dinámicos (`DispatchProxy`); en Java, con Spring AOP/AspectJ.

#### Ejercicios
1. **(Conceptual)** ¿Por qué el logging es un buen candidato para AOP y no para herencia?
2. Diseña (conceptualmente, sin código) un aspecto de "Seguridad" que verifique permisos antes de ejecutar cualquier método marcado como "sensible".
3. **(V/F)** El tejido (weaving) modifica el archivo fuente del método real. → Falso.


## Tema 3.9 — Introducción a la Arquitectura de Software

#### Explicación

**Intuición.** Puedes construir una casa sin planos (juntando ladrillos "a ojo") y quizás no se caiga... hasta que quieras agregar un segundo piso, o hasta que un terremoto la ponga a prueba. La Arquitectura de Software es exactamente el plano estructural que dice **cómo se organizan las partes grandes** de un sistema, para que pueda crecer, cambiar y resistir el paso del tiempo sin derrumbarse.

**Definición formal:** la arquitectura de software es la organización fundamental de un sistema, expresada en sus componentes, las relaciones entre ellos y con el entorno, y los principios que guían su diseño y evolución.

**¿Qué pasa SIN arquitectura?** (motivación real del tema): sistemas difíciles de mantener, cambios que rompen otras partes sin relación aparente, imposibilidad de escalar, altísimo costo de agregar nuevas funcionalidades, dependencia excesiva de las personas que "se saben de memoria" cómo funciona el sistema.

**¿Qué da tener arquitectura?** Comunicación clara entre los equipos de desarrollo (todos hablan el mismo "mapa"), decisiones tempranas que evitan retrabajos costosos, un sistema que se puede analizar antes de construirlo (como los planos de un edificio antes de la obra).

**Modelo de las 4+1 Vistas** (Kruchten) — cómo se documenta una arquitectura desde distintos ángulos, cada uno respondiendo una pregunta distinta:

| Vista | Pregunta que responde | Para quién |
|---|---|---|
| **Lógica** | ¿Qué funcionalidades ofrece el sistema? | Analistas, usuarios finales |
| **De Procesos** | ¿Cómo se ejecuta y coordina en tiempo real (concurrencia, hilos)? | Integradores de sistema |
| **De Desarrollo** | ¿Cómo se organiza el código en módulos/capas para los programadores? | Programadores, gestión de software |
| **Física (Despliegue)** | ¿En qué máquinas/servidores se instala cada parte? | Ingenieros de sistemas/infraestructura |
| **+1: Casos de Uso** (Escenarios) | ¿Cómo validan las otras 4 vistas que el sistema cumple los requisitos reales? | Todos los interesados — es la vista "que amarra" a las demás |

**TOGAF** (mencionado en tu material como marco para diseñar arquitectura de aplicaciones): es un framework de arquitectura empresarial que ayuda a diseñar, planear e implementar arquitecturas alineadas con los objetivos del negocio, cubriendo desde la arquitectura de negocio hasta la de datos, aplicaciones y tecnología.

#### Ejemplos

Piensa en un sistema de e-commerce simple:
- **Vista Lógica:** catálogo de productos, carrito de compras, procesamiento de pagos, gestión de pedidos.
- **Vista de Desarrollo:** capa de presentación (controladores MVC), capa de negocio (servicios), capa de datos (repositorios) — exactamente los "estilos por capas" que veremos a continuación.
- **Vista Física:** un servidor web, una base de datos, un servicio de pagos externo, todos en máquinas/contenedores distintos.
- **Vista de Procesos:** cuando 1000 usuarios compran simultáneamente, ¿cómo se sincroniza el inventario sin vender el mismo producto dos veces?
- **Casos de Uso:** "Como cliente, quiero pagar con tarjeta y recibir confirmación por correo" — valida que las 4 vistas anteriores realmente soporten ese flujo.

#### Errores comunes
- Pensar que "arquitectura" es sinónimo de "patrones de diseño" (Strategy, Factory, etc. — Día 4) — los patrones de diseño operan a nivel de **clases**; la arquitectura opera a nivel de **componentes/sistemas completos**. Son escalas distintas del mismo problema (diseño de software), no lo mismo.
- Diseñar la arquitectura pensando solo en la Vista de Desarrollo (código) e ignorar la Vista Física (dónde se despliega) — es una causa común de proyectos que funcionan en el computador del desarrollador pero fallan en producción.
- Creer que la arquitectura se define una sola vez al principio y nunca cambia — en la práctica, la arquitectura **evoluciona** con el sistema (por eso "principios que guían su diseño y evolución" está en la propia definición formal).

#### Relaciones
La arquitectura es el "para qué" de todo lo aprendido hasta ahora: POO bien diseñado (Días 1-2) + SOLID (Día 4) son las herramientas para construir **componentes** correctos; la arquitectura decide **cómo se organizan y comunican** esos componentes a gran escala. Es el Nivel 8 del mapa de conocimiento.

#### Resumen
- Arquitectura = organización fundamental de componentes + relaciones + principios de evolución.
- Sin arquitectura: sistemas frágiles, difíciles de mantener y escalar.
- Modelo 4+1: Lógica, Procesos, Desarrollo, Física, + Casos de Uso (amarra a las 4).
- TOGAF: framework de arquitectura empresarial de referencia.

#### Ejercicios
1. **(Conceptual)** Explica con tus palabras la diferencia entre "patrón de diseño" y "arquitectura de software".
2. Para un sistema de biblioteca, describe brevemente qué contendría cada una de las 4+1 vistas.
3. **(V/F)** La arquitectura se define una única vez y no debe cambiar durante el proyecto. → Falso.

## Tema 3.10 — Estilos Arquitectónicos

#### Explicación

Un **estilo arquitectónico** es un patrón reutilizable de organización de alto nivel para sistemas de software, que define un vocabulario de componentes y conectores, y reglas sobre cómo se combinan. Tu material (slide "Temática") menciona explícitamente estos estilos como parte del programa completo del curso:

| Estilo | Idea central | Cuándo usarlo |
|---|---|---|
| **Por Capas (Layered)** | El sistema se divide en capas horizontales (presentación, negocio, datos), cada una solo habla con la capa inmediatamente inferior. | Sistemas empresariales tradicionales, cuando la separación de responsabilidades por "tipo técnico" es suficiente. |
| **Monolito** | Toda la aplicación se construye, despliega y ejecuta como una **única unidad**. | Proyectos pequeños/medianos, equipos pequeños, cuando la simplicidad operativa importa más que la escalabilidad independiente. |
| **Cliente-Servidor** | Un servidor centraliza recursos/datos; varios clientes los consumen mediante peticiones. | Casi cualquier aplicación web/móvil moderna en su forma más básica. |
| **MVC (Modelo-Vista-Controlador)** | Separa **Modelo** (datos/lógica de negocio), **Vista** (presentación) y **Controlador** (orquesta la interacción entre ambos). | Aplicaciones con interfaz de usuario donde se quiere desacoplar la lógica de la presentación (ver Tema 3.12, ASP.NET Core MVC). |
| **SOA (Service-Oriented Architecture)** | El sistema se compone de **servicios** independientes que se comunican por contratos (interfaces/mensajes), reutilizables por distintas aplicaciones. | Organizaciones grandes con múltiples sistemas que necesitan compartir funcionalidad de negocio. |
| **Microservicios** | Evolución de SOA: servicios **aún más pequeños**, cada uno con su propia base de datos, desplegable y escalable **independientemente**. | Sistemas grandes con equipos autónomos, necesidad de escalar partes específicas del sistema por separado. |
| **DDD (Domain-Driven Design)** | No es un estilo de despliegue, sino una forma de **modelar el software alrededor del dominio de negocio real** (entidades, agregados, value objects, lenguaje ubicuo) — a menudo combinado con microservicios (cada microservicio = un "bounded context"). | Dominios de negocio complejos donde el modelo del negocio debe reflejarse fielmente en el código. |
| **Clean Architecture** | Organiza el código en círculos concéntricos: el **dominio/negocio** en el centro (sin dependencias externas), rodeado de capas de aplicación, y en el borde exterior, los detalles técnicos (base de datos, UI, frameworks) — la regla de dependencia siempre apunta **hacia adentro**. | Sistemas donde se quiere que la lógica de negocio sea completamente independiente de frameworks/tecnología (fácil de probar, fácil de cambiar de BD/UI sin tocar el negocio). |
| **Hexagonal (Puertos y Adaptadores)** | Muy similar en espíritu a Clean Architecture: el núcleo de negocio define **puertos** (interfaces); los **adaptadores** (implementaciones concretas: BD, API REST, cola de mensajes) se "enchufan" a esos puertos desde afuera. | Cuando se necesita poder intercambiar tecnologías externas (cambiar de BD SQL a NoSQL, por ejemplo) sin tocar el núcleo. |
| **Serverless** | El código se ejecuta en funciones gestionadas por un proveedor cloud (ej. AWS Lambda, Azure Functions), sin que el equipo administre servidores directamente; se paga solo por ejecución. | Cargas de trabajo esporádicas/event-driven, minimizar costos de infraestructura ociosa. |
| **Event-Driven (Orientada a Eventos)** | Los componentes se comunican **publicando y reaccionando a eventos** (extensión, a nivel de sistema completo, de lo visto en el Tema 3.7), en vez de llamarse directamente unos a otros. | Sistemas que necesitan bajo acoplamiento entre módulos, procesamiento asíncrono, reacción en tiempo real a cambios. |
| **Arquitectura Agéntica / Multi-Agente** | Estilo emergente donde componentes autónomos ("agentes", frecuentemente basados en IA) colaboran, negocian y toman decisiones para lograr objetivos, en vez de seguir un flujo de control centralizado y predefinido. | Sistemas que requieren autonomía, adaptación y toma de decisiones distribuida (frontera actual de la arquitectura de software, mencionada en tu slide de temática como parte final del curso). |

> 📌 Nota importante: tu deck de "Temática" (slide 4) menciona estos estilos como **mapa completo del semestre**, pero solo la introducción conceptual (sin ejemplos de código) está en el material que me compartiste — es coherente con lo que detectamos en la Fase 1 como "vacío del material": profundizarán esto en semanas futuras del curso (probablemente después de tu examen de estas 4 semanas). Aun así, es información que puede aparecer conceptualmente en tu examen (definiciones, comparaciones), por eso la desarrollo aquí.

#### Ejemplos (esquema visual simplificado de tres estilos clave)

```
POR CAPAS                    MICROSERVICIOS                 EVENT-DRIVEN
┌────────────┐               ┌────────┐ ┌────────┐         ┌──────────┐
│Presentación│               │Servicio│ │Servicio│         │Publicador│──┐
├────────────┤               │Usuarios│ │Pagos   │         └──────────┘  │evento
│  Negocio   │               │ +BD_A  │ │ +BD_B  │              ┌───────▼──────┐
├────────────┤               └────────┘ └────────┘              │ Bus de Eventos│
│   Datos    │                    (cada uno independiente)       └───────┬──────┘
└────────────┘                                                    ┌──────▼──────┐
                                                                   │Suscriptor(es)│
                                                                   └─────────────┘
```

#### Errores comunes
- Pensar que "Monolito" siempre es malo y "Microservicios" siempre es mejor — en realidad, microservicios agrega complejidad operativa considerable (redes, consistencia de datos distribuida) que solo se justifica en ciertos contextos (equipos grandes, necesidad real de escalar partes independientemente).
- Confundir SOA con Microservicios: SOA típicamente comparte una infraestructura común (ESB — Enterprise Service Bus) y servicios más grandes/reutilizables a nivel empresa; Microservicios busca servicios más pequeños, autónomos, cada uno con su propia base de datos, sin depender de un bus centralizado pesado.
- Confundir Clean Architecture con Hexagonal — son casi lo mismo en espíritu (dependencias apuntan hacia el negocio central), con diferencias de terminología/organización de capas más que de principio.

#### Relaciones
Estos estilos son la aplicación, a escala de sistema completo, de todo lo que viste en el Día 1-2 (bajo acoplamiento, alta cohesión) y de SOLID (Día 4) — especialmente DIP, que es el principio que hace posible Clean Architecture y Hexagonal (el negocio no depende de detalles externos, sino de abstracciones).

#### Resumen
- Estilos vistos: Capas, Monolito, Cliente-Servidor, MVC, SOA, Microservicios, DDD, Clean Architecture, Hexagonal, Serverless, Event-Driven, Agéntica.
- Cada estilo resuelve un contexto distinto; no existe "el mejor estilo" universal.

#### Ejercicios
1. **(Conceptual)** ¿Por qué Microservicios no es automáticamente mejor que Monolito? Da un argumento de costos operativos.
2. Compara SOA vs. Microservicios en una tabla de 2 columnas.
3. Explica con tus palabras la "regla de dependencia" de Clean Architecture (todo apunta hacia el centro).

## Tema 3.11 — Programación Orientada a Servicios (SOA / Microservicios / Singleton)

#### Explicación

Este tema profundiza el estilo SOA/Microservicios ya introducido, con dos conceptos operativos adicionales de tu material:

**Servicio:** una unidad de funcionalidad de negocio, expuesta a través de un contrato bien definido (interfaz), que puede ser consumida por distintos clientes/aplicaciones sin que estos conozcan su implementación interna — es la aplicación, a nivel de sistema, de todo lo aprendido sobre Interfaces (Tema 2.4).

**Patrón Singleton** (mencionado en tu material dentro del contexto de servicios): garantiza que una clase tenga **una única instancia** en toda la aplicación, y provee un punto de acceso global a ella. Es muy usado para servicios que representan un recurso compartido único (ej. una conexión de configuración, un logger central).

```csharp
public class ConfiguracionServicio {
    private static ConfiguracionServicio _instancia;
    private static readonly object _lock = new object();

    private ConfiguracionServicio() { }   // constructor PRIVADO: nadie puede hacer "new" desde afuera

    public static ConfiguracionServicio Instancia {
        get {
            lock (_lock) {
                if (_instancia == null) _instancia = new ConfiguracionServicio();
                return _instancia;
            }
        }
    }
}
// Uso: ConfiguracionServicio.Instancia.AlgunMetodo();  -- SIEMPRE la misma instancia
```

**Java (equivalente exacto):**
```java
public class ConfiguracionServicio {
    private static ConfiguracionServicio instancia;
    private ConfiguracionServicio() { }
    public static synchronized ConfiguracionServicio getInstancia() {
        if (instancia == null) instancia = new ConfiguracionServicio();
        return instancia;
    }
}
```

#### Errores comunes
- Abusar de Singleton para "todo" — introduce estado global compartido en toda la aplicación, lo cual dificulta las pruebas unitarias (Tema relacionado con DIP, Día 4: es preferible inyectar dependencias explícitamente que depender de un Singleton global oculto).
- Confundir un "servicio" (concepto arquitectónico, contrato de negocio) con simplemente "una clase con métodos públicos" — un servicio en SOA/Microservicios normalmente implica también un límite de **despliegue independiente** (su propio proceso, posiblemente su propia base de datos), no solo una separación lógica dentro del mismo programa.

#### Relaciones
SOA/Microservicios son la materialización, a escala de sistema, de "programar contra interfaces, no contra implementaciones" (Interfaces, Tema 2.4, y DIP, Día 4). Singleton es uno de los primeros patrones de diseño "con nombre propio" que verás formalmente clasificado en el Día 4.

#### Resumen
- Un servicio expone funcionalidad de negocio vía contrato (interfaz), oculta implementación.
- Singleton garantiza una única instancia global — útil para servicios/recursos compartidos, pero se debe usar con moderación.

#### Ejercicios
1. Implementa en C# un Singleton `LoggerServicio` con un método `Log(string mensaje)`.
2. **(Conceptual)** ¿Por qué el abuso de Singleton puede dificultar las pruebas unitarias?

## Tema 3.12 — Introducción a ASP.NET Core MVC

#### Explicación

**Intuición.** Recuerda el estilo MVC del Tema 3.10. ASP.NET Core MVC es la implementación concreta de ese estilo, hecha por Microsoft para construir aplicaciones web en .NET.

**Los 3 componentes:**
- **Modelo (Model):** representa los datos y la lógica de negocio (tus clases POO de los Días 1-2, en esencia).
- **Vista (View):** la interfaz que ve el usuario (archivos `.cshtml`, con HTML + una sintaxis especial llamada Razor para insertar código C#).
- **Controlador (Controller):** recibe las peticiones HTTP del usuario, decide qué hacer (consultar/actualizar el Modelo) y elige qué Vista devolver como respuesta.

**Verbos HTTP relevantes** (según tu material): `GET` (consultar/mostrar datos, sin efectos secundarios), `POST` (enviar/crear datos nuevos).

**Mecanismos para pasar datos del Controlador a la Vista:**

| Mecanismo | Alcance | Tipado |
|---|---|---|
| **Model** | Una sola petición, hacia una vista fuertemente asociada a un tipo de dato | Fuertemente tipado (recomendado) |
| **ViewBag** | Una sola petición | Dinámico (sin tipo fijo, se accede como propiedad: `ViewBag.Titulo`) |
| **ViewData** | Una sola petición | Diccionario (`ViewData["Titulo"]`), sin tipo fijo |
| **TempData** | Sobrevive **una petición adicional** (útil para redirecciones, ej. mostrar un mensaje de éxito tras guardar y redirigir) | Diccionario, sin tipo fijo |

#### Ejemplos

**C# — Controlador básico:**
```csharp
public class ProductoController : Controller {
    private readonly IProductoServicio _servicio;   // <- Inyección de dependencias (DIP, Día 4)

    public ProductoController(IProductoServicio servicio) { _servicio = servicio; }

    [HttpGet]
    public IActionResult Listar() {
        var productos = _servicio.ObtenerTodos();
        ViewBag.Titulo = "Listado de Productos";
        return View(productos);          // envía "productos" como Modelo a la Vista
    }

    [HttpPost]
    public IActionResult Crear(Producto nuevo) {
        _servicio.Guardar(nuevo);
        TempData["Mensaje"] = "Producto creado con éxito";  // sobrevive a la redirección
        return RedirectToAction("Listar");
    }
}
```

**Vista (Razor, `.cshtml`):**
```html
<h1>@ViewBag.Titulo</h1>
@if (TempData["Mensaje"] != null) { <p>@TempData["Mensaje"]</p> }
@foreach (var p in Model) { <p>@p.Nombre - @p.Precio</p> }
```

> Java no tiene "ASP.NET Core MVC" (es un framework propietario de Microsoft), pero el equivalente conceptual más directo y usado en la industria es **Spring MVC** (parte de Spring Framework), que sigue exactamente el mismo patrón Modelo-Vista-Controlador, con `@Controller`, `@GetMapping`, `@PostMapping`, y plantillas Thymeleaf en vez de Razor. La lógica arquitectónica es idéntica; solo cambia la sintaxis específica del framework.

#### Errores comunes
- Poner lógica de negocio directamente en el Controlador ("Controlador gordo") en lugar de delegarla a un Servicio — rompe SRP (Día 4) y hace el controlador difícil de probar.
- Confundir `ViewBag`/`ViewData` (una sola petición) con `TempData` (sobrevive una petición extra, útil solo tras un `Redirect`).
- Usar `GET` para operaciones que modifican datos (debería ser `POST`) — rompe la semántica esperada de HTTP y puede causar efectos secundarios accidentales (ej. un buscador de internet que "pre-carga" un enlace GET podría disparar sin querer una acción destructiva).

#### Relaciones
ASP.NET Core MVC es la convergencia final de casi todo el curso: usa Clases/Objetos (Día 1) como Modelos, Interfaces + Inyección de Dependencias (Días 2 y 4/DIP) para los Servicios, el estilo arquitectónico MVC (Tema 3.10) como esqueleto, y potencialmente Eventos (Tema 3.7) para notificaciones internas.

#### Resumen
- MVC: Modelo (datos/negocio), Vista (presentación), Controlador (orquesta).
- `GET` consulta, `POST` envía/crea.
- Model (tipado) > ViewBag/ViewData (una petición, sin tipo) ; TempData (sobrevive una petición extra).

#### Ejercicios
1. **(V/F)** `ViewBag` sobrevive a una redirección (`RedirectToAction`). → Falso, solo `TempData` lo hace.
2. Diseña un controlador `FacturaController` con una acción `GET` para listar y una `POST` para crear, usando `TempData` para el mensaje de confirmación.
3. **(Conceptual)** ¿Por qué es mala práctica poner lógica de negocio directamente en el Controlador?

---

### 🧪 Mini examen — Día 3 completo

1. Diferencia entre Publisher y Subscriber en Eventos.
2. ¿Qué problema resuelve AOP que SRP por sí solo no resuelve?
3. Define Arquitectura de Software con tus palabras.
4. Nombra las 5 vistas del modelo 4+1 y qué pregunta responde cada una.
5. Compara Monolito vs. Microservicios: una ventaja y una desventaja de cada uno.
6. ¿Qué es un Singleton y cuándo NO deberías usarlo?
7. En ASP.NET Core MVC, ¿cuál es la diferencia entre `ViewBag` y `TempData`?
8. **(Trampa)** Un compañero dice "SOA y Microservicios son lo mismo". ¿Qué le responderías?
9. Explica la diferencia entre `Func` y `Action` (repaso).
10. ¿Qué es LINQ y con qué fuentes de datos puede trabajar?


---

# 📅 DÍA 4 — SOLID a Fondo, Patrones de Diseño e Integración

> SOLID fue formulado por **Robert C. Martin ("Uncle Bob")**. Su objetivo, textual de tu material: *"promover buenas prácticas de diseño de software, en particular en programación orientada a objetos, abordando problemas comunes que enfrentan los desarrolladores a medida que los sistemas de software crecen en tamaño y complejidad."* Nota clave: **SOLID no son reglas para seguir a ciegas**; son heurísticas para razonar sobre diseño. En tu propio material (caso Tránsito) verás que hasta el profesor reconoce trade-offs y principios "parcialmente cumplidos" — el examen premia tu **razonamiento**, no una respuesta binaria de "cumple/no cumple".

## SRP — Principio de Responsabilidad Única

### 1. Problema

Mira este código (mal diseñado, aunque compila perfectamente):

```csharp
public class Empleado {
    public string Nombre { get; set; }
    public double HorasTrabajadas { get; set; }

    public double CalcularSalario() => HorasTrabajadas * 25000;

    // 2ª preocupación, mezclada en la misma clase: formato de tiempo
    public string ObtenerHorasEnFormatoTexto() {
        int horas = (int)HorasTrabajadas;
        int minutos = (int)((HorasTrabajadas - horas) * 60);
        return $"{horas}h {minutos}m";
    }

    // 3ª preocupación, mezclada: envío de correo
    public void EnviarReciboPorCorreo(string emailDestino) {
        Console.WriteLine($"Enviando recibo a {emailDestino}...");
        // lógica de SMTP, credenciales, formato de correo, etc.
    }
}
```

¿Por qué es malo? Porque esta clase tiene **tres razones distintas para cambiar**: (1) si cambia la fórmula del salario, (2) si cambia el formato de presentación del tiempo, (3) si cambia el proveedor de correo (de SMTP a una API de terceros, por ejemplo). Un cambio en la lógica de envío de correo (una preocupación de infraestructura, no de negocio) obliga a tocar la misma clase que calcula salarios (una preocupación de negocio) — y cualquier error al tocar el envío de correo **arriesga romper** el cálculo de salario, aunque nada tenga que ver.

### 2. Intuición

Piensa en un **restaurante**. El **chef** cocina; el **mesero** atiende mesas; el **cajero** cobra. Si una sola persona tuviera que hacer las tres cosas a la vez, cualquier cambio en el menú, en el protocolo de atención, o en el sistema de pagos, afectaría a la MISMA persona y generaría caos. Separar responsabilidades permite que cada rol cambie **independientemente** sin afectar a los demás.

### 3. Definición formal

*"Una clase debe tener solo una razón (o preocupación) para cambiar. Debe tener solo una tarea simple y bien definida dentro del software."* Se logra dividiendo las clases en unidades más pequeñas y específicas. Beneficios (textuales de tu material): reduce la complejidad, mejora la legibilidad y comprensión, facilita mantener y ampliar el código.

### 4. Ejemplo incorrecto (Java, para comparar con el C# de arriba)
```java
public class Empleado {
    private String nombre;
    private double horasTrabajadas;

    public double calcularSalario() { return horasTrabajadas * 25000; }
    public String obtenerHorasEnFormatoTexto() { /* ... */ return ""; }
    public void enviarReciboPorCorreo(String email) { /* lógica SMTP aquí mismo */ }
}
```

### 5. Refactorización (paso a paso)

**Paso 1:** identifica las preocupaciones. Aquí hay tres: cálculo de negocio, formato/presentación, infraestructura de correo.

**Paso 2:** extrae cada preocupación a su propia clase, con una única responsabilidad:
```csharp
public class Empleado {
    public string Nombre { get; set; }
    public double HorasTrabajadas { get; set; }
    public double CalcularSalario() => HorasTrabajadas * 25000;   // única preocupación: datos + regla de negocio del empleado
}

public class FormateadorTiempo {
    public string AFormatoTexto(double horas) {
        int h = (int)horas;
        int m = (int)((horas - h) * 60);
        return $"{h}h {m}m";
    }
}

public class ServicioCorreo {
    public void EnviarRecibo(Empleado emp, string emailDestino) {
        Console.WriteLine($"Enviando recibo de {emp.Nombre} a {emailDestino}...");
    }
}
```

**Paso 3:** el código cliente ahora **coordina** estas 3 clases en vez de que una sola clase haga todo:
```csharp
Empleado emp = new Empleado { Nombre = "Ana", HorasTrabajadas = 160.5 };
double salario = emp.CalcularSalario();
string horasTxt = new FormateadorTiempo().AFormatoTexto(emp.HorasTrabajadas);
new ServicioCorreo().EnviarRecibo(emp, "ana@correo.com");
```

### 6. Resultado final

Ahora, si cambia el proveedor de correo, **solo tocas `ServicioCorreo`**; si cambia el formato de horas, **solo tocas `FormateadorTiempo`**; si cambia la fórmula de salario, **solo tocas `Empleado`**. Cada clase tiene una única razón para cambiar — cumple SRP.

### 7. UML — antes y después

```
ANTES (viola SRP):                    DESPUÉS (cumple SRP):
┌──────────────────┐                 ┌───────────┐   ┌──────────────────┐   ┌───────────────┐
│     Empleado      │                │ Empleado  │   │ FormateadorTiempo │   │ ServicioCorreo │
├──────────────────┤                 ├───────────┤   ├──────────────────┤   ├───────────────┤
│+CalcularSalario() │                │+CalcularSalario()│ │+AFormatoTexto() │   │+EnviarRecibo() │
│+ObtenerHorasTexto()│                └───────────┘   └──────────────────┘   └───────────────┘
│+EnviarReciboCorreo()│
└──────────────────┘
```

### 8. Casos reales
- **Tienda virtual:** una clase `Pedido` no debería, además de guardar sus datos, también generar el PDF de la factura y enviarlo por correo — eso son 3 responsabilidades (datos de negocio, generación de documentos, notificación).
- **Banco:** una clase `CuentaBancaria` no debería, además de manejar el saldo, también calcular reportes regulatorios para el banco central — son preocupaciones de negocio completamente distintas con "razones de cambio" independientes.
- **Hospital:** una clase `Paciente` no debería manejar tanto su historia clínica como la facturación de su seguro médico — dos equipos distintos (clínico y administrativo) cambiarían esa clase por razones no relacionadas.

### 9. Violaciones comunes (que parecen cumplir SRP pero no)
- Una clase con **un solo método público gigante** que internamente hace 5 cosas distintas — técnicamente "una sola responsabilidad pública", pero el método mismo viola SRP a nivel interno.
- Nombrar una clase de forma genérica ("Manager", "Helper", "Utility") — es una señal casi segura de que esa clase terminó absorbiendo múltiples responsabilidades sin que nadie se diera cuenta.

### 10. Cómo reconocerlo — preguntas guía
- ¿Esta clase tiene **más de una razón** para cambiar?
- Si describes lo que hace la clase con "y" (`hace X **y** hace Y **y** hace Z`), probablemente viola SRP.
- ¿Distintos "interesados" (stakeholders) del negocio pedirían cambios en esta misma clase por razones no relacionadas?

### 11. Ejercicios
1. Detecta el problema: una clase `Factura` que calcula el total, genera el PDF, y lo imprime en la impresora física. ¿Qué principio viola? Propón una solución.
2. Refactoriza: una clase `Usuario` que valida su propia contraseña, hashea contraseñas, Y envía correos de bienvenida.
3. **(V/F)** Una clase con 20 métodos siempre viola SRP. → Falso — el número de métodos no es el criterio; el criterio es cuántas **razones distintas de cambio** existen.

### 12. Corrección (razonada)
1. Viola **SRP**: calcular, generar documento e imprimir son 3 responsabilidades. Solución: `Factura` (datos+cálculo), `GeneradorPDF` (documento), `ServicioImpresion` (impresión física).
2. Separar en: `Usuario` (datos), `ValidadorContraseña`/`HasherContraseña` (seguridad), `ServicioCorreoBienvenida` (notificación).

### 13. Comparaciones
**SRP vs. OCP** (verás OCP a continuación): SRP se enfoca en **cuántas responsabilidades** tiene una clase (cohesión interna); OCP se enfoca en si puedes **extender** su comportamiento sin **modificarla**. Son complementarios: una clase con SRP bien aplicado es mucho más fácil de que además cumpla OCP, porque tiene menos razones para necesitar ser modificada en primer lugar.

### 14. Relación con Patrones de Diseño
- **Facade:** agrupa varias subclases de responsabilidad única bajo una interfaz simple, sin que el cliente necesite conocer la separación interna.
- **Decorator:** permite añadir responsabilidades a un objeto dinámicamente, en capas separadas, en lugar de meterlas todas en una sola clase.

### 15. Checklist SRP
> - [ ] ¿Puedo describir lo que hace esta clase en una sola frase, sin usar "y"?
> - [ ] ¿Un cambio en un aspecto técnico (formato, envío, persistencia) obliga a tocar la misma clase que la lógica de negocio?
> - [ ] ¿El nombre de la clase es específico (no "Manager"/"Helper"/"Util")?

---

## OCP — Principio Abierto/Cerrado

### 1. Problema

```csharp
public class CalculadoraDescuento {
    public double Calcular(string tipoCliente, double monto) {
        if (tipoCliente == "Regular") return monto * 0.95;
        else if (tipoCliente == "VIP") return monto * 0.85;
        else if (tipoCliente == "Empleado") return monto * 0.70;
        // Si mañana llega un nuevo tipo de cliente, hay que MODIFICAR este método,
        // arriesgando romper la lógica de los tipos que ya funcionaban.
        return monto;
    }
}
```

### 2. Intuición

Piensa en un **enchufe eléctrico de pared**. Puedes conectar una lámpara, un cargador de celular, una licuadora — **sin modificar el enchufe ni la instalación eléctrica de la casa**. El enchufe está "cerrado" (no lo tocas, ya funciona) pero "abierto" a que conectes cosas nuevas.

### 3. Definición formal

*"Las clases deben estar abiertas para extensión pero cerradas para modificación."* Una clase está **cerrada** si su interfaz está claramente definida y no cambiará; está **abierta** si se puede extender (agregar métodos/campos, anular comportamiento) sin tocar su código fuente ya probado. Se logra fomentando **abstracción y polimorfismo** (herencia o composición). Si la clase principal tiene un error, **ese** error se corrige en ella — **no** se soluciona creando una subclase para "parchar" el problema.

### 4. Ejemplo incorrecto
Ya está arriba (Paso 1): el `if/else if` que crece cada vez que aparece un tipo de cliente nuevo, obligando a **modificar** una clase ya probada y en producción.

### 5. Refactorización

**Paso 1:** identifica la variación (el "eje de cambio"): el tipo de cliente determina la fórmula de descuento.

**Paso 2:** extrae una abstracción común (interfaz) que represente "una forma de calcular descuento":
```csharp
public interface IEstrategiaDescuento {
    double Calcular(double monto);
}
```

**Paso 3:** cada tipo de cliente se convierte en una implementación separada, **sin tocar las demás**:
```csharp
public class DescuentoRegular : IEstrategiaDescuento {
    public double Calcular(double monto) => monto * 0.95;
}
public class DescuentoVIP : IEstrategiaDescuento {
    public double Calcular(double monto) => monto * 0.85;
}
public class DescuentoEmpleado : IEstrategiaDescuento {
    public double Calcular(double monto) => monto * 0.70;
}
```

**Paso 4:** la clase que ORQUESTA el cálculo ya no cambia nunca más, sin importar cuántos tipos nuevos aparezcan:
```csharp
public class CalculadoraDescuento {
    public double Calcular(IEstrategiaDescuento estrategia, double monto) => estrategia.Calcular(monto);
}
```

### 6. Resultado final

Para agregar un nuevo tipo de cliente (ej. "Estudiante"), **solo agregas una clase nueva** (`DescuentoEstudiante : IEstrategiaDescuento`) — **cero líneas modificadas** en el código existente y ya probado. Esto es exactamente el **patrón Strategy** (Tema 4.6, más abajo).

### 7. UML — antes y después
```
ANTES:                                    DESPUÉS:
┌─────────────────────┐                   «interface» IEstrategiaDescuento
│ CalculadoraDescuento │                              △
├─────────────────────┤                     ┌─────────┼─────────┐
│+Calcular(tipo,monto)│                DescuentoRegular DescuentoVIP DescuentoEmpleado
│  (if/else if...)     │
└─────────────────────┘                CalculadoraDescuento ──uso──> IEstrategiaDescuento
```

### 8. Casos reales
- **Banco:** cálculo de tasas de interés según tipo de producto — nuevos productos financieros se agregan como nuevas estrategias, sin tocar el motor de cálculo.
- **Videojuego:** distintos tipos de enemigos con distinto comportamiento de ataque — se agregan como nuevas clases de estrategia de ataque.
- **Biblioteca:** distintos tipos de material (Libro, Revista, DVD) con distinta política de días de préstamo — cada política es una implementación separada.

### 9. Violaciones comunes
- Crear una **subclase que sobrescribe un método para "arreglar" un bug** de la clase base, en lugar de corregir el bug en la clase base directamente (tu material lo señala explícitamente: *"si la clase principal tiene un error, esta es la que se debe corregir. NO aplica la creación de una subclase para resolver el error"*).
- Grandes cadenas `switch`/`if-else` sobre un "tipo" (`enum` o `string`) que crecen indefinidamente con cada nueva variante del negocio — señal clásica de violación de OCP.

### 10. Cómo reconocerlo
- ¿Necesito **modificar** código existente y ya probado para agregar una funcionalidad nueva? → Viola OCP.
- ¿Hay una cadena larga de `if/else if` o `switch` sobre un "tipo" que crece con el tiempo? → Candidato fuerte a violación de OCP.

### 11. Ejercicios
1. Detecta la violación: un método `CalcularEnvio(string metodoEnvio, double peso)` con `if (metodoEnvio == "Terrestre") ... else if (metodoEnvio == "Aereo") ...`. Refactorízalo con Strategy.
2. **(V/F)** OCP se logra únicamente con herencia, nunca con composición. → Falso, tu material menciona explícitamente "herencia o composición".

### 12. Corrección
1. Crear `IEstrategiaEnvio` con `MetodoTerrestre`, `MetodoAereo`, cada uno con su propio `CalcularCosto(peso)`.

### 13. Comparaciones
**OCP vs. YAGNI** ("You Aren't Gonna Need It" — no implementes lo que no necesitas todavía): pueden entrar en tensión. OCP te dice "diseña para que sea fácil extender"; YAGNI te dice "no sobre-diseñes abstracciones para variaciones que quizás nunca lleguen". **Cómo decidir:** aplica OCP con fuerza donde el negocio **ya te ha mostrado** que ese eje cambia con frecuencia (ej. tipos de descuento, tipos de vehículo); no crees una interfaz de estrategia "por si acaso" para algo que nunca ha cambiado ni hay indicio de que cambiará — eso sería sobre-ingeniería.

### 14. Relación con Patrones de Diseño
- **Strategy:** la aplicación más directa de OCP — encapsula algoritmos intercambiables detrás de una interfaz común (justo lo que hicimos arriba).
- **Factory:** permite crear objetos de distintos tipos sin que el código cliente conozca la clase concreta, facilitando agregar tipos nuevos sin tocar el código que los consume.
- **Template Method:** define el esqueleto de un algoritmo en una clase base, dejando que las subclases "extiendan" pasos específicos sin modificar el flujo general.

### 15. Checklist OCP
> - [ ] ¿Para agregar una variante nueva del negocio, solo necesito **agregar una clase**, sin tocar el código existente?
> - [ ] ¿Existe una cadena `if/else`/`switch` sobre un "tipo" que crece con cada nuevo caso de negocio?
> - [ ] ¿Los errores se corrigen en la clase original, no "parchando" con una subclase?


---

## LSP — Principio de Sustitución de Liskov

### 1. Problema (ejemplo REAL y textual de tu deck)

```csharp
abstract class Automovil {
    public abstract void Repostar();  // Método genérico para cargar energía o combustible
}

class Electrico : Automovil {
    public override void Repostar() {
        throw new NotImplementedException("Los autos eléctricos no pueden repostar gasolina.");
    }
}

class Gasolina : Automovil {
    public override void Repostar() => Console.WriteLine("Cargando gasolina...");
}

class Program {
    static void Main() {
        List<Automovil> autos = new List<Automovil> { new Gasolina(), new Electrico() };
        foreach (var auto in autos) {
            auto.Repostar(); // 🚨 Este fallará (excepción) cuando el objeto sea Electrico
        }
    }
}
```

¿Por qué es grave? Porque el código cliente (`Main`) trata a `Electrico` como si fuera "sustituible" por `Automovil` (así lo promete la herencia), pero al invocar `Repostar()` **explota en tiempo de ejecución**. Esto es exactamente lo que LSP prohíbe.

### 2. Intuición

Piensa en un contrato de trabajo: si contratas a alguien para el puesto de "Cajero" (la superclase, el puesto general), esperas que **cualquier** persona que ocupe ese puesto pueda cobrar, dar cambio, abrir la caja — sin sorpresas. Si contratas a un "Cajero Junior" (subclase) que **no puede** abrir la caja y **lanza una alarma** cuando se lo pides, rompiste la promesa implícita del puesto: el cliente (quien usa al Cajero) no debería tener que saber "ah, pero si es Junior, no le pidas esto".

### 3. Definición formal

*"Los objetos de una clase derivada deben poder sustituir a los objetos de su clase base sin alterar el comportamiento esperado del programa."* Al extender una clase, se debe poder pasar objetos de la subclase en lugar de objetos de la superclase **sin romper el código del cliente**. La subclase debe seguir siendo compatible con el comportamiento (no solo la firma) de la superclase.

### 4. Ejemplo incorrecto
Ya visto arriba: `Electrico.Repostar()` lanza una excepción inesperada — viola directamente el requisito "un método en una subclase no debe generar tipos de excepciones que no se espera que genere el método base".

### 5. Refactorización (paso a paso, exactamente como en tu material)

**Paso 1:** identifica qué comportamiento **sí** comparten todas las subclases de forma segura: **ambos pueden `Conducir()`**. Eso sí pertenece a la superclase abstracta.

**Paso 2:** identifica qué comportamiento **NO** es universal (repostar gasolina vs. cargar batería) y **sepáralo en interfaces específicas** en vez de forzarlo en la superclase:

```csharp
abstract class Automovil {
    public abstract void Conducir();
}

interface ICombustible { void Repostar(); }
interface IElectrico { void CargarBateria(); }

class Gasolina : Automovil, ICombustible {
    public override void Conducir() => Console.WriteLine("Conduciendo un auto de gasolina...");
    public void Repostar() => Console.WriteLine("Cargando gasolina...");
}

class Electrico : Automovil, IElectrico {
    public override void Conducir() => Console.WriteLine("Conduciendo un auto eléctrico...");
    public void CargarBateria() => Console.WriteLine("Cargando batería...");
}
```

### 6. Resultado final

```csharp
List<Automovil> autos = new List<Automovil> { new Gasolina(), new Electrico() };
foreach (var auto in autos) { auto.Conducir(); }              // ✅ Ambos pueden conducir sin problemas

List<ICombustible> autosGasolina = new List<ICombustible> { new Gasolina() };
foreach (var auto in autosGasolina) { auto.Repostar(); }       // ✅ Solo los de gasolina, sin riesgo

List<IElectrico> autosElectricos = new List<IElectrico> { new Electrico() };
foreach (var auto in autosElectricos) { auto.CargarBateria(); } // ✅ Solo los eléctricos, sin riesgo
```
Ya nadie puede llamar `Repostar()` sobre un `Electrico` **porque el compilador ni siquiera lo permite** — el error se detecta en tiempo de compilación, no explota en producción.

### 7. UML — antes y después (caso Document/WritableDocument de tu deck)

Tu material incluye un segundo ejemplo de LSP: *"La clase `Document` es un documento readonly y la clase `WritableDocument` extiende `Document` e implementa el comportamiento `save`."*

```
ANTES (problemático si Document tuviera save() que lanza NotSupportedException):
        Document
    (con save() que a veces falla)
            △
      ┌─────┴─────┐
  PDFDocument   ReadOnlyDocument (rompe LSP al heredar save() inválido)

DESPUÉS (correcto, tal como en tu material):
        Document                    (readonly puro, sin save())
            △
     WritableDocument               (extiende Document, AGREGA save() — no lo hereda roto)
```
La solución de tu propio profesor: en vez de que `Document` tenga un método `save()` que unas subclases pueden usar y otras no, `Document` se queda **sin** `save()` (solo lectura), y `WritableDocument` es quien **agrega** ese comportamiento — nadie hereda una promesa que no puede cumplir.

### 8. Requisitos formales completos para que una subclase cumpla LSP (los 7 de tu material — MUY probable pregunta de examen textual)

| # | Regla |
|---|---|
| 1 | Los tipos de parámetros en un método de la subclase deben **coincidir o ser más abstractos** que en la superclase (no más restrictivos). |
| 2 | El tipo de retorno en un método de la subclase debe **coincidir o ser un subtipo** del de la superclase (puede ser más específico). |
| 3 | Un método de la subclase **no debe generar excepciones inesperadas** que el método base no generaría. |
| 4 | Una subclase **no debe reforzar (endurecer) las condiciones previas** (precondiciones) — deben ser iguales o más débiles. |
| 5 | Una subclase **no debe debilitar las condiciones posteriores** (postcondiciones) — la garantía de la superclase debe mantenerse. |
| 6 | Las **invariantes** de la superclase deben conservarse en la subclase. |
| 7 | Una subclase **no debe cambiar los valores de los campos privados** de la superclase. |

### 9. Casos reales
- **Tienda virtual:** un `MetodoPagoBase` con `Procesar()`; si `MetodoPagoRegalo` lanza excepción cuando el monto excede el saldo de la tarjeta de regalo mientras que la superclase nunca lanzaba excepciones, viola la regla 3.
- **Banco:** una subclase `CuentaAhorroJunior` que "refuerza" la precondición del método `Retirar(monto)` exigiendo siempre `monto < 50000` cuando la superclase `Cuenta` permitía cualquier monto positivo — viola la regla 4 (no se puede reforzar precondiciones).

### 10. Cómo reconocerlo — preguntas guía
- ¿Puedo reemplazar cualquier uso de la superclase por la subclase, **en cualquier parte del código**, sin que nada se rompa o lance una excepción inesperada?
- ¿La subclase "hereda" un método que en realidad **no puede cumplir** de forma consistente con el resto? → señal de alarma inmediata (como `Electrico.Repostar()`).

### 11. Ejercicios
1. Detecta la violación: una clase `Ave` con método `Volar()`, y una subclase `Pinguino` que hace `throw new NotSupportedException()` en `Volar()`. Propón la solución (pista: interfaces).
2. **(V/F)** Una subclase puede debilitar las precondiciones de un método heredado. → Verdadero (puede debilitarlas, no reforzarlas).
3. Identifica cuál de las 7 reglas viola este código: `public override void Retirar(double monto) { if (monto > 1000) throw new Exception(); base.Retirar(monto); }` cuando la superclase no tenía ese límite. *(Regla 4: refuerza la precondición.)*

### 12. Corrección
1. `Ave` se queda solo con comportamiento común real (`Comer()`, `Dormir()`); se crea `IVolador` con `Volar()`, implementada solo por las aves que realmente vuelan (`Aguila : Ave, IVolador`), y `Pinguino : Ave` simplemente no implementa `IVolador`.

### 13. Comparaciones
**Herencia vs. Composición** (retomado aquí formalmente): tu propio material lo dice explícitamente como recomendación de diseño para LSP: *"Usar composición en lugar de herencia: si una relación 'ES UN' no es clara, quizás sea mejor que un objeto 'TENGA UN' otro objeto."* Es la aplicación práctica, en el contexto de LSP, de la regla general "prefiere composición sobre herencia" que vimos en el Tema 2.2.

### 14. Relación con Patrones de Diseño
- **Template Method:** ayuda a cumplir LSP definiendo un esqueleto de algoritmo donde las subclases solo personalizan pasos específicos bien delimitados, reduciendo el riesgo de romper el comportamiento esperado del conjunto.
- **Strategy:** al preferir composición (inyectar un comportamiento) sobre herencia, se evita el riesgo de violaciones de LSP por completo en muchos casos.

### 15. Checklist LSP
> - [ ] ¿Toda subclase puede sustituir a la superclase en cualquier contexto sin lanzar excepciones inesperadas?
> - [ ] ¿Ninguna subclase refuerza precondiciones ni debilita postcondiciones?
> - [ ] ¿Se evitó forzar una relación "ES-UN" dudosa, usando interfaces/composición donde correspondía?

---

## ISP — Principio de Segregación de Interfaces

### 1. Problema

```csharp
interface ITrabajador {
    void Trabajar();
    void Comer();
    void Dormir();
}

class Robot : ITrabajador {
    public void Trabajar() => Console.WriteLine("Robot trabajando");
    public void Comer() => throw new NotImplementedException();   // 🚨 un robot no come
    public void Dormir() => throw new NotImplementedException();  // 🚨 un robot no duerme
}
```

### 2. Intuición

Piensa en un **control remoto universal con 40 botones** para un televisor que solo necesita 5. La mayoría de esos botones no aplican a tu aparato, generan confusión, y si el fabricante cambia el botón #37 (que ni usas), corres el riesgo de que se rompa algo en tu control sin que a ti te importara ese botón. Es mejor tener **varios controles pequeños y específicos**.

### 3. Definición formal

*"Una interfaz no debe obligar a una clase a implementar métodos que no usa. Es mejor tener varias interfaces pequeñas y específicas en lugar de una única interfaz grande y genérica que fuerce a las clases a implementar métodos innecesarios."*

### 4. Ejemplo incorrecto
Ya visto arriba: `ITrabajador` obliga a `Robot` a implementar `Comer()`/`Dormir()`, que no tienen sentido para él.

### 5. Refactorización

**Paso 1:** identifica los "grupos naturales" de comportamiento que no siempre van juntos.

**Paso 2:** segrega la interfaz grande en varias pequeñas:
```csharp
interface ICapazDeTrabajar { void Trabajar(); }
interface ICapazDeComer { void Comer(); }
interface ICapazDeDormir { void Dormir(); }
```

**Paso 3:** cada clase implementa solo lo que necesita:
```csharp
class Robot : ICapazDeTrabajar {
    public void Trabajar() => Console.WriteLine("Robot trabajando");
}
class Humano : ICapazDeTrabajar, ICapazDeComer, ICapazDeDormir {
    public void Trabajar() => Console.WriteLine("Humano trabajando");
    public void Comer() => Console.WriteLine("Humano comiendo");
    public void Dormir() => Console.WriteLine("Humano durmiendo");
}
```

### 6. Resultado final
`Robot` ya no tiene métodos "fantasma" que lanzan excepciones — implementa exactamente lo que necesita, ni más ni menos.

### 7. UML — antes y después
```
ANTES:                          DESPUÉS:
«interface» ITrabajador         «interface» ICapazDeTrabajar   «interface» ICapazDeComer   «interface» ICapazDeDormir
  +Trabajar()                          △                              △                            △
  +Comer()                             │                              │                            │
  +Dormir()                          Robot                         Humano ─────────────────────────┘
      △                                                              │
   ┌──┴──┐                                                    (Humano implementa las 3)
 Robot  Humano
```

### 8. Casos reales
- **Tienda virtual:** una interfaz `IProducto` que obliga a todo producto a implementar `RequiereEnvioRefrigerado()` — un libro digital no necesita eso; mejor segregar `IProductoFisico` (con esa capacidad) de `IProductoDigital`.
- **Banco:** una interfaz `ICuenta` con `AplicarInteres()` que no aplica a una cuenta corriente sin intereses — segregar `ICuentaConInteres` de `ICuenta` base.
- **Hospital:** una interfaz `IPersonalMedico` con `RecetarMedicamentos()` que no debería obligar a implementarlo a personal de enfermería sin esa facultad legal — segregar por rol real.

### 9. Violaciones comunes
- Crear una interfaz "todo en uno" ("God Interface") que agrupa TODO lo que cualquier clase del dominio podría necesitar, "para no tener que crear varias interfaces" — es exactamente lo que ISP prohíbe.
- El caso real de tu propio material (caso Multa del Tránsito): *"ISP: Implementa `ISancionEconomica` directamente, pero no todas las multas podrían necesitar esta interfaz en el futuro"* — muestra que incluso una interfaz que hoy parece razonable puede volverse una violación de ISP si el dominio crece y aparecen tipos de multa sin componente económico.

### 10. Cómo reconocerlo
- ¿Esta interfaz obliga a implementar métodos innecesarios para alguna de sus clases implementadoras?
- ¿Alguna implementación de la interfaz tiene métodos que lanzan `NotImplementedException` o quedan vacíos "porque no aplican"? → Señal segura de violación de ISP.

### 11. Ejercicios
1. Detecta la violación: interfaz `IEmpleado` con `Trabajar()`, `CobrarComision()`, `SupervisarEquipo()` — implementada por `EmpleadoOperativo` (que no cobra comisión ni supervisa a nadie). Refactoriza.
2. **(V/F)** ISP recomienda una única interfaz grande para simplificar el diseño. → Falso, exactamente lo contrario.

### 12. Corrección
1. Segregar en `ITrabajador` (`Trabajar()`), `IConComision` (`CobrarComision()`), `ISupervisor` (`SupervisarEquipo()`); `EmpleadoOperativo` implementa solo `ITrabajador`.

### 13. Comparaciones
**ISP vs. SRP:** SRP habla de responsabilidades de una **clase** (cuántas razones tiene para cambiar); ISP habla del tamaño de los **contratos** (interfaces) que expones. Están relacionados: una interfaz "gorda" a menudo empuja a las clases implementadoras a violar SRP también, porque terminan absorbiendo responsabilidades que no les correspondían solo para "cumplir" con la interfaz.

### 14. Relación con Patrones de Diseño
- **Adapter:** cuando una clase existente no cumple con una interfaz pequeña y específica que necesitas, un Adapter "traduce" su interfaz sin modificarla, permitiendo cumplir ISP sin tocar código de terceros.
- **Facade:** en la dirección opuesta a ISP (simplifica un subsistema complejo tras una interfaz única), pero para el **consumidor externo** — no contradice ISP porque el Facade normalmente delega a componentes internos que sí mantienen interfaces segregadas entre sí.

### 15. Checklist ISP
> - [ ] ¿Alguna clase implementa métodos de una interfaz que no usa realmente (o que lanza excepción/vacío)?
> - [ ] ¿Las interfaces agrupan métodos realmente afines entre sí (cohesión de interfaz)?
> - [ ] ¿Cada clase implementa solo las interfaces que necesita?

---

## DIP — Principio de Inversión de Dependencias

### 1. Problema (ejemplo real y textual de tu deck)

```csharp
public class Motor {
    public void Encender() => Console.WriteLine("Motor de combustión encendido");
}

public class Automovil {
    private Motor motor = new Motor();   // 🚨 Automovil CREA directamente una implementación concreta
    public void Arrancar() => motor.Encender();
}
```

¿Por qué es grave? *"La clase `Automovil` está directamente acoplada a la implementación concreta de `Motor`. Si se quiere cambiar `Motor` por `MotorElectrico`, se tendría que MODIFICAR `Automovil`, lo que rompe OCP. No se pueden hacer pruebas unitarias de `Automovil` fácilmente porque depende de una implementación específica de `Motor`."* (textual de tu material).

### 2. Intuición

Piensa en un **enchufe de pared estándar (110V/220V)**. Tu lámpara no dice "yo solo funciono con ESTE cable específico de ESTA fábrica" — dice "yo funciono con **cualquier cosa** que respete el estándar del enchufe". El enchufe es la **abstracción**; la empresa eléctrica específica detrás del cable es un **detalle** que a la lámpara no le importa. Eso es Inversión de Dependencias: en vez de depender de un detalle concreto, dependes de un contrato/estándar.

### 3. Definición formal

*"Las clases de alto nivel (tienen implementada la lógica del negocio) no deberían depender de las clases de bajo nivel (tienen implementaciones específicas). Ambas deberían depender de abstracciones. Las abstracciones no deberían depender de los detalles. Los detalles deberían depender de abstracciones (interfaces o clases abstractas)."* Cuando se cumple esto, se dice que hay una **inversión** de dependencia: normalmente uno esperaría que "alto nivel depende de bajo nivel" (así se ve el código a simple vista), pero DIP invierte esa flecha para que **ambos dependan de una interfaz intermedia**.

### 4. Ejemplo incorrecto
Ya visto arriba (`Automovil` crea `new Motor()` directamente).

### 5. Refactorización (paso a paso, exactamente el ejemplo de tu material)

**Paso 1:** define una interfaz de "alto nivel" que describa la operación en términos de negocio, sin importar la implementación:
```csharp
public interface IMotor {
    void Encender();
}
```

**Paso 2:** las implementaciones concretas (bajo nivel) implementan esa interfaz:
```csharp
public class MotorCombustion : IMotor {
    public void Encender() => Console.WriteLine("Motor de combustión encendido");
}
public class MotorElectrico : IMotor {
    public void Encender() => Console.WriteLine("Motor eléctrico encendido silenciosamente");
}
```

**Paso 3:** `Automovil` (alto nivel) depende de la **interfaz**, no de una clase concreta, y la recibe por **Inyección de Dependencias en el Constructor** (*"la forma más común de Inversión de Dependencia"*, textual de tu material — *"patrón de diseño en el que las dependencias de una clase se proporcionan desde el exterior en lugar de ser creadas dentro de la clase"*):
```csharp
public class Automovil {
    private readonly IMotor motor;
    public Automovil(IMotor motor) { this.motor = motor; }   // <- inyección por constructor
    public void Arrancar() => motor.Encender();
}
```

### 6. Resultado final
```csharp
Automovil autoGasolina = new Automovil(new MotorCombustion());
Automovil autoElectrico = new Automovil(new MotorElectrico());
autoGasolina.Arrancar();   // "Motor de combustión encendido"
autoElectrico.Arrancar();  // "Motor eléctrico encendido silenciosamente"
```
Beneficios (textuales de tu material): **Desacoplamiento** — `Automovil` no depende de ninguna implementación concreta de `IMotor`; **Facilidad de cambio** — se puede cambiar `MotorCombustion` por `MotorElectrico` **sin modificar `Automovil`** (¡y esto automáticamente también arregla OCP, por eso tu material dice **"DIP va de la mano con OCP"**!).

### 7. UML — antes y después
```
ANTES:                                DESPUÉS:
Automovil ──crea──> Motor            Automovil ──depende de──> «interface» IMotor
(acoplamiento directo,                                                 △
 clase de alto nivel                                       ┌───────────┴───────────┐
 atada a un detalle)                                MotorCombustion          MotorElectrico
                                       (Automovil recibe un IMotor por el CONSTRUCTOR)
```

### 8. Casos reales — el ejemplo `BudgetReport`/`MySQLDatabase` de tu material
Textual: *"La clase `BudgetReport` utiliza la clase `MySQLDatabase` de bajo nivel para leer y conservar sus datos. Un cambio en `MySQLDatabase` podría alterar el funcionamiento de `BudgetReport`."* Solución: *"Se crea una interfaz de alto nivel que describa las operaciones de lectura/escritura. La clase `BudgetReport` utiliza la interfaz, en lugar de conectarse con las clases de bajo nivel. La clase de bajo nivel original se puede cambiar o ampliar implementando la nueva interfaz de lectura/escritura declarada por la lógica de negocio."* Esto es exactamente la base de por qué en ASP.NET Core MVC (Tema 3.12) los controladores reciben **interfaces** de servicio por el constructor, no clases concretas.

### 9. Violaciones comunes (los 3 "Errores Comunes" textuales de tu material)
1. **Acoplamiento fuerte entre clases:** cuando una clase de alto nivel crea instancias de clases de bajo nivel (`new` directamente dentro de la clase).
2. **Uso de implementaciones en lugar de abstracciones:** si un método espera un `MotorCombustion` concreto en lugar de un `IMotor`, el código no es extensible.
3. **Abuso de contenedores de inyección de dependencias:** si se abusa creando demasiadas interfaces innecesarias, el código se vuelve complejo sin beneficio real (relacionado con la tensión OCP vs. YAGNI del Tema anterior).

### 10. Cómo reconocerlo
- ¿Esta clase depende directamente de implementaciones concretas (`new ClaseConcreta()` dentro de ella)?
- ¿Puedo probar esta clase con un "doble de prueba" (mock) sin necesitar una base de datos/servicio real? Si no puedo, probablemente viola DIP.

### 11. Ejercicios
1. Detecta la violación: `class ServicioNotificacion { private EmailSender sender = new EmailSender(); }`. Refactoriza con DIP.
2. **(V/F)** DIP significa que las clases de bajo nivel nunca deben implementar interfaces. → Falso, exactamente lo contrario: deben implementarlas.
3. Explica con tus palabras qué significa "invertir" en Inversión de Dependencias (qué se invierte respecto a qué).

### 12. Corrección
1. Crear `IServicioNotificacion` (o `ICanalEnvio`), `EmailSender : ICanalEnvio`, e inyectar `ICanalEnvio` por constructor en `ServicioNotificacion` (o renombrar el orquestador).

### 13. Comparaciones
**DIP vs. KISS** ("Keep It Simple, Stupid"): igual que con OCP/YAGNI, hay tensión: DIP puede generar muchas interfaces pequeñas que, si se abusa, aumentan la complejidad accidental del proyecto ("Abuso de contenedores de DI" de la lista de errores comunes). **Cómo decidir:** aplica DIP donde real y probablemente haya necesidad de intercambiar implementaciones (bases de datos, servicios externos, motores de cálculo que cambian) — no crees una interfaz para una clase que jamás tendrá una segunda implementación ni se necesita mockear en pruebas.

### 14. Relación con Patrones de Diseño
- **Dependency Injection (DI):** es, literalmente, el patrón que implementa DIP en la práctica (inyección por constructor, como se mostró arriba).
- **Factory:** a menudo se usa junto con DIP para decidir, en un solo lugar centralizado, **qué implementación concreta** inyectar según el contexto (configuración, ambiente).
- **Adapter:** cuando la clase de bajo nivel no puede cambiar su interfaz original, un Adapter permite que igual cumpla con la abstracción de alto nivel esperada.

### 15. Checklist DIP
> - [ ] ¿Las clases de alto nivel dependen de interfaces, no de clases concretas?
> - [ ] ¿Las dependencias se reciben por constructor (inyección), en vez de crearse con `new` internamente?
> - [ ] ¿Puedo sustituir una implementación por otra (o por un mock de prueba) sin tocar la clase de alto nivel?


---

## Tema 4.6 — Patrones de Diseño Asociados a SOLID (desarrollo completo)

> Estos patrones se mencionan en tu material de forma tangencial; los desarrollo a fondo porque las instrucciones de este curso lo piden explícitamente y tu examen probablemente los dará por conocidos.

### Strategy (ya codificado arriba, en OCP)
**Problema que resuelve:** algoritmos intercambiables (distintas formas de hacer "lo mismo" conceptualmente). **Principio que ayuda a cumplir:** OCP (agregar estrategias nuevas sin modificar código existente) y DIP (el contexto depende de la interfaz `IEstrategia`, no de una implementación concreta).

### Factory (Method)
**Problema que resuelve:** el código cliente necesita crear objetos, pero no debería saber la clase concreta exacta a instanciar (eso rompería DIP y OCP: si mañana agregas un tipo nuevo, tendrías que tocar cada lugar donde se hace `new`).

```csharp
public interface IVehiculo { void Conducir(); }
public class Moto : IVehiculo { public void Conducir() => Console.WriteLine("Conduciendo moto"); }
public class Carro : IVehiculo { public void Conducir() => Console.WriteLine("Conduciendo carro"); }

public static class VehiculoFactory {
    public static IVehiculo Crear(string tipo) {
        return tipo switch {
            "Moto" => new Moto(),
            "Carro" => new Carro(),
            _ => throw new ArgumentException("Tipo no soportado")
        };
    }
}
// Uso: IVehiculo v = VehiculoFactory.Crear("Moto");  -- el cliente NO conoce las clases concretas
```
**Principio que ayuda a cumplir:** DIP (el cliente depende de `IVehiculo`, no de `Moto`/`Carro`) y OCP (agregar un tipo nuevo solo toca la Factory, no el código cliente disperso).

### Observer
**Problema que resuelve:** notificar automáticamente a varios objetos interesados cuando el estado de otro objeto cambia, sin acoplarlos directamente. **Es, formalmente, el patrón detrás de la Programación Orientada a Eventos (Tema 3.7)** — de hecho, `event`/`Publisher`/`Subscriber` en C# es una implementación nativa del patrón Observer.

```csharp
public interface IObservador { void Actualizar(string mensaje); }

public class Sujeto {
    private List<IObservador> observadores = new List<IObservador>();
    public void Suscribir(IObservador o) => observadores.Add(o);
    public void Notificar(string mensaje) {
        foreach (var o in observadores) o.Actualizar(mensaje);
    }
}

public class ObservadorConcreto : IObservador {
    public void Actualizar(string mensaje) => Console.WriteLine("Recibido: " + mensaje);
}
```
**Principio que ayuda a cumplir:** SRP (el sujeto no necesita saber qué hacen sus observadores, cada uno mantiene su propia responsabilidad) y DIP (el sujeto depende de la interfaz `IObservador`, no de clases concretas).

### Decorator
**Problema que resuelve:** añadir responsabilidades/comportamiento a un objeto de forma dinámica, sin modificar su clase ni usar herencia rígida para cada combinación posible.

```csharp
public interface ICafe { string Descripcion(); double Precio(); }

public class CafeSimple : ICafe {
    public string Descripcion() => "Café";
    public double Precio() => 3000;
}

public abstract class CafeDecorator : ICafe {
    protected ICafe cafeDecorado;
    public CafeDecorator(ICafe cafe) { cafeDecorado = cafe; }
    public virtual string Descripcion() => cafeDecorado.Descripcion();
    public virtual double Precio() => cafeDecorado.Precio();
}

public class ConLeche : CafeDecorator {
    public ConLeche(ICafe cafe) : base(cafe) { }
    public override string Descripcion() => cafeDecorado.Descripcion() + " + Leche";
    public override double Precio() => cafeDecorado.Precio() + 500;
}
// Uso: ICafe miCafe = new ConLeche(new CafeSimple());  -- se "envuelve" dinámicamente
```
**Principio que ayuda a cumplir:** SRP (cada decorador añade UNA responsabilidad) y OCP (agregar un nuevo "extra" — ej. `ConCanela`— no modifica `CafeSimple` ni los demás decoradores).

### Adapter
**Problema que resuelve:** hacer compatible una clase existente (que no puedes o no quieres modificar) con la interfaz que tu código espera.

```csharp
// Clase existente, de un proveedor externo, con una interfaz distinta a la que necesitas
public class ImpresoraVieja { public void ImprimirTextoPlano(string texto) => Console.WriteLine(texto); }

public interface IImpresoraModerna { void Imprimir(string documento); }

public class AdaptadorImpresora : IImpresoraModerna {
    private ImpresoraVieja impresoraVieja;
    public AdaptadorImpresora(ImpresoraVieja imp) { impresoraVieja = imp; }
    public void Imprimir(string documento) => impresoraVieja.ImprimirTextoPlano(documento); // traduce la llamada
}
```
**Principio que ayuda a cumplir:** ISP (permite cumplir con una interfaz pequeña y específica sin tocar la clase original) y DIP (el resto del sistema depende de `IImpresoraModerna`, no de `ImpresoraVieja`).

### Facade
**Problema que resuelve:** simplificar el uso de un subsistema complejo (varias clases con muchas interacciones) ofreciendo una interfaz única y sencilla al cliente.

```csharp
public class Facade_ProcesoCompra {
    private Inventario inventario = new Inventario();
    private ProcesadorPago pago = new ProcesadorPago();
    private ServicioEnvio envio = new ServicioEnvio();

    public void ProcesarCompra(string producto, double monto, string direccion) {
        inventario.Reservar(producto);
        pago.Cobrar(monto);
        envio.Despachar(direccion);
    }
}
// El cliente solo llama: facade.ProcesarCompra(...) sin conocer las 3 subclases internas
```
**Principio que ayuda a cumplir:** SRP (cada subsistema mantiene su propia responsabilidad; el Facade solo coordina) y reduce el acoplamiento del cliente con múltiples clases internas.

### Template Method
**Problema que resuelve:** definir el "esqueleto" invariable de un algoritmo en una clase base, dejando que las subclases personalicen solo pasos específicos, sin poder alterar el orden general — esto ayuda a **prevenir violaciones de LSP** porque el comportamiento global queda protegido.

```csharp
public abstract class ProcesoDeReporte {
    public void Generar() {                    // Template Method: define el ORDEN, no se sobrescribe
        ObtenerDatos();
        FormatearDatos();
        Exportar();
    }
    protected abstract void ObtenerDatos();
    protected abstract void FormatearDatos();
    protected virtual void Exportar() => Console.WriteLine("Exportando a PDF por defecto");
}

public class ReporteVentas : ProcesoDeReporte {
    protected override void ObtenerDatos() => Console.WriteLine("Obteniendo datos de ventas");
    protected override void FormatearDatos() => Console.WriteLine("Formateando ventas");
}
```
**Principio que ayuda a cumplir:** LSP (todas las subclases respetan el mismo flujo general, garantizado por la clase base) y OCP (nuevos tipos de reporte se agregan como nuevas subclases, sin modificar el algoritmo general).

### Singleton (ya visto en Tema 3.11) y Dependency Injection (ya visto en DIP)
Ambos ya fueron desarrollados con código completo en secciones anteriores.

### Resumen de la tabla Patrón → Principio(s) que ayuda a cumplir

| Patrón | Principio(s) SOLID relacionado(s) |
|---|---|
| Strategy | OCP, DIP |
| Factory | DIP, OCP |
| Observer | SRP, DIP |
| Decorator | SRP, OCP |
| Adapter | ISP, DIP |
| Facade | SRP (reduce acoplamiento del cliente) |
| Template Method | LSP, OCP |
| Singleton | (Gestión de instancia única — usar con moderación por DIP/testabilidad) |
| Dependency Injection | DIP (es su implementación práctica) |


---

# 🏗️ PROYECTO INTEGRADOR — Sistema de Concesionario de Vehículos

> Elegí este dominio porque **es literalmente el mismo que usa tu profesor** en los ejercicios de Herencia, Polimorfismo y en el caso de estudio de SOLID — dominarlo aquí te prepara directamente para tu **Primer Trabajo (25% de la nota)**, que exige exactamente este ejercicio: tomar un proyecto POO y rediseñarlo aplicando SOLID, con diagrama UML antes/después y sustentación de qué principio se aplicó y por qué.

## Versión Día 1 — Clases, atributos, accesores, constructores

```csharp
public class Automovil {
    public string Marca { get; private set; }
    public string Placa { get; private set; }
    private int anio;

    public int Anio {
        get => anio;
        set {
            if (value < 1990 || value > DateTime.Now.Year + 1)
                throw new ArgumentException("Año fuera de rango permitido");
            anio = value;
        }
    }

    public Automovil(string marca, string placa, int anio) {
        Marca = marca; Placa = placa; Anio = anio;
    }
}

public class Cliente {
    public string Nombre { get; set; }
    public string Cedula { get; set; }
}

public class Vendedor {
    public string Nombre { get; set; }
    public double Comision { get; set; }
}
```
**UML (Día 1):** 3 clases independientes, sin relaciones aún — solo estructura interna correcta (atributos privados, accesor `Anio` con validación, constructor).

## Versión Día 2 — Herencia, polimorfismo, interfaces + relaciones UML

```csharp
public abstract class Vehiculo {
    public string Placa { get; protected set; }
    public abstract void MostrarFicha();
}

public interface IMantenimiento {
    void ProgramarMantenimiento();
}

public class Automovil : Vehiculo, IMantenimiento {
    public string Marca { get; set; }
    public override void MostrarFicha() => Console.WriteLine($"Automóvil {Marca} - Placa {Placa}");
    public void ProgramarMantenimiento() => Console.WriteLine("Mantenimiento cada 5000km");
}

public class Camioneta : Automovil {
    public double CapacidadCarga { get; set; }
    public override void MostrarFicha() =>
        Console.WriteLine($"Camioneta {Marca} - Placa {Placa} - Carga {CapacidadCarga}kg");
}

// Venta: Asociación/Agregación con Cliente, Vendedor y Automovil (todos con vida propia fuera de la venta)
public class Venta {
    public Cliente Cliente { get; set; }
    public Vendedor Vendedor { get; set; }
    public Automovil Automovil { get; set; }
    public DateTime Fecha { get; set; }
}

// Concesionario: Composición hacia Venta (si el concesionario cierra, el registro de venta como
// entidad de ESE sistema deja de tener sentido) + Agregación hacia Vendedor
public class Concesionario {
    private List<Venta> ventas = new List<Venta>();     // COMPOSICIÓN
    private List<Vendedor> vendedores = new List<Vendedor>(); // AGREGACIÓN

    public void RegistrarVenta(Venta v) => ventas.Add(v);
}
```
**UML (Día 2):** `Camioneta` △→ `Automovil` △→ `Vehiculo` (abstracta); `Automovil` ┄△→ `«interface» IMantenimiento`; `Concesionario` ◆— `Venta` (composición); `Venta` — `Cliente`/`Vendedor`/`Automovil` (asociación/agregación, todos sobreviven a la venta).

## Versión Día 3 — LINQ, Eventos

```csharp
public class Concesionario {
    private List<Venta> ventas = new List<Venta>();
    public event Action<string> StockBajo;   // Evento (Publisher)

    private int stockDisponible = 3;

    public void RegistrarVenta(Venta v) {
        ventas.Add(v);
        stockDisponible--;
        if (stockDisponible == 0) StockBajo?.Invoke("¡Stock de vehículos agotado!");
    }

    // LINQ: funciones de orden superior aplicadas al reporte de ventas
    public double TotalVentasDelMes(int mes) =>
        ventas.Where(v => v.Fecha.Month == mes).Sum(v => 100); // simplificado

    public List<Vendedor> TopVendedores() =>
        ventas.GroupBy(v => v.Vendedor)
              .OrderByDescending(g => g.Count())
              .Select(g => g.Key)
              .ToList();
}

public class GerenteConcesionario {
    public void NotificarCompras(string mensaje) => Console.WriteLine("[Gerente] " + mensaje);
}
// concesionario.StockBajo += gerente.NotificarCompras;  (Subscriber)
```

## Versión Día 4 — Auditoría y Refactorización SOLID (formato idéntico al caso "Tránsito" de tu profesor)

> A continuación, el mismo formato **exacto** que tu profesor usa en el caso de estudio real (✅ Cumplidos / 🚫 No cumplidos / 👷 Qué hacer) — practica describiéndolo así en tu propio trabajo del 25%.

### Análisis clase a clase: `Automovil`

**✅ Principios cumplidos:** SRP (solo gestiona datos/comportamiento de un automóvil); encapsulamiento correcto con validación en `Anio`.
**🚫 Principios no cumplidos:** OCP — si aparece un nuevo tipo de vehículo con reglas de validación distintas, hay que tocar la jerarquía existente; DIP — si `Automovil` llegara a depender de constantes de `Concesionario` (como `anio_maximo`), se genera acoplamiento innecesario (igual que el caso real de tu deck con `valor_minimo_nuevo`).
**👷 Qué hacer:** extraer las validaciones a una clase/servicio `ValidadorVehiculo` inyectado por interfaz (`IValidadorVehiculo`), en vez de tenerlas embebidas en el `set` de cada atributo cuando crecen en complejidad.

### Análisis clase a clase: `Concesionario`

**✅ Principios cumplidos:** SRP parcial (coordina ventas); Composición correctamente aplicada hacia `Venta`.
**🚫 Principios no cumplidos:** SRP violado en la versión del Día 3 — `Concesionario` calcula reportes de ventas (`TotalVentasDelMes`, `TopVendedores`) **y** gestiona el registro de ventas **y** dispara eventos de stock. Son 3 responsabilidades.
**👷 Qué hacer:** extraer `ServicioReportesVentas` (con la lógica LINQ) y `ServicioInventario` (con la lógica de stock y el evento `StockBajo`), dejando `Concesionario` solo como coordinador de alto nivel que inyecta ambos servicios por interfaz (DIP).

### Análisis clase a clase: `Venta`

**✅ Principios cumplidos:** SRP (solo representa el hecho de negocio "una venta"); Asociación/Agregación correctamente modeladas hacia `Cliente`, `Vendedor`, `Automovil`.
**🚫 Principios no cumplidos:** ninguno grave detectado — es una clase simple y bien acotada. *(Nota pedagógica: no toda clase tiene que estar "mal"; parte de razonar bien sobre SOLID es reconocer cuándo el diseño ya es correcto, tal como el caso real de `Menor`/`Multa` en tu deck, que cumple varios principios sin problema.)*

### Refactorización final (aplicando DIP + OCP):
```csharp
public interface IValidadorVehiculo { void Validar(Automovil auto); }
public interface IServicioReportes { List<Vendedor> TopVendedores(List<Venta> ventas); }
public interface IServicioInventario {
    event Action<string> StockBajo;
    void RegistrarVenta();
}

public class Concesionario {
    private readonly IServicioInventario inventario;
    private readonly IServicioReportes reportes;
    private List<Venta> ventas = new List<Venta>();

    public Concesionario(IServicioInventario inventario, IServicioReportes reportes) {
        this.inventario = inventario;
        this.reportes = reportes;
    }

    public void RegistrarVenta(Venta v) {
        ventas.Add(v);
        inventario.RegistrarVenta();
    }

    public List<Vendedor> ObtenerTopVendedores() => reportes.TopVendedores(ventas);
}
```
Ahora `Concesionario` **depende de interfaces** (DIP), tiene **una única responsabilidad real** (coordinar el registro de ventas, delegando el resto — SRP), y se puede **extender** con nuevas formas de reportes o de gestión de inventario **sin modificar** `Concesionario` (OCP).


---

# 🧪 Examen SOLID — 40 Preguntas de Análisis (no de memoria)

> No mires las respuestas hasta intentarlo. Para las preguntas de código, identifica el principio violado Y propone la corrección.

1. Una clase `Reporte` genera datos, los formatea en HTML y los envía por correo. ¿Qué principio se viola?
2. Un método con 15 `if/else if` sobre un campo `tipoProducto`. ¿Qué principio se viola y qué patrón lo resuelve?
3. Una subclase `Pinguino : Ave` lanza excepción en `Volar()`. ¿Qué principio se viola?
4. Una interfaz `IReporte` con 12 métodos, de los cuales una clase solo usa 3. ¿Qué principio se viola?
5. Una clase `Pedido` crea internamente `new ServicioCorreo()` en vez de recibirlo por constructor. ¿Qué principio se viola?
6. ¿Cuál es la definición formal de SRP?
7. ¿Cuál es la definición formal de OCP?
8. ¿Cuál es la definición formal de LSP?
9. ¿Cuál es la definición formal de ISP?
10. ¿Cuál es la definición formal de DIP?
11. Nombra las 7 reglas formales de LSP vistas en clase.
12. ¿Qué patrón de diseño ayuda directamente a cumplir OCP mediante algoritmos intercambiables?
13. ¿Qué patrón ayuda a cumplir ISP cuando una clase externa no tiene la interfaz que necesitas?
14. ¿Qué patrón implementa DIP en la práctica?
15. Explica la diferencia entre SRP e ISP (uno sobre clases, otro sobre interfaces).
16. **(Código)** ¿Qué principio viola esto?
```csharp
class Pedido {
    public void Guardar() { /* SQL directo aquí */ }
    public void EnviarConfirmacion() { /* SMTP aquí */ }
    public double CalcularTotal() { /* ... */ return 0; }
}
```
17. Refactoriza el código de la pregunta 16 aplicando SRP.
18. **(Código)** ¿Qué principio viola esto?
```csharp
class ProcesadorPago {
    public void Procesar(TarjetaCredito t) { /* ... */ }
}
```
*(pista: solo acepta un tipo concreto, no una abstracción)*
19. Refactoriza la pregunta 18 aplicando DIP.
20. ¿Por qué "DIP va de la mano con OCP", según tu propio material?
21. Da un ejemplo (propio) de violación de LSP con excepciones inesperadas.
22. Da un ejemplo (propio) de violación de LSP reforzando una precondición.
23. ¿Qué significa que "las invariantes de la superclase deben conservarse"?
24. ¿Puede una subclase devolver un tipo MÁS ESPECÍFICO que el de la superclase sin violar LSP? Justifica.
25. ¿Puede una subclase aceptar un tipo de parámetro MÁS ESPECÍFICO sin violar LSP? Justifica. *(No, debe ser igual o más abstracto.)*
26. En el caso real del Tránsito de tu material, ¿por qué la clase `Mayor` viola SRP?
27. En el caso real del Tránsito, ¿qué solución propone el profesor para las excepciones usadas como notificación en `Mayor`?
28. ¿Qué es Inyección de Dependencias y cómo se relaciona con DIP?
29. Menciona 2 ventajas de aplicar LSP correctamente (según tu material).
30. Menciona 2 ventajas de aplicar ISP correctamente (según tu material).
31. ¿Cuál es la diferencia entre Strategy y Factory como patrones?
32. ¿Cuándo es una violación de OCP realmente un problema, y cuándo aplicar OCP sería sobre-ingeniería (YAGNI)?
33. Explica con tus palabras qué significa que "una clase esté cerrada" en el contexto de OCP.
34. Explica con tus palabras qué significa que "una clase esté abierta" en el contexto de OCP.
35. **(Código)** Identifica el/los principios violados y corrige:
```csharp
interface IAnimal { void Volar(); void Nadar(); void Correr(); }
class Perro : IAnimal {
    public void Volar() => throw new NotSupportedException();
    public void Nadar() => Console.WriteLine("Nadando");
    public void Correr() => Console.WriteLine("Corriendo");
}
```
36. ¿Qué patrón usarías para agregar "queso extra" y "tocineta" a una hamburguesa base sin crear una subclase por cada combinación?
37. ¿Qué diferencia hay entre Adapter y Facade?
38. En el caso real de `Vehiculo` de tu material, ¿qué recomienda el profesor para cumplir mejor OCP?
39. En el caso real de `Conductor` de tu material, ¿qué violación de responsabilidad detecta el profesor (generación de licencia)?
40. Diseña (sin código, solo el razonamiento) cómo aplicarías los 5 principios SOLID a un sistema de `Biblioteca` con `Libro`, `Usuario`, `Prestamo`.

### ✅ Corrección razonada (resumen — usa esto para autoevaluarte, no para copiar sin pensar)

1. **SRP** (3 responsabilidades: generación, formato, envío).
2. **OCP**; se resuelve con **Strategy**.
3. **LSP** (excepción inesperada en una capacidad que no todas las subclases cumplen).
4. **ISP**.
5. **DIP** (crea la dependencia internamente en vez de recibirla).
6-10. Ver las "Definiciones formales" de cada principio desarrolladas arriba — revísalas literalmente si fallaste alguna.
11. Ver Tema LSP, punto 8 (tabla de las 7 reglas).
12. **Strategy**.
13. **Adapter**.
14. **Dependency Injection**.
15. SRP limita las razones de cambio de una **clase concreta**; ISP limita el tamaño de un **contrato/interfaz** que otras clases deben implementar.
16. **SRP** (persistencia + notificación + cálculo de negocio mezclados).
17. Separar en `Pedido` (cálculo), `RepositorioPedido` (guardar), `ServicioNotificacion` (enviar confirmación).
18. **DIP/OCP** (depende de `TarjetaCredito` concreta, no de una interfaz `IMetodoPago`).
19. Crear `IMetodoPago` con `Procesar()`; `TarjetaCredito`, `PSE`, `Efectivo` la implementan; `ProcesadorPago.Procesar(IMetodoPago mp)`.
20. Porque al depender de abstracciones (DIP), agregar una nueva implementación (nuevo tipo de motor, de pago, etc.) no requiere modificar la clase de alto nivel (OCP) — ambos principios se refuerzan mutuamente.
21. Ejemplo tipo: `CuentaAhorro.Retirar()` nunca lanza excepción, pero `CuentaAhorroJunior.Retirar()` lanza una si el monto > 50000 — el cliente que no sabía de ese límite se rompe en producción.
22. Ejemplo tipo: `Figura.CalcularArea(double base, double altura)` acepta cualquier número positivo; `Triangulo.CalcularArea` (subclase) exige además que `base == altura` (precondición reforzada indebidamente).
23. Que las reglas que definen la validez de un objeto (ej. "el saldo nunca es negativo") se sigan cumpliendo en todas las subclases, sin excepción.
24. Sí — el punto 2 de las 7 reglas lo permite explícitamente ("puede devolver un tipo más específico").
25. No — rompería la sustituibilidad: código cliente que le pasa el tipo general esperado fallaría con la subclase.
26. Porque maneja cálculo de sanción, asignación de trabajo social, anulación de licencia y manejo de eventos — 4 responsabilidades distintas en una sola clase.
27. Usar el patrón **Observer** para eventos, en lugar de lanzar excepciones como mecanismo de notificación.
28. DIP es el **principio** (las dependencias deben ir hacia abstracciones); Inyección de Dependencias es el **patrón/mecanismo concreto** para lograrlo (recibir la abstracción ya resuelta, típicamente por constructor).
29. Código más robusto/mantenible; mayor reutilización de código (textual de tu material).
30. Evita clases sobrecargadas; mejora la flexibilidad (textual de tu material).
31. Strategy resuelve **cómo se ejecuta** un comportamiento (intercambiar algoritmos); Factory resuelve **cómo se crea** un objeto (intercambiar qué clase concreta se instancia).
32. Es un problema real cuando el negocio ya muestra evidencia de que ese eje cambia (nuevos tipos recurrentes); es sobre-ingeniería (YAGNI) crear abstracciones especulativas para variaciones que nunca se han presentado ni hay evidencia de que ocurrirán.
33. Que su interfaz pública ya está definida y no cambiará — el código que la usa puede confiar en que seguirá funcionando igual.
34. Que se puede extender su comportamiento (nuevas subclases, composición) sin tocar su código fuente ya probado.
35. Viola **ISP** (Perro no debería tener que implementar `Volar()`). Corrección: segregar en `IAnimalQueNada`, `IAnimalQueCorre`, `IAnimalQueVuela`; `Perro` implementa solo las 2 primeras.
36. **Decorator**.
37. Adapter hace que una interfaz existente **encaje** con la esperada (traduce un contrato); Facade **simplifica** el acceso a un subsistema complejo con muchas clases (no traduce, unifica).
38. Crear una interfaz `IVehiculo` para permitir diferentes implementaciones, y mover constantes (como el año) a configuración externa.
39. Que `Conductor` genera su propio número de licencia — una responsabilidad de infraestructura/generación que debería extraerse a un servicio `GeneradorLicencias`.
40. SRP: `Libro` (datos), `Prestamo` (registro del hecho de negocio), `ServicioNotificacionVencimiento` (aparte); OCP: `IPoliticaPrestamo` para distintos tipos de material con distintas reglas de días; LSP: si `LibroDigital` hereda de `Libro`, no debe romper el contrato de préstamo físico (ej. no debería fallar en `Devolver()` si nunca "se presta" físicamente — mejor usar interfaz separada `IPrestable` solo para lo prestable físicamente); ISP: separar `IPrestable` de `IConsultable` (un libro de referencia que no se presta, solo se consulta en sala); DIP: `Prestamo` depende de `IPoliticaPrestamo` y de `IServicioNotificacion`, inyectados por constructor, no creados internamente.


---

# 🎓 EXAMEN FINAL — Integrador de los 4 Días

> Simula el formato más probable de tu examen real: enunciado de negocio → diseño UML → implementación → identificación de principios. No mires las respuestas hasta terminar. Tómate ~60 minutos, sin material de apoyo, como simulacro real.

**Enunciado único para todo el examen: Sistema de Gestión de un Gimnasio.**
*El gimnasio tiene `Miembro`s que se inscriben a `Plan`es (Mensual, Trimestral, Anual). Cada plan tiene un precio y una duración. Un `Entrenador` puede dictar varias `Clase`s grupales (Yoga, Spinning, Crossfit); una clase pertenece a un único entrenador. El gimnasio necesita notificar automáticamente cuando el cupo de una clase se llena. Además, hay `MiembroVIP`s que tienen acceso a clases exclusivas y descuentos.*

### Parte A — UML (Días 1-2)
1. Identifica las clases candidatas del enunciado.
2. Diseña la relación entre `Gimnasio` y `Miembro` (¿asociación, agregación o composición? Justifica).
3. Diseña la relación entre `Entrenador` y `Clase` (incluye multiplicidad y navegabilidad).
4. Diseña la jerarquía de herencia para `Miembro`/`MiembroVIP`. ¿Es correcta la relación ES-UN aquí?
5. ¿Necesitarías una interfaz en este sistema? ¿Para qué?
6. Dibuja el diagrama de clases completo (notación extendida) para `Miembro`, `Plan`, `Clase`.

### Parte B — Implementación (Días 1-3)
7. Implementa en C# la clase `Plan` con validación (el precio no puede ser negativo, la duración debe ser >0).
8. Implementa `Miembro` y `MiembroVIP` con herencia y polimorfismo (`CalcularDescuento()`).
9. Implementa el evento `CupoLleno` en la clase `Clase`, y un `Entrenador` que se suscriba a notificarse.
10. Usa LINQ para obtener la lista de miembros VIP ordenados por fecha de inscripción.
11. Usa una lambda/`Func` para calcular el precio final de un plan con un descuento variable.

### Parte C — SOLID (Día 4)
12. Propón una violación de SRP en este sistema y su corrección.
13. Propón una violación de OCP en este sistema (ej. cálculo de descuento con `if/else`) y su corrección con Strategy.
14. Propón un escenario donde `MiembroVIP` podría violar LSP si no se diseña con cuidado, y cómo evitarlo.
15. Propón una interfaz mal segregada en este sistema (ISP) y su corrección.
16. Propón una violación de DIP (ej. `Gimnasio` creando directamente `new ServicioNotificacion()`) y su corrección con inyección de dependencias.

### Parte D — Arquitectura (Día 3)
17. ¿Qué estilo arquitectónico usarías si el gimnasio quiere una app móvil y un panel administrativo web compartiendo la misma lógica de negocio? Justifica.
18. ¿Cómo aplicarías el patrón MVC si construyes esto en ASP.NET Core?
19. ¿En qué vista del modelo 4+1 documentarías dónde se despliega la base de datos de miembros?

### Parte E — Preguntas conceptuales rápidas
20. Explica la diferencia entre Overload y Override con un ejemplo de este dominio (gimnasio).
21. ¿Qué es la inmutabilidad y la aplicarías a la clase `Plan`? Justifica sí o no.
22. Explica AOP con un ejemplo de este dominio (ej. loguear cada vez que un miembro se inscribe a una clase).

---

## ✅ Corrección del Examen Final (guía de evaluación, no respuesta única)

1. `Gimnasio`, `Miembro`, `MiembroVIP`, `Plan`, `Entrenador`, `Clase`, `Inscripcion` (posible clase de asociación).
2. **Agregación**: un miembro existe como persona incluso si cancela su membresía; no tiene sentido "componer" personas dentro de un gimnasio.
3. **Asociación** `Entrenador (1) — dictaA — (0..*) Clase`; navegabilidad `Entrenador -> Clase` (lista `clasesDictadas`), porque normalmente se pregunta "¿qué clases dicta este entrenador?".
4. `MiembroVIP △→ Miembro` — sí es correcta la relación ES-UN **si** `MiembroVIP` no rompe ningún comportamiento esperado de `Miembro` (cuidado con LSP: no debería, por ejemplo, lanzar excepción en un método que todo miembro regular sí puede ejecutar).
5. Sí: por ejemplo `INotificable` para cualquier entidad que deba recibir notificaciones (Miembro, Entrenador), o `IDescuento` para separar la lógica de descuento de la jerarquía de herencia si se vuelve compleja (ISP).
6. (Evalúa: 3 compartimentos, visibilidad correcta, tipos de retorno explícitos, atributos en singular.)
7. 
```csharp
public class Plan {
    public double Precio { get; private set; }
    public int DuracionMeses { get; private set; }
    public Plan(double precio, int duracionMeses) {
        if (precio < 0) throw new ArgumentException("Precio no puede ser negativo");
        if (duracionMeses <= 0) throw new ArgumentException("Duración debe ser mayor a 0");
        Precio = precio; DuracionMeses = duracionMeses;
    }
}
```
8.
```csharp
public class Miembro {
    public string Nombre { get; set; }
    public virtual double CalcularDescuento() => 0;
}
public class MiembroVIP : Miembro {
    public override double CalcularDescuento() => 0.15;
}
```
9.
```csharp
public class Clase {
    public event Action<string> CupoLleno;
    private int cupoActual, cupoMax;
    public void Inscribir() {
        cupoActual++;
        if (cupoActual >= cupoMax) CupoLleno?.Invoke("Cupo lleno");
    }
}
```
10. `miembros.Where(m => m is MiembroVIP).OrderBy(m => m.FechaInscripcion).ToList();`
11. `Func<double, double, double> precioConDescuento = (precio, descuento) => precio * (1 - descuento);`
12. Ej.: `Miembro` que además genera su propio recibo de pago en PDF → extraer `GeneradorReciboServicio`.
13. `if(esVIP) descuento=0.15; else if(...)` → `IEstrategiaDescuento` con `DescuentoVIP`, `DescuentoRegular`.
14. Si `Miembro.CancelarMembresia()` nunca lanza excepción, pero `MiembroVIP.CancelarMembresia()` la lanza porque "los VIP no pueden cancelar directamente" — viola LSP; mejor manejarlo con una regla de negocio explícita que no rompa el contrato (ej. un estado "pendiente de cancelación" en vez de una excepción).
15. Una interfaz `IMiembro` con `Inscribirse()`, `AccederClaseExclusiva()`, `RecibirDescuento()` obligaría a un `Miembro` regular a implementar métodos VIP → segregar en `IMiembroBase` e `IBeneficiosVIP`.
16. `Gimnasio` con `new ServicioNotificacion()` interno → inyectar `IServicioNotificacion` por constructor.
17. **Cliente-Servidor** con una API backend común (posiblemente estilo **SOA/Microservicios** si el gimnasio crece a una cadena con múltiples sedes) consumida tanto por la app móvil como por el panel web — la lógica de negocio vive en el backend, no duplicada en cada cliente.
18. `GimnasioController` con acciones `GET Listar()` y `POST Inscribir()`, inyectando `IMiembroServicio` por constructor (DIP), usando `Model`/`ViewBag` para pasar datos a las vistas Razor.
19. **Vista Física (Despliegue)**.
20. Overload: `Inscribir(Miembro m)` y `Inscribir(Miembro m, Plan p)` en la misma clase `Gimnasio` (mismo nombre, distintos parámetros, resuelto en compilación). Override: `Miembro.CalcularDescuento()` sobrescrito por `MiembroVIP` (mismo nombre y firma, jerarquía de herencia, resuelto en ejecución).
21. Sí tendría sentido para `Plan`: es un objeto simple, con pocos atributos, que rara vez cambia una vez creado (si cambia el precio, normalmente se crea un "Plan Mensual v2" en vez de mutar el existente, para no afectar retroactivamente a quienes ya se inscribieron con el precio anterior) — buen candidato a inmutabilidad.
22. Un aspecto de "Auditoría" con un Pointcut sobre el método `Inscribir()` de `Clase`, que registra automáticamente fecha, miembro y clase en un log centralizado, sin que `Clase` tenga que implementar esa lógica de logging directamente en su propio código.

---

# 📖 Glosario

- **Abstracción:** proceso de quedarse solo con las características relevantes de un concepto para el problema que se resuelve.
- **Accesor (get/set):** método controlado para leer/escribir un atributo privado, con posible transformación o validación.
- **Acoplamiento:** grado de dependencia entre módulos/clases; se busca que sea bajo.
- **Agregación:** relación Todo-Partes donde la parte puede existir independientemente del todo (rombo hueco).
- **AOP (Programación Orientada a Aspectos):** paradigma para modularizar preocupaciones transversales (logging, seguridad) separadas de la lógica de negocio.
- **Asociación:** relación estructural permanente entre dos clases, donde una usa/conoce a la otra (guardada como atributo).
- **Atributo derivado:** atributo que se calcula a partir de otros, no se almacena directamente; se marca con `/` en UML.
- **Class de asociación:** clase adicional que almacena datos propios de una relación entre otras dos clases.
- **Clase:** plano/plantilla que describe la estructura y comportamiento común de un conjunto de objetos.
- **Clase abstracta:** clase no instanciable, base para herencia, puede mezclar métodos concretos y abstractos.
- **Cohesión:** grado en que los elementos de un módulo están relacionados entre sí para un fin común; se busca que sea alta.
- **Composición:** relación Todo-Partes donde la parte NO tiene sentido fuera del todo (rombo relleno).
- **Constructor:** método especial, mismo nombre que la clase, sin tipo de retorno, ejecutado al instanciar con `new`.
- **DDD (Domain-Driven Design):** enfoque de modelado centrado en el dominio de negocio real.
- **Delegado:** tipo que referencia métodos con una firma compatible; base de `Func`/`Action` y de los eventos.
- **Dependencia:** relación débil y temporal entre clases (típicamente parámetro de método, sin persistir).
- **DIP:** las clases de alto nivel no deben depender de clases de bajo nivel concretas; ambas dependen de abstracciones.
- **Encapsulamiento:** agrupar atributos y comportamiento, exponiendo solo lo necesario (caja negra).
- **Enlace dinámico (dynamic binding):** resolución de qué método ejecutar, decidida en tiempo de ejecución según el tipo real del objeto (override).
- **Enlace estático (static binding):** resolución decidida en tiempo de compilación (overload).
- **Evento:** delegado especial (`event`) usado en el patrón Publisher/Subscriber.
- **Func / Action:** delegados genéricos predefinidos de .NET; `Func` retorna valor, `Action` no.
- **Generalización:** relación UML de herencia entre clases (triángulo hueco, línea continua).
- **Herencia:** mecanismo para compartir atributos/métodos y definir clases nuevas sobre clases existentes (ES-UN).
- **Inmutabilidad:** los objetos no cambian tras su creación; "modificar" implica crear una copia nueva.
- **Instanciación:** proceso de crear un objeto concreto a partir de una clase, con `new`.
- **Interfaz:** contrato público de métodos sin implementación ni estado; se "implementa" (Realización).
- **ISP:** una interfaz no debe obligar a implementar métodos que no se usan; preferir varias interfaces pequeñas.
- **Lambda:** función anónima y compacta, operador `=>`.
- **LINQ:** extensión de .NET con operaciones funcionales (Map/Filter/Fold) sobre colecciones, SQL, XML.
- **LSP:** los objetos de una subclase deben poder sustituir a los de la superclase sin alterar el comportamiento esperado.
- **Multiplicidad:** cantidad de objetos que participan en cada extremo de una relación UML.
- **MVC:** patrón arquitectónico Modelo-Vista-Controlador.
- **Navegabilidad:** dirección de una asociación UML, indica qué clase "conoce"/referencia a la otra.
- **Objeto:** instancia concreta de una clase, con estado propio en memoria.
- **OCP:** las clases deben estar abiertas para extensión, cerradas para modificación.
- **Overload (Sobrecarga):** mismo nombre de método/constructor, distinta firma de parámetros; se resuelve en compilación.
- **Override (Sobrescritura):** subclase redefine un método `virtual`/`abstract` de la superclase; se resuelve en ejecución.
- **Polimorfismo:** capacidad de que un mismo mensaje/método se comporte distinto según el objeto real que lo recibe.
- **Predicado:** expresión que evalúa una condición y devuelve `bool` (`Predicate<T>`).
- **Realización:** relación UML entre una clase y una interfaz que implementa (triángulo hueco, línea punteada).
- **Rol de asociación:** nombre del atributo generado por la navegabilidad de una asociación.
- **SOA:** estilo arquitectónico basado en servicios independientes que se comunican por contratos.
- **Singleton:** patrón que garantiza una única instancia de una clase en toda la aplicación.
- **SOLID:** conjunto de 5 principios de diseño OO (SRP, OCP, LSP, ISP, DIP).
- **SRP:** una clase debe tener una sola razón para cambiar.
- **Tipo anónimo:** clase creada "al vuelo" con `new { }`, sin declaración formal previa.
- **Value Object:** objeto que representa un valor (no una entidad con identidad), típicamente inmutable (mencionado como concepto complementario, ver LSP/DIP en el caso Vehículo).

---

# 🔑 Conceptos Clave (los que NO puedes fallar en el examen)

1. Clase vs. Objeto; las 6 propiedades de buen diseño OO (Modularidad, Extensibilidad, Complejidad, Reuso, Cohesión, Acoplamiento).
2. Los 3 tipos de atributos (dato, estado, proyecto) y los atributos derivados.
3. Los 4 modificadores de acceso y su símbolo UML.
4. Constructor: sin retorno, mismo nombre, sobrecargable; `this` para desambiguar.
5. Accesores get/set: validan (set) y pueden transformar (get).
6. Las 6 relaciones UML y su símbolo/línea: Dependencia (punteada, flecha simple), Asociación (continua, flecha simple), Agregación (rombo hueco), Composición (rombo relleno), Generalización (triángulo hueco, continua), Realización (triángulo hueco, punteada).
7. Herencia simple/multinivel (sí soportadas), múltiple de clases (NO soportada — se simula con interfaces).
8. `virtual`+`override` = polimorfismo real (enlace dinámico); `new` = ocultamiento (NO polimorfismo, enlace estático).
9. `abstract` = obligatorio sobrescribir; `virtual` = opcional.
10. Overload (mismo nombre, distinta firma, compilación) vs. Override (misma firma, jerarquía, ejecución).
11. `is`/`as` y los 4 métodos de `Object` (Equals, GetHashCode, GetType, ToString).
12. Lambda, Predicado, Delegado, Func/Action, LINQ (Map=Select, Filter=Where, Fold=Aggregate/Sum).
13. Inmutabilidad: cuándo sí (objetos simples, concurrencia) y cuándo no (objetos grandes/gradualmente poblados).
14. Publisher/Subscriber en Eventos; AOP (Aspecto, Advice, Pointcut, Join point, Weaving).
15. Modelo 4+1 (Lógica, Procesos, Desarrollo, Física, Casos de Uso) y los 11 estilos arquitectónicos.
16. Los 5 principios SOLID, sus definiciones EXACTAS, y al menos un patrón de diseño asociado a cada uno.
17. Las 7 reglas formales de LSP.
18. Los 3 errores comunes de DIP (acoplamiento fuerte, uso de implementaciones en vez de abstracciones, abuso de contenedores DI).

---

# ✅ Checklist para el Examen (repaso de 15 minutos antes de entrar)

> - [ ] Puedo explicar clase/objeto/abstracción sin dudar.
> - [ ] Puedo dibujar de memoria las 6 relaciones UML con su símbolo correcto.
> - [ ] Puedo explicar la diferencia entre `override` y `new` con un ejemplo de código.
> - [ ] Puedo explicar la diferencia entre Overload y Override sin confundirlos.
> - [ ] Puedo recitar las 5 definiciones formales de SOLID sin mirar el material.
> - [ ] Puedo dar un ejemplo de código que viole cada uno de los 5 principios SOLID, y su corrección.
> - [ ] Puedo explicar qué es un delegado y su relación con Func/Action y con Eventos.
> - [ ] Puedo explicar Map/Filter/Fold y dar su equivalente en LINQ (Select/Where/Aggregate-Sum).
> - [ ] Sé qué patrón de diseño usar para: intercambiar algoritmos (Strategy), crear objetos sin conocer la clase concreta (Factory), notificar cambios (Observer), añadir responsabilidades dinámicamente (Decorator), adaptar una interfaz existente (Adapter), simplificar un subsistema (Facade).
> - [ ] Recuerdo el caso de estudio real de tu profesor (Tránsito/Multa/Mayor/Menor) y qué violaba cada clase.
> - [ ] Puedo explicar los 4 componentes de ASP.NET Core MVC y la diferencia entre ViewBag/ViewData/TempData/Model.

---

# ❓ Preguntas Frecuentes

**¿El examen será en C# o podré responder en Java?**
Según el material, el curso completo está en C#/.NET — es altamente probable que el examen use sintaxis de C#. Prepárate para leer/escribir C#, aunque entiendas los conceptos también en Java.

**¿Debo memorizar los símbolos UML exactos o basta con entender el concepto?**
Ambos: tu material corrige explícitamente errores de estudiantes en los diagramas (ej. el caso de la Venta-Cliente-Automóvil), lo que sugiere que la notación exacta sí se evalúa, no solo el concepto.

**¿Vale la pena estudiar los 11 estilos arquitectónicos a fondo si el material solo los nombra?**
Repasa al menos las definiciones y diferencias clave (Monolito vs. Microservicios, SOA vs. Microservicios, Clean vs. Hexagonal) — es razonable que el examen pregunte definiciones y comparaciones, aunque no pida implementarlos en código.

**¿Qué tan importante es el caso de estudio de Tránsito?**
Muy importante — es el único ejemplo de tu material donde se aplica un análisis SOLID completo, clase por clase, con el formato exacto (✅/🚫/👷) que tu propio Primer Trabajo (25%) te pedirá replicar sobre tu propio proyecto.

**¿Debo saber programar con AOP y Proxies en detalle, o solo el concepto?**
El material es introductorio en este tema (corto, sin ejemplos de código propios) — prioriza entender el vocabulario (Aspecto, Advice, Pointcut, Join point) y el problema que resuelve, más que la sintaxis exacta de `DispatchProxy`.

---

# ⚠️ Errores Comunes (consolidado final — repaso rápido)

- Nombrar clases en plural o con nombres genéricos ("Manager", "Dato").
- Confundir `private`/`protected`/`public` en jerarquías de herencia.
- Poner tipo de retorno a un constructor.
- Olvidar `this` cuando el parámetro tiene el mismo nombre que el atributo.
- Confundir Agregación (rombo hueco, la parte sobrevive) con Composición (rombo relleno, la parte no sobrevive).
- Confundir Generalización (línea continua) con Realización (línea punteada) — ambas con triángulo hueco.
- Usar `new` (ocultamiento) esperando comportamiento de `override` (polimorfismo real).
- Confundir Overload (compilación) con Override (ejecución).
- Crear una interfaz "gigante" que viola ISP.
- Crear clases de alto nivel que instancian (`new`) directamente clases de bajo nivel, violando DIP.
- Usar herencia cuando la relación ES-UN no es sólida (mejor preferir composición o interfaces).
- Confundir SOA con Microservicios.
- Confundir `ViewBag`/`ViewData` (una petición) con `TempData` (sobrevive una redirección).
- Aplicar SOLID de forma dogmática sin razonar el trade-off (recordar SRP vs. sobre-fragmentación, OCP vs. YAGNI, DIP vs. KISS).

---

# Cierre

Este documento cubre el 100% de los temas presentes en tus 16 archivos, reorganizados pedagógicamente para un estudio de 4 días. Recomendación final de uso: no leas todo de corrido — resuelve cada bloque de ejercicios ANTES de mirar la corrección, y usa el Proyecto Integrador como tu propio "laboratorio" para practicar el mismo tipo de análisis que tu Primer Trabajo (25%) exige. Éxitos en tu examen.
