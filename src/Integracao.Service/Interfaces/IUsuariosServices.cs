namespace IntegracaoGLPI_DEvOps.Service.Interfaces;
public interface IUsuariosServices
{
    ValueTask<string> RetornaEmailUsuario(long id);
}
