> [!warning] OBSOLETO (2026-09-02)
> Esta rama del análisis quedó desincronizada tras el saneo de la base del Reto 1.
> La rama canónica es [[Reto2-Hacienda/Opcion1/00-Plan|Opción 1]] — auditada y alineada al código real. No actualizar este directorio.

---
title: "Reto 2 — Plan de Trabajo"
tags:
  - arquitectura-software
  - reto2
  - patrones-diseno
  - hacienda
  - plan
curso: "Arquitectura de Software"
proyecto: "Hacienda (SolucionSOLID)"
fecha_entrega_oficial: 2026-09-06T23:59:59
peso_evaluacion: "20%"
estado: "en-curso"
---

# 00 — Plan de Trabajo · Reto 2: Patrones de Diseño Arquitectónico

> [!abstract] Propósito de este documento
> Documento rector del Reto 2. Consolida el encargo, las restricciones no negociables, la rúbrica con la que se califica, la estructura documental, la metodología de análisis, el cronograma y el registro de decisiones del equipo. Todo los demás documentos de este proyecto se derivan de aquí y enlazan de vuelta a este plan.
>
> **División de trabajo acordada**: la IA (actuando como arquitecto asistente) produce los documentos fuente 00–10; **el equipo** destila el PDF de sustentación (máx. 15 páginas), implementa el código refactorizado y toma todas las decisiones finales.

---

## 1. El encargo (lo que autorizó la Líder Técnica)

El sistema del Reto 1 logró separación de responsabilidades y dependencias invertidas, pero:

1. Cada tipo nuevo obliga a modificar el sitio donde se arman los objetos (la decisión de implementación quedó regada en condicionales).
2. Hay interfaces limpias pero ningún lugar donde leer cómo colaboran entre ellas.
3. El ensamblaje del sistema solo se entiende leyéndolo todo.

**Tres límites no negociables:**

| # | Límite | Consecuencia de violarlo |
|---|--------|--------------------------|
| L1 | El comportamiento observable sigue congelado: ni una salida, ni un cálculo, ni una regla | −0.5 sobre la nota final por caso no autorizado |
| L2 | No se cambia el estilo arquitectónico (nada de clean, hexagonal, servicios, frameworks, DI automática, ORM) | El criterio 3 no supera 2.5 |
| L3 | SOLID no se toca: si un patrón tensiona un principio, se declara y se compensa | Celdas "Roto" sin declarar hunden el criterio 4 |

### 1.1 Alcance autorizado

| ✅ Sí se interviene | ❌ No se interviene (descuenta) |
|--------------------|-------------------------------|
| Cómo se crean los objetos y dónde se decide qué implementación concreta se instancia | Migrar a arquitectura limpia, hexagonal, por capas o servicios separados |
| Cómo se componen y relacionan las estructuras existentes | Frameworks, contenedores de inyección automática, ORM o librerías que resuelvan el problema |
| Cómo se selecciona y coordina el comportamiento en tiempo de ejecución | Base de datos real, red, nube, concurrencia o interfaz gráfica |
| El punto donde se ensambla el sistema | Reescribir el sistema o cambiar de lenguaje |

> [!important] Regla de enfoque (observación del profesor al Reto 1)
> El trabajo está en **fortalecer principalmente el Core (`Hacienda.Domain`)**: demasiadas capas, responsabilidades fuera del dominio y fábricas mal implementadas fueron los señalamientos recibidos. `Hacienda.Application` solo se toca si un patrón necesita coordinación; `Hacienda.Web/Program.cs` solo para cablear; `Hacienda.Infrastructure` y los Controllers casi nunca deberían cambiar.
>
> **Encuadre acordado (D-05)**: esos señalamientos son la **deuda técnica heredada del Reto 1**; el TO-BE existe para pagarla. El análisis y el diseño se limitan a la colaboración de objetos del **backend**; el frontend (Views/Razor) queda fuera del alcance porque el equipo lo conserva igual en la entrega.

---

## 2. Entregables y fechas

| Entregable | Responsable | Detalle |
|------------|-------------|---------|
| Documento de sustentación (PDF ≤ 15 páginas, paginado, con índice) | **Equipo** (destila de estos documentos) | Contiene las 6 actividades en orden |
| Código refactorizado según el TO-BE | **Equipo** (guiado por [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/09-VistaTecnica]]) | Debe compilar y ejecutar; si no, criterio 4 = 0.0 |
| Video de 20 minutos (todos los integrantes con cámara) | **Equipo** | Lo que pase del minuto 20 no se evalúa |

**Fecha oficial**: domingo 6 de septiembre de 2026, 23:59:59. **Meta interna**: viernes 4 de septiembre (deja el fin de semana para video y correcciones).

### 2.1 Guion del video (bloques del enunciado)

