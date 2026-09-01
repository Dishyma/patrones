---
tags: [reto2, actividad-4, verificacion, solid, comportamiento, hacienda]
estado: en-revision
fecha: 2026-08-30
---

# 06 — Verificación: SOLID sigue en pie y el comportamiento no cambió (Actividad 4)

> [!abstract] Propósito
> Demostrar —no afirmar— que (a) ningún patrón adoptado rompe SOLID (toda tensión declarada y compensada) y (b) el comportamiento observable es idéntico al AS-IS. Contiene los entregables 4.1 (matriz de verificación con evidencia) y 4.2 (los doce casos de comportamiento, salidas lado a lado).

> [!info] Principio rector
> "Un patrón mal aplicado deshace SOLID con una elegancia que asusta" (enunciado). Cada celda no-Neutra lleva su línea de evidencia. Ninguna celda puede quedar en **Roto** sin declaración del beneficio que lo compensa — en este diseño **no hay celdas Roto**.

---

## 4.1 · Entregable 1 — Matriz de verificación Patrón × SOLID

| Patrón adoptado | SRP | OCP | LSP | ISP | DIP |
|-----------------|-----|-----|-----|-----|-----|
| **Factory Method** | Refuerza | Refuerza | Tensionado pero compensado | Neutro | Refuerza |
| **Builder** | Refuerza | Refuerza | Refuerza | Neutro | Refuerza |
| **Template Method** | Refuerza | Refuerza | Tensionado pero compensado | Neutro | Refuerza |
| **Observer** | Refuerza | Refuerza | Neutro | Refuerza | Refuerza |

### Evidencia por celda (toda celda ≠ Neutro)

**Factory Method**

- **SRP — Refuerza.** Cada creador tiene una única razón de cambio: su subtipo. Evidencia de contraste: en el AS-IS `FabricaRes` cambia por cualquier subtipo (`FabricaRes.cs:17-23`) y además por el switch de rangos (`:42-48`) — dos razones en una clase.
- **OCP — Refuerza.** El escenario medido P-01 baja de 9 puntos de edición en 4 capas a **1 clase nueva + 1 registro**. Es la definición operativa de "abierto a extensión, cerrado a modificación".
- **LSP — Tensionado pero compensado.** Tensión: los creadores concretos deben poder sustituir a la base en el registro sin romper a `GestorReses` ni a los repositorios. Compensación: el contrato del creador es cerrado y pequeño (`TipoAtendido`, hook `Construir`, `Rehidratar`); ningún creador puede rechazar la llamada ni lanzar tipos nuevos — las validaciones lanzan las **mismas excepciones con los mismos textos** que hoy (`ArgumentException` por nombre vacío, etc.). Evidencia de que es compensado y no roto: los 4 creadores actuales implementan el contrato completo sin pasos vacíos.
- **DIP — Refuerza.** `GestorReses` y los repositorios dependen de `RegistroDeReses` (abstracción); ninguna clase de Application/Infrastructure nombra un creador concreto. Mismo idioma ya probado en `AutorizadorRbca.cs:13-16`.

**Builder**

- **SRP — Refuerza.** El builder solo ensambla; las invariantes se validan en `Build()`; el cálculo del total vive con la venta. Contraste AS-IS: `FabricaVenta.cs:20` validaba monto (tres razones: construir, validar, envolver dinero).
- **OCP — Refuerza.** Un tipo de ítem nuevo (`ProductoDerivado`) entra implementando `IVendible` — el builder no se toca.
- **LSP — Refuerza.** `Res` conserva exactamente su superficie actual (DEC-09): cualquier código que hoy funciona con `Res` sigue funcionando; `ProductoDerivado` implementa el mismo contrato mínimo. Nadie recibe un mensaje que no pueda atender (la lección del `ReadOnlyDocument`/`Electrico.Repostar()` de las diapositivas — [[guía SOLID]] §5).
- **DIP — Refuerza.** El builder pide `IVendible` y publicadores abstractos, nunca `Res`-concreto-ni-producto-concreto.

**Template Method**

