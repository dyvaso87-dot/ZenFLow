using Moq;
using Xunit;
using ZenFlow.Logica;
using ZenFlow.Modelos;
using ZenFlow.Repositorios;

namespace ZenFlow.Tests.Logica
{
    public class GestorAppsTests
    {
        [Fact]
        public void AgregarApp_NombreVacio_DebeLanzarArgumentException()
        {
            // Arrange
            var repositorioMock = new Mock<IAppBloqueadaRepo>();
            var gestor = new GestorApps(repositorioMock.Object);

            string nombre = "";
            string proceso = "Spotify";

            // Act
            Action accion = () =>
                gestor.AgregarApp(nombre, proceso);

            // Assert
            var excepcion = Assert.Throws<ArgumentException>(accion);

            Assert.Equal(
                "El nombre y el proceso son requeridos.",
                excepcion.Message
            );

            repositorioMock.Verify(
                repositorio =>
                    repositorio.Guardar(It.IsAny<AppBloqueada>()),
                Times.Never
            );
        }

        [Fact]
        public void AgregarApp_ProcesoVacio_DebeLanzarArgumentException()
        {
            // Arrange
            var repositorioMock = new Mock<IAppBloqueadaRepo>();
            var gestor = new GestorApps(repositorioMock.Object);

            string nombre = "Spotify";
            string proceso = "";

            // Act
            Action accion = () =>
                gestor.AgregarApp(nombre, proceso);

            // Assert
            var excepcion = Assert.Throws<ArgumentException>(accion);

            Assert.Equal(
                "El nombre y el proceso son requeridos.",
                excepcion.Message
            );

            repositorioMock.Verify(
                repositorio =>
                    repositorio.Guardar(It.IsAny<AppBloqueada>()),
                Times.Never
            );
        }

        [Fact]
        public void AgregarApp_DatosValidos_DebeGuardarAppActiva()
        {
            // Arrange
            var repositorioMock = new Mock<IAppBloqueadaRepo>();
            var gestor = new GestorApps(repositorioMock.Object);

            string nombre = "Spotify";
            string proceso = "  Spotify  ";

            // Act
            gestor.AgregarApp(nombre, proceso);

            // Assert
            repositorioMock.Verify(
                repositorio => repositorio.Guardar(
                    It.Is<AppBloqueada>(app =>
                        app.Nombre == nombre &&
                        app.Proceso == "Spotify" &&
                        app.Activa
                    )
                ),
                Times.Once
            );
        }

        [Fact]
        public void ObtenerProcesosActivos_DebeRetornarSoloProcesosActivos()
        {
            // Arrange
            var repositorioMock = new Mock<IAppBloqueadaRepo>();

            var aplicaciones = new List<AppBloqueada>
            {
                new AppBloqueada
                {
                    Id = 1,
                    Nombre = "Spotify",
                    Proceso = "Spotify",
                    Activa = true
                },
                new AppBloqueada
                {
                    Id = 2,
                    Nombre = "Discord",
                    Proceso = "Discord",
                    Activa = false
                },
                new AppBloqueada
                {
                    Id = 3,
                    Nombre = "Steam",
                    Proceso = "Steam",
                    Activa = true
                }
            };

            repositorioMock
                .Setup(repositorio => repositorio.ObtenerTodas())
                .Returns(aplicaciones);

            var gestor = new GestorApps(repositorioMock.Object);

            // Act
            var resultado = gestor.ObtenerProcesosActivos();

            // Assert
            Assert.Equal(2, resultado.Count);
            Assert.Contains("Spotify", resultado);
            Assert.Contains("Steam", resultado);
            Assert.DoesNotContain("Discord", resultado);
        }

        [Fact]
        public void ToggleActivar_AppExistente_DebeCambiarEstadoActivo()
        {
            // Arrange
            var repositorioMock = new Mock<IAppBloqueadaRepo>();

            var aplicacion = new AppBloqueada
            {
                Id = 10,
                Nombre = "YouTube",
                Proceso = "chrome",
                Activa = true
            };

            repositorioMock
                .Setup(repositorio => repositorio.ObtenerTodas())
                .Returns(new List<AppBloqueada> { aplicacion });

            var gestor = new GestorApps(repositorioMock.Object);

            // Act
            gestor.ToggleActivar(10);

            // Assert
            Assert.False(aplicacion.Activa);

            repositorioMock.Verify(
                repositorio => repositorio.Guardar(
                    It.Is<AppBloqueada>(app =>
                        app.Id == 10 &&
                        app.Activa == false
                    )
                ),
                Times.Once
            );
        }

        [Fact]
        public void ToggleActivar_AppNoExistente_NoDebeGuardarCambios()
        {
            // Arrange
            var repositorioMock = new Mock<IAppBloqueadaRepo>();

            repositorioMock
                .Setup(repositorio => repositorio.ObtenerTodas())
                .Returns(new List<AppBloqueada>());

            var gestor = new GestorApps(repositorioMock.Object);

            // Act
            gestor.ToggleActivar(99);

            // Assert
            repositorioMock.Verify(
                repositorio =>
                    repositorio.Guardar(It.IsAny<AppBloqueada>()),
                Times.Never
            );
        }

        [Fact]
        public void Eliminar_DebeEnviarIdAlRepositorio()
        {
            // Arrange
            var repositorioMock = new Mock<IAppBloqueadaRepo>();
            var gestor = new GestorApps(repositorioMock.Object);

            int id = 7;

            // Act
            gestor.Eliminar(id);

            // Assert
            repositorioMock.Verify(
                repositorio => repositorio.Eliminar(id),
                Times.Once
            );
        }
    }
}