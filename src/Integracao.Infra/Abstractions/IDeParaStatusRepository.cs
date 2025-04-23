using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integracao.Infra.Entities;

namespace Integracao.Infra.Abstractions
{
    public interface IDeParaStatusRepository
    {
        ValueTask<TDEPARAStatus> RetornaTDeParaStatusById(long? idStatusDevOps = null, long? idStatusGlpi = null);
    }
}
