# Curso Intensivo de 4 Días — Arquitectura de Software
### (con repaso integral de POO, Paradigmas de Programación y fundamentos de Arquitectura)

> Preparado a partir de los 16 documentos oficiales de la asignatura. Cubre: repaso de POO (clases, relaciones, herencia, polimorfismo), UML completo, paradigma funcional (lambdas, delegados, LINQ, inmutabilidad), programación orientada a eventos, programación orientada a aspectos, introducción a arquitectura de software, principios SOLID, programación orientada a servicios (SOA) e introducción a ASP.NET Core MVC.
>
> **Convención:** todo lo marcado como `🧩 Contexto complementario` es un concepto necesario para entender el material que **no aparece explícitamente** en los PDFs, añadido para que no haya vacíos de comprensión. Todo lo demás proviene directamente de las 16 presentaciones oficiales.

---

## Fase 1 — Análisis Global (resumen ejecutivo)

Antes de construir el curso, se analizaron los 16 documentos. Hallazgos clave:

**Los documentos forman dos grandes bloques que se relacionan entre sí:**

1. **Bloque de repaso (Programación y Diseño OO / Paradigmas):** `2_Conceptos_Básicos_de_Clases`, `3_Accesibilidad...`, `4_Relaciones_entre_clases`, `5_Herencia`, `6_Polimorfismo`, `7_Object_As_Is`, `8_Programación_Orientada_Eventos`, `9_Paradigma_Orientado_a_Aspectos`, `Paradigma_Funcional_Del_Mut`, `1_1_Codificación_Funcional`, y una parte de `1_Repaso_POO_Paradigmas_NF` (que es en sí mismo un resumen que unifica casi todo el bloque anterior).
2. **Bloque nuevo de Arquitectura de Software:** `0_Presentacion_del_Curso` (logística, sin contenido técnico evaluable), `2_Intro_Arquitectura`, `3__Principios_SOLID`, `Introduccion-a-la-Programacion-Orientada-a-Servicios`, `Introduccion-al-Proyecto-Aplicacion-Web-ASPNET-Core-MVC`.

**Temas duplicados detectados (se unificaron sin perder ningún detalle):**
- Clases/objetos/atributos/UML básico aparece en `2_Conceptos_Básicos_de_Clases` **y** en `1_Repaso_POO_Paradigmas_NF` (slides 2-24) — contenido idéntico, unificado en Día 1.
- Constructores/accesores/instanciación aparece en `3_Accesibilidad...` **y** en `1_Repaso_POO...` (slides 9-18) — unificado en Día 1.
- Relaciones entre clases aparece en `4_Relaciones_entre_clases` **y** en `1_Repaso_POO...` (slides 26-32) — unificado en Día 2.
- Herencia aparece en `5_Herencia` **y** en `1_Repaso_POO...` (slides 33-42) — unificado en Día 2.
- Polimorfismo, overriding, interfaces aparece en `6_Polimorfismo` **y** en `1_Repaso_POO...` (slides 43-60) — unificado en Día 2.
- Paradigma funcional (lambdas, delegados, LINQ, tipos anónimos) aparece en **tres** documentos: `1_1_Codificación_Funcional`, `Paradigma_Funcional_Del_Mut`, y `1_Repaso_POO...` (slides 63-107) — unificado en Día 3. `Paradigma_Funcional_Del_Mut` es el único que agrega el tema de **mutabilidad/inmutabilidad**, que se integró.

**Dependencias detectadas (qué necesitas saber antes de qué):**
Clase/Objeto/Atributo → Constructores/Accesores/Instanciación → Relaciones entre clases → Herencia → Polimorfismo (Overriding, Interfaces) → Clase Object (is/as) → todo lo anterior es prerrequisito de **SOLID** (SOLID no se entiende sin herencia, interfaces y acoplamiento/cohesión). Paralelamente: Delegados → Expresiones Lambda → LINQ → Programación Orientada a Eventos (los eventos usan delegados) → Programación Orientada a Aspectos (requiere entender interfaces + proxys). Arquitectura de Software (estilos, vistas) es prerrequisito conceptual de SOA y de ASP.NET MVC (MVC es un estilo arquitectónico concreto).

**Conceptos fundamentales (alta probabilidad de examen):**
Clase vs Objeto vs Instancia; Cohesión/Acoplamiento; Encapsulamiento y modificadores de acceso; Constructor vs método normal; Sobrecarga (overload) vs Sobreescritura (override); las 5 relaciones UML (asociación, agregación, composición, dependencia, herencia) y cómo distinguirlas; clases abstractas vs interfaces; los 5 principios SOLID con ejemplos de violación y corrección; DIP + Inyección de Dependencias; estilos arquitectónicos (capas, monolito, cliente-servidor, SOA, microservicios, hexagonal, Clean Architecture); modelo 4+1 de Kruchten; MVC.

**Vacíos del material (rellenados como 🧩 Contexto complementario):** notación UML de multiplicidad avanzada con ejemplos combinados, diferencia formal Agregación/Composición en código C# con destructores, patrones de diseño asociados a cada principio SOLID (el material los menciona tangencialmente en la Fase de instrucciones pero no los desarrolla en los PDFs), buenas prácticas de nombramiento de servicios en SOA, ciclo de vida de un Singleton en .NET (Dependency Injection container).

**Posibles preguntas de examen** se identificaron por tema y se distribuyeron en los mini-exámenes de cada día, en el proyecto integrador y en el examen final.

---

## Fase 2 — Mapa de Conocimiento

```
NIVEL 0 (Base absoluta)
  Objeto → Clase → Atributo/Método → Abstracción/Encapsulamiento
       │
       ▼
NIVEL 1 (Estructura de una clase)
  Modificadores de acceso (Ocultamiento) → Constructor → Accesores (get/set) → Instanciación → this → Sobrecarga (overload)
       │
       ▼
NIVEL 2 (Relaciones entre clases — UML)
  Asociación → Agregación → Composición → Dependencia → Clases de asociación
       │
       ▼
NIVEL 3 (Herencia y jerarquías)
  Herencia (simple/múltiple/multinivel) → Clases abstractas → Miembros protegidos
       │
       ▼
NIVEL 4 (Polimorfismo — depende de Herencia)
  Overriding (virtual/override, abstract) → Ocultamiento con "new" → Interfaces → Overload como polimorfismo
       │
       ├──────────────────────────────────┐
       ▼                                  ▼
NIVEL 5a (Object / tipos)          NIVEL 5b (Paradigma funcional — rama independiente,
  Object, Equals/GetHashCode/        solo requiere Nivel 0-1)
  GetType/ToString, is/as              Delegados → Expresiones Lambda → Predicados →
                                        Funciones de orden superior → LINQ → Tipos
                                        Inmutables → Tipos Anónimos
       │                                  │
       │                                  ▼
       │                             NIVEL 6 (Programación Orientada a Eventos)
       │                               Requiere: Delegados (Nivel 5b)
       │                                  │
       └──────────────┬───────────────────┘
                       ▼
NIVEL 7 (Programación Orientada a Aspectos)
  Requiere: Interfaces (Nivel 4) + noción de Proxy 🧩
       │
       ▼
NIVEL 8 (Fundamentos de Arquitectura de Software)
  Definición de arquitectura → Vistas arquitectónicas → Modelo 4+1 → Estilos
  arquitectónicos (capas, monolito, cliente-servidor, MVC, SOA, microservicios,
  DDD, Clean Architecture, Hexagonal, Basada en eventos, Agéntica)
       │
       ▼
NIVEL 9 (Principios SOLID) — el más importante para el examen
  Requiere TODO lo anterior: Herencia (LSP), Interfaces (ISP, DIP), Acoplamiento/
  Cohesión (SRP), Polimorfismo (OCP)
       │
       ├───────────────────────┐
       ▼                       ▼
NIVEL 10a (SOA)          NIVEL 10b (ASP.NET Core MVC)
  Requiere: Nivel 8         Requiere: Nivel 8 (estilo MVC) + Nivel 9 (DIP se usa
  y noción de servicio      constantemente en Dependency Injection de .NET)
```

**Regla de estudio:** si vas a estudiar SOLID el día 4 sin dominar bien Herencia, Interfaces y Acoplamiento/Cohesión (Día 2), el 60% del principio no se va a entender realmente — solo memorizarás la sigla. Por eso el plan de 4 días respeta este orden.

---

## Fase 3 — Índice Maestro del Curso

**Bloque A — Fundamentos de Clases y Objetos**
1. Objeto, Clase, Abstracción, Cohesión, Acoplamiento, Modularidad, Extensibilidad, Reuso, Encapsulamiento
2. Atributos (tipos, derivados, restricciones), Operaciones/Métodos
3. Representación UML de una clase (notación compacta y extendida)
4. Errores comunes al nombrar clases/objetos/atributos

**Bloque B — Comportamiento y ciclo de vida del objeto**
5. Ocultamiento / Accesibilidad (modificadores de acceso)
6. Método Constructor (obligatorio, sin retorno, sobrecargado)
7. Métodos Accesores (Get/Set)
8. `this`
9. Sobrecarga de métodos (Overload)
10. Instanciación

**Bloque C — Relaciones entre clases (UML completo)**
11. Asociación (multiplicidad, navegabilidad, rol, clase de asociación)
12. Agregación (Todo-Parte débil)
13. Composición (Todo-Parte fuerte)
14. Dependencia
15. Cómo decidir qué relación usar — reglas prácticas

