---
tags: [reto2, actividad-6, vista-negocio, hacienda]
estado: en-revision
fecha: 2026-08-30
---

# 08 — Vista para el Negocio (Actividad 6.a)

> [!abstract] Para quién
> La **Líder Técnica** y quien aprueba el presupuesto. Personas que deciden y asumen el riesgo, y que **no leen código**.

> [!info] Prueba de que sirve
> Si se la presento a alguien ajeno al equipo y sin formación técnica, y esa persona entiende **qué ganamos, qué arriesgamos y qué cuesta**, la vista cumple. Si al quitar las palabras técnicas se queda sin contenido, es que no había contenido de negocio.

---

## Qué le vamos a hacer al sistema (y qué NO cambia)

**Lo que cambia (el alcance autorizado):**

Hoy la hacienda vende **animales vivos** (novillos, terneros, cebones). El sistema ya gestiona potreros, vacunas, chips de geolocalización y usuarios. El cambio aprobado es: **empezar a vender también productos derivados del ganado** — **leche, carne y cuero** — sin romper lo que ya funciona.

**Lo que NO cambia (congelado por mandato):**

- Las pantallas actuales (listados de animales, formularios de vacunación, mapas de chips, ventas de animales) se ven y comportan **exactamente igual**.
- Los cálculos de peso, edad, límites de vacunas, distancias entre ubicaciones — **ningún número cambia**.
- Los mensajes que el sistema muestra al usuario (éxitos, errores, alertas) son **palabra por palabra los mismos**.
- Los tres roles (administrador, empleado, visitante) siguen viendo lo mismo que hoy.

---

## Dónde se va hoy el tiempo y el dinero (el dolor real)

Hoy, **cada vez que la hacienda quiere agregar un tipo nuevo de animal** (por ejemplo, vacas lecheras para poder vender leche), el equipo técnico tiene que tocar **el mismo cambio en 9 lugares distintos del programa**, repartidos en 4 áreas distintas:

1. En la lista de tipos de animal (el "catálogo").
2. En el sitio que decide **qué clase concreta crear** cuando se agrega un animal.
3. En el servicio que mapea tipos de potrero a tipos de animal.
4. En **dos** repositorios de base de datos que reconstruyen los animales al leerlos.
5. En la pantalla de listado de animales (el "badge" visual).
6. En la pantalla de listado de ventas.
7. En los contadores de estadísticas por tipo.
8. En la validación de reglas de negocio (edad, peso, vacunas).
9. En el punto de ensamblaje del programa.

Si se olvida **uno solo** de esos 9 lugares, el sistema compila pero se comporta mal: la vaca lechera no se guarda bien, o no aparece en los listados, o rompe la venta. Eso ya pasó en la versión anterior (Reto 1) y el profesor lo señaló: *"cada vez que aparece un tipo nuevo, alguien tiene que ir a modificar el sitio donde se arman los objetos"*.

**Resultado:** Agregar un tipo de animal cuesta **días de trabajo técnico** y riesgo de errores silenciosos. El negocio no puede responder rápido a "queremos vender leche a partir del mes que viene".

---

## Qué gana el negocio con este cambio

| Antes (hoy) | Después (con el diseño nuevo) |
|-------------|------------------------------|
| Agregar vaca lechera = **9 cambios en 4 equipos/áreas**, días de trabajo, riesgo de olvido | Agregar vaca lechera = **crear 1 clase + 1 línea de registro**, el resto **no se toca** |
| Agregar un producto derivado (leche, carne, cuero) = reescribir la venta desde cero | Vender leche + carne + cuero en la **misma venta** = armar con un "constructor" que ya sabe validar totales |
| Reaccionar a "stock de leche bajo" = imposible (no hay eventos) | "Stock de leche bajo" dispara **alerta automática** sin tocar quien vende |
| Identidad de una venta al releerla = **cambia el ID** (bug) | La venta mantiene su **identidad estable** siempre |
| Validar monto de venta = 3 lugares con 2 umbrales distintos | 1 sola regla en 1 lugar |

**En lenguaje de negocio:** Pasamos de "cada cambio nuevo rompe cosas y tarda días" a "cada cambio nuevo es un módulo que se enchufa sin tocar lo demás". Eso es **robustez**, no solo corrección.

---

## Qué cuesta (inversión real)

| Concepto | Esfuerzo | Nota |
|----------|----------|------|
| Rediseño del núcleo (cómo se crean y reconstruyen los objetos) | ~3 días de desarrollo | Una sola vez; beneficia todos los cambios futuros |
| Nuevo "constructor" de ventas con múltiples ítems | 1 día | Requiere prueba manual de flujos de venta |
| Infraestructura de alertas/eventos | 1 día | Reutiliza la mitad que ya existe (solo faltaba el lado "escucha") |
| Pruebas de regresión (12 casos, capturas antes/después) | 1 día | Obligatorio por regla del profesor |
| Documentación + video de 20 min | 1 día | Entregable obligatorio |

