---
title: "Reto 2 — Puntos de dolor del AS-IS"
tags:
  - arquitectura-software
  - reto2
  - hacienda
  - puntos-de-dolor
estado: "completo — pendiente selección del equipo"
---

# 02 — Puntos de dolor · Inventario P-XX

> [!abstract] Propósito
> Inventario completo de puntos de dolor **arquitectónicos** del AS-IS: lugares donde el diseño, aun siendo correcto, es rígido o caro de cambiar. Cada punto se describe por su **síntoma** (costo de cambio), con **evidencia en `archivo:línea`** y **costo medido contando clases y archivos reales** — nunca "alto/medio/bajo".
>
> **Cómo leerlo**: la columna *¿Intervenir?* contiene la **recomendación de la IA con su argumento**; la decisión es del equipo. El requisito del enunciado de "al menos tres puntos encontrados sin IA" lo satisface el equipo con su propia lectura del código — este inventario no simula orígenes.

---

## 1. Matriz resumen

| ID | Ubicación | Síntoma en una línea | Costo real | Prioridad | ¿Intervenir? (recom.) |
|----|-----------|----------------------|------------|-----------|------------------------|
| P-01 | `Entities/Venta.cs:10` + pipeline de venta | Vender algo que no sea una res exige reescribir el pipeline entero | 14 archivos / ~14 clases | Alta | **Sí** |
| P-02 | `Factories/FabricaRes.cs:17-48` + 5 switches hermanos | Un subtipo nuevo de Res dispara 7 puntos de decisión paralelos | 10 archivos / 7 clases | Alta | **Sí** |
| P-03 | `Application/Services/*` (contrato string) | El éxito/fracaso se detecta con `Contains("correctamente")` | 8 servicios + 5 controladores | Alta | **No** (argumento en §4) |
| P-04 | `ServicioVacunacion.cs:131-146` vs `Res.cs:35-40` | La regla de vacunación vive duplicada en el servicio y contradice a la entidad | 2 clases en conflicto | Alta | **Sí** |
| P-05 | `Repositorio*Sqlite` (switches de subtipos) | Cada repo re-decide subtipos al leer: la creación tiene 2 caminos paralelos | +2 ramas × subtipo × 3 repos | Alta | **Sí** |
| P-06 | `IVacunaFactory`/`IServicioVacunacion`/`VacunaController` | El "switch" de vacunas se implementó como métodos paralelos en 4 capas | 12 archivos / ~10 clases por tipo nuevo | Alta | **Sí** |
| P-07 | `AutorizadorRbca` + 3 políticas | Autorización ensamblada pero muerta: 0 llamadas a `.Autorizar(` | 5 clases inertes; activarla = 8 controladores | Alta | **No** (argumento en §4) |
| P-08 | `Res.cs:13-16` (setters) + `GestorReses.cs:93` | El estado del dominio se muta desde fuera; los invariantes no existen | servicio + entidad + vista por regla nueva | Media-Alta | **Sí** |
| P-09 | `Enums/TipoPotrero.cs` + `GestorReses.cs:137-143` | Dos enums idénticos traducidos a mano: dos fuentes de verdad de un concepto | 4 puntos de sincronización por tipo | Media | **Sí** (absorbido por P-02) |
| P-10 | `RepositorioVentaSqlite.cs:41-43` | Cada lectura inventa un GUID nuevo: la identidad de la res vendida se destruye | bug latente; corrección 2-3 archivos | Media | **Sí** (oportunista con P-05) |
| P-11 | `Validaciones/Validador*.cs` (los 4) | Validadores triviales que llegan tarde, sin composición de reglas | 4 clases + doble mecanismo de error | Media | **Sí, condicionado** |
| P-12 | `DomainEventPublisherConsola.cs` + `GestorReses.cs:55-75` | Observer a medio instalar: publica a una consola invisible y el efecto real se duplica a mano en strings | infra + 2 publicadores por consumidor nuevo | Media | **No** (riesgo de comportamiento; se evalúa igual en [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/03-PatronesEvaluados]]) |
| P-13 | `ServicioChip.cs:54-56` | Tres agregados persistidos sin transacción común | BD inconsistente ante fallo intermedio | Media | **No con patrón** (Unit of Work no está en el Anexo A) |
| P-14 | `DataLoader.cs:60-98` | El seed bypasea dominio y validaciones (254 INSERTs crudos) | 1 archivo gigante + esquema | Media | **No** (datos de prueba) |
| P-15 | `DTOs/Dto.cs` (muerto) + vistas tipadas a entidades | Cambiar una entidad rompe vistas; el anticorrupción existe pero desconectado | 8 vistas + 8 acciones | Media | **No** (frontend fuera de alcance, D-05) |
| P-16 | `PoliticaEmpleado.cs:12` / `PoliticaVisitante.cs:12` | Permisos por `Contains` sobre strings sin catálogo de operaciones | fragilidad latente (inerte por P-07) | Baja | **No** (cadena de P-07) |
| P-17 | `DatabaseInitializer.cs:34-43` + repos save-all | Esquema a mano + DELETE masivo + reescritura total por operación | 3 archivos por campo nuevo | Baja | **No** (BD real fuera de alcance del encargo) |
| P-18 | `ServicioAutenticacion.cs:57` | Rol inicial hardcodeado sin política de ascenso | SC de roles reabre servicio + controlador | Baja | **No** (sin SC de permisos en alcance) |

