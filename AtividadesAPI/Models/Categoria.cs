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
        public string NomeCategoria { get; set; }

        [Required]
        [MaxLength(255)]
        public string DescricaoCategoria { get; set; }

        [Required]
        public DateTime DataCriacaoCategoria { get; set; }

        public DateTime? DataAlteracaoCategoria { get; set; } = null; 

        [JsonIgnore]
        public ICollection<Atividade>? Atividades { get; set; }
    }
}
