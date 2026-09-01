# Reto 2 - Proyecto Hacienda (Opción 1)

## Estructura de la entrega

```
Opcion1/
├── 00-Plan.md                    # Plan maestro, metodología, mapeo actividad↔criterio
├── 01-AS-IS.md                   # Foto completa del sistema actual (99 archivos, ~3.7k LOC)
├── 02-PuntosDolor.md             # Actividad 1: 12 puntos P-01..P-12 con evidencia archivo:línea
├── 03-PatronesEvaluados.md       # Actividad 2.1: 22 fichas completas (Anexo A)
├── 04-DecisionesArquitectonicas.md # Act. 2: 10 decisiones (DEC-01..DEC-10)
├── 05-TOBE.md                    # Actividad 3: diagramas A/B, tabla E-XX, 4 fichas
├── 06-VerificacionSOLID.md       # Actividad 4: matriz 4×5 + 12 casos C-01..C-12
├── 07-Riesgos.md                 # Actividad 5: 8 riesgos + plan F0-F7
├── 08-VistaNegocio.md            # Actividad 6.a: 0 jerga técnica
├── 09-VistaTecnica.md            # Actividad 6.b: guía "dónde tocar" (7 filas)
├── 10-BitacoraIA.md              # Actividad 2.2: 12/10 registros
├── diagramas/
│   ├── Reto2_ASIS_TOBE_Capas.drawio    # ← DIAGRAMA PRINCIPAL (2 capas superpuestas)
│   └── mermaid_sources/              # Fuentes Mermaid para importar/editar
│       ├── 01_ASIS_Arquitectura.mmd
│       ├── 02_TOBE_Arquitectura.mmd
│       ├── 03_FactoryMethod_TemplateMethod_Observer.mmd
│       └── 04_Casos_Comportamiento.mmd
└── README.md                       # Este archivo
```

## Diagrama principal — la EVOLUCIÓN completa (varios diagramas, como pidió el profesor)

**Cadena de evolución (3 generaciones, cada una en su archivo):**

| Generación | Archivo | Contenido |
|------------|---------|-----------|
| Gen 1 — Legado (Reto 1 AS-IS) | `01-diagnostico/UML_AS-IS_editable.dia` (existe, 143 objetos) | Bib_Hacienda + p_mvcHacienda — el código de los hallazgos H-01…H-32 |
| Gen 2 — SolucionSOLID (Reto 1 TO-BE = **Reto 2 AS-IS**) | `02-diseno/UML_Hacienda_Unificado.dia` (existe, 293 objetos) | Inventario completo de clases del rediseño SOLID |
| **Gen 3 — Reto 2 TO-BE** | **`diagramas/Reto2_Evolucion_UML.drawio`** (NUEVO, 2 páginas) | **Página 1:** recorte del AS-IS marcado (🔴 sale / 🟠 se transforma / ⚪ permanece, con archivo:línea). **Página 2:** TO-BE por patrón (FM azul, TM amarillo, Builder verde, Observer rosa, Registros morado, SC-1 punteado, ⚪ lo conservado en negro) — mismo layout que la página 1 para VER la evolución al alternar pestañas |

**Bonus — Capas superpuestas (formato que el enunciado valora "muy bien"):** `diagramas/Reto2_ASIS_TOBE_Capas.drawio` — una página, dos capas alternables (AS-IS gris debajo + TO-BE en color encima). Ideal para el video (min 6–11): mostrar capa base → activar la superpuesta.

**Fuentes Mermaid editables:** `diagramas/mermaid_sources/*.mmd` (4 archivos).

## Cómo regenerar el PDF

1. Abrir los `.md` en Obsidian (o VS Code + Markdown Preview Enhanced).
2. Exportar a PDF (≤15 pág, paginado, con índice).
3. Los diagramas Mermaid se renderizan automáticamente; el `.drawio` se exporta como imagen aparte y se inserta en la sección 3.1 de `05-TOBE.md`.

## Checklist rápido antes de entregar

- [ ] 11 archivos Markdown en orden (00 → 10)
- [ ] Diagrama `.drawio` con 2 capas alternables
- [ ] 12 casos C-01..C-12 con capturas "antes/después" en `04-evidencia/`
- [ ] Código refactorizado compila (`dotnet build Hacienda.Web`)
- [ ] Video 20 min con 4 frentes (guion en `00-Plan.md` §7)
- [ ] Bitácora IA con 12/10 registros (`10-BitacoraIA.md`)

---

**Estado:** Listo para sustentación (6 de septiembre).  
**Dudas abiertas:** D-04 (frentes 3 personas), D-06 (hallazgos propios), D-11 (esquema BD).
