---
tags: [reto2, actividad-4, verificacion, solid, comportamiento, hacienda]
estado: v2 — alineada al TO-BE v2 y al catálogo P-01..P-15 (2026-09-02)
---

# 06 — Verificación: SOLID sigue en pie y el comportamiento no cambió (Actividad 4)

> [!abstract] Propósito
> Demostrar, no afirmar: (4.1) **matriz patrón×principio** con evidencia por celda no-neutra; (4.2) **casos de comportamiento** del Reto 1 + nuevos que recorren lo que los patrones tocan, con salidas **antes/después lado a lado**. La base ya fue auditada (B-15); aquí se audita que **los patrones no la rompan**.

> [!warning] Regla de la Líder Técnica
> "SOLID no se toca. Un patrón mal aplicado deshace SOLID con una elegancia que asusta." Toda celda **Tensionado** trae su compensación; ninguna celda queda **Roto**.

---

## 4.1 · Entregable 1 — Matriz de verificación patrón × principio

| Patrón adoptado | SRP | OCP | LSP | ISP | DIP |
|---|---|---|---|---|---|
| **Factory Method** (+ `RegistroDe*`) | **Refuerza** ① | **Refuerza** ② | Neutro | Neutro | **Refuerza** ③ |
| **Template Method** | **Refuerza** ④ | **Tensionado → compensado** ⑤ | **Tensionado → compensado** ⑥ | Neutro | Neutro |
| **Builder** | **Refuerza** ⑦ | **Refuerza** ⑧ | **Refuerza** ⑨ | **Refuerza** ⑩ | **Refuerza** ⑪ |
| **Observer** | **Refuerza** ⑫ | **Refuerza** ⑬ | Neutro | **Refuerza** ⑭ | **Refuerza** ⑮ |

### Evidencias (una línea por celda no-neutra)

① La creación sale de los servicios: `GestorReses` orquesta, no decide tipo (hoy decide en `FabricaRes.cs:17-21` + `GestorReses.cs:129-134`).
② `FabricaVacaLechera` = **1 clase + 1 registro, 0 ediciones** (E-13) — el experimento OCP medible: hoy 6 archivos/2 capas (P-01).
③ Servicios y repos dependen de `RegistroDeReses`/interfaces — nadie conoce creators concretos (idioma ya probado en `AutorizadorRbca.cs:13-16`).
④ El pipeline (validar→construir→exigir regla→publicar) vive **una vez** en la base: mata la triplicación de P-04 y la duplicación de P-09/P-10.
⑤ *Tensión:* un paso nuevo del esqueleto edita la base (todas las subclases lo heredan). *Compensación:* los pasos son estables por naturaleza (comunes del dominio); lo que varia por subtipo son **datos** (rangos, parámetros), que entran por propiedades sin tocar la base. Declarado como costo en la Ficha 2.
⑥ *Tensión:* una subclase podría "no poder" cumplir un paso (error típico del enunciado). *Compensación de diseño:* **hooks como propiedades-dato** (`RangoEdad`, `MaxVacunas*`) — ningún subtipo puede "no poder"; el esqueleto no tiene pasos skipping-ables. Caso C-17 lo prueba con `VacaLechera`.
⑦ Construcción separada de representación: `VentaBuilder` ensambla, `Venta` protege invariantes en `Build()` — una sola vez (hoy: `FabricaVenta.cs:20` + `ValidadorVenta.cs:15` con umbrales contradictorios).
⑧ Nuevo `IVendible` (SC-3: servicios clínicos) entra sin tocar builder ni `Venta`.
⑨ `Res` y `ProductoDerivado` son intercambiables como ítems — el contrato `IVendible` es explícito y mínimo.
⑩ `IVendible` = **una** operación; no arrastra superficie de `Res` (el error "interfaz que obliga a implementar de más" queda estructuralmente imposible).
⑪ Builder recibe reloj/guid **por ctor** como todo el sistema — mata la excepción de `IVentaFactory.cs:8` (P-13).
⑫ El publicador no sabe quién reacciona: `GestorReses` publica, los handlers deciden (P-14 muere).
⑬ Nueva reacción (SC-1: stock bajo) = 1 handler + 1 registro — cero ediciones a publicadores (hoy: editar `GestorReses` en N flujos).
⑭ `IDomainEventHandler<T>` segregado por evento — un handler de stock no ve eventos de vacunación.
⑮ Los publicadores actuales **no cambian**: dependen de `IDomainEventPublisher` existente (el DIP del Reto 1 rinde: E-07).

