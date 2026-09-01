---
tags: [reto2, actividad-1, puntos-dolor, hacienda]
estado: en-revision
fecha: 2026-08-30
---

# 02 — Puntos de Dolor de la Arquitectura Actual (Actividad 1)

> [!abstract] Propósito
> Inventario de los puntos donde el diseño, **aun siendo correcto**, sigue siendo un punto de dolor: rígido o caro de cambiar. Cada punto se mide con un **escenario de cambio concreto** y su costo en **clases y archivos reales** que hay que abrir/modificar. Base de [[Reto2-Hacienda/Opcion1/03-PatronesEvaluados]] y [[Reto2-Hacienda/Opcion1/05-TOBE]].

> [!info] Reglas de este documento
> 1. **El síntoma se describe como dolor, no como principio violado.** El principio (SOLID) es consecuencia que se analiza en [[Reto2-Hacienda/Opcion1/06-VerificacionSOLID]], no evidencia.
> 2. **Todo costo es un conteo** (clases y archivos), nunca "alto/medio/bajo" (requisito del enunciado).
> 3. Las vistas cuentan como **costo** (hay que abrirlas y modificarlas) aunque estén **excluidas como objetivo de intervención** (decisión del equipo, [[00-Plan]] §2.2).
> 4. El punto de referencia de cambio es la solicitud **SC-1 (productos derivados)** elegida por el equipo, más los escenarios de cambio previsibles del negocio.

---

## 0. Protocolo de origen (requisito del enunciado)

El enunciado exige: *mínimo cinco puntos, al menos tres encontrados sin IA*. Este documento presenta **12 puntos propuestos con asistencia de IA, cada uno con evidencia verificable**. La consolidación en grupo debe:

- [ ] Mateo: releer por su cuenta y contrastar contra P-01…P-12; aportar hallazgos propios (anotar origen **"equipo"**).
- [ ] María Alejandra: ídem, foco en dependencias y composición.
- [ ] David: ídem, foco en comportamiento observable y flujos de ejecución.
- [ ] Fusionar en este archivo los hallazgos propios (nuevos P-XX o confirmas) y ajustar la columna **Origen**.

| Convención de origen | Significado |
|----------------------|-------------|
| `IA` | Detectado con asistencia (evidencia adjunta) |
| `equipo` | Detectado por un integrante sin IA |
| `IA+equipo` | Detectado por ambos independientemente (máxima confianza) |

---

## 1. Tabla resumen

