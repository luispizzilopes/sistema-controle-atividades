using AtividadesAPI.Context;
using AtividadesAPI.Dto;
using AtividadesAPI.Migrations;
using AtividadesAPI.Models;
using AtividadesAPI.Repositories;
using AtividadesAPI.Repositories.Interfaces;
using AtividadesAPI.Services.Interfaces;

namespace AtividadesAPI.Services
{
    public class AtividadeFuturaService : IAtividadeFutura
    {
        private readonly IRepository<AtividadeFutura> _repositoryAtividadeFutura;
        private readonly IRepository<RegistroLog> _repositoryRegistroLog;
        private readonly AppDbContext _context;

        public AtividadeFuturaService(IRepository<AtividadeFutura> repositoryAtividadeFutura, AppDbContext context, IRepository<RegistroLog> repositoryRegistroLog)
        {
            _repositoryAtividadeFutura = repositoryAtividadeFutura;
            _context = context;
            _repositoryRegistroLog = repositoryRegistroLog;
        }

        public async Task<bool> AddAtividadeFutura(Atividade atividade)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAtividadeFutura(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AtividadeDTO>> GetAllAtividadesFuturas(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<AtividadeDTO?> GetByIdAtividadeFutura(string userId, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAtividadeFutura(Atividade atividade)
        {
            throw new NotImplementedException();
        }
    }
}
