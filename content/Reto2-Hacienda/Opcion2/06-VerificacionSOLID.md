---
title: "Reto 2 — Verificación SOLID y comportamiento"
tags:
  - arquitectura-software
  - reto2
  - hacienda
  - solid
  - verificacion
estado: "protocolo definido — evidencia lado a lado la ejecuta el equipo al implementar"
---

# 06 — Verificación SOLID y del comportamiento congelado

> [!abstract] Propósito
> Demostrar, no afirmar (Actividad 4): (4.1) matriz patrón × principio con evidencia por celda — valores permitidos **Refuerza / Neutro / Tensionado pero compensado / Roto**; (4.2) protocolo de los 12 casos de comportamiento (8 del Reto 1 + 4 nuevos que recorren lo que los patrones tocan) con salidas lado a lado.
>
> **Disciplina de honestidad**: las celdas verdes tienen evidencia del código; las 3 celdas tensionadas están declaradas con su compensación; no hay celdas "Roto". Los mensajes esperados se citan por su sitio de construcción en el AS-IS — el equipo captura la evidencia lado a lado al implementar.

---

## 1. Entregable 4.1 — Matriz de verificación

| Patrón adoptado | SRP | OCP | LSP | ISP | DIP |
|-----------------|-----|-----|-----|-----|-----|
| **Factory Method** | Refuerza ¹ | Refuerza ² | **Tensionado pero compensado** ³ | Refuerza ⁴ | Refuerza ⁵ |
| **Facade** | **Tensionado pero compensado** ⁶ | Refuerza ⁷ | Neutro ⁸ | Refuerza ⁹ | Refuerza ¹⁰ |
| **Strategy** | Refuerza ¹¹ | Refuerza ¹² | **Tensionado pero compensado** ¹³ | Refuerza ¹⁴ | Refuerza ¹¹ |

### Evidencia por celda (toda celda ≠ Neutro)

**1 · FM×SRP — Refuerza.** Hoy `FabricaRes` acumula tres motivos de cambio: crear (diccionario), validar edad y fabricar mensajes (`FabricaRes.cs:17-48`); `ServicioVacunacion` acumula creación unitaria+lote por subtipo (185 LOC). TO-BE: cada creador tiene una sola razón de cambio — su subtipo (`CreadorTernero` crea, rehidrata, describe y valida Ternero y nada más).

**2 · FM×OCP — Refuerza.** La extensión hoy cuesta 10 archivos / 7 clases con 7 puntos de decisión (P-02); TO-BE: 1 subclase + 1 creador + **1 línea en el registro del punto de ensamblaje**. Esa línea es el único punto de crecimiento y es el que el enunciado autoriza expresamente ("el punto donde se ensambla el sistema") — a diferencia del error tipificado ("fábrica que crece con un condicional por tipo": ahí el condicional se movía; aquí desaparece: el registro no decide, lista). Regla de protección: prohibido reintroducir `switch`/`if` sobre el tipo — [[Reto2-Hacienda/Opcion2/09-VistaTecnica]].

**3 · FM×LSP — Tensionado pero compensado.** *Tensión*: `Rehidratar` reconstruye desde estado persistido; si re-validara las reglas de alta (p. ej. `EsEdadValida`), una fila legada con edad fuera de rango — que hoy carga sin protestar vía switch (`RepositorioVentaSqlite.cs:39-45`) — empezaría a **lanzar en lectura**: cambio de comportamiento observable. *Compensación*: `Rehidratar` restaura el estado persistido tal cual (el invariante fue validado al crear el objeto; los datos legados se aceptan como estaban), la ruta de validación vive solo en `Crear`. Verificación: caso 12 (datos legados). Esta distinción crear/rehidratar queda documentada en el contrato del creador.

**4 · FM×ISP — Refuerza.** Registros segregados por familia (`IRegistroReses`, `IRegistroVacunas`, `IRegistroProductos`): ningún consumidor ve familias que no usa. Hoy `IVacunaFactory` obliga a conocer ambos subtipos y `Viva.GradoAtenuacion` filtra a Application/Web (`Viva.cs:7-12`); TO-BE: los datos propios del subtipo quedan dentro del subtipo.

**5 · FM×DIP — Refuerza.** Los repositorios dejan de hacer `new Ternero(...)` (concreto, `RepositorioPotreroSqlite.cs:152-154`) y pasan a depender de la abstracción del registro; `GestorReses` ya inyectaba `IResFactory` y mantiene la dirección de la dependencia.

**6 · Facade×SRP — Tensionado pero compensado.** *Tensión*: es el riesgo que el Anexo A tipifica ("una fachada que va absorbiendo lógica de negocio hasta ser un objeto que lo hace todo") — y el servicio actual YA contiene secuenciación de reglas (`ServicioVentas.cs:40-51`: buscar, fabricar, validar, remover). *Compensación*: (a) las reglas bajan al dominio (`Res.AlConfirmarVenta`, `Res.EvaluarPeso`, `IVendible.ValidarParaVenta`); (b) **métrica de control verificable**: ningún método de `ServicioVentas` contiene una condición de negocio — si aparece, la regla baja al dominio; (c) revisión en el checklist de merge del equipo.