| ID | Dónde (archivo · clase) | Escenario de cambio que lo hace doler | Costo hoy (clases / archivos) | Prioridad | ¿Intervenir? | Origen |
|----|--------------------------|----------------------------------------|-------------------------------|-----------|--------------|--------|
| **P-01** | `Factories/FabricaRes.cs` y cadena de tipos | Agregar un subtipo de res (p.ej. `VacaLechera` para SC-1) | 9 puntos / 8 archivos (7 mod + 1 nueva) | 🔴 Alta | ✅ Sí | IA |
| **P-02** | `Factories/IVacunaFactory.cs` | Agregar un tipo de vacuna | 8 clases (7 mod + 1 nueva) / 8 archivos | 🔴 Alta | ✅ Sí | IA |
| **P-03** | `Entities/Venta.cs` + `RepositorioVentaSqlite.cs` | Vender algo que no sea una res (SC-1) | 8 clases / 8 archivos backend | 🔴 Alta | ✅ Sí | IA |
| **P-04** | `Entities/Res.cs` + servicios | Cambiar o añadir una regla de negocio del ganado | 13 reglas dispersas en 8 archivos; bypass posible sin pasar por ninguna | 🔴 Alta | ✅ Sí | IA |
| **P-05** | `Web/Controllers/VacunaController.cs` | Agregar una operación que maneje dos tipos | 2 bloques `if/else` por tipo duplicados en 1 archivo; crece con cada tipo | 🟡 Media | ✅ Sí | IA |
| **P-06** | 8 interfaces de servicios + 3 controladores | Cambiar un mensaje de resultado o agregar una operación | 11 clases / 11 archivos coordinados a mano | 🟡 Media | ✅ Sí (parcial) | IA |
| **P-07** | `FabricaVenta` · `Dinero` · `ValidadorVenta` | Cambiar la regla de monto de venta | 3 clases / 3 archivos con 2 umbrales y 2 contratos de error distintos | 🟡 Media | ✅ Sí | IA |
| **P-08** | `Application/Services/AutorizadorRbca.cs` + políticas | Activar permisos diferenciados por rol | 12+ clases / 11 archivos **y cambia comportamiento observable** | 🟡 Media | ⛔ **No** | IA |
| **P-09** | `RepositorioPotreroSqlite` · `RepositorioVentaSqlite` · `RepositorioResSqlite` | Reconstruir objetos desde persistencia al agregar subtipo / confiar en identidad | 3 clases / 3 archivos con mapeos duplicados; identidad inestable en 1 | 🟡 Media | ✅ Sí (parcial) | IA |
| **P-10** | `Events/DomainEvents.cs` + `DomainEventPublisherConsola` | Reaccionar a un evento de dominio (ej. stock de derivado agotado) | Infraestructura de consumo inexistente: 1 publicador a consola, 0 handlers | 🟡 Media | ✅ Sí | IA |
| **P-11** | Código muerto (§ficha) | Mantener el código legible | 10+ elementos muertos en 9 archivos | 🟢 Baja | ⛔ **No** (higiene) | IA |
| **P-12** | `Web/Program.cs` (inicialización en registro) | Cambiar el orden de arranque | 1 clase / 1 archivo | 🟢 Baja | ⛔ **No** | IA |

**Criterio de priorización explícito** (requisito de la rúbrica): `Prioridad = (frecuencia esperada del escenario de cambio) × (costo por ocurrencia en clases/archivos) × (anclaje a SC-1)`. Alta = escenario probable en este reto o el próximo, costo ≥ 8 clases o riesgo de bypass de reglas. Media = escenario plausible, costo 3–8 clases. Baja = escenario improbable o remedio sin beneficio de robustez.

---

## 2. Fichas de puntos de dolor

### P-01 · Agregar un subtipo de res obliga a tocar 4 capas

| Campo | Contenido |
|-------|-----------|
| Archivo | `SOLID/Hacienda.Domain/Enums/TipoRes.cs` (epicentro), cadena completa abajo |
| Clase | `FabricaRes` (+ enum + mapeos) |
| Método | `FabricaRes.Crear`, `FabricaRes.DescribirRango`, `GestorReses.MapearTipoRes`, `RepositorioPotreroSqlite.MapearRes`, `RepositorioVentaSqlite.ObtenerTodas` |
| Responsabilidad | Decidir qué subtipo de `Res` instanciar y cómo presentarlo/rehidratarlo |
| Síntoma | La decisión de "qué implementación concreta" está **regada en 9 puntos**; el diccionario de `FabricaRes` da la ilusión de un solo punto de cambio, pero enum, switch de rangos, mapeos de servicio, mapeos de 2 repositorios, contadores de estadísticas y badges de vistas quedan fuera de él |
| Evidencia | Enum cerrado `TipoRes.cs`; diccionario `FabricaRes.cs:17-23`; default silencioso `_ => "desconocido"` en `DescribirRango` (`FabricaRes.cs:42-48`); switch `GestorReses.cs:137-143`; contadores hardcodeados `GestorReses.cs:130-132`; switch `RepositorioPotreroSqlite.cs:150-157`; switch `RepositorioVentaSqlite.cs:39-45` (heredado por `RepositorioResSqlite.cs:29,96`); badges `Views/Res/Index.cshtml:71-77` y `Views/Venta/Index.cshtml:106` |
| Costo real | **9 puntos de modificación en 8 archivos (7 modificados + 1 nuevo), 4 capas** — Domain 3, Application 2, Infrastructure 2, Web 2 (vistas: costo declarado, intervención excluida) |
| Escenario ancla | SC-1: la leche exige vacas lecheras → `TipoRes.VacaLechera` pasa de hipótesis a requisito |
| Prioridad | Alta |
| ¿Intervenir? | **Sí** — es el dolor con mayor costo medido y ancla directa de SC-1 |

