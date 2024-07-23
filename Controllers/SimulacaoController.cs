using Microsoft.AspNetCore.Mvc;
using SimulacaoEmprestimo.Models;
using SimulacaoEmprestimo.Services;
using SimulacaoEmprestimo.Repositories;

namespace SimulacaoEmprestimo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SimulacaoController : ControllerBase
    {
        private readonly ISimulacaoService _simulacaoService;
        private readonly ISimulacaoRepository _simulacaoRepository;

        public SimulacaoController(ISimulacaoService simulacaoService, ISimulacaoRepository simulacaoRepository)
        {
            _simulacaoService = simulacaoService;
            _simulacaoRepository = simulacaoRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CalcularSimulacao([FromBody] SimulacaoRequest request)
        {
            var simulacao = _simulacaoService.CalcularSimulacao(request, request.SistemaAmortizacao);
            await _simulacaoRepository.AddAsync(simulacao);
            return Ok(simulacao);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSimulacaoById(Guid id)
        {
            var simulacao = await _simulacaoRepository.GetByIdAsync(id);
            if (simulacao == null)
            {
                return NotFound();
            }
            return Ok(simulacao);
        }
    }
}
