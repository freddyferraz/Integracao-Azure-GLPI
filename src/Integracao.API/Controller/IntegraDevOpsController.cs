using AutoMapper;
using IntegracaoGLPI_DevOps.Service.Interfaces;
using IntegracaoGLPI_DevOps.ViewModel;
using IntegracaoGLPI_DEvOps.Service.DTO;
using IntegracaoGLPI_DEvOps.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntegracaoGLPI_DevOps.Controller;

[ApiController]
public class IntegraDevOpsController : ControllerBase
{
    IMapper _mapper;
    IIntegraDevOpsService _integraDevOpsService;

    public IntegraDevOpsController(
        IIntegraDevOpsService integraDevOpsService,
        IMapper mapper)
    {
        _mapper = mapper;
        _integraDevOpsService = integraDevOpsService;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("/api/v1/atualiza-dev-ops")]
    public async Task<IActionResult> UpdateDevOps([FromBody]CardDevOpsViewModel updateDevOps)
    {

        var updateDevOpsDTO = _mapper.Map<CardDevOpsDTO>(updateDevOps);

        var retorno = await _integraDevOpsService.UpdateDevOps(updateDevOpsDTO);

        return Ok(new ResultViewModel
        {
            Message = "Integração com Dev-Ops realizada com sucesso",
            Success = true,
            Data = retorno
        });
    }
}