> [!note] Dato demoledor para la sustentación
> La documentación del Reto 1 promete "1 clase + 1 entrada, 0 modificaciones" (`02-diseno/TOBE_Arquitextura_Completa.md:938` — ver `TOBE_Arquitectura_Completa.md:938`). La medición da 9/9. Este delta promesa/realidad es el argumento central de por qué el TO-BE debe **eliminar** el punto de modificación, no moverlo.

---

### P-02 · Agregar un tipo de vacuna obliga a modificar una interfaz "estable"

| Campo | Contenido |
|-------|-----------|
| Archivo | `SOLID/Hacienda.Domain/Factories/IVacunaFactory.cs` |
| Clase | `IVacunaFactory`, `FabricaVacuna` |
| Método | Interfaz con un método por tipo concreto: `CrearBacteriana(...)`, `CrearViva(...)` (`IVacunaFactory.cs:8-12`) |
| Responsabilidad | Creación de vacunas |
| Síntoma | La abstracción que debía proteger a los consumidores **filtra los tipos concretos**: cada tipo nuevo = método nuevo en la interfaz → cadena obligada de cambios en fábrica, servicio, controlador y repositorio. Además `Viva.GradoAtenuacion` (miembro de un subtipo concreto) se filtra a los contratos de Application |
| Evidencia | `IVacunaFactory.cs:8-12`; `FabricaVacuna.cs:16-33`; dispatch por string en `VacunaController.cs:49-68 y 101-120`; mapeo `is Bacteriana`/`is Viva` en `RepositorioVacunaSqlite.cs:103-115 y 126-163`; `ServicioVacunacion.cs:40,54,76,105` (un método de servicio por tipo) |
| Costo real | **8 clases (7 modificadas + 1 nueva) / 8 archivos**: `IVacunaFactory`, `FabricaVacuna`, nueva `Recombinante`, `IServicioVacunacion`, `ServicioVacunacion`, `VacunaController`, `RepositorioVacunaSqlite`, `DatabaseInitializer` (columnas TPH) |
| Prioridad | Alta |
| ¿Intervenir? | **Sí** — misma enfermedad de raíz que P-01 (creación decidida por tipo concreto), segunda instancia independiente; corregirla junto con P-01 demuestra comprensión del mecanismo, no del caso suelto |

> [!warning] Conexión con la propia historia del equipo
> El equipo documentó este exacto antipatrón en el código legado como H-21/H-22 (`01-diagnostico/Inventario_Hallazgos.md:32`): *"agregar un tipo de vacuna obliga a modificar una interfaz que debería mantenerse estable"*. Se lo reimportaron al rediseñar. Citarlo en la sustentación muestra madurez: el dolor no es nuevo, es heredado del diagnóstico propio.

---

### P-03 · La venta solo sabe vender reses (ancla directa de SC-1)

