---
title: "Reto 2 — Vista para el equipo de desarrollo"
tags:
  - arquitectura-software
  - reto2
  - hacienda
  - vista-tecnica
audiencia: "El ingeniero que entra dentro de seis meses y tiene que cambiar algo sin romper nada"
estado: "completa"
---

# 09 — Vista para el equipo de desarrollo

> [!abstract] Propósito
> La misma arquitectura de [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/08-VistaNegocio]], explicada a quien la va a mantener. Responde: qué patrones hay y dónde vive cada uno, dónde se ensambla el sistema, qué reglas no se deben romper y por qué, y **la guía de dónde tocar** para cada cambio previsible (mínimo 5 filas, cubre las 3 SC del Anexo B). Cierra con la deuda declarada.
>
> Prueba de que sirve: alguien que no participó en el diseño debe poder ubicar, solo con este documento, dónde hacer un cambio.

---

## 1. Qué patrones hay, dónde vive cada uno y cómo se relacionan

| Patrón | Dónde vive | Participantes | Papel |
|--------|------------|---------------|-------|
| **Factory Method** | `Hacienda.Domain/Factories/` (rehecha) + registro consumido desde Application e Infrastructure | `ICreadorRes` + `CreadorTernero/Novillo/Cebon`; `ICreadorVacuna` + `CreadorViva/Bacteriana`; `IRegistroReses`, `IRegistroVacunas`, `IRegistroProductos` | Cada subtipo sabe **crear, rehidratar, describir y validar** su propio tipo. El registro resuelve clave → creador. Alta por UI y rehidratación desde SQLite usan **el mismo mecanismo** |
| **Strategy** | `Hacienda.Domain/Venta/` (nuevo) | `IEstrategiaPrecio` + `MontoManual` (reses: devuelve el monto provisto — comportamiento congelado) + `PrecioUnitario` (derivados: precio × cantidad) | El cálculo del precio se selecciona **en runtime** según el producto; los consumidores no cambian al entrar políticas nuevas |
| **Facade** | `Hacienda.Application/Services/ServicioVentas.cs` (reestructurado) | `IServicioVentas.Vender(IEspecVenta)` + `EspecVentaRes`/`EspecVentaDerivado` | Orquesta el subsistema de venta: **proveer → validar (dominio) → calcular (estrategia) → confirmar (`AlConfirmarVenta`) → persistir**. Controladores aislados de creadores y estrategias |

**Cómo se relacionan entre sí** (la idea central del diseño): `Program.cs` registra **pares (proveedor del producto, estrategia de precio)** en `IRegistroProductos`. La fachada pide el par por tipo y ejecuta su secuencia invariante; no conoce ningún tipo concreto; **no existe ningún `switch`/`if` de tipo en el camino de venta**. El Factory Method alimenta de productos; la Strategy decide el precio; la Fachada coordina. Uno por familia, tres piezas que encajan.

## 2. Dónde se ensambla el sistema

**`Hacienda.Web/Program.cs`** — el único lugar que crece al extender el sistema:

```
Program.cs
├── Registros de creadores (por familia)
│   ├── IRegistroReses     → CreadorTernero, CreadorNovillo, CreadorCebon
│   ├── IRegistroVacunas   → CreadorViva, CreadorBacteriana
│   └── IRegistroProductos → pares:
│         ├── ("Res",     ProveedorResDePotrero + MontoManual)
│         └── ("Derivado", ProveedorDerivado    + PrecioUnitario)
├── Fachada y servicios (Scoped) — igual que hoy
├── Repositorios (delegan rehidratación a los registros)
└── Inicialización y seed (sin cambios)
```

Para responder "¿cómo se arma el sistema?" ya no hay que leerlo todo: se lee `Program.cs` y las 3 fichas de [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/05-TOBE]] §4.

## 3. Reglas que no se deben romper y por qué

