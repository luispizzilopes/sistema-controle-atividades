using System.ComponentModel.DataAnnotations;

namespace AtividadesAPI.Dto
{
    public class AtividadeDTO
    {
        public int AtividadeId { get; set; }
        public string NomeAtividade { get; set; } = null!;
        public string DescricaoAtividade { get; set; } = null!;
        public DateTime InicioAtividade { get; set; }
        public DateTime FinalAtividade { get; set; }
        public string NomeCategoria { get; set; } = null!; 
    }
}
