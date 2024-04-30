using NerdStore.Core.Data;
using NerdStore.Vendas.Domain.Entities;

namespace NerdStore.Vendas.Domain.Interfaces.Repositories;

public interface IPedidoRepository : IRepository<Pedido>
{
    Task<IEnumerable<Pedido>> ObterListaPorClienteId(Guid clienteId);
    Task<Pedido> ObterPedidoRascunhoPorClienteId(Guid clienteId);
    void Adicionar(Pedido pedido);
    void Atualizar(Pedido pedido);

    Task<PedidoItem> ObterItemPorPedido(Guid pedidoId, Guid produtoId);
    void AdicionarItem(PedidoItem pedidoItem);
    void AtualizarItem(PedidoItem pedidoItem);
    void RemoverItem(PedidoItem pedidoItem);

    Task<Voucher> ObterVoucherPorCodigo(string codigo);
}