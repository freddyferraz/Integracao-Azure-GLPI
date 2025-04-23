using Dapper;
using Integracao.Infra.Abstractions;
using Integracao.Infra.Entities;

namespace Integracao.Infra.Repositories;
public sealed class TicketsRepository(IntegracaoContext dbContext, IDbSession dbSession) : ITicketsRepository
{
    public async ValueTask<Tickets> RetornaTicketsById(long id)
    {
        var sql = @"SELECT ACOD_TICKETS AS acodTickets, ACOD_TICKETS_GLPI as acodTicketsGlpi, ACOD_TICKETS_DEVOPS as acodTicketsDevops, ACOD_STATUS as acodStatus, ADAT_ALTERACAO as adatAlteracao, ACOD_USUARIO as acodUsuario
                        FROM TICKETS WHERE ACOD_TICKETS = @CodigoTickets ";

        var query = await dbSession.Connection.QueryFirstOrDefaultAsync<Tickets>(sql, new { CodigoTickets = id });
    
        return query;
    }
    public ValueTask InserirTicket(Tickets ticket)
    {
        dbContext.Add(ticket);

        return ValueTask.CompletedTask;
    }
    public ValueTask AtualizarTicket(Tickets tickets)
    {
        dbContext.Update(tickets);

        return ValueTask.CompletedTask;
    }
}