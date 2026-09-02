---
tags: [reto2, actividad-6, vista, negocio, hacienda]
estado: v2 — para la Líder Técnica y quien aprueba presupuesto (2026-09-02)
---

# 08 — Vista para el Negocio

> [!important] Regla del enunciado
> Esta vista **no puede** contener: nombres de patrones, nombres de clases o archivos, diagramas UML, siglas sin desarrollar (incluida SOLID), ni las palabras refactorizar/desacoplar/inyección de dependencias. Si al quitar las palabras técnicas se queda sin contenido, es que no había contenido.

## 1. Qué le vamos a hacer al sistema (y qué NO)

**Sí vamos a tocar:** cómo se fabrican y se conectan las piezas internas del sistema de ganado — la manera en que nacen los registros de animales, vacunas y ventas, y cómo reaccionan los procesos entre sí.

**No vamos a tocar:** nada de lo que usted ve y usa. Las pantallas, los reportes, los mensajes, los cálculos de dinero y los totales siguen **exactamente igual** — palabra por palabra. Tampoco cambiamos tecnología, base de datos ni proveedor.

## 2. Dónde se está yendo hoy el tiempo y el dinero

Cuando el negocio pide algo nuevo, el costo no está en escribir lo nuevo: está en **todos los lugares que hay que recordar tocar** para que lo nuevo exista:

- **Un nuevo tipo de animal** (p. ej. la vaca lechera que viene para los derivados): hoy exige editar **6 sitios distintos** en 2 capas del sistema. Olvidar uno = datos a medias que se descubren semanas después.
- **Una venta con varios productos** (animal + derivados juntos, lo que pide el nuevo negocio): hoy la venta solo admite un animal. Convertirla en multi-producto reescribe la venta completa de punta a punta.
- **Reaccionar a lo que pasa** (quedarse sin stock de un derivado, una vacuna por vencer): hoy el único aviso posible es texto en pantalla del operador. No existe forma de que el sistema "haga algo" cuando algo ocurre, sin reabrir lo que ya funciona.
- **Reglas que dependen de la memoria de la gente**: la misma regla vive escrita en varios lugares con pequeñas diferencias — hoy ya hay una contradicción documentada (un proceso acepta un monto que otro rechaza).

## 3. Qué gana el negocio

| Ganancia | Medida |
|---|---|
| **Responder rápido el cambio de catálogo** | Un tipo nuevo de animal o producto pasa de 6 sitios editados a **1 solo alta** — días de trabajo se vuelven horas, y sin riesgo de dejar algo a medias |
| **La venta multi-producto existe** | Vender animal + derivados en una sola operación (el caso de negocio pedido) sin reescribir la venta |
| **El sistema reacciona solo** | Cuando algo ocurre (una venta, un stock mínimo, un vencimiento), el proceso correspondiente se dispara sin que nadie edite lo que ya funciona |
| **Una sola versión de cada regla** | Las reglas de negocio viven en un único lugar auditable; se acaba el riesgo de dos procesos respondiendo distinto al mismo caso |
| **Menos riesgo al cambiar** | El 100% de las salidas actuales quedan congeladas y verificadas caso por caso, antes y después, con evidencia |

## 4. Qué cuesta

- **Equipo**: el mismo equipo back, sin compra de licencias, sin nuevos proveedores, sin migración de base de datos destructiva (solo se agregan tablas/columnas nuevas).
- **Tiempo**: implementación en **6 fases verificables**, cada una con su prueba de "nada cambió" antes de avanzar. Si algo falla, se regresa solo esa fase.
- **Lo que no haremos**: no reescribimos el sistema, no cambiamos arquitectura ni lenguaje — eso quedará claro en el contrato de alcance.

## 5. Riesgos (en lenguaje de operación)

| Riesgo | Cómo lo evitamos | Cómo nos damos cuenta |
|---|---|---|
| Un mensaje o cálculo cambia sin querer | Tabla de casos congelados comparados automáticamente antes/después | La comparación arroja diferencias |
| Se pierde una función al mover piezas (ya nos pasó una vez y la atrapamos) | Revisión pieza por pieza con historial de cambios + un caso de prueba por función | Un caso que hoy pasa, falla |
| Los avisos nuevos salgan en desorden | Orden de aviso fijo y probado (primero pantalla, luego procesos) | El orden varía entre corridas |
| El calendario apriete la entrega | Fases independientes: cada fase entrega valor completo; el calendario se revisa a diario | Actividad sin avance al cierre del día |

## 6. Qué necesitamos del negocio

1. **Confirmar el catálogo de derivados** de la primera entrega (leche, carne, cuero) — define el alta inicial.
2. **Decidir el stock mínimo** que dispara el aviso de reposición de derivados.
3. **Una persona de operación** 30 minutos para validar que los mensajes le siguen pareciendo correctos (prueba de que la vista sirve).

## 7. Qué pasa si no se hace

- Cada nuevo tipo de animal/producto seguirá costando 6 ediciones coordinadas — el riesgo de "quedó a medias" se repite con cada solicitud.
- La venta seguirá limitada a un solo animal: **el negocio de derivados no se puede facturar en una sola venta**.
- Cada reacción nueva (avisos, alertas) seguirá exigiendo reabrir procesos que ya funcionan, con el riesgo de romper lo andando.

---

*Prueba de que esta vista sirve: se presenta a una persona sin formación técnica; lo que entienda queda anotado como evidencia en el video (requisito del enunciado).*

## Navegación

- [[Reto2-Hacienda/Opcion1/09-VistaTecnica|09-Vista Técnica]] — el mismo diseño para el equipo de desarrollo.
