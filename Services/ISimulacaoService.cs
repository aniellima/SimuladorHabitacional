using SimulacaoEmprestimo.Models;

namespace SimulacaoEmprestimo.Services
{
    public interface ISimulacaoService
    {
        Simulacao CalcularSimulacao(SimulacaoRequest request, string sistemaAmortizacao);
    }
}
