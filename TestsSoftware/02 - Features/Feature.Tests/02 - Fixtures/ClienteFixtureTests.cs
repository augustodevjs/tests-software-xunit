using Xunit;

namespace Feature.Tests._02___Fixtures;

[Collection(nameof(ClienteCollection))]
public class ClienteFixtureTests
{
    private readonly Fixture _fixture;

    public ClienteFixtureTests(Fixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Novo Cliente Válido")]
    [Trait("Categoria", "Cliente Fixture Testes")]
    public void Cliente_NovoCliente_DeveEstarValido()
    {
        // Arrange
        var cliente = _fixture.GerarClienteValido();

        // Act
        var result = cliente.EhValido();

        // Assert
        Assert.True(result);
        Assert.Empty(cliente.ValidationResult.Errors);
    }

    [Fact(DisplayName = "Novo Cliente Inválido")]
    [Trait("Categoria", "Cliente Fixture Testes")]
    public void Cliente_NovoCliente_DeveEstarInvalido()
    {
        // Arrange
        var cliente = _fixture.GerarClienteInvalido();

        // Act
        var result = cliente.EhValido();

        // Assert
        Assert.False(result);
        Assert.NotEmpty(cliente.ValidationResult.Errors);
    }
}
