# Guion Video Reto 2 — Palabra por palabra (20 minutos)

**Equipo:** Mateo (líder) · Alejandra (verificación) · David (comunicación) · Sara (invitada, no técnica, solo bloque 2)

> Cómo usar este guion: cada bloque dice **QUÉ MOSTRAR** y **QUÉ DECIR** (textual). Los tiempos internos son guía para no pasarse. Hablar natural, no recitado: si una frase no suena a uno, decirla con las propias palabras manteniendo la idea.

**Checklist antes de grabar:**
- [ ] PDF del documento abierto en: sección 1 (dolores), 3.3 (fichas), 4.1 (matriz), 4.2 (casos), 5 (riesgos), 6.1 (vista negocio), 2.2 (bitácora)
- [ ] Los dos diagramas a mano: `diagramas/Layer_3_sale.png` (lo que sale) y `diagramas/Layer_4_tobe.png` (TO-BE)
- [ ] Grabación de la app: inventario de Derivados + una venta con derivados + historial de ventas (ya hecha o por hacer)
- [ ] Sara con la vista de negocio leída ANTES de grabar
- [ ] Segundero visible para quien modere (Mateo)

---

## BLOQUE 1 · 0:00 – 3:00 — MATEO — Los puntos de dolor

**[PANTALLA: portada 10 segundos → sección 1 del documento: tabla de puntos de dolor]**

### 0:00 – 0:30 (presentación)
**MATEO:**
"Buenas tardes. Somos el equipo de arquitectura del proyecto Hacienda. Yo soy Mateo Rojas, el arquitecto líder. Me acompañan Alejandra Vargas, arquitecta de verificación, y David Salcedo, arquitecto de comunicación y vistas. Este video muestra la segunda evolución del sistema: pasamos de un diseño correcto a un diseño robusto aplicando patrones de diseño, sin cambiar el estilo arquitectónico y sin modificar el comportamiento que el usuario ya conoce."

### 0:30 – 1:10 (el problema)
**MATEO:**
"Empecemos por el problema. El Reto 1 nos dejó un sistema que cumplía los principios: responsabilidades separadas, dependencias invertidas. Pero cuando medimos cuánto costaba cambiarlo, encontramos puntos muy caros. Y no lo estimamos a ojo: para cada dolor contamos los archivos y las clases reales que había que abrir."

### 1:10 – 2:35 (los dolores, señalando la tabla)
**MATEO:**
"El más caro: agregar un tipo de res nuevo, por ejemplo una vaca lechera. Hoy exige editar **seis archivos repartidos en dos capas**: la fábrica, el catálogo, los parámetros, el gestor y el enum. Si se olvida uno, quedan datos a medias.

El segundo: **la venta solo admite una res**. Vender una res junto con sus derivados — leche, carne, cuero — obligaba a reescribir la venta completa: constructor, fábrica, repositorio y vista.

El tercero: **la decisión de qué vacuna crear estaba partida en tres sitios**: la interfaz tenía un método por tipo, el controlador decidía con un condicional por texto, y la vista repetía los mismos nombres cinco veces.

El cuarto: **la validación estaba duplicada en cuatro validadores**, y ya tenían una contradicción documentada: una parte aceptaba un monto que otra parte rechazaba.

Y el quinto: **el sistema solo podía reaccionar a sus propios eventos escribiendo en consola**. Teníamos un evento definido que jamás se publicaba, porque no había forma de reaccionar sin editar al que publica.

En total catalogamos más de diez puntos de dolor. Tres de ellos los encontramos sin ayuda de ninguna herramienta: a mano, leyendo código."

### 2:35 – 3:00 (lo que no se toca)
**MATEO:**
"Y tres puntos los marcamos como 'no intervenimos' a propósito: el control de permisos, el orden de arranque y la moneda única. Porque activarlos o moverlos cambia lo que el usuario ve, o cuesta más que el problema que curan. Esa también es una decisión de arquitecto: saber no tocar."

---

## BLOQUE 2 · 3:00 – 6:00 — DAVID + SARA — La vista para el negocio

**[PANTALLA: sección 6.1 del documento (vista de negocio). REGLA: cero palabras técnicas. Sara aparece en cámara al lado de David]**

### 3:00 – 3:20
**DAVID:**
"Yo soy David y me tocó traducir todo esto para el negocio. Esta parte la voy a contar sin ninguna palabra técnica, porque está pensada para la persona que aprueba el presupuesto, no para la que programa."

### 3:20 – 4:45
**DAVID:**
"¿Qué le vamos a hacer al sistema? Por dentro cambia la forma en que nacen los animales, las vacunas y las ventas, y la forma en que el sistema reacciona cuando pasa algo importante. Por fuera no cambia nada: las pantallas, los reportes, los mensajes y los cálculos siguen exactamente iguales, palabra por palabra.

