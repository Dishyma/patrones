---
title: "Reto 2 — Bitácora de decisiones frente a la IA"
tags:
  - arquitectura-software
  - reto2
  - patrones-diseno
  - hacienda
  - bitacora
estado: "viva — se actualiza al cierre de cada actividad"
---

# 10 — Bitácora de decisiones frente a la IA

> [!abstract] Propósito
> Registro real de cómo trabajó el equipo con la herramienta de IA durante el Reto 2, exigido por el enunciado (Actividad 2, Entregable 2.2): **mínimo diez decisiones** al cierre. Se auditarán registros al azar en la sustentación: cada fila debe poder defenderse con evidencia.
>
> **Reglas de esta bitácora**: no se inventan registros; lo que aún no ha ocurrido queda como *pendiente*; las decisiones son siempre del equipo. Valores posibles: **Aceptada · Corregida · Rechazada · Fue idea nuestra**.

## Formato (el exigido por el enunciado)

| ID | Qué consultaron | Qué propuso la herramienta | Qué hicieron | Argumento propio y evidencia |
|----|-----------------|---------------------------|--------------|------------------------------|

---

## Registros

| ID | Qué consultaron | Qué propuso la herramienta | Qué hicieron | Argumento propio y evidencia |
|----|-----------------|---------------------------|--------------|------------------------------|
| B-01 | Cómo organizar el trabajo del Reto 2 (actividades del enunciado → documentos) | Mapear cada actividad a archivos fuente (00–10) en Obsidian, con la IA como arquitecto asistente y gates de aprobación por actividad | **Corregida** — el equipo ajustó la división: la IA produce los documentos fuente y el equipo destila el PDF, implementa el código y decide | Evita que la sustentación dependa de material que el equipo no domina; ver [[Reto2-Hacienda/Opcion1/00-Plan]] §6, D-04 |
| B-02 | Dónde ubicar los documentos de trabajo | Carpeta nueva `Reto2-Hacienda/` en la bóveda de Obsidian, ignorando cualquier trabajo previo | **Aceptada**, con decisión previa del equipo de partir desde cero (la carpeta existente `Trabajo2/` quedó excluida por directiva del equipo) | Ver [[Reto2-Hacienda/Opcion1/00-Plan]] §6, D-03 |
| B-03 | Cómo tratar el requisito "al menos tres puntos de dolor encontrados sin IA" | No simularlo: documentar el inventario completo como colaboración y que el equipo satisfaga ese requisito con su propia lectura | **Aceptada** — no se fabrican orígenes; la bitácora registra solo eventos reales | Directiva del equipo; ver [[Reto2-Hacienda/Opcion1/00-Plan]] §5, Fase 2 |
| B-04 | Cómo encuadrar los señalamientos del profesor al Reto 1 (capas de más, lógica fuera del dominio, fábricas mal hechas, encapsulación débil) | Tratarlos como el inventario de deuda técnica que el TO-BE debe pagar, concentrado en el Core | **Fue idea nuestra** — el equipo definió el encuadre de deuda técnica heredada; la IA lo formalizó en el plan | Ver [[Reto2-Hacienda/Opcion1/00-Plan]] §1.1 (regla de enfoque) y §6, D-05 |
| B-05 | Alcance del análisis respecto del frontend | Incluirlo en el mapa AS-IS para completitud del flujo | **Corregida** — el equipo decidió excluirlo: el frontend final será el mismo; el trabajo es la colaboración de objetos del backend | Ahorra esfuerzo donde no hay decisión de patrones; ver [[Reto2-Hacienda/Opcion1/00-Plan]] §6, D-05 |
| B-06 | Qué solicitud de cambio (SC) implementar en el TO-BE | Análisis de impacto contado: SC-1 = 14 archivos/~14 clases y ataca el clúster creacional completo (P-01/02/05/06); SC-3 = 7-8 archivos, más conservadora. Recomendación: SC-1 | **Aceptada** — el equipo eligió SC-1 | Anexo B exige SC distinta a la del Reto 1 (SC-2 ya integrada: ≈17-18 archivos); conteos y comparación en [[Reto2-Hacienda/Opcion2/02-PuntosDolor]] §5 |
| B-07 | Qué puntos de dolor intervenir y cuáles declarar deuda | Matriz de 18 puntos: 9 a intervenir, 9 como deuda; "no intervenir" oficiales argumentados: P-03, P-07, P-17 | **Aceptada** — el equipo aprobó "los 9" | Decisión registrada en [[Reto2-Hacienda/Opcion2/02-PuntosDolor]] §1; argumentos en §4 |
| B-08 | Qué patrón usar para el clúster de creación (P-02/05/06) | Factory Method real (creadores por subtipo + registro en el punto de ensamblaje), descartando Abstract Factory (advertencia del Anexo A) y Prototype (rompería P-08) | **Aceptada** | Fichas con alternativas en [[Reto2-Hacienda/Opcion2/03-PatronesEvaluados]] §1 y §4; ADR-01 en [[Reto2-Hacienda/Opcion2/04-DecisionesArquitectonicas]] |
| B-09 | Cómo hacer legible la colaboración del subsistema de venta sin absorber reglas | Facade como **rol del `ServicioVentas` existente** (no clase nueva), con límite declarado y métrica de control ("si la fachada contiene un if de negocio, se pasó") | **Aceptada** | ADR-02 en [[Reto2-Hacienda/Opcion2/04-DecisionesArquitectonicas]]; ficha 2 en [[Reto2-Hacienda/Opcion2/05-TOBE]] §4 |
| B-10 | Cómo evitar que SC-1 reproduzca la bifurcación por tipo (el defecto P-06) | Strategy: `MontoManual` (reses — preserva el comportamiento congelado exacto) + `PrecioUnitario` (derivados — nuevo comportamiento autorizado) | **Aceptada** | ADR-03 en [[Reto2-Hacienda/Opcion2/04-DecisionesArquitectonicas]]; ficha 3 en [[Reto2-Hacienda/Opcion2/05-TOBE]] §4 |
| B-11 | Observer para P-12 (eventos a medio instalar) | Propuesta y autocensura: encaja técnico, pero rediseña el ensamblado de mensajes visibles = comportamiento congelado (L1) | **Rechazada por la propia IA** — deuda declarada con condición de activación | Ficha 18 de [[Reto2-Hacienda/Opcion2/03-PatronesEvaluados]]; primer patrón a incorporar si se autoriza |
| B-12 | Cómo modelar los derivados de SC-1 (lácteos/carne/piel) | UNA clase `ProductoDerivado` configurada por datos (`TipoDerivado`, nombre, precio) — no subtipos: la variación es de datos, no de comportamiento | **Aceptada** | Si apareciera comportamiento propio por derivado (perecederos), ahí sí subtipos + creadores — umbral declarado en [[Reto2-Hacienda/Opcion2/05-TOBE]] |
| B-13 | Dónde vive la traducción TipoPotrero→TipoRes (P-09) tras el TO-BE | Eliminar `TipoPotrero` tocaría BD y vistas congeladas (D-05): se conserva el enum y queda UNA traducción declarativa alimentada por los creadores; mueren el switch y los contadores | **Aceptada** | Trade-off declarado en [[Reto2-Hacienda/Opcion2/05-TOBE]] E-01/E-07; sinergia con regla "prohibido switch de tipo" |
| B-14 | Cómo verificar SOLID sin adornar la matriz | 3 tensiones declaradas con compensación (rehidratación sin re-validar; fachada sin ifs de negocio; estrategias emparejadas en registro) en vez de matriz toda verde | **Aceptada** | Matriz 15 celdas con evidencia por celda: [[Reto2-Hacienda/Opcion2/06-VerificacionSOLID]] §1 |
| B-15 | Cómo evidenciar el comportamiento congelado | 12 casos (8 del Reto 1 + 4 nuevos que recorren los patrones), con el caso 10 exigiendo salida idéntica al 5 por el contrato nuevo, y el 12 protegiendo datos legados | **Aceptada** | Protocolo con sitios de construcción de mensajes citados: [[Reto2-Hacienda/Opcion2/06-VerificacionSOLID]] §2 |
| B-16 | Cómo proteger la entrega contra el calendario | Plan de 5 fases que compila al final de cada una + criterio de parada (4/9 mediodía): congelar alcance y proteger lo verificado | **Aceptada** | [[Reto2-Hacienda/Opcion2/07-Riesgos]] §2; riesgo R-03 (P×I 15) |
| B-17 | Redacción de la vista de negocio sin jerga | Vista completa sin ninguna palabra técnica, con checklist de palabras prohibidas y protocolo de prueba con persona no técnica para el video | **Aceptada** — prueba pendiente (video) | [[Reto2-Hacienda/Opcion2/08-VistaNegocio]]; traducción técnica en [[Reto2-Hacienda/Opcion2/09-VistaTecnica]] §6 |

