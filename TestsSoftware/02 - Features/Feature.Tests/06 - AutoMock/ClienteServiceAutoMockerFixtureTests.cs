using Moq;
using Xunit;
using MediatR;
using Feature.Clientes;

namespace Feature.Tests._06___AutoMock;

[Collection(nameof(ClienteAutoMockerCollection))]
public class ClienteServiceAutoMockerFixtureTests
{
    private readonly ClienteService _clienteService;
    private readonly ClienteTestsAutoMockerFixture _clienteTestsAutoMockerFixture;

    public ClienteServiceAutoMockerFixtureTests(ClienteTestsAutoMockerFixture clienteTestsAutoMockerFixture)
    {
        _clienteTestsAutoMockerFixture = clienteTestsAutoMockerFixture;
        _clienteService = _clienteTestsAutoMockerFixture.ObterClienteService();
    }

    [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
    [Trait("Categoria", "Cliente Service AutoMockFixture Tests")]
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
    [Trait("Categoria", "Cliente Service AutoMockFixture Tests")]
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
    [Trait("Categoria", "Cliente Service AutoMockFixture Tests")]
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

        Assert.True(clientes.Any());
        Assert.False(clientes.Count(c => !c.Ativo) > 0);
    }
}