- **SRP — Refuerza.** El pipeline de creación existe una vez. Contraste: la regla de monto vivía en 3 clases con 2 umbrales (`FabricaVenta.cs:20`, `Dinero.cs:10-11`, `ValidadorVenta.cs:15`) — P-07.
- **OCP — Refuerza.** Extender el pipeline = nuevo creador colgado de la base; modificarlo = editar una clase (la base) — y esa es su **única** razón de cambio.
- **LSP — Tensionado pero compensado.** ⚠️ Tensión declarada en [[Reto2-Hacienda/Opcion1/04-DecisionesArquitectonicas]] DEC-03: el riesgo clásico es "un paso que alguna subclase no puede cumplir" (la advertencia textual del enunciado). **Compensación:** los hooks del esqueleto son **propiedades-dato del subtipo** (`EsEdadValida`, `MaxVacunasBacterianas/Vivas`, rangos — propiedades que ya existen en `Res.cs:27-33` y `Cebon.cs:10-16`), no métodos que alguien pueda implementar vacíos; el esqueleto está sellado en su estructura (pasos fijos: validar comunes → construir → exigir regla → publicar). No existe un paso "optativo": el mínimo común es exigible a **todo** subtipo porque es la definición de ser una res/vacuna/producto. Evidencia de compensación en código: los 3 subtipos actuales ya declaran todas las propiedades-dato (verificado `Ternero/Cebon/Novillo.cs:10-16`).
- **DIP — Refuerza.** El paso "publicar ocurrido" de la base habla con `IDomainEventPublisher` inyectado — nunca con la consola concreta.

**Observer**

- **SRP — Refuerza.** El publicador no conoce consumidores (fin de la doble responsabilidad publicar+imprimir de `DomainEventPublisherConsola.cs:5-11`); cada handler tiene una sola reacción.
- **OCP — Refuerza.** Nueva reacción (stock mínimo de derivados SC-1) = 1 handler + 1 registro; cero ediciones en publicadores.
- **ISP — Refuerza.** `IDomainEventHandler<T>` segregado por tipo de evento: un handler de `VentaRealizadaEvent` no ve métodos de `VacunaAplicadaEvent`.
- **DIP — Refuerza.** La consola deja de ser destino hardcodeado y pasa a **handler inyectado**; el despachador implementa la abstracción existente `IDomainEventPublisher` — los servicios publicadores no cambian ni una línea.

### Mini-matriz complementaria — decisiones de diseño sin patrón ([[Reto2-Hacienda/Opcion1/04-DecisionesArquitectonicas]] §3)

| Decisión | SRP | OCP | LSP | ISP | DIP |
|----------|-----|-----|-----|-----|-----|
| DEC-07 Encapsulamiento del Core | Refuerza¹ | Neutro | Neutro² | Neutro | Neutro |
| DEC-08 Contrato de resultados | Refuerza³ | Neutro | Neutro | Neutro | Refuerza |
| DEC-09 Contrato hacia vistas | Neutro | Tensionado pero compensado⁴ | Neutro | Neutro | Neutro |

¹ Las entidades dejan de tener dos razones de cambio (datos + quién los muta desde afuera). ² La superficie pública se **reduce** sin cambiar contratos existentes usados por vistas. ³ Fin del parsing de mensajes como semántica (`VacunaController.cs:153`). ⁴ Tensión: mantener `TipoRes` vivo parecería conservar el punto de decisión; compensación: el enum queda degradado a **superficie de lectura** (vistas/BD), la decisión vive en el registro — declarado en DEC-09.

### Mapa contra los "errores típicos" del enunciado

| Situación prohibida | Nuestra defensa |
|---------------------|-----------------|
| Una fábrica que crece con un condicional por tipo nuevo (OCP: se movió el punto de modificación) | No hay condicional: registro abierto de creadores; tipo nuevo = clase nueva ([[Reto2-Hacienda/Opcion1/05-TOBE]] E-12) |
| Una fachada que absorbe lógica de negocio (SRP) | No adoptamos fachada; los servicios existentes **pierden** reglas hacia el Core ([[Reto2-Hacienda/Opcion1/03-PatronesEvaluados]] §3.4) |
| Un punto de acceso global de instancia única (DIP/testabilidad) | Sin Singleton; unicidad por composition root ([[Reto2-Hacienda/Opcion1/03-PatronesEvaluados]] §3.2) |
| Un método plantilla con pasos que alguna subclase no puede cumplir (LSP) | Hooks como propiedades-dato + esqueleto sellado (evidencia arriba) |
| Una envoltura que cambia el contrato de lo que envuelve (LSP) | `DespachadorDeEventos` implementa la interfaz existente sin tocar su firma; `VentaBuilder` no cambia la superficie de `Venta`; `HandlerConsola` reproduce la salida línea por línea |