### Errores típicos del enunciado — cómo este set los evita

| Error típico (enunciado) | Principio | Cómo se evita en el TO-BE | Prueba |
|---|---|---|---|
| Fábrica que crece con un condicional por tipo | OCP | Creators concretos sin switches; la decisión vive en `RegistroDe*` por DI | Grep post-implementación: `case TipoRes` = 0 en fábricas |
| Fachada que absorbe lógica de negocio | SRP | Los servicios **adelgazan** (delegan en entidades/registros); contador de líneas por servicio baja | C-14 |
| Punto de acceso global único | DIP/test | No hay singleton: unicidad por composition root | Grep `static.*Instance` = 0 |
| Método plantilla con pasos que una subclase no puede cumplir | LSP | Hooks como propiedades-dato; sin pasos opcionales | C-17 (VacaLechera pasa el esqueleto sin métodos vacíos) |
| Envoltura que cambia el contrato de lo que envuelve | LSP | `HandlerConsola` reproduce salida **byte a byte**; `Venta` mantiene superficie pública | C-01…C-16 |

---

## 4.2 · Entregable 2 — Casos de comportamiento (antes/después)

> [!info] Protocolo de captura (idéntico para todos)
> 1. **Antes**: correr el caso sobre el commit base (seed determinista del `DataLoader`, `TimeProvider` falso fijado a `2026-01-15T10:00Z`), capturar consola + `ResultadoOperacion.Mensaje`.
> 2. **Después**: mismo caso, mismo seed, mismo reloj, sobre el TO-BE.
> 3. **Diff** de las dos capturas: debe ser **vacío** (o exactamente el declarado).

### Casos del Reto 1 (comportamiento congelado)

| ID | Caso | Salida congelada (esencia) | Qué patrón lo toca |
|----|------|---------------------------|---------------------|
| C-01 | Agregar res válida (Ternero 8 meses, 200 kg) | `La res 'X' fue añadida al potrero 'P'.` + eventos peso/mitad/lleno según corresponda | FM/TM (E-01) |
| **C-02** | **Agregar res fuera de rango (Ternero 60 meses)** | `La edad 60 no es válida para Ternero. Rango: 0-12 meses` — **la validación restaurada (DEC-09)** | TM paso sellado |
| C-03 | Res duplicada en potrero | `Ya existe una res 'X' en el potrero 'P'` (desde `Potrero.AgregarRes`) | — (base sana) |
| C-04 | Aplicar vacuna (esquema incompleto) | `Vacuna 'V' aplicada a 'X' correctamente. Datos válidos. Guardado exitoso en BD. La res 'X' aún no ha completado… Bacterianas: n, Vivas: m` | FM vacunas (E-03) |
| C-05 | Vacuna con lote ya aplicado a la res | `La vacuna lote 'L' ya fue aplicada a 'X'` (desde `Res.AplicarVacuna`) | — (base sana) |
| C-06 | Exceder máximos por categoría | `No se pueden aplicar más bacterianas a 'X' (máximo N)` / `…vivas…` | — (base sana; consumirá `ParametrosRes` ⚪) |
| C-07 | Crear lote bacteriano (25 uds) | `Lote de vacunas bacterianas creado: 25 de 25. Lotes: B-001 a B-025. Período: 3 semanas.` + tope `cantidad debe estar entre 1 y 100` | TM `CrearLote` (E-09) |
| C-08 | Vender res (monto válido) | `Venta de la res 'X' realizada con éxito por $ 1.500.000,00.` | Builder (E-08) |
| C-09 | Vender en potrero/res inexistente | `Potrero 'P' no encontrado` / `Res 'X' no encontrada` | Builder |
| C-10 | Instalar chip (éxito / duplicado / res inexistente) | `Chip N instalado correctamente en la res X` · `Ya existe un chip con el número de serie N` · `La res ya tiene un chip instalado (N)` | — (base sana) |
| C-11 | Cambiar estado chip (Perdido→Activo válido; Perdido→Perdido inválido) | `Estado del chip N cambiado a Activo` · `Transición de estado no permitida: de Perdido a Perdido. Contacte al administrador…` (desde `TransicionesChip` ⚪) | — (base sana) |
| C-12 | Alimentar res (desnutrida → apta) | `La res 'X' fue alimentada, ahora pesa N kg.` + `[Evento] …` — las **dos copias** de la reacción producen las mismas líneas | Observer (E-05/P-14) |

