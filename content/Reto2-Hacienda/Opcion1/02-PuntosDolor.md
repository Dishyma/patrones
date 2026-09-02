---
tags: [reto2, actividad-1, dolor, hacienda]
estado: catálogo completo sobre base sana (auditoría 2026-09-02)
---

# 02 — Puntos de Dolor de la Arquitectura Actual (Catálogo completo)

> [!abstract] Propósito
> **Catálogo amplio** de puntos donde el diseño, aun siendo correcto, sigue siendo caro de cambiar — medido contando clases y archivos reales. Cada dolor lista los **patrones candidatos** que lo atacarían: el equipo valida cuáles justifican adopción (regla PILAS del enunciado: patrón sin dolor anclado = sobre-ingeniería penalizada).

> [!info] Cómo usar este catálogo
> ★ = candidato fuerte · ○ = candidato posible · ✗ = evaluado y descartado (ver [[Reto2-Hacienda/Opcion1/03-PatronesEvaluados|03-Patrones]]). Los congelados llevan su argumento.

## 1. Tabla resumen

| ID | Dónde (archivo / clase) | Qué lo hace rígido o caro | Cuánto cuesta hoy | Candidatos |
|----|------------------------|---------------------------|-------------------|------------|
| **P-01** | `FabricaRes`, `CatalogoRes`, `ParametrosRes`, `GestorReses`, `TipoRes` | Agregar un subtipo = editar 5 puntos + la clase nueva: diccionario de creadores (`FabricaRes.cs:17-21`), config + switch de instanciación (`CatalogoRes`), parámetros, contador de estadísticas (`GestorReses.cs:129-134`), enum | **6 archivos / 2 capas por subtipo** | ★ Factory Method · ✗ Abstract Factory · ✗ Prototype |
| **P-02** | `IVacunaFactory`, `FabricaVacuna`, `VacunaController` | La decisión "qué vacuna" vive partida: método por tipo en la interfaz (`IVacunaFactory.cs:8-13`), ternario por string en el controller (`VacunaController.cs:49-54`), strings `"Bacteriana"/"Viva"` repetidos en ViewBag ×5 | **~8 clases / 5 archivos por categoría** | ★ Factory Method · ○ Strategy · ✗ Command |
| **P-03** | `Venta`, `FabricaVenta`, `RepositorioVentaSqlite` | `Venta` nace soldada a una `Res` (`Venta.cs:10`); venta multi-ítem (SC-1) reescribe constructor, factory, repo y vista | **~6 clases / 6 archivos** | ★ Builder · ○ Composite · ✗ Prototype |
| **P-04** | `Validador*` + fábricas + servicios | Pipeline "fabricar → validar" disperso y re-hecho a mano en cada servicio (`GestorReses.cs:45-48`, `ServicioVentas.cs:44-47`); **umbrales contradictorios**: `FabricaVenta.cs:20` rechaza `monto < 0` pero `ValidadorVenta.cs:15` rechaza `Monto <= 0`; `ValidadorRes`/`ValidadorPotrero` re-chequean invariantes que ctors/VOs ya garantizan (validar lo imposible) | **4 validadores / 4 archivos desincronizables** | ★ Template Method · ○ Chain of Resp. · ✗ Strategy |
| **P-05** | `RepositorioVentaSqlite` | Rehidrata cada venta fabricando la `Res` con **GUID nuevo por lectura** (`RepositorioVentaSqlite.cs:39`) — identidad inestable | **1 archivo** que contamina toda lectura | (colateral de Factory Method — no patrón propio) |
| **P-06** | `DomainEventPublisherConsola`, `DomainEvents` | Publicación con un único destino posible (`:5-11`, todo a consola); `VacunaVencidaEvent` definido con 0 publicaciones (verificado); no hay forma de reaccionar sin editar al publicador | **1 clase** que bloquea N consumidores | ★ Observer · ✗ Mediator |
| **P-07** | `AutorizadorRbca`, políticas RBAC | Registrado y funcional pero sin llamadas en el flujo real | — | **No se interviene** (congelado: activarlo cambia comportamiento observable; el remedio cuesta más que el problema) |
| **P-08** | `Program.cs` | Composition root con efectos secundarios de arranque; reordenar registros cambia comportamiento | — | **No se interviene** (zona congelada; el TO-BE solo añade registros al final) |
| **P-09** | `Res`+subtipos, `Vacuna`+subtipos | `Serializar()` copiado en 5 subtipos con el mismo formato pipe (`Ternero.cs:19`, `Cebon.cs:18`, `Novillo.cs:18`, `Bacteriana`, `Viva`); cambiar formato = 5 ediciones | **5 implementaciones / 5 archivos** | ★ Template Method · ○ Visitor · ✗ Decorator |
| **P-10** | `ServicioVacunacion` | Lotes gemelos: `CrearLoteVacunaBacteriana` (`:61-88`) ≡ `CrearLoteVacunaViva` (`:90-117`), 27 líneas que difieren en 3; los métodos simples `CrearVacuna*` (`:33-59`) repiten el sesgo | **4 métodos / 1 archivo** (2 gemelos + 2 simples) | ★ Template Method · ○ Strategy · ✗ Command |
| ~~P-11~~ | ~~`Res.EsEdadValida`~~ | **REGRESIÓN corregida (2026-09-02)** — no era dolor: la validación existía en el commit base del Reto 1 (`FabricaRes.cs:35` en HEAD) y en el legado (`Ternero/Cebon/Novillo.cs:22`, `Potrero.cs:86`); se perdió en una refactorización intermedia y se restauró con mensaje idéntico (ver §2bis) | — | ~~corregido — restaurada~~ |
| **P-12** | `TipoRes` ↔ `TipoPotrero` | Dos enums paralelos que deben evolucionar juntos + switch de mapeo (`CatalogoRes.MapearDesdePotrero`); agregar un subtipo = tocar 2 enums + el mapeo | **3 puntos de edición extra** (se suma a P-01) | (colateral de Factory Method; costo declarado) |
| **P-13** | Las 4 fábricas (`Fabrica*`) | **Cuatro idiomas distintos para el mismo problema**: `FabricaRes` (diccionario + valida nombre), `FabricaVacuna` (métodos por tipo + validación común privada), `FabricaVenta` (valida monto + **`TimeProvider` por parámetro de método** — `IVentaFactory.cs:8`, única que filtra infra en su firma), `FabricaPotrero` (solo ctor). Cada tipo nuevo se agrega "según la fábrica que le tocó" | **4 clases / 4 archivos sin esqueleto común** | ★ Factory Method + Template Method (esqueleto unificado) · ★ Builder (venta) |
| **P-14** | `GestorReses.AgregarRes` vs `AlimentarRes` | Reacciones duplicadas entre flujos: el par `PesoMinimoEvent`+mensaje está escrito 2 veces (`GestorReses.cs:56-58` y `:99-101`), igual `PesoVentaEvent` (`:61-63` / `:104-106`); una reacción nueva se escribe 2 veces y puede desincronizarse | **2 flujos / 1 archivo** duplicando 4 reacciones | ★ Observer (la reacción vive en handlers) · ○ Template Method (hook post-mutación) |
| **P-15** | `Dinero` | Moneda por defecto `"COP"` quemada (`Dinero.cs:13`) | **1 literal** | **No se interviene** (dominio moneda única; externalizarlo no responde a ninguna solicitud) |

