using NerdStore.Vendas.Application.ViewModels;

namespace NerdStore.Vendas.Application.Interfaces;

public interface IPedidoQueries
{
    Task<CarrinhoViewModel> ObterCarrinhoCliente(Guid clienteId);
    Task<IEnumerable<PedidoViewModel>> ObterPedidosCliente(Guid clienteId);
}