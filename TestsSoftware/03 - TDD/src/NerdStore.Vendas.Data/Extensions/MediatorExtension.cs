﻿using MediatR;
using NerdStore.Core.DomainObjects;
using NerdStore.Vendas.Data.Context;

namespace NerdStore.Vendas.Data.Extensions;

public static class MediatorExtension
{
    public static async Task PublicarEventos(this IMediator mediator, VendasContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Notificacoes)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.LimparEventos());

        var tasks = domainEvents
            .Select(async (domainEvent) => {
                await mediator.Publish(domainEvent);
            });

        await Task.WhenAll(tasks);
    }
}