¿Dónde se va hoy el tiempo y el dinero? En tres cosas. Uno: sacar un tipo nuevo de animal exige tocar seis sitios distintos del sistema, y olvidarse de uno deja la información a medias. Dos: no se puede vender un animal junto con sus derivados en una sola operación; hoy eso es imposible sin reescribir la venta completa. Tres: cuando algo pasa — por ejemplo, que un producto quede con poco stock — el sistema no hace nada solo; alguien tiene que ir a revisar.

¿Qué gana el negocio? Agregar un tipo nuevo pasa de seis sitios a uno solo. La venta de un animal con sus derivados pasa a ser una sola operación, que es justamente lo que pidió el negocio. Y el sistema empieza a avisar solo cuando algo requiere atención.

¿Qué cuesta? El mismo equipo, sin licencias ni tecnologías nuevas, en seis etapas, y cada etapa trae su prueba de que nada cambió para el usuario. ¿Y si no se hace? Cada tipo nuevo seguirá costando seis ediciones coordinadas, y el negocio de los derivados no se podrá facturar junto."

### 4:45 – 6:00 (la prueba con Sara — LA evidencia que pide la rúbrica)
**DAVID** *(mirando a Sara)*:
"Invitamos a Sara, que no es del equipo y no es de sistemas. Sara, usted leyó esta hoja. Cuéntenos con sus palabras: ¿qué entiende que se le va a hacer al sistema?"

**SARA** *(con sus palabras; esto es la guía de lo que debe cubrir)*:
"Entiendo que están acomodando por dentro cómo se crean los animales, las vacunas y las ventas, para que agregar algo nuevo sea más fácil y no toque seis sitios. Que la venta de un animal con sus derivados se podrá hacer de una sola vez. Y que lo que ve la gente que usa el sistema no cambia: las mismas pantallas y los mismos mensajes."

**DAVID:**
"Eso es exactamente lo que queríamos escuchar. Si una persona sin formación técnica lo entiende, la vista sirve. Esa es la prueba que pide el encargo."

---

## BLOQUE 3 · 6:00 – 11:00 — MATEO — El TO-BE: qué sale, qué entra, cómo se conecta

**[PANTALLA: `Layer_3_sale.png` → luego `Layer_4_tobe.png` → luego fichas de la sección 3.3]**

### 6:00 – 7:00 (lo que sale — mostrar diagrama 3)
**MATEO:**
"Entramos al diseño. Primero, lo que se retira, marcado en rojo. Sale la vieja fábrica de reses, que era un diccionario con un condicional por tipo. Sale la fábrica de vacunas con un método por categoría. Salen los cuatro validadores con sus umbrales contradictorios. Y sale el publicador que solo sabía escribir en consola. En naranja, lo que se transforma: el gestor de reses deja de decidir el tipo; el catálogo se queda solo con leer; y la venta pasa de estar soldada a una sola res a ser de varios ítems."

### 7:00 – 9:00 (patrón más discutido 1: Factory Method + Template Method — mostrar TO-BE y ficha)
**MATEO:**
"El patrón más discutido del proyecto fue **Factory Method**, fusionado con **Template Method**. ¿Por qué fue el más discutido? Porque el dolor más caro era ese condicional que crecía con cada tipo nuevo, y porque tuvimos que decidir cómo repartir la creación y la validación sin duplicar reglas.

La solución: **una clase fábrica por tipo** — una para ternero, una para cebón, una para novillo, una para vaca lechera — todas hijas de una base que tiene el proceso común sellado: validar los datos, generar el identificador y construir. Lo que cambia entre tipos no es lógica: son **datos** — el rango de edad, los máximos de vacunas — y entran como propiedades. Eso es Template Method: el procedimiento vive una sola vez en la base.

¿Y quién decide qué fábrica usar? **Un registro**. En el arranque, el sistema inyecta todas las fábricas y el registro arma el diccionario tipo-fábrica. Ya nadie más en el sistema pregunta 'si es ternero haz esto'.

La prueba de que funcionó es medible: agregar la vaca lechera costó **una clase de dieciséis líneas y una línea de registro**. Antes costaba seis archivos en dos capas. Esa es la promesa del principio abierto-cerrado, cumplida y medida. Y ese mismo idioma lo repetimos en vacunas y en los productos derivados: tres familias de fábricas, un solo lenguaje."

### 9:00 – 10:00 (patrón más discutido 2: Builder)
**MATEO:**
"El segundo más discutido fue **Builder**, y llegó empujado por la solicitud de cambio. El constructor de la venta se estaba volviendo telescópico: parámetros que a veces aplican y a veces no. Con el builder, la venta se arma paso a paso: se inicia, se agrega la res — que es obligatoria — se agregan los productos que sean, y al construir valida todas las reglas de una sola vez y calcula el total. La venta pasó a ser una **lista de ítems vendibles**: una res y un queso son intercambiables como ítems, gracias a un contrato mínimo que solo pide la descripción."