| Campo | Contenido |
|-------|-----------|
| Archivo | `SOLID/Hacienda.Domain/Entities/Venta.cs` · `SOLID/Hacienda.Infrastructure/Persistence/Sqlite/RepositorioVentaSqlite.cs` |
| Clase | `Venta`, `RepositorioVentaSqlite`, `FabricaVenta` |
| Método | `Venta` (agregado), `RepositorioVentaSqlite.ObtenerTodas`, `InsertVenta` |
| Responsabilidad | Representar y persistir una venta |
| Síntoma | `Venta` sostiene una `Res` concreta (`Venta.cs:10`) y el repositorio **desnormaliza campos de la res** dentro de la tabla `ventas` (`DatabaseInitializer.cs:68-79`) y la re-fabrica al leer. No existe la noción de "cosa vendible": vender un litro de leche o un cuero exige o transformar el agregado o crear un flujo paralelo que duplica servicio, repositorio, factory y tabla |
| Evidencia | `Venta.cs:8-14` (agregado acoplado a `Res`); `FabricaVenta.cs:22-28` (solo firma con res); `RepositorioVentaSqlite.cs:39-45` (switch de rehidratación de res); `DatabaseInitializer.cs:68-79` (tabla denormalizada sin FK a reses) |
| Costo real | **8 clases / 8 archivos backend**: `Venta`, `IVentaFactory`, `FabricaVenta`, `IServicioVentas`, `ServicioVentas`, `IRepositorioVenta`, `RepositorioVentaSqlite`, `DatabaseInitializer` (+ vistas de venta como costo declarado) |
| Escenario ancla | SC-1 es literalmente este escenario: "la hacienda va a comenzar a vender productos derivados del ganado" |
| Prioridad | Alta |
| ¿Intervenir? | **Sí** — sin resolver esto, SC-1 no entra al sistema sin cirugía mayor en flujo congelado |

---

### P-04 · Las reglas del ganado viven fuera del ganado (y el ganado no puede defenderse)

| Campo | Contenido |
|-------|-----------|
| Archivo | `SOLID/Hacienda.Domain/Entities/Res.cs` (epicentro) · `GestorReses.cs` · `ServicioVacunacion.cs` · `ServicioChip.cs` · `ServicioGeolocalizacion.cs` |
| Clase | `Res` y subtipos; servicios de Application |
| Método | `GestorReses.Alimentar` (`:93`), `ServicioVacunacion` límites (`:134-143`), `ServicioChip.InstalarChip` (`:43-44`), `ServicioGeolocalizacion.RegistrarUbicacion` (`:38-42`) |
| Responsabilidad | Las entidades deberían proteger sus invariantes; hoy son estructuras de datos que cualquiera edita |
| Síntoma | (a) `Res` expone setters públicos (`Peso`, `Edad`, `Chip` — `Res.cs:13-16`) y la lista `VacunasAplicadas` cruda → **toda regla es bypassable** sin pasar por ningún control; (b) las reglas que sí existen viven dispersas en servicios: alimentar es `res.Peso += cantidad` en el servicio, el límite de vacunas se re-implementa imperativamente ignorando `MaxVacunas*` que la entidad ya expone, un-chip-por-res vive en `ServicioChip`, los rangos geográficos en un servicio de Application |
| Evidencia | Mapa completo de 13 reglas con ubicación actual y esperada: [[Reto2-Hacienda/Opcion1/01-AS-IS]] §5. Bypass documentado: `potrero.Reses.Add(...)` (`Potrero.cs:12` expone la lista que `AgregarRes` protege — `:24-30`); regla de edad exigida solo si se pasa por `FabricaRes` (`FabricaRes.cs:33-37`) con constructores públicos alternativos (`Cebon.cs:7`) |
| Costo real | **13 reglas de negocio dispersas en 8 archivos**; para ubicar una regla hay que abrir en promedio 3 archivos; para confiar en que se cumple, ninguno — porque no se cumple salvo por el camino feliz |
| Escenario ancla | SC-1: cada derivado trae reglas nuevas (perecederos, stock, precio por unidad) — si el patrón de "regla en servicio + entidad editable" se mantiene, SC-1 multiplica la dispersión en vez de mejorarla |
| Prioridad | Alta |
| ¿Intervenir? | **Sí** — es la crítica central del profesor ("responsabilidades fuera del dominio") y la base para que los patrones creacionales no regresen regalando entidades mutables |

---

### P-05 · La capa Web decide por tipo de vacuna (dos veces)

