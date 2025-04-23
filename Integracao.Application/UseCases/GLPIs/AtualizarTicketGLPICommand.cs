using Integracao.Application.Abstractions;

namespace Integracao.Application.UseCases.GLPIs;

public sealed record AtualizarTicketGLPICommand(string token,long acodTicketAzure, int status,string email, string content) : ICommand;
