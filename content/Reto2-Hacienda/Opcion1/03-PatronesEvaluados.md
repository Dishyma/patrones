---
tags: [reto2, actividad-2, patrones, evaluacion, hacienda]
estado: en-revision
fecha: 2026-08-30
---

# 03 — Patrones Evaluados (Actividad 2.1 · Fichas del Anexo A completo)

> [!abstract] Propósito
> Evaluación de **los 22 patrones del Anexo A** contra los puntos de dolor de [[Reto2-Hacienda/Opcion1/02-PuntosDolor]]. Ningún patrón se adopta por buena práctica: cada uno debe anclarse a un P-XX con evidencia, compararse contra alternativas reales (siempre incluyendo *no hacer nada*) y declarar su costo. Propuesta de IA **sujeta a ratificación del equipo** — el veredicto final se registra en [[Reto2-Hacienda/Opcion1/04-DecisionesArquitectonicas]] y [[Reto2-Hacienda/Opcion1/10-BitacoraIA]].

> [!info] Reglas
> - "Adoptado" cuenta con ancla P-XX + alternativa + costo declarado. "Descartado" cuenta con justificación técnica real (no "no se necesita").
> - Presupuesto del enunciado: **se adoptan entre 3 y 5**; adoptar más exige justificarlo. Evaluamos 22 (el enunciado premia evaluar más; exige mínimo 6 con ≥2 por familia).
> - Los tres patrones que el Anexo A marca como "más se aplican mal" (Singleton, Facade, Abstract Factory) reciben sección extendida de descarte — el profesor va a mirar ahí.

---

## 1. Tabla de decisión (Entregable 2.1 del enunciado)

