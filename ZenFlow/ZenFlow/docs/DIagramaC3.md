```mermaid
classDiagram
    direction LR

    class Tarea {
        +int Id
        +string Titulo
        +DateTime FechaLimite
        +bool Completada
    }
    class Habito {
        +int Id
        +string Nombre
        +int RachaDias
        +DateTime UltimoCumplimiento
    }
    class AppBloqueada {
        +int Id
        +string Nombre
        +string Proceso
        +bool Activa
    }
    class ITareaRepo {
        <<interface>>
        +Guardar(tarea)
        +ObtenerTodas() List~Tarea~
        +Eliminar(id)
    }
    class IHabitoRepo {
        <<interface>>
        +Guardar(habito)
        +ObtenerTodos() List~Habito~
        +Eliminar(id)
    }
    class IAppBloqueadaRepo {
        <<interface>>
        +Guardar(app)
        +ObtenerTodas() List~AppBloqueada~
        +Eliminar(id)
    }
    class IGestorTareas {
        <<interface>>
        +AgregarTarea(titulo, fecha)
        +ObtenerTareasPendientes() List~Tarea~
        +CompletarTarea(id)
        +EliminarTarea(id)
    }
    class IGestorHabitos {
        <<interface>>
        +AgregarHabito(nombre)
        +RegistrarCumplimiento(id)
        +ObtenerTodos() List~Habito~
        +EliminarHabito(id)
    }
    class GestorTareas {
        -ITareaRepo _repo
        +AgregarTarea(titulo, fecha)
        +ObtenerTareasPendientes() List~Tarea~
        +CompletarTarea(id)
        +EliminarTarea(id)
    }
    class GestorHabitos {
        -IHabitoRepo _repo
        +AgregarHabito(nombre)
        +RegistrarCumplimiento(id)
        +ObtenerTodos() List~Habito~
        +EliminarHabito(id)
    }
    class GestorApps {
        -IAppBloqueadaRepo _repo
        +ObtenerTodas() List~AppBloqueada~
        +ObtenerProcesosActivos() List~string~
        +AgregarApp(nombre, proceso)
        +ToggleActivar(id)
        +Eliminar(id)
    }
    class MotorEnfoque {
        -DispatcherTimer _timer
        -List~string~ _appsABloquear
        +bool EnSesion
        +IniciarSesion(minutos, apps)
        +Pausar()
        +Terminar()
    }
    class TareaRepoJson {
        <<Singleton>>
        -static TareaRepoJson _instancia
        -static object _lock
        +static Instancia TareaRepoJson
        +Guardar(tarea)
        +ObtenerTodas() List~Tarea~
        +Eliminar(id)
    }
    class HabitoRepoJson {
        <<Singleton>>
        -static HabitoRepoJson _instancia
        -static object _lock
        +static Instancia HabitoRepoJson
        +Guardar(habito)
        +ObtenerTodos() List~Habito~
        +Eliminar(id)
    }
    class AppBloqueadaRepoJson {
        <<Singleton>>
        -static AppBloqueadaRepoJson _instancia
        -static object _lock
        +static Instancia AppBloqueadaRepoJson
        +Guardar(app)
        +ObtenerTodas() List~AppBloqueada~
        +Eliminar(id)
    }
    class TareaServiceProxy {
        <<Proxy>>
        -GestorTareas _gestor
        -string _tokenEsperado
        +AgregarTarea(titulo, fecha)
        +ObtenerTareasPendientes() List~Tarea~
        +EliminarTarea(id)
    }
    class HabitoServiceProxy {
        <<Proxy>>
        -GestorHabitos _gestor
        -string _tokenEsperado
        +AgregarHabito(nombre)
        +RegistrarCumplimiento(id)
        +ObtenerTodos() List~Habito~
        +EliminarHabito(id)
    }
    class TareasController {
        -IGestorTareas _gestor
        +GetTodas() List~Tarea~
        +Crear(request)
        +Eliminar(id)
    }
    class HabitosController {
        -IGestorHabitos _gestor
        +GetTodos() List~Habito~
        +Crear(request)
        +Cumplir(id)
        +Eliminar(id)
    }
    class VistaTareas {
        +AgregarTarea_Click()
        +Completar_Click()
        +Eliminar_Click()
    }
    class VistaHabitos {
        +AgregarHabito_Click()
        +Cumplir_Click()
        +Eliminar_Click()
    }
    class VistaEnfoque {
        +Iniciar_Click()
        +Pausar_Click()
        +Terminar_Click()
        +Toggle_Click()
        +AgregarApp_Click()
    }

    IGestorTareas <|.. GestorTareas
    IGestorHabitos <|.. GestorHabitos
    IGestorTareas <|.. TareaServiceProxy
    IGestorHabitos <|.. HabitoServiceProxy
    ITareaRepo <|.. TareaRepoJson
    IHabitoRepo <|.. HabitoRepoJson
    IAppBloqueadaRepo <|.. AppBloqueadaRepoJson
    GestorTareas --> ITareaRepo : usa
    GestorHabitos --> IHabitoRepo : usa
    GestorApps --> IAppBloqueadaRepo : usa
    TareaServiceProxy --> GestorTareas : delega
    HabitoServiceProxy --> GestorHabitos : delega
    TareasController --> IGestorTareas : usa
    HabitosController --> IGestorHabitos : usa
    VistaTareas --> GestorTareas : usa
    VistaHabitos --> GestorHabitos : usa
    VistaEnfoque --> GestorApps : usa
    VistaEnfoque --> MotorEnfoque : usa
    GestorTareas --> Tarea
    GestorHabitos --> Habito
    GestorApps --> AppBloqueada

    note for TareaRepoJson "Patrón Singleton:\nuna sola instancia\ncon lock para concurrencia"
    note for TareaServiceProxy "Patrón Proxy:\nverifica token\nescribe bitácora\ndelega al gestor real"
    note for IGestorTareas "DIP aplicado:\nControllers y Proxy\ndependen de la interfaz,\nno de la implementación"
```
