---
tags: [reto2, actividad-2, bitacora, ia, hacienda]
estado: viva — se actualiza en cada sesión
fecha-actualizacion: 2026-08-30
---

# 10 — Bitácora de Decisiones frente a la IA

> [!abstract] Propósito
> Registro vivo de cómo trabajamos con la herramienta (entregable 2.2 del enunciado, mínimo 10 registros). **Nunca se inventan registros**: cada fila corresponde a una consulta/propuesta real de este trabajo. La sustentación auditará filas al azar — cada una debe poder defenderse con evidencia.

> [!info] Cómo leer las decisiones
> **Aceptamos** = la propuesta de la IA entró tal cual · **Corregimos** = la propuesta entró ajustada por el equipo · **Rechazamos** = el equipo la descartó con argumento · **Fue idea nuestra** = nació del equipo (la IA solo dio forma).

---

## 1. Registro

| ID | Qué consultamos | Qué propuso la herramienta | Qué hicimos | Argumento propio y evidencia |
|----|-----------------|---------------------------|-------------|------------------------------|
| B-01 | Análisis completo del repositorio del Reto 1 y validación de las observaciones del profesor con evidencia | Síntesis AS-IS con ~90 citas `archivo:línea`: Simple Factories disfrazados, dominio anémico (setters públicos), RBAC muerto, validación triplicada, cero pruebas | **Aceptamos** | Las 5 críticas del profesor quedaron verificadas con medición, no opinión: p.ej. agregar subtipo = 9 clases/9 archivos en 4 capas (`FabricaRes.cs:17-23`, `GestorReses.cs:137-143`, `RepositorioPotreroSqlite.cs:150-157`) contra la promesa de "0 modificaciones" de `02-diseno/TOBE_Arquitectura_Completa.md:938`. Ver [[Reto2-Hacienda/Opcion1/01-AS-IS]] |
| B-02 | Plan de trabajo y estructura de entregables | Vault de 11 archivos Markdown mapeados actividad↔criterio de rúbrica, con gates de aprobación por actividad y reglas de evidencia | **Aceptamos** | El mapeo archivo↔actividad↔criterio garantiza que nada del enunciado quede sin artefacto y que cada afirmación cite `archivo:línea` (regla anti-deriva). Ver [[00-Plan]] |
| B-03 | Alcance del análisis (la IA incluía las vistas Razor como puntos de modificación) | — (no aplicó: corrección iniciada por el equipo) | **Fue idea nuestra** | Decisión del equipo: el frontend queda exactamente igual; el TO-BE debe ser compatible con lo que las vistas consumen (`Res`, `TipoRes`, `TempData`). Las vistas siguen **contando como costo** en las mediciones, pero no como objetivo. Ver [[00-Plan]] §2.2 y [[Reto2-Hacienda/Opcion1/02-PuntosDolor]] regla 3 |
| B-04 | Cómo encuadrar los hallazgos del análisis | La IA los presentó como "críticas verificadas" | **Corregimos** (encuadre) | El equipo decidió tratarlos como **deuda técnica heredada del Reto 1** a corregir en esta evolución cuando el remedio se justifique — no como acusación al trabajo propio. Ver [[Reto2-Hacienda/Opcion1/01-AS-IS]] §14 |
| B-05 | ¿Cuál solicitud del Anexo B conviene implementar: SC-1 o SC-3? ¿Cuál es más compleja? | Medición comparativa: SC-1 (derivados) = ~15–20 clases en ~15 archivos, transforma el agregado `Venta` (flujo congelado) y dispara P-01; SC-3 (historia clínica) = ~9–12 clases, aditiva. Recomendó SC-1 por ser la más exigente y maximizar el contraste AS-IS/TO-BE | **Aceptamos** (elegimos explícitamente la más difícil) | El equipo pidió "la más complicada": SC-1 ancla los dos dolores más caros (P-01, P-03) y su costo de 9-clases-por-subtipo es el argumento medible de la vista de negocio. Riesgo asumido y declarado: más carga de evidencia en la Act. 4. Ver [[Reto2-Hacienda/Opcion1/02-PuntosDolor]] §3 |
| B-06 | Detección de puntos de dolor | 12 puntos P-01…P-12 con costo contado y 3 marcados "no intervenir" (RBAC por congelamiento, código muerto por higiene, composición por riesgo) | **Aceptamos** | El "no intervenir" de P-08 es la decisión con mejor defensa técnica: activar permisos cambia comportamiento observable (−0.5/caso) y SC-1 es la única solicitud autorizada. Queda pendiente consolidar los ≥3 hallazgos propios sin IA (protocolo [[Reto2-Hacienda/Opcion1/02-PuntosDolor]] §0) |
| B-07 | Estrategia de evaluación de patrones | Evaluar los 22 del Anexo A (el enunciado exige mínimo 6) con ficha completa y alternativas reales incluyendo "no hacer nada" | **Aceptamos** | El enunciado valorará evaluar más; la ficha con "no hacer nada" y costo declarado responde la fórmula con que la Líder Técnica auditará. Ver [[Reto2-Hacienda/Opcion1/03-PatronesEvaluados]] |
| B-08 | Set de patrones a adoptar | 4 adoptados (Factory Method, Builder*, Template Method, Observer) + 18 descartes argumentados; descartes extendidos respondiendo las 3 advertencias del Anexo A; declaración explícita de que P-04/P-06 se curan con diseño y no con patrón | **Aceptamos** | El set cabe en el presupuesto 3–5 del enunciado; la honestidad de "no todo dolor se cura con patrón" blinda contra la penalización de sobre-ingeniería. *Builder condicionado a D-05. Ver [[Reto2-Hacienda/Opcion1/03-PatronesEvaluados]] §1 y §5 |
| B-09 | Diseño TO-BE (Actividad 3) | Dos diagramas marcados por patrón, tabla E-XX de 15 elementos, 4 fichas, efecto Anexo B medido (SC-1: ~6–8 clases nuevas y 0 ediciones de switches) | **Aceptamos** | El diseño reutiliza el idioma ya probado del Reto 1 (registro por inyección múltiple — `AutorizadorRbca.cs:13-16`) en vez de inventar maquinaria nueva. Ver [[Reto2-Hacienda/Opcion1/05-TOBE]] |
| B-10 | D-05: ¿SC-1 con producción propia (vaca lechera) o stock directo? | **Recomendación: Variante A (producción propia con `FabricaVacaLechera`)** — el enunciado dice "derivados **del ganado**" (la leche viene de vacas lecheras); sin producción propia el modelo queda incoherente con el dominio; además es la variante que mide la promesa OCP (9 puntos de modificación en 4 capas → 1 clase + 1 registro) | **Aceptamos por delegación explícita** ("hace lo que más recomiendes, recuerda el deber ser") | El equipo delegó la decisión con mandato explícito de seguir el deber ser; la justificación es de dominio (coherencia del modelo ganadero) y de rúbrica (demostración OCP medible). E-14 confirmado. Ver [[Reto2-Hacienda/Opcion1/05-TOBE]] §D-05 resuelta |
| B-11 | Diagramador por capas (el enunciado lo valora "muy bien") | Generación de `diagramas/Reto2_ASIS_TOBE_Capas.drawio`: una página, dos capas alternables — AS-IS en gris (rojo=sale, naranja=se transforma) y TO-BE en color por patrón | **Aceptamos** | Cumple la sugerencia literal del enunciado ("una capa AS IS y otra superpuesta, el TOBE") y sirve para el recorrido del video (min 6–11). El par Mermaid de [[Reto2-Hacienda/Opcion1/05-TOBE]] §3.1 queda como fuente legible en Obsidian y el .drawio como capa visual para el PDF/video |
| B-12 | Validación adversarial completa (post-delegación fallida) | La delegación a juez ciego y verificador de citas devolvió vacío; se realizó **auditoría manual completa** in-situ: re-verificación de 17 archivos fuente contra 38 citas críticas, grep de `VacunaVencidaEvent` (0 publicaciones) y `.Autorizar(` (0 llamadas), verificación de consistencia numérica (9 puntos/8 archivos), enlaces, mermaid, D-IDs. Correcciones aplicadas: 22 edits en 7 archivos (conteos P-01, D-IDs, Builder/Strategy, mermaid CORE2, guía SOLID, State family, bitácora B-10). | **Aceptamos** (auditoría propia) | Cumple la regla "fresh review" del protocolo: aunque la delegación falló, se realizó auditoría manual completa con evidencia antes de cerrar la Act. 4. Evidencia: 38/38 citas verificadas ✅; 8 inconsistencias corregidas; 0 hallazgos BLOQUEANTES. Ver commits de corrección y [[Reto2-Hacienda/Opcion1/06-VerificacionSOLID]] §Riesgos. |

