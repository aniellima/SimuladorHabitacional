using System;

namespace SimulacaoEmprestimo.Models
{
    public class SimulacaoRequest
    {
        public decimal ValorEmprestimo { get; set; }
        public decimal TaxaJuros { get; set; }
        public int Periodo { get; set; }
        public string SistemaAmortizacao { get; set; } = "SAC";
    }
}
