---
title: "Reto 2 — Evaluación de patrones (Anexo A completo)"
tags:
  - arquitectura-software
  - reto2
  - hacienda
  - patrones-diseno
estado: "completo — adopción pendiente de aprobación del equipo"
---

# 03 — Patrones evaluados · Los 22 del Anexo A, ficha por ficha

> [!abstract] Propósito
> Evaluación completa de los 22 patrones del Anexo A contra la evidencia del AS-IS. **Ningún patrón se adopta por buena práctica**: cada ficha parte de un punto de dolor aprobado ([[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/02-PuntosDolor]]), evalúa mínimo tres alternativas (este patrón / otro patrón / no hacer nada), declara costos e impacto, y termina en veredicto argumentado.
>
> **Regla de anclaje** (enunciado, punto 2): un patrón sin punto rígido que lo justifique es sobre-ingeniería y penaliza (−0.3). Los patrones sin punto de dolor aprobado se **descartan por anclaje**, no por desconocimiento.

## 0. Matriz de veredictos

| # | Patrón | Familia | Ancla (P-XX) | Veredicto |
|---|--------|---------|--------------|-----------|
| 1 | Factory Method | Creacional | P-02, P-05, P-06, P-09, P-10 | ✅ **Adoptado** |
| 2 | Abstract Factory | Creacional | — (analizado sobre P-01/P-02) | ❌ Descartado |
| 3 | Builder | Creacional | — | ❌ Descartado |
| 4 | Prototype | Creacional | P-05 evaluado | ❌ Descartado |
| 5 | Singleton | Creacional | — | ❌ Descartado |
| 6 | Adapter | Estructural | P-05 evaluado | ❌ Descartado |
| 7 | Bridge | Estructural | — | ❌ Descartado |
| 8 | Composite | Estructural | — | ❌ Descartado |
| 9 | Decorator | Estructural | P-03 evaluado | ❌ Descartado |
| 10 | Facade | Estructural | P-01 | ✅ **Adoptado** |
| 11 | Flyweight | Estructural | — | ❌ Descartado |
| 12 | Proxy | Estructural | — | ❌ Descartado |
| 13 | Chain of Responsibility | Comportamiento | P-11 evaluado | ❌ Descartado |
| 14 | Command | Comportamiento | — | ❌ Descartado |
| 15 | Iterator | Comportamiento | P-08 evaluado | ❌ Descartado |
| 16 | Mediator | Comportamiento | — | ❌ Descartado |
| 17 | Memento | Comportamiento | — | ❌ Descartado |
| 18 | Observer | Comportamiento | P-12 (declarado no intervenir) | ❌ Descartado (con deuda) |
| 19 | State | Comportamiento | evaluado sobre `Chip` | ❌ Descartado |
| 20 | Strategy | Comportamiento | P-01, P-04 | ✅ **Adoptado** |
| 21 | Template Method | Comportamiento | P-06 evaluado | ❌ Descartado |
| 22 | Visitor | Comportamiento | — | ❌ Descartado |

**Balance**: 3 adoptados (uno por familia), 19 descartados con justificación técnica. La decisión formal está en [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/04-DecisionesArquitectonicas]].

---

# Fichas creacionales

## 1. Factory Method ✅

**Familia**: Creacional.

**Punto de dolor que resolvería**: P-02 (un subtipo nuevo de Res dispara 7 puntos de decisión), P-05 (la creación tiene dos caminos paralelos que discrepan), P-06 (el switch de vacunas propagado a firmas en 4 capas), P-09 (enums paralelos) y P-10 (GUID nuevo en lectura, como subproducto).

**Evidencia**: `FabricaRes.cs:17-22` (diccionario `TipoRes→lambda`: la decisión vive en UNA clase, no en subclases — Simple Factory); `FabricaRes.cs:42-48` (segundo switch espejo); `GestorReses.cs:137-143` (`MapearTipoRes`); `RepositorioPotreroSqlite.cs:150-156` y `RepositorioVentaSqlite.cs:39-45` (switches propios de rehidratación); `IVacunaFactory.cs` + `ServicioVacunacion.cs:33-117` (4 métodos paralelos) + `VacunaController.cs:49,101` (if sobre string mágico). Responsabilidad afectada: decidir qué implementación concreta instanciar — hoy regada en 7+ sitios.

