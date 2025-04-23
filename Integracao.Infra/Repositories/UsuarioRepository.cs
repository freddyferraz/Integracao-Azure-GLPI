using Integracao.Domain.Abstractions.Repositories;
using Integracao.Domain.Entities;
using Integracao.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Integracao.Infra.Repositories;
public sealed class UsuarioRepository(SqlServerDbContext sqlServerDbContext) : IUsuarioRepository
{
    public async ValueTask<Usuario> GetUsuarioById(long id)
    {
        var data = await sqlServerDbContext.Usuarios.Where(t => t.AcodUsuario == id).FirstOrDefaultAsync();

        return data!;
    }
    public async ValueTask<Usuario> GetUsuarioByEmail(string email)
    {
        var data = await sqlServerDbContext.Usuarios.Where(t => t.AdesEmail == email).FirstOrDefaultAsync();

        return data!;
    }
}