**Balance**: 9 candidatos a intervenir (P-01, 02, 04, 05, 06, 08, 09, 10, 11-condicional) · 9 recomendados como deuda declarada. De estos 9, los patrones adoptados (3–5) anclarán únicamente a los de prioridad Alta con costo contado.

> [!success] Decisión del equipo (30 de agosto)
> **D-01: SC-1** (productos derivados) · **Intervención aprobada: los 9 puntos recomendados** (P-01, 02, 04, 05, 06, 08, 09, 10, 11) · Los 9 restantes quedan como **deuda declarada**, con P-03, P-07 y P-17 como los "no intervenir" oficiales argumentados (§4). Registrado en [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/10-BitacoraIA]] (B-07, B-08).

---

## 2. Fichas de detalle

### P-01 · Venta acoplada a `Res` — no existe "lo vendible"

| Campo | Valor |
|-------|-------|
| Archivo | `Hacienda.Domain/Entities/Venta.cs` (+ 13 archivos del pipeline, ver evidencia) |
| Clase | `Venta` |
| Método | — (0 métodos: entidad anémica) |
| Responsabilidad | Hoy registra la venta de UNA res: `public Res Res { get; }` (l.10). El monto llega suelto; la política de precio no existe |
| Síntoma | Vender un derivado (lácteos, carne, piel — SC-1) obliga a reescribir el pipeline completo de venta, porque el caso de uso "vender ganado" está modelado como entidad sin abstracción de producto |
| Evidencia | `Venta.cs:10` (`Res Res`); tabla `ventas` con columnas `res_nombre, res_peso, res_edad, res_tipo` (`DatabaseInitializer.cs:69-79`); única entrada `ResController.Vender` (`ResController.cs:79`); precio/kg calculado en la vista sobre `venta.Res.Peso` (`Views/Venta/Index.cshtml`) |
| Costo real | **14 archivos / ~14 clases** (trace completo en §5.1) |
| Prioridad | Alta |
| ¿Intervenir? | **Sí (recomendado)** — es el ancla natural de la SC-1 y del clúster creacional |

### P-02 · Un subtipo nuevo de Res = 7 puntos de decisión paralelos

| Campo | Valor |
|-------|-------|
| Archivo | `Hacienda.Domain/Factories/FabricaRes.cs` (+ `GestorReses.cs`, 2 repos SQLite, 2 enums, 2 vistas) |
| Clase | `FabricaRes` |
| Método | `Crear` (l.17-22) y privado `DescribirRango` (l.42-48) |
| Responsabilidad | Decide qué subtipo de Res instanciar a partir de `TipoRes` mediante diccionario de lambdas, valida edad y fabrica mensajes de error |
| Síntoma | La decisión de tipo está regada: fábrica (×2 sitios), servicio (mapeo de enums + contadores de estadísticas), 2 repositorios (switches de reconstrucción), 2 vistas (badges), 2 enums paralelos. Añadir `Toro` obliga a editarlos todos — el diccionario solo relocalizó el `switch` |
| Evidencia | `FabricaRes.cs:17-22` (dict `TipoRes→lambda`), `:42-48` (switch de rangos); `GestorReses.cs:130-132` (contadores por nombre de tipo), `:137-143` (`MapearTipoRes`); `RepositorioPotreroSqlite.cs:150-156`; `RepositorioVentaSqlite.cs:39-45`; `Views/Res/Index.cshtml:71-77`; `Views/Venta/Index.cshtml:106-112` |
| Costo real | **10 archivos / 7 clases editadas + 1 nueva** (tabla completa en §3.1) |
| Prioridad | Alta |
| ¿Intervenir? | **Sí (recomendado)** — es el dolor creacional central del sistema |

### P-03 · Contrato de servicios: strings y heurística de texto

| Campo | Valor |
|-------|-------|
| Archivo | `Hacienda.Application/Services/*` (los 8) + `ChipController.cs`, `VacunaController.cs` |
| Clase | Todos los servicios |
| Método | Todos los que devuelven `string` |
| Responsabilidad | Los servicios devuelven mensajes en español terminados para TempData; los controladores clasifican éxito/fracaso con `Contains` |
| Síntoma | Renombrar un mensaje rompe silenciosamente la clasificación success/danger de la UI; los `Results` del dominio (`ValidationResult`, etc.) existen pero no se usan en servicios de negocio |
| Evidencia | `ChipController.cs:45,70` (`mensaje.Contains("correctamente") ? "success" : "danger"`); `VacunaController.cs:153`; `"Datos válidos. Guardado exitoso en BD."` (`ServicioVacunacion.cs:166` — filtración de detalle de persistencia al mensaje) |
| Costo real | 8 servicios + 5 controladores tocan el contrato a la vez |
| Prioridad | Alta |
| ¿Intervenir? | **No (recomendado)** — ver argumento en §4.1 |