**Alternativas evaluadas**:
1. **Factory Method real** — cada subtipo aporta su propio creador (`ICreadorRes`, `ICreadorVacuna`, …); un registro único ensamblado en `Program.cs` resuelve tipo→creador; los repositorios **delegan** la rehidratación al mismo mecanismo (el GUID persistido entra por argumento → P-10 se corrige por construcción).
2. **Abstract Factory** — agrupar creadores por familia (ver ficha 2: descartada, el eje de variación es el tipo, no la familia de estilo).
3. **No hacer nada** — cada tipo nuevo sigue costando 10 archivos (Res) / 12 archivos (Vacuna), con dos caminos de creación que pueden discrepar (ya lo hacen: P-10).

**Beneficios**: un solo punto de creación para alta y rehidratación; añadir un tipo = 1 subclase + 1 creador + 1 línea de registro en el punto de ensamblaje (los switches espejo desaparecen porque el creador también sabe describir su producto — absorbe `DescribirRango` y los badges de vista vía `Serializar`); identidad preservada en persistencia; las firmas de `IVacunaFactory`/`IServicioVacunacion` colapsan (4 métodos → 1 por operación).

**Costos**: +1 clase creadora por subtipo existente (≈7-9 clases nuevas); +1 nivel de indirección entre "quiero una res" y "nace la res"; depurar exige saber qué creador participó (mitigado: el registro es legible en `Program.cs`); riesgo OCP si alguien vuelve a escribir un switch sobre el tipo — mitigado con regla explícita en [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/09-VistaTecnica]].

**Impacto** (solo diseño): clases nuevas ≈8-10 (creadores por subtipo + registro); modificadas: `FabricaRes`/`FabricaVacuna` (se transforman en registro + creadores), `IRepositorio*`/repos (delegan rehidratación), `ServicioVacunacion` (colapsa métodos), `GestorReses` (cae `MapearTipoRes` y contadores), `Program.cs` (ensambla el registro); eliminadas: ninguna física (las interfaces `I*Factory` se transforman). Capas: Domain (creadores, registro) + Infrastructure (repos delegan) + Program.cs. **Core afectado: sí — es el objetivo.**

**Veredicto: ADOPTADO**. Es la corrección directa del hallazgo central del profesor ("factorías mal implementadas"): hoy la decisión de instanciación no la toma ninguna jerarquía — la toma un diccionario y sus 5 switches espejo. Factory Method real devuelve la decisión al que varía (el subtipo) y deja el punto de extensión donde el enunciado lo permite explícitamente: el punto de ensamblaje. Es además prerrequisito de la corrección de P-05/P-10 sin tocar esquema de datos.

## 2. Abstract Factory ❌

**Familia**: Creacional.

**Punto de dolor que resolvería**: analizado sobre P-01/P-02 (el mismo clúster de creación).

**Evidencia**: familias presentes en el sistema: reses (3 subtipos) y vacunas (2 subtipos); con SC-1, productos vendibles (reses + derivados). `FabricaVacuna.cs` como prototipo de "agrupación".

**Alternativas evaluadas**: 1) Abstract Factory (una fábrica de familias: p. ej. `IFabricaGanado` que produce reses **y** sus reglas). 2) Factory Method + registro (ficha 1). 3) No hacer nada.

**Beneficios hipotéticos**: coherencia garantizada entre productos de una misma familia; sustituibilidad de familias completas.

**Costos**: una jerarquía de fábricas por encima de los creadores; cada tipo nuevo exige tocar su fábrica de familia (el punto de modificación se mueve, no desaparece); para el consumidor, una interfaz más ancha (ISP en riesgo).

**Impacto**: +2-3 jerarquías de fábrica; Core y Application afectados sin eliminar ni un switch que Factory Method no elimine ya.

**Veredicto: DESCARTADO**. El Anexo A lo advierte literalmente: *"si solo tienen una familia de productos, la abstracción adicional probablemente no se justifica"*. Aquí el eje de variación es **el tipo de producto**, no **la familia de estilo**: no existen dos variantes de "hacienda" que produzcan familias intercambiables (no hay `HaciendaLechera` vs `HaciendaDeCarne` produciendo cada una su res-potrero-vacuna coherentes). Añadir AF movería el punto de modificación (penalización OCP que el propio enunciado ejemplifica: "una fábrica que crece con un condicional por cada tipo nuevo") sin matar ningún condicional adicional. La advertencia del Anexo A existe para este caso exacto.

## 3. Builder ❌

**Familia**: Creacional.

**Punto de dolor que resolvería**: ninguno aprobado. Analizado sobre P-01 (construcción de ventas) por descarte.

