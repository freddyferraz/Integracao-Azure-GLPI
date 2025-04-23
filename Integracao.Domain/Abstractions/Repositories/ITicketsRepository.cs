using Integracao.Domain.Entities;

namespace Integracao.Domain.Abstractions.Repositories;

public interface ITicketsRepository
{
    ValueTask<Ticket> InsertTicketAsync(Ticket ticket);
    ValueTask<Ticket> UpdateTicket(Ticket ticket);
    ValueTask<Ticket> GetTicketByIdAzureAsync(long acodTicketAzure, CancellationToken cancellationToken);
    ValueTask<Ticket> GetTicketByIdGlpiAsync(long acodTicketGlpi, CancellationToken cancellationToken);
}
