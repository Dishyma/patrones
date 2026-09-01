---
title: "Reto 2 — Decisiones arquitectónicas"
tags:
  - arquitectura-software
  - reto2
  - hacienda
  - decisiones
  - adr
estado: "completo — pendiente de aprobación del equipo"
---

# 04 — Decisiones arquitectónicas · Actividad 2

> [!abstract] Propósito
> Formaliza la decisión sobre patrones: tabla de decisión completa (formato del Entregable 2.1), registros de decisión (ADR) para los adoptados, el destino de las 4 factorías existentes, los refactors del Core que **no** llevan patrón, y la deuda que se declara. La adopción definitiva requiere aprobación del equipo — este documento recomienda con evidencia.

---

## 1. Tabla de decisión de patrones (Entregable 2.1)

| Patrón evaluado | Familia | Punto de dolor que atacaría | Qué gana y qué cuesta | Decisión | Por qué |
|-----------------|---------|------------------------------|------------------------|----------|---------|
| Factory Method | Creacional | P-02, P-05, P-06, P-09, P-10 | Gana: un solo punto de creación para alta y rehidratación; tipo nuevo = 1 subclase + 1 creador + 1 línea de registro. Cuesta: ≈8-10 clases creadoras, +1 indirección | **Adoptado** | Corrige el hallazgo central del profesor (0/4 factorías reales); prerrequisito de P-05/P-10 sin tocar esquema |
| Abstract Factory | Creacional | — | Gana: coherencia por familia. Cuesta: jerarquía extra que mueve el punto de modificación | Descartado | El eje de variación es el tipo, no la familia — advertencia literal del Anexo A |
| Builder | Creacional | — | Gana: construcción gradual. Cuesta: 2-4 clases | Descartado | No hay constructor telescópico (aridad 2-4); la variación es de tipo, no de configuración |
| Prototype | Creacional | P-05 | Gana: rehidratación uniforme. Cuesta: exponer llenado → rompe P-08 | Descartado | El creador-con-estado-persistido logra lo mismo reforzando encapsulación |
| Singleton | Creacional | — | Gana: acceso global coherente. Cuesta: DIP roto, insustituible en pruebas | Descartado | El composition root ya da instancias únicas sustituibles — Singleton sería una variable global vestida |
| Adapter | Estructural | P-05 | Gana: conversión estandarizada. Cuesta: capa paralela a la factoría | Descartado | El dolor es quién decide el subtipo, no cómo se convierte la fila |
| Bridge | Estructural | — | Gana: variar persistencia. Cuesta: 2 jerarquías | Descartado | El segundo eje (persistencia) está congelado por el encargo |
| Composite | Estructural | — | Gana: parte-todo uniforme. Cuesta: jerarquía de nodos | Descartado | No hay estructura arbórea en alcance (SC-3 sería el escenario futuro) |
| Decorator | Estructural | P-03 | Gana: tipificar resultados sin tocar servicios. Cuesta: 1 envoltorio por servicio | Descartado | Mueve la heurística `Contains` sin eliminarla; P-03 es deuda congelada (L1) |
| **Facade** | Estructural | P-01 + "no se puede leer cómo colaboran" | Gana: un punto legible del subsistema de venta; controladores aislados. Cuesta: riesgo tipificado de absorber reglas → **límite declarado** | **Adoptado** | Responde el alcance "cómo se componen"; el límite del Anexo A se declara con métrica de control |
| Flyweight | Estructural | — | — | Descartado | Sin masas de objetos compartidos: dolor inexistente (regla de anclaje) |
| Proxy | Estructural | — | Gana: carga diferida. Cuesta: estado + riesgo de orden de accesos | Descartado | Optimización de un recurso (BD) fuera de alcance |
| Chain of Responsibility | Comportamiento | P-11 | Gana: reglas enchufables. Cuesta: 6-10 clases de regla | Descartado | La validación necesita colectar todos los errores; CoR corta en el primer handler — semántica incompatible |
| Command | Comportamiento | — | Gana: deshacer/encolar/auditar. Cuesta: 1 clase por operación | Descartado | Sin cola, undo ni auditoría en alcance |
| Iterator | Comportamiento | P-08 | Gana: recorrido encapsulado. Cuesta: clases de iterador | Descartado | P-08 se corrige con `IReadOnlyList` + métodos de dominio; no hay traversals especiales |
| Mediator | Comportamiento | P-01/P-13 | Gana: coordinación central. Cuesta: matriz de referencias cruzadas | Descartado | La colaboración necesaria es por caso de uso (fachada), no entre colegas paritarios |
| Memento | Comportamiento | — | Gana: restaurar estados. Cuesta: historial por entidad | Descartado | Sin undo/rollback en alcance (regla de anclaje) |
| Observer | Comportamiento | P-12 | Gana: reacción desacoplada a eventos. Cuesta: rediseñar el ensamblado de mensajes visibles | **Descartado con deuda** | La reacción observable está congelada (L1, −0.5/caso); primero patrón a incorporar si se autoriza |
| State | Comportamiento | evaluado fuera de inventario (`Chip`) | Gana: transiciones polimórficas. Cuesta: 4+ clases de estado en la entidad mejor encapsulada | Descartado | Sin P-XX aprobado: añadir un estado hoy cuesta 1 archivo — remedio > problema (−0.3) |
| **Strategy** | Comportamiento | P-01, P-04 | Gana: comportamiento de precio seleccionado en runtime por producto; añadir política = nueva estrategia + registro. Cuesta: +1 interfaz, +2-3 implementaciones, indirección | **Adoptado** | SC-1 introduce variación genuina de comportamiento; `MontoManual` preserva el comportamiento congelado de reses |
| Template Method | Comportamiento | P-06 | Gana: esqueleto de lote compartido. Cuesta: jerarquía para variar un paso | Descartado | La variación es un dato (qué creador), no pasos: gana el método parametrizado + ficha 1 |
| Visitor | Comportamiento | — | Gana: operaciones sin tocar subtipos. Cuesta: interfaz visitante + `Aceptar` en todos | Descartado | Dirección de cambio invertida: aquí crecen los tipos (SC-1) y las operaciones son estables |