**7 · Facade×OCP — Refuerza.** SC-1 entra **detrás** de la fachada sin editarla: el par (proveedor, estrategia) se registra en `Program.cs`; la fachada itera su secuencia invariante. Un segundo tipo de producto vendible (o una tercera política de precio) no la toca.

**8 · Facade×LSP — Neutro.** No introduce jerarquía de sustitución: `IServicioVentas` conserva una única implementación (igual que hoy).

**9 · Facade×ISP — Refuerza.** El contrato se mantiene angosto (3 operaciones: vender, listar, estadísticas) **aunque la funcionalidad crece** con SC-1: el nuevo tipo de producto no agregó operaciones al contrato — entró por el registro. Evidencia negativa verificable en `IServicioVentas` TO-BE vs AS-IS.

**10 · Facade×DIP — Refuerza.** La fachada depende de `IRegistroProductos`, `IRepositorioVenta`, `IRepositorioPotrero`, `TimeProvider` — ninguna implementación concreta; los controladores siguen dependiendo solo de `IServicioVentas`.

**11 · Strategy×SRP — Refuerza (y DIP ídem).** La regla de precio vive en una clase por política; hoy la regla por categoría vive duplicada en un if del servicio (`ServicioVacunacion.cs:137-143` reimplementando `Res.cs:35-40`) — dos motivos de cambio para una sola regla. `Venta` delega el cálculo: no conoce implementaciones (DIP).

**12 · Strategy×OCP — Refuerza.** Política de precio nueva (2×1, precio por bulto) = clase nueva + registro; consumidores (`Venta`, fachada) intactos. El condicional por tipo que SC-1 induciría (`if (producto es res) …`) no existe en ningún nivel.

**13 · Strategy×LSP — Tensionado pero compensado.** *Tensión*: `PrecioUnitario` necesita datos que `MontoManual` ignora (cantidad, precio unitario) — si cada estrategia "bajara el tipo" de su entrada para conseguir sus datos, el contrato se rompería por surprise. *Compensación*: **emparejamiento en el registro**: cada estrategia se registra junto al proveedor de SU producto (par inmutable); ninguna estrategia recibe jamás un producto/especificación que no sea la suya — la sustituibilidad se garantiza por el par, no por castes defensivos. Riesgo residual declarado: registrar un par cruzado — mitigado en R-05 [[Reto2-Hacienda/Opcion2/07-Riesgos]].

**14 · Strategy×ISP — Refuerza.** Contrato de una sola operación (`Calcular`); el riesgo tipificado ("interfaz que crece con métodos no aplicables a todas las variantes") queda como regla de control: si `IEstrategiaPrecio` gana un segundo método, se segrega.

> [!success] Lectura de la matriz
> 11 Refuerza · 1 Neutro · 3 Tensionados pero compensados · 0 Rotos. Las tres tensiones son los tres puntos que el equipo debe vigilar en implementación y sustentación — y cada una tiene su caso de verificación (3→caso 12; 6→casos 5 y 10; 13→casos 9 y 11).

---

## 2. Entregable 4.2 — Los 12 casos de comportamiento

**Protocolo**: ejecutar cada caso en el AS-IS (rama base) y en el TO-BE (rama refactorizada) con los mismos datos de entrada; capturar la salida visible (TempData/render) lado a lado; **deben coincidir carácter a carácter** salvo los casos 9-10, que son comportamiento nuevo autorizado por SC-1 (se verifica contra su especificación). Semilla de datos idéntica (`DataLoader`/`seed_data.sql` sin cambios).

### Bloque A — Los 8 casos del Reto 1 (comportamiento congelado)

