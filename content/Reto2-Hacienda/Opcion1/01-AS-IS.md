---
tags: [reto2, actividad-1, asis, hacienda]
estado: base-saneada — retrato del código actual
fecha-actualizacion: 2026-09-02
---

# 01 — AS-IS: Comprensión del Sistema Actual

> [!abstract] Propósito
> Retrato fiel del sistema **tal como está hoy el código**, tras cerrar la deuda del Reto 1. Este documento es la línea de base sobre la que se miden los puntos de dolor ([[Reto2-Hacienda/Opcion1/02-PuntosDolor|02-PuntosDolor]]) y el TO-BE ([[Reto2-Hacienda/Opcion1/05-TOBE|05-TOBE]]).

## 0. Linaje del código (de dónde viene este AS-IS)

| Generación | Diagrama | Código |
|---|---|---|
| Gen 1 — Legado (Reto 1 AS-IS) | `01-diagnostico/UML_AS-IS_editable.dia` | Bib_Hacienda + p_mvcHacienda |
| Gen 2 — SolucionSOLID (Reto 1 TO-BE = **este AS-IS**) | `02-diseno/UML_Hacienda_Unificado.dia` | `03-src/SolucionSOLID` (99 .cs) |
| Gen 3 — Reto 2 TO-BE | `diagramas/Reto2_Evolucion_UML.drawio` | Por diseñar en [[Reto2-Hacienda/Opcion1/05-TOBE|05-TOBE]] |

> [!info] Base saneada (2026-09)
> Tras la retroalimentación del Reto 1 se cerró la deuda de la base: entidades con encapsulamiento real (mutación solo por métodos con regla), parámetros de negocio centralizados en `Domain/Reglas/` (`ParametrosRes`, `ParametrosVacuna`, `ParametrosPotrero`, `TransicionesChip`), y la tabla de configuración de subtipos con **fuente única** (`ParametrosRes` → consumida por `CatalogoRes` y por las entidades). Este documento describe el resultado.

## 1. Vista general

Cuatro proyectos back (estilo arquitectónico congelado — no se cambia):

```
Hacienda.Web ──▶ Hacienda.Application ──▶ Hacienda.Domain ◀── Hacienda.Infrastructure
 (MVC, DI)         (servicios, DTOs)        (entidades, VO,     (SQLite + consola)
                                             Reglas, eventos)
```

Dependencias verificadas: Web→todo; Application→Domain; Infrastructure→Domain; Domain→nada. El composition root es `Hacienda.Web/Program.cs` (único punto que conoce la foto completa).

## 2. Reglas de preservación (congeladas por la Líder Técnica)

1. **Mensajes de usuario idénticos** — mismo texto, mismo orden.
2. **Salida de consola idéntica byte a byte** (`DomainEventPublisherConsola`).
3. **Cálculos idénticos** — totales, distancias, estados, umbrales.
4. **Contrato hacia vistas intacto** — `TipoRes`, `Res.Tipo`, bindings y `TempData` sobreviven.
5. **Sin capas ni proyectos nuevos** — Domain se fortalece; Application adelgaza; Infrastructure delega.

## 3. Inventario por capa (responsabilidad real, hoy)

### 3.1 Hacienda.Domain — el núcleo fuerte
- **Entidades encapsuladas**: `Res` (abstracta; `Alimentar`, `InstalarChip`, `AplicarVacuna` con reglas de límite; setters privados; `VacunasAplicadas` read-only), `Ternero/Cebon/Novillo` (solo datos de identidad + `Serializar`), `Chip` (valida su ciclo de vida; transiciones vía `TransicionesChip`), `Potrero` (capacidad desde `ParametrosPotrero`), `Venta` (inmutable), `Vacuna` + `Bacteriana/Viva`.
- **Reglas centralizadas** (`Domain/Reglas/`): `ParametrosRes` (fuente única de rangos/vacunas/pesos por subtipo), `ParametrosVacuna`, `ParametrosPotrero`, `TransicionesChip`.
- **Catálogo de tipos** (`ValueObjects/CatalogoRes`): configuración por subtipo (delegada a `ParametrosRes`), `Parsear`, `MapearDesdePotrero` y `CrearDesdeNombre` — **concentra los puntos de decisión de tipo** (ver §4).
- **Factories**: `FabricaRes` (diccionario interno de creadores), `FabricaVacuna` (método por tipo), `FabricaVenta`, `FabricaPotrero`.
- **Events**: `DomainEvents.cs` (6 eventos) + `IDomainEventPublisher`.
- **Interfaces de repositorio** (puertos): `IRepositorio*` — la inversión la respeta toda la solución.

