using System.ComponentModel.DataAnnotations;

namespace AtividadesAPI.Dto
{
    public class CategoriaDTO
    {
        public int CategoriaId { get; set; }
        public string NomeCategoria { get; set; }
        public string DescricaoCategoria { get; set; }
        public DateTime DataCriacaoCategoria { get; set; }
        public DateTime? DataAlteracaoCategoria { get; set; } = null;
    }
}