**Cobertura del enunciado**: 22 evaluados (mín. 6 ✓) · ≥2 por familia (5-5-10 ✓) · 3 adoptados (rango 3-5 ✓) · descartes argumentados ≥2 (19, de los cuales Abstract Factory, Prototype, CoR, Observer, State, Template Method y Visitor llevan análisis de trade-off explícito ✓).

---

## 2. ADR-01 · Adoptar Factory Method (real) como mecanismo único de creación

- **Contexto**: la decisión de "qué subtipo instanciar" está regada en 7+ sitios (ficha 1 de [[Reto2-Hacienda/Opcion2/03-PatronesEvaluados]]): diccionario y switch espejo en `FabricaRes`, firmas bifurcadas en `IVacunaFactory`→servicio→controlador, switches propios en 3 repositorios. Costo medido: 10 archivos por tipo nuevo de Res, 12 por tipo nuevo de Vacuna; además los dos caminos de creación discrepan (P-10: GUID nuevo en lectura).
- **Decisión**: cada subtipo aporta su propio **creador** (`ICreadorRes`, `ICreadorVacuna`, …) que encapsula construcción + descripción + rehidratación (acepta el estado persistido, GUID incluido). Un **registro único** por familia, ensamblado en `Program.cs` (el punto de ensamblaje que el encargo autoriza), resuelve clave→creador. Los repositorios dejan de decidir subtipos: **delegan** la rehidratación al registro.
- **Alternativas**: Abstract Factory (mueve el punto de modificación), Prototype (rompe la encapsulación que P-08 exige), no hacer nada (10-12 archivos por tipo; discrepancia de identidad persiste).
- **Consecuencias**: (+) tipo nuevo = 1 subclase + 1 creador + 1 línea de registro; `MapearTipoRes`, `DescribirRango`, switches de repos y 4 métodos paralelos de vacunas desaparecen; identidad preservada por construcción. (−) ≈8-10 clases más, un nivel más de indirección, el registro hay que leerlo en `Program.cs` para depurar. **Regla de protección OCP**: prohibido volver a escribir `switch`/`if` sobre el tipo para crear — queda en [[Reto2-Hacienda/Opcion2/09-VistaTecnica]].
- **SOLID**: SRP ✓ (crear y describir vuelven al subtipo) · OCP ✓ (extensión por adición) · LSP ✓ (creadores sustituibles por contrato) · ISP ✓ (una operación por contrato de creador) · DIP ✓ (consumidores dependen de la abstracción del registro). Evidencia por celda: [[Reto2-Hacienda/Opcion2/06-VerificacionSOLID]].

## 3. ADR-02 · Adoptar Facade (rol declarado) sobre el subsistema de venta