**Evidencia**: aridades reales: `Venta` (3-4 argumentos), `Res` (4), `Potrero` (2), `Chip.Crear` (4). Ningún constructor telescópico; ninguna construcción paso a paso con configuración opcional.

**Alternativas evaluadas**: 1) Builder para `Venta` con partes opcionales (res **o** derivado, cantidad, monto). 2) Constructores/factorías tipadas (ficha 1). 3) No hacer nada.

**Beneficios hipotéticos**: construcción legible de objetos complejos; inmutabilidad del producto final.

**Costos**: +1 director + 1 builder por variante; la "variación" real de SC-1 es **qué producto** (tipo), no **cómo se parametriza** (configuración gradual) — Builder resuelve el problema que no tenemos.

**Impacto**: +2-4 clases en Domain sin anclar a ningún costo de cambio medido.

**Veredicto: DESCARTADO**. Builder paga cuando la construcción es el dolor (muchos parámetros opcionales, orden, representaciones múltiples del ensamblado). Aquí la aridad es 2-4 y la variación es polimórfica. Adoptarlo sería el caso de manual de "patrón por aesthetics" que la rúbrica penaliza con −0.3.

## 4. Prototype ❌

**Familia**: Creacional.

**Punto de dolor que resolvería**: P-05 evaluado en serio — la rehidratación de repositorios es clonación disfrazada: `MapearRes`/`MapearVacuna` reconstruyen objetos desde filas.

**Evidencia**: `RepositorioVacunaSqlite.cs:103-113`, `RepositorioPotreroSqlite.cs:150-156`.

**Alternativas evaluadas**: 1) Prototype: un prototipo por subtipo; rehidratar = clonar y llenar. 2) Factory Method con creadores que aceptan estado persistido (incluido el GUID). 3) No hacer nada (dos caminos de creación que discrepan).

**Beneficios hipotéticos**: rehidratación uniforme sin conocer constructores; añadir subtipos sin tocar repos.

**Costos**: para clonar-y-llenar hay que **exponer setters o métodos de llenado** en entidades que precisamente estamos devolviendo a la encapsulación (P-08: `Res.Peso` con setter público es el hallazgo a corregir); un prototipo clonado puede propagar estados inválidos heredados sin pasar por invariantes; complejidad de copia profunda (`Res` contiene `VacunasAplicadas`, `Chip`).

**Impacto**: entidades modificadas para exponer llenado (retroceso de P-08); +1 prototipo/registro por subtipo.

**Veredicto: DESCARTADO**. La alternativa 2 logra lo mismo (un solo camino de creación, sin tocar esquema) **reforzando** la encapsulación en vez de deshaciéndola: el creador rehidratante llama al constructor con el estado persistido — el invariante se verifica (o se declara explícitamente la ruta de datos-legados). Prototype aquí compraría uniformidad pagando con el principio que el reto exige no tocar. Descarte con trade-off real, no por catálogo.

## 5. Singleton ❌

**Familia**: Creacional.

**Punto de dolor que resolvería**: ninguno aprobado. Analizado porque el registro de creadores (ficha 1) es candidato natural a "instancia única".

**Evidencia**: `Program.cs:34-36` ya registra singletons de abstracción (`IGuidProvider`, `IHasher`, `TimeProvider`).

**Alternativas evaluadas**: 1) Singleton GoF (`static Instance`) para el registro de creadores. 2) Instancia única por composition root (DI), que es el mecanismo actual. 3) No hacer nada.

**Beneficios hipotéticos**: acceso global coherente al registro desde cualquier capa sin pasar dependencias.

**Costos**: acoplamiento estático de repos y servicios al tipo concreto (DIP roto — el consumidor vuelve a depender de algo concreto, exactamente el error tipificado del enunciado); sustitución en pruebas imposible sin hacks de reflexión; ciclo de vida oculto (variable global de facto).

**Impacto**: ninguna clase nueva; degradación de DIP en todo el Core.

**Veredicto: DESCARTADO** — y es el descarte que mejor responde la pregunta del Anexo A (*"qué lo diferencia de una variable global y cómo se sustituye en una prueba"*): la respuesta correcta en este sistema es que **el composition root ya proporciona instancias únicas con sustituibilidad**. Un registro `Singleton` estático sería indistinguible de una variable global con vestimenta. Si el equipo quisiera demostrar dominio del patrón en sustentación, este descarte es la evidencia.

---

# Fichas estructurales

## 6. Adapter ❌

**Familia**: Estructural.

**Punto de dolor que resolvería**: P-05 evaluado (la conversión fila-relacional → objeto de dominio).

