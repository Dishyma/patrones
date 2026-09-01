---
tags: [reto2, actividad-6, vista-tecnica, hacienda]
estado: en-revision
fecha: 2026-08-30
---

# 09 — Vista para el Equipo de Desarrollo (Actividad 6.b)

> [!abstract] Para quién
> El ingeniero que entra al equipo dentro de seis meses y tiene que hacer un cambio sin romper nada. No estuvo en ninguna reunión.

> [!info] Prueba de que sirve
> Alguien que **no participó en el diseño** debe poder ubicar, **solo con este documento**, dónde hacer un cambio.

---

## 1. Qué patrones hay, dónde viven y cómo se relacionan

| Patrón | Dónde vive (capa/proyecto) | Qué hace | Con quién colabora |
|--------|----------------------------|----------|-------------------|
| **Factory Method** | `Hacienda.Domain/Factories/` (bases `FabricaDeRes`, `FabricaDeVacuna`, `FabricaDeProducto` + creadores concretos) | Punto único de decisión: **qué concreto instanciar** (creación nueva + rehidratación). Elimina switches dispersos. | Es la base del **Template Method** (hereda su esqueleto); sus instancias se registran en `RegistroDe*` que consumen servicios y repos. |
| **Template Method** | `Hacienda.Domain/Factories/FabricaDeRes.cs` (base abstracta), `FabricaDeVacuna.cs`, `FabricaDeProducto.cs` | Escribe **una vez** el pipeline de creación: `validarComunes → construir → exigirReglaDelSubtipo → publicarOcurrido`. Los hooks son **propiedades-dato** del subtipo, no métodos opcionales. | Lo heredan todos los creadores de Factory Method; el último paso llama al **Observer** (publicador). |
| **Builder** | `Hacienda.Domain/Factories/VentaBuilder.cs` | Ensambla `Venta` multi-ítem (pasos: `Iniciar → ConItem → Build()`). Valida invariantes en `Build()`. | Consume ítems (`IVendible`) creados por los **creadores de Factory Method**. Al `Build()`, publica `VentaRealizadaEvent` vía **Observer**. |
| **Observer** | `Hacienda.Domain/Events/IDomainEventHandler.cs`, `Hacienda.Infrastructure/Events/DespachadorDeEventos.cs`, handlers en `Infrastructure/Events/Handlers/` | Desacopla quien publica de quien reacciona. Registro determinista en `Program.cs` (consola primero, handlers SC-1 después). | Publicadores (`GestorReses`, `ServicioVacunacion`, `VentaBuilder`) usan `IDomainEventPublisher` inyectado — **no cambian**. Handlers se registran en `Program.cs`. |

**Diagrama de colaboración (alto nivel):**

```mermaid
flowchart LR
    subgraph Domain["Hacienda.Domain"]
        FM[Factory Method<br/>creadores + registro]
        TM[Template Method<br/>esqueleto de creación]
        BLD[Builder<br/>VentaBuilder]
        IV[IVendible +<br/>ProductoDerivado]
    end
    subgraph App["Hacienda.Application"]
        SRV[Servicios<br/>(orquestan, no deciden)]
    end
    subgraph Infra["Hacienda.Infrastructure"]
        OBS[Observer<br/>Despachador + Handlers]
    end
    subgraph Web["Hacienda.Web"]
        PGM[Program.cs<br/>ensambla todo]
    end
    SRV --> FM
    SRV --> BLD
    TM -.-> FM
    BLD --> FM
    FM --> OBS
    PGM --> FM
    PGM --> OBS
```

---

## 2. Dónde se ensambla el sistema (el único punto que conoce la foto completa)

**`Hacienda.Web/Program.cs`** — es el **composition root** y el único lugar que conoce la foto completa. Allí se registran:

1. **Creadores** (`IEnumerable<FabricaDeRes>`, `IEnumerable<FabricaDeVacuna>`, `IEnumerable<FabricaDeProducto>`) → `RegistroDeReses`, `RegistroDeVacunas`, `RegistroDeProductos`.
2. **Observer**: `IDomainEventPublisher` → `DespachadorDeEventos` + `HandlerConsola` (primero) + `HandlerStockDerivados` (SC-1).
3. **Servicios** (ya existen, ahora inyectan `RegistroDe*` en lugar de factories sueltas).
4. **Repositorios** (delegan rehidratación a `RegistroDeReses.Rehidratar`).

> **Regla de oro:** *Nadie fuera de `Program.cs` nombra un creador concreto, un handler concreto, ni un repositorio concreto.* Todo pasa por abstracciones (`RegistroDe*`, `IDomainEventPublisher`, `IRepositorio*`).

---

## 3. Qué reglas NO se deben romper y por qué