### P-04 · La regla de vacunación vive dos veces

| Campo | Valor |
|-------|-------|
| Archivo | `Hacienda.Application/Services/ServicioVacunacion.cs` |
| Clase | `ServicioVacunacion` |
| Método | `AplicarVacuna` (l.123-160) |
| Responsabilidad | Aplica vacunas: comprueba límites por categoría, evita duplicadas, consume inventario, muta la lista pública de la entidad |
| Síntoma | El servicio reimplementa el conocimiento polimórfico de `Res` (`MaxVacunasBacterianas/Vivas`) con su propio if (l.137-143), mientras `Res.EsquemaVacunacionCompleto` (l.35-40) ya lo encapsula. Dos clases pueden discrepar; una regla clínica nueva (SC-3) obliga a tocar servicio + entidad + repo |
| Evidencia | `ServicioVacunacion.cs:137-143` vs `Res.cs:28-29,35-40`; `res.VacunasAplicadas` pública mutada desde fuera (`ServicioVacunacion.cs:145`) |
| Costo real | 2 clases en conflicto activo; cada regla clínica nueva = 3 archivos |
| Prioridad | Alta |
| ¿Intervenir? | **Sí (recomendado)** — devolver la regla al Core es exactamente "fortalecer el dominio" |

### P-05 · La creación tiene dos caminos paralelos que discrepan

| Campo | Valor |
|-------|-------|
| Archivo | `Hacienda.Infrastructure/Persistence/Sqlite/RepositorioPotreroSqlite.cs`, `RepositorioVentaSqlite.cs`, `RepositorioVacunaSqlite.cs` |
| Clase | Los 3 repositorios (+ `ServicioAutenticacion`, `ServicioGeolocalizacion`, `ServicioChip`, `DataLoader`) |
| Método | `MapearRes` (l.150-156), `ObtenerTodas` (l.39-45), `MapearVacuna` (l.103-113) / `InsertVacuna` (l.126-159) |
| Responsabilidad | Rehidratar entidades desde SQLite — decidiendo subtipos con switches propios |
| Síntoma | El conocimiento de "cómo se construye cada subtipo" vive en la factoría Y otra vez en cada repo (11 sitios de `new` fuera de factorías). Los dos caminos pueden construir distinto (y ya discrepan: ver P-10) |
| Evidencia | `RepositorioVacunaSqlite.cs:126-162`: `if (vacuna is Bacteriana b) … else if (vacuna is Viva v) …`; `RepositorioPotreroSqlite.cs:152-154`: `new Ternero/Novillo/Cebon` por switch propio |
| Costo real | +2 ramas por subtipo nuevo × 3 repos × 2 métodos; 11 sitios `new` inventariados |
| Prioridad | Alta |
| ¿Intervenir? | **Sí (recomendado)** — centralizar la construcción es prerrequisito para que una factoría corregida signifique algo |

### P-06 · El switch de vacunas se implementó como métodos paralelos en 4 capas

| Campo | Valor |
|-------|-------|
| Archivo | `Hacienda.Domain/Factories/IVacunaFactory.cs` + `ServicioVacunacion.cs` + `VacunaController.cs` + 4 vistas |
| Clase | `IVacunaFactory` / `FabricaVacuna` / `ServicioVacunacion` / `VacunaController` |
| Método | `CrearBacteriana`/`CrearViva` (factory), `CrearVacunaBacteriana`/`CrearVacunaViva`/`CrearLoteVacunaBacteriana`/`CrearLoteVacunaViva` (servicio, cuerpos clonados l.61-117), `Create`/`CrearLote` (controller) |
| Responsabilidad | Crear vacunas unitarias y por lote |
| Síntoma | La decisión por subtipo no está en un switch sino **en la firma**: un método por tipo que se propaga interfaz → servicio → controlador → radio buttons en string. La explosión es lineal en subtipos × operaciones |
| Evidencia | `VacunaController.cs:49-68` y `:101-120` (`if (tipoVacuna == "Bacteriana") … else …` sobre string mágico, duplicado); radios `"Bacteriana"`/`"Viva"` en `Views/Vacuna/Create.cshtml:38-50`; `Viva.GradoAtenuacion` (enum de la subclase concreta) filtrando firmas de Application y Web |
| Costo real | **12 archivos / ~10 clases** por cada tipo nuevo de vacuna |
| Prioridad | Alta |
| ¿Intervenir? | **Sí (recomendado)** — mismo clúster creacional que P-02, peor propagación |

### P-07 · Autorización ensamblada pero muerta