**Evidencia**: los 7 repositorios SQLite ya realizan adaptación informal: `MapearRes`, `MapearVacuna`, `MapearChip`, `MapearGeolocalizacion`.

**Alternativas evaluadas**: 1) Adapter formal por agregado (`IAdaptadorFila<Res>`). 2) Creadores que aceptan estado persistido (ficha 1). 3) No hacer nada.

**Beneficios hipotéticos**: nombrar y estandarizar la conversión; reutilizar mapeo entre repos.

**Costos**: una capa paralela a la factoría (¿quién adapta, el adapter o el creador?); los mapeos de reses están **duplicados entre repos** (Potrero l.150, Venta l.39) — el dolor es la duplicación de decisión, no la falta de una interfaz de conversión.

**Impacto**: +7 adaptadores; repos modificados; ninguna decisión de subtipo eliminada que la ficha 1 no elimine.

**Veredicto: DESCARTADO**. El dolor medido en P-05 es "**quién decide** el subtipo", no "**cómo se convierte** una fila". El adaptador sin factoría dejaría los switches intactos dentro de adaptadores mejor nombrados. Se resuelve en la ficha 1: el creador rehidratante ES la interfaz entre el mundo relacional y el constructor de dominio.

## 7. Bridge ❌

**Familia**: Estructural.

**Punto de dolor que resolvería**: ninguno aprobado. Analizado sobre P-01 (si el eje "producto" × eje "persistencia" justificara separarlos).

**Evidencia**: un único motor de persistencia (SQLite/Dapper, congelado por alcance); la variación está en un solo eje: el tipo de producto.

**Alternativas evaluadas**: 1) Bridge (`AbstraccionVendible` × `ImplementacionPersistencia`). 2) Jerarquía única de productos + repos congelados. 3) No hacer nada.

**Beneficios hipotéticos**: variar persistencia sin tocar la jerarquía de productos (y viceversa).

**Costos**: Bridge paga cuando **ambos ejes varían independientemente**; aquí uno está congelado por el encargo ("base de datos real" fuera de alcance) — pagaríamos la separación de un eje que el enunciado prohíbe mover.

**Impacto**: +2 jerarquías (abstracción e implementación) para un solo caso real de variación.

**Veredicto: DESCARTADO**. Separar un eje congelado es sobre-ingeniería con nombre técnico. Queda declarado en [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/09-VistaTecnica]] como decisión revisitada si algún día cambia el motor de persistencia.

## 8. Composite ❌

**Familia**: Estructural.

**Punto de dolor que resolvería**: ninguno aprobado. Analizado: `Potrero` contiene reses (¿árbol?); historia clínica compuesta sería SC-3 (no elegida).

**Evidencia**: `Potrero.Reses` es agregación plana de un nivel; no hay tratamiento uniforme parte-todo en ningún flujo aprobado.

**Alternativas evaluadas**: 1) Composite potreros→reses→chips. 2) Agregación plana actual. 3) No hacer nada.

**Veredicto: DESCARTADO**. No existe operación que deba aplicarse recursivamente sobre una estructura arbórea (registrar peso a TODO el árbol, serializar el potrero completo…). El patrón resolvería uniformidad que nadie pidió. Reevaluable si SC-3 (historia clínica con episodios anidados) se elige en el futuro — anoto el escenario en [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/09-VistaTecnica]].

## 9. Decorator ❌

**Familia**: Estructural.

**Punto de dolor que resolvería**: P-03 evaluado (envolver servicios para tipificar/classificar resultados sin tocarlos).

**Evidencia**: contrato string de servicios (`ChipController.cs:45,70` detecta éxito con `Contains`).

**Alternativas evaluadas**: 1) Decorator sobre `IServicio*` que intercepta y tipifica el resultado. 2) Corregir el contrato (descartado: P-03 es no-intervenir — comportamiento congelado). 3) No hacer nada.

**Beneficios hipotéticos**: clasificación success/danger sin modificar servicios.

**Costos**: un decorator por servicio afectado; el envoltorio **reinterpreta** el mismo string (la heurística solo cambia de casa); riesgo de envoltura que "cambia el contrato de lo que envuelve" (error LSP tipificado por el enunciado: "el cliente nota la diferencia").

**Veredicto: DESCARTADO**. Decorator traslada la heurística sin eliminarla — mueve el problema de capa, que es exactamente lo que el equipo hizo mal en el Reto 1. P-03 quedó formalmente declarado deuda congelada; envolverlo no la paga.

## 10. Facade ✅

**Familia**: Estructural.