**Bloque D — Herencia**
16. Definición, relación "ES-UN"
17. Sintaxis en C#
18. Tipos de herencia (simple, múltiple —no soportada en C#—, multinivel)
19. Clases abstractas
20. Miembros protegidos vs privados en jerarquías

**Bloque E — Polimorfismo**
21. Definición y ejemplo intuitivo
22. Overriding: métodos virtuales (`virtual`/`override`) y métodos abstractos (`abstract`)
23. Ocultamiento con `new`
24. Interfaces (definición, herencia entre interfaces, segregación)
25. Overload como forma de polimorfismo
26. Atributos estáticos

**Bloque F — Clase Object y operadores de tipo**
27. Clase `Object` (Equals, GetHashCode, GetType, ToString)
28. Operadores `is` / `as`

**Bloque G — Paradigma Funcional**
29. Qué es la programación funcional, características, ventajas
30. Expresión condicional ternaria
31. Delegados (declaración, instancia, multicast, genéricos)
32. Expresiones Lambda (sintaxis de expresión y de instrucción)
33. Predicados (`Predicate<T>`)
34. Funciones de orden superior; delegados `Action<>` y `Func<>`
35. LINQ (categorías de métodos, ejemplos con listas y objetos)
36. Tipos Anónimos
37. Mutabilidad e Inmutabilidad (ventajas, desventajas, cuándo usar)

**Bloque H — Programación Orientada a Eventos**
38. Roles: Publisher, Subscriber, Delegado, Evento
39. Diagrama de clases de un evento, implementación en C#

**Bloque I — Programación Orientada a Aspectos**
40. El problema de las inquietudes transversales
41. Por qué OO no basta (y por qué AOP complementa a OO)
42. Conceptos: Punto de unión, Aspecto, Consejo (advice), Punto de corte (pointcut)
43. Implementación con proxys dinámicos (DispatchProxy, Castle.DynamicProxy)

**Bloque J — Fundamentos de Arquitectura de Software**
44. Qué es arquitectura de software, definiciones formales (IEEE 1471-2000)
45. Con/sin arquitectura de software: consecuencias
46. Rol del arquitecto de software
47. Procesos de diseño arquitectónico (Big Design Up Front vs Adaptativa)
48. Vistas arquitectónicas y Modelo 4+1 de Kruchten
49. Estilos/patrones arquitectónicos: Capas, Monolito, MVC, Cliente-Servidor (2N/3N/multinivel), SOA, Microservicios, DDD, Clean Architecture, Arquitectura Hexagonal, Arquitectura Basada en Eventos, Arquitectura Agéntica
50. Tipos de dependencias (verticales/horizontales)

**Bloque K — Principios SOLID** (el núcleo del examen de arquitectura)
51. Objetivo general de SOLID (Robert C. Martin)
52. SRP — Principio de Responsabilidad Única
53. OCP — Principio Abierto/Cerrado
54. LSP — Principio de Sustitución de Liskov (con las 7 reglas formales)
55. ISP — Principio de Segregación de Interfaces
56. DIP — Principio de Inversión de Dependencias + Inyección de Dependencias
57. Caso de estudio real del material: SOLID aplicado a Concesionario y a Tránsito/Multas

**Bloque L — Programación Orientada a Servicios**
58. Principios básicos (bajo acoplamiento, alta cohesión, interfaces claras, independencia)
59. Componentes clave (Servicios, APIs, Mensajería, Registro)
60. POO vs Orientación a Servicios
61. Servicios en ASP.NET Core (DI, acceso a datos, lógica empresarial)
62. Servicio vs Microservicio
63. Patrón Singleton aplicado al registro de servicios

**Bloque M — Introducción a ASP.NET Core MVC**
64. Arquitectura MVC (Modelo, Vista, Controlador)
65. Rol del controlador, verbos HTTP (GET, POST, PUT, PATCH, DELETE, HEAD, OPTIONS)
66. Vistas y Razor
67. ViewBag, ViewData, Model, TempData
68. Flujo completo de una solicitud en MVC

---

## Fase 4 — Plan Intensivo de 4 Días

**Supuesto:** jornadas de 8-9 horas efectivas de estudio, con descansos. Ajusta a tu ritmo, pero no te saltes el orden (respeta el mapa de conocimiento).

| Día | Bloques del Índice | Objetivo del día | Tiempo estimado |
|---|---|---|---|
| **Día 1** | A, B | Dominar la anatomía de una clase: qué es, cómo se representa en UML, cómo se construye (constructor, accesores, instanciación). Al final del día debes poder diseñar cualquier clase simple sin dudar. | 8 h (Teoría 3h / Práctica 3h / Repaso+Autoevaluación 2h) |
| **Día 2** | C, D, E, F | Relaciones entre clases completas (UML), Herencia y Polimorfismo. Es el día más denso — aquí se enseña UML con la metodología de intuición-primero. Al final debes poder leer y dibujar cualquier diagrama de clases. | 9 h (Teoría 4h / UML práctica 3h / Repaso 2h) |
| **Día 3** | G, H, I, J | Paradigma funcional completo (lambdas, LINQ, delegados), Eventos, Aspectos, y arranque de Arquitectura de Software (estilos, vistas, 4+1). Día de transición entre "programador" y "arquitecto". | 8.5 h (Teoría 4h / Práctica 3h / Repaso 1.5h) |
| **Día 4** | K, L, M | El corazón del examen: SOLID completo con metodología problema→refactor, más SOA y ASP.NET MVC. Cierre con Proyecto Integrador y Examen Final. | 9 h (Teoría 3.5h / SOLID práctica 3.5h / Examen final simulado 2h) |

**Antes de empezar cada día:** repasa en 10 minutos el resumen del día anterior (está al final de cada bloque).

---

# Día 1 — Fundamentos de Clases y Objetos

**Nota de idioma de código:** todo el material oficial del curso usa **C#** (no Java). Para que cada ejemplo se corresponda 1:1 con lo que verás en clase y en el examen, todos los ejemplos de este curso están en C#. La lógica es idéntica a Java en el 95% de los casos.

## Tema 1.1 — Objeto, Clase, Abstracción y las propiedades fundamentales de la POO

### Explicación

**Intuición primero.** Piensa en la palabra "casa". Cuando alguien te dice "casa", tu mente no piensa en una casa específica con una dirección exacta — piensa en un concepto general: algo con paredes, techo, puertas, ventanas. Ese concepto general, que ignora los detalles particulares de cada casa concreta y se queda solo con lo esencial y compartido, es una **abstracción**.

Ahora, si yo quisiera "construir" ese concepto en un programa, necesito un molde: una **clase**. La clase "Casa" no es una casa — es la plantilla que describe cómo es cualquier casa (tiene atributos como número de habitaciones, color, dirección) y qué puede hacer (abrir la puerta, encender las luces). Cuando yo uso ese molde para crear una casa concreta —mi casa, con mis valores específicos— eso es un **objeto** (una instancia de la clase).

**Definición formal:**
- **Objeto:** del latín *objectus* (ob: hacia, jacere: arrojar) — literalmente "algo que se puede arrojar/mostrar". En la práctica: cualquier cosa que tenga una estructura (datos) y un comportamiento (acciones) se puede modelar como objeto. Los objetos corresponden a sustantivos, se nombran en singular, y existen siempre en el contexto de un problema (una guitarra tiene un contexto distinto en un taller que en una banda musical).
- **Clase:** una descripción de un conjunto de objetos que comparten los mismos atributos, operaciones, relaciones y semántica. Es el agrupador de objetos del mismo tipo. Se nombra en **singular** y comienza con mayúscula.

**Estructura de un objeto:** atributos (propiedades/estado) + métodos (operaciones/comportamiento).

### Las propiedades del paradigma OO (pilares y propiedades de apoyo)

| Propiedad | Definición del material | Idea clave |
|---|---|---|
| **Abstracción** | Modelo de un objeto o fenómeno del mundo real, limitado a un contexto específico, que representa los detalles relevantes con precisión y omite el resto. | Cuanto más alto el nivel de abstracción, menos elementos necesitas para representar el sistema — más fácil de manejar la complejidad. Si un objeto tiene más características de las necesarias, se vuelve difícil de usar, modificar y entender. |
| **Encapsulamiento** | Propiedad de las clases para agrupar características y acciones bajo una misma unidad de programación ("caja negra"). | Se conoce el "qué" (los métodos públicos) pero no el "cómo" (la implementación interna). |
| **Herencia** | Capacidad de crear nuevas clases sobre las existentes. | Beneficio principal: reuso de código. Conceptos: Superclase (padre) y Subclase (hijo). (Se profundiza en el Día 2). |
| **Polimorfismo** | Capacidad de un programa de detectar la clase real de un objeto y llamar a su implementación específica. | (Se profundiza en el Día 2). |
| **Modularidad** | División de la solución en varias partes que se integran perfectamente entre sí. | Permite agregar o extraer componentes sin afectar el funcionamiento del todo. Disminuye la dificultad del problema, facilita actualización y comprensión. |
| **Extensibilidad** | Facilidad de modificar la solución durante su vida útil. | La modularidad facilita la extensibilidad. Cambios *externos* a un objeto repercuten en toda la solución; cambios *internos* solo afectan al objeto particular. |
| **Reuso** | Aprovechar módulos ya desarrollados. | Reduce tiempo de diseño, codificación y costo. Exige componentes genéricos, sencillos, con interfaces bien definidas. |
| **Cohesión** | Qué tan estrecha es la relación entre los componentes de algo para un fin común. | **Cohesión alta** = los métodos de una clase están relacionados entre sí, comparten una "temática" común. Es deseable. |
| **Acoplamiento** | Qué tanto dependen los módulos de un programa entre sí. | **Acoplamiento bajo** = componentes independientes (deseable). **Acoplamiento alto** = muchas dependencias entre componentes (indeseable). Existe entre métodos de una misma clase, entre clases distintas y entre paquetes. |

**🧩 Contexto complementario — Relación Cohesión/Acoplamiento:** el objetivo de todo buen diseño OO es **alta cohesión + bajo acoplamiento**. Una clase con alta cohesión y bajo acoplamiento es fácil de entender, probar, modificar y reutilizar sin afectar al resto del sistema. Este es el principio que subyace a SRP (Día 4).

### Atributos (propiedades)

- Definen la estructura de la clase y de sus objetos.
- Definen el valor de un dato para los objetos de esa clase.
- Corresponden a sustantivos; sus valores pueden ser sustantivos o adjetivos.
- Cada objeto puede tomar un valor de atributo igual o distinto a los demás.
- El nombre del atributo es **único dentro de la clase** (ej: no puede haber dos atributos "color"; deben diferenciarse como `colorFondo` y `colorFrente`).

**Tipos de atributos** (según el material, en el contexto de reglas de negocio):
- **De datos:** el usuario los cambia (ej: nombre, potencia del motor).
- **De estado:** los cambian exclusivamente los métodos (ej: cambio actual, abierto/cerrado).
- **De proyecto:** los define una regla de negocio de la organización (ej: valor de entrada a un parque, % de descuento VIP).

**Atributos derivados:** dependen de otros atributos (básicos o derivados). Se identifican con un slash `/` antes del nombre.
```
Clase: Persona
Atributos: Peso, Altura, /MasaCorporal
Restricción: { MasaCorporal = Peso / Altura² }
```

**Restricciones de atributos:** limitan los valores posibles. En el diseño se escriben fuera de la clase entre llaves `{ }`.

### Operaciones o Métodos
- Son las funciones/transformaciones que el objeto puede ejecutar.
- Son una acción (verbo) ejecutada sobre o por el objeto.
- Deben ser únicas dentro de cada clase (salvo que se use polimorfismo — Día 2).
- Si retorna un valor → se comporta como función. Si no retorna nada → se comporta como procedimiento.

### Representación UML de una clase

**Notación compacta:**
```
| NombreClase          |
```
**Notación extendida (la que usarás casi siempre):**
```
┌───────────────────────────────┐
│           Bicicleta            │  ← Nombre de la clase (singular, mayúscula inicial)
├───────────────────────────────┤
│ Marca: String                  │
│ Tamaño_Marco: float            │  ← Atributos
│ Tamaño_Llanta: float           │
│ Material: String                │
│ Número_Cambios: int             │
│ Color: String                   │
├───────────────────────────────┤
│ Bicicleta()                     │
│ Subir_un_Cambio(): int          │  ← Métodos (operaciones)
│ Bajar_un_Cambio(): int          │
│ Acelerar(int VelFinal): void    │
│ Desacelerar(int VelFinal): void │
└───────────────────────────────┘
```

**🧩 Contexto complementario — qué es UML formalmente:** UML (*Unified Modeling Language*) es un lenguaje gráfico estándar para diseñar soluciones de software. Tiene **diagramas estructurales** (Clases, Componentes, Despliegue, Objetos, Paquetes) y **diagramas de comportamiento** (Actividades, Casos de Uso, Estado) y **de interacción** (Comunicación, Secuencia, Tiempos). En este curso el foco casi total es el **Diagrama de Clases**, porque es el que conecta directamente con el diseño OO y con SOLID.

### Errores comunes en la definición de clases/objetos

1. **Nombrar en plural.** "Motores" no es una clase; "Motor" sí lo es. "Motores" describe una *cantidad* de objetos Motor.
2. **Usar partes o cualidades como clase.** "Lado Izquierdo" no es una clase (es, a lo sumo, un atributo o un rol).
3. **Llamar a una clase "Dato" o "Información".** Es un nombre demasiado genérico — no describe una entidad del dominio.
4. **Confundir clase con entrada/salida del programa.** Una clase no debe modelarse como si fuera un formulario de entrada de datos; debe modelar una entidad real del dominio del problema.

### Errores comunes al definir atributos

- Repetir nombres de atributo dentro de la misma clase.
- Modelar como atributo algo que en realidad es una clase completa (por ejemplo, si "Motor" tiene sus propios atributos y comportamientos, probablemente merece ser su propia clase y relacionarse — ver Día 2).
- Olvidar las restricciones del dominio (ej: un año que "puede ser igual al año actual o hasta 2 años mayor" — esa restricción **debe** quedar documentada, no solo el tipo de dato).

### Relaciones con lo que viene

Este tema es la base de **todo** el curso: sin entender qué es una clase, un objeto y sus atributos, no podrás entender relaciones entre clases (Día 2), ni herencia, ni SOLID (Día 4) — porque SOLID son reglas sobre *cómo diseñar bien las clases y sus relaciones*.

### Resumen — Conceptos clave

- Objeto = estructura (atributos) + comportamiento (métodos), instancia de una clase.
- Clase = molde/plantilla que agrupa objetos con atributos, operaciones, relaciones y semántica comunes.
- Abstracción = quedarse con lo esencial, ignorar el detalle irrelevante al contexto.
- Encapsulamiento = ocultar el "cómo", exponer el "qué".
- Alta cohesión + bajo acoplamiento = objetivo de todo buen diseño.
- Atributos: de datos, de estado, de proyecto; pueden ser derivados (`/atributo`) y tener restricciones (`{ }`).
- Clase en UML: nombre / atributos / métodos, en notación compacta o extendida.

### Ejercicios

**Conceptuales**
1. Explica con tus propias palabras la diferencia entre clase, objeto e instancia.
2. ¿Por qué "Ruedas" no puede ser el nombre de una clase? ¿Cuál sería el nombre correcto?
3. Da un ejemplo de atributo derivado que no sea `MasaCorporal`.
4. Explica por qué la cohesión alta y el acoplamiento bajo suelen ir de la mano en un buen diseño.

**Verdadero/Falso** (justifica cada respuesta)
5. Una clase puede tener dos atributos llamados `Color`, siempre que uno sea `int` y el otro `String`.
6. Los atributos de estado los puede cambiar directamente el usuario desde la interfaz.
7. Un objeto puede existir sin pertenecer a ninguna clase.
8. El nombre de una clase siempre debe escribirse en plural porque agrupa muchos objetos.

**Selección múltiple**
9. ¿Cuál de los siguientes NO es un pilar/propiedad mencionado del paradigma OO? (a) Herencia (b) Polimorfismo (c) Recursividad (d) Encapsulamiento
10. Un atributo derivado se identifica en el diseño con: (a) un asterisco `*` (b) un slash `/` (c) llaves `{}` (d) un signo `#`

**Diseño (UML)**
11. Diseña en notación extendida UML la clase `Estudiante` con al menos 4 atributos y 3 métodos, siguiendo las convenciones de nombramiento vistas.
12. Diseña la clase `CuentaBancaria` incluyendo un atributo derivado y su restricción.

**Programación (C#)**
13. Declara en C# (solo la forma de los atributos, sin implementación completa) la clase `Bicicleta` según la tabla de la sección de explicación.

### Mini Examen — Tema 1.1

*(Intenta resolverlo primero. Las respuestas están al final del Día 1, en la sección "Soluciones — Mini Exámenes Día 1").*

1. Explica la diferencia entre "cohesión" y "acoplamiento" con un ejemplo propio (no el de la bicicleta ni la persona).
2. ¿Qué está mal en esta definición de clase? `Clase: Colores. Atributos: nombre.`
3. Convierte a notación UML extendida: la clase `Libro` con atributos título (String), autor (String), año (int), precio (float) y un atributo derivado `/descuentoAplicado`.
4. V o F: "Un objeto puede tener un comportamiento distinto a otro objeto de la misma clase, incluso si ambos comparten los mismos atributos definidos por la clase." Justifica.

---

## Tema 1.2 — Ocultamiento (Accesibilidad), Constructores, Accesores e Instanciación

### Explicación

**Intuición primero.** Imagina un cajero automático. Vos no metes la mano directamente en la bóveda del banco (eso sería un atributo público sin protección) — usas una interfaz controlada: el teclado y la pantalla (esos son los "accesores"). El banco decide qué operaciones podés hacer (retirar, consultar saldo) y valida cada una antes de ejecutarla. Eso es exactamente lo que hace el **ocultamiento (encapsulamiento a nivel de código)**: protege los atributos y solo permite tocarlos a través de métodos controlados.

### Ocultamiento / Accesibilidad

Protege el acceso a los componentes de una clase mediante **modificadores de acceso**. Las clases pueden declararse:
- **Públicas.**
- **Sin modificador** (solo accesible desde clases en la misma ubicación/ensamblado).

El criterio de qué hacer público o privado depende del problema: ¿qué atributos deben ser modificables desde otras clases? ¿qué métodos deben ser visibles?

**Notación UML de visibilidad:**

| Símbolo | Significado |
|---|---|
| `+` | Público |
| `-` | Privado |
| `#` | Protegido |

Ejemplo (clase `Bicicleta` con ocultamiento aplicado):
```
+Marca: String        (público)
+Tamaño_Marco: float   (público)
-Cambio: int           (privado)
-Velocidad: int        (privado)
+Subir_un_Cambio(): int (método público)
```

### Método Constructor

- Es un **método** que se crea con el **mismo nombre de la clase**.
- Es **obligatorio** para instanciar (crear) objetos de una clase.
- **NO tiene tipo de retorno — ni siquiera `void`.**
- Su función: permitir la creación de un nuevo objeto cuando se usa la palabra reservada `new`.
- Se pueden declarar varios constructores para una misma clase → esto se llama **sobrecarga** (ver más abajo).
- En el constructor se pueden asignar valores a los atributos (a todos o a parte) y validar la información antes de pasarla al atributo (si este no es público).
- Los valores para inicializar se envían **como parámetros** al constructor (no se recomienda leerlos dentro del método, porque eso mezclaría entrada/salida con la lógica de la clase).

```csharp
public class Bicicleta
{
    private string marca;
    private float tamañoMarco;
    private int color;

    // Constructor vacío ("en blanco"): permite crear el objeto con atributos en blanco;
    // se asignan luego manualmente desde donde se creó el objeto.
    public Bicicleta() { }

    // Constructor con parámetros: asigna directamente el valor de los parámetros a los atributos.
    public Bicicleta(string marca, float marco, string material, int cambios, string color)
    {
        this.marca = marca;
        this.tamañoMarco = marco;
        // ... resto de atributos
    }
}
```

### Métodos Accesores (Get / Set)

Dos métodos disponibles para cada atributo:
- **Set:** modifica (cambia) el valor del atributo.
- **Get:** obtiene (lee) el valor del atributo.

Se puede usar solo `get`, solo `set`, o ambos. **Si otra clase quiere acceder a un atributo privado, debe hacerlo a través de los métodos accesores** — esta es la esencia del encapsulamiento aplicado en código.

```csharp
public class Bicicleta
{
    private string marca; // atributo privado

    public string Marca // accesor público con validación
    {
        get { return marca; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                marca = value;
            else
                throw new ArgumentException("La marca no puede ser vacía.");
        }
    }

    public Bicicleta(string marca)
    {
        this.Marca = marca; // usa el accesor, no el atributo directo -> se valida también aquí
    }
}
```

En notación UML compacta, los accesores se representan así: `+Marca{get,set}: String`.

### `this`

Palabra reservada que hace referencia explícita a un elemento **dentro de la misma clase**. Usos:
1. Cuando el nombre del atributo es igual al del parámetro y hay que diferenciarlos:
   `this.color = color;` (el primero es el atributo, el segundo el parámetro).
2. Para acceder a un atributo del objeto actual: `return this.cambio;`

### Sobrecarga (Overload)

Dos o más métodos (incluyendo el constructor) **dentro de la misma clase** pueden compartir el mismo nombre, siempre que se diferencien en la **declaración de parámetros** (cantidad y/o tipo).

```csharp
// Ejemplo del material: PerroCaliente
PerroCaliente()                              // abre el pan y pone la salchicha
PerroCaliente(salsa1, salsa2)                // + echa salsa1 y salsa2
PerroCaliente(salsa1, salsa2, tocineta)      // + agrega tocineta
```

Cuando se instancia un objeto, C# muestra **todos** los constructores definidos para que elijas el que necesitás. Igual pasa con los métodos sobrecargados.

### Instanciación

Proceso mediante el cual se crean objetos de una clase. Cada objeto instanciado:
- Es **independiente** de los demás objetos de la misma clase.
- Tiene su propia representación en memoria.
- Toma todas las propiedades definidas por la clase.
- Se le pueden asignar valores a sus atributos y ejecutar sus operaciones.

```csharp
Bicicleta bici1 = new Bicicleta("Trek", 54, "Aluminio", 21, "Rojo");
Bicicleta bici2 = new Bicicleta("Specialized", 52, "Carbono", 18, "Negro");
// bici1 y bici2 son independientes: cambiar bici1 no afecta a bici2
```

### Errores comunes

- Olvidar que el constructor **no lleva tipo de retorno**, ni siquiera `void` — es el error más frecuente de estudiantes que vienen de otros paradigmas.
- Escribir validaciones dentro del constructor en lugar de delegarlas al *setter* del accesor (rompe la reutilización de la validación).
- Confundir sobrecarga (mismo nombre, distintos parámetros, **misma clase**) con sobreescritura/*overriding* (mismo nombre, misma firma, **clase padre vs. clase hija** — se ve en el Día 2). Es la confusión #1 detectada en el material.
- Exponer atributos como públicos sin necesidad, perdiendo la posibilidad de validar (rompe el encapsulamiento).
- No usar `this` cuando el parámetro tiene el mismo nombre que el atributo, generando ambigüedad o, peor, que el atributo nunca se actualice (el compilador asigna el parámetro a sí mismo, no al atributo).

### Relaciones con lo anterior/siguiente

Este tema conecta directamente el "diseño" (Tema 1.1) con la "implementación" en C#. Es prerrequisito absoluto para el Día 2 (herencia necesita constructores encadenados con `base()`, polimorfismo necesita accesores bien diseñados) y para SOLID (SRP habla de que una clase tenga una sola razón para cambiar — si mezclás validación, lectura de datos y lógica de negocio dentro del constructor, ya estás violando SRP sin saberlo).

### Resumen — Conceptos clave

- Constructor: mismo nombre de la clase, sin tipo de retorno, obligatorio para instanciar, se puede sobrecargar.
- Accesores: Get (leer) / Set (modificar); son la puerta controlada a un atributo privado.
- `this`: referencia al objeto actual, se usa para desambiguar parámetro vs. atributo.
- Sobrecarga (Overload): mismo nombre, distinta firma de parámetros, dentro de la **misma clase**.
- Instanciación: crear un objeto con `new`; cada instancia es independiente en memoria.
- Visibilidad UML: `+` público, `-` privado, `#` protegido.

### Ejercicios

**Conceptuales**
1. ¿Por qué el constructor no puede tener tipo de retorno, ni siquiera `void`?
2. Explica la diferencia entre sobrecarga de constructores y tener un solo constructor con parámetros opcionales. ¿En qué se parecen conceptualmente?
3. ¿Por qué es mala práctica validar solo en el constructor y no en el *setter*?

**Verdadero/Falso**
4. Un `set` siempre debe existir si existe un `get` para el mismo atributo.
5. `this` solo puede usarse dentro de un constructor.
6. Dos constructores pueden tener exactamente los mismos tipos de parámetros si tienen distinto nombre de parámetro.

**Trampa (analiza con cuidado):**
7. Dado este código, ¿qué error de diseño hay?
```csharp
public class Persona
{
    public string nombre;
    public Persona(string nombre) { nombre = nombre; }
}
```
(Pista: revisa qué hace exactamente `nombre = nombre;`)

**Programación (C#)**
8. Escribe la clase `Lampara` (usa el ejercicio del material: marca en mayúscula sin espacios ni nulos, color solo Negro/Cromo/Rojo, voltaje 110V o 220V) con: atributos privados, accesores con validación, dos constructores (uno vacío y uno con parámetros), y demuestra la instanciación de dos lámparas en un `Main`.
9. Agrega a la clase anterior un método `Encender()` que solo cambie el atributo si la lámpara está apagada, y `CambiarBombillo()` que retorne `bool` indicando éxito, y **no** se pueda ejecutar si la lámpara está encendida.

### Mini Examen — Tema 1.2

1. Explica con tus palabras qué relación hay entre el ocultamiento (encapsulamiento) y los métodos accesores.
2. Corrige el siguiente constructor (tiene 2 errores de diseño):
```csharp
public void Bicicleta(string marca)
{
    marca = marca;
}
```
3. Diseña en UML compacto los accesores de la clase `Persona` con atributos privados `nombre` y `edad`.
4. V o F: "La sobrecarga permite que dos métodos tengan el mismo nombre y los mismos parámetros, pero diferente tipo de retorno." Justifica con la regla exacta del material.

---

## Proyecto Integrador — Etapa 1 (Día 1)

A lo largo de todo el curso vas a construir, en capas, un único proyecto: el **Sistema del Parque de Diversiones** (tomado directamente del material oficial — es el ejercicio más completo y recurrente en los documentos). Cada día le agregamos lo que aprendimos.

**Enunciado base (oficial, del documento de Relaciones entre clases):**
> Un parque de diversiones está compuesto por 10 atracciones y 3 taquillas. La persona, al ingresar, adquiere una manilla que carga con dinero (carga mínima $20.000). De la carga se descuentan $4.000 de ingreso, y el dinero restante se convierte en 1 punto por cada $500. El parque tiene nombre (mayor a 8 caracteres, se guarda y se entrega en mayúscula) y métodos para abrir/cerrar. Las taquillas solo venden manillas si el parque está abierto.

**Etapa 1 — Lo que ya puedes construir con lo del Día 1:**

Identifica los sustantivos candidatos a clase (técnica del material: "identifica los sustantivos, luego revisa si tienen atributos y comportamiento propio — si no tienen ninguno, se descarta como clase"):
- `Parque` → tiene atributos (nombre) y comportamiento (abrir, cerrar) → **es clase**.
- `Manilla` → tiene atributos (id, saldo) y comportamiento (cargar, descontar) → **es clase**.
- `Dinero` → no tiene comportamiento propio en este contexto → probablemente **no** es clase, es solo un valor (`decimal`/`float`).

```csharp
public class Manilla
{
    private string id;
    private int saldoPuntos;

    public string Id { get { return id; } }
    public int SaldoPuntos { get { return saldoPuntos; } }

    public Manilla()
    {
        // el id se calcula aleatoriamente y el saldo de puntos siempre inicia en 0
        id = Guid.NewGuid().ToString();
        saldoPuntos = 0;
    }

    public void CargarSaldo(decimal monto)
    {
        if (monto < 20000)
            throw new ArgumentException("La carga mínima es $20.000");
        decimal restante = monto - 4000; // se descuenta el ingreso
        saldoPuntos += (int)(restante / 500);
    }
}
```

*(Seguiremos construyendo esta clase `Parque`, `Taquilla`, `Atraccion` y `Registro` en los días siguientes, a medida que aprendamos relaciones, herencia, eventos y SOLID.)*

---

## Soluciones — Mini Exámenes Día 1

*(Revisa esto solo después de intentar resolver los mini exámenes tú mismo.)*

**Tema 1.1**
1. Cohesión = qué tan relacionados están los métodos/atributos *dentro* de una clase (ideal: alta). Acoplamiento = qué tanto depende una clase de otras clases externas (ideal: bajo). Ejemplo: una clase `CalculadoraImpuestos` con métodos `CalcularIVA()`, `CalcularRetencion()` tiene alta cohesión (todo gira en torno a impuestos). Si además necesita instanciar directamente `ConexionBaseDatos`, `ServicioCorreo` y `Logger` dentro de sus métodos, tiene alto acoplamiento (depende de muchas cosas externas concretas).
2. Está mal porque "Colores" está en plural (debería ser "Color") y porque la clase no describe comportamiento, solo un atributo — es una señal de que tal vez ni siquiera debería ser una clase, sino un tipo enumerado (`enum`), salvo que el dominio exija comportamiento propio.
3.
```
┌───────────────────────┐
│         Libro          │
├───────────────────────┤
│ titulo: String          │
│ autor: String            │
│ año: int                 │
│ precio: float             │
│ /descuentoAplicado        │
├───────────────────────┤
│ (métodos según necesidad) │
└───────────────────────┘
```
4. **Verdadero.** Aunque dos objetos compartan la misma clase (y por tanto la misma *estructura* de atributos), cada uno puede tener **valores distintos** en esos atributos, y por lo tanto su comportamiento (por ejemplo, un método que depende del valor de un atributo) puede diferir en el resultado, aunque el *código* del método sea el mismo. Esto no es polimorfismo (eso es otra cosa, Día 2) — es simplemente que cada objeto tiene su propio estado.

**Tema 1.2**
1. El ocultamiento hace privados los atributos para que no se puedan modificar libremente desde fuera de la clase; los accesores (get/set) son la **única puerta autorizada** para leer o modificar ese atributo, y esa puerta puede incluir validaciones — así se protege la integridad del objeto.
2. Errores: (1) falta el modificador `public` correcto en la firma del constructor — un constructor **no lleva `void` ni ningún tipo de retorno**, aquí está escrito como si fuera un método normal (`public void Bicicleta(...)`); un constructor real sería `public Bicicleta(string marca) { ... }`. (2) `marca = marca;` no asigna el parámetro al atributo — asigna el parámetro a sí mismo, porque no hay forma de diferenciarlos. Se corrige con `this.marca = marca;`.
3. `+Nombre{get,set}: String` y `+Edad{get,set}: int` dentro del bloque de métodos de la clase `Persona` en notación UML compacta.
4. **Falso.** La regla exacta del material es: "estos se diferencian en que la declaración de **parámetros** sea diferente" — es decir, la sobrecarga se basa en la firma de parámetros (cantidad y/o tipo), **no** en el tipo de retorno. Dos métodos con el mismo nombre, mismos parámetros y distinto tipo de retorno **no** son una sobrecarga válida (de hecho, en C# eso ni siquiera compila).

---

# Día 2 — Relaciones entre Clases (UML), Herencia y Polimorfismo

> Este es el día más denso del curso. Aquí UML se enseña como se pidió: primero intuición, después formalismo — nunca al revés. Tómate tu tiempo aquí; todo lo que viene después (incluido SOLID el Día 4) depende de que esto quede sólido.

## Tema 2.1 — Relaciones entre Clases (UML)

### 1. Intuición — antes de cualquier símbolo

Pensá en 6 escenarios de la vida real y hacete una pregunta simple en cada uno: **"si el objeto A desaparece, ¿el objeto B deja de tener sentido / deja de existir?"**

- **Universidad y Facultad:** si la Universidad desaparece, ¿la Facultad sigue teniendo sentido por sí sola? No — una Facultad sin Universidad no existe como tal. Fuerte.
- **Universidad y Estudiante:** si la Universidad desaparece, ¿el Estudiante (la persona) deja de existir? No, la persona sigue existiendo, solo deja de ser estudiante de esa universidad. Débil.
- **Biblioteca y Libro:** si la Biblioteca cierra, ¿los libros dejan de existir? No, se pueden mover a otra biblioteca. Débil-medio.
- **Biblioteca y Préstamo:** un objeto `Préstamo` sin una Biblioteca que lo respalde no tiene sentido — es un registro que pertenece exclusivamente a esa relación. Fuerte.
- **Banco y CuentaBancaria:** si el Banco desaparece, ¿la cuenta sigue existiendo? En términos prácticos no — la cuenta es parte constitutiva del banco que la emitió. Fuerte.
- **Hospital y Médico:** si el Hospital cierra, ¿el Médico deja de existir como persona/profesional? No, se va a trabajar a otro hospital. Débil.
- **Tienda y Factura:** una Factura sin Tienda que la emita no tiene sentido de existir por separado. Fuerte.
- **Videojuego y Personaje:** el personaje generalmente no tiene sentido fuera de la partida/juego que lo contiene. Fuerte-medio.
- **Videojuego y Jugador (persona real):** el Jugador sigue existiendo aunque el videojuego se desinstale. Débil.

Esta simple pregunta ("¿puede existir A sin B?") es el corazón de toda la teoría de relaciones UML que sigue.

### 2. Definición formal — las 5 relaciones

Del material oficial, la tabla central:

| Relación                        | Definición textual del material                                                                                                                                                                                     | Fuerza del acoplamiento                                                                                                    |
| ------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------- |
| **Asociación**                  | Un objeto usa o interactúa con otro. La flecha apunta al objeto que se utiliza. Se usa para representar un atributo tipo clase.                                                                                     | La más débil de las "permanentes"                                                                                          |
| **Agregación** ("Todo-Partes")  | La clase2 **puede existir aparte** de la Clase1. Acoplamiento más fuerte que la asociación. Una clase es el TODO, las demás son las PARTES.                                                                         | Media                                                                                                                      |
| **Composición** ("Todo-Partes") | La clase2 **solo existe como parte** de la Clase1. Tipo especial de agregación con restricciones: cada componente pertenece a un solo todo; si el todo se borra o se copia, sus partes se copian o suprimen con él. | Fuerte                                                                                                                     |
| **Dependencia**                 | Un tipo de asociación más débil: no hay relación permanente. A veces implica que una clase recibe a la otra como parámetro de un método.                                                                            | La más débil de todas                                                                                                      |
| **Herencia**                    | La Clase2 es una subclase de la Clase1 ("Es-Un(a)").                                                                                                                                                                | No es acoplamiento en el mismo sentido — es una relación de generalización/especialización (se profundiza en el Tema 2.2). |

**🧩 Contexto complementario — Realización (falta explícita en los PDFs pero es necesaria):** cuando una clase **implementa** una interfaz (no hereda de una clase concreta), esa relación se llama **Realización**. Se dibuja como una flecha discontinua con punta triangular hueca, apuntando a la interfaz, y se etiqueta "Implementa" — tal como menciona el material al hablar de interfaces en el Día 2 (Tema 2.3).

### 3. Cómo reconocerla — el cuestionario de decisión

Antes de dibujar nada, hacete estas preguntas en este orden:

1. **¿Es-Un(a) o Tiene-Un(a)?** Si A "es un tipo de" B → Herencia. Si A "tiene un/una" B → sigue a la pregunta 2.
2. **¿Puede existir A sin B (y viceversa)?** Si el "todo" no puede existir sin la "parte" específica y viceversa → Composición.
3. **¿La parte puede existir independientemente y quizás pertenecer a otro todo?** → Agregación.
4. **¿Es solo un uso puntual, no permanente (por ejemplo, un parámetro de un método)?** → Dependencia.
5. **¿Es un atributo permanente tipo clase, sin la restricción fuerte de la composición?** → Asociación.
6. **¿Quién crea a quién? ¿Quién es dueño de quién? ¿Quién depende de quién? ¿Quién conoce a quién?** — estas 4 preguntas del material te ayudan a decidir la dirección de la flecha y el rombo.

### 4. Cómo dibujarla

| Relación | Símbolo | Dirección |
|---|---|---|
| Asociación | Línea simple, con flecha `>` opcional (navegabilidad) | La flecha apunta a la clase **vista** (la que se convierte en atributo de la otra) |
| Agregación | Línea con **rombo blanco (hueco)** | El rombo va en la clase que representa el **TODO** |
| Composición | Línea con **rombo negro (relleno)** | El rombo va en la clase que representa el **TODO** |
| Dependencia | Línea **punteada** con flecha abierta | Apunta a la clase de la que se depende |
| Herencia | Línea sólida con **triángulo hueco** apuntando al padre | Apunta siempre a la **superclase** |
| Realización (interfaces) | Línea **punteada** con triángulo hueco | Apunta a la **interfaz**, etiqueta "Implementa" |

**Multiplicidad** (se coloca del lado de la clase asociada, junto a la línea):

| Símbolo | Significado |
|---|---|
| `1` | Uno a Uno |
| `0..1` | Cero o Uno |
| `1..n` | De 1 a n (n entero positivo) |
| `0..*` | De cero a muchos |
| `1..*` | De uno a muchos |
| `2` | Dos (o cualquier entero positivo exacto) |
| `5..11` | De 5 a 11 |
| `5, 11` | Cinco u once (uno de los dos, no en medio) |

**Navegabilidad:** se denota con `>` hacia la clase **vista**. Ayuda a establecer qué clase "ve" a la otra, de forma que la clase vista se convierte en un **atributo** de la clase que la ve.

**Rol de asociación:** el nombre que recibe el extremo de la relación desde el punto de vista de la clase que ve — generalmente se convierte en el nombre del atributo.

Ejemplo completo del material (Persona - Automóvil):
```
Persona ────Posee───> Automovil
   1                      0..*
```
Se lee: "la clase Persona ve/conoce a Automovil". `Automovil` es la clase vista, y en `Persona` aparecerá un atributo `l_automoviles: Automovil[]`.

**Clases de asociación:** son clases adicionales que aparecen conectadas a una asociación (con línea punteada) cuando la relación misma tiene atributos/métodos propios que no pertenecen ni a una clase ni a la otra. Ejemplo del material: `Persona` (1) —Posee→ `Automovil` (0..*), y la relación misma tiene una `Tarjeta_propiedad` (1 a 1 con la relación) que no es propiedad exclusiva de Persona ni de Automóvil, sino de la afiliación entre ambos.

### 5. Cómo se implementa en C# (Diagrama → Código → Diseño)

**Asociación / Agregación** (la clase "parte" se recibe o se asigna desde fuera; puede existir sin el todo):
```csharp
public class Automovil { /* ... */ }

public class Persona
{
    private List<Automovil> lAutomoviles = new List<Automovil>();

    public void AgregarAutomovil(Automovil auto) // el auto ya existe, se lo "agrega"
    {
        lAutomoviles.Add(auto);
    }
}
```

**Composición** (la clase "parte" se crea DENTRO del constructor del todo; no se recibe desde fuera):
```csharp
public class Tapa { /* ... */ }

public class Album
{
    private Tapa tapa; // Album "tiene" su propia Tapa

    public Album()
    {
        tapa = new Tapa(); // se crea internamente: Tapa no existe fuera de un Album
    }
}
```

**Dependencia** (la otra clase aparece solo como parámetro de un método, no como atributo):
```csharp
public class Impresora
{
    public void Imprimir(Documento doc) // Impresora "depende" de Documento solo durante esta llamada
    {
        Console.WriteLine(doc.Contenido);
    }
    // Documento NO es un atributo de Impresora -> no hay relación permanente
}
```

### 6. Errores comunes — por qué se confunden

- **Agregación vs. Composición:** el error #1. Ambas se ven casi iguales en UML (solo cambia si el rombo está relleno o hueco) y ambas son "Todo-Partes". La diferencia real está en la **vida útil**: en composición, si el todo muere, la parte muere con él (y en código, la parte se crea *dentro* del constructor del todo). En agregación, la parte sigue viva aunque el todo desaparezca (y en código, la parte se *recibe* desde fuera, ya creada).
- **Asociación vs. Dependencia:** ambas representan "uso", pero la Asociación es una relación **permanente** (se convierte en atributo), mientras que la Dependencia es **temporal** (aparece solo como parámetro de un método, variable local, o tipo de retorno — nunca como atributo).
- **Herencia vs. Composición ("Es-Un" vs. "Tiene-Un"):** confundir estas dos genera jerarquías de herencia rotas. Ejemplo clásico de error: hacer que `Motor` **herede** de `Automovil` porque "todo automóvil tiene motor" — esto está mal, un Motor NO ES UN Automovil, un Automovil TIENE UN Motor → es Composición, no Herencia.
- **Composición vs. Herencia (caso ambiguo real):** una `Rueda` ¿hereda de `Automovil` o es composición? Ninguna heredaría — `Rueda` no "es un" `Automovil`. Es composición: el Automóvil está compuesto por Ruedas.

### 7. Comparaciones directas

| | Asociación | Dependencia |
|---|---|---|
| Duración | Permanente | Temporal (una llamada) |
| ¿Se convierte en atributo? | Sí | No |
| Ejemplo | `Persona` tiene `List<Automovil>` | `Impresora.Imprimir(Documento doc)` |

| | Agregación | Composición |
|---|---|---|
| ¿La parte sobrevive sin el todo? | Sí | No |
| ¿Puede la parte pertenecer a más de un todo? | Sí, en general | No, pertenece a un único todo |
| ¿Cómo se crea la parte en código? | Se recibe/asigna desde fuera | Se crea dentro del constructor del todo |
| Símbolo | Rombo blanco (hueco) | Rombo negro (relleno) |

*(La comparación Herencia vs. Composición y Clase abstracta vs. Interface se desarrolla en el Tema 2.2 y 2.3, una vez viste herencia y polimorfismo — comparar antes sería prematuro.)*

### 8. Ejercicios visuales — piensa antes de mirar la solución

Para cada escenario, decide: ¿qué relación usarías? (Sistema de Biblioteca)

**Escenario A:** `Biblioteca` y `Libro`. La biblioteca tiene muchos libros; si la biblioteca cierra, los libros se trasladan a otra sede y siguen existiendo.

**Escenario B:** `Libro` y `Capitulo`. Un capítulo no tiene sentido fuera de un libro específico; si el libro se elimina del sistema, sus capítulos se eliminan con él.

**Escenario C:** `Bibliotecario` y `Libro` en el método `Bibliotecario.Catalogar(Libro l)`. El bibliotecario no "guarda" el libro como atributo, solo lo recibe momentáneamente para catalogarlo.

**Escenario D:** `EstudiantePosgrado` y `Estudiante`. Todo estudiante de posgrado es, ante todo, un estudiante.

*(Piensa tu respuesta antes de seguir leyendo — las respuestas con razonamiento completo están en la sección "9. Corrección" más abajo.)*

### 9. Corrección — razonamiento paso a paso

- **Escenario A → Agregación.** Los libros existen independientemente de esa biblioteca (pueden trasladarse). El rombo blanco va en `Biblioteca`.
- **Escenario B → Composición.** Un `Capitulo` no tiene sentido fuera de su `Libro`; se crea y muere con él. Rombo negro en `Libro`.
- **Escenario C → Dependencia.** `Libro` aparece solo como parámetro de un método, no como atributo permanente de `Bibliotecario`. Línea punteada.
- **Escenario D → Herencia.** "Es-Un(a)": `EstudiantePosgrado` **es un** `Estudiante` con comportamientos adicionales. Triángulo hueco apuntando a `Estudiante`.

### 10. Casos difíciles (ambigüedad real)

- **`Profesor` y `Curso`:** ¿el profesor "tiene" cursos (agregación) o simplemente está asociado (asociación)? — Depende del negocio: si el sistema no exige que el profesor "posea" el curso (el curso puede seguir existiendo con otro profesor asignado, o sin profesor), es **Asociación**, no Agregación. La agregación se reserva para relaciones Todo-Parte reales, no para cualquier "tiene una lista de".
- **`Pedido` y `LineaPedido`:** clásico caso de **Composición** (una línea de pedido no existe sin su pedido), pero muchos estudiantes lo modelan como Asociación simple porque "solo es una lista". Revisa siempre: si el Pedido se borra, ¿tiene sentido que sobrevivan sus líneas sueltas? No → Composición.
- **`Motor` en un `Automovil` vs. un `Motor` en un banco de pruebas independiente:** aquí la composición depende del **contexto del sistema que estás modelando**, no de una verdad absoluta. Si tu sistema modela motores que se venden por separado y luego se instalan, quizás sea Agregación. Si tu sistema solo modela automóviles completos donde el motor nunca se separa, es Composición. **UML no tiene una respuesta "correcta" universal — tiene la respuesta correcta *para tu contexto de negocio*.**

### 11. Reglas prácticas — Cómo decidir rápidamente

> - Si un objeto **puede existir sin el otro** → probablemente **NO** es composición (revisa si es agregación o asociación).
> - Si una clase **solamente usa otra temporalmente** (parámetro de método, variable local) → probablemente es **Dependencia**.
> - Si una clase **almacena permanentemente** otra como atributo, sin restricción de "parte exclusiva" → probablemente es **Asociación**.
> - Si además de "almacenar permanentemente", la parte **no tiene sentido fuera de ese todo específico y se crea dentro del todo** → es **Composición**.
> - Si "A es un tipo especial de B" (puedes sustituir A donde se espera B) → **Herencia**, no ninguna de las anteriores.

### Resumen — Conceptos clave

- 5 relaciones: Asociación, Agregación, Composición, Dependencia, Herencia (+ Realización como caso especial de interfaces).
- La pregunta clave siempre es: ¿puede existir A sin B?
- Multiplicidad se coloca del lado de la clase asociada; navegabilidad indica quién "ve" a quién.
- En código: Asociación/Agregación = la parte se recibe desde fuera; Composición = la parte se crea dentro del constructor del todo; Dependencia = la otra clase solo aparece como parámetro.


### 12. Ejercicio Final de UML — 50 ejercicios de análisis

Para cada uno decide, según corresponda: **tipo de relación**, **multiplicidad**, **navegabilidad**, **visibilidad** de atributos involucrados, e **implementación en C#** cuando se pida. No son ejercicios de memoria — son de análisis. Las soluciones razonadas están en la sección "Soluciones — Ejercicios UML" al final del Día 2.

**Bloque 1 — Tipo de relación (1 a 15)**
1. `Universidad` y `Facultad`.
2. `Universidad` y `Rector` (una persona).
3. `Factura` y `DetalleFactura`.
4. `Taxi` y `Conductor` (el conductor puede cambiar de taxi).
5. `Mecánico` y `Automóvil` en el método `Mecanico.Reparar(Automovil a)`.
6. `Perro` y `Animal`.
7. `Equipo de fútbol` y `Jugador`.
8. `Casa` y `Habitación`.
9. `Cliente` y `Cuenta Bancaria` (un cliente puede tener varias cuentas, que existen mientras el banco exista, pero podrían transferirse a otro cliente).
10. `Guitarra` y `Cuerda` (las cuerdas se crean con la guitarra y no tienen identidad fuera de ella en este sistema).
11. `Médico` y `Hospital` (el médico trabaja allí pero puede cambiar de hospital).
12. `Pedido` y `Cliente`.
13. `ClaseVehiculo` (abstracta) y `Camioneta`.
14. `Reporte` y `GeneradorPDF` (el reporte usa un generador solo al momento de exportarse, sin guardarlo).
15. `Libro` e `Interfaz IPrestable` que el libro implementa.

**Bloque 2 — Multiplicidad (16 a 25)**
16. Un `Departamento` tiene entre 1 y muchos `Empleados`; cada `Empleado` pertenece exactamente a un `Departamento`. Da la multiplicidad en ambos extremos.
17. Una `Orden de compra` puede tener de 1 a 20 `Items` (regla de negocio del sistema).
18. Un `Paciente` puede tener entre 0 y muchas `Citas Médicas`; cada `Cita` pertenece a exactamente 1 `Paciente`.
19. Un `Carro` tiene exactamente 4 `Ruedas` (ignorando el repuesto).
20. Un `Curso` puede tener exactamente 25 o 30 estudiantes (dos tamaños de grupo permitidos, ninguno intermedio).
21. Una `Aerolínea` tiene entre 5 y 200 `Aviones`.
22. Un `Empleado` tiene 0 o 1 `Supervisor` directo.
23. Un `Torneo` tiene entre 8 y 16 `Equipos` participantes.
24. Una `Factura` tiene 1 y solo 1 `Cliente` asociado.
25. Un `Autor` puede haber escrito de 0 a muchos `Libros`; cada `Libro` tiene 1 a 3 `Autores`.

**Bloque 3 — Navegabilidad, rol y visibilidad (26 a 35)**
26. En la relación `Persona`–`Automovil` (Persona conoce a Automovil, Automóvil no conoce a Persona), dibuja la flecha de navegabilidad e indica en qué clase aparece el atributo resultante.
27. ¿Qué nombre de rol/atributo le darías a la relación `Empresa`–`Empleado` desde el punto de vista de `Empresa`?
28. Si `Cliente` conoce a `Pedido` pero `Pedido` **también** necesita conocer a `Cliente` (para saber quién lo hizo), ¿la relación es navegable en una o en dos direcciones? ¿Cómo se llama eso?
29. En UML, ¿qué símbolo de visibilidad usarías para un atributo que solo pueden usar las subclases de una jerarquía?
30. Diseña la clase `Persona` con atributo `l_automoviles` como resultado de una relación navegable hacia `Automovil` (usa notación extendida completa con visibilidad).
31. En la relación `Cliente` (1) —Posee→ `TarjetaFidelidad` (1), donde la tarjeta almacena puntos y fecha de afiliación que no son propiedad natural de ninguna de las dos clases, ¿qué constructo UML usarías?
32. ¿Por qué normalmente los atributos derivados de una asociación (como `l_automoviles`) deben ser privados y accederse por un getter, aunque representen el resultado de una relación pública en el diagrama?
33. En el diagrama `Taquilla` (3) — vende → `Manilla` (0..*), ¿qué clase es la "vista" y en cuál aparece el atributo lista?
34. Explica la diferencia entre el **rol** de una asociación y el **nombre de la relación** (verbo, como "Posee").
35. Si una relación no tiene flecha de navegabilidad dibujada explícitamente, ¿qué se asume en la práctica de este curso?

**Bloque 4 — Implementación en C# (36 a 45)**
36. Implementa en C# la relación de Composición `Album`–`Tapa` (una tapa por álbum, creada internamente).
37. Implementa en C# la relación de Agregación `Equipo`–`Jugador` (el jugador se crea afuera y se agrega al equipo).
38. Implementa en C# la relación de Dependencia `ServicioEmail`–`Mensaje` (el mensaje solo se usa como parámetro del método `Enviar`).
39. Implementa en C# la relación de Asociación `Persona`–`Automovil[]` con su respectivo método para agregar un automóvil.
40. Implementa en C# la relación de Herencia simple `Empleado` → `Gerente` (sin métodos abstractos todavía).
41. Convierte este diagrama a código C#: `Biblioteca` (1) ◆—contiene—> `Ejemplar` (0..\*) [composición].
42. Convierte este diagrama a código: `Cliente` (1) —realiza→ `Pedido` (0..\*) [asociación con navegabilidad hacia Pedido].
43. Dado el método `public void ProcesarPago(TarjetaCredito t)`, identifica la relación entre la clase actual y `TarjetaCredito`, y justifica por qué NO debe ser un atributo.
44. Diseña en C# la clase de asociación `Matricula` que conecta `Estudiante` y `Curso` (agrega atributos propios de la relación: fecha, nota).
45. Explica, con código, qué cambiaría si `Rueda` fuera Agregación en vez de Composición dentro de `Automovil` (dónde se movería la creación del objeto `Rueda`).

**Bloque 5 — Casos difíciles y mixtos (46 a 50)**
46. `ClienteVIP` hereda de `Cliente`; además, cada `Cliente` tiene una lista de `Compra`. Dibuja el diagrama de clases completo con ambas relaciones correctamente diferenciadas.
47. Un `Hospital` tiene `Departamentos` (Composición: un departamento no existe fuera de un hospital específico en este sistema), y cada `Departamento` tiene `Médicos` asignados (Agregación: el médico puede transferirse de departamento sin dejar de existir). Dibuja el diagrama completo de 3 clases.
48. Explica por qué modelar `Ala` (de un avión) como clase que **hereda** de `Avion` sería un error de diseño, y cuál sería la relación correcta.
49. Un sistema de streaming tiene `Usuario`, `Playlist` y `Canción`. Una playlist pertenece a un único usuario y no tiene sentido sin él (Composición Usuario-Playlist); una canción puede estar en muchas playlists distintas y existir sin ninguna playlist (Asociación Playlist-Canción, muchos a muchos). Dibuja el diagrama completo.
50. Revisa el `Sistema del Parque de Diversiones` (Proyecto Integrador): clasifica la relación entre `Parque` y `Taquilla`, entre `Parque` y `Atraccion`, y entre `Taquilla` y `Manilla`, justificando cada una con la pregunta "¿puede existir la parte sin el todo, en este sistema?".

---

## Tema 2.2 — Herencia

### Explicación

**Intuición.** "Libro es un Material Bibliográfico" (ejemplo textual del material). Todo libro tiene lo que tiene cualquier material bibliográfico (código, título, ubicación), pero además tiene cosas propias (autor, ISBN). En vez de repetir código, "Libro" **hereda** de "MaterialBibliografico" todo lo común, y solo agrega lo específico.

**Definición formal:** la herencia es la propiedad para (1) compartir atributos y métodos entre clases y (2) definir nuevas clases usando como base clases ya existentes.
- La clase que **hereda** (es heredada por otras) se llama **superclase** o **clase padre**.
- La clase que **quiere heredar** se llama **subclase** o **clase derivada**.
- La subclase hereda los atributos y comportamientos **específicos** de la clase existente.
- Beneficios: desarrollo más fiable, comprensible, de bajo costo, adaptable y reutilizable.

**Relación "ES-UN":** es la relación semántica entre padre e hijo (`Libro` **es un** `MaterialBibliografico`).

### Sintaxis en C#
```csharp
class NombreClaseDerivada : NombreClaseBase { }

class Libro : MaterialBibliografico { }
```

### Tipos de herencia
1. **Simple:** una sola clase base, de la cual heredan una o más clases derivadas.
2. **Múltiple:** una clase deriva de **dos** clases padre. **No está soportada en C#** (a diferencia de otros lenguajes). Este es un dato de examen muy frecuente.
3. **De niveles múltiples:** una clase derivada se usa como base para otra clase derivada — se puede extender a tantos niveles como el problema lo requiera.

### Clases abstractas
- **No se pueden instanciar directamente.** Existen exclusivamente para ser heredadas.
- Se implementan anteponiendo la palabra `abstract` al nombre de la clase.
- En UML, se identifican escribiendo el nombre en **itálica**.
- Los **métodos abstractos** también van en itálica en UML y **deben** ser implementados obligatoriamente por las subclases.

```csharp
public abstract class Automovil
{
    public abstract void Repostar(); // método abstracto: sin cuerpo, obligatorio en subclases
}
```

### Miembros protegidos vs. privados en jerarquías

- Una subclase **no puede** acceder a atributos/métodos **privados** de su superclase.
- Para exponer detalles solo a la jerarquía (subclases) sin hacerlos totalmente públicos, la superclase usa el modificador **`protected`** en vez de `private`.
- Los miembros **protegidos** solo los pueden usar los métodos de las subclases.
- Los miembros **públicos** están disponibles para las subclases y para todos en general.

### Ejemplos progresivos

**Nivel 1 — Herencia simple:**
```csharp
public class Persona
{
    protected string nombre;
    protected string cedula;
    public Persona(string nombre, string cedula)
    {
        this.nombre = nombre;
        this.cedula = cedula;
    }
}

public class Vendedor : Persona
{
    private float comision;
    public Vendedor(string nombre, string cedula, float comision) : base(nombre, cedula)
    {
        this.comision = comision;
    }
}
```
*(Nota: `base(...)` invoca el constructor de la clase padre — necesario porque el padre no tiene constructor vacío en este ejemplo.)*

**Nivel 2 — Herencia multinivel + clase abstracta:**
```csharp
public abstract class Automovil
{
    protected string placa;
    public abstract void ConsultarMantenimiento(); // obligatorio para todos los tipos de auto
}

public class Camioneta : Automovil
{
    private bool doble;
    public override void ConsultarMantenimiento() { /* plan de mantenimiento propio */ }
}
```

### Errores comunes

- **Intentar herencia múltiple en C#** (`class X : A, B` con dos clases concretas) — no compila. Para simular herencia múltiple se usan **interfaces** (Tema 2.3).
- **Redefinir atributos en la subclase.** El material es explícito: "los atributos NO deberían redefinirse — esto daría a pensar que el hijo no es realmente un hijo de ese padre". Si sientes la necesidad de redefinir un atributo, repiensa la jerarquía; puede ser síntoma de una mala relación "Es-Un" (revisa si en realidad debería ser composición).
- **Usar herencia donde correspondía composición** ("Motor hereda de Automóvil" — error clásico, ver Tema 2.1).
- **Acceder a un miembro privado del padre desde el hijo** — no compila; hay que usar `protected`.
- **Instanciar una clase abstracta directamente** — no compila (`new Automovil()` si `Automovil` es `abstract`).

### Relaciones con lo anterior

La herencia formaliza en código la relación **"Es-Un(a)"** de UML vista en el Tema 2.1 (el triángulo hueco). Es la base absoluta del Tema 2.3 (Polimorfismo no existe sin herencia o interfaces) y del principio **LSP** de SOLID (Día 4), que literalmente son "reglas para diseñar bien la herencia".

### Resumen — Conceptos clave

- Superclase/padre vs. subclase/hijo; relación "Es-Un(a)".
- Sintaxis: `class Hija : Padre`.
- Herencia múltiple **no existe en C#** (sí existe para interfaces).
- Clase abstracta: no instanciable, itálica en UML, obligatoria en subclases si tiene métodos abstractos.
- `protected`: visible para la jerarquía, no para todo el mundo.
- No redefinir atributos en subclases.

### Ejercicios

1. ¿Por qué C# no permite herencia múltiple entre clases concretas? ¿Qué mecanismo la reemplaza?
2. Diseña la jerarquía (UML) `Automovil` (abstracta) → `Camioneta`, `Deportivo`, `Taxi` según el ejercicio oficial del concesionario (usa los atributos mencionados en el material: placa, marca, modelo, año, cilindraje, etc.).
3. ¿Qué pasa si intentas hacer `new Automovil()` cuando `Automovil` es abstracta? ¿Por qué el lenguaje lo impide?
4. V o F: "Una subclase puede acceder directamente a un atributo `private` de su superclase porque hereda todo de ella." Justifica.
5. Explica con tus palabras la diferencia semántica entre herencia simple y herencia de niveles múltiples, dando un ejemplo distinto al del concesionario.

### Mini Examen — Tema 2.2

1. Corrige el error de diseño: `class Rueda : Automovil { }`. Explica por qué está mal y cuál sería el diseño correcto.
2. ¿Cuál es la diferencia entre un método `virtual` y uno `abstract`? *(Se responde formalmente en el Tema 2.3, pero intenta razonarlo ya con lo visto de clases abstractas.)*
3. Diseña en C# la superclase abstracta `Persona` (cédula, nombre protegidos) y la subclase `Cliente` (agrega descuento) usando `base()` en el constructor.

---

## Tema 2.3 — Polimorfismo

### Explicación

**Intuición.** Pensá en la palabra "pintar". `Casa.pintar()`, `Auto.pintar()`, `Carretera.pintar()` — la misma palabra, el mismo "mensaje", pero cada objeto la ejecuta de forma completamente distinta según lo que realmente es. Eso es **polimorfismo**: "muchas formas". El método es polimórfico porque su comportamiento depende del objeto real que lo está ejecutando, no del tipo con el que fue declarada la variable.

**Definición formal:** capacidad de un programa de detectar la clase real de un objeto (aunque esté referenciado con un tipo más general) y llamar a **su** implementación específica.

### Se implementa mediante tres mecanismos

1. **Sobreescritura (Overriding)** — con métodos virtuales o abstractos.
2. **Interfaces.**
3. **Sobrecarga (Overload)** — como forma de polimorfismo (distinta de la sobrecarga de constructores del Día 1, aunque el mecanismo es el mismo).

### 1. Overriding (Sobreescritura)

"B" hereda características de "A", pero "B" **re-define** las funcionalidades (métodos) heredadas de "A". Si un método es redefinido en la subclase, se dice que fue **sobrescrito**.

**Con métodos virtuales:**
- Se declaran con la palabra `virtual` en la clase base.
- Se sobrescriben con `override` en la subclase.
- Un método virtual **PUEDE** ser sobrescrito, o usarse tal como está (es opcional).
- Solo se puede usar `override` si el método base está marcado `virtual`, `abstract`, u `override`.
- El método `override` debe mantener el **mismo nivel de acceso** que el `virtual` correspondiente.

```csharp
public class Vehiculo
{
    public virtual string PlanMantenimiento() => "Revisión general";
}

public class Deportivo : Vehiculo
{
    public override string PlanMantenimiento() =>
        "Lavado de inyectores, cambio aceite, revisión sonido, revisión techo, cambio aceite caja, revisión rines, revisión potencia";
}
```

**Con métodos abstractos:**
- Se declaran con `abstract` en la clase base (que a su vez debe ser `abstract`).
- Un método `abstract` **TIENE** que ser sobrescrito en la subclase — no es opcional (a diferencia de `virtual`).

```csharp
public abstract class Vehiculo
{
    public abstract string PlanMantenimiento(); // obligatorio en subclases
}
```

**🧩 Contexto complementario — `virtual` vs `abstract`, cuándo usar cada uno:** usa `virtual` cuando la clase base **sí** tiene una implementación por defecto razonable y las subclases *pueden* cambiarla si quieren. Usa `abstract` cuando la clase base **no tiene forma de saber** cuál sería una implementación por defecto razonable (obligas a cada subclase a decidir). Este criterio es clave para diseñar bien jerarquías y para OCP (Día 4).

### 2. Ocultamiento de métodos con `new`

- Cuando se usa `new` como modificador de una declaración, **oculta explícitamente** un miembro heredado de la clase base (no lo sobrescribe polimórficamente).
- La versión derivada reemplaza a la de la clase base, pero **sin** el comportamiento polimórfico real: si se llama al método desde una variable tipada como la clase base, se ejecuta la versión del padre, no la del hijo (a diferencia de `override`).
- Se puede ocultar sin `new` explícito, pero el compilador da una advertencia; usar `new` la suprime.
- **⚠️ Nunca usar `new` y `override` al mismo tiempo — son excluyentes.**

### 3. Interfaces

**Definición:** es el **qué** debería hacer una clase (todo lo que se puede hacer con ella), sin especificar el **cómo**. Es una especie de "clase abstracta pura" con métodos abstractos públicos sin código — el "contrato" que la clase implementadora debe cumplir. En la interfaz **no se maneja visibilidad** (todos sus miembros son, en esencia, públicos por contrato).

- Es una estructura de datos que muestra únicamente las **firmas** de los métodos.
- Se usa para **simular herencia múltiple** (una clase puede implementar varias interfaces, aunque solo pueda heredar de una clase concreta).
- Se etiqueta con **"Implementa"** en el diagrama (relación de Realización, ver Tema 2.1).

**¿Cuándo usar interfaces?** Cuando se tiene más de una clase que "hace lo mismo" (comparte comportamiento) pero **no necesariamente comparte una jerarquía de herencia natural**.

**Segregar interfaz:** dividir sus métodos en varias interfaces más pequeñas cuando una interfaz tiene muchos métodos que no todas sus clases implementadoras necesitan (esto es literalmente el germen del principio **ISP** de SOLID, Día 4).

```csharp
public interface IVehiculo
{
    void Acelerar(int kmh);
    void Frenar();
    void Girar(int angulos);
}

// Herencia entre interfaces:
public interface IVehiculoVolador : IVehiculo
{
    void Despegar();
    void Aterrizar();
}
```

**Ejemplo completo del material (Universidad):** una universidad tiene estudiantes de pregrado y posgrado. Comportamientos comunes a todos: `Estudiar()`, `Exponer()`. Solo posgrado tiene `EscribirTesis()`, `SustentarTesis()`.

```csharp
public abstract class Estudiante
{
    public void Estudiar() { /* ... */ }
    public abstract void Exponer(); // cada tipo expone distinto
}

public interface IInvestigador
{
    void EscribirTesis();
    void SustentarTesis();
}

public class EstudiantePregrado : Estudiante
{
    public override void Exponer() => Console.WriteLine("Expone un trabajo de clase");
}

public class EstudiantePosgrado : Estudiante, IInvestigador
{
    public override void Exponer() => Console.WriteLine("Expone su investigación");
    public void EscribirTesis() { /* ... */ }
    public void SustentarTesis() { /* ... */ }
}
```
Aquí se ve cómo se combinan **Herencia** (todo estudiante "es un" `Estudiante`) e **Interfaces** (solo posgrado "implementa" `IInvestigador`) en el mismo diseño.

### 4. Sobrecarga (Overload) como polimorfismo

Dos formas mencionadas en el material:
- **Sobrecarga paramétrica – 1:** mismo método, diferentes parámetros, mismo tipo (ej: distinta cantidad de parámetros del mismo tipo).
- **Sobrecarga paramétrica – 2:** mismo método, diferentes parámetros, diferentes tipos.
- **Polimorfismo de sobrecarga (entre clases distintas, sin herencia):** mismo nombre de método en clases completamente distintas, sin relación de herencia entre ellas, haciendo cosas totalmente diferentes en cada una. Ejemplo: `RepararMotor()` existe en `Vehiculo`, `Electrodomestico` y `Elevador` — el nombre es el mismo, pero no comparten jerarquía y cada una lo implementa a su manera. También aplica **entre subclases de una misma jerarquía**: `SubirCambio()` es distinto en `CarroMecanico`, `CarroAutomatico`, `Tractomula`, `Bicicleta`.

### Atributos estáticos

- **No existen "atributos abstractos"** — el material lo aclara explícitamente porque es una confusión común. Lo que sí existe son **atributos estáticos**.
- Un atributo estático se **comparte entre todos los objetos** instanciados de una clase.
- Uso típico: numeración consecutiva (ej: número de boleta o de factura). Se define un atributo estático y se maneja desde el constructor o cualquier método.

```csharp
public class Factura
{
    private static int consecutivo = 0;
    private int numero;

    public Factura()
    {
        consecutivo++;
        numero = consecutivo; // compartido entre TODAS las instancias
    }
}
```

### Errores comunes

- Confundir `virtual`/`override` con `new` (ocultamiento) — producen resultados **distintos** al invocar el método desde una referencia de tipo base. Es una de las preguntas trampa más típicas de examen.
- Olvidar que un método `abstract` es **obligatorio** de sobrescribir, mientras que uno `virtual` es **opcional**.
- Pensar que interfaces manejan visibilidad — no la manejan; todo en una interfaz es, por contrato, implementable públicamente.
- Creer que una interfaz "es una clase abstracta con otro nombre" — se parecen, pero una clase solo puede heredar de **una** clase (abstracta o no), mientras que puede implementar **varias** interfaces.
- Usar `new` y `override` en el mismo método — no compila y, conceptualmente, son mecanismos opuestos.

### Comparaciones (Clase abstracta vs. Interface)

| | Clase Abstracta | Interface |
|---|---|---|
| ¿Puede tener implementación de métodos? | Sí (métodos concretos + abstractos) | No (solo firmas — salvo *default methods*, fuera del alcance de este curso) |
| ¿Puede tener atributos? | Sí | No (no maneja estado propio) |
| ¿Cuántas puede "heredar/implementar" una clase? | Solo **una** clase base | **Varias** interfaces |
| ¿Maneja visibilidad? | Sí (`public`, `protected`, `private`) | No |
| ¿Cuándo usarla? | Cuando hay una jerarquía natural "Es-Un" con comportamiento compartido real | Cuando distintas clases, con o sin relación de herencia, deben cumplir el mismo "contrato" de comportamiento |

### Relaciones con lo anterior

El polimorfismo es la consecuencia práctica y más poderosa de la herencia y las interfaces: te permite tratar objetos distintos de manera uniforme (por su tipo base o interfaz común) mientras cada uno ejecuta su propia lógica. Es la base técnica del principio **OCP** de SOLID ("abierto para extensión, cerrado para modificación" se logra casi siempre con polimorfismo) y del **LSP** (que exige que el polimorfismo no rompa el comportamiento esperado).

### Resumen — Conceptos clave

- Polimorfismo = mismo mensaje, comportamiento distinto según el objeto real.
- `virtual`+`override` = sobrescritura polimórfica real y opcional; `abstract` = obligatoria.
- `new` = ocultamiento, NO es polimorfismo real (rompe si usas la variable con el tipo padre).
- Interfaces = contrato sin implementación, sin visibilidad, permite "herencia múltiple" simulada.
- No existen atributos abstractos, existen atributos **estáticos** (compartidos entre instancias).

### Ejercicios

1. Explica con tus palabras por qué `new` NO es polimorfismo real, con un ejemplo de código donde el resultado cambie según el tipo de la variable usada para invocar el método.
2. Diseña (UML + C#) el comportamiento polimórfico "Consultar plan de mantenimiento" para `Deportivo`, `Camioneta` y `Taxi`, heredando de `Automovil` abstracta, usando los planes exactos del material (ver Tema 6 del material original).
3. ¿Por qué una interfaz no puede tener atributos con estado?
4. V o F: "Un método `virtual` obliga a todas las subclases a sobrescribirlo." Justifica con la regla exacta.
5. Diseña un atributo estático `contador` para la clase `Boleta`, y demuestra en un `Main` que se comparte entre 3 instancias distintas.

### Mini Examen — Tema 2.3

1. Diferencia, con una tabla propia, `virtual`/`override` vs. `abstract` vs. `new`.
2. Dado el siguiente código, ¿qué imprime y por qué? (pregunta trampa clásica)
```csharp
public class A { public virtual void Saludar() => Console.WriteLine("Hola desde A"); }
public class B : A { public override void Saludar() => Console.WriteLine("Hola desde B"); }
// main
A obj = new B();
obj.Saludar();
```
3. Ahora cambia `override` por `new` en `B` y vuelve a responder qué imprime `obj.Saludar()` con la misma declaración `A obj = new B();`. Explica la diferencia con la pregunta anterior.
4. ¿Cuándo elegirías una interfaz en lugar de una clase abstracta para el diseño de `IPrestable` en un sistema de biblioteca?

---

## Tema 2.4 — Clase `Object` y operadores `is` / `as`

### Explicación

**Intuición.** En C#, absolutamente **todo** hereda de una clase raíz llamada `Object` (así como en biología todo ser vivo pertenece, en última instancia, a la categoría "ser vivo"). Por eso una variable de tipo `object` puede contener una referencia a **cualquier tipo de objeto**, aunque solo pueda usar directamente los 4 métodos que `Object` define.

### Definición formal

- `Object` es la clase superior de todas las clases en C#.
- Una variable declarada como `object` puede recibir un objeto de **cualquier tipo**.
- Sin embargo, esa variable **no puede llamar métodos propios del tipo real** — solo los 4 métodos genéricos que ofrece `Object`:
  - `Equals()`
  - `GetHashCode()`
  - `GetType()`
  - `ToString()`

### Operador `is`

Verifica si el tipo de un objeto **en tiempo de ejecución** es compatible con otro tipo dado.
```csharp
if (vehiculo is Deportivo) { /* ... */ }
```

### Operador `as`

Realiza conversiones (*cast*) entre tipos compatibles. Es similar a hacer un *cast* explícito, pero más seguro: si la conversión no es válida, retorna `null` en vez de lanzar una excepción.
```csharp
Deportivo d = vehiculo as Deportivo; // si vehiculo no es Deportivo, d queda en null (no explota)
```

### Errores comunes

- Usar un *cast* explícito `(Deportivo)vehiculo` en vez de `as` cuando no estás seguro del tipo real — un *cast* fallido lanza `InvalidCastException`, mientras que `as` fallido simplemente da `null` (hay que decidir cuál comportamiento quieres).
- Olvidar validar `null` después de usar `as` (si el objeto no era del tipo esperado, seguir usándolo como si lo fuera genera `NullReferenceException`).
- Intentar llamar un método específico de una subclase directamente sobre una variable `object` sin antes verificar el tipo con `is` o convertir con `as`.

### Relaciones con lo anterior

`Object` es la "superclase de todo", el techo absoluto de cualquier jerarquía de herencia que diseñes (Tema 2.2) — por eso todo objeto en C#, sin excepción, puede usar `ToString()` o `Equals()`, aunque nunca los hayas definido explícitamente en tu clase. Los operadores `is`/`as` se vuelven imprescindibles cuando trabajas con polimorfismo (Tema 2.3) y necesitás, en tiempo de ejecución, saber qué tipo concreto tenés detrás de una referencia general.

### Resumen — Conceptos clave

- Todo hereda de `Object`: `Equals`, `GetHashCode`, `GetType`, `ToString`.
- `is`: pregunta booleana sobre el tipo real de un objeto.
- `as`: intenta convertir; si falla, retorna `null` (no lanza excepción).

### Ejercicios

1. ¿Qué diferencia hay entre usar `(Deportivo)vehiculo` y `vehiculo as Deportivo`?
2. Escribe un método que reciba una lista de `object` (mezcla de `Deportivo`, `Camioneta`, `Taxi`) y use `is` para contar cuántos son de cada tipo.
3. ¿Por qué toda clase en C# "hereda" `ToString()` aunque nunca escribas `: Object` explícitamente?

### Mini Examen — Tema 2.4

1. V o F: "`as` lanza una excepción si la conversión no es válida." Justifica.
2. ¿Qué método de `Object` usarías para comparar si dos objetos representan el mismo valor lógico (más allá de si son la misma instancia en memoria)?

---

## Proyecto Integrador — Etapa 2 (Día 2)

Con relaciones, herencia y polimorfismo, ya podemos construir el modelo completo del Parque:

```
┌──────────────┐  1        3 ┌────────────┐  0..*        1  ┌───────────┐
│    Parque     │◆───────────│  Taquilla   │────vende────>   │  Manilla   │
├──────────────┤  compone     ├────────────┤                 ├───────────┤
│ -nombre       │              │ -idTaquilla │                 │ -id        │
│ -abierto: bool│              │ -saldo      │                 │ -saldoPuntos│
│ -l_manillas    │              │ -abierta    │                 └───────────┘
│ -l_atracciones │              └────────────┘
│ -l_taquillas    │  1        10 ┌────────────┐
│ -l_registros     │◆──────────── │ Atraccion   │
├──────────────┤  compone       ├────────────┤
│ +Abrir()         │              │ -nombre     │
│ +Cerrar()         │              │ -puntos     │
└──────────────┘              └────────────┘
```

**Relaciones identificadas y su justificación (aplicando el método del Tema 2.1):**
- `Parque` ◆— `Taquilla`: **Composición.** Si el Parque cierra definitivamente, las Taquillas de ese parque no tienen sentido — se crean en el constructor del Parque.
- `Parque` ◆— `Atraccion`: **Composición**, por la misma razón.
- `Parque` ◆— `Manilla`: **Composición.** El constructor del Parque carga automáticamente 1000 manillas (dato textual del ejercicio oficial).
- `Taquilla` —vende→ `Manilla`: **Asociación** (navegable desde Taquilla hacia Manilla) — la Taquilla toma manillas ya existentes de la lista del Parque (Agregación desde el punto de vista de la Taquilla, porque la manilla no se crea allí, se transfiere).

```csharp
public class Parque
{
    private string nombre;
    private bool abierto;
    private List<Manilla> lManillas = new List<Manilla>();
    private List<Taquilla> lTaquillas = new List<Taquilla>();
    private List<Atraccion> lAtracciones = new List<Atraccion>();

    public string Nombre
    {
        get { return nombre; }
        set
        {
            if (value != null && value.Length > 8)
                nombre = value.ToUpper();
            else
                throw new ArgumentException("El nombre debe tener más de 8 caracteres.");
        }
    }

    public Parque(string nombre)
    {
        this.Nombre = nombre;
        // el constructor carga automáticamente la lista con 1000 manillas
        for (int i = 0; i < 1000; i++)
            lManillas.Add(new Manilla());
    }

    public void Abrir() { abierto = true; }
    public void Cerrar() { abierto = false; }
    public bool EstaAbierto() { return abierto; }
}
```

*(Seguiremos ampliando `Taquilla`, `Atraccion` y `Registro` el Día 3, cuando agreguemos eventos.)*

---

## Soluciones — Ejercicios UML (Bloque 1 a 5)

*(Resumen razonado — revisa después de intentar cada bloque tú mismo.)*

**Bloque 1 (tipo de relación):**
1. Composición (una Facultad no existe fuera de su Universidad, en el contexto típico de este tipo de sistema). 2. Asociación (el Rector es una persona, existe independientemente). 3. Composición. 4. Asociación (el conductor puede cambiar de taxi — no es composición). 5. Dependencia (parámetro de método). 6. Herencia ("Es-Un"). 7. Asociación (o Agregación si el negocio exige que el jugador "pertenezca" al equipo como su dueño — depende del contexto, ver caso difícil). 8. Composición. 9. Agregación (la cuenta puede transferirse; sobrevive al vínculo específico con "ese" cliente, aunque no al banco). 10. Composición. 11. Asociación (o Agregación si el hospital "posee" el vínculo laboral, pero el médico sobrevive). 12. Asociación. 13. Herencia. 14. Dependencia. 15. Realización (implementa).
2. **Bloque 2 (multiplicidad):** 16. `Departamento (1) — Empleado (1..*)`. 17. `Orden (1) — Item (1..20)`. 18. `Paciente (1) — Cita (0..*)`. 19. `Carro (1) — Rueda (4)`. 20. `Curso (1) — Estudiante (25, 30)`. 21. `Aerolínea (1) — Avion (5..200)`. 22. `Empleado (1) — Supervisor (0..1)`. 23. `Torneo (1) — Equipo (8..16)`. 24. `Factura (1) — Cliente (1)`. 25. `Autor (0..*) — Libro (1..3)` (relación muchos a muchos).
3. **Bloque 3 (navegabilidad/rol/visibilidad):** 26. Flecha `>` de `Persona` hacia `Automovil`; el atributo `l_automoviles` aparece en `Persona`. 27. Por ejemplo `l_empleados` o `nomina`. 28. Navegable en ambas direcciones — se llama **asociación bidireccional**. 29. `#` (protegido). 30. Clase `Persona` con `-l_automoviles: Automovil[]` y accesor público. 31. Una **clase de asociación** conectada con línea punteada a la relación `Posee`. 32. Porque aunque la relación sea "pública" a nivel de diseño (se puede consultar), el atributo en sí debe seguir las mismas reglas de encapsulamiento del Día 1: se expone solo mediante accesores controlados. 33. `Manilla` es la vista; el atributo lista aparece en `Taquilla`. 34. El rol es el nombre del *extremo* de la relación (se convierte en atributo); el nombre de la relación es la etiqueta verbal general de la línea (ej. "Posee"), que puede o no coincidir con el rol. 35. Se asume bidireccional (ambas clases se conocen) salvo que se indique lo contrario.
4. **Bloque 4 (implementación):** 36-45. Siguen exactamente los patrones de código mostrados en la sección "5. Cómo se implementa en C#" del Tema 2.1: composición = creación interna en el constructor; agregación/asociación = recepción externa vía parámetro/método `Agregar`; dependencia = parámetro de método sin guardar referencia. Para el ejercicio 43: `TarjetaCredito` no debe ser atributo porque su uso es puntual (solo durante el pago), y guardarla permanentemente violaría el principio de mínima exposición de datos sensibles y generaría una dependencia innecesaria y permanente (acoplamiento alto sin necesidad real).
5. **Bloque 5 (casos difíciles):** 46-50. La clave en todos es aplicar la pregunta base del Tema 2.1 ("¿puede existir la parte sin el todo, en *este* sistema?") de forma explícita y justificada — no hay una única respuesta "correcta" universal, sino una respuesta defendible según el contexto de negocio descrito en cada enunciado. Para el ejercicio 50 (Parque): ver la justificación completa en "Proyecto Integrador — Etapa 2" más arriba.

---

## Soluciones — Mini Exámenes Día 2

**Tema 2.2**
1. Está mal porque `Rueda` NO "es un tipo de" `Automovil` — una rueda no tiene las características ni el comportamiento de un automóvil. El diseño correcto es **Composición**: `Automovil` ◆— `Rueda` (el automóvil está compuesto por ruedas).
2. (Se resuelve formalmente en Tema 2.3, pero la intuición correcta es): `virtual` permite que la subclase decida si sobrescribe o no; `abstract` obliga a la subclase a sobrescribir, y además la clase que lo contiene debe ser abstracta.
3.
```csharp
public abstract class Persona
{
    protected string cedula;
    protected string nombre;
    protected Persona(string cedula, string nombre)
    {
        this.cedula = cedula; this.nombre = nombre;
    }
}
public class Cliente : Persona
{
    private float descuento;
    public Cliente(string cedula, string nombre, float descuento) : base(cedula, nombre)
    {
        this.descuento = descuento;
    }
}
```

**Tema 2.3**
1. Tabla: `virtual+override` = sobrescritura polimórfica (se resuelve según el tipo **real** del objeto en tiempo de ejecución); `abstract` = igual, pero obligatorio; `new` = ocultamiento (se resuelve según el tipo **declarado** de la variable, no el real — NO es polimorfismo).
2. Imprime **"Hola desde B"**, porque `override` genera **enlace dinámico**: aunque la variable esté declarada como `A`, en tiempo de ejecución C# consulta el tipo real del objeto (`B`) y ejecuta su versión sobrescrita.
3. Con `new` en lugar de `override`, imprime **"Hola desde A"**, porque `new` no participa del mecanismo polimórfico — la llamada se resuelve según el **tipo declarado de la variable** (`A obj`), no según el tipo real del objeto. Esta es exactamente la diferencia entre polimorfismo real y ocultamiento.
4. Usarías una interfaz `IPrestable` cuando "poder prestarse" es un comportamiento que aplica a clases **sin relación de herencia natural entre sí** (por ejemplo, `Libro`, `DVD`, `Revista` no comparten necesariamente una superclase común significativa, pero todos deben poder "prestarse"). Si en cambio todos fueran, sin duda, subtipos de una única superclase con comportamiento común real, una clase abstracta también sería válida — pero la interfaz da más flexibilidad porque no consume la única herencia disponible en C#.

**Tema 2.4**
1. **Falso.** `as` retorna `null` si la conversión no es válida; quien lanza excepción ante un *cast* inválido es el operador de conversión explícita `(Tipo)variable`.
2. `Equals()`.

---

# Día 3 — Paradigma Funcional, Eventos, Aspectos e Introducción a Arquitectura de Software

## Tema 3.1 — Paradigma Funcional

### Explicación

**Intuición.** Hasta ahora programaste pensando en "objetos que cambian de estado" (POO). El paradigma funcional propone otra forma de pensar: en vez de un objeto que "hace" cosas y cambia su estado interno, pensás en **funciones puras** que reciben datos y devuelven nuevos datos, **sin modificar nada externo**. Es como una fábrica de conservas: entra fruta cruda, sale fruta enlatada — la fruta original nunca se toca ni se altera, se produce algo nuevo.

**Definición formal:** paradigma centrado en funciones. Raíces en el cálculo lambda (Alonzo Church, 1930). Lisp (1958) fue el primer lenguaje con noción funcional.

**Características:**
- No hay ciclos explícitos — se explota el poder de la **recursividad**.
- No hay variables ni asignaciones — se potencia la **inmutabilidad**.
- No hay estados — se evitan **efectos colaterales**.
- C# no es 100% funcional, pero tiene estructuras (lambdas, LINQ, delegados) para aprovechar sus beneficios.

**Ventajas:** foco en entradas/salidas; no hay que rastrear estado; código más reducido y mantenible; favorece la ejecución paralela (nada externo puede afectar el resultado de una función pura).

### Expresión condicional ternaria

`condicional ? consecuente : alternativa`
```csharp
bool sw = false;
string estado = !sw ? "Apagado" : "Encendido";
```
Se usa para simplificar `if` simples y dentro de expresiones lambda.

### Delegados

**🧩 Intuición complementaria:** un delegado es como un "control remoto" que apunta a un método. No ejecuta nada por sí mismo, pero cuando lo "aprietas" (lo invocás), ejecuta el método al que apunta — y podés cambiar a qué método apunta en tiempo de ejecución.

**Definición formal:** tipo que representa referencias a métodos con una lista de parámetros determinada y un tipo de retorno. Al crear una instancia de un delegado, se le puede asociar cualquier método con firma compatible.

```csharp
delegate bool VerificadorPrimo(int numero); // declaración

class Program
{
    static void Main()
    {
        VerificadorPrimo d = EsPrimo; // el delegado apunta a la función
        Console.WriteLine(d(17));
    }
    static bool EsPrimo(int num) { /* ... */ return true; }
}
```
- `[privacidad] delegate [TipoRetorno] [Nombre]([Parámetros])`.
- El modificador de acceso de un delegado **no puede** ser `private` ni `protected`.
- **Multicast:** un mismo delegado puede apuntar a varios métodos a la vez (se ejecutan todos en orden al invocarlo).
- **Delegado genérico:** usa una firma genérica, ej. `delegate T Nombre<T>(T parametro)`.

### Expresiones Lambda

Forma concisa de definir métodos anónimos. Puede contener expresiones o instrucciones.
- **Elementos:** tipo de retorno (a veces inferido), lista de parámetros, cuerpo.
- **Operador principal:** `=>` (misma precedencia que `=`).
- **Sintaxis de expresión:** `(parámetros) => expresión` (sin `return`).
- **Sintaxis de instrucción:** `(parámetros) => { instrucciones; return ...; }` (necesita delegado).
- Los paréntesis son opcionales con **un solo** parámetro; se ponen `()` vacíos si no recibe nada.

```csharp
(a) => a * a;                              // expresión
(a) => { return a * a; };                  // instrucción
(a, b) => { return Math.Pow(a, b); };       // instrucción con 2 parámetros
bool cumplePpto(long vtas) => vtas > 180000 ? true : false; // con ternario
```

### Predicados

`Predicate<T>` — expresión que evalúa si una condición se cumple, siempre devuelve `bool`.
```csharp
var numeros = new List<int> { 3, 7, 10, 15, 20, 22, 25 };
Predicate<int> esImpar = x => x % 2 != 0;
var impares = numeros.FindAll(esImpar);
```

### Funciones de orden superior; delegados `Action<>` y `Func<>`

**Función de orden superior:** acepta otra función como parámetro y/o devuelve una función. Es el pilar de la programación funcional. Las tres funciones clásicas son **Map, Filter, Fold** — .NET las entrega a través de **LINQ** (desde .NET 3.5).

- **`Action<T>`**: representa una lambda que **no** devuelve valor.
  ```csharp
  Action<bool> establecerEstado = e => Console.WriteLine("Estado: " + e);
  establecerEstado(true);
  ```
- **`Func<T, TResult>`**: representa una lambda que **sí** devuelve valor (hasta 16 parámetros en C#).
  ```csharp
  Func<double, double, double> sumar = (x, y) => x + y;
  Func<double, double, double> dividir = (x, y) =>
  {
      if (y == 0) throw new DivideByZeroException();
      return x / y;
  };
  ```

**⚠️ Pregunta trampa del material (clausura de variables):**
```csharp
int j = 6;
Func<int, int> function = i => { j = j * j; return 100 + i + j; };
int result = function(50); // ¿qué vale j luego de esta llamada? ¿y result?
```
La lambda "captura" la variable `j` por referencia (no por valor) — al ejecutarse, `j` se actualiza a `36`, y `result = 100 + 50 + 36 = 186`. Si volvieras a llamar `function(50)`, el resultado cambiaría porque `j` ya no vale 6.

### LINQ (Language Integrated Query)

Extensión de .NET con métodos que siguen el paradigma funcional, simplifica el trabajo con distintas fuentes de datos (`IEnumerable<T>`: arrays, listas; también SQL, XML).

| Categoría | Métodos |
|---|---|
| Cuantificar | `All`, `Any`, `Contains` |
| Filtrar | `Where`, `OfType` |
| Transformar | `Select`, `Zip` |
| Criterios (conjuntos) | `Distinct`, `Except`, `Intersect`, `Union` |
| Ordenamiento | `OrderBy`, `OrderByDescending`, `ThenBy`, `ThenByDescending`, `Reverse` |
| Agregación | `Aggregate`, `Average`, `Count`, `LongCount`, `Max`, `Min`, `Sum` |
| Partir/Unir | `Skip`, `SkipWhile`, `Take`, `TakeWhile`, `Join`, `GroupJoin` |
| Agrupamiento | `GroupBy`, `ToLookup` |

```csharp
List<Paciente> lPac = new List<Paciente>
{
    new Paciente("Juan", 39, 5000000),
    new Paciente("Pedro", 86, 800000),
    new Paciente("Ana", 18, 700000),
    new Paciente("Elena", 16, 4890000),
};

var mayoresDeEdad = lPac.Where(p => p.edad >= 18).OrderBy(p => p.name);
decimal saldoTotal = lPac.Sum(p => p.saldo);
```

### Tipos Anónimos

Clases simples creadas "al vuelo" para almacenar un conjunto de valores. Se crean con `new { ... }`. Usados comúnmente junto con LINQ.
```csharp
var libro = new { titulo = "Cien Años de Soledad", valor = 45000, editorial = "Oveja Negra" };
var libros = new[]
{
    new { Nombre = "Cien Años de Soledad", valor = 45000 },
    new { Nombre = "C# Libro de referencia", valor = 60000 },
};
```

### Mutabilidad e Inmutabilidad

- **Mutable (POO clásica):** los objetos pueden cambiar sus valores luego de creados. Más difíciles de mantener/depurar. Error frecuente: "variables que deberían tener un valor y tienen otro (o ninguno)".
- **Inmutable (Programación Funcional estricta):** los objetos NO cambian una vez creados. Para "modificar" algo, se crea una **copia nueva** con el nuevo valor (`const`, `readonly` son las herramientas básicas de C# para esto).

**Beneficios de la inmutabilidad:** más fácil de entender/mantener/probar; código más seguro; reduce mucho los problemas de sincronización en hilos/concurrencia; controla efectos colaterales.

**Desventaja:** sobrecosto de generar un objeto nuevo cada vez que "cambia" un atributo — puede degradar el rendimiento.

```csharp
// MUTABLE
public class Cuenta
{
    public ulong saldo { get; set; }
    public void Depositar(ulong valor) { saldo += valor; }
}
// la MISMA instancia cambia de valor -> mismo GetHashCode antes y después

// INMUTABLE (mismo ejemplo)
public class Cuenta
{
    public ulong saldo { get; }
    public Cuenta(ulong saldo) { this.saldo = saldo; }
    public Cuenta Depositar(ulong valor) => new Cuenta(saldo + valor); // retorna una NUEVA instancia
}
// cta = cta.Depositar(...) -> nueva instancia, distinto GetHashCode
```

**¿Cuándo usar inmutabilidad?**
- **Sí:** objetos simples y pequeños que se modifican esporádicamente; programación concurrente (hilos).
- **No:** objetos muy grandes donde retornar una instancia completa por un solo cambio no es significativo; objetos que se "van poblando" poco a poco (ejemplo del material: llenado de sillas de un avión); cualquier objeto que se modifica con alta frecuencia.

### Errores comunes

- Confundir sobrecarga de operador ternario anidado con `if-else` legible — el ternario se vuelve ilegible si se anida más de 1 nivel.
- Olvidar que las lambdas capturan variables externas **por referencia**, no por valor (ver pregunta trampa arriba) — causa de bugs difíciles de rastrear.
- Usar `Action<>` cuando el método sí retorna algo (o viceversa) — no compilan si no coinciden.
- Pensar que `readonly`/`const` por sí solos hacen "inmutable" toda la clase — si la clase tiene **otros** atributos mutables, sigue siendo mutable en conjunto.
- Aplicar inmutabilidad a objetos grandes que cambian con alta frecuencia — degrada el rendimiento sin necesidad real.

### Relaciones con lo anterior/siguiente

Los delegados son **prerrequisito directo** de Programación Orientada a Eventos (Tema 3.2) — un evento en C# literalmente se implementa con un delegado. Las lambdas y LINQ se usarán constantemente al escribir servicios (Día 4) y en el proyecto integrador para procesar listas (manillas, registros, ventas).

### Resumen — Conceptos clave

- Delegado = referencia a un método con firma compatible; puede ser multicast.
- Lambda: `=>`; forma de expresión (sin `return`) o de instrucción (con `{ }` y `return`).
- `Action<>` = no retorna valor; `Func<>` = sí retorna valor.
- LINQ: filtrar, transformar, ordenar, agrupar colecciones de forma funcional.
- Tipos anónimos: `new { ... }`, se usan con LINQ.
- Inmutabilidad: no se cambia el objeto, se crea uno nuevo; ventajas en concurrencia y mantenibilidad; desventaja en rendimiento con objetos grandes/frecuentes.

### Ejercicios

1. Escribe una expresión lambda que reciba un `int` y retorne si es par (`Func<int, bool>`).
2. Reescribe con LINQ: dada una `List<Automovil>`, obtener solo los del año actual, ordenados por marca.
3. Explica por qué en el ejemplo de la clausura (`j = j * j`) el resultado cambiaría si llamás `function(50)` dos veces seguidas.
4. Diseña la clase `Manilla` (del proyecto integrador) como **inmutable**: cada vez que se carga saldo, se retorna una nueva instancia.
5. ¿Por qué NO conviene hacer inmutable la clase `Registro` que va acumulando movimientos de las atracciones del parque (llenado progresivo)? Relaciónalo con la regla "cuándo no usar inmutabilidad".

### Mini Examen — Tema 3.1

1. Diferencia `Action<T>` de `Func<T, TResult>` con un ejemplo propio de cada uno.
2. V o F: "Los delegados solo pueden apuntar a un método a la vez." Justifica.
3. Explica, en tus palabras, por qué la inmutabilidad ayuda en programación concurrente (hilos).

---

## Tema 3.2 — Programación Orientada a Eventos (POE)

### Explicación

**Intuición.** Pensá en una alarma de incendios. El sensor de humo no le pregunta constantemente a cada persona del edificio "¿hay humo?" — simplemente **dispara un evento** ("¡Alarma!") y **cualquiera que esté suscrito** a esa alarma (empleados, bomberos, sistema de rociadores) reacciona. El sensor ni siquiera sabe quién está escuchando.

**Definición formal:** los eventos permiten que una clase u objeto **notifique** a otras clases u objetos cuando ocurre algo de interés, sin necesidad de conocerlas directamente.

### Roles de la POE

| Rol | Definición del material |
|---|---|
| **Publisher (Publicador)** | La clase que envía (genera) el evento. |
| **Subscriber (Suscriptor)** | La(s) clase(s) que reciben el evento. |
| **Evento** | La notificación en sí. |
| **Delegado** | Objeto que contiene la referencia (apunta) a un método — es el mecanismo técnico detrás del evento. |

En el diagrama del material, un delegado (ej. `delegado_nota`) puede apuntar a **dos métodos** a la vez (multicast); `EventHandler` es el método que maneja el evento y se **suscribe** a él.

### Implementación en C# (esqueleto general)

```csharp
public class Taquilla // Publisher
{
    public delegate void SinBoletasHandler(string mensaje);
    public event SinBoletasHandler SinBoletas; // el evento

    private int boletasDisponibles;

    public void Vender()
    {
        boletasDisponibles--;
        if (boletasDisponibles == 0)
            SinBoletas?.Invoke("¡La taquilla se quedó sin boletas!"); // dispara el evento
    }
}

public class Monitor // Subscriber
{
    public void AlertaSinBoletas(string mensaje) => Console.WriteLine(mensaje);
}

// Main
Taquilla t = new Taquilla();
Monitor m = new Monitor();
t.SinBoletas += m.AlertaSinBoletas; // el subscriber SE SUSCRIBE al evento del publisher
t.Vender();
```

### Ejemplo del material aplicado al proyecto

> "Se necesita un evento que dispare un mensaje cuando la taquilla no tenga más boletas. El evento deberá ir asociado al método [de venta]."

Esto es exactamente el patrón mostrado arriba: la `Taquilla` (publisher) dispara el evento `SinBoletas` cuando su saldo de manillas disponibles llega a cero.

### Errores comunes

- Invocar el evento sin verificar que tenga al menos un suscriptor (`SinBoletas?.Invoke(...)` con el operador `?.` es la forma segura; invocarlo directamente sin suscriptores lanza `NullReferenceException`).
- Confundir el **delegado** (el tipo/mecanismo) con el **evento** (la instancia pública basada en ese delegado) — un evento es, en esencia, un delegado con reglas de encapsulamiento adicionales (solo la clase que lo declara puede invocarlo con `Invoke`; las demás solo pueden suscribirse con `+=`).
- Olvidar desuscribirse (`-=`) cuando el suscriptor ya no debe escuchar, generando referencias colgantes.

### Relaciones con lo anterior

La POE es una aplicación directa de **Delegados** (Tema 3.1) — sin entender delegados, un evento en C# es pura magia sin sentido. También se relaciona con el principio **DIP** de SOLID (Día 4): los eventos son una forma natural de **desacoplar** al publicador de sus suscriptores (el publicador no necesita conocer clases concretas de sus suscriptores).

### Resumen — Conceptos clave

- Publisher genera el evento; Subscriber se suscribe (`+=`) y reacciona.
- Un evento se implementa internamente con un **delegado**.
- `evento?.Invoke(...)` es la forma segura de disparar un evento.

### Ejercicios

1. Diseña el evento `SinBoletas` para la clase `Taquilla` del proyecto integrador (código completo: delegado, evento, método que lo dispara, y un suscriptor `Registro` que lo escuche).
2. ¿Qué pasaría si invocaras el evento sin el operador `?.` y no hubiera ningún suscriptor?
3. V o F: "Un evento puede tener más de un suscriptor a la vez." Justifica con el concepto de multicast del Tema 3.1.

### Mini Examen — Tema 3.2

1. Explica con tus palabras la relación entre Delegado y Evento.
2. ¿Por qué la POE reduce el acoplamiento entre el Publisher y sus Subscribers, comparado con que el Publisher llamara directamente a un método específico de cada suscriptor?

---

## Tema 3.3 — Programación Orientada a Aspectos (AOP)

### Explicación

**Intuición.** Imagina que en tu sistema, **todas** tus clases (`Usuario`, `Producto`, `Pedido`...) necesitan: (1) verificar autenticación, (2) registrar auditoría, (3) loguear operaciones de base de datos. Si escribís ese código dentro de cada clase, lo estás **repitiendo** en decenas de lugares — y si cambia la política de auditoría, tenés que tocar todas las clases. Eso es una **inquietud transversal** (*cross-cutting concern*): algo que se repite en varias partes del programa **independientemente de si esas partes tienen relación directa entre sí**.

### Por qué OO no basta (razonamiento del material)

Con herencia/interfaces, las alternativas son:
1. Agregar la funcionalidad directamente a la clase → le da una responsabilidad extra (viola SRP, Día 4).
2. Crear una clase nueva que la ejecute y llamarla desde la antigua → surge el problema: ¿qué pasa si querés usar la clase **sin** esa funcionalidad?
3. Heredar una clase nueva agregando la funcionalidad → con 3 inquietudes (auditoría, validación, autenticación) terminás con una explosión combinatoria de clases (algunas con las 3, algunas con 2, algunas con 1).

Consecuencias: viola **DRY** ("No te repitas" — replicar el mismo código en varias partes), cualquier cambio de requisitos provoca un cambio masivo, mantenimiento difícil y costoso.

### La solución: AOP

Si una funcionalidad se repite en distintos módulos, se **extrae** del programa principal, se convierte en un **aspecto**, y se especifica mediante reglas en qué partes del código se debe "tejer" (aplicar). AOP **complementa** a la POO — no la reemplaza; le da la modularidad que la POO no le puede dar a las inquietudes transversales.

### Conceptos de AOP

| Concepto | Definición del material |
|---|---|
| **Punto de unión** (*join point*) | Puntos específicos en la ejecución del programa (invocación de un método, manejo de una excepción). |
| **Aspecto** | La funcionalidad transversal que se implementa de forma separada — el concepto principal. |
| **Consejo** (*advice*) | El código que ejecutará el aspecto (el cuerpo del algoritmo). |
| **Punto de corte** (*pointcut*) | Especifica, mediante expresiones (regex), en qué parte del programa se inserta un aspecto — identifica los puntos de unión relevantes. |

### Implementación con proxys dinámicos

Un **proxy dinámico** es una clase generada en tiempo de ejecución que envuelve un objeto real y le aplica aspectos, interceptando las llamadas a métodos para agregar comportamiento adicional (útil para seguridad, caché, auditoría).

**Pasos:** (1) definir una interfaz con los métodos a interceptar, (2) usar una librería generadora de proxy para crear un objeto que implemente esa interfaz, (3) adjuntar interceptores al proxy.

.NET ofrece `Castle.DynamicProxy`, `RealProxy`, `DispatchProxy`. `DispatchProxy` es una clase base que permite crear proxys dinámicos para interfaces, interceptando llamadas mediante la sobrescritura del método `Invoke`.

### Ventajas de AOP (del material)

- Ahorra tiempo/esfuerzo y facilita mantenimiento (los aspectos se escriben independientemente del código al que se envuelven).
- Facilita agregar nuevas funcionalidades sin modificar el código existente (a menudo basta con escribir un nuevo aspecto).
- Los aspectos se pueden envolver/desenvolver dinámicamente en tiempo de ejecución sin cambiar el código del programa — mejora la flexibilidad.
- Mejora la calidad facilitando la adición de código de pruebas y depuración.

### Errores comunes

- Meter demasiada lógica de negocio dentro de un aspecto (los aspectos son para inquietudes **transversales** — auditoría, logging, seguridad — no para lógica de dominio).
- Confundir un aspecto con un patrón Decorator clásico — se parecen conceptualmente (ambos "envuelven" comportamiento), pero AOP se aplica declarativamente vía *pointcuts* a **muchos** puntos del programa a la vez, mientras que Decorator envuelve manualmente **una** instancia específica.

### Relaciones con lo anterior

AOP requiere entender **Interfaces** (Tema 2.3) — los proxys dinámicos se construyen sobre interfaces — y es la solución arquitectónica a un problema que, sin AOP, terminaría rompiendo el principio **SRP** de SOLID (Día 4) en cada clase que necesite auditoría, autenticación, etc.

### Resumen — Conceptos clave

- Inquietud transversal = se repite en módulos sin relación directa entre sí.
- Aspecto = la funcionalidad extraída; Advice = el código que ejecuta; Pointcut = dónde se aplica; Join point = el punto específico de ejecución.
- Se implementa con proxys dinámicos (`DispatchProxy`, `Castle.DynamicProxy`) que interceptan llamadas a métodos.

### Ejercicios

1. Da un ejemplo propio (no autenticación/auditoría) de una inquietud transversal en un sistema real.
2. Explica por qué agregar logging directamente en cada método de negocio viola SRP.
3. ¿Qué diferencia hay entre un *pointcut* y un *join point*?

### Mini Examen — Tema 3.3

1. Explica, con tus palabras, por qué "envolver dinámicamente en tiempo de ejecución" es una ventaja frente a modificar el código fuente de cada clase.
2. V o F: "AOP reemplaza completamente a la POO." Justifica con la definición exacta del material.

---

## Tema 3.4 — Introducción a la Arquitectura de Software

### Explicación

**Intuición.** El material abre con una pregunta provocadora: *¿es el Coliseo Romano una arquitectura?* No — el Coliseo es el **resultado** de una arquitectura; es una instancia, una implementación de ella. Si los arquitectos romanos no hubieran creado primero una representación descriptiva (planos, principios estructurales), no habrían podido construirlo. **La arquitectura es el conjunto de representaciones necesarias para crear un objeto** — no el objeto en sí.

Trasladado al software: el código que corre en producción es "el Coliseo" (el resultado). La arquitectura es el conjunto de decisiones, estructuras y principios que hicieron posible construir ese código de forma coherente.

**¿Se puede construir software sin arquitectura?** Sí, de forma simple y empírica, pero solo te da prestaciones básicas — sin capacidad de crecer o adaptarse bien.

### Definiciones formales (tres perspectivas del material)

1. *"La estructura del sistema, que comprende elementos software, las propiedades de esos elementos visibles externamente y las relaciones entre ellos. Conforma el esqueleto de cualquier sistema software, y es la principal responsable de los atributos de calidad del sistema."*
2. *"La especificación técnica que explica cómo debiera organizarse y funcionar una infraestructura tecnológica a través de patrones y métodos para procesar y producir información coherente al medio organizacional al cual se aplica."*
3. **IEEE 1471-2000:** *"Es la organización fundamental de un sistema, que se detalla en: componentes, la relación que existe entre ellos y el ambiente, también los principios que guían el diseño y evolución del mismo."*

**Definición sintetizada:** un plano detallado del sistema a nivel de sus componentes, para guiar su implementación — define estructura, interrelaciones y los principios que rigen diseño y evolución.

### Con y sin Arquitectura de Software

| **Con arquitectura** | **Sin arquitectura** |
|---|---|
| Eficiencia en el desarrollo (estructura clara, reutilización) | Caos en el desarrollo (código desorganizado, difícil de mantener) |
| Calidad del producto (estándares y patrones) | Problemas de calidad (inconsistencias, errores frecuentes) |
| Mantenimiento y escalabilidad facilitados | Dificultad de mantenimiento (alto riesgo de efectos secundarios) |
| Integración de sistemas más fácil | Complejidad en la integración (sistemas aislados) |
| Gestión de riesgos desde etapas tempranas | Riesgos elevados (fallos críticos, problemas de rendimiento sin detección temprana) |

Requiere una inversión inicial, que se justifica por los beneficios a largo plazo.

### Implicaciones de la Arquitectura de Software

1. **Diseño de Alto Nivel** — estructura y organización general (como el plano de una casa).
2. **Abstracciones y Patrones** — usa conceptos y estructuras predefinidas para organizar el desarrollo.
3. **Marco de Producción** — directrices para construcción, asegurando coherencia y eficiencia.
4. **Objetivos de Desarrollo** — guía el proceso hacia el cumplimiento de metas y requisitos.

### El Rol: Arquitecto de Software

Profesional responsable de diseñar la estructura general de un sistema de software; define estructura, componentes e interacciones, asegurando que se cumplan los requisitos técnicos y funcionales.

### Procesos de Diseño Arquitectónico

- **Big Design Up Front:** se diseña la arquitectura completa **antes** de comenzar el desarrollo.
- **Adaptativa:** basada en principios ágiles; propone diseñar la arquitectura **por sprints**, de forma incremental.

**Proceso típico (ejemplo del material, U. de los Andes):**
`Definición de Requerimientos → Diseño Arquitectónico → Desarrollo e Implementación → Pruebas y Validación`

### Vistas Arquitectónicas y el Modelo 4+1 (Philippe Kruchten)

Una **vista** es la aplicación de un punto de vista (perspectiva) sobre una arquitectura específica. Marco de referencia para describir arquitecturas de sistemas software intensivos, basado en múltiples vistas concurrentes:

| Vista | Qué describe |
|---|---|
| **Vista Lógica** | Estructura y funcionalidad del sistema: componentes y sus interacciones. |
| **Vista de Desarrollo** | Organización del código fuente, dependencias, cómo se gestionan y desarrollan los componentes. |
| **Vista de Proceso** | Comportamiento dinámico: concurrencia y comunicación entre procesos en ejecución. |
| **Vista Física** | Infraestructura tecnológica: cómo se despliegan los componentes en el hardware y sus conexiones físicas. |
| **Escenarios** (el "+1") | Casos de uso concretos que validan el diseño e ilustran cómo las demás vistas trabajan juntas para cumplir los requisitos. Dirigidos a todos los interesados (usuarios, desarrolladores, gerentes). Se representan con descripciones narrativas o diagramas UML. |

### Estilos / Patrones Arquitectónicos Relevantes

| Estilo | Descripción (del material) |
|---|---|
| **Diseño por Capas** | Organiza el software en capas (presentación, lógica de negocio, acceso a datos), cada una con responsabilidad específica, interactuando solo con la capa adyacente. Promueve modularidad y mantenimiento. |
| **Monolítica** | Funcionalidades acopladas en un solo bloque de código; todos los componentes comparten memoria y recursos. Facilita el desarrollo inicial pero complica mantenimiento/escalabilidad al crecer. |
| **MVC (Modelo-Vista-Controlador)** | Separa el código por responsabilidades: Modelo (datos/lógica de negocio), Vista (interfaz, presenta info del modelo), Controlador (coordina interacción modelo-vista, responde a eventos del usuario). Facilita desarrollo colaborativo y reutilización; muy usado en apps web. |
| **Cliente-Servidor** | Cliente solicita servicios, servidor los provee. Común en web (navegador=cliente, servidor aloja lógica y BD). Puede ser multinivel (2N, 3N...). |
| **SOA (Arquitectura Orientada a Servicios)** | Provee servicios débilmente acoplados, independientes, que interactúan por interfaces bien definidas — promueve interoperabilidad y flexibilidad (se profundiza en el Tema 4.2). |
| **Microservicios** | Estructura la app como un conjunto de servicios pequeños e independientes, cada uno para una función empresarial específica (DDD), que se comunican vía APIs. Cada uno se implementa/escala/mantiene independientemente; limita el impacto de fallas individuales. |
| **DDD (Domain Driven Design)** | Metodología centrada en comprender profundamente el dominio del negocio y estructurar el código para enfrentar su complejidad. Elementos: lenguaje ubicuo, dominios enriquecidos, Entidad, Agregado, Objetos de Valor; también Diseño por Contrato, Lógica de Coordinación, Persistencia, Corrupción del Dominio, Diseño de Caché, Gestión de Transacciones. |
| **Clean Architecture** | Prioriza organización/estructura; independiente de frameworks y tecnologías externas; busca mantenibilidad, flexibilidad, testeabilidad. **Regla de Dependencia:** las dependencias de código fuente (imports, herencia, uso de clases) solo pueden dirigirse desde las capas externas (detalles técnicos) hacia las internas (lógica de negocio). Las capas internas no deben saber nada de las externas. |
| **Arquitectura Hexagonal** (Puertos y Adaptadores) | Divide el sistema en componentes intercambiables y débilmente acoplados, conectados mediante "puertos" expuestos. Separa la lógica del negocio del código de infraestructura, permitiendo cambiar tecnologías externas sin afectar el núcleo. **Puertos** = interfaces que definen la comunicación con el exterior. **Adaptadores** = clases que traducen las solicitudes externas al formato del núcleo y viceversa. |
| **Arquitectura Basada en Eventos** | Componentes se comunican vía eventos (publicadores/suscriptores — igual que POE del Tema 3.2, pero a escala de sistema). Reacción en tiempo real; diseño desacoplado (componentes evolucionan independientemente). |
| **Arquitectura Agéntica** | Sistema donde un agente (semi)autónomo persigue un objetivo planificando acciones, invocando herramientas, observando resultados, actualizando conocimiento y repitiendo el ciclo hasta cumplir el objetivo o restricciones de seguridad. El agente descompone la tarea, llama a los servicios adecuados, y verifica su propio trabajo. |

### Tipos de Dependencias

- **Dependencias Verticales:** relaciones entre **diferentes tipos** de componentes (ej: un servicio que depende de una aplicación específica).
- **Dependencias Horizontales:** relaciones entre componentes **similares** (ej: aplicaciones que interactúan entre sí).

### Errores comunes

- Confundir **arquitectura** con **el diagrama** que la representa — el diagrama es solo una *vista*; la arquitectura es el conjunto completo de decisiones estructurales.
- Pensar que "arquitectura monolítica" es sinónimo de "mal diseño" — no lo es necesariamente; facilita el desarrollo inicial y es válida para sistemas pequeños/medianos; el problema surge cuando **crece** sin control.
- Confundir Agregación/Composición (Día 2, a nivel de clases) con Microservicios/SOA (a nivel de sistemas completos) — son ideas análogas mentalmente (acoplamiento, independencia) pero operan en escalas completamente distintas.
- Creer que Clean Architecture y Arquitectura Hexagonal son lo mismo — comparten filosofía (independencia de infraestructura, regla de dependencia hacia el núcleo) pero Hexagonal enfatiza **puertos y adaptadores** explícitos como mecanismo, mientras Clean Architecture enfatiza **capas concéntricas** con una regla de dependencia unidireccional.

### Relaciones con lo que sigue

Este tema es el **puente** hacia SOLID (Día 4): SOLID son los principios que aplicás **dentro** de cada clase/componente para que la arquitectura elegida (por ejemplo, Clean Architecture o Hexagonal) realmente funcione. Sin SOLID, aunque dibujes una arquitectura hexagonal perfecta, el código interno la va a violar constantemente. También es la base de SOA (Tema 4.2) y de MVC (Tema 4.3), que son aplicaciones concretas de estos estilos.

### Resumen — Conceptos clave

- Arquitectura ≠ el sistema construido; es el conjunto de representaciones/decisiones que lo hicieron posible.
- IEEE 1471-2000: organización fundamental — componentes, relaciones, principios de diseño y evolución.
- Modelo 4+1: Lógica, Desarrollo, Proceso, Física + Escenarios.
- Big Design Up Front vs. Adaptativa (por sprints).
- Estilos clave para el examen: Capas, Monolito, MVC, Cliente-Servidor, SOA, Microservicios, DDD, Clean Architecture, Hexagonal, Basada en Eventos, Agéntica.

### Ejercicios

1. Explica con tus palabras por qué "el Coliseo Romano" es una buena analogía para diferenciar arquitectura de implementación.
2. Compara Arquitectura Monolítica vs. Microservicios en una tabla propia (mínimo 4 criterios).
3. ¿Qué vista del modelo 4+1 usarías para explicarle a un gerente no técnico cómo funciona el sistema? ¿Por qué?
4. Relaciona la Arquitectura Hexagonal con el principio DIP de SOLID que verás mañana (adelanta tu intuición: ¿por qué "puertos y adaptadores" suena a "invertir dependencias"?).
5. Clasifica el Sistema del Parque de Diversiones (proyecto integrador): si tuvieras que elegir un estilo arquitectónico para implementarlo como sistema completo (no solo como clases), ¿elegirías Monolito o Microservicios? Justifica con al menos 2 razones.

### Mini Examen — Tema 3.4

1. Define arquitectura de software usando la definición IEEE 1471-2000, con tus propias palabras.
2. V o F: "Big Design Up Front es incompatible con metodologías ágiles." Justifica.
3. Nombra las 5 vistas del modelo 4+1 y qué responde cada una en una frase.
4. ¿Cuál es la diferencia principal entre Agregación/Dependencia Horizontal y Vertical en el contexto de arquitectura de sistemas (no de clases)?

---

## Proyecto Integrador — Etapa 3 (Día 3)

Con paradigma funcional y eventos, completamos el sistema del Parque:

```csharp
public class Taquilla
{
    public delegate void SinManillasHandler(string mensaje);
    public event SinManillasHandler SinManillas; // evento: se dispara cuando se agotan

    private int idTaquilla;
    private decimal saldoDinero;
    private bool abierta;
    private List<Manilla> disponibles = new List<Manilla>();
    private List<Manilla> vendidas = new List<Manilla>();

    public Taquilla(int id, List<Manilla> manillasDelParque)
    {
        idTaquilla = id;
        saldoDinero = 0;
        // en el constructor toma 100 manillas del parque y las quita de allí
        disponibles = manillasDelParque.Take(100).ToList();      // <- uso de LINQ
        manillasDelParque.RemoveAll(m => disponibles.Contains(m));
    }

    public Manilla VenderManilla(decimal carga)
    {
        if (!abierta) throw new InvalidOperationException("La taquilla está cerrada.");
        Manilla m = disponibles.First(); // <- LINQ
        m.CargarSaldo(carga);
        disponibles.Remove(m);
        vendidas.Add(m);
        saldoDinero += carga;

        if (disponibles.Count == 0)
            SinManillas?.Invoke($"Taquilla {idTaquilla} sin manillas disponibles.");

        return m;
    }
}
```

Nótese el uso de **LINQ** (`Take`, `First`, `RemoveAll`) para manipular las listas de forma funcional, y el **evento** `SinManillas` que notifica cuando se agotan — exactamente lo pedido en el material original.

---

## Soluciones — Mini Exámenes Día 3

**Tema 3.1**
1. `Action<T>` no retorna valor (ej: `Action<string> imprimir = s => Console.WriteLine(s);`); `Func<T,TResult>` sí retorna (ej: `Func<int,int> cuadrado = n => n*n;`).
2. **Falso.** Un delegado puede ser *multicast*: apuntar a varios métodos a la vez, y al invocarlo se ejecutan todos en orden.
3. Porque al ser inmutables, distintos hilos pueden leer el mismo objeto simultáneamente sin riesgo de que uno lo modifique mientras otro lo lee — se elimina la necesidad de bloqueos (locks) para lectura, reduciendo drásticamente los problemas de sincronización.

**Tema 3.2**
1. El Evento es, técnicamente, un Delegado con reglas de encapsulamiento adicionales: solo la clase que lo declara puede "dispararlo" (`Invoke`), mientras que las clases externas solo pueden suscribirse (`+=`) o desuscribirse (`-=`), nunca dispararlo directamente.
2. Porque el Publisher nunca necesita conocer ni referenciar directamente las clases concretas de sus Subscribers — solo dispara el evento y quien esté suscrito reacciona. Si tuviera que llamar directamente a un método de cada suscriptor, el Publisher tendría que conocer sus tipos concretos, generando alto acoplamiento.

**Tema 3.3**
1. Porque el aspecto se activa/desactiva sin tocar ni recompilar el código de las clases de negocio — reduce el riesgo de introducir errores al modificar código que ya funciona, y permite activar/desactivar comportamientos (como auditoría) según el entorno (desarrollo vs. producción) sin cambios de código.
2. **Falso.** El material dice explícitamente que "este enfoque... es algo que a la POO no le es posible, pero la **complementa**" — AOP no reemplaza a la POO, resuelve un problema que la POO por sí sola no resuelve bien (las inquietudes transversales).

**Tema 3.4**
1. Es la organización fundamental de un sistema, detallada en sus componentes, la relación entre ellos y con el ambiente, y los principios que guían su diseño y evolución.
2. **Falso.** Big Design Up Front es más propio de metodologías predictivas (en cascada); la alternativa **Adaptativa** es la que se basa explícitamente en principios ágiles (diseño por sprints, incremental) — son dos procesos distintos, pero ninguno es intrínsecamente "incompatible" en términos absolutos; simplemente la adaptativa es la que encaja naturalmente con agilidad.
3. Lógica (componentes e interacciones), Desarrollo (organización del código/dependencias), Proceso (concurrencia/comunicación en ejecución), Física (despliegue en hardware), Escenarios (casos de uso que validan e integran las 4 vistas).
4. Vertical = entre componentes de **distinto tipo** (ej: servicio que depende de una app específica). Horizontal = entre componentes **similares** (ej: dos aplicaciones que interactúan entre sí).

---

# Día 4 — Principios SOLID, Programación Orientada a Servicios y ASP.NET Core MVC

> Este es el núcleo evaluable de "Arquitectura de Software". Todo lo de los días 1-3 fue para poder entender esto de verdad, no de memoria.

## Tema 4.1 — Principios SOLID

### Objetivo general (Robert C. Martin, "Uncle Bob")

*"Promover buenas prácticas de diseño de software, en particular en programación orientada a objetos, abordando problemas comunes que enfrentan los desarrolladores a medida que los sistemas crecen en tamaño y complejidad."*

SOLID son **5 principios**: **S**RP, **O**CP, **L**SP, **I**SP, **D**IP.

---

### 🅢 SRP — Principio de Responsabilidad Única (Single Responsibility Principle)

#### 1. El problema (código mal diseñado)

```csharp
public class Empleado
{
    public string Nombre { get; set; }
    public DateTime FechaIngreso { get; set; }

    // 1ª preocupación: gestionar los datos del empleado
    public void ActualizarDatos(string nombre) { Nombre = nombre; }

    // 2ª preocupación: ¿qué pasa si cambia el formato de tiempo?
    public string ObtenerAntiguedadFormateada()
    {
        TimeSpan t = DateTime.Now - FechaIngreso;
        return $"{t.Days} días, {t.Hours} horas"; // lógica de FORMATO mezclada con lógica de EMPLEADO
    }
}
```
**¿Por qué es malo?** Si mañana cambia el formato de fecha (por internacionalización, por ejemplo), tenés que modificar la clase `Empleado`, que **no tiene nada que ver conceptualmente** con formatos de tiempo. Con el tiempo, esta clase acumula cada vez más "razones para cambiar" que nada tienen que ver entre sí, y se vuelve frágil: tocar una cosa rompe otra sin relación aparente.

#### 2. Intuición

Pensá en un cocinero de restaurante que además tuviera que atender el teléfono, cobrar en caja y limpiar los baños. Cualquier cambio en el protocolo de "cómo cobrar" lo distrae de cocinar, y viceversa. Un buen restaurante separa: un cocinero cocina, un cajero cobra, un mesero atiende. Cada rol tiene **una sola razón para cambiar**.

#### 3. Definición formal

**"Una clase debe tener solo una razón (o preocupación) para cambiar."** Debe tener una tarea simple y bien definida. Se logra dividiendo las clases en unidades más pequeñas y específicas. Beneficios: reduce complejidad, mejora legibilidad/comprensión, facilita mantener y ampliar el código.

#### 4-6. Refactorización paso a paso

**Paso 1 — identificar las preocupaciones mezcladas:** en `Empleado`, hay dos: (a) gestión de datos del empleado, (b) formato de tiempo.
**Paso 2 — extraer cada preocupación a su propia clase:**
```csharp
public class Empleado
{
    public string Nombre { get; set; }
    public DateTime FechaIngreso { get; set; }
}

public class FormateadorTiempo // única responsabilidad: formatear tiempo
{
    public string Formatear(DateTime desde)
    {
        TimeSpan t = DateTime.Now - desde;
        return $"{t.Days} días, {t.Hours} horas";
    }
}
```
**Paso 3 — resultado final:** ahora `Empleado` solo cambia si cambian las reglas de negocio sobre empleados; `FormateadorTiempo` solo cambia si cambia la política de formato — y puede reutilizarse en cualquier otra clase del sistema, no solo en `Empleado`.

#### 7. UML antes/después

```
ANTES:                          DESPUÉS:
┌───────────┐                  ┌───────────┐      ┌───────────────────┐
│  Empleado  │                  │  Empleado  │      │ FormateadorTiempo  │
├───────────┤                  ├───────────┤      ├───────────────────┤
│ +ActualizarDatos()│           │ +ActualizarDatos()│  │ +Formatear()       │
│ +ObtenerAntiguedad│           └───────────┘      └───────────────────┘
│  Formateada()      │                 └─────usa (Dependencia)──────┘
└───────────┘
```

#### 8. Casos reales

| Dominio | Ejemplo de violación SRP | Refactor |
|---|---|---|
| Tienda virtual | `Producto` que además calcula impuestos y envía emails de confirmación | Separar `CalculadoraImpuestos`, `ServicioNotificacion` |
| Banco | `CuentaBancaria` que además genera el PDF del extracto | Separar `GeneradorExtractoPDF` |
| Biblioteca | `Libro` que además controla el préstamo y las multas por retraso | Separar `ServicioPrestamos`, `CalculadoraMultas` |
| Videojuego | `Jugador` que además dibuja en pantalla (renderizado) | Separar `RenderizadorJugador` |
| Hospital | `Paciente` que además calcula facturación del seguro | Separar `ServicioFacturacion` |

#### 9. Violaciones comunes que parecen cumplir SRP (pero no)

- Una clase con **un solo método público**, pero ese método internamente hace 4 cosas distintas (valida, calcula, persiste, notifica) — **sigue violando SRP** aunque "parezca" pequeña.
- Nombrar una clase `EmpleadoManager` o `EmpleadoService` y pensar que por tener "una responsabilidad de negocio" ya cumple SRP — hay que revisar sus métodos: si mezcla persistencia + lógica de negocio + validación, sigue teniendo múltiples razones para cambiar.

#### 10. Cómo reconocerlo rápido

- ¿Esta clase tiene **más de una razón** para cambiar?
- ¿Puedo describir la responsabilidad de esta clase **sin usar la palabra "y"**?
- ¿Si cambia el requisito de negocio A, tengo que tocar código que también sirve al requisito B, sin relación?

#### 13. Comparaciones

**SRP vs. OCP:** SRP se preocupa por **cuántas razones** tiene una clase para cambiar; OCP se preocupa por **cómo** se le agrega comportamiento nuevo sin modificar lo existente. Suelen ir de la mano: una clase con una sola responsabilidad es mucho más fácil de extender sin modificarla.

#### 14. Relación con Patrones de Diseño

- **Strategy:** si una clase tenía "muchas formas de calcular algo" mezcladas en un `switch`, extraerlas a estrategias separadas es, a la vez, aplicar SRP (cada estrategia tiene una sola razón para cambiar) y preparar el terreno para OCP.
- **Facade:** agrupa varias responsabilidades **detrás de una interfaz simple**, pero cada clase interna sigue teniendo su propia responsabilidad única — Facade no viola SRP, lo protege de la complejidad externa.

#### 15. Checklist SRP

- [ ] ¿Puedo nombrar la responsabilidad de la clase con una sola frase, sin "y"?
- [ ] ¿Cada método de la clase contribuye a esa única responsabilidad?
- [ ] ¿Si cambia una regla de negocio no relacionada, tengo que tocar esta clase?
- [ ] ¿Esta clase mezcla lógica de negocio con acceso a datos, formato o notificaciones?

#### 11. Ejercicios SRP *(no mires la solución hasta intentarlo — está en "Soluciones SOLID" al final del Tema 4.1)*

1. Detecta el problema: ¿qué principio viola y por qué?
```csharp
public class Factura
{
    public List<string> Items = new List<string>();
    public void AgregarItem(string item) => Items.Add(item);
    public void GuardarEnBaseDeDatos() { /* código SQL directo aquí */ }
    public void EnviarPorEmail() { /* código de SMTP aquí */ }
}
```
2. Propón una solución (refactoriza) para el ejercicio anterior.
3. ¿La clase `ValidadorAutomovil` del caso de estudio del material (que "solo hace las validaciones") cumple o viola SRP? Justifica.

---

### 🅞 OCP — Principio Abierto/Cerrado (Open/Closed Principle)

#### 1. El problema

```csharp
public class CalculadoraEnvio
{
    public decimal Calcular(string tipoEnvio, decimal peso)
    {
        if (tipoEnvio == "Terrestre") return peso * 1000;
        if (tipoEnvio == "Aereo") return peso * 3000;
        if (tipoEnvio == "Maritimo") return peso * 500;
        // cada vez que aparece un nuevo tipo de envío, hay que MODIFICAR este método
        throw new ArgumentException("Tipo no soportado");
    }
}
```
**¿Por qué es malo?** Cada nuevo tipo de envío obliga a **modificar** una clase que ya estaba funcionando y probada — alto riesgo de romper algo que ya andaba bien (el famoso "no toques lo que funciona").

#### 2. Intuición

Pensá en un enchufe eléctrico de pared. Podés **extender** lo que hace conectando distintos electrodomésticos (una lámpara, un cargador, una licuadora) **sin modificar** el enchufe ni la instalación eléctrica de la casa. El enchufe está "cerrado" a modificación pero "abierto" a que le conectes cosas nuevas.

#### 3. Definición formal

**"Las clases deben estar abiertas para extensión, pero cerradas para modificación."** Una clase está *cerrada* si está 100% lista para ser usada por otras: su interfaz está claramente definida y no cambiará. Una clase está *abierta* si se puede extender (agregar métodos/campos, anular comportamiento) sin tocar su código original. Se logra con **abstracción y polimorfismo**. Si la clase principal tiene un error, ese error se corrige en ella — **no** se crea una subclase para "parchear" el error (una subclase no debe ser responsable de arreglar los problemas de su padre).

#### 4-6. Refactorización

```csharp
public interface IEstrategiaEnvio
{
    decimal Calcular(decimal peso);
}

public class EnvioTerrestre : IEstrategiaEnvio
{
    public decimal Calcular(decimal peso) => peso * 1000;
}
public class EnvioAereo : IEstrategiaEnvio
{
    public decimal Calcular(decimal peso) => peso * 3000;
}
// Agregar un nuevo tipo de envío = crear una NUEVA clase, sin tocar ninguna existente

public class CalculadoraEnvio
{
    public decimal Calcular(IEstrategiaEnvio estrategia, decimal peso) => estrategia.Calcular(peso);
}
```

#### 7. UML antes/después

```
ANTES:                         DESPUÉS:
┌────────────────┐            ┌────────────────┐        ┌─────────────────┐
│ CalculadoraEnvio │            │ CalculadoraEnvio │───usa─>│ IEstrategiaEnvio │
├────────────────┤            └────────────────┘        ├─────────────────┤
│ +Calcular(tipo,peso)│                                       │ +Calcular(peso)  │
└────────────────┘                                       └────────△────────┘
  (if/else interno)                                    implementa │
                                                    ┌───────────────┼───────────────┐
                                              EnvioTerrestre   EnvioAereo   EnvioMaritimo
```

#### 8. Casos reales

| Dominio | Aplicación de OCP |
|---|---|
| Tienda virtual | Nuevos métodos de pago se agregan como nuevas clases `IMedioPago`, sin tocar el carrito de compras. |
| Banco | Nuevos tipos de interés/producto financiero como nuevas estrategias, sin tocar el motor de cálculo. |
| Videojuego | Nuevos tipos de enemigos como nuevas clases que implementan `IComportamientoEnemigo`. |

#### 9. Violaciones comunes que parecen cumplir OCP

- Usar polimorfismo (herencia) pero seguir teniendo un `switch (obj.GetType())` en otra parte del código para decidir qué hacer con cada tipo — eso **sigue violando OCP**, porque agregar un tipo nuevo obliga a tocar ese `switch`.
- Marcar todos los métodos como `virtual` "por si acaso" sin un verdadero diseño de extensión — no es OCP real, es solo flexibilidad accidental sin intención de diseño.

#### 10. Cómo reconocerlo

- ¿Necesito modificar código existente para agregar una funcionalidad nueva?
- ¿Hay un `if/else` o `switch` que crece cada vez que aparece un caso nuevo del mismo tipo de decisión?

#### 13. Comparaciones

**OCP vs. YAGNI** ("You Aren't Gonna Need It"): tensión real de diseño. OCP invita a diseñar pensando en la extensión futura (usar abstracciones); YAGNI advierte contra sobre-diseñar para escenarios hipotéticos que tal vez nunca ocurran. **Cómo decidir:** aplicá OCP cuando ya **ves evidencia real** de que ese punto de variación va a cambiar (ej: ya tenés 2 tipos de envío y sabés que vendrán más); no crees abstracciones especulativas para "por si acaso" un único caso que nunca cambió.

#### 14. Relación con Patrones de Diseño

- **Strategy:** es prácticamente la forma canónica de implementar OCP (como en el ejemplo de arriba).
- **Template Method:** define el esqueleto de un algoritmo en la clase base y deja que las subclases extiendan pasos específicos sin modificar el flujo general — cerrado para modificación del flujo, abierto para extensión de los pasos.
- **Decorator:** permite añadir responsabilidades a un objeto dinámicamente sin modificar su clase.

#### 15. Checklist OCP

- [ ] ¿Agregar un caso nuevo implica crear una clase nueva, no modificar una existente?
- [ ] ¿Existen `switch`/`if-else` que crecerán con cada caso nuevo del mismo tipo?
- [ ] ¿La abstracción (interfaz/clase abstracta) realmente representa una variación **real**, no especulativa?

#### 11. Ejercicios OCP

4. Identifica la violación de OCP en este código y refactorízalo:
```csharp
public class Notificador
{
    public void Enviar(string tipo, string mensaje)
    {
        if (tipo == "Email") { /* enviar email */ }
        else if (tipo == "SMS") { /* enviar sms */ }
    }
}
```
5. En el caso de estudio del material (`Automovil`), se dice: "Automovil es más extensible sin modificarla directamente. Si cambia la validación de algún atributo, ya no se tiene que cambiar la implementación dentro de Automovil." ¿Qué patrón de diseño está detrás de esta mejora?

---

### 🅛 LSP — Principio de Sustitución de Liskov (Liskov Substitution Principle)

#### 1. El problema (ejemplo textual del material)

```csharp
abstract class Automovil
{
    public abstract void Repostar(); // método genérico para cargar energía/combustible
}

class Electrico : Automovil
{
    public override void Repostar()
    {
        throw new NotImplementedException("Los autos eléctricos no pueden repostar gasolina."); // 🚨
    }
}

class Gasolina : Automovil
{
    public override void Repostar() => Console.WriteLine("Cargando gasolina...");
}

class Program
{
    static void Main()
    {
        List<Automovil> autos = new List<Automovil> { new Gasolina(), new Electrico() };
        foreach (var auto in autos)
            auto.Repostar(); // ❌ Violación de LSP: Electrico NO puede ejecutar este método sin explotar
    }
}
```

#### 2. Intuición

Si te prometen "esta caja hace todo lo que hace una caja normal" y resulta que, al usarla como caja normal, a veces **explota** en tus manos, esa caja no es sustituible de verdad — rompió la promesa. LSP dice: si tu código funciona con un objeto de la clase padre, **debe seguir funcionando exactamente igual** si le pasás cualquier objeto de una subclase, sin sorpresas ni excepciones inesperadas.

#### 3. Definición formal

**"Los objetos de una clase derivada deben poder sustituir a los objetos de su clase base sin alterar el comportamiento esperado del programa."** Al extender una clase, debe poder pasarse un objeto de la subclase en lugar de uno de la superclase sin romper el código cliente. La subclase debe seguir siendo compatible con el comportamiento de la superclase.

**Qué tener en cuenta al diseñar la herencia:**
- Evitar herencias incorrectas: no forzar subclases que no cumplen el mismo contrato que la superclase.
- Aplicar interfaces en lugar de heredar métodos innecesarios.
- Usar **composición en lugar de herencia** si la relación "Es-Un" no es clara — quizás sea mejor que un objeto "Tenga-Un" otro objeto (¡conexión directa con el Tema 2.1!).

**Las 7 reglas formales del material (requisitos para las subclases):**
1. Los tipos de parámetros en un método de la subclase deben coincidir o ser **más abstractos** que en la superclase (no más específicos).
2. El tipo de retorno en un método de la subclase debe coincidir o ser un **subtipo** del de la superclase.
3. Un método de la subclase **no debe generar tipos de excepción** que no se esperan del método base.
4. Una subclase **no debe reforzar** las condiciones previas (precondiciones).
5. Una subclase **no debe debilitar** las condiciones posteriores (postcondiciones).
6. Las **invariantes** de la superclase deben conservarse en la subclase.
7. Una subclase **no debe cambiar** los valores de los campos privados de la superclase.

#### 4-6. Refactorización paso a paso (ejemplo del material)

**Paso 1 — identificar que `Repostar()` no aplica a todos los `Automovil` por igual.**
**Paso 2 — separar el comportamiento incompatible en interfaces específicas:**
```csharp
abstract class Automovil
{
    public abstract void Conducir(); // esto SÍ lo hacen todos, sin excepción
}

interface ICombustible { void Repostar(); }      // solo para autos de gasolina
interface IElectrico { void CargarBateria(); }    // solo para autos eléctricos

class Gasolina : Automovil, ICombustible
{
    public override void Conducir() => Console.WriteLine("Conduciendo un auto de gasolina...");
    public void Repostar() => Console.WriteLine("Cargando gasolina...");
}

class Electrico : Automovil, IElectrico
{
    public override void Conducir() => Console.WriteLine("Conduciendo un auto eléctrico...");
    public void CargarBateria() => Console.WriteLine("Cargando batería...");
}
```
**Paso 3 — resultado final:**
```csharp
List<Automovil> autos = new List<Automovil> { new Gasolina(), new Electrico() };
foreach (var auto in autos) auto.Conducir(); // ✅ ambos pueden conducir sin problemas

List<ICombustible> autosGasolina = new List<ICombustible> { new Gasolina() };
foreach (var a in autosGasolina) a.Repostar(); // ✅ solo gasolina reposta

List<IElectrico> autosElectricos = new List<IElectrico> { new Electrico() };
foreach (var a in autosElectricos) a.CargarBateria(); // ✅ solo eléctricos cargan batería
```
Ahora **cualquier** `Automovil` es 100% sustituible donde se espera un `Automovil` — nunca va a explotar con una excepción inesperada.

#### Otro ejemplo del material (solución alternativa): `Document`/`WritableDocument`

Caso: un método esperaba poder llamar `Guardar()` sobre cualquier `Documento`, pero algunos documentos son de solo lectura. La solución: `Document` es un documento *readonly* (sin `Guardar()`), y `WritableDocument extends Document` **agrega** el comportamiento `Save()` — nunca se fuerza a un documento readonly a "implementar" guardar lanzando una excepción.

#### 7. UML antes/después

```
ANTES (viola LSP):                    DESPUÉS (cumple LSP):
   Automovil (abstract)                    Automovil (abstract)
   +Repostar()                              +Conducir()
        △                                        △
   ┌────┴────┐                          ┌────────┼────────┐
Gasolina  Electrico                  Gasolina            Electrico
(Electrico.Repostar()               implementa           implementa
 lanza excepción)                   ICombustible          IElectrico
```

#### 8. Casos reales

| Dominio | Violación típica de LSP | Corrección |
|---|---|---|
| Tienda | `ProductoDigital : Producto` que lanza excepción en `CalcularPesoEnvio()` (los productos digitales no pesan) | Separar `IEnviable` para productos físicos |
| Banco | `CuentaAhorros : Cuenta` que lanza excepción en `SobregirarSaldo()` porque no lo permite, mientras `CuentaCorriente` sí | Repensar si `SobregirarSaldo()` debería estar en la superclase, o moverlo a una interfaz específica de cuentas con sobregiro |
| Biblioteca | `LibroDigital : Libro` que lanza excepción en `Renovar()` porque no aplica devolución física | Separar `IPrestamoFisico` |
| Hospital | `PacienteAmbulatorio : Paciente` que lanza excepción en `AsignarCama()` | Separar `IHospitalizable` |

#### 9. Violaciones comunes que parecen cumplir LSP

- Una subclase que sobrescribe un método y simplemente **no hace nada** (`{ }` vacío) en lugar de lanzar excepción — sigue violando LSP en la práctica, porque el comportamiento esperado silenciosamente **no ocurre**, lo cual puede ser tan peligroso como una excepción (bugs silenciosos).
- Una subclase que "amplía" las precondiciones de un método (por ejemplo, el padre acepta cualquier `int`, pero el hijo lanza excepción si es negativo) — viola la regla 4 (no reforzar precondiciones), aunque a simple vista "funcione".

#### 10. Cómo reconocerlo

- ¿Esta subclase rompe el comportamiento esperado del padre en algún escenario?
- ¿Hay algún método heredado que la subclase no puede cumplir de verdad?
- ¿El código cliente necesita hacer `if (obj is TipoEspecífico)` para evitar que algo falle? (síntoma clásico de violación LSP)

#### 13. Comparaciones

**LSP vs. herencia "correcta" (Día 2):** LSP es, en esencia, la **regla de validación** de que tu jerarquía de herencia está bien diseñada semánticamente, no solo sintácticamente. El compilador te deja heredar y sobrescribir con excepciones — LSP es una regla de **diseño**, no del lenguaje, por eso el compilador nunca te avisa cuando lo violás.

#### 14. Relación con Patrones de Diseño

- **Strategy / Composición sobre herencia:** cuando LSP se rompe, la solución casi siempre es cambiar herencia rígida por composición + interfaces (tal como en el ejemplo `ICombustible`/`IElectrico`).
- **Template Method:** ayuda a mantener LSP porque el flujo general vive en la superclase y solo pasos bien definidos se sobrescriben, reduciendo el riesgo de que una subclase rompa el contrato completo.

#### 15. Checklist LSP

- [ ] ¿Puedo reemplazar cualquier subclase por su superclase en cualquier parte del código sin que nada se rompa?
- [ ] ¿Alguna subclase lanza excepciones que la superclase no esperaba?
- [ ] ¿Alguna subclase refuerza precondiciones o debilita postcondiciones?
- [ ] ¿El código cliente necesita preguntar "qué tipo específico es este objeto" para funcionar bien?

#### 11. Ejercicios LSP

6. Identifica la violación de LSP:
```csharp
class Ave { public virtual void Volar() => Console.WriteLine("Volando..."); }
class Pinguino : Ave { public override void Volar() => throw new NotSupportedException(); }
```
7. Propón dos soluciones de diseño distintas para el ejercicio anterior (una con interfaces, otra repensando la jerarquía completa).
8. Analiza: ¿el caso `CuentaAhorros`/`CuentaCorriente` de la tabla de "Casos reales" tiene una solución única, o depende del negocio? Justifica.

---

### 🅘 ISP — Principio de Segregación de Interfaces (Interface Segregation Principle)

#### 1. El problema

```csharp
public interface IImpresora
{
    void Imprimir();
    void Escanear();
    void Fax(); // una impresora vieja de solo impresión NO tiene fax ni escáner
}

public class ImpresoraBasica : IImpresora
{
    public void Imprimir() => Console.WriteLine("Imprimiendo...");
    public void Escanear() => throw new NotImplementedException(); // 🚨 forzada a implementar algo que no tiene
    public void Fax() => throw new NotImplementedException();       // 🚨
}
```

#### 2. Intuición

Si un contrato de trabajo te obliga a "saber tocar el piano" aunque tu puesto sea de contador, ese contrato está mal armado — te está obligando a algo irrelevante para tu rol real. Un buen contrato de trabajo (interfaz) solo exige lo que **realmente** te corresponde.

#### 3. Definición formal

**"Una interfaz no debe obligar a una clase a implementar métodos que no usa."** Es mejor tener varias interfaces pequeñas y específicas que una única interfaz grande y genérica que fuerce implementaciones innecesarias.

**Qué tener en cuenta al diseñar para cumplir ISP:**
1. Evitar interfaces muy generales (si tiene demasiados métodos, probablemente está mal diseñada).
2. Agrupar métodos relacionados en interfaces específicas.
3. Aplicar "solo lo que necesito" — cada clase implementa solo las interfaces que usa.
4. Usar interfaces pequeñas y reutilizables — mejora flexibilidad y mantenimiento.

*(Conexión directa con el material del Día 2: "Segregar Interfaz" = dividir sus métodos en varias interfaces porque tiene muchos métodos que no todas sus clases deben implementar — es literalmente la definición de ISP vista antes de tener nombre.)*

#### 4-6. Refactorización

```csharp
public interface IImprimible { void Imprimir(); }
public interface IEscaneable { void Escanear(); }
public interface IFaxeable { void Fax(); }

public class ImpresoraBasica : IImprimible
{
    public void Imprimir() => Console.WriteLine("Imprimiendo...");
}

public class ImpresoraMultifuncional : IImprimible, IEscaneable, IFaxeable
{
    public void Imprimir() => Console.WriteLine("Imprimiendo...");
    public void Escanear() => Console.WriteLine("Escaneando...");
    public void Fax() => Console.WriteLine("Enviando fax...");
}
```
Ahora `ImpresoraBasica` solo implementa lo que realmente hace — sin métodos "fantasma" que lanzan excepción.

#### 7. UML antes/después

```
ANTES:                          DESPUÉS:
┌────────────┐                 ┌────────────┐  ┌────────────┐  ┌───────────┐
│ IImpresora  │                 │ IImprimible │  │ IEscaneable │  │ IFaxeable  │
│ +Imprimir()  │                 │ +Imprimir() │  │ +Escanear() │  │ +Fax()      │
│ +Escanear()  │                 └──────△─────┘  └──────△─────┘  └─────△─────┘
│ +Fax()        │                        │realiza        │realiza          │realiza
└──────△─────┘                 ImpresoraBasica    ImpresoraMultifuncional (implementa las 3)
       │realiza (forzado)
ImpresoraBasica (Escanear/Fax lanzan excepción)
```

#### 8. Casos reales

| Dominio | Interfaz "gorda" mal diseñada | Segregación correcta |
|---|---|---|
| Tienda virtual | `IProductoCompleto` con `CalcularEnvio()`, `AplicarDescuentoVIP()`, `GenerarFactura()` para todos los productos | `IEnviable`, `IDescontable`, `IFacturable` por separado |
| Videojuego | `IPersonaje` con `Volar()`, `Nadar()`, `Excavar()` para todos los personajes | `IVolador`, `INadador`, `IExcavador` |
| Hospital | `IPersonalMedico` con `Operar()`, `RecetarMedicamento()`, `Enfermeria()` para todo el personal | `ICirujano`, `IMedicoGeneral`, `IEnfermero` |

#### 9. Violaciones comunes que parecen cumplir ISP

- Dividir interfaces "por capricho" sin agrupar métodos realmente relacionados — termina generando decenas de micro-interfaces inconexas, tan difícil de mantener como una sola gigante (el ISP no significa "una interfaz por método").
- Implementar todos los métodos de una interfaz grande pero dejando la mitad con `throw new NotImplementedException()` "temporalmente" — sigue siendo una violación activa, no una solución.

#### 10. Cómo reconocerlo

- ¿Esta interfaz obliga a implementar métodos innecesarios para alguna de sus clases implementadoras?
- ¿Alguna implementación de esta interfaz tiene métodos vacíos o que lanzan excepción "porque no aplica"?

#### 13. Comparaciones

**ISP vs. SRP:** SRP habla de que una **clase** tenga una sola razón para cambiar; ISP habla de que una **interfaz** no obligue a implementar cosas irrelevantes. Son primos: ambos buscan evitar que un componente cargue con responsabilidades/contratos que no le corresponden — SRP mira "hacia adentro" de la clase, ISP mira "hacia afuera" desde el punto de vista del contrato que consumen otros.

#### 14. Relación con Patrones de Diseño

- **Adapter:** cuando una clase existente no cumple con la interfaz segregada que necesitás, un Adapter la envuelve para adaptarla sin modificarla — mantiene ISP sin tocar código de terceros.
- **Facade:** agrupa varias interfaces pequeñas detrás de una interfaz simplificada para el cliente que sí necesita todo — sin obligar a los demás clientes que no necesitan todo.

#### 15. Checklist ISP

- [ ] ¿Alguna clase implementa métodos de una interfaz que nunca usa realmente?
- [ ] ¿Hay métodos que lanzan `NotImplementedException` "porque la interfaz me obliga"?
- [ ] ¿Las interfaces agrupan métodos genuinamente relacionados (ni muy grandes, ni fragmentadas sin sentido)?

#### 11. Ejercicios ISP

9. Detecta la violación: una interfaz `ITrabajador` con `Programar()`, `DisenarUI()`, `GestionarProyecto()` implementada por `Programador`, `Diseñador` y `GerenteProyecto`. Refactoriza segregando correctamente.
10. ¿Por qué segregar interfaces en exceso (una interfaz por cada método individual) también es un problema de diseño, aunque técnicamente "cumpla" ISP?

---

### 🅓 DIP — Principio de Inversión de Dependencia (Dependency Inversion Principle)

#### 1. El problema

```csharp
public class Motor // clase de BAJO nivel (implementación concreta)
{
    public void Encender() => Console.WriteLine("Motor a combustión encendido");
}

public class Automovil // clase de ALTO nivel (lógica de negocio)
{
    private Motor motor = new Motor(); // ❌ Automovil CREA directamente una implementación concreta
    public void Arrancar() => motor.Encender();
}
```
**¿Por qué es malo?** `Automovil` está directamente acoplada a `Motor`. Si querés cambiar a `MotorElectrico`, tenés que **modificar** `Automovil` (rompe OCP también). Tampoco podés probar `Automovil` de forma aislada (pruebas unitarias) porque siempre arrastra un `Motor` real.

#### 2. Intuición

Un enchufe de pared (alto nivel: "la instalación eléctrica de la casa") no depende de si le conectás una lámpara marca X o marca Y (bajo nivel). Ambos, el enchufe y el electrodoméstico, dependen de un **estándar común** (la forma del enchufe — la abstracción), no el uno del otro directamente.

#### 3. Definición formal

**"Las clases de alto nivel (lógica de negocio) no deberían depender de las clases de bajo nivel (implementaciones específicas). Ambas deberían depender de abstracciones. Las abstracciones no deberían depender de los detalles — los detalles deberían depender de abstracciones."** Cuando esto se cumple, hay "inversión de dependencia": la dependencia originalmente apuntaba de alto→bajo nivel, y ahora ambos apuntan hacia la abstracción.

**Recomendaciones del material:**
- Describir interfaces para las operaciones de bajo nivel en términos comerciales/de negocio.
- Diseñar para que las clases de alto nivel dependan de esas interfaces, no de clases concretas.
- Las clases de bajo nivel, al implementar esas interfaces, se vuelven dependientes de la lógica de negocio — se invierte la dirección original de la dependencia.
- **DIP va de la mano con OCP.**

#### 4-6. Refactorización — Inyección de Dependencias en el Constructor

```csharp
public interface IMotor { void Encender(); } // la abstracción, definida en términos de negocio

public class MotorCombustion : IMotor
{
    public void Encender() => Console.WriteLine("Motor a combustión encendido");
}
public class MotorElectrico : IMotor
{
    public void Encender() => Console.WriteLine("Motor eléctrico encendido silenciosamente");
}

public class Automovil // clase de alto nivel
{
    private readonly IMotor motor; // depende de la ABSTRACCIÓN, no de una implementación concreta

    public Automovil(IMotor motor) // Inyección de Dependencias por Constructor
    {
        this.motor = motor;
    }

    public void Arrancar() => motor.Encender();
}

// Main
Automovil autoGasolina = new Automovil(new MotorCombustion());
Automovil autoElectrico = new Automovil(new MotorElectrico());
```

**Beneficios:** desacoplamiento total (`Automovil` no depende de ninguna implementación concreta de `IMotor`); facilidad de cambio (podés reemplazar `MotorCombustion` por `MotorElectrico` **sin tocar** `Automovil`); testeable (podés inyectar un `IMotor` falso/mock para pruebas unitarias).

#### 7. UML antes/después

```
ANTES:                              DESPUÉS:
┌───────────┐                     ┌───────────┐         ┌────────┐
│ Automovil  │──crea/usa──>       │ Automovil  │──usa──> │ IMotor  │
└───────────┘         Motor       └───────────┘         └───△────┘
                                                    implementa │
                                              ┌───────────────┼───────────────┐
                                        MotorCombustion              MotorElectrico
```

#### 8. Errores comunes al violar DIP (del material, textual)

1. **Acoplamiento fuerte entre clases:** cuando una clase de alto nivel crea instancias de clases de bajo nivel (`new` directamente dentro de la clase).
2. **Uso de implementaciones en lugar de abstracciones:** si un método espera un `MotorCombustion` en vez de un `IMotor`, el código no es extensible.
3. **Abuso de contenedores de inyección de dependencias:** si se abusa creando demasiadas interfaces innecesarias, el código se vuelve complejo sin beneficio real (¡ojo! el material advierte explícitamente contra el exceso, no solo contra el defecto).

#### 9. Casos reales (con foco en el caso de estudio oficial del material: proyecto Concesionario)

> "`Automovil` accedía a atributos definidos en `Concesionario`, como `valor_minimo_nuevo`, lo que generaba acoplamiento innecesario. Para romper el acoplamiento entre `Automovil` y `Validacion`, se diseña una arquitectura donde `Automovil` no dependa directamente de `Validaciones`, sino que reciba una instancia de un servicio de validación a través de inyección de dependencias (DIP)."

Esto es exactamente el patrón enseñado: en lugar de que `Automovil` conozca directamente los detalles de `Concesionario` o de `Validaciones`, recibe una **abstracción** (`IServicioValidacion`) inyectada desde fuera.

#### 10. Cómo reconocerlo

- ¿Esta clase depende directamente de implementaciones concretas (con `new` adentro)?
- ¿Esta clase se pregunta cosas que le corresponden a otra capa (una clase de dominio consultando atributos de otra clase "dueña" de una regla de negocio ajena)?
- ¿Puedo probar esta clase con un *mock*, sin instanciar sus dependencias reales?

#### 13. Comparaciones

**DIP vs. KISS** ("Keep It Simple, Stupid"): tensión real. DIP invita a introducir interfaces y capas de abstracción; KISS advierte contra la complejidad innecesaria. **Cómo decidir:** aplicá DIP cuando la dependencia es hacia algo que **realmente puede variar** (una fuente de datos, un proveedor externo, un mecanismo de validación) o que necesitás simular en pruebas. No apliques DIP a algo que nunca va a tener una segunda implementación real (ej: no necesitás una interfaz `ISumador` para una simple operación matemática interna).

#### 14. Relación con Patrones de Diseño

- **Dependency Injection (el patrón, no solo el principio):** es el mecanismo técnico más directo para lograr DIP — como en el ejemplo de arriba.
- **Factory:** cuando la creación de la implementación concreta es compleja, una Factory encapsula esa creación, y la clase de alto nivel sigue dependiendo solo de la abstracción.
- **Observer:** en el caso de estudio de `Mayor` (multas de tránsito), el material recomienda explícitamente "usar patrón Observer para eventos en lugar de excepciones" — conecta directamente con Programación Orientada a Eventos (Tema 3.2) como forma de desacoplar (DIP) el disparo de una notificación de quién la escucha.

#### 15. Checklist DIP

- [ ] ¿Las clases de alto nivel dependen de interfaces, no de clases concretas?
- [ ] ¿Las dependencias se reciben por constructor (o método), en vez de crearse con `new` internamente?
- [ ] ¿Podría reemplazar cualquier implementación concreta sin tocar la clase de alto nivel?
- [ ] ¿No estoy creando interfaces "por moda" donde nunca habrá una segunda implementación real?

#### 11. Ejercicios DIP

11. Refactoriza para cumplir DIP:
```csharp
public class ServicioReportes
{
    private BaseDatosMySQL db = new BaseDatosMySQL();
    public void GenerarReporte() { var datos = db.Consultar("SELECT * FROM ventas"); }
}
```
12. En el caso de estudio de `Transito` (material oficial), se dice que viola DIP porque "depende directamente de clases concretas (Mayor, Menor)". Propón la interfaz que resolvería esto y explica cómo cambiaría el diseño.

---

## 40 Preguntas de Análisis — Examen de SOLID

*(No son preguntas de memoria: cada una exige analizar código o un escenario, detectar el/los principio(s) en juego, y justificar. Soluciones razonadas en "Soluciones SOLID" al final del Tema 4.1.)*

**Bloque SRP (1-8)**
1. Una clase `Reporte` que genera el reporte, lo formatea en HTML y lo envía por FTP. ¿Qué principio viola? Refactoriza.
2. ¿Una clase con 15 métodos públicos necesariamente viola SRP? Justifica.
3. Diferencia "una clase con una responsabilidad" de "una clase con un solo método".
4. Un `ValidadorAutomovil` que además registra logs de auditoría de cada validación. ¿Cumple SRP?
5. ¿Qué señal de código (*code smell*) sugiere más fuertemente una violación de SRP: una clase larga, o una clase con métodos que usan grupos de atributos completamente distintos entre sí? Justifica.
6. Diseña `Manilla` del proyecto integrador cumpliendo SRP: ¿qué responsabilidades NO deberían estar en esa clase?
7. Un método `GuardarYNotificar()` — ¿qué te dice el nombre sobre una posible violación de SRP?
8. ¿SRP se aplica solo a clases, o también a métodos? Justifica con tus palabras.

**Bloque OCP (9-16)**
9. Identifica la violación: un método `CalcularDescuento(string tipoCliente)` con `if/else` para "Regular", "VIP", "Premium".
10. Refactoriza el ejercicio anterior aplicando OCP.
11. ¿Agregar un nuevo `if` a un `switch` ya existente rompe OCP siempre, o depende del contexto? Justifica con la tensión OCP vs. YAGNI.
12. ¿Por qué "está cerrada para modificación" no significa "no se puede tocar nunca"? (pista: la corrección de errores es distinta a la extensión de funcionalidad).
13. En el proyecto del Parque, ¿cómo aplicarías OCP para agregar nuevos tipos de atracciones con reglas de puntos distintas sin modificar la clase `Atraccion` existente?
14. ¿Qué patrón de diseño usarías para que `CalculadoraMultas` (caso de estudio Tránsito) permita agregar nuevos tipos de infracción sin modificar código existente?
15. V o F: "Cerrar una clase para modificación significa hacerla `sealed` en C#." Justifica con la definición formal de "cerrada" del material.
16. Explica con tus palabras por qué OCP "fomenta el uso de la abstracción y el polimorfismo".

**Bloque LSP (17-24)**
17. `Cuadrado : Rectangulo` donde `Cuadrado` sobrescribe `SetAncho()` y `SetAlto()` para que ambos cambien juntos. ¿Viola LSP? Justifica con al menos una de las 7 reglas formales.
18. Identifica cuál de las 7 reglas formales de LSP viola este código: la superclase `Repositorio.Guardar()` nunca lanza excepciones, pero `RepositorioRemoto.Guardar()` (subclase) lanza `TimeoutException`.
19. ¿Por qué la regla "una subclase no debe reforzar las precondiciones" protege al código cliente?
20. Diseña un caso propio (no del material) donde LSP se viole por "debilitar una postcondición".
21. ¿Es siempre incorrecto que `Pinguino` herede de `Ave`? Propón un rediseño que sí respete LSP sin usar excepciones ni métodos vacíos.
22. Relaciona LSP con la regla del Día 2: "usar composición en lugar de herencia si la relación ES-UN no es clara". Da un ejemplo propio.
23. ¿Qué evidencia en el código cliente (fuera de las clases mismas) delata que hay una violación de LSP?
24. V o F: "Si el código compila sin errores, la jerarquía de herencia cumple LSP." Justifica.

**Bloque ISP (25-30)**
25. Identifica la violación: una interfaz `IEmpleado` con `CalcularNomina()`, `CalcularComisionVentas()`, implementada tanto por `EmpleadoAdministrativo` como por `Vendedor` (el administrativo no tiene comisión de ventas).
26. Refactoriza el ejercicio anterior.
27. ¿Segregar demasiado (una interfaz de un solo método por cada capacidad) puede ser un problema? ¿Por qué sí o por qué no?
28. Relaciona ISP con "Segregar Interfaz" tal como se explicó en el Día 2 (Tema 2.3), citando la definición textual del material.
29. En el proyecto del Parque, ¿qué interfaz(es) segregarías si `Atraccion` tuviera métodos como `RequiereAlturaMinima()` que no todas las atracciones necesitan?
30. ¿Cómo detectás, revisando solo el código de una clase implementadora (sin ver la interfaz), que probablemente hay una violación de ISP?

**Bloque DIP (31-36)**
31. Identifica la violación: `ServicioPedidos` instancia directamente `new EnviadorEmailSMTP()` dentro de uno de sus métodos.
32. Refactoriza el ejercicio anterior aplicando DIP con Inyección de Dependencias por constructor.
33. ¿Por qué DIP "va de la mano con OCP" (frase textual del material)? Explica la relación causal.
34. Da un ejemplo (usa el proyecto del Parque) de una dependencia que **no** valdría la pena invertir con una interfaz, aplicando el criterio DIP vs. KISS.
35. En el caso de estudio de `Mayor` (Tránsito), se recomienda "inyectar dependencias para cada responsabilidad" tras dividir la clase en varios servicios. ¿Por qué dividir primero (SRP) y luego inyectar (DIP), y no al revés?
36. ¿Qué diferencia hay entre "Inyección de Dependencias" (el patrón) y "Inversión de Dependencia" (el principio)? (🧩 contexto complementario: son conceptos relacionados pero no idénticos — DI es una técnica; DIP es el principio de diseño que DI ayuda a cumplir).

**Bloque integrador — varios principios a la vez (37-40)**
37. Analiza esta clase y di **todos** los principios SOLID que viola, justificando cada uno:
```csharp
public class GestorPedidos
{
    private MySqlConnection conexion = new MySqlConnection("...");
    public void ProcesarPedido(string tipoPedido, Pedido p)
    {
        if (tipoPedido == "Normal") { /* ... */ }
        else if (tipoPedido == "Express") { /* ... */ }
        // guarda en BD directamente aquí
        conexion.Open();
        // ... y también envía el email de confirmación aquí mismo
        var smtp = new SmtpClient();
        smtp.Send(/* ... */);
    }
}
```
38. Refactoriza completamente el ejercicio 37 aplicando los 5 principios donde corresponda.
39. Retoma el caso de estudio oficial de `Vehiculo` (del material): identifica qué principio cumple (SRP) y cuál no cumple (OCP), y aplica la corrección sugerida ("crear interfaz `IVehiculo` para permitir diferentes implementaciones").
40. Diseña el `Sistema del Parque de Diversiones` completo (proyecto integrador) aplicando explícitamente los 5 principios SOLID, y documenta en una tabla qué decisión de diseño corresponde a cada principio (verás la solución completa en la sección "Proyecto Integrador — Etapa 4" más adelante).

---

## Soluciones SOLID — Ejercicios 1-12 y las 40 Preguntas

*(Revisa esto solo después de intentar resolver tú mismo. Las respuestas son razonadas, no solo "correcto/incorrecto".)*

**Ejercicios 1-12 (por principio):**
1. Viola **SRP**: mezcla gestión de items, persistencia (BD) y notificación (email) en una sola clase — 3 razones de cambio distintas.
2. Separar en `Factura` (solo datos), `RepositorioFactura` (persistencia), `NotificadorFactura` (email) — cada una con una sola responsabilidad.
3. **Cumple SRP** si `ValidadorAutomovil` **solo** valida (una responsabilidad clara: validación). Si además registrara logs, auditoría o persistiera datos, empezaría a violarlo.
4. Viola **OCP**: cada nuevo tipo de notificación exige modificar el `if/else` existente dentro de `Enviar()`.
5. Crear `INotificador` con implementaciones `NotificadorEmail`, `NotificadorSMS`; `Notificador` pasa a depender de la abstracción, no de los `if`.
6. El patrón detrás es **Strategy** (o el uso general de polimorfismo vía interfaz para reemplazar condicionales de tipo).
7. Viola **LSP**: `Pinguino` hereda `Volar()` pero no puede cumplirlo — lanza excepción donde el código cliente esperaba que funcionara igual que con cualquier otra `Ave`.
8. **Solución 1 (interfaz):** separar `IVolador` de `Ave`, y que solo las aves voladoras lo implementen. **Solución 2 (rediseño de jerarquía):** hacer que `Ave` no tenga `Volar()` en absoluto (muchas aves no vuelan), y crear una subclase o interfaz específica `AveVoladora` para las que sí.
9. Depende del negocio: si "sobregirar" es una regla que **algunas** cuentas cumplen y otras no por diseño del dominio (no por error), puede resolverse igual que el caso Ave/Pingüino — separando el comportamiento en una interfaz específica en lugar de forzarlo en la superclase común.
10. `ITrabajador` gorda se segrega en `IProgramador { Programar() }`, `IDiseñador { DisenarUI() }`, `IGerente { GestionarProyecto() }`; cada clase implementa solo la(s) que le corresponde(n).
11. Porque genera un número excesivo de interfaces triviales que dificultan la navegación y comprensión del código sin aportar un beneficio real de desacoplamiento — el ISP busca cohesión de contrato, no fragmentación extrema.
12. `ServicioReportes` debe recibir una abstracción `IRepositorioVentas` inyectada por constructor, en lugar de instanciar `BaseDatosMySQL` directamente — así se puede cambiar de MySQL a otra fuente de datos, o inyectar un mock para pruebas, sin tocar `ServicioReportes`.

**Las 40 preguntas (resumen razonado por bloque):**

*SRP (1-8):* 1. Viola SRP (genera, formatea y envía — 3 responsabilidades); separar en `GeneradorReporte`, `FormateadorHTML`, `ServicioFTP`. 2. No necesariamente — el número de métodos no es el criterio; lo que importa es si todos giran en torno a **una sola** responsabilidad coherente. 3. Una responsabilidad puede requerir varios métodos relacionados entre sí (cohesión); "un solo método" no garantiza una sola responsabilidad si ese método hace varias cosas internamente. 4. No cumple SRP si mezcla validación + auditoría — son dos razones de cambio distintas (cambia la regla de validación vs. cambia la política de auditoría). 5. La segunda señal (métodos que usan grupos de atributos completamente distintos) es más confiable — indica que la clase agrupa, de hecho, dos "sub-clases" ocultas; el tamaño solo es una señal indirecta. 6. `Manilla` no debería manejar impresión de tickets, ni lógica de venta (eso es responsabilidad de `Taquilla`), ni persistencia en base de datos. 7. El nombre con "Y" (`GuardarYNotificar`) es una señal textual directa de dos responsabilidades mezcladas. 8. Se aplica principalmente a clases, pero el mismo razonamiento (una responsabilidad, una razón de cambio) es útil para evaluar métodos también, aunque el material lo define formalmente a nivel de clase.

*OCP (9-16):* 9. Viola OCP: cada tipo de cliente nuevo exige modificar el método. 10. Crear `IEstrategiaDescuento` con `DescuentoRegular`, `DescuentoVIP`, `DescuentoPremium`. 11. Depende del contexto (tensión con YAGNI): si ya hay evidencia de que van a seguir apareciendo casos, vale la pena abstraer; si es un caso aislado sin evidencia de crecimiento, agregar un `if` puntual no es necesariamente una falta grave. 12. Porque "cerrada para modificación" se refiere a **no modificar el comportamiento ya probado y en uso** al agregar funcionalidad nueva; corregir un error real (un bug) es distinto de agregar una funcionalidad — los errores sí se corrigen en la clase original. 13. Crear una interfaz `ITipoAtraccion` (o clase abstracta) con un método `CalcularPuntos()`, y que cada nuevo tipo de atracción sea una nueva clase que la implemente, sin tocar `Atraccion`. 14. **Strategy**. 15. Falso — "cerrada" es un concepto de **diseño** (su interfaz pública no debería necesitar cambiar), no una palabra clave del lenguaje; `sealed` impide herencia, que es lo opuesto a lo que OCP busca habilitar. 16. Porque el polimorfismo permite que el código que usa la abstracción funcione igual sin importar qué implementación concreta reciba, y la abstracción no necesita cambiar cuando aparece una implementación nueva.

*LSP (17-24):* 17. Sí viola LSP — viola la regla de invariantes (regla 6): un `Rectangulo` normal permite cambiar ancho y alto independientemente; un `Cuadrado` que los fuerza a cambiar juntos rompe esa invariante esperada por el código cliente que trata a cualquier `Rectangulo` genéricamente. 18. Viola la regla 3 (no debe generar tipos de excepción que no se esperan del método base). 19. Porque el código cliente fue escrito confiando en las precondiciones del método base; si una subclase exige condiciones más estrictas, el código cliente que funcionaba con el padre puede fallar inesperadamente con la subclase. 20. Ejemplo propio: `Ordenador.Ordenar(lista)` en la superclase garantiza que la lista queda sin duplicados; una subclase que "optimiza" el método pero deja de eliminar duplicados debilita la postcondición esperada. 21. No es incorrecto per se; el rediseño correcto es que `Ave` no tenga `Volar()` como comportamiento garantizado para todas, y que exista una interfaz `IVolador` (o una subclase intermedia `AveVoladora`) que solo implementen las aves que realmente vuelan. 22. Ejemplo propio: si `Empleado` y `Practicante` no comparten completamente el mismo contrato (el practicante no puede "AprobarCompra()"), quizás `Practicante` no debería heredar de `Empleado`, sino que ambos podrían **tener** una `Persona` en común vía composición, y compartir solo lo genuinamente común. 23. Código cliente lleno de `if (obj is TipoEspecífico) { ... } else { ... }` para evitar que ciertos tipos rompan el flujo general — es la señal más clara. 24. Falso — LSP es una regla semántica/de comportamiento, no sintáctica; el código puede compilar perfectamente y aun así violar LSP en tiempo de ejecución (como el ejemplo de `Electrico.Repostar()`).

*ISP (25-30):* 25. Viola ISP — `EmpleadoAdministrativo` se ve forzado a implementar `CalcularComisionVentas()` sin necesitarlo. 26. Segregar en `ICalculableNomina` (todos) y `ICalculableComision` (solo vendedores). 27. Sí puede ser un problema — genera fragmentación excesiva sin beneficio real de cohesión (ver pregunta 11/27 de ejercicios anteriores). 28. El material define "Segregar interfaz" como "dividir sus métodos en varias interfaces más pequeñas cuando una interfaz tiene muchos métodos que no todas sus clases implementadoras necesitan" — es exactamente la definición de ISP, presentada antes de nombrarlo formalmente. 29. Una interfaz `IAtraccionConRestriccion` con `RequiereAlturaMinima()`/`ValidarAltura()`, implementada solo por las atracciones que la necesitan (montañas rusas, por ejemplo), no por todas. 30. Si encontrás métodos con cuerpos vacíos, `throw new NotImplementedException()`, o comentarios tipo "esto no aplica aquí", es una señal directa de violación de ISP en esa implementación.

*DIP (31-36):* 31. Viola DIP — `ServicioPedidos` depende de una implementación concreta (`EnviadorEmailSMTP`) creada internamente. 32. Crear `IEnviadorEmail`, inyectarlo por constructor en `ServicioPedidos`. 33. Porque ambos apuntan a la misma solución técnica: depender de abstracciones (interfaces) en vez de implementaciones concretas permite tanto **extender sin modificar** (OCP) como **invertir la dependencia hacia la abstracción** (DIP) — son dos caras de usar bien el polimorfismo. 34. Ejemplo: la relación entre `Manilla` y un simple cálculo aritmético interno (puntos = saldo/500) no necesita una interfaz — nunca vas a tener una "segunda implementación" de esa fórmula que amerite abstraerla; aplicar DIP aquí sería sobre-ingeniería (viola KISS sin necesidad real). 35. Porque inyectar dependencias en una clase que todavía tiene múltiples responsabilidades mezcladas (violando SRP) no resuelve el problema de fondo — primero hay que separar responsabilidades (SRP) para que cada servicio resultante tenga sentido propio, y **luego** decidir qué abstracciones inyectarle (DIP); invertir el orden produce interfaces mal delimitadas. 36. La **Inyección de Dependencias (DI)** es una técnica concreta (pasar dependencias por constructor, propiedad o método) — es el **cómo**. La **Inversión de Dependencia (DIP)** es el principio de diseño que dice que las dependencias deben apuntar hacia abstracciones — es el **por qué**/**qué**. DI es una de las formas (la más común) de lograr DIP, pero se puede usar DI sin necesariamente cumplir DIP a fondo (por ejemplo, inyectando igual una clase concreta en vez de una interfaz).

*Integrador (37-40):* 37. Viola **SRP** (procesa, persiste y notifica en un solo método), **OCP** (el `if/else` de tipo de pedido crece con cada caso nuevo), y **DIP** (crea `MySqlConnection` y `SmtpClient` directamente, sin abstracciones). No viola directamente LSP ni ISP porque no hay herencia ni interfaces involucradas en este fragmento. 38. Separar en `ProcesadorPedido` (con `IEstrategiaPedido` para Normal/Express — OCP), `IRepositorioPedidos` inyectado (DIP, reemplaza el acceso directo a MySQL), `IServicioNotificacion` inyectado (DIP, reemplaza el `SmtpClient` directo) — cada uno con su propia responsabilidad (SRP). 39. `Vehiculo` cumple SRP porque cada clase (validación aparte) tiene una responsabilidad clara; no cumple OCP porque agregar un nuevo tipo de vehículo exige tocar código de validación existente en vez de extenderlo — la corrección es crear `IVehiculo` (o mantener `Automovil` abstracta con métodos virtuales) para que nuevos tipos se agreguen como nuevas clases. 40. Ver tabla completa en "Proyecto Integrador — Etapa 4" a continuación.

---

## Tema 4.2 — Programación Orientada a Servicios (SOA)

### Explicación

**Intuición.** Pensá en un aeropuerto. No hay una sola "megaentidad" que venda tiquetes, revise maletas, controle vuelos y sirva comida — hay **servicios independientes** (aerolíneas, seguridad, control aéreo, restaurantes) que se comunican mediante protocolos bien definidos (check-in, rayos X, torre de control) sin necesitar saber **cómo** funciona internamente cada uno. Eso es Orientación a Servicios: dividir el sistema en unidades independientes que se comunican por **contratos claros**, no por conocimiento interno mutuo.

### Definición y principios básicos

Enfoque de diseño que estructura las aplicaciones como un **conjunto de servicios independientes**, cada uno con una función específica, que se comunican entre sí a través de **interfaces bien definidas** — generalmente usando protocolos estándar como HTTP, SOAP o REST.

| Principio | Descripción |
|---|---|
| **Bajo acoplamiento** | Los servicios deben ser independientes entre sí — minimizar dependencias directas facilita el mantenimiento y la evolución de cada uno por separado. *(Idéntico en espíritu a "acoplamiento bajo" del Día 1 y a DIP del Día 4, pero a escala de sistemas completos.)* |
| **Alta cohesión** | Cada servicio debe centrarse en una única función o conjunto de funciones relacionadas. *(Es SRP, pero a nivel de servicio en vez de a nivel de clase.)* |
| **Interfaces claras y bien definidas** | Los servicios se comunican mediante interfaces (APIs) que exponen operaciones sin revelar la implementación interna. |
| **Independencia tecnológica** | Los servicios deben poder implementarse con diferentes tecnologías, siempre que cumplan con el contrato de comunicación establecido. |

### Componentes clave

| Componente | Rol |
|---|---|
| **Servicios** | Unidades funcionales autónomas que implementan una lógica de negocio específica. |
| **APIs (Interfaces de Programación de Aplicaciones)** | Puntos de acceso definidos que permiten a otros sistemas interactuar con el servicio. |
| **Mensajería** | Mecanismo de comunicación entre servicios (síncrono o asíncrono), que permite el intercambio de datos. |
| **Registro de Servicios** | Catálogo donde se registran los servicios disponibles, sus ubicaciones y sus contratos de comunicación. |

### Programación Orientada a Objetos vs. Programación Orientada a Servicios

| | POO | SOA |
|---|---|---|
| **Unidad de organización** | Objetos (clases) que encapsulan datos y comportamiento | Servicios que exponen funcionalidad a través de interfaces |
| **Escala** | Dentro de una misma aplicación/proceso | Entre aplicaciones y sistemas distintos, potencialmente distribuidos en red |
| **Comunicación** | Llamadas a métodos directas, en memoria | Mensajes/llamadas de red (HTTP, colas de mensajes) entre servicios independientes |
| **Acoplamiento** | Bajo (idealmente), pero dentro del mismo proceso | Bajo por diseño explícito — los servicios ni siquiera comparten memoria ni proceso |

**🧩 Contexto complementario:** SOA no reemplaza a la POO — un **servicio** individual, internamente, casi siempre está construido usando POO (con sus clases, SOLID, etc.). SOA es un nivel de organización **por encima** de la POO: cómo esas "cajas" completas (servicios) se relacionan entre sí, análogo a cómo las relaciones UML del Día 2 organizan cómo se relacionan las clases dentro de una caja.

### Servicios en ASP.NET Core

En ASP.NET Core, los "servicios" se implementan típicamente como clases registradas en el **contenedor de Inyección de Dependencias** (built-in DI container de .NET — conexión directa con DIP, Tema 4.1), responsables de:
- **Lógica empresarial:** reglas de negocio encapsuladas en clases de servicio.
- **Acceso a datos:** un servicio que abstrae el acceso a la base de datos (típicamente vía un **Repositorio**), para que el resto de la aplicación no dependa directamente de detalles de persistencia (otra vez, DIP en acción).

### Servicio vs. Microservicio

Un **Servicio** (en SOA clásico) puede ser una unidad relativamente grande dentro de una aplicación más amplia. Un **Microservicio** lleva esa idea al extremo: cada uno cubre una función de negocio **muy específica**, se despliega y escala **de forma completamente independiente**, y suele tener su propia base de datos — es el estilo arquitectónico visto en el Tema 3.4, aplicando los mismos principios de SOA de forma más granular y estricta.

### Patrón Singleton aplicado al registro de servicios

**🧩 Contexto complementario:** el "Registro de Servicios" mencionado arriba se implementa habitualmente con el patrón **Singleton** (una única instancia compartida durante toda la vida de la aplicación) — en ASP.NET Core, cuando registrás un servicio con ciclo de vida `Singleton` en el contenedor de DI, se garantiza que exista **una sola instancia** compartida por toda la aplicación, útil precisamente para catálogos/registros centralizados.

### Errores comunes

- Confundir "tener varias clases con interfaces" (POO bien hecha, Día 2) con "tener una arquitectura SOA real" — SOA implica división real de **procesos/despliegues** independientes, no solo buen diseño de clases dentro de un único programa.
- Pensar que Microservicio y Servicio son sinónimos exactos — un microservicio es un caso particular, más granular y estricto, dentro del paraguas general de "orientación a servicios".
- Olvidar que "bajo acoplamiento" en SOA también implica **independencia tecnológica** (un servicio en Python debería poder hablar con uno en C# a través de la interfaz/API, sin problema).

### Relaciones con lo anterior

SOA es la aplicación, a escala de sistema completo, de exactamente los mismos valores que ya aprendiste a nivel de clase: alta cohesión, bajo acoplamiento (Día 1), interfaces bien definidas (Día 2), inversión de dependencias (DIP, Día 4). Es también uno de los **estilos arquitectónicos** presentados en el Tema 3.4.

### Resumen — Conceptos clave

- SOA: servicios independientes, comunicados por interfaces/APIs, bajo acoplamiento, alta cohesión, independencia tecnológica.
- Componentes: Servicios, APIs, Mensajería, Registro de Servicios.
- Servicio ≠ Microservicio (el segundo es más granular y con despliegue/escalado independiente).
- En ASP.NET Core, los servicios se gestionan vía el contenedor de Inyección de Dependencias.

### Ejercicios

1. Explica con tus palabras la diferencia entre "una clase con interfaz bien diseñada" (Día 2) y "un servicio SOA".
2. Diseña (a alto nivel, sin código) cómo dividirías el Sistema del Parque de Diversiones en servicios independientes si tuviera que crecer a nivel empresarial (ej: servicio de venta de manillas, servicio de control de acceso a atracciones, servicio de reportes).
3. ¿Por qué el "Registro de Servicios" suele implementarse como Singleton? ¿Qué pasaría si cada consulta al registro creara una nueva instancia?

### Mini Examen — Tema 4.2

1. Nombra los 4 componentes clave de SOA según el material.
2. V o F: "Todo microservicio es, por definición, un servicio SOA, pero no todo servicio SOA es necesariamente un microservicio." Justifica.

---

## Tema 4.3 — Introducción al Proyecto: Aplicación Web ASP.NET Core MVC

### Explicación

**Intuición.** MVC separa una aplicación web en 3 roles claros, como una obra de teatro: el **Modelo** es el guion (los datos y las reglas de la historia), la **Vista** es el escenario que el público ve (la interfaz), y el **Controlador** es el director que decide qué escena mostrar según lo que pide el público (las acciones del usuario).

### Arquitectura MVC — los 3 componentes

| Componente | Rol |
|---|---|
| **Modelo (Model)** | Representa los datos y la lógica de negocio de la aplicación. |
| **Vista (View)** | Presenta los datos al usuario (interfaz de usuario). |
| **Controlador (Controller)** | Maneja la lógica de la aplicación y actúa como intermediario entre el Modelo y la Vista. |

### El Controlador

Es responsable de recibir las **solicitudes del usuario**, interactuar con el modelo para procesar datos, y seleccionar la vista adecuada para mostrar el resultado.

**Verbos HTTP que maneja un controlador:**

| Verbo | Uso típico |
|---|---|
| `GET` | Obtener/consultar datos |
| `POST` | Crear un nuevo recurso / enviar datos de un formulario |
| `PUT` | Actualizar un recurso completo |
| `PATCH` | Actualizar parcialmente un recurso |
| `DELETE` | Eliminar un recurso |
| `HEAD` | Igual que GET pero sin cuerpo de respuesta (solo headers) |
| `OPTIONS` | Consulta qué métodos/operaciones soporta un recurso |

### Las Vistas y Razor

Las vistas usan la sintaxis **Razor** (`.cshtml`) para combinar HTML con código C# embebido, permitiendo generar contenido dinámico.

**Mecanismos para pasar datos del Controlador a la Vista:**

| Mecanismo | Descripción |
|---|---|
| **Model** | Objeto fuertemente tipado que se pasa explícitamente a la vista — la forma más robusta y recomendada (con *IntelliSense* y chequeo de tipos en tiempo de compilación). |
| **ViewBag** | Propiedad dinámica (`dynamic`) para pasar datos del controlador a la vista sin definir un tipo específico. |
| **ViewData** | Diccionario (`Dictionary<string, object>`) similar a `ViewBag`, pero requiere *casting* explícito al leer los valores. |
| **TempData** | Almacena datos que **persisten entre dos solicitudes consecutivas** (por ejemplo, para pasar un mensaje de confirmación tras una redirección) — a diferencia de `ViewBag`/`ViewData`, que solo viven durante la solicitud actual. |

### Flujo completo de una solicitud en MVC

```
1. El usuario hace una petición HTTP (ej: GET /Atracciones/Detalle/5)
         │
         ▼
2. El enrutador (routing) determina qué Controlador y Acción manejan esa ruta
         │
         ▼
3. El Controlador ejecuta la Acción correspondiente:
   - interactúa con el Modelo (ej: consulta la atracción con id=5)
   - decide qué Vista corresponde
         │
         ▼
4. El Controlador pasa los datos a la Vista (Model / ViewBag / ViewData)
         │
         ▼
5. La Vista (Razor) renderiza el HTML final combinando la plantilla con los datos
         │
         ▼
6. El HTML resultante se envía como respuesta HTTP al navegador del usuario
```

### Errores comunes

- Poner lógica de negocio **dentro** del Controlador (validaciones complejas, cálculos) en vez de delegarla al Modelo o a un Servicio inyectado — viola SRP (el controlador debería **coordinar**, no implementar la lógica de negocio él mismo).
- Confundir `ViewBag` con `ViewData` — funcionalmente similares, pero `ViewBag` usa propiedades dinámicas (sin *casting*) y `ViewData` es un diccionario (requiere *casting* explícito al leer).
- Usar `TempData` para datos que deberían durar toda la sesión — `TempData` solo sobrevive **una** redirección; para datos de sesión completa se necesita `Session`, un mecanismo distinto.
- No usar el verbo HTTP correcto (usar `GET` para operaciones que modifican datos, por ejemplo) — rompe las convenciones REST y puede generar problemas de seguridad (cacheo accidental de operaciones destructivas).

### Relaciones con lo anterior

MVC es uno de los **estilos arquitectónicos** presentados en el Tema 3.4, aplicado de forma concreta con un framework real (.NET). El Controlador típicamente recibe **servicios inyectados** (Tema 4.2 + DIP, Tema 4.1) para delegarles la lógica de negocio real, en vez de implementarla él mismo — todo el curso converge aquí: POO (Modelo son clases bien diseñadas), SOLID (servicios inyectados con DIP, responsabilidades separadas con SRP), SOA (los servicios que el Controlador consume), y Arquitectura (MVC como estilo).

### Resumen — Conceptos clave

- MVC: Modelo (datos/lógica) — Vista (presentación) — Controlador (coordina).
- Verbos HTTP: GET, POST, PUT, PATCH, DELETE, HEAD, OPTIONS.
- Model (tipado) > ViewBag (dinámico) / ViewData (diccionario, requiere *casting*) > TempData (persiste 1 redirección).
- Flujo: petición → routing → controlador → modelo → vista → HTML → respuesta.

### Ejercicios

1. Diseña el flujo MVC completo para "comprar una manilla" en el Sistema del Parque: ¿qué controlador, qué acción, qué verbo HTTP, qué modelo, qué vista?
2. ¿Cuándo usarías `TempData` en vez de `ViewBag` en el flujo de compra de una manilla?
3. ¿Por qué es mejor que el `AtraccionesController` reciba un `IServicioAtracciones` inyectado (DIP) en vez de instanciar la lógica de negocio directamente dentro del controlador?

### Mini Examen — Tema 4.3

1. Ordena correctamente el flujo de una solicitud MVC (dado en desorden): Vista renderiza / Controlador ejecuta acción / Routing determina controlador / Respuesta HTTP / Petición del usuario / Controlador pasa datos a la vista.
2. V o F: "ViewBag y ViewData almacenan exactamente el mismo tipo de dato de la misma forma." Justifica la diferencia técnica exacta.
3. ¿Qué verbo HTTP usarías para eliminar una manilla del sistema, y por qué no `GET`?

---

## Proyecto Integrador — Etapa 4 (Día 4, versión final con SOLID)

**Diseño final del Sistema del Parque de Diversiones, aplicando los 5 principios:**

| Principio | Decisión de diseño aplicada |
|---|---|
| **SRP** | Se separan: `Manilla` (datos + saldo), `Taquilla` (venta), `Atraccion` (registro de uso), `IRepositorioParque` (persistencia — separada de las clases de dominio), `INotificador` (para el evento `SinManillas`, en vez de mezclarlo dentro de `Taquilla`). |
| **OCP** | Los distintos tipos de atracciones (mecánicas, acuáticas, infantiles, con reglas de puntos distintas) se modelan con una interfaz `ITipoAtraccion` — agregar un tipo nuevo no obliga a tocar `Atraccion`. |
| **LSP** | Todas las subclases de `Atraccion` cumplen exactamente el mismo contrato (`RegistrarUso()`, `CalcularPuntos()`) sin lanzar excepciones inesperadas ni reforzar precondiciones — si alguna atracción tuviera una restricción real (ej: altura mínima), se modela con una interfaz adicional `IConRestriccionAltura`, no forzando el método en todas. |
| **ISP** | Se separan `IVendible` (Taquilla), `IRegistrable` (Atraccion), `IConRestriccionAltura` (solo algunas atracciones) — ninguna clase implementa métodos que no necesita. |
| **DIP** | `Taquilla` y `Atraccion` no crean directamente su `IRepositorioParque` ni su `INotificador` — los reciben inyectados por constructor, permitiendo cambiar de base de datos o de mecanismo de notificación (email, SMS, log) sin modificar las clases de dominio. |

```csharp
public interface IRepositorioParque
{
    void GuardarManilla(Manilla m);
    Manilla ObtenerManilla(string id);
}

public interface INotificador
{
    void Notificar(string mensaje);
}

public class Taquilla
{
    private readonly IRepositorioParque repositorio; // DIP
    private readonly INotificador notificador;         // DIP
    public event Action<string> SinManillas;             // SRP: la notificación externa sigue siendo responsabilidad del evento, no de Taquilla

    public Taquilla(IRepositorioParque repositorio, INotificador notificador)
    {
        this.repositorio = repositorio;
        this.notificador = notificador;
        SinManillas += notificador.Notificar; // conecta el evento con la abstracción inyectada
    }
    // ... resto de la lógica de venta
}
```

Este es el diseño que deberías poder **reproducir de memoria conceptual** (no de memoria literal) en el examen: identificar responsabilidades, extraer interfaces donde haya variación real, e inyectar dependencias entre clases de dominio y servicios externos (BD, notificación).

---

## Soluciones — Mini Exámenes Tema 4.2 y 4.3

**Tema 4.2**
1. Servicios, APIs, Mensajería, Registro de Servicios.
2. **Verdadero.** Todo microservicio cumple los principios de SOA (bajo acoplamiento, alta cohesión, interfaces claras) de forma más estricta y granular, pero SOA como concepto general también incluye servicios más grandes que no necesariamente están desplegados y escalados de forma tan independiente como exige un microservicio.

**Tema 4.3**
1. Petición del usuario → Routing determina controlador → Controlador ejecuta acción → Controlador pasa datos a la vista → Vista renderiza → Respuesta HTTP.
2. **Falso.** `ViewBag` es una propiedad dinámica (no requiere *casting* al leer), mientras que `ViewData` es un diccionario `Dictionary<string, object>` que sí requiere *casting* explícito al leer los valores — funcionalmente similares en propósito, distintos en mecanismo.
3. `DELETE`, porque `GET` no debería usarse para operaciones que modifican o eliminan el estado del sistema (rompe la semántica REST y puede ejecutarse accidentalmente por cacheo, prefetching de navegadores, o crawlers).

---

# Examen Final Integrador

*(Simula un examen universitario real: preguntas de análisis, casos de diseño, UML y programación. No mires las soluciones antes de intentarlo — están en la sección final "Soluciones — Examen Final".)*

## Parte 1 — Preguntas conceptuales (V/F y selección múltiple)

1. V o F: "La composición y la agregación se diferencian únicamente en el símbolo del rombo, sin ninguna implicación práctica en el código." 
2. V o F: "Un método `abstract` puede coexistir con una implementación por defecto en la misma declaración."
3. V o F: "OCP y DIP suelen reforzarse mutuamente porque ambos se apoyan en el uso de abstracciones."
4. Selección múltiple: ¿cuál principio SOLID se viola más directamente cuando una clase de alto nivel instancia con `new` una clase de bajo nivel concreta? (a) SRP (b) OCP (c) LSP (d) DIP
5. Selección múltiple: en el modelo 4+1, ¿qué vista describe la concurrencia y comunicación entre procesos en ejecución? (a) Vista Lógica (b) Vista de Desarrollo (c) Vista de Proceso (d) Vista Física

## Parte 2 — Casos de diseño (UML)

6. Diseña el diagrama de clases completo (con relaciones, multiplicidad y visibilidad) para un **Sistema de Biblioteca**: `Biblioteca`, `Libro`, `Ejemplar` (cada libro puede tener varios ejemplares físicos), `Socio`, `Prestamo` (clase de asociación entre `Socio` y `Ejemplar`, con fecha de préstamo y fecha límite).
7. Un sistema de **Hospital** tiene `Hospital`, `Departamento` (composición), `Medico` (agregación con Departamento), `Paciente`, y una interfaz `IAtendible` que implementan tanto `Medico` como `Enfermero` (herencia común desde `PersonalMedico` abstracta). Dibuja el diagrama completo.

## Parte 3 — Casos de diseño (SOLID)

8. Este código de un sistema de **Tienda Virtual** tiene múltiples violaciones a SOLID. Detéctalas todas y refactoriza:
```csharp
public class Pedido
{
    private SqlConnection conn = new SqlConnection("...");

    public void Procesar(string tipoCliente, decimal total)
    {
        decimal descuento = 0;
        if (tipoCliente == "Regular") descuento = 0;
        else if (tipoCliente == "VIP") descuento = total * 0.1m;
        else if (tipoCliente == "Premium") descuento = total * 0.2m;

        decimal totalFinal = total - descuento;

        conn.Open();
        // guarda el pedido directamente aquí

        // envía email de confirmación directamente aquí
        var smtp = new SmtpClient();
        smtp.Send("confirmación de pedido...");
    }
}
```
9. Diseña, desde cero, un mini-sistema de **Videojuego** con `Personaje` (abstracta), al menos 2 tipos concretos con comportamientos distintos vía polimorfismo, una interfaz `IAtacable`, y un servicio `IGuardadoPartida` inyectado por DIP.

## Parte 4 — Programación

10. Implementa en C# la clase `Automovil` (abstracta) con un método abstracto `CalcularMantenimiento()`, dos subclases concretas, y demuestra en un `Main` el uso polimórfico recorriendo una `List<Automovil>`.
11. Implementa un evento `StockAgotado` en una clase `Inventario`, con al menos un suscriptor que reaccione imprimiendo una alerta.
12. Usando LINQ, dada una `List<Manilla>` del proyecto integrador, obtén la manilla con mayor saldo de puntos, y la suma total de puntos de todas las manillas.

## Parte 5 — Arquitectura

13. Explica, en un párrafo, por qué implementar el Sistema del Parque de Diversiones únicamente con buen diseño OO (SOLID) **no es suficiente** para llamarlo "una arquitectura de software" — ¿qué le falta según la definición IEEE 1471-2000 vista en el Tema 3.4?
14. Si el Parque de Diversiones creciera a una cadena con 50 sedes en distintas ciudades, cada una con su propia base de datos e infraestructura, ¿qué estilo arquitectónico (de los vistos en el Tema 3.4) sería más adecuado? Justifica con al menos 3 razones.

---

# Glosario

- **Abstracción:** modelo simplificado de un objeto/fenómeno real, limitado a un contexto, que conserva lo relevante y omite el resto.
- **Acoplamiento:** grado de dependencia entre módulos/clases/servicios (ideal: bajo).
- **Advice (Consejo):** código que ejecuta un aspecto en AOP.
- **Agregación:** relación "Todo-Parte" donde la parte puede existir independientemente del todo (rombo blanco).
- **Aspecto:** funcionalidad transversal extraída del código principal en AOP.
- **Atributo derivado:** atributo cuyo valor depende de otros atributos (notación `/atributo`).
- **Clase:** molde/plantilla que agrupa objetos con atributos, operaciones y semántica comunes.
- **Clase abstracta:** clase no instanciable, diseñada exclusivamente para ser heredada.
- **Clase de asociación:** clase adicional conectada a una asociación cuando la relación misma tiene atributos/métodos propios.
- **Cohesión:** qué tan relacionados están los elementos internos de un módulo/clase (ideal: alta).
- **Composición:** relación "Todo-Parte" donde la parte no tiene sentido de existir sin el todo (rombo negro).
- **Constructor:** método especial, mismo nombre de la clase, sin tipo de retorno, obligatorio para instanciar.
- **DDD (Domain Driven Design):** metodología centrada en modelar profundamente el dominio del negocio.
- **Dependencia:** relación temporal y débil entre clases, típicamente vía parámetro de método.
- **DIP:** Principio de Inversión de Dependencia — alto nivel y bajo nivel dependen de abstracciones.
- **Encapsulamiento:** ocultar el "cómo" (implementación) y exponer solo el "qué" (interfaz pública).
- **Evento:** notificación que un Publisher dispara y a la que uno o varios Subscribers reaccionan.
- **Herencia:** mecanismo para crear nuevas clases (subclases) a partir de clases existentes (superclases); relación "Es-Un(a)".
- **Inmutabilidad:** propiedad de un objeto que no cambia tras su creación; "modificarlo" implica crear una nueva instancia.
- **Instanciación:** proceso de crear un objeto concreto a partir de una clase, con `new`.
- **Interfaz:** contrato de métodos (firmas) sin implementación, que una clase se compromete a cumplir ("Implementa").
- **ISP:** Principio de Segregación de Interfaces — ninguna clase debería implementar métodos que no usa.
- **LSP:** Principio de Sustitución de Liskov — una subclase debe poder sustituir a su superclase sin romper el comportamiento esperado.
- **Modularidad:** dividir la solución en partes que se integran entre sí sin afectar el todo al cambiarlas.
- **Multiplicidad:** cantidad de instancias que participan en un extremo de una relación UML.
- **Navegabilidad:** indica qué clase "conoce"/"ve" a la otra en una relación (se convierte en atributo).
- **OCP:** Principio Abierto/Cerrado — abierto para extensión, cerrado para modificación.
- **Overload (Sobrecarga):** mismo nombre de método/constructor, distinta firma de parámetros, en la misma clase.
- **Override (Sobreescritura):** redefinir en una subclase un método `virtual` o `abstract` de la superclase.
- **Ocultamiento con `new`:** oculta un miembro heredado sin comportamiento polimórfico real (se resuelve por tipo declarado, no real).
- **Pointcut (Punto de corte):** expresión que especifica dónde se aplica un aspecto en AOP.
- **Polimorfismo:** capacidad de detectar la clase real de un objeto y ejecutar su implementación específica.
- **Realización:** relación de una clase que implementa una interfaz (línea punteada, triángulo hueco).
- **Reuso:** aprovechamiento de componentes ya desarrollados.
- **SOA:** Programación/Arquitectura Orientada a Servicios — sistema como conjunto de servicios independientes comunicados por interfaces.
- **SRP:** Principio de Responsabilidad Única — una clase, una sola razón para cambiar.
- **`this`:** referencia al objeto/instancia actual dentro de una clase.
- **Vista arquitectónica:** perspectiva específica sobre una arquitectura (Lógica, Desarrollo, Proceso, Física, Escenarios en el modelo 4+1).

---

# Conceptos Clave (para memorizar antes del examen)

- Alta cohesión + bajo acoplamiento = objetivo de todo buen diseño, a cualquier escala (clase, servicio, arquitectura).
- Composición vs. Agregación: la pregunta decisiva es "¿la parte sobrevive sin el todo, en este sistema?"
- Herencia múltiple entre clases concretas: **no existe en C#**; se simula con interfaces.
- `virtual`/`override` = polimorfismo real (se resuelve por tipo real en tiempo de ejecución); `new` = ocultamiento (se resuelve por tipo declarado — NO es polimorfismo).
- Clase abstracta: una sola por jerarquía, puede tener estado y visibilidad. Interfaz: varias por clase, sin estado, sin visibilidad.
- SOLID no son reglas aisladas — se refuerzan entre sí: SRP facilita OCP; LSP valida que la herencia esté bien hecha; ISP es SRP aplicado a interfaces; DIP y OCP casi siempre van de la mano.
- DIP se implementa técnicamente con Inyección de Dependencias (DI), típicamente por constructor.
- IEEE 1471-2000: arquitectura = organización fundamental + componentes + relaciones + principios de diseño/evolución.
- Modelo 4+1: Lógica, Desarrollo, Proceso, Física, Escenarios.
- MVC: Modelo (datos/lógica) — Vista (presentación) — Controlador (coordina, no implementa lógica de negocio él mismo).
- SOA aplica los mismos valores de POO (cohesión, acoplamiento, interfaces) pero a escala de sistemas completos, no de clases.

---

# Checklist para el Examen

**Antes de entrar al examen, deberías poder responder "sí" a todo esto:**

- [ ] Puedo diferenciar de memoria las 5 relaciones UML y justificar cuál usar con la pregunta "¿puede existir A sin B?".
- [ ] Puedo dibujar la notación UML completa de una clase (visibilidad, atributos, métodos, multiplicidad, navegabilidad).
- [ ] Puedo explicar la diferencia entre `virtual`/`override` y `new`, y predecir la salida de código con ambos.
- [ ] Puedo explicar por qué una interfaz permite "herencia múltiple simulada" y una clase abstracta no.
- [ ] Para cada uno de los 5 principios SOLID, puedo: (a) dar su definición exacta, (b) escribir un ejemplo de violación, (c) refactorizarlo, (d) explicar con qué patrón de diseño se relaciona.
- [ ] Puedo explicar la diferencia entre Inyección de Dependencias (técnica) e Inversión de Dependencia (principio).
- [ ] Puedo nombrar las 5 vistas del modelo 4+1 y qué responde cada una.
- [ ] Puedo comparar al menos 4 estilos arquitectónicos (Monolito, MVC, SOA, Microservicios, Hexagonal, Clean Architecture) con sus ventajas/desventajas.
- [ ] Puedo explicar el flujo completo de una solicitud en ASP.NET Core MVC.
- [ ] Puedo diseñar de memoria (sin copiar) un sistema completo de dominio simple aplicando POO + UML + SOLID en conjunto (como el Sistema del Parque de Diversiones).

---

# Preguntas Frecuentes

**¿Composición o Agregación — cómo estoy 100% seguro en el examen?**
No hay una respuesta universal absoluta: la respuesta correcta depende del **contexto del sistema que estás modelando** (ver "Casos difíciles", Tema 2.1). Lo que el examen evalúa no es que "adivines" la respuesta única, sino que **justifiques tu elección** con la pregunta clave: "¿puede existir la parte sin el todo, en este sistema en particular?"

**¿Por qué C# no permite herencia múltiple si otros lenguajes sí?**
Es una decisión de diseño del lenguaje para evitar el "problema del diamante" (ambigüedad cuando dos clases padre tienen un método con el mismo nombre). Las interfaces resuelven la necesidad de "comportamiento múltiple" sin ese problema, porque no traen implementación consigo (evitan la ambigüedad de estado/implementación).

**¿Un `virtual` sin `override` en ninguna subclase es un error?**
No es un error de compilación, pero es una señal de que quizás no se necesitaba marcarlo `virtual` — si ninguna subclase lo sobrescribe nunca, no hay variación real que justifique el polimorfismo ahí.

**¿SOLID aplica solo a C#/POO, o también a otros paradigmas?**
Los principios (especialmente SRP e ISP) son ideas de diseño que trascienden el lenguaje — pero OCP, LSP y DIP, tal como se formulan clásicamente, están pensados específicamente para diseño orientado a objetos (dependen de herencia, polimorfismo, interfaces).

**¿Cuál es la diferencia real entre Arquitectura Hexagonal y Clean Architecture si ambas "protegen el núcleo de negocio"?**
Comparten la filosofía (dependencias apuntando hacia el núcleo, independencia de infraestructura), pero Hexagonal formaliza esa idea explícitamente como **Puertos** (interfaces) y **Adaptadores** (implementaciones), mientras que Clean Architecture la formaliza como **capas concéntricas** con una **Regla de Dependencia** unidireccional. En la práctica, un buen diseño DIP (Tema 4.1) es el mecanismo técnico común a ambas.

**¿Qué tan literal debo memorizar las 7 reglas de LSP?**
El material las presenta explícitamente como lista formal — es razonable esperar que el examen pida reconocerlas o aplicarlas (no necesariamente citarlas palabra por palabra), así que enfócate en **entender qué protege cada una** más que en memorizar el enunciado exacto.

---

# Errores Comunes (resumen transversal de todo el curso)

1. Confundir Agregación con Composición (el error #1 de todo el curso — repásalo si tenés dudas).
2. Confundir Sobrecarga (Overload, misma clase) con Sobreescritura (Override, jerarquía padre-hijo).
3. Confundir `virtual`/`override` (polimorfismo real) con `new` (ocultamiento, no polimórfico).
4. Pensar que una interfaz es "una clase abstracta con otro nombre" — una clase implementa varias interfaces, pero hereda de una sola clase (abstracta o no).
5. Olvidar que el constructor no tiene tipo de retorno, ni siquiera `void`.
6. Redefinir atributos en subclases (síntoma de una jerarquía de herencia mal diseñada).
7. Violar LSP silenciosamente con métodos sobrescritos vacíos o que lanzan excepciones inesperadas.
8. Crear interfaces "gordas" que fuerzan a implementar métodos irrelevantes (viola ISP), o segregar en exceso sin cohesión real.
9. Instanciar dependencias concretas con `new` dentro de clases de alto nivel en vez de inyectarlas (viola DIP).
10. Confundir "tener varias clases con interfaces" con "tener una arquitectura SOA real" (SOA implica separación real de procesos/despliegues).
11. Pensar que arquitectura monolítica es sinónimo de mal diseño — es válida para muchos contextos; el problema es no controlarla al crecer.
12. Mezclar lógica de negocio dentro del Controlador en ASP.NET MVC en vez de delegarla a servicios inyectados.

---

# Soluciones — Examen Final

*(Revisa esto solo después de intentar resolver el examen completo tú mismo.)*

**Parte 1**
1. **Falso.** Sí hay implicación práctica real: en composición, la parte se crea *dentro* del constructor del todo y su ciclo de vida está atado a él; en agregación, la parte se recibe/asigna desde fuera y sobrevive independientemente — no es solo un cambio visual del rombo.
2. **Falso.** Un método `abstract` nunca tiene cuerpo/implementación en la clase que lo declara — es exactamente lo opuesto a `virtual` (que sí puede traer una implementación por defecto).
3. **Verdadero.** Ambos dependen de que el código dependa de **abstracciones** en lugar de implementaciones concretas: OCP logra extensibilidad vía polimorfismo/abstracción, y DIP invierte la dirección de la dependencia hacia esa misma abstracción.
4. **(d) DIP.**
5. **(c) Vista de Proceso.**

**Parte 2**
6. `Biblioteca (1) ◆— Ejemplar (0..*)` [Composición: el ejemplar físico no existe fuera de la biblioteca que lo tiene]; `Libro (1) — Ejemplar (1..*)` [Asociación: un libro puede tener varios ejemplares]; `Socio (1..*) — Prestamo — Ejemplar (1..*)` con `Prestamo` como **clase de asociación** conteniendo `fechaPrestamo` y `fechaLimite`.
7. `Hospital (1) ◆— Departamento (1..*)` [Composición]; `Departamento (1) —◇— Medico (1..*)` [Agregación]; `PersonalMedico` (abstracta) → `Medico`, `Enfermero` [Herencia]; ambos implementan `IAtendible` [Realización, línea punteada con triángulo hueco].
8. Viola **SRP** (calcula descuento + persiste + notifica en un solo método), **OCP** (el `if/else` de tipo de cliente crece con cada caso nuevo), **DIP** (`SqlConnection` y `SmtpClient` instanciados directamente). Refactor: `IEstrategiaDescuento` (OCP), `IRepositorioPedidos` inyectado (DIP), `IServicioNotificacion` inyectado (DIP), y `Pedido` reducida a solo representar los datos del pedido (SRP), coordinado todo desde un `ProcesadorPedido` que recibe las 3 abstracciones inyectadas.
9. Diseño esperado: `Personaje` abstracta con método abstracto `Atacar()` (o vía interfaz `IAtacable` si el ataque no aplica a todos los personajes del juego, por ejemplo un personaje de soporte que nunca ataca — aplica el razonamiento de LSP del Tema 4.1); subclases concretas (`Guerrero`, `Mago`) sobrescribiendo `Atacar()` con comportamiento distinto (polimorfismo real, Tema 2.3); `IGuardadoPartida` inyectado por constructor en la clase que orquesta el juego, en vez de que cada `Personaje` sepa cómo guardarse a sí mismo en un archivo o base de datos concreta (DIP).
10. Sigue exactamente el patrón mostrado en el Tema 2.2/2.3 (clase abstracta + método abstracto + `override` en subclases + recorrido polimórfico de `List<Automovil>` en el `Main`, como en el ejemplo de `Deportivo`/`Camioneta` del Tema 2.3).
11. Sigue el patrón exacto del Tema 3.2 (`Publisher` = `Inventario` con `event`, `Subscriber` con un método que se suscribe con `+=`, e invocación segura con `?.Invoke(...)`).
12. `manillas.OrderByDescending(m => m.SaldoPuntos).First()` para la de mayor saldo; `manillas.Sum(m => m.SaldoPuntos)` para la suma total — ambos son métodos LINQ de Agregación/Ordenamiento vistos en el Tema 3.1.
13. Le falta la **organización explícita de componentes, relaciones y principios de diseño/evolución documentados** más allá del código mismo — un buen diseño OO/SOLID garantiza que **cada clase** esté bien construida, pero no define por sí solo cómo se despliega el sistema, cómo se comunican sus partes a nivel de infraestructura, ni los principios que guiarán su evolución futura como sistema completo (eso es exactamente lo que agrega la "arquitectura" sobre el "buen código").
14. **Microservicios** (o, como mínimo, un estilo distribuido tipo Cliente-Servidor multinivel): (1) cada sede necesita escalar y desplegarse de forma independiente sin afectar a las demás, (2) cada sede maneja su propia base de datos (dato explícito del enunciado), lo cual encaja naturalmente con el principio de independencia de Microservicios, (3) una falla en la infraestructura de una sede no debería tumbar el sistema completo de las otras 49 sedes — exactamente el beneficio de aislamiento de fallas que ofrece este estilo (Tema 3.4).

---

*Fin del curso. Éxitos en tu examen — tenés 4 días y ahora tenés el mapa completo para recorrerlos en el orden correcto.*