| Campo | Valor |
|-------|-------|
| Archivo | `Hacienda.Application/Services/AutorizadorRbca.cs` + `Infrastructure/Policies/*` + `UsuarioController.cs:11,15-19` |
| Clase | `AutorizadorRbca`, `PoliticaAdmin`, `PoliticaEmpleado`, `PoliticaVisitante` |
| Método | `Autorizar(Usuario, string operacion)` — **0 llamadas en toda la solución** (grep) |
| Responsabilidad | Resolvía permisos por rol con registro múltiple de estrategias (el único mecanismo de extensión real del código, `Program.cs:83-85`) |
| Síntoma | El sistema registra 3 políticas y un autorizador que nadie invoca: ningún controlador consulta permisos; la protección real es solo `[Authorize]` (autenticado sí/no). El rol viaja en claims y no se consume |
| Evidencia | `_autorizador` inyectado y sin usar (`UsuarioController.cs:15-19`); grep `.Autorizar(` = 0 resultados |
| Costo real | 5 clases inertes; activarlas exigiría tocar los 8 controladores |
| Prioridad | Alta (es el "ensamblaje invisible" del correo de la Líder Técnica) |
| ¿Intervenir? | **No (recomendado)** — ver argumento en §4.2 |

### P-08 · Entidades semi-anémicas: el estado se muta desde fuera

| Campo | Valor |
|-------|-------|
| Archivo | `Hacienda.Domain/Entities/Res.cs` |
| Clase | `Res` (y `Venta`, `Usuario`, `Geolocalizacion`) |
| Método | — (setters públicos en `Peso`, `Edad`, `Chip`, `VacunasAplicadas`) |
| Responsabilidad | Ser portadora de datos del ganado; hoy los invariantes los aplican los servicios |
| Síntoma | `GestorReses.AlimentarRes` hace `res.Peso += cantidad` (l.93) sobre un setter público; los umbrales de desnutrición/aptitud viven en el servicio (l.56-65). La entidad no puede defenderse; una regla de peso nueva = editar servicio + vista |
| Evidencia | `Res.cs:13-16` (setters), `GestorReses.cs:56-65,93,96-105`; contraste: `Potrero.AgregarRes` (invariante dentro, `Potrero.cs:24-30`) y `Chip` (ctor privado + `Crear` + máquina de estados, `Chip.cs:14-71`) — **el estándar correcto ya existe en el propio código** |
| Costo real | 1 servicio + 1 entidad + 1 vista por cada regla nueva de negocio del ganado |
| Prioridad | Media-Alta |
| ¿Intervenir? | **Sí (recomendado)** — es la observación central del profesor sobre encapsulación |

### P-09 · Dos enums idénticos para un solo concepto

| Campo | Valor |
|-------|-------|
| Archivo | `Hacienda.Domain/Enums/TipoPotrero.cs` + `Hacienda.Application/Services/GestorReses.cs` |
| Clase | `TipoPotrero` / `GestorReses` |
| Método | `MapearTipoRes` (l.137-143) |
| Responsabilidad | Traducir a mano el tipo del potrero al tipo de res (asume potrero homogéneo) |
| Síntoma | `TipoRes` y `TipoPotrero` son idénticos valor a valor; añadir un tipo obliga a sincronizar 2 enums + switch + 3 contadores de estadísticas nombrados a mano (`["Terneros"]/["Cebones"]/["Novillos"]`, l.130-132) |
| Evidencia | `GestorReses.cs:130-143` |
| Costo real | 4 puntos de sincronización por tipo nuevo |
| Prioridad | Media |
| ¿Intervenir? | **Sí (recomendado)** — se absorbe naturalmente en la corrección de P-02 |

### P-10 · Identidad destruida en lectura

| Campo | Valor |
|-------|-------|
| Archivo | `Hacienda.Infrastructure/Persistence/Sqlite/RepositorioVentaSqlite.cs` |
| Clase | `RepositorioVentaSqlite` |
| Método | `ObtenerTodas` (l.39-45) |
| Responsabilidad | Reconstruir ventas persistidas |
| Síntoma | Reconstruye la res con `_guidProvider.Nuevo()`: **cada lectura inventa una identidad nueva** — dos lecturas de la misma venta producen reses con IDs distintos. Bug latente: cualquierfeature que dependa de identidad (reportes, historia clínica SC-3) se apoya en datos frágiles |
| Evidencia | `RepositorioVentaSqlite.cs:41-43` (`new Ternero(_guidProvider.Nuevo(), …)` dentro del switch de reconstrucción) |
| Costo real | Corrección: 2-3 archivos (repo + esquema persistido) |
| Prioridad | Media |
| ¿Intervenir? | **Sí (recomendado)** — barato, y se corrige de paso al centralizar la construcción (P-05) |

### P-11 · Validadores triviales, tardíos y sin composición

| Campo | Valor |
|-------|-------|
| Archivo | `Hacienda.Application/Validaciones/ValidadorRes.cs`, `ValidadorVacuna.cs`, `ValidadorVenta.cs`, `ValidadorPotrero.cs` |
| Clase | Los 4 `Validador*` |
| Método | `Validar(entidad)` |
| Responsabilidad | Chequear 2 condiciones triviales por entidad DESPUÉS de que factorías y constructores ya lanzaron excepciones por lo mismo |
| Síntoma | Doble mecanismo de error (excepción vs `ValidationResult`) para las mismas reglas; `ValidadorPotrero` re-valida lo que el VO `Identificacion` ya garantiza en su ctor (l.9-11); `ValidadorVacuna` está registrado (`Program.cs:57`) y nunca inyectado; no existe punto de extensión para componer reglas — una regla nueva = editar la clase existente |
| Evidencia | `ValidadorRes.cs:14-15`; `GestorReses.AgregarRes` mezcla `throw` (l.40) con `return string.Join("; ", …)` (l.50-51) |
| Costo real | 4 clases + el costo cognitivo de dos semánticas de error conviviendo |
| Prioridad | Media |
| ¿Intervenir? | **Sí, condicionado (recomendado)** — solo si la unificación de reglas entra en el alcance del Core sin tocar el contrato string (P-03 lo amarra); si no, deuda declarada |