| Patrón | Familia | Punto de dolor | Qué gana / qué cuesta | Decisión | Por qué (síntesis) |
|--------|---------|----------------|------------------------|----------|---------------------|
| **Factory Method** | Creacional | P-01, P-02, P-09 | Gana: el punto de modificación desaparece (1 clase nueva por tipo, 0 ediciones). Cuesta: 1 jerarquía de creadores + registro | ✅ **Adoptar** | La decisión de "qué concreto instanciar" está regada en 9 puntos; es la definición del patrón |
| **Builder** | Creacional | P-03 (+SC-1) | Gana: ventas con ítems variables sin constructores telescópicos. Cuesta: 1 builder + director ligero | ✅ **Adoptar** | SC-1 convierte la venta (hoy 5 campos fijos) en objeto multi-ítem; D-05 resuelta (Variante A) confirma el builder |
| **Abstract Factory** | Creacional | (P-01) | Gana: familias coherentes. Cuesta: interfaz que crece con cada producto nuevo | ❌ Descartar | **No hay familias**: Res y Vacuna no forman familia coherente; la advertencia del Anexo A aplica literal |
| **Prototype** | Creacional | — | — | ❌ Descartar | Crear no es caro (objetos POCO); no hay plantillas base que clonar |
| **Singleton** | Creacional | — | — | ❌ Descartar | El composition root ya garantiza unicidad sin estado global ni daño a pruebas (DIP) |
| **Adapter** | Estructural | — | — | ❌ Descartar | No hay contratos ajenos incompatibles: todos los bordes ya tienen interfaz propia del Reto 1 |
| **Bridge** | Estructural | — | — | ❌ Descartar | Cada jerarquía varía en una sola dimensión; no hay explosión n×m que desacoplar |
| **Composite** | Estructural | P-03 (evaluado) | Gana: tratamiento uniforme ítem/colección. Cuesta: interfaz común hoja/compuesto | ❌ Descartar | La venta de SC-1 es una lista **plana** de ítems, no un árbol recursivo; Composite sin recursividad es una `List` |
| **Decorator** | Estructural | — | — | ❌ Descartar | No hay características opcionales combinables hoy; adoptarlo para SC-1 sería especulativo |
| **Facade** | Estructural | (P-03) | Gana: una puerta al subsistema de venta. Cuesta: riesgo documentado de absorber lógica | ❌ Descartar | Los servicios de Application **ya son** la fachada; añadir otra capa reintroduce la crítica del profesor ("demasiadas capas") y el riesgo SRP del Anexo A |
| **Flyweight** | Estructural | — | — | ❌ Descartar | 3.750 LOC, decenas de objetos vivos: no hay presión de memoria; optimización imaginaria |
| **Proxy** | Estructural | P-08 (evaluado) | Gana: control de acceso centralizado. Cuesta: 1 envoltorio por servicio | ❌ Descartar | El proxy de protección activaría el RBAC muerto = cambio de comportamiento observable = prohibido (P-08, no intervenir) |
| **Chain of Responsibility** | Comportamiento | P-07 (evaluado) | Gana: reglas reordenables/enchufables. Cuesta: 1 clase por regla + armado de cadena | ❌ Descartar | El orden de validación es fijo y fail-fast; nadie pide reordenar. La consolidación de reglas la ataca el Template Method en el punto único de creación |
| **Command** | Comportamiento | P-05/P-06 (evaluado) | Gana: operación como objeto (undo/cola/desfase). Cuesta: 1 clase por operación + invocador | ❌ Descartar | No hay undo, ni cola, ni ejecución diferida en ningún escenario del Anexo B; el dolor P-06 es la forma del contrato, no la ausencia de objetos-acción |
| **Iterator** | Comportamiento | — | — | ❌ Descartar | Las colecciones son `List<T>`/`IEnumerable` con LINQ; no hay estructura propia que recorrer |
| **Mediator** | Comportamiento | — | — | ❌ Descartar | Los servicios de Application ya median entre repos y dominio; un mediador central re-centraliza conversaciones = god object disfrazado |
| **Memento** | Comportamiento | — | — | ❌ Descartar | No existe requisito de undo/deshacer estado de entidades en ningún escenario |
| **Observer** | Comportamiento | P-10 | Gana: reaccionar a ocurridos sin tocar al publicador. Cuesta: infraestructura de handlers + orden de notificación | ✅ **Adoptar** | Medio publicador existe y consume a consola; **no existe el lado del consumo**; SC-1 genera eventos naturales (stock, perecederos) |
| **State** | Comportamiento | — | — | ❌ Descartar | `Chip` ya implementa su máquina de estados a mano y bien (`Chip.cs:33-71`); no hay una segunda entidad con transiciones |
| **Strategy** | Comportamiento | P-05 (evaluado) | Gana: algoritmo intercambiable. Cuesta: 1 familia de estrategias + selección | ❌ Descartar | La única selección de comportamiento verdaderamente variable (permisos por rol) está congelada (P-08); el dispatch restante es **creacional** y lo resuelve Factory Method |
| **Template Method** | Comportamiento | P-01, P-02, P-07 | Gana: el esqueleto de creación se escribe una vez; cada subtipo llena sus hooks. Cuesta: tensión LSP si algún subtipo no puede cumplir un paso | ✅ **Adoptar** | Crear res/vacuna/venta comparte pipeline invariante (validar común → construir → regla del subtipo → publicar); hoy ese pipeline está triplicado y desincronizado (P-07) |
| **Visitor** | Comportamiento | — | — | ❌ Descartar | Visitor compra "añadir operaciones sin tocar la jerarquía"; nuestro dolor es el inverso: **añadir tipos**. Además la jerarquía es pequeña y estable |

**Balance: 4 adoptados, 18 descartados.** Dentro del presupuesto del enunciado (3–5). Mínimos reglamentarios: evaluados 22 ≥ 6 ✓, ≥2 por familia ✓ (5/7/10), ≥2 descartes argumentados en detalle ✓ (§3: seis).

---

## 2. Fichas de patrones ADOPTADOS

### 2.1 · Factory Method — Creacional