**Punto de dolor que resolvería**: P-01 — y la queja directa de la Líder Técnica: *"interfaces limpias y ningún lugar donde se pueda leer cómo colaboran entre ellas… cómo se ensambla el sistema: hay que leerlo todo"*.

**Evidencia**: el pipeline de venta hoy exige leer 9 piezas para entender una operación: `ResController.Vender` → `ServicioVentas.VenderRes` → `FabricaVenta` → `ValidadorVenta` → `Potrero.RemoverRes` → 2 repositorios → con SC-1 además fabricación de derivados + estrategia de precio. `ServicioVentas.cs:32-54` ya orquesta, pero asumiendo el caso único res.

**Alternativas evaluadas**: 1) **Facade declarada sobre el subsistema de venta**: `ServicioVentas` reestructurado como la interfaz unificada (`Vender(especificación)`) que coordina creadores + estrategia de precio + reglas del producto + potreros + persistencia; los controladores dependen solo de ella. 2) Mediator (ficha 16: descartado). 3) No hacer nada (con SC-1, el controlador tendría que conocer y coordinar 5+ colaboradores nuevos).

**Beneficios**: un punto legible donde leer la colaboración del subsistema de venta (responde la pregunta de ensamblaje del enunciado); los controladores no conocen creadores ni estrategias; SC-1 se implementa detrás de la fachada sin nuevas dependencias hacia arriba.

**Costos**: riesgo tipificado por el Anexo A — *"tiende a absorber lógica de negocio hasta romper SRP"*. **Límite declarado**: la fachada ORQUESTA (secuencia: obtener producto → validar → calcular → persistir), NO decide reglas (las reglas viven en el dominio: `Res.EvaluarPeso`, `IVendible.ValidarParaVenta`). Métrica de control: si un método de la fachada contiene una condición de negocio, se pasó del límite. Costo adicional: una indirección más entre UI y dominio.

**Impacto**: nuevas: 0 (el rol lo asume `ServicioVentas` reestructurado — la fachada es el servicio existente con contrato unificado y dependencias declaradas); modificadas: `IServicioVentas`/`ServicioVentas`, `ResController` (redirige la entrada de venta), `Program.cs`; Capas: Application (orquestación) + Domain (reglas que la fachada NO absorbe). Core: indirectamente fortalecido.

**Veredicto: ADOPTADO**. Es la respuesta estructural a la pregunta "cómo se componen las estructuras existentes" del alcance, con el límite explícito que el Anexo A exige. Adoptarlo como **rol del servicio existente** (en vez de una clase nueva encima) evita lafachada-de-fachada y muestra que el patrón es una decisión de responsabilidades, no de nombres.

## 11. Flyweight ❌

**Familia**: Estructural.

**Punto de dolor que resolvería**: ninguno. Sin candidatura seria: no hay masas de objetos finos compartidos con estado extrínseco (el sistema maneja decenas de entidades, no millones).

**Alternativas evaluadas**: 1) Flyweight de vacunas/reses. 2) Nada. 3) Nada con más rigor.

**Veredicto: DESCARTADO**. Sin punto de dolor, sin costo de cambio medible, sin adopción. Caso de manual de la regla de anclaje (−0.3).

## 12. Proxy ❌

**Familia**: Estructural.

**Punto de dolor que resolvería**: ninguno aprobado. Candidatura: carga diferida de `VacunasAplicadas`/`Chip` al listar reses.

**Evidencia**: `GestorReses.ListarReses` llama manualmente a `CargarVacunasAplicadasEnPotreros` (`GestorReses.cs:112-122`) — un "proxy escrito a mano".

**Alternativas evaluadas**: 1) Proxy de carga diferida sobre `IRepositorio*`. 2) Carga explícita actual. 3) No hacer nada.

**Costos**: el proxy introduce estado (¿ya cargué?) en un sistema sin concurrencia ni latencia medible; persistencia congelada por alcance; riesgo de comportamiento (cuándo se dispara la carga cambia el orden de accesos a BD).

**Veredicto: DESCARTADO**. La carga diferida es optimización de un recurso que el encargo sacó del alcance (base de datos real). El proxy aquí sería decoración costosa sin dolor presente.

---

# Fichas de comportamiento

## 13. Chain of Responsibility ❌

**Familia**: Comportamiento.

**Punto de dolor que resolvería**: P-11 (validadores sin composición: una regla nueva = editar la clase existente).