| Minuto | Contenido | Frente |
|--------|-----------|--------|
| 0–3 | Puntos de dolor | Diseño |
| 3–6 | Vista de negocio + evidencia de comprensión de un no técnico | Comunicación |
| 6–11 | TO-BE: qué sale, qué entra, cómo se relaciona; recorrido por diagramas y la ficha del patrón más discutido | Diseño |
| 11–14 | SOLID sigue en pie: matriz comentada + ejecución en vivo de los 12 casos | Verificación |
| 14–17 | Riesgos principales con su señal de alerta | Riesgos y plan |
| 17–20 | Decisiones frente a la IA (propuestas aceptadas/corregidas/rechazadas) + SC implementada + deuda declarada | Comunicación |

> [!note] Frentes vs. integrantes
> El enunciado define 4 frentes (Arquitecto Líder, Verificación, Riesgos y despliegue, Comunicación gráfica). Si el equipo son 3 integrantes, uno asume dos frentes — decisión del equipo que debe quedar registrada en [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/10-BitacoraIA]].

---

## 3. Rúbrica y reglas duras

### 3.1 Criterios y pesos

| # | Criterio | Peso | Documento fuente |
|---|----------|------|------------------|
| 1 | Detección de puntos de dolor | 15 % | [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/02-PuntosDolor]] |
| 2 | Decisión de patrones + criterio frente a la IA | 20 % | [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/03-PatronesEvaluados]] · [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/04-DecisionesArquitectonicas]] · [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/10-BitacoraIA]] |
| 3 | Diseño TO-BE (qué sale, qué entra, relaciones, impacto) | 20 % | [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/05-TOBE]] |
| 4 | Garantía de SOLID y del comportamiento | 15 % | [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/06-VerificacionSOLID]] |
| 5 | Análisis de riesgos | 15 % | [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/07-Riesgos]] |
| 6 | Las dos vistas y la sustentación | 15 % | [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/08-VistaNegocio]] · [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/09-VistaTecnica]] |

### 3.2 Penalizaciones (checklist anti-descuento)

- [ ] Ningún patrón sin punto rígido que lo justifique (**−0.3 por patrón**)
- [ ] Ningún cambio no autorizado en comportamiento observable (**−0.5 por caso**)
- [ ] Bitácora de decisiones presente y defendible (**sin ella, criterio 2 = 0.0**)
- [ ] Sin cambio de estilo arquitectónico ni frameworks (**criterio 3 máx 2.5**)
- [ ] Diagramas correspondientes al código entregado (**criterios 3 y 4 máx 3.0**)
- [ ] Código compila y ejecuta (**criterio 4 = 0.0**)
- [ ] Vista de negocio sin nombres de patrones, clases, archivos, UML ni siglas (**criterio 6 máx 3.0**)
- [ ] Todos los integrantes en el video respondiendo su frente (**−1.5 individual**)
- [ ] Entrega a tiempo (**−0.5 por hora de retraso**)

### 3.3 Advertencias del Anexo A sobre los tres patrones que más se aplican mal

| Patrón | Condición impuesta por el enunciado |
|--------|-------------------------------------|
| Singleton | Explicar cómo se sustituye en una prueba y qué lo diferencia de una variable global |
| Facade | Declarar su límite (tiende a absorber lógica de negocio hasta romper SRP) |
| Abstract Factory | Si solo hay una familia de productos, la abstracción adicional probablemente no se justifica |

---

## 4. Estructura documental

| Archivo | Contenido | Actividad del enunciado | Estado |
|---------|-----------|------------------------|--------|
| [[00-Plan]] | Este documento: encargo, rúbrica, metodología, cronograma, decisiones | — | ✅ |
| [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/01-AS-IS]] | Arquitectura actual: capas, clases, dependencias, flujo de ensamblaje | Base de A1 | ✅ |
| [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/02-PuntosDolor]] | Inventario P-XX con evidencia, costo medido y decisión de intervención | A1 | ✅ (equipo aprobó 9 a intervenir) |
| [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/03-PatronesEvaluados]] | Ficha completa de los 22 patrones del Anexo A | A2 (tabla 2.1) | ✅ |
| [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/04-DecisionesArquitectonicas]] | Tabla de decisión: adoptados (3–5) y descartes argumentados | A2 | ✅ (pendiente aprobación del trío adoptado) |
| [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/05-TOBE]] | Diagramas por capas AS-IS/TO-BE, tabla de cambio E-XX, fichas de adoptados | A3 | ✅ |
| [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/06-VerificacionSOLID]] | Matriz patrón×principio con evidencia + 12 casos de comportamiento | A4 | ✅ |
| [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/07-Riesgos]] | Registro de riesgos (condición→consecuencia, P×I, señal observable) | A5 | ✅ |
| [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/08-VistaNegocio]] | Vista para la Líder Técnica (cero jerga técnica) | A6 | ✅ |
| [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/09-VistaTecnica]] | Vista para el dev nuevo + guía de dónde tocar (≥5 filas, cubre las 3 SC) | A6 | ✅ |
| [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/10-BitacoraIA]] | Registro de decisiones frente a la IA (mín. 10, reales) | Transversal | ✅ 17 registros |