| # | Caso | Pasos | Salida esperada (sitio de construcción en AS-IS) | Qué recorre del TO-BE |
|---|------|-------|--------------------------------------------------|------------------------|
| 1 | Alta de res | Crear Ternero "Luna" peso 180 edad 8 en potrero de terneros | mensaje de éxito + res visible en listado + badge de tipo (`GestorReses.cs:79`; badge: switch de `Views/Res/Index.cshtml:71-77` → TO-BE: `DescripcionTipo()` del creador) | Registro + `CreadorTernero` |
| 2 | Alimentar res bajo umbral | Alimentar res con peso < mínimo | eventos de peso mínimo/mensaje (`GestorReses.cs:56-65`) — TO-BE: `Res.EvaluarPeso()` dentro de la entidad, mismos textos | Refactor P-08 (sin patrón) |
| 3 | Crear vacuna y lote | Crear Bacteriana y lote de 10 "Lote-A" | numeración `{base}-{i:D3}` (`ServicioVacunacion.cs:64-65`) y mensaje (`:166`) — TO-BE: `CrearLote` parametrizado, misma numeración | Colapso de 4 métodos → 2 |
| 4 | Aplicar vacuna hasta el límite | Aplicar a una res más vacunas bacterianas que su máximo | error de límite (`ServicioVacunacion.cs:137-143`) — TO-BE: `Res.AplicarVacuna` lanza el mismo mensaje (la regla se muda, no cambia) | Refactor P-04 |
| 5 | Vender res | Vender res válida con monto 1.500.000 | mensaje de venta + res fuera del potrero + fila en ventas (`ServicioVentas.cs:40-51` + mensajes actuales) | **Fachada + MontoManual + `AlConfirmarVenta`** — el caso crítico de preservación |
| 6 | Instalar chip y ubicación | Instalar chip activo a una res; registrar ubicación | mensajes de chip/ubicación (`ServicioChip.cs:43-56`; éxito detectado por `Contains("correctamente")` — **intacto, P-03 congelado**) | Sin cambios (control de no-regresión) |
| 7 | Autenticación y creación de usuario | Login admin; crear usuario nuevo | resultados de login y usuario creado (`ServicioAutenticacion.cs`; rol Visitante — P-18 congelado) | Sin cambios |
| 8 | Geolocalización | Ver historial de ubicaciones del chip | listado y distancias (`ServicioGeolocalizacion.cs:88-99` — Haversine intacto) | Sin cambios |

### Bloque B — Los 4 casos nuevos (recorren lo que los patrones tocan)

| # | Caso | Pasos | Verificación | Qué patrón recorre |
|---|------|-------|--------------|--------------------|
| 9 | **Venta de derivado (SC-1)** | Vender 20 unidades de "Queso Campesino" (lácteo) a $18.000 | comportamiento **nuevo autorizado**: monto = 20 × 18.000 = $360.000; fila en ventas con descripción y tipo de producto; **sin** tocar potrero | Fachada + registro (par ProveedorDerivado + PrecioUnitario) |
| 10 | **Venta de res por el contrato nuevo** | Vender la misma res del caso 5 por la especificación unificada | salida **idéntica al caso 5 carácter a carácter** (mismo mensaje, misma remoción del potrero, misma fila de venta) — prueba de que la fachada no cambió el flujo visible | Fachada + ProveedorResDePotrero + MontoManual (compensación 6) |
| 11 | **Identidad estable en lectura** | Listar ventas dos veces consecutivas; comparar identificadores de la res vendida | TO-BE: el `Id` de la res en ambas lecturas es **el mismo** (AS-IS: cambia en cada lectura — bug P-10); badges de tipo consistentes | `Rehidratar` con GUID persistido + `DescripcionTipo()` |
| 12 | **Datos legados** | Con la BD pre-TO-BE (ventas históricas de reses), listar ventas | TO-BE: reconstrucción vía `Rehidratar` produce las mismas filas que AS-IS — sin excepciones por re-validación (compensación 3: crear≠rehidratar) | FM×LSP compensado |

> [!warning] Regla de captura de evidencia
> Casos 1-8 y 10-12: cualquier diferencia de un carácter es un incumplimiento de L1 (−0.5 por caso) → corregir antes de seguir. Casos 9: verificar contra la especificación SC-1. La captura lado a lado (AS-IS izquierda, TO-BE derecha) es la evidencia del video, minuto 11-14.

---

## 3. Comprobación de los errores tipificados del enunciado

| Situación tipificada (Actividad 4) | ¿Ocurre en el TO-BE? | Evidencia |
|------------------------------------|----------------------|-----------|
| Fábrica que crece con un condicional por tipo nuevo | **No** | El registro no decide: lista; los creadores son por subtipo (§1 celda 2) |
| Fachada que absorbe lógica de negocio | **Controlado** | Métrica "cero `if` de negocio en ServicioVentas" + reglas en el dominio (celda 6) |
| Acceso global de instancia única | **No** | Sin Singleton: instancias únicas por composition root, sustituibles en pruebas (ficha 5 de [[Reto2-Hacienda/Opcion2/03-PatronesEvaluados]]) |
| Método plantilla con pasos que una subclase no puede cumplir | **No aplica** | Template Method descartado (ficha 21); sin pasos vacíos |
| Envoltura que cambia el contrato de lo que envuelve | **No** | `MontoManual` devuelve exactamente el monto provisto (caso 10); la fachada conserva los textos (caso 5) |

> [!tip] Navegación
> Diseño verificado: [[Reto2-Hacienda/Opcion2/05-TOBE]] · Riesgos de esta implementación: [[Reto2-Hacienda/Opcion2/07-Riesgos]] · Matriz en sustentación: [[Reto2-Hacienda/Opcion2/08-VistaNegocio]] no la menciona (audiencia no técnica); [[Reto2-Hacienda/Opcion2/09-VistaTecnica]] la explica por patrón.
