using Moq;
using Xunit;
using ZenFlow.Logica;
using ZenFlow.Repositorios;

namespace ZenFlow.Tests.Logica;

public class GestorHabitosTests
{
    [Fact]
    public void AgregarHabito_NombreVacio_DebeLanzarArgumentException()
    {
        // Arrange
        var repositorioMock = new Mock<IHabitoRepo>();
        var gestor = new GestorHabitos(repositorioMock.Object);

        string nombre = "";

        // Act
        Action accion = () => gestor.AgregarHabito(nombre);

        // Assert
        var excepcion = Assert.Throws<ArgumentException>(accion);

        Assert.Equal(
            "El nombre no puede estar vacío.",
            excepcion.Message
        );
    }

    [Fact]
    public void AgregarHabito_NombreValido_DebeGuardarHabitoConValoresIniciales()
    {
        // Arrange
        var repositorioMock = new Mock<IHabitoRepo>();
        var gestor = new GestorHabitos(repositorioMock.Object);

        string nombre = "Leer 20 minutos";

        // Act
        gestor.AgregarHabito(nombre);

        // Assert
        repositorioMock.Verify(
            repositorio => repositorio.Guardar(
                It.Is<ZenFlow.Modelos.Habito>(habito =>
                    habito.Nombre == nombre &&
                    habito.RachaDias == 0 &&
                    habito.UltimoCumplimiento == DateTime.MinValue
                )
            ),
            Times.Once
        );
    }
}