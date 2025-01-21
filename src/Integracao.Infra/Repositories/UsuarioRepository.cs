using Dapper;
using Integracao.Infra.Abstractions;
using Integracao.Infra.Entities;

namespace Integracao.Infra.Repositories;
internal sealed class UsuarioRepository(IntegracaoContext dbContext, IDbSession dbSession) : IUsuarioRepository
{
    public async ValueTask<(IEnumerable<Usuario> data, int total)> RetornaUsuarioById(long id, CancellationToken cancellationToken)
    {
        var sql = @"SELECT ACOD_USUARIO AS acodUsuario, ADES_EMAIL as adesEmail, ADES_USUARIO as adesUSuario
                        FROM TUSUARIO WHERE ACOD_USUARIO = @CodigoUsuario ";

        var query = dbSession.Connection.Query<Usuario>(sql, new { CodigoUsuario = id });

        var totalCount = query.Count();

        return (query, totalCount);

    }
}