**Evidencia**: `Validador*.cs` (4 clases de 2 checks triviales, sin mecanismo de composición); `GestorReses.AgregarRes` mezcla `throw` y `ValidationResult` (`GestorReses.cs:40` vs `50-51`).

**Alternativas evaluadas**: 1) CoR: cadena de `IReglaValidacion` donde cada regla procesa y pasa la solicitud. 2) Composición polimórfica: cada producto valida lo suyo (`IVendible.ValidarParaVenta`), las reglas compartidas viven en VOs/creadores. 3) No hacer nada.

**Beneficios hipotéticos**: reglas enchufables en cadena; añadir regla = nueva clase sin editar otras.

**Costos**: la semántica de validación del dominio es **colectar todos los errores** (así funciona `ValidationResult` y así lo muestran los mensajes actuales — comportamiento observable), mientras CoR clásico **corta en el primer handler que atiende**. Forzar la cadena a acumular es usar el patrón contra su intención; añadiría N clases de regla + indirección de enlace para obtener… una lista de reglas, que la alternativa 2 ya compone por polimorfismo.

**Impacto**: +6-10 clases de reglas; Application hinchada en el punto que queremos adelgazar.

**Veredicto: DESCARTADO**. El dolor de P-11 es real pero su remedio no es una cadena: la validación correcta ya está migrando a VOs + creadores + polimorfismo del producto (ficha 1 y 20). P-11 se cierra **degradando** los 4 validadores triviales (duplican mecanismos de error) y dejando la composición donde pertenece. Descarte técnico con análisis de semántica del patrón — no un "no se necesita".

## 14. Command ❌

**Familia**: Comportamiento.

**Punto de dolor que resolvería**: ninguno aprobado. Candidatura: encapsular "vender" como comando desacoplado del controlador.

**Alternativas evaluadas**: 1) Command por operación de negocio (`VenderResCommand`, `AplicarVacunaCommand`…). 2) Servicios actuales (ya encapsulan la operación tras una interfaz). 3) No hacer nada.

**Costos**: Command paga cuando hay **cola, deshacer, auditoría de operaciones o despacho diferido** — ninguno en alcance; una clase comando por operación duplica la superficie de `IServicio*` sin escenario de cambio que lo exija.

**Veredicto: DESCARTADO**. Sin parámetros de encendido (undo/queue/log), Command es una interfaz con un método `Ejecutar()` envolviendo otra interfaz. Regla de anclaje.

## 15. Iterator ❌

**Familia**: Comportamiento.

**Punto de dolor que resolvería**: P-08 evaluado (encapsular colecciones del agregado).

**Evidencia**: `Res.VacunasAplicadas` es `List<Vacuna>` pública; `Potrero` expone sus reses.

**Alternativas evaluadas**: 1) Iterator custom por agregado. 2) Exponer `IReadOnlyList<>`/`IEnumerable<>` + métodos de dominio (`Res.AplicarVacuna`). 3) No hacer nada.

**Veredicto: DESCARTADO**. El remedio de P-08 es encapsulación de dominio (la alternativa 2 — se aplica en el TO-BE como refactor del Core, no como patrón), no un protocolo de recorrido: no hay traversals especiales (filtrados, saltos, árboles) que requieran implementar `IEnumerator`. El patrón añadiría clases de recorrido para recorrer listas que el lenguaje ya recorre.

## 16. Mediator ❌

**Familia**: Comportamiento.

**Punto de dolor que resolvería**: evaluado sobre P-01/P-13 (coordinación multi-repo en `ServicioChip` y `ServicioVentas`).

**Alternativas evaluadas**: 1) Mediator central entre servicios/repos. 2) Fachada por caso de uso (ficha 10). 3) No hacer nada.

**Costos**: un mediador central vuelve copartícipes a todos los colegas (todos lo conocen y él a todos): más acoplamiento total, no menos; duplicaría el rol de la fachada con la variante peor (la fachada es unidireccional y legible; el mediador es una plaza de tráfico).

**Veredicto: DESCARTADO**. La colaboración que hay que volver legible es **por caso de uso** (vender), no transversal entre colegas paritarios. La ficha 10 la resuelve sin la matriz de referencias cruzadas del mediador.

## 17. Memento ❌

**Familia**: Comportamiento.

**Punto de dolor que resolvería**: ninguno. Sin deshacer, sin historial de estados de entidades, sin rollback en alcance.

**Alternativas evaluadas**: 1) Memento del peso/estado de la res. 2) Nada. 3) Nada.

**Veredicto: DESCARTADO** por la regla de anclaje: ningún P-XX pide restaurar estados previos. (La "historia clínica" de SC-3 es registro de eventos nuevos, no restauración — y SC-3 no fue elegida.)

