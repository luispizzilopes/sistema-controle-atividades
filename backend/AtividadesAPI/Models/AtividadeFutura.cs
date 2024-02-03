using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AtividadesAPI.Models
{
    public class AtividadeFutura
    {
        [Key]
        public int AtividadeFuturaId { get; set; }

        public string UserId { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string NomeAtividade { get; set; } = null!;

        [Required]
        [StringLength(255)]
        public string DescricaoAtividade { get; set; } = null!;

        [Required]
        public DateTime DataPrevista { get; set; } 
        public DateTime? DataRealizada { get; set; }

        public int CategoriaId { get; set; }

        [JsonIgnore]
        public Categoria? Categoria { get; set; }
    }
}