**Total: 17 registros** (mínimo del enunciado: 10). Perfil: 8 aceptadas · 1 aceptada con condición · 2 corregidas por el equipo · 2 ideas propias del equipo · 1 rechazada por la propia IA · 3 pendientes menores (prueba de video, PDF, sustentación).

---

## Pendientes de cierre (ocurren → se registran)

- [x] Selección de puntos de dolor y del "no se interviene" (B-07, B-08) ✓
- [x] Decisión D-01: SC-1 (B-06) ✓
- [x] Adopción/descarte de patrones (B-08 a B-12) ✓
- [x] Declaración de principios tensionados y compensación (B-14) ✓
- [x] Decisiones sobre riesgos y plan de cambio (B-16) ✓
- [ ] Prueba de la vista de negocio con persona no técnica (evidencia del video) → completar el checklist de [[Reto2-Hacienda/Opcion2/08-VistaNegocio]]
- [ ] Correcciones o rechazos que surjan durante la implementación del TO-BE (registrarlos acá con el mismo formato)

> [!tip] Navegación
> Plan: [[Reto2-Hacienda/Opcion1/00-Plan]] · Dolor: [[Reto2-Hacienda/Opcion2/02-PuntosDolor]] · Patrones: [[Reto2-Hacienda/Opcion2/03-PatronesEvaluados]] · Decisiones: [[Reto2-Hacienda/Opcion2/04-DecisionesArquitectonicas]]
