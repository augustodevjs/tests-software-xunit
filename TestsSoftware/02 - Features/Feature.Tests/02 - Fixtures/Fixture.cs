using Xunit;
using Feature.Clientes;

namespace Feature.Tests._02___Fixtures;

[CollectionDefinition(nameof(ClienteCollection))]
public class ClienteCollection : ICollectionFixture<Fixture>
{

}

public class Fixture : IDisposable
{
    public Cliente GerarClienteValido()
    {
        var cliente = new Cliente(
            Guid.NewGuid(),
            "Eduardo",
            "Pires",
            DateTime.Now.AddYears(-30),
            "edu@edu.com",
            true,
            DateTime.Now
        );

        return cliente;
    }

    public Cliente GerarClienteInvalido()
    {
        var cliente = new Cliente(
            Guid.NewGuid(),
            "",
            "",
            DateTime.Now,
            "edu@edu.com",
            true,
            DateTime.Now
        );

        return cliente;
    }

    public void Dispose()
    {
    }
}