| # | Regla | Por qué existe |
|---|-------|----------------|
| R1 | **Prohibido `switch`/`if` sobre tipos** (de res, vacuna o producto) fuera de: (a) el registro en `Program.cs`, (b) el mapeo único string-de-vista→categoría en `VacunaController` (heredado, D-05) | Cada condicional de tipo es un punto de modificación en potencia: reintroduciría el dolor P-02/P-06 (10-12 archivos por tipo nuevo) y movería el punto de decisión que acabamos de eliminar. Fuente: [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/02-PuntosDolor]] |
| R2 | **La fachada orquesta, no legisla**: cero `if` de negocio en `ServicioVentas` | Es el riesgo que el Anexo A tipifica (fachada que absorbe lógica → SRP roto). La regla vive en el dominio: `Res.EvaluarPeso`, `IVendible.ValidarParaVenta`, `AlConfirmarVenta`. Métrica de la celda 6 de [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/06-VerificacionSOLID]] |
| R3 | **Mensajes 1:1**: los textos visibles se copian de su sitio AS-IS, jamás se re-escriben de memoria | Son comportamiento congelado: una diferencia de un carácter = −0.5 en la nota (regla del enunciado). Verificación: 12 casos lado a lado |
| R4 | **Crear ≠ rehidratar**: `Crear` valida invariantes; `Rehidratar` restaura el estado persistido tal cual | Re-validar en lectura rompería los datos históricos (la BD pre-TO-BE tiene filas que las reglas actuales no aceptarían). Compensación declarada de la tensión FM×LSP, caso 12 |
| R5 | **Las estrategias se registran en par** con su proveedor; nunca se cruzan | Sustituibilidad por emparejamiento (tensión Strategy×LSP, celda 13): `PrecioUnitario` jamás recibe una especificación de res ni viceversa |
| R6 | **Los subtipos se registran completos** (subclase + creador + línea de registro) en el mismo commit | Un registro a medias = falla en runtime "tipo no registrado" (riesgo R-05 de [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/07-Riesgos]]) |

## 4. La guía de dónde tocar ⭐

> El artefacto central de esta vista. Para cada cambio previsible: qué hay que **crear**, qué **modificar** y qué **no se debe tocar**.

| Cambio previsible | Crear | Modificar | NO tocar |
|-------------------|-------|-----------|----------|
| **SC-1 · Vender un derivado nuevo** (p. ej. "Cuero curtido") | — (si es solo variedad: fila de configuración del tipo de derivado) | `Program.cs` (1 línea: registrar el par si el derivado tiene política propia), vista nueva autorizada por la SC | **Todo lo demás**: fachada, estrategias, repos, entidades, los 8 casos existentes |
| **SC-1 (variante fuerte) · Derivado con comportamiento propio** (p. ej. perecedero con fecha) | `ProductoPerecedero : IVendible` + su `ProveedorDerivadoPerecedero` + (si cambia el precio) su estrategia | `Program.cs` (registrar el par), tabla de persistencia si tiene datos propios | Fachada (`ServicioVentas` no cambia: secuencia invariante), servicios de reses/vacunas |
| **SC-2 (variante) · Nuevo estado o atributo del chip** | — | `Chip` (máquina de transiciones, ya encapsulada), su repo si hay columna nueva, `DatabaseInitializer` (ALTER), badge de vista | Creadores, registros, fachada, estrategias — el chip no participa del subsistema de venta |
| **SC-3 · Historia clínica: nuevo tipo de evento clínico** | `EventoClinico` (o subtipo), `IRepositorioEventoClinico` + implementación SQLite, tabla nueva | `Res` (colección encapsulada + método de registro), `GestorReses.ListarReses` (carga, mismo patrón que vacunas aplicadas), `Program.cs` (registro del repo), vista de detalle | Fachada de ventas, estrategias de precio, creadores de reses/vacunas. *(Si SC-3 llega con eventos que deban notificar: activar la deuda Observer — ver §5)* |
| **Tipo nuevo de res** (p. ej. Toro) | `Toro : Res` (constantes + rangos) + `CreadorToro : ICreadorRes` | `Program.cs` (1 línea en `IRegistroReses`), seed | `GestorReses`, repos (ya no tienen switches), `FabricaRes` (ya no existe como switch), vistas de badges (comen de `DescripcionTipo()`) — **comparar con AS-IS: 10 archivos** |
| **Tipo nuevo de vacuna** | `SubtipoVacuna : Vacuna` + su `Creador*` | `Program.cs` (1 línea), vista de creación (radio), `DatabaseInitializer` si hay columna | `ServicioVacunacion` (ya colapsado: `CrearVacuna`/`CrearLote` genéricos), `IVacunaFactory` (desapareció), repos — **comparar AS-IS: 12 archivos** |
| **Política de precio nueva** (2×1, precio por bulto) | `PrecioPorBulto : IEstrategiaPrecio` | `Program.cs` (el par del producto que la usa) | `Venta`, fachada, controladores — la sustituibilidad del par lo absorbe |
| **Rol de usuario nuevo** | `PoliticaX : IPoliticaPermisos` | `Program.cs` (registro múltiple existente l.83-85), seed | ⚠️ **Deuda activa**: el autorizador está ensamblado pero nadie lo llama (P-07); activarlo es decisión de negocio y cambio de comportamiento **no autorizado** — pedir autorización primero |

