using IntegracaoGLPI_DevOps.Core.Structs;
using IntegracaoGLPI_DEvOps.Service.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IntegracaoGLPI_DevOps.Service.Interfaces
{
    public interface IIntegraGLPIService
    {
        Task<Optional<TokenGLPIDTO>> InitSession(string authToken);
        Task<Optional<RetornoTicketDTO>> UpdateGLPI(string authToken, TicketGLPIDTO updatedGLPI);
        Task<Optional<bool>> KillSession(string authToken);
    }
}
