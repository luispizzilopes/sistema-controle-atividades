
using AtividadesAPI.Context;
using AtividadesAPI.Dto;
using AtividadesAPI.Models;
using AtividadesAPI.Repositories.Interfaces;
using AtividadesAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AtividadesAPI.Services
{
    public class AtividadeService : IAtividade
    {
        private readonly IRepository<Atividade> _repositoryAtividade;
        private readonly IRepository<RegistroLog> _repositoryRegistroLog;
        private readonly AppDbContext _context; 

        public AtividadeService(IRepository<Atividade> repositoryAtividade, IRepository<RegistroLog> repositoryRegistroLog, AppDbContext context)
        {
            _repositoryAtividade = repositoryAtividade;
            _repositoryRegistroLog = repositoryRegistroLog;
            _context = context;
        }

        public async Task<IEnumerable<AtividadeDTO>> GetAllAtividades(string userId)
        {
            return await _context.Atividades
                .Where(a => a.UserId == userId)
                .Include(a => a.Categoria)
                .Select(a => new AtividadeDTO
                {
                    AtividadeId = a.AtividadeId,
                    DescricaoAtividade = a.DescricaoAtividade,
                    NomeCategoria = a.Categoria!.NomeCategoria,
                    InicioAtividade = a.InicioAtividade,
                    FinalAtividade = a.FinalAtividade,
                    NomeAtividade = a.NomeAtividade,
                })
                .OrderByDescending(a => a.AtividadeId)
                .ToListAsync();
        }

        public async Task<AtividadeDTO?> GetByIdAtividade(string userId, int id)
        {
            return await _context.Atividades
                .Where(a => a.AtividadeId == id && a.UserId == userId)
                  .Include(a => a.Categoria)
                  .Select(a => new AtividadeDTO
                  {
                      AtividadeId = a.AtividadeId,
                      DescricaoAtividade = a.DescricaoAtividade,
                      NomeCategoria = a.Categoria!.NomeCategoria,
                      InicioAtividade = a.InicioAtividade,
                      FinalAtividade = a.FinalAtividade,
                      NomeAtividade = a.NomeAtividade,
                  }).FirstOrDefaultAsync();
        }

        public async Task<bool> AddAtividade(Atividade atividade)
        {
            if(atividade != null)
            {
                atividade.FinalAtividade = DateTime.Now;
                atividade.InicioAtividade = atividade.InicioAtividade.AddHours(-3);
                await _repositoryAtividade.Add(atividade);

                await _repositoryRegistroLog.Add(new RegistroLog
                {
                    UserId = atividade.UserId,
                    DescricaoRegistro = $"Nova atividade '{atividade.DescricaoAtividade}' registrada na base de dados às {DateTime.Now.TimeOfDay} do dia {DateTime.Now.ToString("dd/MM/yyyy")}"
                }); 

                return true; 
            }

            return false; 
        }

        public async Task<bool> UpdateAtividade(Atividade atividade)
        {
            var atividadeExiste = await _repositoryAtividade.GetById(a => a.AtividadeId == atividade.AtividadeId) != null ? true : false;

            if (atividadeExiste)
            {
                atividade.CategoriaId = await _context.Atividades.Where(a => a.AtividadeId == atividade.AtividadeId).Select(a => a.CategoriaId).FirstOrDefaultAsync(); 

                await _repositoryAtividade.Update(atividade);

                await _repositoryRegistroLog.Add(new RegistroLog
                {
                    UserId = atividade.UserId,
                    DescricaoRegistro = $"Atividade de Id {atividade.AtividadeId} modificada na base de dados às {DateTime.Now.TimeOfDay} do dia {DateTime.Now.ToString("dd/MM/yyyy")}"
                });

                return true; 
            }

            return false;
        }

        public async Task<bool> DeleteAtividade(int id) 
        {
            var atividade = await _repositoryAtividade.GetById(a => a.AtividadeId == id);

            if (atividade != null)
            {
                await _repositoryAtividade.Delete(atividade);

                await _repositoryRegistroLog.Add(new RegistroLog
                {
                    DescricaoRegistro = $"Atividade de Id {atividade.AtividadeId} removida na base de dados às {DateTime.Now.TimeOfDay} do dia {DateTime.Now.ToString("dd/MM/yyyy")}"
                });

                return true; 
            }

            return false; 
        }
    }
}
