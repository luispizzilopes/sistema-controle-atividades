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
        public string NomeAtividade { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string DescricaoAtividade { get; set; } = null!; 

        [Required]
        public DateTime InicioAtividade { get; set; }

        [Required]
        public DateTime FinalAtividade { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        [JsonIgnore]
        public Categoria? Categoria { get; set; }
    }
}
