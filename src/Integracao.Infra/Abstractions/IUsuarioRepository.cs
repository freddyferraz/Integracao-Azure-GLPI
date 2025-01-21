using Integracao.Infra.Entities;

namespace Integracao.Infra.Abstractions;

public interface IUsuarioRepository
{
    ValueTask<(IEnumerable<Usuario> data, int total)> RetornaUsuarioById(long id, CancellationToken cancellationToken);
}