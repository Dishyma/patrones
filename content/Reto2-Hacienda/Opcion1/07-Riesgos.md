---
tags: [reto2, actividad-5, riesgos, plan-de-cambio, hacienda]
estado: en-revision
fecha: 2026-08-30
---

# 07 — Análisis de Riesgos y Plan de Cambio (Actividad 5)

> [!abstract] Propósito
> Registro formal de riesgos del diseño TO-BE ([[Reto2-Hacienda/Opcion1/05-TOBE]]) y plan concreto para ejecutar el cambio sin romper el comportamiento congelado. Cumple el entregable 5.1 del enunciado: mínimo 3 riesgos con condición→consecuencia, P×I, prevención y señal observable, más el plan de cambio.

---

## 1. Registro de Riesgos

| ID | Riesgo (si ocurre X, entonces Y) | Prob (1–5) | Imp (1–5) | Exp (P×I) | Qué hacen para evitarlo | Señal observable |
|----|----------------------------------|-----------|----------|-----------|-------------------------|------------------|
| **R-01** | Si un texto de mensaje cambia al migrar validaciones al esqueleto/builder, entonces el comportamiento observable se rompe (−0.5 por caso según enunciado). | 4 | 5 | 20 | Extraer **tabla de mensajes congelados** (literales exactos del código actual) *antes* de refactorizar; migrar por copia exacta, no reescritura; diff automatizado de salidas en los 12 casos (C-01…C-12). | Algún caso C-01…C-08 falla el diff lado a lado (texto distinto). |
| **R-02** | Si el orden de handlers del Observer cambia, la salida de consola difiere (comportamiento observable = consola). | 3 | 4 | 12 | Registrar `HandlerConsola` **primero** y síncrono en `Program.cs`; caso C-04/C-07 verifica orden y contenido idéntico byte a byte. | Orden o contenido de líneas en consola distinto al AS-IS tras refactorizar. |
| **R-03** | Regresión en el flujo de ventas (SC-1 toca zona congelada; `VentaBuilder` transforma `Venta` sin tocar la lectura legacy). | 3 | 5 | 15 | Captura previa de C-08 completa; compatibilidad de lectura legacy (ítem único) implementada y probada; prueba manual de ventas antes/después en cada iteración. | C-08 falla el diff; ventas históricas no cargan o muestran datos distintos. |
| **R-04** | Código que no compila/ejecuta (regla dura: criterio 4 en 0.0) — cambios en Domain rompen vistas que bindean `Res`, `TipoRes`, `Venta`. | 2 | 5 | 10 | Contrato hacia vistas (DEC-09) verificado por `dotnet build Hacienda.Web` en *cada paso* del refactor; refactor incremental con build incremental. | `dotnet build` falla en `Hacienda.Web` (vistas no compilan). |
| **R-05** | Deriva documento↔código (la heredada del Reto 1) → diagramas no corresponden al código entregado (criterios 3 y 4 capados a 3.0). | 3 | 4 | 12 | Regla archivo:línea en todo documento; regenerar diagramas Mermaid desde el código **antes** de exportar el PDF; revisión cruzada antes de la entrega. | Algún E-XX de [[Reto2-Hacienda/Opcion1/05-TOBE]] §3.2 sin clase real correspondiente en el código final. |
| **R-06** | Cronograma: fecha 6-sep (una semana) con 3 integrantes + SC-1 complejo → entrega tardía (−0.5/hora) o video sin los 4 frentes. | 4 | 4 | 16 | Núcleo (Factory Method + Template Method + Observer) **committing** primera semana; Builder/SC-1 como segunda ola; video con guion + turnos asignados; capturas "antes" desde día 1. | Falta de avance en implementación del núcleo al 2-sep; algún frente sin dueño (D-04). |
| **R-07** | Badge de vista para `VacaLechera` — la vista `Views/Res/Index.cshtml:71-77` no tiene case para el tipo nuevo → UI incompleta percibida. | 5 | 2 | 10 | Declarado como limitación autorizada (SC-1); opción: añadir el `case` al `switch` de la vista como parte del cambio autorizado (decisión al implementar). | Revisión visual de la vista Res con vaca lechera muestra badge vacío/desconocido. |
| **R-08** | Pregunta del evaluador: "¿por qué no activaron los permisos RBAC?" → defensa débil si no está documentada la decisión P-08. | 3 | 3 | 9 | P-08 + [[Reto2-Hacienda/Opcion1/04-DecisionesArquitectonicas]] DEC-06 explican: activar = cambio de comportamiento congelado (−0.5/caso) y no requerido por SC-1. | Pregunta en sustentación sin respuesta preparada. |