### P-12 · Observer a medio instalar

| Campo | Valor |
|-------|-------|
| Archivo | `Hacienda.Infrastructure/Events/DomainEventPublisherConsola.cs` + `GestorReses.cs:55-75,95-105` |
| Clase | `DomainEventPublisherConsola` / `GestorReses` |
| Método | `Publicar<TEvento>` / `AgregarRes`, `AlimentarRes` |
| Responsabilidad | Publicar eventos de dominio a consola; el efecto visible al usuario se logra concatenando strings a mano junto a cada publish |
| Síntoma | La reacción al evento está duplicada: se publica (invisible en web) y se escribe el mensaje a mano. Un consumidor real (auditoría, SC-3) obliga a cambiar la infraestructura y reeditar los publicadores |
| Evidencia | `mensajeEventos += $"\n[Evento] …"` junto a cada `Publicar` (`GestorReses.cs:58-73`); `VacunaVencidaEvent` definido y nunca publicado |
| Costo real | Infra + 2 publicadores por cada consumidor nuevo |
| Prioridad | Media |
| ¿Intervenir? | **No (recomendado para Reto 2)** — las strings de reacción son comportamiento observable congelado (L1): rediseñar el dispatch toca exactamente lo que no se puede mover. Se evalúa Observer en [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/03-PatronesEvaluados]] con este riesgo declarado |

### P-13 · Tres agregados, ninguna transacción

| Campo | Valor |
|-------|-------|
| Archivo | `Hacienda.Application/Services/ServicioChip.cs` |
| Clase | `ServicioChip` |
| Método | `InstalarChip` (l.54-56) |
| Responsabilidad | Persistir chip + potreros + vacunas aplicadas en tres llamadas consecutivas a repos |
| Síntoma | Un fallo entre llamadas deja la BD inconsistente; añadir un agregado más es otra línea |
| Evidencia | `_repoChip.Guardar(chip); _repoPotrero.GuardarTodos(potreros); _repoVacuna.GuardarAplicadas(potreros);` |
| Costo real | Riesgo de consistencia latente; remedio = Unit of Work (no está en el Anexo A) |
| Prioridad | Media |
| ¿Intervenir? | **No con patrón** — corrección puntual posible durante la implementación de la SC si el equipo lo decide; se declara deuda |

### P-14 · El seed bypasea el dominio

| Campo | Valor |
|-------|-------|
| Archivo | `Hacienda.Infrastructure/CrossCutting/DataLoader.cs` + `seed_data.sql` |
| Clase | `DataLoader` |
| Método | `CargarDatosAsync` (l.60-98) |
| Responsabilidad | Sembrar datos de prueba con 254 INSERTs crudos y usuarios a mano |
| Síntoma | Imposible de mantener con factorías/validaciones; cada feature nueva de datos = SQL manual con tipos en string |
| Costo real | 1 archivo gigante + dependencia del esquema |
| Prioridad | Media |
| ¿Intervenir? | **No (recomendado)** — es dato de prueba, no producción; el remedio (seed por dominio) cuesta más que el problema |

### P-15 · DTOs muertos, vistas tipadas a entidades

| Campo | Valor |
|-------|-------|
| Archivo | `Hacienda.Application/DTOs/Dto.cs` + las 8 vistas |
| Clase | `PotreroDto`, `ResDto`, `VacunaDto`, `VentaDto` (0 usos) |
| Método | — |
| Responsabilidad | Nada: el anticorrupción entre capas existe y está desconectado |
| Síntoma | Las vistas consumen `List<Venta>` y tuplas de entidades; cambiar una entidad rompe N vistas |
| Costo real | Activar DTOs = 8 vistas + 8 acciones |
| Prioridad | Media |
| ¿Intervenir? | **No (recomendado)** — el frontend queda igual por D-05; tocar las vistas está fuera de alcance |

### P-16 · Permisos por `Contains` sin catálogo

| Campo | Valor |
|-------|-------|
| Archivo | `Hacienda.Infrastructure/Policies/PoliticaEmpleado.cs` / `PoliticaVisitante.cs` |
| Clase | Las 2 políticas |
| Método | `Evaluar(operacion)` (l.12-14) |
| Responsabilidad | Decidir permisos comparando substrings: `Contains("Eliminar")`, `Contains("Consultar"/"Listar")` |
| Síntoma | Renombrar una acción cambia los permisos silenciosamente (hoy inerte por P-07) |
| Costo real | Fragilidad latente de seguridad |
| Prioridad | Baja (hasta que P-07 se active) |
| ¿Intervenir? | **No** — cadena de P-07; deuda declarada |

