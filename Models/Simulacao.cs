using System;
using System.Collections.Generic;

namespace SimulacaoEmprestimo.Models
{
    public class Simulacao
    {
        public Guid Id { get; set; }
        public decimal ValorEmprestimo { get; set; }
        public decimal TaxaJuros { get; set; }
        public int Periodo { get; set; }
        public decimal ValorTotalPagar { get; set; }
        public List<EncargoMensal> EncargosMensais { get; set; } = new List<EncargoMensal>();

        public decimal PrimeiraPrestacao { get; set; }
        public decimal UltimaPrestacao { get; set; }
    }
}
