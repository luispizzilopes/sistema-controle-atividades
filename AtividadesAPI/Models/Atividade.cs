using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AtividadesAPI.Models
{
    public class Atividade
    {
        [Key]
        public int AtividadeId { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string NomeAtividade { get; set; }

        [Required]
        [MaxLength(255)]
        public string DescricaoAtividade { get; set; }

        [Required]
        public DateTime DataCriacaoAtividade { get; set; }

        public DateTime? DataAlteracaoAtividade { get; set; } = null;

        [Required]
        public int CategoriaId { get; set; }

        [JsonIgnore]
        public Categoria? Categoria { get; set; }
    }
}
