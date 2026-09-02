---
tags: [reto2, actividad-5, riesgos, hacienda]
estado: v2 — riesgos del TO-BE v2 (2026-09-02)
---

# 07 — Registro de Riesgos (Actividad 5)

> [!abstract] Propósito
> Riesgos reales de implementar el TO-BE ([[Reto2-Hacienda/Opcion1/05-TOBE|05-TOBE]]), en formato del enunciado: *si ocurre X, entonces Y* · Prob (1-5) · Imp (1-5) · **Exposición = P×I** · cómo evitar · **señal observable** de materialización. Ordenados por exposición. Mínimo exigido: 3 → entregamos 8.

| ID | Riesgo (si ocurre X, entonces Y) | P | I | P×I | Qué hacemos para evitarlo | Señal observable |
|----|----------------------------------|---|---|-----|--------------------------|------------------|
| **R-01** | **El cronograma colapsa**: quedan 4 días para implementar + video + PDF con los frentes sin repartir (D-04 abierto) → se entrega análisis sin implementación y la nota de implementación se repite | 4 | 4 | **16** | Repartir los 4 frentes HOY; hito diario por actividad; implementación por fases (E-01…E-07 primero) | Una actividad sin artefacto al cierre del día; "lo hacemos mañana" dos días seguidos |
| **R-02** | **Regresión silenciosa en la refactorización**: una llamada funcional se pierde al mover código (ya ocurrió: DEC-09, `EsEdadValida`) → funcionalidad heredada deja de operar sin que nadie lo note | 3 | 4 | **12** | Barrido sistemático `git diff` línea a línea + un caso C-XX por cada llamada congelada (C-01…C-18) | Un caso que hoy pasa y falla tras el merge; `git log -S <método>` sin llamadas |
| **R-03** | **Un mensaje congelado cambia de texto**: al migrar validaciones al esqueleto/builder se reescribe un string → −0.5 por caso según la Líder Técnica | 2 | 5 | **10** | Tabla congelada (06 §C-01…C-18) + diff automatizado de capturas antes/después con seed y reloj fijos | Diff no-vacío entre capturas; cualquier string nuevo en el esqueleto que no esté en la tabla |
| **R-04** | **El despachador pierde determinismo**: el orden de handlers varía (hash de DI) → la consola imprime en desorden y el "byte a byte" se rompe | 2 | 4 | **8** | Orden de registro explícito (consola 1º) en `Program.cs`; caso C-16 con dos corridas | Líneas de consola en orden distinto entre corridas |
| **R-05** | **Un creator futuro "no puede" cumplir el esqueleto** y alguien implementa el hook vacío → un subtipo mudo que rompe la sustituibilidad | 2 | 4 | **8** | Hooks como propiedades-dato (nadie puede "no poder"); revisión de pares: cero overrides vacíos; C-17 | Override vacío o que lance "no soportado" dentro de un creator |
| **R-06** | **El esquema multi-ítem elegido (D-11) resulta el equivocado**: JSON y el handler de stock necesita consultar por producto (o tabla y nunca se consulta) → retrabajo en zona BD | 3 | 3 | **9** | Decidir D-11 con una pregunta: ¿el handler de stock consulta ventas por producto? Solo adiciones (sin migración destructiva) | Query con N+1 en el handler; reportes de stock lentos |
| **R-07** | **Añadir registros a `Program.cs` rompe el arranque** (P-08): un registro nuevo se cuela antes del seeder/inicialización → startup falla o seed duplicado | 2 | 4 | **8** | Solo append al FINAL del composition root; prueba de humo de arranque tras cada fase | Excepción en startup; filas duplicadas tras el seed |
| **R-08** | **Sobrecarga percibida (sobre-ingeniería)**: 16-20 clases nuevas espantan en revisión → el evaluador lee inflado lo que es decisión | 2 | 3 | **6** | Set en 4 patrones (Composite solo con canastas — D-12); cada ficha declara costo; las vistas separan público | Ningún integrante ubica una clase nueva en el diagrama sin buscarla |

> [!tip] Prioridad de mitigación (orden de exposición)
> **R-01 → R-02 → R-03** concentran el 60% de la exposición y TODOS son de proceso, no de código: repartir frentes, auditar diffs, congelar capturas. Se mitigan hoy, gratis.

## 1. Plan de cambio (fases sugeridas)

| Fase | Alcance | Riesgos cubiertos | Verificación |
|------|---------|-------------------|--------------|
| F1 | `FabricaDeRes` + creators + `RegistroDeReses` (E-01, E-02) + rehidratación con Id (E-06) | R-02, R-05, R-07 | C-01, C-02, C-03, C-13, C-18 |
| F2 | `FabricaDeVacuna` + `DatosVacuna` + `CrearLote` común (E-03, E-09) | R-02, R-03 | C-04…C-07 |
| F3 | Validadores fuera + esqueleto sellado completo (E-04, E-10) | R-03, R-05 | C-02, C-15, C-18 |
| F4 | `VentaBuilder` + `IVendible` + multi-ítem (E-08) + D-11 resuelto | R-06 | C-08, C-09, C-14, C-15 |
| F5 | `DespachadorDeEventos` + `HandlerConsola` (E-07) + reacciones a handlers (E-05) | R-04 | C-12, C-16 |
| F6 | SC-1: productos + `FabricaVacaLechera` (E-12, E-13) + `HandlerStockDerivados` | R-01, R-08 | C-14, C-16, C-17 |

Cada fase: build limpio + casos de su fila en verde → siguiente fase. Rollback = revert de la fase (commits por fase).

## 2. Navegación

- [[Reto2-Hacienda/Opcion1/05-TOBE|05-TOBE]] — lo que se va a cambiar · [[Reto2-Hacienda/Opcion1/06-VerificacionSOLID|06-Verificación]] — los casos que blindan cada fase.
