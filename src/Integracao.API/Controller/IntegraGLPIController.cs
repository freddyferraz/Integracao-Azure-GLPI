using AutoMapper;
using IntegracaoGLPI_DevOps.Service.Interfaces;
using IntegracaoGLPI_DevOps.ViewModel;
using IntegracaoGLPI_DEvOps.Service.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntegracaoGLPI_DevOps.Controller
{
    [ApiController]
    public class IntegraGLPIController :ControllerBase
    {


        IIntegraGLPIService _integraGlpi;
        IMapper _mapper;

        public IntegraGLPIController(
            IIntegraGLPIService integraGlpi, 
            IMapper mapper)
        {
            _integraGlpi = integraGlpi;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/api/v1/atualiza-glpi")]
        public async Task<IActionResult> UpdateGLPI(string authToken, [FromBody]TicketGLPIViewModel updatedGLPI)
        {
            var sessionToken = await _integraGlpi.InitSession(authToken);



            var updatedGLPIDTO = _mapper.Map<TicketGLPIDTO>(updatedGLPI);

            if (sessionToken.Value.session_token == null)
            {
                return Ok("Erro ao conectar com GLPI");
            }


            var retorno = await _integraGlpi.UpdateGLPI(authToken, updatedGLPIDTO);

            var finalizacao = await _integraGlpi.KillSession(authToken);



            return Ok(new ResultViewModel
            {
                Message = "Integração com GLPI realizada com sucesso!",
                Success = true,
                Data = updatedGLPIDTO
            });
        }

    }
}