## 18. Observer ❌ (con deuda declarada)

**Familia**: Comportamiento.

**Punto de dolor que resolvería**: P-12 — Observer a medio instalar: publica a consola invisible y la reacción real se duplica concatenando strings junto al publish.

**Evidencia**: `DomainEventPublisherConsola.cs:7-10`; `mensajeEventos += "\n[Evento] …"` junto a cada `Publicar` (`GestorReses.cs:55-75,95-105`); `VacunaVencidaEvent` definido y jamás publicado.

**Alternativas evaluadas**: 1) **Observer completo**: `IDomainEventPublisher` con suscripción múltiple y handlers por tipo; los strings de reacción los produce un handler y el servicio los recoge. 2) Mantener publisher + strings manuales (status quo). 3) Eliminar el publisher muerto.

**Beneficios hipotéticos**: reacción al evento desacoplada y extensible (SC-3 dispararía eventos clínicos sin reeditar publicadores); fin de la duplicación publish/string.

**Costos**: la composición de los mensajes visibles **es comportamiento observable congelado (L1: −0.5 por caso)**: reordenar cómo se ensamblan los textos de TempData es exactamente el tipo de salida que no se autorizó a cambiar; tocar `GestorReses`/`ServicioVacunacion` en su zona de mensajes con 2 días de implementación es riesgo desproporcionado.

**Impacto**: infra de eventos + 2 publicadores + handlers; riesgo directo sobre las salidas de los 8 casos del Reto 1.

**Veredicto: DESCARTADO EN RETO 2, CON DEUDA DECLARADA**. Este es el descarte más incómodo y por eso el más honesto: el patrón encaja técnicamente (la abstracción ya existe a medias), pero el encargo congela la reacción observable que habría que rediseñar. La líder técnica fue explícita: no acepta robustez comprada rompiendo lo pagado. Queda registrado en [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/07-Riesgos]] y [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/09-VistaTecnica]] con su señal de alerta: **si una futura SC autoriza tocar mensajes de eventos, Observer es el primer patrón a incorporar**.

## 19. State ❌

**Familia**: Comportamiento.

**Punto de dolor que resolvería**: evaluado fuera de inventario: la máquina de estados de `Chip` (`Chip.cs:47-71`, `ValidarTransicionEstado` con switch) es el candidato natural de manual.

**Evidencia**: switch de transiciones Activo/Inactivo/Perdido/Dañado en una sola clase, cerrada y probada; añadir un estado hoy cuesta 1 switch interno + 1 badge de vista.

**Alternativas evaluadas**: 1) State: una clase por estado con transiciones polimórficas. 2) Switch interno actual. 3) Tabla de transiciones (diccionario).

**Costos**: 4+ clases de estado + contexto modificable en la entidad mejor encapsulada del sistema (`Chip` es el ejemplo a imitar según [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/01-AS-IS]] §2.1) — se tocaría lo que funciona para resolver lo que no duele.

**Veredicto: DESCARTADO** por anclaje: **no existe P-XX aprobado** sobre el estado del chip (costo de cambio actual: 1 archivo). Adoptarlo activaría la penalización −0.3 del enunciado. Se documenta el umbral de adopción futura: si aparecen ≥2 estados nuevos o reglas de transición dependientes de contexto, State pasa la prueba de costo.

## 20. Strategy ✅

**Familia**: Comportamiento.

**Punto de dolor que resolvería**: P-01 (SC-1: el comportamiento de venta varía por tipo de producto) y P-04 (la selección de regla según categoría hoy es un if en el servicio). Es además el alcance explícito del encargo: *"cómo se selecciona y coordina el comportamiento en tiempo de ejecución"*.

**Evidencia**: hoy el "comportamiento por tipo" vive en condicionales: `ServicioVacunacion.cs:137-143` (if por categoría duplicando a `Res`), `VacunaController.cs:49,101` (if por string), y con SC-1 aparecería el bifurcador `if (producto es res) … else (derivado) …` en servicio y validador si no se diseñara.

**Alternativas evaluadas**: 1) **Strategy**: `IEstrategiaPrecio` con `MontoManual` (reses: preserva exactamente el comportamiento actual — el monto lo sigue dando el usuario) y `PrecioUnitario` (derivados: nuevo comportamiento autorizado por SC-1); seleccionada al crear el producto (junto a su creador, ficha 1) y consumida por la fachada (ficha 10). 2) Template Method (la variación es el comportamiento completo, no un paso del algoritmo). 3) No hacer nada / condicional por tipo (el patrón que ya duele en el AS-IS y que SC-1 multiplicaría).

