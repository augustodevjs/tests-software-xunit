using Microsoft.EntityFrameworkCore;
using NerdStore.Vendas.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NerdStore.Vendas.Data.Mappings;

public class PedidoItemMapping : IEntityTypeConfiguration<PedidoItem>
{
    public void Configure(EntityTypeBuilder<PedidoItem> builder)
    {
        builder.HasKey(c => c.Id);


        builder.Property(c => c.ProdutoNome)
            .IsRequired()
            .HasColumnType("varchar(250)");

        builder.HasOne(c => c.Pedido)
            .WithMany(c => c.PedidoItens);

        builder.ToTable("PedidoItems");
    }
}