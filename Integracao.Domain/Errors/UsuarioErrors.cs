namespace Integracao.Domain.Errors;
public static class UsuarioErrors
{
    public static readonly Error IdUsuarioNaoEncontrado = new("Usuario.IdUsuarioNaoEncontrado", "O Id do Usuário não foi encontrado");
    public static readonly Error EmailUsuarioNaoEncontrado = new("Usuario.EmailUsuarioNaoEncontrado", "O Email do Usuário não foi encontrado");
    public static readonly Error EmailUsuarioNaoPodeSerNulo = new("Usuario.EmailUsuarioNaoPodeSerNulo", "O Email do Usuário não pode ser nulo");
    public static readonly Error IdUsuarioNaoPodeSerNulo = new("Usuario.IdUsuarioNaoPodeSerNulo", "O Id do Usuário não pode ser nulo");

}