**Beneficios**: el comportamiento de precio/variedad se selecciona en runtime sin tocar consumidores; añadir un derivado con política de precio distinta (2×1, precio por bulto) = nueva estrategia + registro, sin editar la fachada ni los servicios; elimina el bifurcador por tipo que SC-1 induciría; la estrategia `MontoManual` documenta explícitamente que el comportamiento de reses no cambia (verificable en los 12 casos de [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/06-VerificacionSOLID]]).

**Costos**: +1 interfaz +2-3 implementaciones; una indirección más entre "vender" y "saber el precio" (depurar exige preguntar qué estrategia participó); riesgo ISP si la interfaz crece con métodos no aplicables a todas las variantes (mitigado: una sola operación `Calcular`).

**Impacto**: nuevas: `IEstrategiaPrecio` + 2-3 estrategias; modificadas: `Venta`/`ServicioVentas` (delegan el cálculo), registro en `Program.cs`; eliminadas: ninguna. Capas: Domain (estrategias + contrato) + Application (fachada delega). Core: sí.

**Veredicto: ADOPTADO**. SC-1 introduce variación de comportamiento genuina por tipo de producto: es el caso legítimo de Strategy (elijo el algoritmo en ejecución, el cliente no cambia). La estrategia `MontoManual` para reses garantiza que la adopción **preserva el comportamiento congelado** en lugar de riesgarlo — decisión de diseño que la matriz SOLID podrá evidenciar celda por celda.

## 21. Template Method ❌

**Familia**: Comportamiento.

**Punto de dolor que resolvería**: P-06 evaluado: `CrearLoteVacunaBacteriana`/`CrearLoteVacunaViva` son cuerpos clonados (`ServicioVacunacion.cs:61-117`).

**Evidencia**: algoritmo de lote idéntico salvo la llamada de creación unitaria.

**Alternativas evaluadas**: 1) Template Method: clase base `GeneradorLotes` con el esqueleto y el paso `CrearUnica()` abstracto por subclase. 2) **Un método parametrizado `CrearLote(categoria, …)` que resuelve el creador por registro (ficha 1)**. 3) No hacer nada (clonación actual).

**Costos de TM**: una jerarquía nueva para variar **un paso que ya delega la factoría**; la herencia como mecanismo de reutilización donde alcanza la composición de datos (qué creador inyectar); riesgo LSP tipificado por el enunciado ("subclase que implementa el paso vacío").

**Veredicto: DESCARTADO**. La duplicación de lotes es real y se corrige — pero con la alternativa 2, porque la variación es **un dato** (qué vacuna crear), no una secuencia de pasos. El Template Method se reserva para cuando el esqueleto del algoritmo sea estable y los pasos varíen por familia; aquí el esqueleto entero es compartido y el único paso variable ya tiene dueño polimórfico (el creador). Descarte con análisis de por qué la alternativa más liviana gana.

## 22. Visitor ❌

**Familia**: Comportamiento.

**Punto de dolor que resolvería**: ninguno aprobado. Candidatura: operaciones sobre la jerarquía de Res (serializar, estadísticas) sin tocar los subtipos.

**Alternativas evaluadas**: 1) Visitor con `IVisitanteRes` y `Aceptar()` en cada subtipo. 2) Métodos polimórficos en los subtipos (`Serializar` ya existe y funciona así). 3) No hacer nada.

**Costos**: Visitor sirve cuando **la estructura es estable y las operaciones varían**; aquí es exactamente al revés: los **tipos crecen** (SC-1 lo demuestra) y las operaciones son pocas y estables. Añadir un subtipo con Visitor obliga a tocar la interfaz del visitante y todas sus implementaciones — un generador de OCP inverso. Además distribuiría `Serializar` (hoy polimórfico y legible en cada subtipo) hacia afuera del dominio, contra el mandato de fortalecer el Core.

**Veredicto: DESCARTADO**. El patrón correcto para la dirección de cambio equivocada. Es el descarte que mejor demuestra comprensión del trade-off estructura/operaciones en sustentación.

---

> [!tip] Navegación
> Dolor: [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/02-PuntosDolor]] · Decisión formal y diseño de los 3 adoptados: [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/04-DecisionesArquitectonicas]] · Verificación SOLID de esta adopción: [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/06-VerificacionSOLID]] · Plan: [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion1/00-Plan]]