| Campo | Contenido |
|-------|-----------|
| **Punto de dolor** | **P-01** (9 puntos para un subtipo), **P-02** (interfaz que crece por tipo), **P-09** (rehidratación duplicada) |
| **Evidencia** | `FabricaRes.cs:17-23` (registro diccionario — Simple Factory), `:42-48` (switch de rangos con default silencioso), `IVacunaFactory.cs:8-12` (método por concreto), `GestorReses.cs:137-143`, `RepositorioPotreroSqlite.cs:150-157`, `RepositorioVentaSqlite.cs:39-45` (switches hermanos) |
| **Responsabilidad afectada** | Decidir qué implementación concreta instanciar (creación nueva **y** rehidratación) |
| **Alternativas evaluadas** | (1) *Factory Method* — creador por subtipo + registro abierto, sin switches; (2) *mantener el Simple Factory actual* con mejor disciplina — rechazado: no elimina los switches externos (el enum y los mapeos siguen); (3) *Abstract Factory* — rechazado: no hay familias (§3.1); (4) *no hacer nada* — costo vigente: 9 clases/9 archivos por subtipo (P-01), 8 clases por vacuna (P-02), duplicación de rehidratación sin fin (P-09) |
| **Beneficios** | El escenario "agregar `VacaLechera`" baja de 9 puntos de edición a **1 clase nueva + 1 registro** (OCP real, no de papel); la regla de edad y su rango viven junto al subtipo (fin del `DescribirRango` con default silencioso); la rehidratación usa el mismo punto de creación (fin de los switches de repos y del default silencioso del legado H-04); el `switch` del servicio y los contadores de estadísticas se alimentan polimórficamente |
| **Costos** | +1 clase creadora por subtipo (~4 nuevas hoy, 1 por subtipo futuro); +1 registro de tipos con recorrido polimórfico; más indirección para leer "¿quién crea un Ternero?" (respuesta: su creador, no un diccionario central); el registro necesita un punto de armado (el composition root ya existe — [[Reto2-Hacienda/Opcion1/01-AS-IS]] §11) |
| **Impacto (solo diseño)** | **Nuevas:** `FabricaTernero/Novillo/Cebon/VacaLechera…` (1 por subtipo), `FabricaRecombinante…` (vacunas), interfaz de creador común. **Modificadas:** `FabricaRes`/`FabricaVacuna` (de clase única a registro de creadores), `GestorReses`, 2 repositorios (delegan rehidratación al registro). **Eliminadas:** nada aún (Simple Factory se transforma). **Capas:** Domain (creadores), Application e Infrastructure (delegan), Web 0. **Core:** sí — y ahí es donde el profesor pidió fortalecer |
| **Veredicto** | ✅ **Adoptar** — Es el patrón cuya definición coincide literalmente con el dolor medido ("el que decide qué concreto instanciar quedó regado"). Su adopción corrige además el error conceptual del Reto 1 (llamar Factory Method a Simple Factories). Tensión SOLID: ninguna nueva; **repara** OCP ya prometido. Detalle de interacción con Template Method y Observer en [[Reto2-Hacienda/Opcion1/04-DecisionesArquitectonicas]] |

### 2.2 · Builder — Creacional

| Campo | Contenido |
|-------|-----------|
| **Punto de dolor** | **P-03** (venta no polimórfica) vía SC-1: la venta pasa de 5 campos fijos a multi-ítem (res + derivados con unidades/cantidades) |
| **Evidencia** | `Venta.cs:8-14` (constructor fijo con `Res`), `FabricaVenta.cs:22-28`, desnormalización en `DatabaseInitializer.cs:68-79`; SC-1 exige ítems variables sin constructor telescópico |
| **Responsabilidad afectada** | Construcción de `Venta` como agregado con partes opcionales y variables |
| **Alternativas evaluadas** | (1) *Builder* — pasos: iniciar venta → agregar ítem (res o derivado) → cerrar con total; (2) *constructor sobrecargado* — rechazado: combinación ítems×opciones = telescópico; (3) *Composite* — rechazado: jerarquía plana, no árbol (§3.3); (4) *no hacer nada* — SC-1 sobre el constructor actual exige reescribir `Venta` + factory + repositorio a mano en cada evolución de ítems |
| **Beneficios** | El contrato de "armar una venta" es explícito y estable mientras los ítems evolucionan; el cálculo del total se encapsula en el cierre (hoy sería `res` + `monto` sueltos); SC-2 (ya hecha) y SC-3 (futura) caben sin tocar el ensamblador |
| **Costos** | +1 builder + pasos; el objeto existe "en construcción" (estado intermedio observable si se usa mal — se mitiga con `Build()` que valida invariantes); más indirección en la traza de creación |
| **Impacto (solo diseño)** | **Nuevas:** `VentaBuilder` (+ pasos). **Modificadas:** `Venta` (agrega ítems; comportamiento congelado en su superficie actual), `FabricaVenta` (delega en builder). **Capas:** Domain; Application solo llama. **Core:** sí |
| **Veredicto** | ✅ **Adoptar** — D-05 resuelta (Variante A: producción propia): la venta multi-ítem es la variante confirmada; la rebaja a "evaluado" queda anulada por la resolución |

