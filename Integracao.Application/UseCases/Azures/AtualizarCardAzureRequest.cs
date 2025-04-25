namespace Integracao.Application.UseCases.Azures;
public sealed record AtualizarCardAzureRequest(string? Area, string? Title, string? Description, long Requerente, long? Observador, long Status,
    long? ResponsavelAtendimento, string? TipoChamado, string? CaterogiraChamado, string? Prioridade, string? Impacto, string? Urgencia, string? Localizacao,
    string? Data, long AcodTicketAzure);
