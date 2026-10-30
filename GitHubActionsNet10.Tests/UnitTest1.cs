using GitHubActionsNet10.Api.Services;

namespace GitHubActionsNet10.Tests;

public class CalculadoraServiceTests
{
    [Fact]
    public void Sumar_DosNumeros_RetornaResultadoCorrecto()
    {
        // Arrange
        var calculadora = new CalculadoraService();

        // Act
        var resultado = calculadora.Sumar(10, 20);

        // Assert
        Assert.Equal(30, resultado);
    }
}