---

## 2. Plan de Cambio Concreto

| Fase | Actividad | Responsable | Entregable | Fecha objetivo | Criterio de salida |
|------|-----------|-------------|------------|----------------|-------------------|
| **F0** | Capturas "antes" (C-01…C-08) + tabla de mensajes congelados | Arquitecto de Verificación | Evidencias `04-evidencia/` | Día 1 (ya) | 8 casos + tabla mensajes con diff ✅ |
| **F1** | Núcleo Domain: creadores (FM) + esqueleto (TM) + encapsulamiento | Arquitecto de Dominio | `Hacienda.Domain` + tests unitarios básicos | Día 2–3 | `dotnet build` ok; `FabricaRes` → `FabricaDeRes` base + 3 creadores; `Res` setters cerrados; 0 tests fallan |
| **F2** | Application adelgaza: servicios dejan de mapear/validar → delegan al registro | Arquitecto de Aplicación | `Hacienda.Application` | Día 3–4 | `GestorReses`/`ServicioVacunacion` sin switches ni validación duplicada; inyectan `RegistroDeReses`/`RegistroDeVacunas`; 0 tests fallan |
| **F3** | Infraestructura Observer: `IDomainEventHandler`, `DespachadorDeEventos`, `HandlerConsola` (salida idéntica) | Arquitecto de Infraestructura | `Hacienda.Infrastructure` | Día 4 | Handler consola reproduce salida byte a byte; handlers SC-1 registrables; C-04/C-07 pasan |
| **F4** | SC-1: `ProductoDerivado` + `IVendible` + `VentaBuilder` + 3 creadores + handler stock | Equipo completo | Domain + Application + Infra + Web (builder) | Día 5 | C-10 pasa; venta multi-ítem compila y corre; `dotnet build` ok |
| **F5** | Rehidratación con `Id` estable (`RegistroDeReses.Rehidratar`) + repo delega | Infra | `RepositorioVentaSqlite`/`PotreroSqlite` | Día 5 | C-11 pasa; `Id` estable entre lecturas; `dotnet test` (si existieran) |
| **F6** | Capturas "después" (C-01…C-12) + matriz SOLID + casos | Arquitecto de Verificación | Evidencias + [[Reto2-Hacienda/Opcion1/06-VerificacionSOLID]] | Día 6 | 12 casos ✅; matriz con evidencia ✅ |
| **F7** | Documento PDF (≤15 pág) + video 20 min (4 frentes) | Arquitecto de Comunicación + todos | PDF + video | Día 7 (6-sep) | PDF ≤15 pág, índice, trazable; video 4 frentes, <20 min, audio OK |

> **Nota sobre D-04 (frentes entre 3 integrantes):** se resuelve en la primera reunión de F0 asignando un frente a dos integrantes (p.ej., Arquitecto de Verificación + Arquitecto de Riesgos) y documentando la decisión en [[Reto2-Hacienda/Opcion1/10-BitacoraIA]].

---

## 3. Mitigaciones Transversales

- **Regla de oro**: *primero capturar, después refactorizar*. Nada se cambia sin evidencia "antes".
- **Diff obligatorio**: cada caso C-XX tiene captura "antes" y "después" comparada con `diff -u` en CI local.
- **Build gate**: `dotnet build Hacienda.Web` **en cada commit** del refactor; si falla, revert inmediato.
- **Diario de cambios**: cada sesión registra en [[Reto2-Hacienda/Opcion1/10-BitacoraIA]] qué se tocó, qué caso validó, qué mensaje confirmó.

---

## 4. Navegación

- [[Reto2-Hacienda/Opcion1/05-TOBE]] — el diseño que este registro protege.
- [[Reto2-Hacienda/Opcion1/06-VerificacionSOLID]] — la evidencia de que el comportamiento no cambia.
- [[Reto2-Hacienda/Opcion1/10-BitacoraIA]] — registro de decisiones de este plan (B-12).