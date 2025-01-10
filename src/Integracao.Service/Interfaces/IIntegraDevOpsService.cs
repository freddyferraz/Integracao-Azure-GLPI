using IntegracaoGLPI_DevOps.Core.Structs;
using IntegracaoGLPI_DEvOps.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegracaoGLPI_DEvOps.Service.Interfaces;
public interface IIntegraDevOpsService
{
    Task<Optional<RetornoDevOpsDTO>> UpdateDevOps(CardDevOpsDTO updateDevOps);
}
