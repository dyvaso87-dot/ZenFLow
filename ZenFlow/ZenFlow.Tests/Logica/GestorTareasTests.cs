using Moq;
using Xunit;
using ZenFlow.Logica;
using ZenFlow.Modelos;
using ZenFlow.Repositorios;

namespace ZenFlow.Tests.Logica
{
    public class GestorTareasTests
    {
        [Fact]
        public void AgregarTarea_TituloVacio_DebeLanzarArgumentException()
        {
            // Arrange
            var repositorioMock = new Mock<ITareaRepo>();
            var gestor = new GestorTareas(repositorioMock.Object);

            string titulo = "";
            DateTime fechaLimite = DateTime.Today.AddDays(1);

            // Act
            Action accion = () =>
                gestor.AgregarTarea(titulo, fechaLimite);

            // Assert
            var excepcion = Assert.Throws<ArgumentException>(accion);

            Assert.Equal(
                "El título no puede estar vacío.",
                excepcion.Message
            );

            repositorioMock.Verify(
                repositorio => repositorio.Guardar(It.IsAny<Tarea>()),
                Times.Never
            );
        }

        [Fact]
        public void AgregarTarea_DatosValidos_DebeGuardarTareaPendiente()
        {
            // Arrange
            var repositorioMock = new Mock<ITareaRepo>();
            var gestor = new GestorTareas(repositorioMock.Object);

            string titulo = "Terminar proyecto";
            DateTime fechaLimite = new DateTime(2026, 8, 10);

            // Act
            gestor.AgregarTarea(titulo, fechaLimite);

            // Assert
            repositorioMock.Verify(
                repositorio => repositorio.Guardar(
                    It.Is<Tarea>(tarea =>
                        tarea.Titulo == titulo &&
                        tarea.FechaLimite == fechaLimite &&
                        tarea.Completada == false
                    )
                ),
                Times.Once
            );
        }

        [Fact]
        public void ObtenerTareasPendientes_DebeRetornarSoloTareasNoCompletadas()
        {
            // Arrange
            var repositorioMock = new Mock<ITareaRepo>();

            var tareas = new List<Tarea>
            {
                new Tarea
                {
                    Id = 1,
                    Titulo = "Tarea pendiente",
                    Completada = false
                },
                new Tarea
                {
                    Id = 2,
                    Titulo = "Tarea terminada",
                    Completada = true
                },
                new Tarea
                {
                    Id = 3,
                    Titulo = "Otra tarea pendiente",
                    Completada = false
                }
            };

            repositorioMock
                .Setup(repositorio => repositorio.ObtenerTodas())
                .Returns(tareas);

            var gestor = new GestorTareas(repositorioMock.Object);

            // Act
            var resultado = gestor.ObtenerTareasPendientes();

            // Assert
            Assert.Equal(2, resultado.Count);
            Assert.All(resultado, tarea => Assert.False(tarea.Completada));

            Assert.Contains(resultado, tarea => tarea.Id == 1);
            Assert.Contains(resultado, tarea => tarea.Id == 3);
            Assert.DoesNotContain(resultado, tarea => tarea.Id == 2);
        }

        [Fact]
        public void CompletarTarea_TareaExistente_DebeMarcarlaComoCompletada()
        {
            // Arrange
            var repositorioMock = new Mock<ITareaRepo>();

            var tarea = new Tarea
            {
                Id = 5,
                Titulo = "Preparar exposición",
                Completada = false
            };

            repositorioMock
                .Setup(repositorio => repositorio.ObtenerTodas())
                .Returns(new List<Tarea> { tarea });

            var gestor = new GestorTareas(repositorioMock.Object);

            // Act
            gestor.CompletarTarea(5);

            // Assert
            Assert.True(tarea.Completada);

            repositorioMock.Verify(
                repositorio => repositorio.Eliminar(5),
                Times.Once
            );

            repositorioMock.Verify(
                repositorio => repositorio.Guardar(
                    It.Is<Tarea>(t =>
                        t.Id == 5 &&
                        t.Completada
                    )
                ),
                Times.Once
            );
        }

        [Fact]
        public void CompletarTarea_TareaNoExistente_NoDebeModificarRepositorio()
        {
            // Arrange
            var repositorioMock = new Mock<ITareaRepo>();

            repositorioMock
                .Setup(repositorio => repositorio.ObtenerTodas())
                .Returns(new List<Tarea>());

            var gestor = new GestorTareas(repositorioMock.Object);

            // Act
            gestor.CompletarTarea(99);

            // Assert
            repositorioMock.Verify(
                repositorio => repositorio.Eliminar(It.IsAny<int>()),
                Times.Never
            );

            repositorioMock.Verify(
                repositorio => repositorio.Guardar(It.IsAny<Tarea>()),
                Times.Never
            );
        }
    }
}