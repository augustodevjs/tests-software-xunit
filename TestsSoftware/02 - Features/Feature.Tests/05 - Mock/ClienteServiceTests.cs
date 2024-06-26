﻿using Moq;
using Xunit;
using MediatR;
using Feature.Clientes;
using Feature.Tests._04___Dados_Humanos;

namespace Feature.Tests._05___Mock;

[Collection(nameof(ClienteBogusCollection))]
public class ClienteServiceTests
{
    private readonly ClienteBogusFixture _clienteBogusFixture;

    public ClienteServiceTests(ClienteBogusFixture clienteBogusFixture)
    {
        _clienteBogusFixture = clienteBogusFixture;
    }

    [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
    [Trait("Categoria", "Cliente Service Mock Tests")]
    public void ClienteService_Adicionar_DeveExecutarComSucesso()
    {
        // Arrange
        var mediatr = new Mock<IMediator>();
        var clienteRepo = new Mock<IClienteRepository>();
        var cliente = _clienteBogusFixture.GerarClienteValido();

        var clienteService = new ClienteService(mediatr.Object, clienteRepo.Object);

        // Act
        clienteService.Adicionar(cliente);

        // Assert
        clienteRepo.Verify(r => r.Adicionar(cliente), Times.Once);
        mediatr.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
    }

    [Fact(DisplayName = "Adicionar Cliente com Falha")]
    [Trait("Categoria", "Cliente Service Mock Tests")]
    public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
    {
        // Arrange
        var mediatr = new Mock<IMediator>();
        var clienteRepo = new Mock<IClienteRepository>();
        var cliente = _clienteBogusFixture.GerarClienteInvalido();
        var clienteService = new ClienteService(mediatr.Object, clienteRepo.Object);

        // Act
        clienteService.Adicionar(cliente);

        // Assert
        Assert.NotEmpty(cliente.ValidationResult.Errors);
        clienteRepo.Verify(r => r.Adicionar(cliente), Times.Never);
        mediatr.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
    }

    [Fact(DisplayName = "Obter Clientes Ativos")]
    [Trait("Categoria", "Cliente Service Mock Tests")]
    public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
    {
        // Arrange
        var mediatr = new Mock<IMediator>();
        var clienteRepo = new Mock<IClienteRepository>();

        clienteRepo.Setup(c => c.ObterTodos())
            .Returns(_clienteBogusFixture.ObterClientesVariados());

        var clienteService = new ClienteService(mediatr.Object, clienteRepo.Object);

        // Act
        var clientes = clienteService.ObterTodosAtivos();

        // Assert
        clienteRepo.Verify(r => r.ObterTodos(), Times.Once);
        Assert.True(clientes.Any());
        Assert.False(clientes.Count(c => !c.Ativo) > 0);
    }

    [Fact(DisplayName = "Atualizar Cliente com Sucesso")]
    [Trait("Categoria", "Cliente Service Mock Tests")]
    public void ClienteService_Atualizar_DeveExecutarComSucesso()
    {
        // Arrange
        var mediatr = new Mock<IMediator>();
        var clienteRepo = new Mock<IClienteRepository>();
        var cliente = _clienteBogusFixture.GerarClienteValido();

        var clienteService = new ClienteService(mediatr.Object, clienteRepo.Object);

        // Act
        clienteService.Atualizar(cliente);

        // Assert
        clienteRepo.Verify(r => r.Atualizar(cliente), Times.Once);
        mediatr.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
    }

    [Fact(DisplayName = "Atualizar Cliente com Falha")]
    [Trait("Categoria", "Cliente Service Mock Tests")]
    public void ClienteService_Atualizar_DeveFalharDevidoClienteInvalido()
    {
        // Arrange
        var mediatr = new Mock<IMediator>();
        var clienteRepo = new Mock<IClienteRepository>();
        var cliente = _clienteBogusFixture.GerarClienteInvalido();

        var clienteService = new ClienteService(mediatr.Object, clienteRepo.Object);

        // Act
        clienteService.Atualizar(cliente);

        // Assert
        clienteRepo.Verify(r => r.Atualizar(cliente), Times.Never);
        mediatr.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
    }

    [Fact(DisplayName = "Inativar Cliente com Sucesso")]
    [Trait("Categoria", "Cliente Service Mock Tests")]
    public void ClienteService_Inativar_DeveExecutarComSucesso()
    {
        // Arrange
        var mediatr = new Mock<IMediator>();
        var clienteRepo = new Mock<IClienteRepository>();
        var cliente = _clienteBogusFixture.GerarClienteValido();

        var clienteService = new ClienteService(mediatr.Object, clienteRepo.Object);

        // Act
        clienteService.Inativar(cliente);

        // Assert
        Assert.False(cliente.Ativo);
        clienteRepo.Verify(r => r.Atualizar(cliente), Times.Once);
        mediatr.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
    }

    [Fact(DisplayName = "Inativar Cliente com Falha")]
    [Trait("Categoria", "Cliente Service Mock Tests")]
    public void ClienteService_Inativar_DeveFalharDevidoClienteInvalido()
    {
        // Arrange
        var mediatr = new Mock<IMediator>();
        var clienteRepo = new Mock<IClienteRepository>();
        var cliente = _clienteBogusFixture.GerarClienteInvalido();

        var clienteService = new ClienteService(mediatr.Object, clienteRepo.Object);

        // Act
        clienteService.Inativar(cliente);

        // Assert
        Assert.True(cliente.Ativo);
        clienteRepo.Verify(r => r.Atualizar(cliente), Times.Never);
        mediatr.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
    }

    [Fact(DisplayName = "Remover Cliente")]
    [Trait("Categoria", "Cliente Service Mock Tests")]
    public void ClienteService_Remover_DeveRemoverCliente()
    {
        // Arrange
        var mediatr = new Mock<IMediator>();
        var clienteRepo = new Mock<IClienteRepository>();
        var cliente = _clienteBogusFixture.GerarClienteInvalido();

        var clienteService = new ClienteService(mediatr.Object, clienteRepo.Object);

        // Act
        clienteService.Remover(cliente);

        // Assert
        clienteRepo.Verify(r => r.Remover(cliente.Id), Times.Once);
        mediatr.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
    }
}