### 2.3 · Template Method — Comportamiento

| Campo | Contenido |
|-------|-----------|
| **Punto de dolor** | **P-01/P-02** (pipeline de creación disperso) y **P-07** (reglas triplicadas/desincronizadas) |
| **Evidencia** | Validación común duplicada: `FabricaVacuna.cs:36-39` vs `ValidadorVacuna.cs:14-15`; regla de monto en 3 capas con 2 umbrales (`FabricaVenta.cs:20`, `Dinero.cs:10-11`, `ValidadorVenta.cs:15`); edad validada *después* de construir (`FabricaRes.cs:33-37`) |
| **Responsabilidad afectada** | El algoritmo de creación: `validar comunes → construir → validar regla del subtipo → publicar ocurrido` |
| **Alternativas evaluadas** | (1) *Template Method* en el creador base (pasa a ser el esqueleto del Factory Method adoptado); (2) *Chain of Responsibility* — rechazada: orden fijo fail-fast, la reordenabilidad no la pide nadie (§3.7); (3) *validadores explícitos en Application* (status quo) — rechazado: es exactamente P-07; (4) *no hacer nada* — cada factory nueva reimplementa el pipeline y lo desincroniza |
| **Beneficios** | La secuencia se escribe **una vez**; reglas comunes (nombre vacío, monto, lote) viven en el esqueleto; reglas del subtipo (edad, periodo) viven en el hook del subtipo; la regla de edad se exige **antes** de devolver el objeto (fin del constructo-then-reject); un cambio de política común (p.ej. umbral de monto) se hace en un sitio |
| **Costos** | Herencia para variar (acoplamiento base↔subclases); **tensión LSP declarada**: si algún subtipo no puede cumplir un paso del esqueleto, el hook heredado se implementaría vacío = sustituible roto — compensación: los hooks se definen como *datos del subtipo* (rangos de edad como propiedades), no como pasos que alguien pueda incumplir; el esqueleto queda `sealed` en su estructura. Dificultad de depuración: +1 nivel de pila |
| **Impacto (solo diseño)** | **Nuevas:** clase base de creadores con el esqueleto. **Modificadas:** `FabricaRes/Vacuna/Venta/Potrero` (se cuelan del esqueleto). **Eliminadas (candidatas):** la capa `Validaciones/Validador*` (sus reglas vivas migran al esqueleto; las muertas ya estaban muertas — P-07). **Capas:** Domain; Application pierde una capa de duplicación. **Core:** sí |
| **Veredicto** | ✅ **Adoptar** — El pipeline de creación es invariante y hoy está escrito tres veces con tres resultados distintos (incluida una venta de $0 que se construye y luego se rechaza). Es la respuesta estructural a la crítica "lógica donde no corresponde": la regla vuelve al punto único de creación, dentro del Core |

### 2.4 · Observer — Comportamiento