| B-13 | Diagramas de evolución (indicación del profesor: "vayan poniendo varios, que se vea la evolución") | Se revisaron los `.dia` existentes (legado 143 objetos; unificado 293) y se construyó la **tercera generación** con la misma fidelidad de clases: `diagramas/Reto2_Evolucion_UML.drawio` (2 páginas: recorte AS-IS marcado con archivo:línea + TO-BE por patrón, mismo layout para VER la evolución al alternar pestañas) + linaje documentado en [[Reto2-Hacienda/Opcion1/01-AS-IS]] §0 | **Aceptamos** (sugerencia del profesor ejecutada por IA) | El recorte marcado responde literalmente al enunciado Act. 3 ("el recorte del diseño actual con los elementos que se retiran o cambian de responsabilidad, marcados"). Mismos nombres de clase que `UML_Hacienda_Unificado.dia` para continuidad visual. Validado: XML bien formado, 0 geometrías inválidas, 132 celdas. |

*(Registro continúa en las actividades 3–6: diseño TO-BE, verificación, riesgos y vistas. Cada decisión nueva se agrega aquí el día que ocurre.)*

---

## 2. Estado del requisito

| Requisito del enunciado | Estado |
|-------------------------|--------|
| Mínimo 10 registros | ✅ **13/10** — el registro sigue vivo para las Acts. 5–6 |
| Correcciones / rechazos / ideas propias presentes | ✅ B-03 (idea nuestra), B-04 (corrección de encuadre), B-10 (delegación explícita documentada con mandato) |
| Auditoría al azar defendible | ✅ cada fila cita evidencia verificable |

---

## 3. Navegación

- [[Reto2-Hacienda/Opcion1/03-PatronesEvaluados]] · [[Reto2-Hacienda/Opcion1/04-DecisionesArquitectonicas]] — las decisiones que esta bitácora registra.
- [[00-Plan]] §5 — reglas de evidencia que aplican a este registro.
