using Integracao.Domain.Entities;

namespace Integracao.Domain.Abstractions.Repositories;

public interface IUsuarioRepository
{
    ValueTask<Usuario> GetUsuarioById(long id);
    ValueTask<Usuario> GetUsuarioByEmail(string email);
}