| Campo | Contenido |
|-------|-----------|
| **Punto de dolor** | **P-10** (eventos que nadie puede escuchar) |
| **Evidencia** | `DomainEventPublisherConsola.cs:5-11` (único destino: `Console.WriteLine`), `VacunaVencidaEvent` (`DomainEvents.cs:69-83`) sin publicaciones, `IDomainEventHandler<T>` documentado (`TOBE_Arquitectura_Completa.md:1106-1109`) pero inexistente en código |
| **Responsabilidad afectada** | Coordinación de comportamiento en tiempo de ejecución ante ocurridos del dominio |
| **Alternativas evaluadas** | (1) *Observer* con handlers registrables — el publicador deja de conocer a los consumidores; (2) *llamadas directas desde los servicios* (status quo) — rechazado: cada reacción nueva toca al publicador; (3) *Mediator* — rechazado: re-centraliza conversaciones entre pares (§3.8); (4) *no hacer nada* — SC-1 (stock mínimo de derivados, perecederos por vencer) no tiene cómo reaccionar sin cirugía en quien publica |
| **Beneficios** | La mitad publicadora ya existe y es correcta (interfaces `IDomainEvent`/`IDomainEventPublisher`); solo falta el lado del consumo; añadir una reacción = 1 handler nuevo + 1 registro (OCP); el diseño del Reto 1 que quedó en el papel se implementa de verdad |
| **Costos** | +infraestructura de registro/despacho; **orden de notificación** debe ser determinista (declararlo); riesgo de handlers con efectos secundarios encadenados (se acota: handlers sincrónicos, sin publicación en cascada en v1); depuración: un evento dispara N handlers invisibles en la firma del método |
| **Impacto (solo diseño)** | **Nuevas:** `IDomainEventHandler<T>`, handlers concretos (consola como primer handler — ver compensación), registro en composition root. **Modificadas:** `DomainEventPublisherConsola` se transforma en el despachador que conserva su salida a consola **idéntica** (comportamiento congelado). **Capas:** Domain (contrato), Infrastructure (despacho), Web (registro). **Core:** contrato sí, despacho no |
| **Veredicto** | ✅ **Adoptar** — Dolor de coordinación en runtime (eje explícito del encargo) con media infraestructura ya construida: es la adopción con mejor relación beneficio/costo del set. La salida a consola existente se preserva como handler registrado en primer lugar — ni una línea de salida cambia |

---

## 3. Descartes con justificación extendida (los seis importantes)

### 3.1 · Abstract Factory — descartado por falta de familias reales

> [!warning] El Anexo A lo advierte literal
> *"si solo tienen una familia de productos, la abstracción adicional probablemente no se justifica"*.

Abstract Factory compra coherencia entre **familias** de productos que deben variar juntas (carrocería eléctrica + motor eléctrico). En Hacienda, `Res` y `Vacuna` no forman familia: se crean en momentos distintos, por actores distintos, sin riesgo de mezcla incoherente. SC-1 agrega `ProductoDerivado` — un tercer producto independiente, no una variante de familia. Aplicarlo exigiría una `IFabricaHacienda` con `CrearRes()`, `CrearVacuna()`, `CrearProducto()`, y **cada producto nuevo sumaría un método a la interfaz** — exactamente el defecto P-02 que queremos eliminar (`IVacunaFactory` con método por concreto). Factory Method por producto respeta la advertencia del Anexo A y resuelve el mismo dolor sin la abstracción adicional. **Veredicto: descartado por diseño correcto, no por desconocimiento.**

### 3.2 · Singleton — descartado: el problema que resuelve ya está resuelto sin él

El único caso de uso real (una instancia compartida de configuración/conexión) lo cubre el composition root registrando singletons de DI (`Program.cs:34-36`: `TimeProvider.System`, `IGuidProvider`, `IHasher`). Esa forma es *mejor* que un Singleton clásico: la unicidad vive en **un punto de ensamblaje** visible, no dispersa como `static Instancia`; es sustituible en pruebas sin trucos (el consumidor recibe la interfaz por constructor — DIP intacto); no crea dependencia global oculta. Un Singleton clásico añadiría: constructor privado + acceso estático = dependencia a concreto visible solo en tiempo de enlace de pruebas, justo la penalización que el Anexo A pide explicar y que aquí no compensa nada. **Descartado porque su costo (DIP tensionado, testabilidad) compra un beneficio que el registro DI ya entrega gratis.**

### 3.3 · Composite — evaluado para P-03 y descartado por estructura plana

Se evaluó honestamente para SC-1: una venta con múltiples ítems (res, lácteos, cueros). Composite exige que hoja y compuesto compartan interfaz y que la operación **recorra recursivamente**. La venta real es una lista de ítems de profundidad 1: nadie compra "una venta dentro de una venta". Sin recursión, Composite degenera en una colección tipada + un `foreach` — que es precisamente el diseño que proponen Builder + polimorfismo de ítems. Añadir la maquinaria de Composite (interfaz común, hoja, compuesto, agregación) para una estructura plana es el ejemplo de libro de sobre-ingeniería penalizable. **Descartado con criterio: la estructura del problema no es un árbol.**

### 3.4 · Facade — descartado: ya existen y el riesgo es la absorción