### P-17 · Esquema a mano y save-all por operación

| Campo | Valor |
|-------|-------|
| Archivo | `Hacienda.Infrastructure/Persistence/Sqlite/DatabaseInitializer.cs` + repositorios |
| Clase | `DatabaseInitializer` / repos save-all |
| Método | `Initialize`; `GuardarTodos` |
| Responsabilidad | Crear esquema con ALTER retrocompatibles; persistir borrando todo y re-insertando |
| Síntoma | Cada campo nuevo toca entidad + repo + initializer a la vez; cada guardado es O(n) con `DELETE` masivo (`conn.Execute("DELETE FROM ventas")`, `RepositorioVentaSqlite.cs:59`) |
| Costo real | 3 archivos por campo nuevo |
| Prioridad | Baja |
| ¿Intervenir? | **No** — "base de datos real" está explícitamente fuera de alcance del encargo |

### P-18 · Rol inicial hardcodeado

| Campo | Valor |
|-------|-------|
| Archivo | `Hacienda.Application/Services/ServicioAutenticacion.cs` |
| Clase | `ServicioAutenticacion` |
| Método | `CrearUsuario` (l.57) |
| Responsabilidad | Crear usuarios siempre con `RolUsuario.Visitante` |
| Síntoma | Sin política de ascenso de rol; una SC de roles reabriría servicio + controlador |
| Costo real | 2 archivos |
| Prioridad | Baja |
| ¿Intervenir? | **No** — no hay SC de permisos en alcance; deuda declarada |

---

## 3. Auditoría de factorías existentes (prioritaria)

> [!question] Las preguntas que el encargo exige responder por cada factoría
> ¿Cumple realmente Factory Method? · ¿Es un Simple Factory disfrazado? · ¿Viola OCP? · ¿Viola SRP? · ¿Centraliza demasiadas decisiones? · ¿Corregir, reemplazar, eliminar o dejar igual?

### 3.1 `FabricaRes` / `IResFactory`

```csharp
// FabricaRes.cs:17-22
_creators = new() {
    [TipoRes.Ternero] = (n, p, e) => new Ternero(_guidProvider.Nuevo(), n, p, e),
    [TipoRes.Novillo] = (n, p, e) => new Novillo(_guidProvider.Nuevo(), n, p, e),
    [TipoRes.Cebon]   = (n, p, e) => new Cebon(_guidProvider.Nuevo(), n, p, e),
};
// FabricaRes.cs:42-48 — segunda decisión por enum en el MISMO archivo
private static string DescribirRango(Res res) => res.Tipo switch { … };
```

| Pregunta | Veredicto | Evidencia |
|----------|-----------|-----------|
| ¿Factory Method real? | **No** — la decisión la toma UNA clase en runtime a partir de un parámetro; no hay subclases de la factoría ni hook de extensión | l.17-22 |
| ¿Simple Factory disfrazado? | **Sí** — el diccionario solo relocaliza el `switch`; añadir un tipo obliga a editar el cuerpo de la misma clase | l.19-21 + l.42-47 |
| ¿Viola OCP? | **Sí** — la extensión exige modificación (aquí y en los 5 switches hermanos de P-02) | tabla §3.2 de [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/01-AS-IS]] |
| ¿Viola SRP? | **Tensionado** — crea, valida edad (l.35-37) y fabrica mensajes de error: tres motivos de cambio | l.27-48 |
| ¿Centraliza demasiadas decisiones? | Paradoja: centraliza la del alta, pero los repos re-deciden lo mismo al leer (P-05) | `RepositorioVentaSqlite.cs:39-45` |
| **¿Qué hacer?** | **Corregir/reemplazar** — devolver la decisión de creación al propio subtipo (cada subtipo sabe construirse y describirse) y eliminar los switches espejo | ancla P-02 |

### 3.2 `FabricaVacuna` / `IVacunaFactory`

| Pregunta | Veredicto | Evidencia |
|----------|-----------|-----------|
| ¿Factory Method real? | **No** — la decisión se movió a la FIRMA: un método por subtipo (`CrearBacteriana`, `CrearViva`) | `IVacunaFactory.cs` |
| ¿Simple Factory disfrazado? | **Sí, y con la peor variante** — el "parámetro que decide" es el nombre del método, y contamina toda la cadena hacia arriba: 4 métodos paralelos en el servicio (cuerpos de lote clonados, l.61-117), 2 if/else sobre string mágico en el controlador (l.49, 101), radios en las vistas | `VacunaController.cs:49-68,101-120` |
| ¿Viola OCP? | **Sí** — un tipo nuevo de vacuna = 12 archivos / ~10 clases (enum, entidad, 2 métodos en interfaz+clase, 2-4 en servicio, 2 acciones en controller, 4 vistas, repo ×2 métodos, initializer) | conteo §2.2 exploración |
| ¿Viola SRP? | Sí en cadena — el servicio acumula creación+lote+aplicación+estadísticas (185 LOC) | `ServicioVacunacion.cs` |
| ¿Centraliza demasiadas decisiones? | No centraliza: **propaga** — además `Viva.GradoAtenuacion` (enum anidado en la subclase concreta) filtra a las firmas de Application y Web (DIP roto hacia abajo) | `Viva.cs:7-12` |
| **¿Qué hacer?** | **Corregir** — misma dirección que P-02: un solo punto de creación polimórfico y los datos propios de cada subtipo dentro del subtipo | ancla P-06 |

