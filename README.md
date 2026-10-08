# Grupo de motores de desarrollo

Proyecto grupal de desarrollo con Unity. Este repositorio conserva el trabajo realizado en ramas temáticas y el historial original de commits.

## Estado del repositorio

**Importante:** la rama `main` actualmente solo contiene `.gitattributes` y `.gitignore`. Los recursos del proyecto están distribuidos entre las ramas `Assets`, `Enemies`, `Enemigos`, `Maps`, `Moobs`, `Player`, `Scenes` y `Systems`. Por lo tanto, todavía **no hay un proyecto Unity completo e integrado en main**. No se debe presentar esta rama como un juego ejecutable.

## Organización objetivo para el proyecto integrado

```text
ProyectoUnity/
├── Assets/
│   ├── Animations/
│   ├── Art/
│   ├── Audio/
│   ├── Materials/
│   ├── Prefabs/
│   │   ├── Characters/
│   │   ├── Enemies/
│   │   ├── Environment/
│   │   └── UI/
│   ├── Scenes/
│   ├── Scripts/
│   │   ├── Player/
│   │   ├── Enemies/
│   │   ├── Systems/
│   │   └── UI/
│   └── Settings/
├── Packages/
├── ProjectSettings/
├── .gitignore
└── README.md
```

La estructura es una **propuesta de integración**, no una descripción de carpetas ya creadas. Se deben mover archivos dentro de Unity, junto con sus `.meta`, para conservar los GUID y evitar referencias rotas. Primero identificar la versión de Unity y el proyecto base, y comprobar escenas, prefabs y dependencias antes de fusionar las ramas.

## Ramas de trabajo

| Rama | Contenido observado |
| --- | --- |
| `Assets` | Animadores, arte y prefabs de ítems |
| `Enemies` / `Enemigos` | Scripts y prefabs de enemigos y jefes |
| `Maps` | Proyecto y recursos de mapas (incluye archivos generados por IDE) |
| `Moobs` | Materiales y prefabs de mapa y animales |
| `Player` | Scripts y modelos del jugador |
| `Scenes` | Escenas y recursos del entorno |
| `Systems` | Scripts y prefabs de sistemas del juego |

## Criterios de entrega

- **Carpetas:** organizar `Assets` por tipo y función; mantener `.meta`.
- **Unity:** versionar `Assets/`, `Packages/` y `ProjectSettings/` del proyecto integrado.
- **Git:** el `.gitignore` excluye cachés, compilaciones y archivos del IDE. No elimina automáticamente archivos que ya fueron subidos.
- **Historial:** conservar los commits existentes y hacer nuevos commits descriptivos por cambio real.
- **Colaboración:** documentar integrantes y aportes comprobables en `CONTRIBUTING.md`, usando commits y pull requests como evidencia.

## Próximos pasos de integración

1. Identificar el proyecto Unity principal y su versión.
2. Integrar los recursos de las ramas temáticas en una rama de trabajo sin sobrescribir archivos homónimos.
3. Reorganizar desde Unity y verificar GUID, escenas, scripts y prefabs.
4. Probar el proyecto en el editor y documentar los resultados.
5. Abrir pull request hacia `main` después de la validación.
