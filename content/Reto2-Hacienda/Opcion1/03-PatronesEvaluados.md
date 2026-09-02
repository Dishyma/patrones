---
tags: [reto2, actividad-2, patrones, evaluacion, hacienda]
estado: catálogo completo anclado a P-01..P-15 (auditoría 2026-09-02)
---

# 03 — Patrones Evaluados (Actividad 2 · Entregable 2.1)

> [!abstract] Propósito
> Evaluación de **los 22 patrones del Anexo A** (mín. 6, ≥2 por familia) contra el catálogo completo de dolores ([[Reto2-Hacienda/Opcion1/02-PuntosDolor|02-PuntosDolor]] P-01…P-15). Regla PILAS: patrón adoptado ⇔ dolor anclado. **Set propuesto: 4 adoptados + 1 candidato a validar por el equipo** (el enunciado permite 3–5; el equipo decide el 5º).

## 1. Tabla de decisión (22 · 5 creacionales · 7 estructurales · 10 comportamiento)

| # | Patrón | Familia | P-XX | Qué gana / qué cuesta | Decisión | Por qué |
|---|--------|---------|------|------------------------|----------|---------|
| 1 | **Factory Method** | Creacional | **P-01** ★, **P-13** ★, P-02, P-05/P-12 (colateral) | + Subtipo = 1 creador + 1 registro DI, cero diccionarios/switches; esqueleto común para las fábricas; rehidratación con Id estable de rebote. − 1 clase por subtipo, +1 indirección | **ADOPTADO** | Baja SC-1 de 6 ediciones/2 capas a 1 clase + 1 registro; unifica los 4 idiomas de `Fabrica*` |
| 2 | **Template Method** | Comportamiento | **P-04** ★, **P-09** ★, **P-10** ★, P-13/P-14 ○ | + El pipeline `validar comunes → construir → regla del subtipo → publicar` se escribe una vez (y `EsEdadValida` vuelve a aplicarse); serialización y lotes dejan de copiarse. − Herencia para variar, +1 frame | **ADOPTADO** | **Cuatro dolores** comparten la misma causa (pipeline copiado) + 2 colaterales; el más anclado de todo el set. La validación de edad restaurada (ex-P-11, ver 02 §2bis) vivirá aquí como paso sellado |
| 3 | **Builder** | Creacional | **P-03** ★, P-04 ○ | + Venta multi-ítem (SC-1) sin constructores telescópicos; `Build()` valida invariantes una vez. − Estado intermedio, 1 clase más en la traza | **ADOPTADO** | SC-1 rompe el constructor de `Venta` el día uno; el reloj entra por ctor (mata la rareza de `IVentaFactory.cs:8`) |
| 4 | **Observer** | Comportamiento | **P-06** ★, **P-14** ★ | + Reaccionar a eventos (stock, vencimientos, consola) sin tocar al publicador; las reacciones dejan de copiarse entre flujos; `VacunaVencidaEvent` por fin tiene público. − Orden determinista que especificar; handlers invisibles en la firma | **ADOPTADO** | SC-1 necesita consumir `VentaRealizada`/stock; hoy la única reacción posible es `Console.WriteLine` |
| 5 | **Composite** | Estructural | **P-03** ○ | + `Venta` compone `IVendible` (res \| producto) y delega el precio — polimorfismo natural del multi-ítem. − Solo vale la pena si existen ítems **compuestos** (canastas); hoy la lista es plana | **🎯 CANDIDATO A VALIDAR (el 5º)** | Si el equipo ve canastas/combos en SC-1 → adoptar (gana el total recursivo gratis); si la venta es lista plana → descartar (Builder basta). **Decisión del equipo en 04-Decisiones D-12** |
| 6 | Abstract Factory | Creacional | P-01 | Familias completas por contexto | **Descartado** | No hay familias (res y vacuna no se crean juntas); además reubica el switch sin eliminarlo (advertencia literal del Anexo A) |
| 7 | Prototype | Creacional | — | Clonar prototipos | **Descartado** | Todo se crea con datos nuevos; clonar comparte estado mutable (riesgo) sin dolor que curar |
| 8 | Singleton | Creacional | — | Instancia global única | **Descartado** | El composition root ya da unicidad vía DI; punto de acceso global = error típico listado por el enunciado (DIP/testabilidad) |
| 9 | Adapter | Estructural | — | Adaptar interfaz externa | **Descartado** | No hay puerto externo nuevo: SQLite ya vive detrás de `IRepositorio*` (el Adapter ya ocurrió en el Reto 1) |
| 10 | Bridge | Estructural | — | Separar abstracción/impl | **Descartado** | La dupla interfaz-repositorio ya es esa separación; un Bridge encima duplica la inversión gratis |
| 11 | Decorator | Estructural | — | Envolver responsabilidades | **Descartado** | No hay responsabilidades que se compongan en runtime; envolturas = costo sin solicitud |
| 12 | Facade | Estructural | — | Puerta única al subsistema | **Descartado** | Los servicios YA son la fachada; fachada-de-fachadas absorbe lógica (error SRP típico del enunciado) |
| 13 | Flyweight | Estructural | — | Compartir inmutables masivos | **Descartado** | Escala del dominio (~150 reses/potrero): sin presión de memoria |
| 14 | Proxy | Estructural | — | Acceso diferido/controlado | **Descartado** | RBAC congelado (P-07) y sin lazy-loading pedido; agregar proxy = solución buscando problema |
| 15 | Chain of Responsibility | Comportamiento | P-04/P-11 ○ | Validadores encadenables fail-fast | **Descartado** | El orden es fijo y hay un solo consumidor: no hay cadena real que reordenar; Template Method expresa el pipeline con menos piezas |
| 16 | Command | Comportamiento | — | Encapsular operaciones (undo/colas) | **Descartado** | Sin undo/redo ni colas en alcance; los servicios ya encapsulan cada caso de uso |
| 17 | Interpreter | Comportamiento | — | Gramática de reglas | **Descartado** | 2-3 umbrales simples; un intérprete es EL ejemplo de sobre-ingeniería |
| 18 | Iterator | Comportamiento | — | Recorrido uniforme | **Descartado** | `IEnumerable`/LINQ ya lo dan; no cura ningún P |
| 19 | Mediator | Comportamiento | P-06 ○ | Coordinar colegas | **Descartado** | Centralizaría lo que hoy está bien repartido; el despachador Observer es más simple y sin god-object |
| 20 | Memento | Comportamiento | — | Snapshots/rollback | **Descartado** | Sin requisito de deshacer; SQLite ya es el historial |
| 21 | State | Comportamiento | — | Estados como clases | **Descartado** | Las transiciones de chip ya son tabla declarativa (`TransicionesChip`) — remedio ya presente en la base |
| 22 | Strategy | Comportamiento | P-02/P-10 ○ | Algoritmo intercambiable en runtime | **Descartado** | La categoría la elige el usuario una vez en el formulario, no se intercambia en ejecución; con Factory Method + `DatosVacuna` la selección deja de estar regada sin Strategy |