---

## 5. Metodología de análisis

### Fase 1 — Leer antes de decidir ✅ (30 de agosto)

- [x] Enunciado, rúbrica y anexos leídos completos
- [x] Repositorio mapeado (108 archivos .cs en 4 proyectos)
- [x] Guías de estudio indexadas (SOLID completo, creacionales, estructurales, comportamiento: CoR y Command). Los 8 patrones de comportamiento restantes se evalúan con el catálogo GoF estándar — se declara en cada ficha.
- [x] Lectura profunda del código (exploración completa: 98 archivos fuente, auditoría de factorías, 18 candidatos a punto de dolor, conteos de impacto SC)

### Fase 2 — Puntos de dolor ([[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/02-PuntosDolor]])

- Se documentan **todos los candidatos encontrados** (no solo cinco); el equipo selecciona, prioriza y decide cuáles intervenir.
- Formato obligatorio: `ID · Archivo · Clase · Método · Responsabilidad · Síntoma · Evidencia · Costo real (clases y archivos contados) · Prioridad · ¿Intervenir?`
- Reglas: el síntoma se describe como dolor de cambio, **no como principio violado**; el costo se mide contando clases y archivos reales; al menos un punto queda marcado **"No intervenir"** con argumento de costo-beneficio.
- El requisito del enunciado de "al menos tres puntos encontrados sin IA" lo satisface el equipo con su propia lectura del código; estos documentos no lo simulan.

### Fase 3 — Evaluación de patrones ([[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/03-PatronesEvaluados]] · [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/04-DecisionesArquitectonicas]])

- Se evalúan los **22 patrones del Anexo A** con ficha completa: punto de dolor que resolvería (P-XX), evidencia, ≥3 alternativas (este patrón / otro patrón / no hacer nada), beneficios, costos, impacto y veredicto.
- Se adoptan **entre 3 y 5**. Todo adoptado queda anclado a un P-XX concreto. Todo descarte tiene justificación técnica real.
- Auditoría prioritaria de los 4 Factory existentes (`FabricaRes`, `FabricaVacuna`, `FabricaVenta`, `FabricaPotrero`): ¿Factory Method real o Simple Factory disfrazado? ¿Viola OCP/SRP? ¿Corregir, reemplazar o dejar?

### Fase 4 — Verificación SOLID ([[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/06-VerificacionSOLID]])

- Matriz patrón × {SRP, OCP, LSP, ISP, DIP} con valores: **Refuerza / Neutro / Tensionado pero compensado / Roto**. Toda celda ≠ Neutro lleva evidencia.
- 12 casos de comportamiento: los 8 del Reto 1 + 4 nuevos que recorran lo que los patrones tocan, con salidas lado a lado.

### Fase 5 — Riesgos ([[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/07-Riesgos]])

- Mínimo 3 riesgos en formato "si ocurre X, entonces Y", Prob×Imp (1–5 cada uno), acción preventiva y **señal observable** de materialización.

### Fase 6 — Vistas ([[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/08-VistaNegocio]] · [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/09-VistaTecnica]])

- Negocio: qué cambia, qué no, dónde va el tiempo/dinero hoy, qué gana, qué cuesta, qué riesgos, qué se necesita del negocio, qué pasa si no se hace. **Prohibido**: nombres de patrones, clases, archivos, UML, siglas (incluida SOLID), "refactorizar", "desacoplar", "inyección de dependencias".
- Técnica: dónde vive cada patrón, dónde se ensambla el sistema, reglas que no se rompen y por qué, deuda pendiente, y la **guía de dónde tocar** (≥5 filas cubriendo SC-1, SC-2 y SC-3).

### Bitácora ([[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/10-BitacoraIA]]) — transversal

- Se registra cada decisión real del trabajo: qué se consultó, qué propuso la IA, qué hizo el equipo (Aceptada / Corregida / Rechazada / Idea propia) y la evidencia. Mínimo 10 registros al cierre. No se inventan registros: los que aún no existen quedan como pendientes.

---

## 6. Registro de decisiones del equipo

