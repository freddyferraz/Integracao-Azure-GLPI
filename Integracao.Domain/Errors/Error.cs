namespace Integracao.Domain.Errors;

public sealed record Error(string Code, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error ErroIniciarSessaoGLPI = new ("Error.ErroIniciarSessaoGLPI", "Erro ao Iniciar Sessão no GLPI");
}