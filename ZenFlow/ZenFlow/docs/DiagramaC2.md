```mermaid
classDiagram
    direction LR

    class Estudiante {
        +usar() AppWPF
        +consultar() API
    }
    class AppWPF {
        <<Container WPF/XAML>>
        +VistaTareas
        +VistaHabitos
        +VistaEnfoque
    }
    class ZenFlowAPI {
        <<Container ASP.NET Core>>
        +Swagger UI
        +TareasController
        +HabitosController
    }
    class CapaLogica {
        <<Container C# Classes>>
        +GestorTareas
        +GestorHabitos
        +GestorApps
        +MotorEnfoque
    }
    class CapaRepositorios {
        <<Container Interfaces>>
        +ITareaRepo
        +IHabitoRepo
        +IAppBloqueadaRepo
    }
    class CapaDatos {
        <<Container JSON Singleton>>
        +TareaRepoJson
        +HabitoRepoJson
        +AppBloqueadaRepoJson
    }
    class WindowsOS {
        <<Sistema Externo>>
        +APIs de procesos
    }
    class ArchivosJSON {
        <<Sistema Externo>>
        +tareas.json
        +habitos.json
        +apps.json
    }

    Estudiante --> AppWPF : interactúa con
    Estudiante --> ZenFlowAPI : consulta via HTTP
    AppWPF --> CapaLogica : llama a
    ZenFlowAPI --> CapaLogica : llama a via Proxy
    CapaLogica --> CapaRepositorios : depende de
    CapaRepositorios <|.. CapaDatos : implementada por
    CapaDatos --> ArchivosJSON : lee y escribe
    CapaLogica --> WindowsOS : bloquea procesos
```