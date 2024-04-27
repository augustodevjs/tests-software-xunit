using Feature.Core;

namespace Feature.Clientes;

public interface IClienteRepository : IRepository<Cliente>
{
    Cliente ObterPorEmail(string email);
}