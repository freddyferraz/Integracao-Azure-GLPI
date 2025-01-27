using Integracao.Infra.Abstractions;
using IntegracaoGLPI_DEvOps.Service.Interfaces;

namespace IntegracaoGLPI_DEvOps.Service.Services;

public class UsuariosServices(IUsuarioRepository usuarioRepository) : IUsuariosServices
{
    public async ValueTask<string> RetornaEmailUsuario(long id)
    {
        var (dados, total) = await usuarioRepository.RetornaUsuarioById(id);

        var usuarioResult = dados.Select(x => x.AdesEmail).FirstOrDefault();

        return usuarioResult;
    }
    

}

