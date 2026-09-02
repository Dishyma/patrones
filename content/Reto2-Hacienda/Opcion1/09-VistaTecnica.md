---
tags: [reto2, actividad-6, vista, tecnica, hacienda]
estado: v2 — para el ingeniero que entra nuevo al back (2026-09-02)
---

# 09 — Vista para el Equipo de Desarrollo

> [!abstract] Para quién
> El ingeniero que llega al back después de este trabajo: dónde vive cada decisión, cómo se extiende el sistema sin romperlo, y qué NO volver a hacer. El diseño completo está en [[Reto2-Hacienda/Opcion1/05-TOBE|05-TOBE]]; aquí va el mapa práctico.

## 1. El mapa en 30 segundos

```
Program.cs (composition root — SOLO se le AÑADEN registros al final, P-08)
   ├── RegistroDeReses      ← IEnumerable<FabricaDeRes>      (crear + rehidratar)
   ├── RegistroDeVacunas    ← IEnumerable<FabricaDeVacuna>   (crear + lotes)
   ├── RegistroDeProductos  ← IEnumerable<FabricaDeProducto> (SC-1)
   ├── VentaBuilder         ← reloj/guid por ctor
   └── DespachadorDeEventos ← handlers en ORDEN (consola 1º) — implementa IDomainEventPublisher (interfaz intacta)

FabricaDeX (abstracta): esqueleto SELLADO
   validar comunes → #Construir (hook FM) → exigir regla del subtipo → publicar evento
   hooks = PROPIEDADES-DATO (RangoEdad, MaxVacunas, parámetros) — nunca pasos opcionales
```

- **La base está sana y congelada**: entidades encapsuladas (mutación solo por métodos), reglas de negocio en `Domain/Reglas/` con fuente única (`ParametrosRes` ← `CatalogoRes`), VOs auto-validados.
- **Las capas no cambian**: Domain se fortalece, Application adelgaza, Infrastructure delega, Web solo DI.

## 2. Las 3 operaciones que vas a hacer y cómo se hacen

### 2.1 Agregar un tipo de res (ej. la vaca lechera ya entra así)
1. `class VacaLechera : Res` — identidad + `RangoEdad` como dato (`ParametrosRes.VacaLechera` si trae parámetros nuevos).
2. `class FabricaVacaLechera : FabricaDeRes` — `TipoAtendido` + `#Construir` (3 líneas).
3. `Program.cs`: `services.AddTransient<FabricaDeRes, FabricaVacaLechera>()` — **al final**.
✅ Estadísticas, vistas y BD la muestran vía `Tipo` **sin tocar nada más**. Es la demostración OCP medida (antes: 6 archivos/2 capas).

### 2.2 Agregar una reacción a un evento (ej. stock bajo de derivados)
1. `class HandlerStockX : IDomainEventHandler<VentaRealizadaEvent>` — tu lógica.
2. `Program.cs`: regístralo **después** del de consola.
✅ Nadie que publica cambia. Si necesitas un evento que no existe, se declara en `DomainEvents` y lo publica el esqueleto de la fábrica correspondiente.

### 2.3 Evolucionar la venta (ítems nuevos)
`IVendible` es el contrato: si el nuevo ítem lo implementa, entra por `VentaBuilder.ConItem(...)` — total e invariantes salen de `Build()`. Persistencia: ver decisión D-11 (`venta_items`).

## 3. Invariantes del sistema (lo que NO se rompe)

| Invariante | Por qué | Dónde se blinda |
|---|---|---|
| Mensajes palabra por palabra | Regla de la Líder Técnica (−0.5/caso) | Tabla congelada 06 §4.2; toda migración de validación copia el string EXACTO |
| `Id` estable en rehidratación | La identidad no se inventa al leer | `RegistroDeReses.Rehidratar` es la ÚNICA puerta de rehidratación (fin del GUID-por-lectura) |
| Enum `TipoRes` no se elimina | Las vistas y la BD lo consumen | Dejó de ser punto de decisión; es superficie de lectura |
| Reglas en un solo lugar | Ya sufrimos umbrales contradictorios (`<0` vs `<=0`) | Esqueleto sellado + `Reglas/ParametrosX`; PROHIBIDO re-implementar una regla en servicio |
| Orden de arranque | Efectos secundarios en startup | `Program.cs`: solo append; nunca reordenar |
| Sin singleton/global | Testabilidad y DIP | Unicidad = composition root |

## 4. Errores típicos que este diseño previene (y cómo detectarlos en code review)

| Si ves… | Está mal porque | Debería ser |
|---|---|---|
| `switch`/diccionario por tipo en una fábrica | Mueves el punto de modificación, no lo eliminas (OCP) | Creator concreto + registro |
| Override vacío o que lanza "no soportado" en un creator | Subtipo mudo = sustituibilidad rota (LSP) | Hook como propiedad-dato |
| Validación post-construcción en el servicio | Vuelve la dupla fábrica/validador desincronizable | Paso sellado del esqueleto o invariantes del ctor |
| Un servicio que arma el mensaje Y reacciona | Reacción copiable (la duplicación de GestorReses) | Publica; el handler reacciona |
| `TimeProvider`/`Guid` pasados por parámetro de método | Idioma partido (la firma vieja de `IVentaFactory`) | Inyección por ctor en todo el sistema |
| Nuevo archivo de constantes de negocio | Se multiplica la fuente de verdad | `Domain/Reglas/ParametrosX` existente o nuevo PERO único |

## 5. Dónde está cada cosa (índice rápido)

- **Decisiones y porqués**: [[Reto2-Hacienda/Opcion1/04-DecisionesArquitectonicas|04-Decisiones]] · [[Reto2-Hacienda/Opcion1/03-PatronesEvaluados|03-Patrones]] (por qué NO los otros 18).
- **Estructura completa del cambio**: [[Reto2-Hacienda/Opcion1/05-TOBE|05-TOBE]] tabla E-XX.
- **Cómo se demuestra que nada rompió**: [[Reto2-Hacienda/Opcion1/06-VerificacionSOLID|06-Verificación]] casos C-01…C-18.
- **Historial de decisiones con IA**: [[Reto2-Hacienda/Opcion1/10-BitacoraIA|10-Bitácora]] (B-01…B-16) — incluye la regresión que atrapamos (DEC-09): *comportamiento congelado también se audita contra el historial de git, no contra la memoria*.
- **Diagramas**: `diagramas/` — evolución UML (2 páginas) y capas superpuestas AS-IS/TO-BE.

## 6. Onboarding en una tarde

1. Corre la solución y hazte un usuario + un potrero + una res + una vacuna + una venta (flujos C-01…C-09).
2. Lee `Program.cs` completo — es la foto del sistema.
3. Abre `FabricaDeRes` y sigue un `Crear` con el debugger: verás el esqueleto completo.
4. Intenta romperlo: agrega un tipo de res (§2.1) y comprueba que nada más se tocó. Ese es el sistema funcionando como debe.
