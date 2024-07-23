using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SimulacaoEmprestimo.Models;
using SimulacaoEmprestimo.Services;

namespace SimulacaoEmprestimo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ISimulacaoService _simulacaoService;

        public IndexModel(ISimulacaoService simulacaoService)
        {
            _simulacaoService = simulacaoService;
            SimulacaoRequest = new SimulacaoRequest();
        }

        [BindProperty]
        public SimulacaoRequest SimulacaoRequest { get; set; }

        public IActionResult OnPost()
        {
            if (SimulacaoRequest.ValorEmprestimo <= 0 || SimulacaoRequest.Periodo <= 0)
            {
                ModelState.AddModelError(string.Empty, "Todos os campos devem ser preenchidos corretamente.");
                return Page();
            }

            var simulacao = _simulacaoService.CalcularSimulacao(SimulacaoRequest, SimulacaoRequest.SistemaAmortizacao);
            TempData["EncargosMensais"] = JsonConvert.SerializeObject(simulacao.EncargosMensais);
            return RedirectToPage("Resultado", new
            {
                valorEmprestimo = SimulacaoRequest.ValorEmprestimo,
                taxaJuros = SimulacaoRequest.TaxaJuros,
                periodo = SimulacaoRequest.Periodo,
                valorTotalPagar = simulacao.ValorTotalPagar
            });
        }
    }
}