### 10:00 – 10:35 (Observer)
**MATEO:**
"Y **Observer** resuelve la reacción. Cuando pasa un hecho — una venta realizada — el sistema publica un evento, y los manejadores suscritos reaccionan: uno escribe en consola exactamente las mismas líneas de antes, y otro revisa el stock de derivados y avisa si algo quedó por debajo del mínimo. El que publica no conoce a los que reaccionan. Agregar una reacción nueva es una clase y un registro."

### 10:35 – 11:00 (cómo se conectan)
**MATEO:**
"¿Cómo se conectan entre ellos? El esqueleto de la fábrica construye y publica; el builder arma la venta con productos que salen de las fábricas; y el evento de la venta lo capturan los manejadores. Todo se ensambla en un solo archivo, al final del arranque, sin reordenar nada de lo que ya funcionaba."

---

## BLOQUE 4 · 11:00 – 14:00 — ALEJANDRA — SOLID sigue en pie

**[PANTALLA: sección 4.1 (matriz) → luego 4.2 (capturas de los casos, lado a lado)]**

### 11:00 – 11:25
**ALEJANDRA:**
"Yo soy Alejandra y me tocó demostrar que los patrones no rompieron nada. Son dos demostraciones: que los principios siguen en pie y que el comportamiento es el mismo. No lo afirmamos: lo demostramos."

### 11:25 – 12:50 (la matriz, señalando)
**ALEJANDRA:**
"Esta es la matriz: cada patrón adoptado contra cada principio.

**Factory Method** refuerza la responsabilidad única, porque la creación salió de los servicios; refuerza abierto-cerrado, porque el tipo nuevo no editó nada existente; y refuerza la inversión de dependencias, porque todos dependen del registro y nadie de una fábrica concreta.

**Template Method** trae dos tensiones, y las declaramos con su compensación. Primera: un paso nuevo del proceso obliga a editar la base, y todas las hijas lo heredan. La compensamos con una decisión de diseño: lo que varía entre tipos son datos, no pasos. Segunda: una subclase podría 'no poder' cumplir un paso, y eso rompe la sustituibilidad. La compensación: los ganchos son propiedades de datos — un rango, un máximo — que ninguna subclase puede dejar vacíos. La vaca lechera pasa por el mismo esqueleto sin ningún método vacío: ese es el caso diecisiete.

**Builder** refuerza los cinco: separa la construcción de las reglas, su contrato tiene una sola operación, y el reloj entra por constructor como en todo el sistema.

**Observer** también refuerza: los publicadores no cambiaron ni una línea — ya dependían de una interfaz — y agregar una reacción no toca a nadie más."

### 12:50 – 14:00 (los casos)
**ALEJANDRA:**
"Y el comportamiento. Corrimos los doce casos del Reto 1 y los seis nuevos que recorren lo que los patrones tocan, con los mismos datos de arranque y el mismo reloj, antes y después. Aquí están las capturas lado a lado: agregar una res válida; rechazar una edad fuera de rango **con el mismo mensaje exacto**; la res duplicada; aplicar vacunas y sus máximos por categoría; el lote de veinticinco vacunas; la venta exitosa; la venta en un potrero inexistente; el chip instalado y el duplicado; el cambio de estado válido y el inválido; y la res alimentada. La consola y los mensajes al usuario son idénticos.

Los seis nuevos demuestran lo que cambió por dentro sin que se note por fuera: la identidad estable al releer una venta; la venta con derivados de punta a punta; el monto en cero con una sola respuesta posible; el orden fijo de los avisos; y la serialización con el mismo formato. La diferencia entre el antes y el después es cero."

---

## BLOQUE 5 · 14:00 – 17:00 — MATEO + ALEJANDRA — Riesgos y plan

**[PANTALLA: sección 5 del documento (tabla de riesgos)]**

### 14:00 – 15:30 (Mateo — los 3 riesgos con su señal)
**MATEO:**
"Ningún cambio es gratis, y esto también se lo debemos contar al negocio. Nuestros tres riesgos de mayor exposición.

**Riesgo uno: el cronograma.** Nos quedaban cuatro días para implementar, documentar y grabar. Probabilidad cuatro, impacto cuatro. Cómo lo evitamos: frentes repartidos con hito diario y el trabajo montado por fases. Cómo nos damos cuenta de que se está cumpliendo: una actividad sin avance al cierre del día.

**Riesgo dos: la regresión silenciosa.** Mover código y perder una función sin enterarnos. Nos pasó una vez: la validación de edad del sistema original se perdió en una refactorización intermedia. La detectamos revisando el historial, la restauramos con el mensaje exacto, y desde ahí cada función congelada tiene su propio caso que la blinda. La señal: un caso que hoy pasa y después de un cambio falla.