### 3.3 `FabricaVenta` / `IVentaFactory`

| Pregunta | Veredicto | Evidencia |
|----------|-----------|-----------|
| ¿Factory Method real? | **No** | `FabricaVenta.cs:17-28` |
| ¿Simple Factory? | **Tampoco** — no decide nada: valida `res != null` y `monto >= 0` y ejecuta `new Venta(...)`. Es un **constructor wrapper** cuyo único valor es encapsular `IGuidProvider`/`TimeProvider` | l.17-28 |
| ¿Viola OCP/SRP? | No directamente — es inocua; el problema es que la familia polimórfica no existe (P-01) | — |
| **¿Qué hacer?** | **Reemplazar cuando se atienda P-01** — con la abstracción de "vendible" la creación vuelve a tener sentido; tal como está, es una factoría de nombre y no de función | ancla P-01 |

### 3.4 `FabricaPotrero` / `IPotreroFactory`

| Pregunta | Veredicto | Evidencia |
|----------|-----------|-----------|
| ¿Qué es? | Otro constructor wrapper: valida no-vacío (que el VO `Identificacion` ya garantiza lanzando en su ctor, `Identificacion.cs:9-11`) y ejecuta `new Potrero` | `FabricaPotrero.cs:18-27` |
| **¿Qué hacer?** | **Eliminar o dejar igual** — no daña, pero tampoco aporta; decisión del equipo con un criterio: si P-01/P-02/P-05 introducen un mecanismo de creación coherente, mantener dos wrappers sin función es ruido | — |

> [!important] Síntesis de la auditoría
> El profesor tiene razón: **0 de 4 factorías son Factory Method**. Dos son Simple Factory (una con switch relocalizado, otra con el switch propagado a la firma en 4 capas) y dos son wrappers de constructor que no factorizan nada. Y la creación real está además duplicada en los repositorios (P-05). La corrección no es "aplicar Factory Method por el libro" — eso se evalúa con alternativas en [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/03-PatronesEvaluados]] y se decide en [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/04-DecisionesArquitectonicas]].

---

## 4. Los "no intervenir" con argumento técnico

El enunciado exige al menos un punto donde **el remedio cuesta más que el problema**. Se ofrecen tres argumentados (el equipo selecciona):

### 4.1 P-03 · Contrato string de servicios

**Argumento**: los mensajes en español que devuelven los servicios **son comportamiento observable congelado (límite L1: −0.5 por caso)**. Sustituir el contrato string por resultados tipados obliga a reescribir el ensamblaje de mensajes en 8 servicios y re-verificar la clasificación en 5 controladores, con riesgo directo de alterar salidas que no se autorizaron a cambiar. El beneficio sería higiene interna, no robustez ante cambio: ningún escenario del Anexo B se abarata corrigiéndolo. **Costo del remedio > costo del problema, con riesgo de penalización encima.** Deuda declarada.

### 4.2 P-07 · Autorización muerta

**Argumento**: activar `AutorizadorRbca` **cambia comportamiento observable** (aparecerían denegaciones que hoy no existen) sin solicitud de cambio autorizada — colisión frontal con L1. Además exigiría tocar los 8 controladores, justo la capa que D-05 mantiene quieta. Lo que sí se hace: dejarlo **declarado como deuda** en [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/09-VistaTecnica]] con su señal de alerta (cualquier SC de permisos futura lo activa). El mecanismo (registro múltiple + diccionario por rol) ya es el diseño correcto; no hay patrón que añadir, solo una decisión de negocio pendiente.

### 4.3 P-17 · Save-all y esquema a mano

**Argumento**: el encargo excluye explícitamente "base de datos real" del alcance. Optimizar persistencia (Unit of Work, change tracking, migraciones) es re-ingeniería de infraestructura con riesgo de alterar datos/orden de escritura, para un sistema de aula sin carga concurrente. **El remedio toca lo que el encargo prohíbe tocar.**

---

## 5. Análisis de impacto de las solicitudes de cambio (Anexo B)

### 5.1 SC-1 · Vender productos derivados (lácteos, carne, piel)

