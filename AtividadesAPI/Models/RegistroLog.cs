using System.ComponentModel.DataAnnotations;

namespace AtividadesAPI.Models
{
    public class RegistroLog
    {
        [Key]
        public int RegistroId { get; set; }

        [Required]
        public string DescricaoRegistro { get; set; }
    }
}
