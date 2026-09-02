---
tags: [reto2, decisiones, arquitectura, hacienda]
estado: actualizado tras auditoría de base sana (2026-09-02)
---

# 04 — Decisiones Arquitectónicas del Equipo

> [!abstract] Propósito
> Registro ejecutivo de lo decidido (DEC) y lo pendiente de decisión del equipo (D-XX). Cada decisión cita su evidencia. Las pendientes son las que EL EQUIMO valida — la IA propone, el equipo decide.

## 1. Decisiones tomadas (DEC)

| ID | Decisión | Justificación y evidencia |
|----|----------|---------------------------|
| DEC-01 | **Solicitud elegida: SC-1 (derivados)** — la más difícil deliberadamente | Ancla los dolores más caros (P-01, P-03, P-04, P-06, P-13) y maximiza el contraste AS-IS/TO-BE. Bitácora B-05 |
| DEC-02 | **Saneamiento de la base ANTES de diseñar** (encapsulamiento, `Reglas/` fuente única, `IChip` fuera) | Feedback directo del profesor sobre el Reto 1; la Líder Técnica fijó "SOLID no se toca" — entonces primero quedó intachable. B-14/B-15. Build 0w/0e |
| DEC-03 | **Estandarización en 3 destinos**: `Reglas/ParametrosX` (recalibrables) · VO/entidad dueña (invariantes) · entidades (solo comportamiento) | Auditoría de estandarización: `CatalogoRes` consumía duplicado → fuente única `ParametrosRes`. Evita que "cada cosa se haga de una forma" |
| DEC-04 | **Diagrama `UML_Hacienda_Unificado.dia` regenerado desde el código** (no dibujado a mano) | 118 clases / 49 relaciones = retrato exacto, re-generable ante cualquier cambio del código. Linaje documentado en [[Reto2-Hacienda/Opcion1/01-AS-IS|01-AS-IS]] §0 |
| DEC-05 | **D-05 resuelta: Variante A** — producción propia con `FabricaVacaLechera` | El enunciado define SC-1 como derivados **del ganado**; sin producción propia el modelo queda incoherente. Es además la variante que mide la promesa OCP (6 archivos → 1+1). B-10 |
| DEC-06 | **Set de patrones propuesto: FM + TM + Builder + Observer** (+ Composite a validar en D-12) | Cada uno anclado a dolor medido (matriz en [[Reto2-Hacienda/Opcion1/03-PatronesEvaluados|03-Patrones]] §2). Template Method es el más defendible: 5 dolores, una causa |
| DEC-07 | **Opción 2 del análisis: obsoleta** | Desincronizada tras el saneo; la rama canónica es Opción 1. No se mantiene doble ruta |
| DEC-09 | **Regresión funcional detectada y corregida**: validación de edad perdida en refactor intermedio, restaurada con mensaje idéntico al commit base | El equipo cuestionó el hallazgo ("¿eso no era funcionalidad del original?"); verificación git (`log -S EsEdadValida`) + legado confirmó la regresión. Refuerza la regla "comportamiento congelado" y alimenta D-06 (hallazgo propio sin IA) — ver 02-PuntosDolor §2bis y bitácora B-16 |
| DEC-08 | **Dolores congelados con argumento**: P-07 (RBAC), P-08 (orden arranque), P-15 (moneda) | Cumplen la regla del enunciado "≥1 no se interviene": el remedio cuesta más que el problema o cambia comportamiento congelado |

## 2. Decisiones PENDIENTES del equipo (D-XX)

| ID | Decisión pendiente | Contexto para decidir | Recomendación IA |
|----|--------------------|----------------------|------------------|
| **D-12** | **¿Adoptar Composite como 5º patrón?** | Solo gana si SC-1 incluye **canastas/combos** de derivados (ítem compuesto recursivo). Si la venta es lista plana, Builder basta y Composite es sobre-ingeniería penalizable | Descartar salvo que el equipo confirme canastas en el alcance |
| **D-11** | Persistencia multi-ítem de `Venta`: columna JSON vs tabla `venta_items` | Zona BD: solo adiciones. JSON = 1 columna, sin migración; tabla = consultable por producto (¿stock SC-1 la necesita?) | Tabla `venta_items` si el handler de stock consulta; JSON si solo se lista |
| **D-04** | Distribución de los 4 frentes entre los integrantes | Roles del enunciado: Líder / Verificación / Riesgos / Comunicación | Repartir por fortaleza, no por afinidad |
| **D-06** | ≥3 hallazgos propios **sin IA** (protocolo Act. 1 §0) | El enunciado exige mínimo 3 dolores encontrados a mano; los ◆ del catálogo son semilla propia ya registrada | Cada integrante relee un flujo distinto (crear res, vacunar, vender) y anota lo suyo |

## 3. Navegación

- [[Reto2-Hacienda/Opcion1/02-PuntosDolor|02-PuntosDolor]] — catálogo P-01…P-15 con candidatos por dolor.
- [[Reto2-Hacienda/Opcion1/03-PatronesEvaluados|03-Patrones]] — las 22 evaluaciones y la matriz de elección.
- [[Reto2-Hacienda/Opcion1/10-BitacoraIA|10-Bitácora]] — B-14/B-15 y el origen de cada decisión.