## 5. Deuda declarada (lo que quedó pendiente y por qué)

| Deuda | Dónde | Señal de alerta | Condición de activación |
|-------|-------|-----------------|-------------------------|
| Contrato string de servicios (éxito por `Contains`) | los 8 servicios + 5 controladores | Un cambio de mensaje altera la clasificación success/danger | SC que autorice tocar mensajes |
| Autorización ensamblada pero muerta | `AutorizadorRbca` + 3 políticas | Cualquier SC de permisos | Activarla cambia comportamiento (denegaciones) — exige orden del negocio |
| Observer a medio instalar | `DomainEventPublisherConsola` + strings junto al publish | Un consumidor real de eventos (auditoría, SC-3 con notificaciones) | **Primer patrón a incorporar** si se autoriza tocar los mensajes de eventos |
| Tres agregados sin transacción común | `ServicioChip.InstalarChip` | Fallo intermedio deja BD inconsistente | Corrección puntual aprobada por el equipo |
| Seed fuera del dominio | `DataLoader` (254 INSERTs) | El seed se vuelve carga de mantenimiento | Migración de datos de prueba |
| DTOs muertos + vistas tipadas a entidades | `Dto.cs` + vistas | Cambio de entidad que rompe vistas | Fuera de alcance (frontend congelado, D-05) |
| Save-all + esquema a mano | repos + `DatabaseInitializer` | Concurrencia o volumen real | "BD real" excluida del encargo |
| Rol inicial hardcodeado | `ServicioAutenticacion` | SC de gestión de roles | Fuera del alcance actual |

## 6. Diccionario vista de negocio ↔ vista técnica (uso interno)

| Frase en [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/08-VistaNegocio]] | Realidad técnica |
|-------------------------------|------------------|
| "Un solo lugar para dar de alta lo nuevo" | Factory Method: registros de creadores en `Program.cs` |
| "Las reglas vuelven a vivir junto a lo que describen" | Refactor de dominio P-04/P-08: `Res.AplicarVacuna/Alimentar/EvaluarPeso`, `IVendible.ValidarParaVenta` |
| "Un punto único de lectura del recorrido de venta" | Facade: `ServicioVentas` reestructurado (E-09) |
| "El precio se calcula según el producto" | Strategy: `MontoManual`/`PrecioUnitario` registradas en par |
| "14 lugares → 1 registro" | Conteo P-01 vs tabla E-XX de [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/05-TOBE]] |
| "Doce verificaciones lado a lado" | Los 12 casos de [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/06-VerificacionSOLID]] |
| "Parada de emergencia el 4/9" | Criterio de parada R-03 en [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/07-Riesgos]] §2.2 |

## 7. Orden de implementación (resumen ejecutivo)

Las 5 fases con puntos de compilación y sus casos están en [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/07-Riesgos]] §2: **(0)** capturar las 12 salidas AS-IS → **(1)** dominio encapsulado → **(2)** creadores + registro → **(3)** fachada + MontoManual → **(4)** SC-1 derivados → **(5)** limpieza + verificación completa. Una fase, un commit hito; rollback al hito anterior, nunca al inicio.

> [!tip] Navegación
> Diseño completo: [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/05-TOBE]] · Verificación: [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/06-VerificacionSOLID]] · Riesgos y plan: [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/07-Riesgos]] · Decisiones: [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/04-DecisionesArquitectonicas]] · Bitácora: [[Universidad/ArquitecturaDeSoftware/Reto2-Hacienda/Opcion2/10-BitacoraIA]]