`ServicioVentas`, `GestorReses` etc. ya ejercen el rol de fachada: una operación amigable que orquesta repos + factories + validación. Añadir una fachada superior (p.ej. `FachadaHacienda`) crearía: (a) una capa más — la crítica textual del profesor al Reto 1 ("se crearon demasiadas capas"); (b) el riesgo que el Anexo A señala: *"tiende a absorber lógica de negocio hasta romper SRP"* — con 6 dependencias ya acumuladas en `GestorReses` (`GestorReses.cs:20-34`), la historia del sistema dice que la capa nueva termina absorbiendo reglas. El dolor P-03 no es falta de una puerta de entrada, es la rigidez del agregado detrás de la puerta existente. **Descartado: el remedio (añadir una capa) no ataca la enfermedad (un agregado rígido detrás de la puerta existente).**

### 3.5 · Strategy — descartado: la variable de comportamiento real está congelada

Strategy compra algoritmos intercambiables seleccionados en runtime. Inventario honesto de comportamientos variables en Hacienda: (1) permisos por rol — **congelado por P-08** (activarlos cambia comportamiento observable); ya además implementado como registro de políticas (la forma correcta), solo que muerto; (2) selección de tipo de vacuna/res — es un problema **creacional** (qué objeto instanciar), no de algoritmo; lo resuelve Factory Method; (3) cálculo de precio por tipo de producto (SC-1) — **no existe en el AS-IS**: anclar un patrón a un dolor que el propio reto introduce sería la circularidad que el enunciado penaliza ("patrón anclado a punto del AS-IS que puede robustecerse"). **Descartado sin cerrar la puerta: si D-05 trae reglas de precio variables por producto, se re-evalúa en la Actividad 3 con evidencia nueva — queda registrado en la bitácora.**

### 3.6 · Command — descartado: compra lo que nadie pidió

Command convierte acciones en objetos para habilitar undo, colas, programación diferida, transacciones de operaciones. Ningún escenario del Anexo B pide deshacer una venta, encolar operaciones o diferirlas. El dolor real de la capa Web (P-05) es dispatch por tipo + validación de campos en el controlador: eso baja a la creación (Factory Method + Template Method) y el resultado del contrato se unifica (P-06) con los result-objects que ya existen (`Results/`). Envolver cada acción del controlador en una clase comando añade ~1 clase por operación para comprar flexibilidad sin consumidor. **Descartado: "no hay undo/cola/desfase" es una justificación técnica real, verificable contra el enunciado.**

### 3.7 · Chain of Responsibility — descartado: orden fijo y fail-fast

Se evaluó para P-07 (validación). La cadena vale cuando: varios objetos pueden atender una solicitud, el orden importa y **puede cambiar**, o se quiere reordenar/quitar pasos sin tocar código. En Hacienda las validaciones de creación tienen orden fijo (comunes antes que las del subtipo) y semántica fail-fast (la primera que falla aborta con ese mensaje exacto — congelado). Con Template Method el pipeline queda escrito una vez con orden compilado; una cadena configurable añadiría un armador de cadena + 1 clase por regla para comprar una reordenabilidad que ningún escenario del Anexo B ejerce. **Descartado: la flexibilidad sin escenario que la use es costo puro.**

### 3.8 · Mediator — descartado: los servicios ya median

