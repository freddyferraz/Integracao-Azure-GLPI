namespace Integracao.Application.UseCases.GLPIs;

public sealed record AtualizarTicketGLPIRequest(string token, int status, string email, string content);