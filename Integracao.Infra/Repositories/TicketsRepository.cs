using Integracao.Domain.Abstractions.Repositories;
using Integracao.Domain.Entities;
using Integracao.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Integracao.Infra.Repositories;
public sealed class TicketsRepository(SqlServerDbContext sqlServerDbContext) : ITicketsRepository
{
    public async ValueTask<Ticket> InsertTicketAsync(Ticket ticket)
    {
        await sqlServerDbContext.AddAsync(ticket);

        return ticket;

    }
    public async ValueTask<Ticket> UpdateTicket(Ticket ticket)
    {
        await ValueTask.FromResult(sqlServerDbContext.Tickets.Update(ticket));

        return ticket;
    }
    public async ValueTask<Ticket> GetTicketByIdAzureAsync(long acodTicketAzure, CancellationToken cancellationToken)
    {
        var data = await sqlServerDbContext.Tickets.Where(t => t.AcodTicketsDevops == acodTicketAzure).FirstOrDefaultAsync();

        return data!;
    }
    public async ValueTask<Ticket> GetTicketByIdGlpiAsync(long acodTicketGlpi, CancellationToken cancellationToken)
    {
        var data = await sqlServerDbContext.Tickets.Where(t => t.AcodTicketsglpi == acodTicketGlpi).FirstOrDefaultAsync();

        return data!;
    }

}