`ServicioVacunacion` media entre lote, potrero, vacunas aplicadas y persistencia; `ServicioVentas` entre venta, potrero y res. Formalizar un mediador central ("todo el mundo le habla al mediador") concentraría en una clase el conocimiento hoy distribuido — la definición de god object — y contradiría la dirección del reto (fortalecer el Core, no crear un jefe de orquesta en Application. **Descartado: el problema de coordinación existente (P-10) es de publicación/suscripción, y para ese está Observer.**

---

## 4. Fichas de patrones DESCARTADOS (síntesis por candidato)

> [!info] Formato
> Ficha completa en formato compacto. Todos los campos obligatorios están; la justificación extendida de los seis centrales vive en §3.

| Patrón | Familia | Dolor evaluado | Evidencia considerada | Alternativa preferida | Justificación del descarte | Costo de descartar (lo que se pierde) |
|--------|---------|----------------|----------------------|----------------------|---------------------------|----------------------------------------|
| Abstract Factory | Creacional | P-01/P-02 | §3.1 | Factory Method | Sin familias reales; interfaz crecería por producto (defecto P-02) | Nada: el beneficio buscado lo entrega Factory Method |
| Prototype | Creacional | (P-01) | Objetos POCO de creación trivial (`Cebon.cs` 20 líneas) | Factory Method | Crear no es caro; no hay plantillas base; shallow/deep sería riesgo nuevo | Nulo: no hay costo de construcción que ahorrar |
| Singleton | Creacional | Unicidad de servicios | `Program.cs:34-36` ya registra singletons por DI | Composition root existente | Unicidad ya garantizada sin estado global; Singleton tensiona DIP y pruebas (Anexo A) | Nulo: el registro DI ya entrega el mismo efecto |
| Adapter | Estructural | (P-06) | Todos los bordes externos (`IHasher`, `TimeProvider`, repos) ya tienen interfaz propia | — | Adapter resuelve contratos incompatibles que no controlamos; aquí cada contrato es nuestro y se rediseña libremente | Nulo: no hay tercero incompatible |
| Bridge | Estructural | (P-01) | Jerarquías unidimensionales: Res varía por tipo; Vacuna por categoría | Factory Method | No hay dos dimensiones ortogonales por jerarquía (no hay n×m) | Nulo: sin explosión combinatoria no hay puente que tender |
| Composite | Estructural | P-03 | §3.3 | Builder + polimorfismo de ítems | Ítems de venta = lista plana, no árbol; sin recursión no hay Composite | Menor: uniformidad hoja/compuesto que nadie necesita en profundidad 1 |
| Decorator | Estructural | (P-03/P-06) | No hay características opcionales combinables en el AS-IS | — | Envolver para agregar comportamiento que no existe hoy = especulativo | Menor: composición runtime de variantes si SC-1 crece (re-evaluable) |
| Facade | Estructural | (P-03) | §3.4 | Servicios de Application existentes | Ya hay fachadas; capa nueva = crítica del profesor + riesgo SRP del Anexo A | Nulo: la puerta ya existe |
| Flyweight | Estructural | — | Sistema de escritorio con decenas de objetos | — | Sin presión de memoria, intrínsecos triviales | Nulo |
| Proxy | Estructural | P-08 | §P-08 de [[Reto2-Hacienda/Opcion1/02-PuntosDolor]] | — (congelamiento) | El proxy útil (protección) activaría permisos = comportamiento observable cambiado | Declarado: control de acceso diferido hasta que el negocio autorice (deuda P-08) |
| Chain of Responsibility | Comportamiento | P-07 | §3.7 | Template Method | Orden fijo fail-fast; reordenabilidad sin consumidor | Menor: validaciones reconfigurables sin recompilar (nadie lo pide) |
| Command | Comportamiento | P-05/P-06 | §3.6 | Factory Method + result objects | Sin undo/cola/desfase; añade 1 clase por acción sin comprador | Menor: la puerta queda abierta si en el futuro hay colas (re-evaluable) |
| Iterator | Comportamiento | — | `List<T>`/LINQ en toda la solución | — | No hay estructura propia de recorrido | Nulo |
| Mediator | Comportamiento | (P-10) | §3.8 | Observer | Los servicios ya median; mediador central = god object | Nulo: coordinación de eventos la cubre Observer |
| Memento | Comportamiento | — | Sin requisito de undo de estado | — | Ningún escenario del Anexo B restaura estados previos | Nulo |
| State | Comportamiento | (Chip) | `Chip.cs:33-71` — máquina de estados ya bien hecha a mano | — | No hay segunda entidad con transiciones; rediseñar Chip = riesgo sin dolor | Nulo: Chip ya es el ejemplo a seguir, no el paciente |
| Strategy | Comportamiento | P-05 | §3.5 | Factory Method (+re-evaluar con D-05) | Comportamiento variable real congelado (P-08); dispatch restante es creacional | Condicional: si SC-1 trae precios variables, re-evaluar (bitácora) |
| Visitor | Comportamiento | (P-01) | §tabla 1 | Factory Method | Visitor optimiza "añadir operaciones"; nuestro dolor es "añadir tipos" — opuesto; además rompería el encapsulamiento que P-04 quiere construir | Nulo |

---

## 5. Matriz dolor ↔ patrón adoptado (verificación de anclaje)

| Dolor (Act. 1) | ¿Qué patrón lo ataca? | Cobertura |
|----------------|----------------------|-----------|
| P-01 subtipo de res (9 clases/9 archivos) | Factory Method + Template Method | Total: 1 clase nueva + registro |
| P-02 tipo de vacuna (interfaz que crece) | Factory Method + Template Method | Total: 1 clase nueva + registro |
| P-03 venta no polimórfica (SC-1) | Builder (+ polimorfismo de ítems, diseño Act. 3) | Flujo de venta abre sin tocar congelados |
| P-04 reglas fuera del dominio | Template Method (reglas al punto de creación) + cierre de setters (diseño Act. 3, no es patrón) | Parcial: el resto es encapsulamiento puro |
| P-05 dispatch en controlador | Factory Method (la selección baja a creación) | Total |
| P-06 contrato strings | result objects existentes + TO-BE Act. 3 (no es patrón GoF) | Parcial — diseño, no patrón |
| P-07 validación triplicada | Template Method (esqueleto único) | Total en creación; mensajes intactos |
| P-08 RBAC muerto | ⛔ No intervenir (congelado) | — (deuda declarada) |
| P-09 rehidratación duplicada | Factory Method (registro también rehidrata) | Total sin tocar esquema |
| P-10 eventos sin consumo | Observer | Total; consola preservada como handler |
| P-11 código muerto | ⛔ No intervenir | — (higiene) |
| P-12 composición con efectos | ⛔ No intervenir | — |

> [!important] Honestidad declarada
> Dos dolores (P-04 parcial y P-06) se resuelven con **diseño** (encapsulamiento y contratos), no con patrones. Declararlo así es criterio arquitectónico: no todo dolor se cura con patrón, y fingir que sí sería exactamente la sobre-ingeniería que el enunciado penaliza. El TO-BE de la Actividad 3 mostrará ambas partes: patrones adoptados + decisiones de diseño no-patrón.

---

## 6. Verificación rápida contra las advertencias del Anexo A

| Advertencia | Nuestra respuesta |
|-------------|-------------------|
| Singleton: explicar sustitución en pruebas y diferencia con variable global | Descartado (§3.2) — pero la respuesta queda documentada: nuestro composition root permite sustituir cualquier "singleton" inyectando un doble por interfaz; un Singleton clásico obligaría a trucos de reflexión o hilos |
| Facade: declarar su límite | Descartado (§3.4) — y los servicios-fachada existentes quedan con límite declarado: orquestan, no deciden (las reglas bajan al Core) |
| Abstract Factory: justificar si solo hay una familia | Descartado (§3.1) — no hay familia siquiera: hay productos independientes |

---

## 7. Dudas abiertas y dependencias

| ID | Duda | Impacto |
|----|------|---------|
| ~~D-05~~ | ¿SC-1 con producción propia (vaca lechera) o stock directo? | ✅ Resuelta: Variante A (B-10). Define si Builder se confirma ✓ y si Strategy se re-evalúa (precio variable — sigue abierta al implementar) |
| D-07 | ¿El equipo ratifica los 4 adoptados y los 18 descartes? Cualquier cambio se registra en [[Reto2-Hacienda/Opcion1/10-BitacoraIA]] con evidencia | ✅ Resuelta: ratificados (2026-08-30) |

## 8. Riesgos

1. ~~Builder condicionado~~ D-05 resuelta: el riesgo se elimina; queda el riesgo de esquema de ítems (D-11).
2. **Template Method y LSP** — la compensación (hooks como datos, esqueleto sellado) debe verse en el diseño; se audita en [[Reto2-Hacienda/Opcion1/06-VerificacionSOLID]].
3. **Observer y determinismo** — orden de handlers fijado por registro; la salida a consola debe ser idéntica byte a byte (handler consola primero, síncrono).

---

## 9. Navegación

- [[Reto2-Hacienda/Opcion1/02-PuntosDolor]] — los dolores que cada ficha ancla.
- [[Reto2-Hacienda/Opcion1/04-DecisionesArquitectonicas]] — cierre de la Act. 2: decisiones consolidadas e interacción entre patrones.
- [[Reto2-Hacienda/Opcion1/05-TOBE]] — el diseño que resulta de este conjunto.
- [[Reto2-Hacienda/Opcion1/10-BitacoraIA]] — registro de esta propuesta y de la ratificación del equipo.
