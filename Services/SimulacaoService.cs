using System;
using System.Collections.Generic;
using System.Linq;
using SimulacaoEmprestimo.Models;

namespace SimulacaoEmprestimo.Services
{
    public class SimulacaoService : ISimulacaoService
    {
        public Simulacao CalcularSimulacao(SimulacaoRequest request, string sistemaAmortizacao)
        {
            var simulacao = new Simulacao
            {
                Id = Guid.NewGuid(),
                ValorEmprestimo = request.ValorEmprestimo,
                TaxaJuros = request.TaxaJuros,
                Periodo = request.Periodo,
                EncargosMensais = new List<EncargoMensal>()
            };

            if (sistemaAmortizacao == "SAC")
            {
                simulacao.EncargosMensais = CalcularSAC(request.ValorEmprestimo, request.TaxaJuros, request.Periodo);
            }
            else if (sistemaAmortizacao == "PRICE")
            {
                simulacao.EncargosMensais = CalcularPRICE(request.ValorEmprestimo, request.TaxaJuros, request.Periodo);
            }

            simulacao.ValorTotalPagar = simulacao.EncargosMensais.Sum(e => e.ValorPrestacao);

            if (simulacao.EncargosMensais.Count > 0)
            {
                simulacao.PrimeiraPrestacao = simulacao.EncargosMensais[0].ValorPrestacao;
                simulacao.UltimaPrestacao = simulacao.EncargosMensais[simulacao.EncargosMensais.Count - 1].ValorPrestacao;
            }

            return simulacao;
        }

        private List<EncargoMensal> CalcularSAC(decimal valorEmprestimo, decimal taxaJuros, int periodo)
        {
            var encargosMensais = new List<EncargoMensal>();
            decimal saldoDevedor = valorEmprestimo;
            decimal amortizacao = valorEmprestimo / periodo;
            decimal taxaJurosAnual = taxaJuros / 100;
            decimal taxaJurosMensal = (decimal)Math.Pow((double)(1 + taxaJurosAnual), 1.0 / 12.0) - 1;

            for (int i = 1; i <= periodo; i++)
            {
                decimal juros = saldoDevedor * taxaJurosMensal;
                decimal prestacao = amortizacao + juros;

                encargosMensais.Add(new EncargoMensal
                {
                    Mes = i,
                    SaldoDevedor = Math.Round(saldoDevedor, 2),
                    ValorAmortizacao = Math.Round(amortizacao, 2),
                    ValorJuros = Math.Round(juros, 2),
                    ValorPrestacao = Math.Round(prestacao, 2)
                });

                saldoDevedor -= amortizacao;
            }

            return encargosMensais;
        }

        private List<EncargoMensal> CalcularPRICE(decimal valorEmprestimo, decimal taxaJuros, int periodo)
        {
            var encargosMensais = new List<EncargoMensal>();
            decimal saldoDevedor = valorEmprestimo;
            decimal taxaJurosAnual = taxaJuros / 100;
            decimal taxaJurosMensal = (decimal)Math.Pow((double)(1 + taxaJurosAnual), 1.0 / 12.0) - 1;
            decimal prestacao = valorEmprestimo * taxaJurosMensal / (1 - (decimal)Math.Pow(1 + (double)taxaJurosMensal, -periodo));

            for (int i = 1; i <= periodo; i++)
            {
                decimal juros = saldoDevedor * taxaJurosMensal;
                decimal amortizacao = prestacao - juros;

                encargosMensais.Add(new EncargoMensal
                {
                    Mes = i,
                    SaldoDevedor = Math.Round(saldoDevedor, 2),
                    ValorAmortizacao = Math.Round(amortizacao, 2),
                    ValorJuros = Math.Round(juros, 2),
                    ValorPrestacao = Math.Round(prestacao, 2)
                });

                saldoDevedor -= amortizacao;
            }

            return encargosMensais;
        }
    }
}