- **Contexto**: el pipeline de venta atraviesa 9 piezas y con SC-1 ganaría 5+ colaboradores (creadores de productos, estrategias de precio, reglas por producto); la Líder Técnica denunció exactamente esto: "hay que leerlo todo".
- **Decisión**: `ServicioVentas` se reestructura como **fachada del subsistema de venta**: un contrato unificado de entrada (`Vender(especificación de venta)`) que coordina producto → validación → precio → persistencia. **No es una clase nueva encima del servicio** — es el rol del servicio existente con dependencias declaradas y el contrato unificado.
- **Límite declarado** (Anexo A): la fachada **orquesta, no legisla**. Secuencia permitida: obtener producto, validar (regla del dominio), calcular (estrategia), persistir. **Métrica de control**: si un método de la fachada contiene una condición de negocio (`if (peso < mínimo) …`), se pasó del límite y la regla baja al dominio.
- **Alternativas**: Mediator (matriz de referencias cruzadas), exponer los colaboradores al controlador (el status quo que duele).
- **Consecuencias**: (+) un lugar legible para responder "¿cómo se vende?"; SC-1 se implementa detrás de la fachada sin que controladores conozcan creadores ni estrategias. (−) una indirección más UI→dominio; el riesgo tipificado (absorber lógica) queda vigente y se controla con la métrica anterior y con la revisión de [[Reto2-Hacienda/Opcion2/06-VerificacionSOLID]].
- **SOLID**: SRP ✓ (orquestación como única responsabilidad) · OCP ✓ (nuevos productos entran sin editar la fachada) · LSP ✓ · ISP ✓ (contrato angosto: 3 operaciones) · DIP ✓ (depende de abstracciones). El detalle por celda con evidencia, en [[Reto2-Hacienda/Opcion2/06-VerificacionSOLID]].

## 4. ADR-03 · Adoptar Strategy para el comportamiento de precio por producto

- **Contexto**: SC-1 introduce variación de comportamiento genuina: una res se vende con monto provisto por el usuario (comportamiento actual, congelado); un derivado se vende a precio unitario × cantidad (comportamiento nuevo autorizado). Sin patrón, la bifurcación `if (es res) … else …` aparecería en fachada, validador y vistas — el mismo defecto del AS-IS (P-06) reproducido en la feature nueva.
- **Decisión**: `IEstrategiaPrecio` con dos implementaciones: **`MontoManual`** (reses: pasa el monto tal como hoy — preserva salidas exactas) y **`PrecioUnitario`** (derivados: precio × cantidad). La estrategia se asigna al crear el producto (el creador la conoce) y la fachada la consume vía `Venta`. 
- **Alternativas**: Template Method (varía el comportamiento completo, no un paso), condicional por tipo (el anti-patrón actual), no hacer nada (SC-1 imposible sin bifurcar).
- **Consecuencias**: (+) nueva política de precio = nueva estrategia + registro, sin editar consumidores; el comportamiento congelado queda representado por una estrategia explícita y verificable en los 12 casos. (−) +1 interfaz +2-3 clases; depurar un precio exige identificar la estrategia participante.
- **SOLID**: SRP ✓ · OCP ✓ · LSP ✓ (estrategias intercambiables ante `Venta`) · ISP ✓ (una sola operación `Calcular`) · DIP ✓ (`Venta` no conoce implementaciones). Evidencia: [[Reto2-Hacienda/Opcion2/06-VerificacionSOLID]].

---

## 5. Destino de las 4 factorías existentes

| Factoría | Diagnóstico ([[Reto2-Hacienda/Opcion2/02-PuntosDolor]] §3) | Destino en el TO-BE |
|----------|--------------------------------------|---------------------|
| `FabricaRes` + `IResFactory` | Simple Factory (diccionario + switch espejo) | **Se transforma**: los 3 subtipos ganan sus creadores; el registro reemplaza el diccionario; `DescribirRango` baja al subtipo; los switches de `GestorReses` y de los 2 repos caen |
| `FabricaVacuna` + `IVacunaFactory` | Simple Factory con el switch en la firma (métodos por subtipo) | **Se transforma**: creadores por subtipo + registro; `IServicioVacunacion` colapsa 4 métodos → 2 (`CrearVacuna`, `CrearLote` parametrizado); los if/else de `VacunaController` desaparecen del contrato |
| `FabricaVenta` + `IVentaFactory` | Wrapper de constructor (no factoriza nada) | **Se reemplaza**: con `IVendible` + estrategia de precio, la venta se construye en la fachada con el producto ya creado; la "factoría" se degrada a construcción directa en `Venta` |
| `FabricaPotrero` + `IPotreroFactory` | Wrapper redundante (valida lo que el VO garantiza) | **Se elimina**: `Identificacion` (VO) ya exige no-vacío; crear potreros es una construcción simple sin familia polimórfica; eliminarla también corrige el ruido de "factoría de nombre" |

---

## 6. Refactors del Core SIN patrón (criterio, no catálogo)

> [!important] No todo dolor se paga con un patrón
> Tres puntos aprobados se corrigen con refactor de dominio puro. Declararlo explícitamente **es** criterio arquitectónico: forzar un patrón aquí activaría la penalización de sobre-ingeniería.

