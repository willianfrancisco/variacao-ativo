using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VariacaoAtivo.Application.Interfaces;

namespace VariacaoAtivo.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtivoController : ControllerBase
    {
        private readonly IAtivoService _ativoService;

        public AtivoController(IAtivoService ativoService)
        {
            _ativoService = ativoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAtivo([FromQuery]string? ativo  = "PETR4.SA")
        {
            var ativos = await _ativoService.PesquisarAtivos(ativo);
            return Ok(ativos);
        }
    }
}