| Campo | Contenido |
|-------|-----------|
| Archivo | `SOLID/Hacienda.Web/Controllers/VacunaController.cs` |
| Clase | `VacunaController` |
| Método | `Crear` (`:49-68`), `CrearLote` (`:101-120`) — bloques `if (tipoVacuna == "Bacteriana") … else …` idénticos |
| Responsabilidad | Orquestar HTTP; hoy también **selecciona el tipo concreto y valida sus campos específicos** |
| Síntoma | Cada tipo nuevo de vacuna agrega un bloque más en **dos** métodos; la validación de campos opcionales (`periodoAplicacion`, `atenuacion`) vive en el controlador en vez de en la creación |
| Evidencia | `VacunaController.cs:49-68 y 101-120`; contraste con `ChipController` que sí delega a un servicio con contrato limpio |
| Costo real | Hoy: 1 clase / 1 archivo con 2 bloques duplicados que crecen linealmente con cada tipo. Con `Recombinante`: 3 bloques en 2 métodos + mensaje nuevo parseado (ver P-06) |
| Prioridad | Media |
| ¿Intervenir? | **Sí** — junto con P-02: la selección de tipo debe salir del controlador y de la firma de la interfaz |

---

### P-06 · El contrato entre capas es un string en español que se parsea

| Campo | Contenido |
|-------|-----------|
| Archivo | `Application/Interfaces/*.cs` (8 interfaces) · `VacunaController.cs:153` · `ChipController.cs:45,70` · `ResController.cs:50-54,70-74,87-91` |
| Clase | Todos los servicios de Application + 3 controladores |
| Método | Contratos `string` de `IGestorReses`, `IServicioVacunacion`, `IServicioVentas`, `IServicioChip`, `IServicioGeolocalizacion` |
| Responsabilidad | Comunicar éxito/fracaso de una operación |
| Síntoma | El éxito se decide haciendo **parsing del texto del mensaje**: `mensaje.Contains("exito") ? "success" : "danger"` (`VacunaController.cs:153`), `Contains("correctamente")` (`ChipController.cs:45`), `Contains("registrada")` (`:70`). Un cambio de redacción rompe la semántica; además el contrato de error es mixto: a veces excepción (`GestorReses.cs:40`), a veces string (`:51`), y `ResController` muestra `ex.Message` crudo al usuario |
| Evidencia | Citas anteriores; `ValidationResult` y `ResultadoAutenticacion` existen en Domain (`Results/`) y **no se usan en estos flujos** — el remedio ya tiene la mitad construida |
| Costo real | **11 clases / 11 archivos** coordinados a mano para cambiar un mensaje o agregar una operación sin romper el parsing (8 servicios/interfaces + 3 controladores) |
| Prioridad | Media |
| ¿Intervenir? | **Sí (parcial)** — el TO-BE puede unificar el contrato interno **manteniendo los mensajes de usuario exactamente iguales** (comportamiento congelado). La redacción de mensajes no se toca; el mecanismo de decisión sí |

---

### P-07 · La misma regla definida tres veces con dos umbrales

| Campo | Contenido |
|-------|-----------|
| Archivo | `SOLID/Hacienda.Domain/Factories/FabricaVenta.cs:20` · `ValueObjects/Dinero.cs:10-11` · `Application/Validaciones/ValidadorVenta.cs:15` |
| Clase | `FabricaVenta`, `Dinero`, `ValidadorVenta` |
| Método | `Crear` / ctor / `Validar` |
| Responsabilidad | Garantizar que el monto de una venta sea válido |
| Síntoma | La regla vive en 3 capas con **2 umbrales distintos** (`< 0` vs `<= 0`) y **2 contratos de error** (excepción vs string). Resultado observable del conflicto: una venta de $0 se construye, se agrega al potrero en memoria y recién el validador la rechaza devolviendo string. Validadores hermanos agravan: `ValidadorPotrero` es inalcanzable (el VO ya lanzó), `IValidarVacuna` registrado en DI (`Program.cs:57`) y jamás inyectado, validación de usuario duplicada controlador+servicio (`UsuarioController.cs:37-49` vs `ServicioAutenticacion.cs:45-54`) |
| Evidencia | Citas anteriores; auditoría completa de duplicación en [[Reto2-Hacienda/Opcion1/01-AS-IS]] §9 |
| Costo real | Cambiar la regla de monto: **3 clases / 3 archivos** + verificación manual de consistencia de umbrales y contratos. Los validadores muertos: 2 clases / 2 archivos que consumen lectura sin efecto |
| Prioridad | Media |
| ¿Intervenir? | **Sí** — fuente única de verdad por regla; los validadores de Application se reubicarán o eliminarán según el TO-BE (sin cambiar mensajes) |

