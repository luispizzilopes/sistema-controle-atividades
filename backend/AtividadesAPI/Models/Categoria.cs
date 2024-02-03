using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AtividadesAPI.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string NomeCategoria { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string DescricaoCategoria { get; set; } = null!; 

        [Required]
        public DateTime DataCriacaoCategoria { get; set; }

        public DateTime? DataAlteracaoCategoria { get; set; } = null;

        [Required]
        public string UserId { get; set; } = null!;

        [JsonIgnore]
        public ICollection<Atividade>? Atividades { get; set; }

        [JsonIgnore]
        public ICollection<AtividadeFutura>? AtividadesFuturas { get; set; }
    }
}
