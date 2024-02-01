using AtividadesAPI.Models;
using AutoMapper;

namespace AtividadesAPI.Dto.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Atividade, AtividadeDTO>().ReverseMap();
            CreateMap<Atividade, NovaAtividadeDTO>().ReverseMap(); 
            CreateMap<Categoria, CategoriaDTO>().ReverseMap(); 
        }
    }
}