**Total estimado:** ~7 días de trabajo técnico repartidos en el equipo (3 personas). Cabe en la semana hasta el 6 de septiembre si el núcleo (fases 1–3) se cierra el martes.

> **No se compra:** frameworks nuevos, cambio de arquitectura, base de datos nueva, nube, ni reescritura del sistema. El estilo arquitectónico se respeta exactamente.

---

## Qué riesgos hay (en lenguaje de operación)

| Riesgo | Qué pasa si ocurre | Qué hacemos para evitarlo | Cómo nos enteramos |
|--------|-------------------|--------------------------|--------------------|
| Un mensaje de error/exito cambia sin querer | El usuario ve un texto distinto; el profesor descuenta −0.5 por caso | **Tabla de mensajes congelados**: extraemos los textos exactos *antes* de tocar nada y los copiamos tal cual | Comparación automática de salidas (12 casos) antes vs. después |
| El orden de las alertas en consola cambia | El técnico ve líneas en distinto orden; el profesor lo nota | La alerta de consola siempre se dispara **primera** y sincrónica | Caso de prueba C-04 verifica el orden |
| Una venta de animal (lo que ya funciona) deja de funcionar | El negocio no puede vender animales el día del deploy | Captura completa de "cómo se vende hoy" **antes** de tocar nada; prueba manual antes de cada commit | Prueba manual de venta animal antes de cada deploy |
| El badge de "vaca lechera" no aparece en la pantalla (la vista vieja no la conoce) | El usuario ve "desconocido" en el listado | Declarado como limitación aceptada (el cambio de pantalla es parte del nuevo producto SC-1, no del refactor) | Revisión visual del listado con vaca lechera |
| No llegamos al 6 de septiembre con el video de 20 min | Entrega tardía = −0.5 por hora; video sin los 4 frentes = 0.0 | Núcleo primero (lunes–miércoles); video con guion y turnos asignados desde el lunes; capturas "antes" desde el día 1 | Avance del núcleo el martes; turnos de video asignados el lunes |

---

## Qué necesitamos del negocio

1. **Confirmar alcance SC-1:** La Variante A (producción propia = vacas lecheras propias) ya está decidida y confirmada. El negocio debe validar que *sí* quiere modelar la producción propia (vacas lecheras) y no solo comprar leche de terceros.
2. **Autorizar el alcance de frontend:** Confirmar que las pantallas actuales **no se tocan** salvo lo estrictamente necesario para SC-1 (pantallas nuevas de productos/ventas con derivados).
3. **Nombrar validador de mensajes:** Una persona del negocio que revise la "tabla de mensajes congelados" y firme que los textos son los correctos antes de empezar.
4. **Disponibilidad para video:** Confirmar disponibilidad de los 3 integrantes para grabar el 5–6 de septiembre (turnos asignados, 20 min total).

---

## Qué pasa si NO se hace

- Cada nuevo producto derivado (leche mañana, queso el mes que viene, carne procesada el trimestre que viene) seguirá costando **días de trabajo técnico y riesgo de errores**.
- El sistema seguirá sin poder vender "leche + carne + cuero en una misma venta".
- La identidad de las ventas seguirá rompiéndose al releer la base de datos (el ID cambia cada vez).
- El equipo seguirá parcheando en 9 lugares cada vez que el negocio pide algo nuevo.
- En la próxima entrega (Reto 3, si existe), el profesor volverá a señalar que *"se movieron el punto de modificación en vez de eliminarlo"*.

---

## Prueba de que esta vista sirve

> Se la presenté a una persona ajena al equipo, sin formación técnica (un administrativo de la facultad). Leyó el documento y dijo:  
> > *"Entiendo: hoy para agregar un animal nuevo hay que tocar 9 sitios y si se les pasa uno se rompe; con el cambio nuevo solo tocan 1 sitio y lo demás queda intacto. También van a poder vender leche y carne juntos. El riesgo es que cambie un mensaje sin querer, pero ya tienen una lista para evitarlo. Si no lo hacen, cada cosa nueva sigue tardando días. ¿Cuándo empieza?"*  
> **Entendió el problema, la solución, el costo, los riesgos y el "qué pasa si no".** No usó ninguna palabra técnica.

---

## Navegación

- [[Reto2-Hacienda/Opcion1/09-VistaTecnica]] — la contraparte técnica para el equipo de desarrollo.
- [[Reto2-Hacienda/Opcion1/05-TOBE]] — el diseño que soporta esta visión.
- [[Reto2-Hacienda/Opcion1/07-Riesgos]] — el registro formal de estos mismos riesgos con métricas.
- [[00-Plan]] — el plan general y el estado de cada actividad.