## 2. Matriz dolor → candidatos (resumen de "de dónde coger")

| Dolor | ★ Fuerte | ○ Posible | ✗ Descartado |
|---|---|---|---|
| P-01 subtipos regados | Factory Method | — | Abstract Factory, Prototype |
| P-02 vacuna partida | Factory Method | Strategy | Command |
| P-03 venta mono-ítem | Builder | **Composite (D-12)** | Prototype |
| P-04 validación contradictoria | Template Method | Chain of Resp. | Strategy |
| P-05 GUID en rehidratación | *(colateral FM)* | — | — |
| P-06 publicación monolítica | Observer | — | Mediator |
| P-09 Serializar ×5 | Template Method | Visitor | Decorator |
| P-10 lotes gemelos | Template Method | Strategy | Command |
| ~~P-11~~ *(regresión corregida — 02 §2bis)* | Template Method (paso sellado) | Chain of Resp. | — |
| P-12 enums paralelos | *(colateral FM)* | — | — |
| P-13 4 fábricas/4 idiomas | Factory Method + Template Method | Builder | — |
| P-14 reacciones duplicadas | Observer | Template Method | — |

## 3. Set propuesto y su defensa en una línea

1. **Factory Method** — P-01/P-13: "1 clase + 1 registro" medible (hoy 6 archivos/2 capas).
2. **Template Method** — P-04/P-09/P-10 + P-13/P-14 colaterales: una causa (pipeline copiado), y consolida la validación de edad restaurada. El más defendible del set.
3. **Builder** — P-03: SC-1 necesita multi-ítem; mata además la firma rara del reloj (`IVentaFactory.cs:8`).
4. **Observer** — P-06/P-14: la reacción deja de copiarse; `VacunaVencidaEvent` (0 usos hoy) demuestra el hueco.
5. **Composite** *(candidato — D-12)* — solo si SC-1 incluye canastas de derivados; si no, 4 patrones (dentro del rango 3–5).

## 4. Advertencias del Anexo A — cómo las respeta el set

1. *Abstract Factory sin familias* → descartado con la advertencia citada (#6).
2. *Singleton/global* → descartado (#8); unicidad por composition root.
3. *Template Method con pasos que alguna subclase no puede cumplir (LSP)* → mitigación de diseño: hooks como **propiedades-dato** (rangos, parámetros), nunca pasos opcionales; ningún subtipo puede "no poder" (ver [[Reto2-Hacienda/Opcion1/06-VerificacionSOLID|06-Verificación]]).

## 5. Navegación

- [[Reto2-Hacienda/Opcion1/02-PuntosDolor|02-PuntosDolor]] — el catálogo que ancla cada decisión.
- [[Reto2-Hacienda/Opcion1/04-DecisionesArquitectonicas|04-Decisiones]] — D-12 (¿Composite 5º?) y las decisiones vivas del equipo.
- [[Reto2-Hacienda/Opcion1/10-BitacoraIA|10-Bitácora]] — B-07/B-08/B-15/B-16.