| ID | Decisión | Estado | Nota |
|----|----------|--------|------|
| D-01 | Solicitud de cambio (SC) a implementar en el TO-BE | ✅ Decidida | **SC-1** (productos derivados) — decisión del equipo 30/08 following el análisis de impacto: 14 archivos vs 7-8 (SC-3), ataca el clúster creacional completo (P-01/02/05/06). La SC-2 (chips) fue la del Reto 1 (≈17-18 archivos). |
| D-02 | Diagramador para capas AS-IS/TO-BE | ⬜ Pendiente | En estos documentos se usa **Mermaid** (un diagrama AS-IS y el TO-BE espejo). Para el PDF/video el enunciado valora "un diagramador que permita diseños por capas": si el equipo prefiere capas superpuestas reales, draw.io con capas es la alternativa. |
| D-03 | Ubicación de los documentos | ✅ Decidida | Bóveda Obsidian: `ArquitecturaDeSoftware/Reto2-Hacienda/` |
| D-04 | División de trabajo | ✅ Decidida | IA produce documentos fuente; equipo decide, implementa y arma el PDF. |
| D-05 | Alcance del análisis y del TO-BE | ✅ Decidida | Encuadre: los señalamientos del profesor son **deuda técnica heredada** que el TO-BE paga. El frontend (Views/Razor) queda **fuera de alcance** — se conserva igual en la entrega. El trabajo es la colaboración de objetos del backend. |

---

## 7. Cronograma (7 días)

| Fecha | Trabajo | Artefacto |
|-------|---------|-----------|
| 30 ago | Plan + exploración profunda del código | [[00-Plan]] |
| 31 ago | AS-IS y puntos de dolor; el equipo selecciona y decide D-01 | [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/01-AS-IS]] · [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/02-PuntosDolor]] |
| 1 sept | Evaluación de los 22 patrones + tabla de decisión | [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/03-PatronesEvaluados]] · [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/04-DecisionesArquitectonicas]] |
| 2 sept | TO-BE: diagramas, tabla de cambio, fichas | [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/05-TOBE]] |
| 3 sept | Verificación SOLID + riesgos | [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/06-VerificacionSOLID]] · [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/07-Riesgos]] |
| 4 sept | Vistas + bitácora al día; **el equipo empieza implementación** | [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/08-VistaNegocio]] · [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/09-VistaTecnica]] · [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/10-BitacoraIA]] |
| 5 sept | Equipo: implementación, compilación, 12 casos, grabación | — |
| 6 sept | Buffer, armado del PDF, entrega antes de 23:59 | PDF + código + video |

> [!warning] Riesgo de calendario
> El cuello de botella no es el diseño: es que **el código refactorizado compile y preserve comportamiento** con solo 2–3 días de implementación. El TO-BE se mantiene deliberadamente conservador: pocos patrones (3–5), core primero, sin tocar Infrastructure salvo cableado.

---

## 8. Reglas de oro de este trabajo

1. **Ningún patrón sin ancla.** Todo adoptado responde a un P-XX con archivo y línea. "Buena práctica" no es evidencia.
2. **Toda decisión declara su costo.** Clases nuevas, indirección, dificultad de depuración. Una decisión sin costo declarado no es una decisión.
3. **Comportamiento congelado.** Las salidas del sistema antes y después son idénticas salvo la SC autorizada.
4. **Core primero.** Domain es el campo de trabajo; Application solo coordinación; Program.cs solo cableado; Infrastructure y controllers intocables salvo evidencia fuerte.
5. **Toda celda no-Neutro lleva evidencia.** En la matriz SOLID nada se afirma sin archivo y línea.
6. **La decisión final es del equipo.** Estos documentos recomiendan con evidencia; no decretan.

---

## 9. Riesgos preliminares del plan

| Riesgo | Señal temprana | Mitigación |
|--------|----------------|------------|
| El TO-BE no llega a código que compile antes del 6 sept | El 4 de septiembre sigue sin compilar | Diseño conservador; guía de dónde tocar detallada; SC acotada |
| Comportamiento observable cambia sin querer | Diferencias en las salidas de los 8 casos del Reto 1 | Casos lado a lado en [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/06-VerificacionSOLID]] antes de dar por cerrado el TO-BE |
| Patrón adoptado por gusto | No hay P-XX que lo ancle | Regla de oro 1; revisión cruzada equipo ↔ IA en la bitácora |
| La vista de negocio se llena de jerga | Aparece cualquier nombre de patrón/clase/sigla | Checklist de palabras prohibidas en [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/08-VistaNegocio]] |

> [!tip] Navegación
> Siguiente documento: [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/01-AS-IS]] · Inventario de dolor: [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/02-PuntosDolor]] · Patrones: [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/03-PatronesEvaluados]] · Decisiones: [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/04-DecisionesArquitectonicas]] · Diseño: [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/05-TOBE]] · Verificación: [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/06-VerificacionSOLID]] · Riesgos: [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/07-Riesgos]] · Vistas: [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/08-VistaNegocio]] / [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/09-VistaTecnica]] · Bitácora: [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/10-BitacoraIA]]
