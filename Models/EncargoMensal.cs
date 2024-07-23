using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulacaoEmprestimo.Models
{
    public class EncargoMensal
    {
        public int Mes { get; set; }
        public decimal SaldoDevedor { get; set; }
        public decimal ValorAmortizacao { get; set; }
        public decimal ValorJuros { get; set; }
        public decimal ValorPrestacao { get; set; }
    }
}