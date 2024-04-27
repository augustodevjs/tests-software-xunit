using Xunit;

namespace Feature.Tests._04___Dados_Humanos;

[Collection(nameof(ClienteBogusCollection))]
public class ClienteBogusTests
{
    private readonly ClienteBogusFixture _clienteBogusFixture;

    public ClienteBogusTests(ClienteBogusFixture clienteBogusFixture)
    {
        _clienteBogusFixture = clienteBogusFixture;
    }

    [Fact(DisplayName = "Novo Cliente Válido")]
    [Trait("Categoria", "Cliente Bogus Testes")]
    public void Cliente_NovoCliente_DeveEstarValido()
    {
        // Arrange
        var cliente = _clienteBogusFixture.GerarClienteValido();

        // Act
        var result = cliente.EhValido();

        // Assert
        Assert.True(result);
        Assert.Equal(0, cliente.ValidationResult.Errors.Count);
    }
}