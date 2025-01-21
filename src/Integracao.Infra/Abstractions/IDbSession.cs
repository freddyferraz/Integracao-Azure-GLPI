using System.Data;

namespace Integracao.Infra.Abstractions;
public interface IDbSession : IDisposable
{
    IDbConnection Connection { get; }
}