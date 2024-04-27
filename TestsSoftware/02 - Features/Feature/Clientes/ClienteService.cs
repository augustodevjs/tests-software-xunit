using MediatR;

namespace Feature.Clientes;

public class ClienteService : IClienteService
{
    private readonly IMediator _mediator;
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(
        IMediator mediator,
        IClienteRepository clienteRepository
    )
    {
        _mediator = mediator;
        _clienteRepository = clienteRepository;
    }

    public IEnumerable<Cliente> ObterTodosAtivos()
    {
        return _clienteRepository.ObterTodos().Where(c => c.Ativo);
    }

    public void Adicionar(Cliente cliente)
    {
        if (!cliente.EhValido()) return;

        _clienteRepository.Adicionar(cliente);

        _mediator.Publish(new ClienteEmailNotification("admin@me.com", cliente.Email, "Olá", "Bem vindo!"));
    }

    public void Atualizar(Cliente cliente)
    {
        if (!cliente.EhValido())
            return;

        _clienteRepository.Atualizar(cliente);

        _mediator.Publish(new ClienteEmailNotification("admin@me.com", cliente.Email, "Mudanças", "Dê uma olhada!"));
    }

    public void Inativar(Cliente cliente)
    {
        if (!cliente.EhValido())
            return;

        cliente.Inativar();
        _clienteRepository.Atualizar(cliente);

        _mediator.Publish(new ClienteEmailNotification("admin@me.com", cliente.Email, "Até breve", "Até mais tarde!"));
    }

    public void Remover(Cliente cliente)
    {
        _clienteRepository.Remover(cliente.Id);

        _mediator.Publish(new ClienteEmailNotification("admin@me.com", cliente.Email, "Adeus", "Tenha uma boa jornada!"));
    }

    public void Dispose()
    {
        _clienteRepository.Dispose();
    }
}