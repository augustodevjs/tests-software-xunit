using Moq;
using Xunit;
using MediatR;
using Feature.Clientes;
using FluentAssertions;
using Feature.Tests._06___AutoMock;

namespace Feature.Tests._07___FluentAssertions;

[Collection(nameof(ClienteAutoMockerCollection))]
public class ClienteServiceFluentAssertionsTests
{
    private readonly ClienteService _clienteService;
    private readonly ClienteTestsAutoMockerFixture _clienteTestsAutoMockerFixture;

    public ClienteServiceFluentAssertionsTests(ClienteTestsAutoMockerFixture clienteTestsAutoMockerFixture)
    {
        _clienteTestsAutoMockerFixture = clienteTestsAutoMockerFixture;
        _clienteService = _clienteTestsAutoMockerFixture.ObterClienteService();
    }

    [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
    [Trait("Categoria", "Cliente Service Fluent Assertions Tests")]
    public void ClienteService_Adicionar_DeveExecutarComSucesso()
    {
        // Arrange
        var cliente = _clienteTestsAutoMockerFixture.GerarClienteValido();

        // Act
        _clienteService.Adicionar(cliente);

        // Assert
        _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>()
            .Verify(r => r.Adicionar(cliente), Times.Once);

        _clienteTestsAutoMockerFixture.Mocker.GetMock<IMediator>()
            .Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
    }

    [Fact(DisplayName = "Adicionar Cliente com Falha")]
    [Trait("Categoria", "Cliente Service Fluent Assertions Tests")]
    public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
    {
        // Arrange
        var cliente = _clienteTestsAutoMockerFixture.GerarClienteInvalido();

        // Act
        _clienteService.Adicionar(cliente);

        // Assert
        _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>()
            .Verify(r => r.Adicionar(cliente), Times.Never);

        _clienteTestsAutoMockerFixture.Mocker.GetMock<IMediator>()
            .Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
    }

    [Fact(DisplayName = "Obter Clientes Ativos")]
    [Trait("Categoria", "Cliente Service Fluent Assertions Tests")]
    public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
    {
        // Arrange
        _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>()
            .Setup(c => c.ObterTodos())
            .Returns(_clienteTestsAutoMockerFixture.ObterClientesVariados());

        // Act
        var clientes = _clienteService.ObterTodosAtivos();

        // Assert
        _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>()
            .Verify(r => r.ObterTodos(), Times.Once);

        clientes.Should().HaveCountGreaterOrEqualTo(1).And.OnlyHaveUniqueItems();
        clientes.Should().NotContain(c => !c.Ativo);
    }
}