| Punto | Corrección en el TO-BE | Por qué sin patrón |
|-------|------------------------|--------------------|
| P-04 | `Res.AplicarVacuna(vacuna)` encapsula la regla (máximos por categoría, duplicadas, vencimiento) y la colección pasa a privada; `ServicioVacunacion` deja de reimplementar `Res.EsquemaVacunacionCompleto` | La regla ya es polimórfica en la entidad — solo estaba siendo duplicada fuera; no hay selección de algoritmo que variar |
| P-08 | `Res.Alimentar(cantidad)` y `Res.EvaluarPeso()` devuelven el estado de peso; setters públicos de `Peso/Edad/Chip` se eliminan; los umbrales bajan del servicio a la entidad (el estándar ya existe: `Potrero`, `Chip`) | Es encapsulación, no selección de comportamiento; Iterator/State/Visitor no aportan nada aquí |
| P-11 | Los 4 `Validador*` triviales se degradan: la validación real queda en VOs + creadores + `IVendible.ValidarParaVenta`; un solo mecanismo de error | CoR evaluado y descartado (semántica de acumulación vs corte — ficha 13); la composición correcta es polimórfica |

**Con esto, P-09 cae al fusionarse los enums** (una sola fuente de verdad del tipo en el subtipo) y **P-10 se corrige por construcción** (el creador de rehidratación recibe el GUID persistido).

---

## 7. La SC-1 en el TO-BE (panorama; el diseño completo en [[Reto2-Hacienda/Opcion2/05-TOBE]])

**Qué entra** (solo diseño): `IVendible` (contrato de producto: descripción, serialización, validación para venta) implementado por la jerarquía `Res` existente y por `ProductoDerivado` (lácteos/carne/piel como configuración de datos del derivado, no como subtipos — la variación es de datos, no de comportamiento); creadores de derivados registrados junto a los de reses; `IEstrategiaPrecio` (MontoManual/PrecioUnitario); persistencia de derivados en **tabla nueva** (`productos`), sin tocar la tabla `ventas` de las reses más allá de referenciar el producto vendido.

**Qué NO cambia**: el flujo de venta de reses produce las mismas salidas (estrategia `MontoManual`); vistas y controladores existentes de reses/potreros/vacunas se conservan; el equipo agrega las vistas nuevas de derivados que la SC-1 autoriza.

**El argumento con números**:

| Escenario de cambio | AS-IS (contado) | TO-BE (diseño) |
|----------------------|-----------------|----------------|
| Vender un tipo nuevo de producto | **14 archivos / ~14 clases** | 1 clase de derivado (o 0 si es configuración) + 1 creador + 1 línea de registro + vistas nuevas que la SC autoriza |
| Añadir un tipo nuevo de res | **10 archivos / 7 clases** | 1 subclase + 1 creador + 1 línea de registro |
| Añadir un tipo nuevo de vacuna | **12 archivos / ~10 clases** | 1 subclase + 1 creador + 1 línea de registro |
| Rehidratar entidades | 3 switches paralelos que discrepan (GUID nuevo) | mismo mecanismo de creación, identidad persistida |

## 8. Deuda técnica declarada (los 9 "no intervenir")

| Deuda | Señal de alerta | Condición de activación |
|-------|-----------------|-------------------------|
| P-03 contrato string de servicios | Un cambio de mensaje altera la clasificación success/danger de la UI | SC que autorice tocar mensajes |
| P-07 autorización muerta + P-16 permisos por `Contains` | Cualquier SC de permisos/roles | Activar `AutorizadorRbca` exige orden del negocio |
| P-12 Observer a medio instalar | Un consumidor real de eventos (auditoría, SC-3) | Observer es el primer patrón a incorporar |
| P-13 tres agregados sin transacción | Fallo intermedio en `InstalarChip` | Corrección puntual en la SC si el equipo lo aprueba |
| P-14 seed fuera del dominio | Migración de datos de prueba | Solo si el seed se vuelve carga de mantenimiento |
| P-15 DTOs muertos + vistas tipadas a entidades | Cambio de entidad que rompe vistas | Fuera de alcance por D-05 |
| P-17 save-all + esquema a mano | Concurrencia o volumen real | "BD real" excluida por el encargo |
| P-18 rol hardcodeado | SC de gestión de roles | Fuera del alcance actual |

> [!tip] Navegación
> Evaluación completa: [[Reto2-Hacienda/Opcion2/03-PatronesEvaluados]] · Diseño TO-BE con diagramas y tabla E-XX: [[Reto2-Hacienda/Opcion2/05-TOBE]] · Matriz SOLID de esta adopción: [[Reto2-Hacienda/Opcion2/06-VerificacionSOLID]] · Bitácora: [[Reto2-Hacienda/Opcion2/10-BitacoraIA]]