### Casos nuevos (recorren lo que los patrones tocan — ≥4 exigidos)

| ID | Caso | Qué demuestra | Aceptación |
|----|------|---------------|------------|
| C-13 | **Identidad estable**: leer la misma venta dos veces; comparar `Guid` de la `Res` | P-05/E-06: rehidratación preserva `Id` (antes: GUID nuevo por lectura) | Los dos `Guid` **iguales**; mensajes de usuario sin cambio |
| C-14 | **SC-1 extremo a extremo**: crear `VacaLechera` + `Lacteo`; vender (1 res + 2 lácteos) | OCP medible (1+1), Builder multi-ítem, total = suma de ítems | Venta persistida con 3 ítems; total correcto; mensaje de venta idéntico en formato |
| C-15 | **Monto $0 en venta** | P-04: la contradicción (`<0` vs `<=0`) queda consolidada en UNA regla — la congelada del constructor (`Monto no puede ser negativo` vía `Dinero`) | UNA sola respuesta posible, la misma que da hoy el `Dinero` del ctor |
| C-16 | **Orden del despachador**: venta que dispara consola + stock | E-07: determinismo Observer (consola 1º, luego stock en orden de registro) | Líneas de consola idénticas a hoy; línea de stock DESPUÉS; orden reproducible |
| C-17 | **VacaLechera pasa el esqueleto** sin ningún método vacío | ⑥: compensación LSP de Template Method | `Crear(VacaLechera…)` valida comunes+edad+rango propio en los mismos mensajes |
| C-18 | **Round-trip de serialización**: persistir y releer 1 res de cada subtipo | E-10: `Serializar` consolidado en la base produce el MISMO formato pipe | Bytes idénticos al formato actual (`Id|Nombre|Peso|Edad|Tipo`) |

> [!note] Caso C-02 — la joya de la sustentación
> La validación de edad fue **regresión detectada por el equipo, restaurada con mensaje exacto y luego consolidada como paso sellado** (DEC-09 → Ficha 2). C-02 demuestra el ciclo completo: cuestionar → verificar → corregir → blindar con patrón.

---

## 5. Verificación de la base (ya ejecutada — contexto)

La auditoría B-15 dejó la base medida: encapsulamiento real (setters privados, `IReadOnly*`), reglas con fuente única (`Reglas/`), `IChip` eliminada, duplicaciones de parámetros y rangos corregidas. Esta actividad **no re-audita la base**: audita que los 4 patrones la preserven (matriz §4.1).

## 6. Riesgos de verificación

1. **Capturas antes/después**: exigen seed determinista y reloj fijo — el protocolo §4.2 los fija; sin ellos el diff es ruido.
2. **C-15 cambia una respuesta posible**: hoy `monto=0` pasa `FabricaVenta` pero NO el validador → el usuario ve el error del validador; el TO-BE responde lo que hoy responde la cadena completa (se congela la respuesta observable, se elimina la ambigüedad interna). Declarado.
3. **Consola en tests**: capturar `Console.Out` con `StringWriter` para comparar bytes.

## 7. Navegación

- [[Reto2-Hacienda/Opcion1/05-TOBE|05-TOBE]] — lo que esta matriz audita.
- [[Reto2-Hacienda/Opcion1/07-Riesgos|07-Riesgos]] — Actividad 5: riesgos de implementar el cambio.
- [[Reto2-Hacienda/Opcion1/10-BitacoraIA|10-Bitácora]] — B-12 (auditoría adversarial) y B-15/B-16.