---

## 4.2 · Entregable 2 — Los doce casos: comportamiento congelado

> [!important] Protocolo de captura (D-03 se ejecuta aquí)
> 1. **Antes de refactorizar:** ejecutar el AS-IS (`dotnet run` en `Hacienda.Web`) y capturar por caso: mensaje en pantalla + salida de consola del servidor (los eventos van a consola — `DomainEventPublisherConsola.cs:5-11`).
> 2. **Después de refactorizar + SC-1:** repetir exactamente los mismos pasos.
> 3. Comparar **lado a lado** (columna ❖ del kit de evidencias `04-evidencia/`): coincidencia = ✅.
> La columna "Salida congelada (fuente)" cita el código que hoy la produce — **el texto exacto se congela de la ejecución, no de la memoria**: si la captura difiere de esta tabla, manda la captura.

### Casos del Reto 1 (8) — flujos existentes, no pueden variar en nada

| # | Caso | Qué recorre (patrón tocado) | Entrada | Salida congelada (fuente AS-IS) | Captura antes / después |
|---|------|------------------------------|---------|--------------------------------|--------------------------|
| C-01 | Login de los 3 roles | Ninguno (control: permisos efectivos idénticos — P-08 no intervenido) | admin/admin123 · empleado/emp456 · visitante/visit789 | Redirección y mensajes actuales (`AccountController.cs:37-46`, credenciales de `DataLoader.cs:42-58`) | ⬜ / ⬜ |
| C-02 | Crear potrero con identificación vacía | TM (regla común al esqueleto) | identificación="" | Mensaje de validación actual (`ValidadorPotrero`/`FabricaPotrero.cs:20-21`) — misma redacción en el esqueleto | ⬜ / ⬜ |
| C-03 | Crear res con edad inválida para el tipo | FM+TM (regla del subtipo como dato) | Ternero con edad 30 | Mensaje de rango actual (`FabricaRes.cs:33-37`: "La edad … no es válida … Rango: …") — el rango ahora lo aporta el subtipo, **el texto es idéntico** | ⬜ / ⬜ |
| C-04 | Alimentar res hasta cruzar `PesoMinimo` y `PesoRecomendadoVenta` | TM/Observer (publicación al final del flujo) | Alimentar novillo 400→550 kg | Eventos "[Evento] …" actuales de `GestorReses.cs:56-65` + consola idéntica vía `HandlerConsola` | ⬜ / ⬜ |
| C-05 | Aplicar vacunas hasta exceder el límite del tipo | FM+TM+DEC-07 (regla al dominio) | 5 vacunas vivas a un Ternero (máx 1) | Mensaje de límite actual (`ServicioVacunacion.cs:134-143`) — ahora lo lanza `Res.AplicarVacuna`, **mismo texto** | ⬜ / ⬜ |
| C-06 | Crear lote de vacunas mixto | FM (selección por `DatosVacuna`, sin if/else del controlador) | Lote base "L1", 3 bacterianas + 2 vivas | Numeración `{loteBase}-{i:D3}` (`ServicioVacunacion.cs:72`) y mensajes de lote — idénticos | ⬜ / ⬜ |
| C-07 | Instalar chip + registrar ubicación y ver distancia | FM rehidratación + Observer | chip activo + 2 ubicaciones | Mensajes `Contains("correctamente"/"registrada")` actuales (`ChipController.cs:45,70`) + Haversine (`ServicioGeolocalizacion.cs:88-99`) — cálculo bit a bit | ⬜ / ⬜ |
| C-08 | Vender una res | Builder+Observer (venta por ítems ahora) | Venta de novillo 550 kg | Mensaje de éxito actual de venta (`ResController.cs:79-93` → `ServicioVentas`) + res fuera del potrero | ⬜ / ⬜ |

