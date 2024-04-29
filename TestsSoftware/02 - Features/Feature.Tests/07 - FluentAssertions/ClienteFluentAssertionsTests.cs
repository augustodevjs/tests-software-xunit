using Xunit;
using FluentAssertions;
using Xunit.Abstractions;
using Feature.Tests._04___Dados_Humanos;

namespace Feature.Tests._07___FluentAssertions;

[Collection(nameof(ClienteBogusCollection))]
public class ClienteFluentAssertionsTests
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly ClienteBogusFixture _clienteBogusFixture;

    public ClienteFluentAssertionsTests(
        ITestOutputHelper outputHelper,
        ClienteBogusFixture clienteBogusFixture
    )
    {
        _outputHelper = outputHelper;
        _clienteBogusFixture = clienteBogusFixture;
    }

    [Fact(DisplayName = "Novo Cliente Válido")]
    [Trait("Categoria", "Cliente Fluent Assertions Testes")]
    public void Cliente_NovoCliente_DeveEstarValido()
    {
        // Arrange
        var cliente = _clienteBogusFixture.GerarClienteValido();

        // Act
        var result = cliente.EhValido();

        // Assert
        result.Should().BeTrue();
        cliente.ValidationResult.Errors.Should().HaveCount(0);
    }

    [Fact(DisplayName = "Novo Cliente Inválido")]
    [Trait("Categoria", "Cliente Fluent Assertions Testes")]
    public void Cliente_NovoCliente_DeveEstarInvalido()
    {
        // Arrange
        var cliente = _clienteBogusFixture.GerarClienteInvalido();

        // Act
        var result = cliente.EhValido();

        // Assert
        result.Should().BeFalse();
        cliente.ValidationResult.Errors.Should().HaveCountGreaterOrEqualTo(1);

        _outputHelper.WriteLine($"Foram encontrados {cliente.ValidationResult.Errors.Count} erros nesta validação");
    }
}