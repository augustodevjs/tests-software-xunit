﻿using MediatR;
using NerdStore.Core.Data;
using NerdStore.Core.Messages;
using Microsoft.EntityFrameworkCore;
using NerdStore.Vendas.Domain.Entities;
using NerdStore.Vendas.Data.Extensions;

namespace NerdStore.Vendas.Data.Context;

public class VendasContext : DbContext, IUnityOfWork
{
    private readonly IMediator _mediatorHandler;


    public VendasContext(DbContextOptions<VendasContext> options, IMediator mediatorHandler)
        : base(options)
    {
        _mediatorHandler = mediatorHandler;
    }

    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoItem> PedidoItems { get; set; }
    public DbSet<Voucher> Vouchers { get; set; }


    public async Task<bool> Commit()
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("DataCadastro").CurrentValue = DateTime.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property("DataCadastro").IsModified = false;
            }
        }

        var sucesso = await base.SaveChangesAsync() > 0;
        if (sucesso) await _mediatorHandler.PublicarEventos(this);
        return sucesso;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.Relational().ColumnType = "varchar(100)";

        modelBuilder.Ignore<Event>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VendasContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        modelBuilder.HasSequence<int>("MinhaSequencia").StartsAt(1000).IncrementsBy(1);
        base.OnModelCreating(modelBuilder);
    }
}