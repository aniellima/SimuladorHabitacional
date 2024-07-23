using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SimulacaoEmprestimo.Models;

namespace SimulacaoEmprestimo.Pages
{
    public class ResultadoModel : PageModel
    {
        public decimal ValorEmprestimo { get; set; }
        public decimal TaxaJuros { get; set; }
        public int Periodo { get; set; }
        public decimal PrimeiraPrestacao { get; set; }
        public decimal UltimaPrestacao { get; set; }
        public decimal ValorTotalPagar { get; set; }
        public List<EncargoMensal> EncargosMensais { get; set; }

        public void OnGet(decimal valorEmprestimo, decimal taxaJuros, int periodo, decimal valorTotalPagar)
        {
            ValorEmprestimo = valorEmprestimo;
            TaxaJuros = taxaJuros;
            Periodo = periodo;
            ValorTotalPagar = valorTotalPagar;
            EncargosMensais = JsonConvert.DeserializeObject<List<EncargoMensal>>(TempData["EncargosMensais"].ToString());

            if (EncargosMensais != null && EncargosMensais.Count > 0)
            {
                PrimeiraPrestacao = EncargosMensais[0].ValorPrestacao;
                UltimaPrestacao = EncargosMensais[EncargosMensais.Count - 1].ValorPrestacao;
            }
        }
    }
}
