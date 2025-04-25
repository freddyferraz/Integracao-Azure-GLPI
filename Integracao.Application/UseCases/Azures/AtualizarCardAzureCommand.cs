using Integracao.Application.Abstractions;

namespace Integracao.Application.UseCases.Azures;
public sealed record AtualizarCardAzureCommand( string? Area, string? Title, string? Description, long Requerente, long? Observador, long Status,
    long? ResponsavelAtendimento, string? TipoChamado, string? CaterogiraChamado, string? Prioridade, string? Impacto, string? Urgencia, string? Localizacao,
    string? Data, long AcodTicketGLPI) : ICommand;