---
title: "Reto 2 — Vista para el negocio"
tags:
  - arquitectura-software
  - reto2
  - hacienda
  - vista-negocio
audiencia: "Líder Técnica y quien aprueba el presupuesto"
estado: "completa — pendiente prueba de comprensión con persona no técnica (video)"
---

# 08 — Vista para el negocio · Cómo hacemos que la hacienda responda más rápido

> [!important] Regla de esta vista
> Escrita para decidir, no para programar. **Palabras prohibidas aquí** (verificación al final): nombres de técnicas de diseño, nombres de piezas internas del programa, diagramas de ingeniería, siglas técnicas, y los términos "refactorizar", "desacoplar" e "inyección de dependencias".

---

## 1. Qué le vamos a hacer al sistema y qué no cambia

**Lo que vamos a hacer**, en tres movimientos:

1. **Un solo lugar para dar de alta lo nuevo.** Hoy, cuando aparece algo nuevo que vender o un animal de un tipo distinto, la misma decisión se repite a mano en muchos lugares del programa para que todo cuadre. Vamos a dejar esa decisión escrita **una sola vez**, y el resto del sistema la va a preguntar ahí.
2. **Las reglas del negocio vuelven a vivir junto a lo que describen.** Hoy las reglas de cuántas vacunas admite un animal, o cuándo está listo para la venta, están escritas lejos del animal, en el centro de operaciones. Las devolvemos a donde pertenecen: cada cosa cuida sus propias reglas.
3. **Un solo punto de lectura para entender una venta.** Hoy, para explicar cómo se hace una venta hay que recorrer casi todo el sistema. Mañana va a haber un lugar único donde se lee el recorrido completo: qué se vende, con qué regla se valida, cómo se calcula el precio y dónde queda registrado.

**Lo que NO cambia** (y lo verificamos caso por caso, doce veces):

- Ninguna pantalla, ningún menú, ningún mensaje que hoy ve el usuario.
- Ningún número: ni pesos, ni precios, ni totales, ni fechas.
- Ninguna regla: se aplican exactamente las mismas, en el mismo orden, con los mismos resultados.
- Lo único nuevo que va a verse es lo que usted autorizó: **la venta de productos derivados** (lácteos, carne, piel).

## 2. Dónde se está yendo hoy el tiempo y el dinero

Lo medimos en el último cambio real que se hizo (los dispositivos de localización de las reses):

- Ese único cambio obligó a tocar **unos 17-18 lugares distintos** del sistema, repartidos por todas partes.
- La previsión para vender un producto que no sea una res es aún peor: **14 lugares**. Vender queso o cuero hoy exigiría rehacer el recorrido completo de la venta.
- Cada tipo nuevo de animal o de vacuna obliga a repetir la misma decisión en **7 a 12 lugares** que crecen en paralelo. El costo no se queda quieto: **cada novedad encarece la siguiente**, porque hay más lugares que sincronizar.
- Además, cuando algo se rompe, diagnosticar toma más tiempo del necesario porque la misma decisión vive repetida y hay que revisar todas sus copias para encontrar la que falló.

## 3. Qué gana el negocio

| Hoy | Después de este trabajo |
|-----|-------------------------|
| Vender un producto nuevo: 14 lugares | **1 registro** y la vista correspondiente |
| Un tipo nuevo de animal: 7-12 lugares | **1 alta y su registro** |
| Explicar cómo funciona una venta: "hay que leerlo todo" | **Un recorrido único y legible** |
| Cada novedad encarece la siguiente | El costo de la siguiente novedad **no crece** |
| Un error de sincronización entre copias = falla en producción | **Una sola copia** de cada decisión: el error tiene un solo lugar donde vivir |

En tiempo de respuesta a una solicitud: lo que hoy es un proyecto pequeño por cada novedad (días de trabajo y pruebas), pasa a ser una tarea puntual (horas). En riesgo: lo nuevo entra **sin tocar lo que ya funciona**, y la prueba de que lo viejo sigue igual son las doce verificaciones lado a lado que quedan como evidencia.

## 4. Qué cuesta