### 3.2 Hacienda.Application — orquestación + una capa de validación heredada
- Servicios: `GestorReses`, `GestorPotreros`, `ServicioVacunacion`, `ServicioVentas`, `ServicioChip`, `ServicioGeolocalizacion`, `ServicioAutenticacion`, `AutorizadorRbca` — orquestan repos + entidades; `TimeProvider`/`IGuidProvider` inyectados.
- `Validaciones/`: 4 validadores post-construcción (`ValidadorRes/Potrero/Vacuna/Venta`) registrados en `Program.cs:57-60` — estado y solapamiento auditados en [[Reto2-Hacienda/Opcion1/02-PuntosDolor|02-PuntosDolor]] P-04.

### 3.3 Hacienda.Infrastructure
- `Persistence/Sqlite/`: 7 repositorios. SQL intacto (zona excluida). La rehidratación de reses pasa por `CatalogoRes.CrearDesdeNombre` — ver P-05 por la identidad de `Venta`.
- `Events/DomainEventPublisherConsola`: implementación única de `IDomainEventPublisher` — ver P-06.

### 3.4 Hacienda.Web
- Controllers delgados + `Program.cs` como punto de ensamblaje (~31 registros, orden de arranque sensible — P-08).

## 4. Dónde vive la decisión hoy (el mapa que el TO-BE debe atacar)

| Decisión | Dónde se toma hoy | Archivo:línea |
|---|---|---|
| Qué subtipo de res instanciar | Diccionario de creadores | `FabricaRes.cs:17-21` |
| Config/mapeo de subtipos | Tabla + 2 switches | `CatalogoRes.cs` (`MapearDesdePotrero`, `CrearDesdeNombre`) |
| Parámetros por subtipo | Registro único | `ParametrosRes.cs` (fuente única ✓) |
| Estadísticas por tipo | Contadores nombrados a mano | `GestorReses.cs:129-134` |
| Qué vacuna instanciar | Método por tipo en interfaz + ternario en controller | `IVacunaFactory.cs:8-13`, `VacunaController.cs:49-54` |
| Venta válida | Dos verificaciones con umbrales distintos | `FabricaVenta.cs:20` vs `ValidadorVenta.cs:15` |
| Reacción a eventos | Un único destino consola | `DomainEventPublisherConsola.cs:5-11` |

**Lectura**: las entidades y la inversión de dependencias están sanas; lo caro de cambiar está en **dónde se decide la creación y la reacción** — exactamente la capa que este reto ataca con patrones.

## 5. Contratos que el TO-BE debe respetar (restricciones de compatibilidad)

1. `TipoRes`, `Res.Tipo` y los bindings de vistas (`ResDto`, `TempData`) sobreviven.
2. Mensajes de error/éxito congelados (tabla completa en [[Reto2-Hacienda/Opcion1/06-VerificacionSOLID|06-Verificación]]).
3. `IDomainEventPublisher` no cambia de firma — los publicadores actuales dependen de ella.
4. Esquema SQLite: solo adiciones (decisión D-11 en [[Reto2-Hacienda/Opcion1/05-TOBE|05-TOBE]]).
5. `CatalogoRes.Parsear`/`MapearDesdePotrero` siguen disponibles para las vistas y el seeder.

## 6. Navegación

- [[Reto2-Hacienda/Opcion1/02-PuntosDolor|02-PuntosDolor]] — la tabla P-01…P-08 medida sobre ESTE código.
- [[Reto2-Hacienda/Opcion1/05-TOBE|05-TOBE]] · [[Reto2-Hacienda/Opcion1/10-BitacoraIA|10-Bitácora]] B-14/B-15 — el saneo de la base y esta auditoría.