---

### P-08 · El control de permisos existe pero no está conectado ⛔

| Campo | Contenido |
|-------|-----------|
| Archivo | `SOLID/Hacienda.Application/Services/AutorizadorRbca.cs` · `Infrastructure/Policies/Politica{Admin,Empleado,Visitante}.cs` · `UsuarioController.cs:15` |
| Clase | `AutorizadorRbca`, políticas |
| Método | `IPoliticaPermisos.Evaluar(operacion)` — cero llamadas en toda la solución |
| Responsabilidad | Autorizar operaciones por rol |
| Síntoma | El mecanismo (registro plugin por diccionario, deny-by-default — `AutorizadorRbca.cs:13-16,26`) está **bien diseñado y muerto**: la única inyección de `IAutorizador` nunca se invoca; la protección real es `[Authorize]` plano → Admin, Empleado y Visitante tienen permisos efectivos idénticos. Las políticas deciden por `operacion.Contains("Eliminar")` (matching de texto). Además no existe acción *Eliminar* en ningún controlador: el vocabulario de operaciones que las políticas esperan no existe |
| Evidencia | Citas anteriores; nota: Mateo detectó el mismo síntoma en el código legado (`00-lectura-en-frio/MateoRojasHernandez.pdf`) — la observación sigue vigente |
| Costo real | Activarlo: **12+ clases / 11 archivos** (8 controladores + vocabulario de operaciones + 3 políticas) |
| ¿Intervenir? | ⛔ **NO INTERVENIR — justificación técnica:** (1) activar permisos diferenciados **cambia el comportamiento observable** (un Visitante hoy entra a todo; pasaría a ser bloqueado) y el comportamiento está congelado con penalización de −0.5 por caso — SC-1 es la única solicitud autorizada; (2) el mecanismo interno ya es el patrón correcto (registro abierto de políticas): no hay robustez que comprar, solo una decisión de negocio pendiente de conectar; (3) el remedio (12+ archivos) no reduce ningún escenario de cambio del alcance de este reto. **Se declara como deuda documentada** y se retoma cuando el negocio autorice el cambio de permisos |

---

### P-09 · Reconstruir objetos desde la base es un asunto privado de cada repositorio

| Campo | Contenido |
|-------|-----------|
| Archivo | `RepositorioPotreroSqlite.cs:150-157` · `RepositorioVentaSqlite.cs:39-45` · `RepositorioResSqlite.cs:29,96` |
| Clase | Los tres repositorios anteriores |
| Método | `MapearRes` (x2, switches independientes), `ObtenerTodas` |
| Responsabilidad | Rehidratar objetos de dominio (que *es* crear objetos) |
| Síntoma | La reconstrucción de un `Res` está duplicada con switches independientes por repositorio; existe acoplamiento estático cruzado entre repositorios "hermanos" (`RepositorioResSqlite` llama a métodos estáticos de `RepositorioPotreroSqlite`, que a su vez llama a `RepositorioChipSqlite.MapearChip` — `:171`); y la rehidratación de ventas **fabrica GUIDs nuevos en cada lectura** (`RepositorioVentaSqlite.cs:41-43`) → la identidad de la res vendida nunca es estable |
| Evidencia | Citas anteriores; SQL de inserción de reses duplicado verbatim entre `RepositorioResSqlite.GuardarTodas:57-69` y `RepositorioPotreroSqlite.GuardarTodos:123-136` |
| Costo real | Agregar subtipo (cuenta compartida con P-01): 2 switches más; el bug de identidad: 1 archivo. Corregir la identidad hoy exige tocar el mapeo de ventas: 1 clase / 1 archivo, pero **cada lectura** produce objetos con identidad falsa — costo de confianza, no solo de edición |
| Prioridad | Media |
| ¿Intervenir? | **Sí (parcial)** — el TO-BE puede unificar *cómo se reconstruye* un objeto (sin tocar esquema ni SQL de la zona excluida): rehidratar es crear, y la creación es ámbito del reto. La estrategia de escritura (`DELETE FROM` total) **no se toca** (base de datos excluida) |

