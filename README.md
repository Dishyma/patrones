# Arquitectura de Software — Vault publicado con Quartz

Sitio estático generado con [Quartz 4](https://quartz.jzhao.xyz) a partir de mi vault de Obsidian.

**🌐 Sitio:** https://dishyma.github.io/patrones/

## Contenido

- 📚 Cursos intensivos de arquitectura de software
- 🧩 Patrones de diseño (creacionales, estructurales, comportamiento)
- 🛠️ Implementaciones .NET 8: Chain of Responsibility y Command
- 🐄 **Reto 2 — Hacienda**: análisis AS-IS → TO-BE completo (Opción 1 y 2), con diagramas Mermaid renderizados y `.drawio` descargables

## Publicar cambios

El contenido vive en `content/`. Cada push a `main` dispara el workflow de GitHub Pages (`.github/workflows/deploy.yaml`) que re-construye y publica el sitio.

Para editar y publicar:

```bash
# editar archivos en content/
npx quartz build   # preview local: npx quartz build --serve
git add content && git commit -m "notas: ..." && git push
```
