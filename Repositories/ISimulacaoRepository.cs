using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimulacaoEmprestimo.Models;

namespace SimulacaoEmprestimo.Repositories
{
    public interface ISimulacaoRepository
    {
        Task AddAsync(Simulacao simulacao);
        Task<Simulacao> GetByIdAsync(Guid id);
        Task<IEnumerable<Simulacao>> GetAllAsync();
    }
}
