# ADRS: Proyecto
## ZenFlow — todo en un lugar, sin que nada te interrumpa
### Autor:	Dylan Vazquez Soriano
### Fecha:	15/05/2025

# Contexto
La idea surgió de un problema que tengo yo mismo: cuando tengo que estudiar o hacer una tarea, termino usando tres o cuatro herramientas distintas al mismo tiempo. Google Calendar para ver qué tengo pendiente, Notion para tomar notas, un timer en el teléfono para el pomodoro, y aun así termino revisando Instagram a los diez minutos. El problema no es que me falten herramientas, es que tengo demasiadas y ninguna me obliga a quedarme concentrado.

ZenFlow es una aplicación de escritorio para Windows dirigida a estudiantes universitarios. La idea central es tener tareas, hábitos y un modo de enfoque en un solo lugar, donde el modo enfoque no sea solo un cronómetro bonito sino que realmente bloquee las aplicaciones que me distraen mientras estoy trabajando. Es un proyecto personal por que creo en la idea de que los estudiantes sean mejor el dia de mañana ademas de que me ayuda a mejorar mis habilidades con las tecnologias que usamos para hacer estos proyectos.

# Decisión
Voy a construir ZenFlow como una aplicación de escritorio Windows usando C# con WPF en Visual Studio. La arquitectura va a seguir una separación en capas: la interfaz no va a saber nada de cómo se guardan los datos, y la lógica del negocio va a estar separada de ambas. Voy a aplicar los principios que vimos en clase, especialmente SRP y DIP, para que agregar módulos nuevos (como el gestor de hábitos) no me obligue a tocar lo que ya funciona.

# ¿Por qué?
La razón principal es el modo enfoque. Para bloquear aplicaciones en Windows necesito usar APIs del sistema operativo como 
EnumWindows o manejar procesos con System.Diagnostics.Process. Eso es C# puro, sin frameworks intermedios que lo compliquen. Si usara una app web tendría que recurrir a extensiones del navegador o herramientas externas, y ya no sería una sola app cohesionada sino un parche encima de otro parche.

La segunda razón es más honesta: WPF es lo que puedo aprender durante este semestre sin sacrificar la calidad de la arquitectura. Prefiero hacer algo bien con tecnología que conozco a hacer algo a medias con algo nuevo.
Alternativas consideradas

# Diagramas
 <img width="945" height="792" alt="image" src="https://github.com/user-attachments/assets/16627abc-2e24-494c-882d-1cafd2701fd7" />

 <img width="1016" height="704" alt="image" src="https://github.com/user-attachments/assets/eaeb5cdd-df9f-4def-b291-2745ef3b7fbb" />

 <img width="748" height="557" alt="image" src="https://github.com/user-attachments/assets/6c0007d0-3a10-40be-bbc2-350b1164132d" />

 <img width="939" height="419" alt="image" src="https://github.com/user-attachments/assets/6d236595-f88e-4a09-b375-bbdcc95c6cb2" />

 <img width="874" height="840" alt="image" src="https://github.com/user-attachments/assets/ccbaaab9-5690-4ea5-962c-d6a5cf624a1d" />
