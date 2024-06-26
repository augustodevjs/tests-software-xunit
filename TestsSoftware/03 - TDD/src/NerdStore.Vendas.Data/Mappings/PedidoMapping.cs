﻿using Microsoft.EntityFrameworkCore;
using NerdStore.Vendas.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NerdStore.Vendas.Data.Mappings;

public class PedidoMapping : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Codigo)
            .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");

        builder.HasMany(c => c.PedidoItens)
            .WithOne(c => c.Pedido)
            .HasForeignKey(c => c.PedidoId);

        builder.ToTable("Pedidos");
    }
}