## 2. Fichas de los dolores nuevos de esta auditoría ◆

### §2bis · Regresión funcional detectada y corregida ◆ (hallazgo del equipo, sin IA)
**Qué pasó.** El equipo cuestionó si la regla de edad era funcionalidad heredada. Verificación con `git log -S EsEdadValida` + `git show HEAD`: el commit base del Reto 1 **sí validaba** (`FabricaRes.cs:35`, mensaje `$"La edad {edad} no es válida para {tipo}. Rango: ..."`), y el legado Bib_Hacienda también (`Ternero/Cebon/Novillo.cs:22` con `ReglaRes.edad_max_*`, y `Potrero.cs:86`). La llamada se perdió en una refactorización intermedia del catálogo de tipos.
**Corrección.** Restaurada en `FabricaRes.Crear` con el mensaje **exacto** del commit base (incluido `DescribirRango`). Build 0w/0e. Barrido de regresiones adicionales en el diff de 50 archivos: solo evoluciones equivalentes (verificado línea por línea).
**Lección.** "Comportamiento congelado" también se audita contra el git history, no solo contra la memoria. El esqueleto del TO-BE (Template Method) consolidará esta validación como paso sellado.

### P-13 · Cuatro fábricas, cuatro idiomas
**Evidencia.** `FabricaRes.cs:17-21` (diccionario de lambdas), `FabricaVacuna.cs:17-32` (método por tipo + `ValidarParametrosComunes`), `FabricaVenta.cs:17-20` (valida monto; firma con `TimeProvider reloj` en `IVentaFactory.cs:8`), `FabricaPotrero.cs` (solo ctor). Ninguna comparte esqueleto: la validación común está en algunas, el reloj llega distinto, la decisión de tipo vive en sitios distintos.
**Costo.** Cada fábrica nueva (SC-1 necesita la de productos) arranca "estilo libre" — el catálogo de inconsistencias crece con cada solicitud.
**Candidato natural.** Base `FabricaDeX` (Creator + Template Method) con esqueleto `validar comunes → construir (hook) → regla del subtipo → publicar`; el reloj entra por ctor como en todo el sistema.

### P-14 · Reacciones copiadas entre flujos
**Evidencia.** `GestorReses.cs:56-58` ≡ `:99-101` (PesoMinimo), `:61-63` ≡ `:104-106` (PesoVenta). El "si pasa X, publica Y y arma mensaje Z" está escrito dos veces en el mismo archivo.
**Costo.** 4 reacciones × 2 flujos; la próxima reacción (SC-1: stock bajo) se escribe en N flujos a mano.
**Candidato natural.** Observer: el flujo solo publica; los handlers (consola primero, stock después) reaccionan una única vez cada uno.

## 3. Anclaje dolor → SC-1 (solicitud elegida)

| Solicitud | Dolores que dispara | Costo sin rediseño |
|---|---|---|
| **SC-1 · Derivados** (elegida) | P-01, P-03, P-04, P-06, P-10, P-13 (la fábrica de productos es la 5ª con idioma nuevo) | ~20 clases / ~15 archivos |
| SC-3 · Historia clínica (futura) | P-03, P-06, P-14 | ~9-12 clases aditivas |

## 4. Resumen para elegir (lo que el equipo validará)

- **Dolores intervenibles**: P-01…P-06, P-09, P-10, P-12, P-13, P-14 (11) · P-11 reclasificada como regresión corregida (§2bis)
- **Congelados con argumento**: P-07, P-08, P-15 (3) — cumplen la regla "≥1 no se interviene"
- **Colaterales sin patrón propio**: P-05, P-12 (curados de rebote por Factory Method)
- Cada ★/○ de la tabla tiene su evaluación completa (adoptado/descartado + por qué) en [[Reto2-Hacienda/Opcion1/03-PatronesEvaluados|03-PatronesEvaluados]].

## 5. Navegación

- [[Reto2-Hacienda/Opcion1/01-AS-IS|01-AS-IS]] — el retrato del código.
- [[Reto2-Hacienda/Opcion1/03-PatronesEvaluados|03-Patrones]] — la decisión por patrón, anclada a esta tabla.