| # | Archivo a tocar | Por qué |
|---|-----------------|---------|
| 1 | `Entities/Venta.cs` | `Res Res` grabado a fuego → hace falta producto abstracto |
| 2 | `Factories/IVentaFactory.cs` | firma `Crear(Res, …)` |
| 3 | `Factories/FabricaVenta.cs` | ídem |
| 4 | `Interfaces/IValidarVenta.cs` | contrato acoplado a venta-con-res |
| 5 | `Validaciones/ValidadorVenta.cs` | `venta.Res == null` (l.14) |
| 6 | `Interfaces/IServicioVentas.cs` | solo existe `VenderRes(potreroId, nombreRes, monto)` |
| 7 | `Services/ServicioVentas.cs` | todo el método asume potrero+res |
| 8 | `RepositorioVentaSqlite.cs` | esquema y switch de reconstrucción |
| 9 | `DatabaseInitializer.cs` | tabla `ventas` con columnas `res_*` |
| 10 | `ResController.cs` | la entrada de venta vive aquí (`Vender`, l.79) |
| 11 | `Views/Res/Index.cshtml` | el formulario es un modal de res |
| 12 | `Views/Venta/Index.cshtml` | columnas/badges de res y precio/kg |
| 13 | `VentaController.cs` | nuevo flujo de alta (hoy solo lista) |
| 14 | `DTOs/Dto.cs` | `VentaDto` con forma de res |

**= 14 archivos / ~14 clases.** De ellos, 9 son backend y 5 frontend-ish (10-12, 13 parcial). Con D-05 (frontend conservado), el costo backend real sigue siendo **9-10 archivos**.

### 5.2 SC-3 · Historia clínica por res

Nueva entidad de eventos clínicos + repo + tabla + colección en `Res` (hoy `VacunasAplicadas` pública) + reglas de aplicación + carga en `GestorReses.ListarReses` (que hoy llama a mano `CargarVacunasAplicadasEnPotreros`) + detalle de vista. **= 7-8 archivos / 5-7 clases**, y cada tipo nuevo de evento clínico reabriría los mismos archivos (no hay polimorfismo de eventos).

### 5.3 Comparación y recomendación (decisión D-01)

| Criterio | SC-1 (derivados) | SC-3 (historia clínica) |
|----------|------------------|-------------------------|
| Costo AS-IS contado | 14 archivos / ~14 clases | 7-8 archivos / 5-7 clases |
| Punto de dolor que ataca | **P-01 + P-02 + P-05 + P-06** (el clúster creacional completo) | **P-04 + P-08** (reglas en el Core) |
| Historia de patrones | Fuerte: creación polimórfica de "vendibles", precios/estrategias por producto — terreno natural creacional + comportamiento | Media: devuelve reglas a la entidad (core strengthening) con menos superficie de patrón |
| Demostración antes/después | "Vender un derivado pasó de tocar 14 archivos a añadir N clases sin tocar existentes" — medible y contundente | "Registrar un evento clínico pasó de tocar 7 a N" — buena, menos espectacular |
| Riesgo de implementación (2 días) | Medio-alto: toca el pipeline de venta + persistencia | Medio: feature aditiva paralela |
| Referencia empírica | La SC-2 del Reto 1 costó ≈17-18 archivos — SC-1 es el mismo tipo de dolor | — |

> [!tip] Recomendación de la IA (D-01 — decide el equipo)
> **SC-1**: es la solicitud que mejor expone la tesis del Reto 2 — ataca el clúster de dolor creacional completo (P-01/02/05/06), permite demostrar la reducción de costo con números contados antes/después, y su dolor es exactamente el que los patrones creacionales y de comportamiento del Anexo A saben tratar. **Contrapartida declarada**: implementarla en 2 días exige que el TO-BE sea conservador en persistencia (los derivados pueden persistirse en tablas nuevas sin tocar la tabla `ventas` de las reses). Si el equipo prefiere minimizar riesgo de implementación, SC-3 es la opción conservadora con P-04/P-08 como anclas.

---

## 6. Hipótesis de anclaje (a evaluar en [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/03-PatronesEvaluados]])

> [!warning] Esto NO es una decisión
> Los 22 patrones del Anexo A se evalúan con ficha completa en el siguiente documento. Este mapa solo organiza los clústeres de dolor para que la evaluación tenga blanco.

| Clúster de dolor | Puntos | Pregunta de diseño que abre | Familías candidatas a evaluar |
|------------------|--------|------------------------------|-------------------------------|
| Creación de objetos y decisión de subtipos | P-01, P-02, P-05, P-06, P-09, P-10 | ¿Puede añadirse un tipo nuevo sin editar 7-14 sitios? ¿Una sola fuente de creación para alta y rehidratación? | Creacionales (Factory Method, Abstract Factory, Builder, Prototype) |
| Reglas y comportamiento en runtime | P-04, P-01 (precios), P-08 | ¿Dónde vive la regla y quién la elige en ejecución? | Comportamiento (Strategy, Template Method, Command) |
| Ensamblaje y composición | P-03, P-07, P-12, P-13 | ¿Dónde se lee cómo colaboran los objetos? | Estructurales (Facade) + Observer — la mayoría ya argumentados como "no intervenir" en este reto |

> [!tip] Navegación
> Base de este inventario: [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/01-AS-IS]] · Evaluación de los 22 patrones: [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/03-PatronesEvaluados]] · Adopción y descartes: [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/04-DecisionesArquitectonicas]] · Plan: [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion1/00-Plan]]