**Riesgo tres: que un mensaje congelado cambie de texto**, porque eso se penaliza caso por caso. Lo evitamos con la tabla de casos que mostró Alejandra, comparada antes y después con los mismos datos y el mismo reloj. La señal de alerta: cualquier diferencia en esa comparación."

### 15:30 – 17:00 (Alejandra — el plan por fases)
**ALEJANDRA:**
"Y el plan de cambio, en seis fases. Uno: las fábricas de reses con su registro. Dos: vacunas, con su objeto de datos y el lote común. Tres: fuera los validadores y el esqueleto completo. Cuatro: el builder y la venta de varios ítems. Cinco: el despachador de eventos con sus manejadores. Y de último la solicitud de cambio: productos derivados, la vaca lechera y el aviso de stock.

Cada fase tiene su prueba de salida: compilación limpia y sus casos en verde antes de pasar a la siguiente. Si una fase falla, se revierte solo esa fase y el sistema queda operativo. Ningún paso requiere migración destructiva: solo se agregan tablas nuevas."

---

## BLOQUE 6 · 17:00 – 20:00 — DAVID — La IA, la solicitud implementada y la deuda

**[PANTALLA: sección 2.2 (bitácora) → grabación de la app → lista de deuda]**

### 17:00 – 18:20 (decisiones frente a la IA)
**DAVID:**
"Cerramos con cómo trabajamos con la IA, que también se evalúa. La regla del equipo fue: **la herramienta propone, el equipo decide con evidencia**.

Tres decisiones que fueron nuestras, no de la herramienta. Primera: la herramienta quería incluir las vistas web como objetivo de cambio; dijimos que no — las pantallas cuentan como costo, no como objetivo — y ese rechazo quedó registrado en la bitácora. Segunda: detectamos que una validación de edad que creíamos innecesaria era en realidad una función que se había perdido del sistema original; lo verificamos con el historial de git y la restauramos con su mensaje exacto. Eso fue criterio del equipo. Tercera: al evaluar los veintidós patrones del catálogo rechazamos dieciocho, cada uno con su argumento técnico: Abstract Factory, porque no hay familias de productos que nazcan juntas; Singleton, porque la unicidad ya la da el punto de ensamblaje y un acceso global complica las pruebas; Fachada, porque nuestros servicios ya cumplen ese papel y una capa más absorbería lógica; y Strategy, porque la categoría de la vacuna se elige una vez en el formulario, no se intercambia mientras el programa corre. No es que no nos gustaran: cada descarte tiene su fila con el por qué."

### 18:20 – 19:20 (SC-1 implementada — pasar a la grabación de la app)
**DAVID:**
"Y la solicitud de cambio que nos autorizaron — vender derivados del ganado — quedó implementada. En pantalla: aquí está el inventario de derivados con su stock y su alerta de mínimo. Y aquí una venta real: una res más dos quesos campesinos **en una sola operación**, con el total sumando los ítems. En el historial de ventas queda registrada con sus ítems desplegables. Y en la consola, el aviso de stock aparece después del mensaje de siempre, en orden fijo."

### 19:20 – 20:00 (deuda + cierre)
**DAVID:**
"Por último, la deuda que dejamos declarada, porque un arquitecto también dice lo que no hizo: la decisión de cómo persistir los ítems de venta quedó documentada, aunque ya funciona; el patrón Composite quedó evaluado y fuera mientras no existan canastas de productos; tres puntos del catálogo siguen congelados a propósito; y en el código, el servicio de vacunas todavía lanza excepciones donde otros servicios devuelven un resultado — el usuario no nota la diferencia, pero es deuda interna que pagamos en la próxima iteración.

En resumen: el sistema pasó de correcto a robusto, con cuatro patrones, cada uno atado a un dolor medido, sin romper ningún principio y sin cambiar una sola palabra de lo que el usuario ve. Gracias."

---

## Notas de grabación

| Momento | Señal de control |
|---|---|
| Fin bloque 1 | 3:00 — si vas tarde, cortar presentación, no los dolores |
| Fin bloque 2 | 6:00 — Sara es intocable: es la evidencia de la rúbrica |
| Fin bloque 3 | 11:00 — si aprieta, comprimir Observer, no Factory |
| Fin bloque 4 | 14:00 — matriz completa sí o sí; casos puede resumir pares |
| Fin bloque 6 | 20:00 — después de 20 no se evalúa: cerrar en 19:30 |

- Cámara prendida para todos, siempre.
- Quien no habla, mira a cámara o a quien habla (no al guion).
- Las frases entre comillas son la base: decir con palabras propias si suena más natural, manteniendo la idea y el número.
