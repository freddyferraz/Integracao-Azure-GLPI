using Integracao.Infra.Abstractions;
using IntegracaoGLPI_DEvOps.Service.Interfaces;

namespace IntegracaoGLPI_DEvOps.Service.Services;

public class UsuariosServices(IUsuarioRepository usuarioRepository, CancellationToken cancellationToken) : IUsuariosServices
{
    public async ValueTask<string> RetornaEmailUsuario(long id)
    {
        var (dados, total) = await usuarioRepository.RetornaUsuarioById(id, cancellationToken);

        var usuarioResult = dados.Select(x => x.AdesEmail).FirstOrDefault();

        return usuarioResult;
    }
    

}