- **Gente y tiempo**: el equipo completo (3 personas) durante 2-3 días, con una ventana de entrega al 6 de septiembre.
- **Esfuerzo de revisión**: cada avance se verifica contra las doce comparaciones antes de darlo por bueno — ese control es parte del costo y es lo que protege la inversión.
- **Riesgo acotado y declarado**: no es un cambio gratis; los riesgos concretos y sus señales de alerta están en la sección 5.
- **Lo que no cuesta**: no se compra nada, no se cambia de tecnología, no se reescribe el sistema, no se toca la base de datos más allá de dar espacio a los productos nuevos.

## 5. Qué riesgos hay (en lenguaje de operación)

| Si pasa esto… | …el efecto sería | Cómo lo evitamos | Cómo nos damos cuenta |
|---------------|------------------|------------------|-----------------------|
| Nos quedamos sin tiempo y apuramos el cierre | Algo entregado a medias rompe lo que hoy funciona | Cronograma por etapas que entregan avances que funcionan por sí solos; **parada de emergencia declarada**: si al mediodía del 4 de septiembre algo no cierra, congelamos alcance y protegemos lo verificado | Al final de cada etapa hay una revisión obligatoria: o pasa, o se detiene y se atiende |
| Al reacomodar, un mensaje o un número cambia aunque sea un detalle | El usuario ve algo distinto y perdemos confianza | Comparación automática lado a lado de las doce verificaciones antes y después; nadie re-escribe textos de memoria | Cualquier diferencia, aunque sea de un carácter, frena la entrega |
| El punto único de alta nueva empieza a acumular decisiones de más | Volvemos al punto de partida: un lugar que lo hace todo y nadie lo entiende | Revisión obligatoria con una regla concreta: ese punto solo orquesta, no decide reglas | Aparece una regla de negocio dentro del punto de orquestación |
| La información vieja (ventas históricas) se lee distinto tras el cambio | Reportes o consultas del pasado dejan de salir igual | El cambio de lectura se probó contra los datos existentes antes de la entrega | La verificación de lectura de datos históricos falla |

## 6. Qué necesitamos del negocio

1. **Confirmar la canasta inicial de derivados** y sus precios (lácteos, carne, piel — ¿con qué variedad empezamos y a qué precio unitario?).
2. **Autorizar la ventana de trabajo** hasta el 6 de septiembre, con el equipo completo asignado.
3. **Una persona no técnica** para quince minutos: le presentamos esta vista y confirmamos que se entiende (queda grabado como evidencia).

## 7. Qué pasa si no se hace

- La próxima novedad comercial (un producto, un tipo de ganado, un dato nuevo del hato) costará lo mismo que la última, **o más**: los lugares a sincronizar no dejan de crecer solos.
- Cada cambio seguirá siendo una oportunidad de romper algo: más lugares tocados, más superficie de error.
- El conocimiento de cómo opera la hacienda seguirá repartido: ninguna persona nueva podrá responder "¿cómo se vende?" sin leerlo todo.
- La deuda no desaparece: se paga ahora barato (2-3 días con el sistema estable) o después caro (con el sistema crecido y bajo presión de una novedad urgente).

---

## ✅ Checklist de palabras prohibidas (verificación de esta vista)

- [x] Sin nombres de técnicas de diseño (no aparece ninguna)
- [x] Sin nombres de piezas internas del programa
- [x] Sin diagramas de ingeniería
- [x] Sin siglas técnicas
- [x] Sin "refactorizar", "desacoplar", "inyección de dependencias"
- [ ] **Pendiente (video)**: presentar a una persona ajena y sin formación técnica; anotar qué entendió. Si al quitar las palabras técnicas la vista se quedara sin contenido, es que no había contenido — este documento responde qué/cuánto/cuándo/riesgo/costo sin una sola de ellas.

> [!tip] Navegación (uso interno del equipo)
> La traducción técnica de cada frase de esta vista está en [[Reto2-Hacienda/Opcion2/09-VistaTecnica]] §6. Evidencia de que lo viejo no cambia: [[Reto2-Hacienda/Opcion2/06-VerificacionSOLID]]. Riesgos completos: [[Reto2-Hacienda/Opcion2/07-Riesgos]].