---

### P-10 · Eventos de dominio que nadie puede escuchar

| Campo | Contenido |
|-------|-----------|
| Archivo | `Domain/Events/DomainEvents.cs` · `Infrastructure/Events/DomainEventPublisherConsola.cs` |
| Clase | `VacunaVencidaEvent` (muerto), `DomainEventPublisherConsola` |
| Método | `IDomainEventPublisher.Publicar<T>` — único destino: `Console.WriteLine` (`DomainEventPublisherConsola.cs:5-11`) |
| Responsabilidad | Notificar ocurridos del dominio a interesados |
| Síntoma | El mecanismo de publicación existe, pero **no existe el lado del consumo**: no hay handlers ni infraestructura para suscribirse; agregar una reacción ("cuando el stock de un derivado baje del mínimo, avisar") obliga a tocar al publicador o a inventar la infraestructura cada vez. El evento `VacunaVencidaEvent` (`DomainEvents.cs:69-83`) lleva esa intención muerta desde el Reto 1; el diseño documentado (`IDomainEventHandler<T>`, `TOBE_Arquitectura_Completa.md:1106-1109`) nunca se implementó |
| Evidencia | Citas anteriores; grep: cero publicaciones de `VacunaVencidaEvent` |
| Costo real | Agregar un consumidor de evento: infraestructura inexistente (0 handlers); tocar 1 publicador + crear consumo desde cero cada vez |
| Escenario ancla | SC-1: producción y venta de derivados es naturalmente generadora de eventos (stock mínimo, lote de lácteo venciendo) |
| Prioridad | Media |
| ¿Intervenir? | **Sí** — el dolor es de coordinación de comportamiento en runtime, eje explícito del encargo |

---

### P-11 · Código muerto que confunde ⛔

| Campo | Contenido |
|-------|-----------|
| Archivo | Ver inventario completo: [[Reto2-Hacienda/Opcion1/01-AS-IS]] §12 |
| Clase | `Serializar()` (6 definiciones, 0 llamadas), `VacunaVencidaEvent`, DTOs de Application, `ServicioChip._repoRes`, `GestorPotreros._eventPublisher`, `Views/Venta/Create.cshtml`, `appsettings` connection string, `IValidarVacuna` |
| Responsabilidad | — |
| Síntoma | Elementos que consumen lectura y generan la pregunta "¿esto se usa?" en cada mantenimiento; no producen rigidez (nadie los modifica) pero sí ruido |
| Costo real | 10+ elementos en 9 archivos (solo lectura/limpieza) |
| Prioridad | Baja |
| ¿Intervenir? | ⛔ **NO INTERVENIR — justificación técnica:** el criterio del reto es robustecer puntos rígidos; el código muerto no es un punto de cambio (nadie lo toca para evolucionar el sistema) ni su limpieza aporta robustez medible. Limpiar en este momento **agranda el diff** del refactor congelado (más superficie de revisión para la Act. 4) sin reducir ningún costo de cambio. Se declara deuda de higiene para después del reto. Excepción: si un patrón adoptado en el TO-BE elimina de paso un elemento muerto (p.ej. re-diseño de creación que jubila `Serializar`), se elimina como **efecto colateral declarado**, no como objetivo |

---

### P-12 · La composición tiene efectos secundarios ⛔