### Casos nuevos (4) — recorren lo que los patrones tocan

| # | Caso | Qué recorre | Entrada | Comportamiento esperado | Captura |
|---|------|-------------|---------|-------------------------|---------|
| C-09 | Crear vacuna bacteriana y viva por la ruta nueva | FM+TM (`DatosVacuna` → registro → creador; **sin** `if (tipoVacuna=="Bacteriana")` del `VacunaController.cs:49-68`) | Formulario de vacuna tal cual hoy | Mensajes idénticos a los actuales (las mismas validaciones con los mismos textos, ahora dentro del hook `ValidarPropios`) | ⬜ |
| C-10 | Venta con derivado (SC-1, cambio **autorizado**) | Builder + `IVendible` + producto de creadores | Venta de 20 L de leche + 1 cuero | **Comportamiento nuevo autorizado**: mensaje de éxito consistente con el estilo actual; listado de ventas muestra la venta con ítems. Los casos C-08 sobre venta de res deben seguir idénticos | ⬜ |
| C-11 | Rehidratación de ventas con identidad estable | FM (`RegistroDeReses.Rehidratar` preserva `Id`) | Listar ventas 2 veces seguidas | **Interno, no observable**: el listado en pantalla es byte a byte igual; el `Id` de la res vendida ya no cambia entre lecturas (bug P-09 corregido — evidencia con log de depuración) | ⬜ |
| C-12 | Agregar `VacaLechera` (demostración OCP en vivo) | FM completo | Un solo commit: 1 clase + 1 registro (ni un switch editado) | El sistema crea/muestra la vaca lechera en estadísticas (polimórficas, `GestorReses.cs:130-132` ahora agrupa por `Tipo`); **nota de maquillaje**: el badge de la vista es un switch congelado (`Views/Res/Index.cshtml:71-77`) — ver riesgo R-3 abajo | ⬜ |

> [!warning] Nota de alcance sobre C-12 y las vistas
> SC-1 es la solicitud **autorizada**: sus pantallas nuevas (productos, venta con derivados) son parte del cambio autorizado y no violan el congelamiento. Distinto es el refactor de patrones: no toca ninguna vista existente. El badge de un tipo nuevo de res es la única fricción estética (la vista vieja no conoce la vaca lechera); se documenta como limitación declarada, no como cambio de comportamiento.

### Comprobación "el código corresponde al diagrama" (rúbrica 4)

Al implementar, cada E-XX de [[Reto2-Hacienda/Opcion1/05-TOBE]] §3.2 se marca ✅ con su archivo real; el diagrama se re-genera desde el código antes de exportar el PDF. Regla de [[00-Plan]] §5: nada documentado contradice lo implementado.

---

## Dudas abiertas y riesgos

| ID | Riesgo/Duda | Mitigación |
|----|-------------|------------|
| R-1 | Un texto de mensaje difiere en la migración al esqueleto/builder (−0.5 por caso) | Tabla de mensajes congelados: extraer los literales exactos del código en el primer día de implementación y trasladarlos por copia, no por memoria |
| R-2 | El orden de handlers del Observer altera la consola | `HandlerConsola` registrado primero, síncrono; caso C-04 lo verifica |
| R-3 | Badge de vista para tipo nuevo (C-12) | Declarado como limitación estética autorizada; no se editan vistas existentes |
| D-03 | ✅ Resuelta aquí: los 12 casos están enumerados (C-01…C-12); falta ejecutar las capturas "antes" **antes** de refactorizar | Dueño: Arquitecto de Verificación (frente pendiente de asignar — D-04) |

---

## Navegación

- [[Reto2-Hacienda/Opcion1/05-TOBE]] — el diseño cuyas promesas aquí se auditan.
- [[Reto2-Hacienda/Opcion1/07-Riesgos]] — la Actividad 5 retoma R-1…R-3 como riesgos formales del registro.
- [[Reto2-Hacienda/Opcion1/10-BitacoraIA]] — B-10/B-11 registran la decisión D-05 y el diagrama de capas.