| Regla | Por qué | Qué pasa si se rompe |
|-------|---------|---------------------|
| **Los mensajes de usuario NO cambian** (ni una coma) | Comportamiento congelado = regla del enunciado; −0.5 por caso. | Un caso C-XX falla el diff lado a lado. |
| **La salida de consola es idéntica byte a byte** | `HandlerConsola` reproduce la salida actual; primer handler registrado. | C-04/C-07 fallan; evidencia en video. |
| **`TipoRes`, `Res.Tipo`, `Venta.Res`, bindings de vistas NO cambian** | DEC-09: frontend intacto = contrato con vistas. | `dotnet build Hacienda.Web` falla; vistas no compilan. |
| **No hay `new` de creador concreto fuera de `Program.cs`** | El registro es el único punto de decisión (Factory Method). | Aparece un switch/if por tipo nuevo → OCP rota; P-01/P-02 regresan. |
| **Validación común vive SOLO en el esqueleto (Template Method)** | P-07: regla de monto/edad/edad en 3 capas con 2 umbrales. | Regresión a validación triplicada; venta de $0 se construye y luego se rechaza. |
| **El orden de handlers del Observer es fijo** (consola primero) | Comportamiento congelado en consola. | Salida de consola distinta; R-02 se materializa. |
| **Los setters de `Res` (`Peso`, `Edad`, `Chip`) y `Potrero.Reses` quedan cerrados** | DEC-07: mutación solo por métodos con regla (`Alimentar`, `AplicarVacuna`, `InstalarChip`). | Bypass de reglas de negocio; P-04 regresa. |
| **No se agregan capas/proyectos/frameworks** | Enunciado: "no me cambien el estilo arquitectónico". | Criterio 3 capado a 2.5. |

---

## 4. Guía de "dónde tocar" (artefacto central)

> Para cada tipo de cambio previsible (cubriendo las 3 solicitudes del Anexo B), qué **crear**, qué **modificar** y qué **NO tocar**.

| # | Cambio previsible | Qué CREAR | Qué MODIFICAR | Qué NO TOCAR |
|---|-------------------|-----------|---------------|--------------|
| **T-01** | Agregar un tipo nuevo de **res** (p.ej. `VacaLechera` para SC-1, o futuro `Buey`) | 1. Clase `VacaLechera : Res` (con sus props-dato: `EsEdadValida`, `MaxVacunas*`, `PesoMinimo`, `PesoRecomendadoVenta`, `Tipo = TipoRes.VacaLechera`)<br>2. Creador `FabricaVacaLechera : FabricaDeRes` (hook `Construir` + `Rehidratar`)<br>3. **1 línea en `Program.cs`**: registrar `FabricaVacaLechera` en el `IEnumerable<FabricaDeRes>` | `TipoRes` enum (añadir `VacaLechera`)<br>`Views/Res/Index.cshtml:71-77` y `Views/Venta/Index.cshtml:106` (añadir `case TipoRes.VacaLechera` en el badge — **cambio autorizado** por SC-1) | **NO tocar** `FabricaDeRes` base, `RegistroDeReses`, `GestorReses`, `ServicioVacunacion`, repositorios, `FabricaRes` vieja (se elimina). **NO** agregar switches en servicios/repos. |
| **T-02** | Agregar un tipo nuevo de **vacuna** (p.ej. `Recombinante`) | 1. Clase `Recombinante : Vacuna` (props + `Categoria = VacunaCategoria.Recombinante`)<br>2. Creador `FabricaRecombinante : FabricaDeVacuna` (hook `ValidarPropios` para sus campos específicos)<br>3. Campo(s) extra en `DatosVacuna` (record) para los parámetros de `Recombinante`<br>4. **1 línea en `Program.cs`**: registrar `FabricaRecombinante` en `IEnumerable<FabricaDeVacuna>` | `VacunaCategoria` enum (añadir `Recombinante`)<br>`VacunaController` **ya no tiene `if/else`** — el controlador arma `DatosVacuna` y pasa al servicio; **no toca lógica de tipos** | **NO tocar** `IVacunaFactory` (se elimina), `FabricaVacuna` vieja, `ServicioVacunacion.CrearVacunaBacteriana/Viva` (se eliminan), repositorios (delega a `RegistroDeVacunas`). |
| **T-03** | Vender un **derivado** (SC-1: leche, carne, cuero) — venta multi-ítem | 1. `ProductoDerivado` (abstracto) + `Lacteo`/`Carne`/`Piel` (con `Unidad`, `Stock`, `PrecioUnitario`, `FechaVencimiento` si aplica)<br>2. `FabricaLacteo`/`FabricaCarne`/`FabricaPiel` (heredan `FabricaDeProducto`)<br>3. `VentaBuilder.ConItem(vendible, cantidad)` usa `IVendible` (implementado por `Res` y `ProductoDerivado`) | `Venta` agrega `List<VentaItem>` (ítems) + método `Total()` que suma ítems; `RepositorioVentaSqlite` persiste ítems (nueva tabla `venta_items` o JSON — decisión D-11); `RegistroDeProductos` en `Program.cs` | **NO tocar** `Venta.Res` (se mantiene para compatibilidad lectura legacy — caso C-11); **NO tocar** `FabricaVenta` vieja (se elimina); **NO tocar** `GestorReses`/`ServicioVentas` lógica de negocio (ya delegan al builder). |
| **T-04** | Reaccionar a un **evento nuevo** (p.ej. "stock de leche bajo 50L" → alerta email) | 1. `StockBajoEvent : IDomainEvent` (en `Domain/Events/`)<br>2. `StockBajoHandler : IDomainEventHandler<StockBajoEvent>` (inyecta `IEmailService` o lo que sea)<br>3. **1 línea en `Program.cs`**: registrar handler **después** de `HandlerConsola` | Nada en publicadores (`GestorReses`, `Builder`, etc. ya llaman a `publisher.Publicar(evento)`). | **NO tocar** `DomainEventPublisherConsola` (se transforma en handler), `DespachadorDeEventos` (solo registra), servicios que publican. |
| **T-05** | Cambiar una **regla de validación común** (p.ej. "monto mínimo de venta pasa de >0 a ≥1000") | — (nada nuevo) | `FabricaDeRes/FabricaDeVacuna/FabricaDeProducto` base: método `ValidarComunes` (línea única del esqueleto) | **NO tocar** `Validador*` (ya no existen), `FabricaVenta` vieja, `Dinero` (su validación `<0` sigue; la regla de negocio está en el esqueleto). |
| **T-06** | Cambiar un **mensaje de usuario** (éxito/error) | — | **NO** (congelado por mandato). Si el negocio exige cambio → editar la **tabla de mensajes congelados** y propagar por copia exacta al esqueleto/builder/handlers. | **NO** inventar redacción; copiar exacto de la tabla. |
| **T-07** | Cambio en **base de datos** (esquema, migración) | Solo **aditivo**: nuevas tablas/columnas (`venta_items`, `productos_derivados`, columnas `stock` en productos) | Scripts de migración aditivos en `DatabaseInitializer` o carpeta `Migrations` | **NO** alterar tablas existentes (`reses`, `ventas`, `potreros`, `chips`) ni borrar datos; FKs existentes intactas. |