| Campo | Contenido |
|-------|-----------|
| Archivo | `SOLID/Hacienda.Web/Program.cs:61-65, 99-103` |
| Clase | Composition root |
| Método | Fase de registro de servicios |
| Responsabilidad | Ensamblar el sistema |
| Síntoma | `DatabaseInitializer.Initialize` ejecuta DDL **durante el registro** de servicios y la siembra corre pre-middleware; `appsettings` declara una cadena de conexión que nunca se lee |
| Costo real | 1 clase / 1 archivo para reordenar el arranque |
| Prioridad | Baja |
| ¿Intervenir? | ⛔ **NO INTERVENIR — justificación técnica:** el arranque funciona, el punto de ensamblaje está correctamente centralizado (fortaleza del Reto 1 — [[Reto2-Hacienda/Opcion1/01-AS-IS]] §11) y reordenar la inicialización es riesgo puro (arranque roto = código que no ejecuta = criterio 4 en 0.0) sin ningún escenario de cambio que lo exija. Los patrones del TO-BE se registran en el mismo composition root sin tocar el orden existente |

---

## 3. Anclaje dolor → SC-1 (solicitud elegida)

> [!important] SC-1 elegida (D-01 resuelta). Este mapa es el argumento de la ficha de patrones ("efecto sobre las solicitudes del Anexo B").

| Punto | Cómo lo cobra SC-1 sobre el AS-IS |
|-------|-----------------------------------|
| P-01 | La leche exige `VacaLechera` → 9 clases / 9 archivos, 4 capas |
| P-03 | Vender derivados exige volver "vendible" la venta → 8 clases / 8 archivos backend en flujo congelado |
| P-04 | Reglas nuevas de derivados (stock, perecederos, precio por unidad) heredarían el patrón disperso: N servicios más |
| P-02 | Enfermedad hermana de P-01; si SC-1 agregara tipos de producto con interfaz por método, clonaría el antipatrón |
| P-10 | Producción/venta de derivados genera eventos naturales sin quién los escuche |
| P-09 | Cada producto nuevo duplicaría rehidratación en otro repositorio |

**Costo agregado de SC-1 sobre el AS-IS: ~15–20 clases en ~15 archivos, varios en zonas de comportamiento congelado.** La promesa medible del TO-BE es reducir el componente *repetitivo* de ese costo (los puntos de modificación), no la parte de novedad inherente (clases nuevas de negocio que siempre habrá que escribir).

---

## 4. Dudas abiertas

| ID | Duda | Para |
|----|------|------|
| D-05 | Confirmar si SC-1 modela **producción propia** (vaca lechera → `TipoRes` nuevo, P-01 se dispara) o **compra/stock directo de derivados** (P-01 queda como escenario futuro). Impacta el alcance del TO-BE | [[Reto2-Hacienda/Opcion1/05-TOBE]] |
| D-06 | Consolidar los ≥3 hallazgos propios sin IA del equipo (protocolo §0) | Este documento | 🟡 **Acción de equipo pendiente** (mínimo 3 hallazgos sin IA para cumplir rúbrica) |

## 5. Riesgos encontrados en esta actividad

1. **P-03 opera en flujo congelado:** cualquier diseño que toque `Venta`/`ServicioVentas` exige banco de evidencia antes/después (salidas idénticas) — cargar esto en el plan de la Act. 4 desde ya.
2. **P-01 cuenta vistas en su costo** pero las vistas no se intervienen: si el TO-BE elimina el enum `TipoRes` o cambia `Res.Tipo`, las vistas que leen `TipoRes` dejan de compilar → restricción dura de diseño (D-05-adjacente): el contrato `TipoRes`/`Res` hacia las vistas debe sobrevivir.

---

## 6. Navegación

- [[Reto2-Hacienda/Opcion1/01-AS-IS]] — evidencia base de cada ficha.
- [[Reto2-Hacienda/Opcion1/03-PatronesEvaluados]] — Actividad 2: cada patrón del Anexo A se ancla a un P-XX de aquí.
- [[Reto2-Hacienda/Opcion1/10-BitacoraIA]] — registro de decisiones.
