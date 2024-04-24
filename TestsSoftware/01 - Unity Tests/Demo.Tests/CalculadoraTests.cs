using Xunit;

namespace Demo.Tests;

public class CalculadoraTests
{
    [Fact]
    public void Calculadora_Somar_RetornarValorSomado()
    {
        // Arrange
        var calculadora = new Calculadora();

        // Act
        var resultado = calculadora.Somar(4, 2);

        // Assert
        Assert.Equal(6, resultado);
    }

    [Theory]
    [InlineData(3, 0, 3)]
    [InlineData(3, 5, 8)]
    [InlineData(2, 5, 7)]
    [InlineData(1, 5, 6)]
    [InlineData(3, 2, 5)]
    [InlineData(3, 15, 18)]
    public void Calculadora_Somar_RetornarSomaValoresCorretos(double v1, double v2, double total)
    {
        // Arrange
        var calculadora = new Calculadora();

        // Act
        var resultado = calculadora.Somar(v1, v2);

        // Assert
        Assert.Equal(total, resultado);
    }
}
