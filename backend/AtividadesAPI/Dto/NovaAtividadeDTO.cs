namespace AtividadesAPI.Dto
{
    public class NovaAtividadeDTO
    {
        public int AtividadeId { get; set; }
        public string UserId { get; set; } = null!; 
        public string NomeAtividade { get; set; } = null!;
        public string DescricaoAtividade { get; set; } = null!;
        public DateTime InicioAtividade { get; set; }
        public DateTime FinalAtividade { get; set; }
        public int CategoriaId { get; set; }
    }
}