---

## 5. Deuda declarada (lo que queda pendiente y por qué)

| Deuda | Por qué se deja | Plan de retomar |
|-------|-----------------|-----------------|
| **P-08 RBAC decorativo** (`AutorizadorRbca` + políticas sin call sites) | Activar permisos = cambio de comportamiento observable (−0.5/caso); SC-1 no lo exige. | Retomar cuando el negocio autorice cambio de permisos (nueva solicitud). |
| **P-11 Código muerto** (`Serializar()`, `VacunaVencidaEvent`, DTOs, `IValidarVacuna`, deps muertas, vista huérfana, config muerta) | Limpiarlo ahora agranda el diff del refactor congelado (más superficie de revisión en Act. 4) sin reducir costo de cambio. | Limpieza post-entrega (post-6-sep) como tarea de higiene. |
| **P-12 Efectos secundarios en composición** (`DatabaseInitializer` en fase de registro) | Reordenar arranque = riesgo puro (arranque roto = criterio 4 en 0.0) sin escenario que lo exija. | Si en el futuro se requiere orden estricto, mover a `IHostedService` o similar. |
| **D-11 Esquema de ítems de venta** (JSON vs tabla `venta_items`) | Decisión aditiva de esquema (zona BD); no afecta lógica de dominio. | Decidir al implementar F4; documentar decisión en ADR. |
| **Badge de vista para tipo nuevo** (`Views/Res/Index.cshtml:71-77` sin case `VacaLechera`) | Vistas congeladas salvo cambio autorizado; SC-1 autoriza pantallas nuevas, badge es límite visual. | Decidir al implementar: (a) añadir case autorizado, o (b) dejar "desconocido" y documentar. |

---

## 6. Cómo se ensambla (resumen para el recién llegado)

1. Abre `Program.cs` → es el **mapa del tesoro**.
2. Los **creadores** se registran como `IEnumerable<FabricaDeX>` → `RegistroDeX` los indexa por `TipoAtendido`.
3. Los **servicios** piden `RegistroDeReses`, `RegistroDeVacunas`, `RegistroDeProductos` — **no** factories sueltas.
4. El **Builder** `VentaBuilder` se inyecta en `ServicioVentas` y pide ítems a los registros.
5. Los **eventos** se publican vía `IDomainEventPublisher` inyectado; el `DespachadorDeEventos` despacha a handlers registrados en `Program.cs` (consola primero).
6. Los **repositorios** leen/escriben SQL; para rehidratar `Res` llaman a `RegistroDeReses.Rehidratar` (preserva `Id`).
7. **Nunca** hagas `new FabricaTernero()` fuera de `Program.cs`. Si sientes la tentación de un `switch`/`if` por tipo → **falta un creador en el registro**.

---

## 7. Navegación

- [[Reto2-Hacienda/Opcion1/08-VistaNegocio]] — la misma historia contada para quien aprueba el presupuesto.
- [[Reto2-Hacienda/Opcion1/05-TOBE]] — el diseño completo (diagramas, tabla E-XX, fichas).
- [[Reto2-Hacienda/Opcion1/06-VerificacionSOLID]] — la auditoría de que nada se rompió.
- [[Reto2-Hacienda/Opcion1/07-Riesgos]] — los riesgos formales con dueños y señales.
- [[00-Plan]] — el plan maestro y el estado de cada actividad.