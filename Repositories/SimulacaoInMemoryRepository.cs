using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimulacaoEmprestimo.Models;

namespace SimulacaoEmprestimo.Repositories
{
    public class SimulacaoInMemoryRepository : ISimulacaoRepository
    {
        private readonly List<Simulacao> _simulacoes = new();

        public Task AddAsync(Simulacao simulacao)
        {
            _simulacoes.Add(simulacao);
            return Task.CompletedTask;
        }

        public Task<Simulacao?> GetByIdAsync(Guid id)
        {
            var simulacao = _simulacoes.FirstOrDefault(s => s.Id == id);
            return Task.FromResult(simulacao);
        }

        public Task<IEnumerable<Simulacao>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Simulacao>>(_simulacoes);
        }
    }
}
