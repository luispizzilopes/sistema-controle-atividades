﻿using AtividadesAPI.Dto;
using AtividadesAPI.Models;

namespace AtividadesAPI.Services.Interfaces
{
    public interface IAtividade
    {
        Task<IEnumerable<AtividadeDTO>> GetAllAtividades(string userId);
        Task<AtividadeDTO?> GetByIdAtividade(string userId, int id);
        Task<bool> AddAtividade(Atividade atividade);
        Task<bool> UpdateAtividade(Atividade atividade);
        Task<bool> DeleteAtividade(int id); 
    }
}
