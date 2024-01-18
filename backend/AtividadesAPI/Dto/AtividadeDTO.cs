using System.ComponentModel.DataAnnotations;

namespace AtividadesAPI.Dto
{
    public class AtividadeDTO
    {
        public int AtividadeId { get; set; }
        public string NomeAtividade { get; set; }
        public string DescricaoAtividade { get; set; }
        public DateTime InicioAtividade { get; set; }
        public DateTime FinalAtividade { get; set; }
        public int CategoriaId { get; set; }
    }
}
