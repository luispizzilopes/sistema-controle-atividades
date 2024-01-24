namespace AtividadesAPI.Dto
{
    public class HomeInfoDTO
    {
        public int AtividadesCadastradas { get; set; }
        public int CategoriasCadastradas { get; set; }
        public double TempoTotalAtividades { get; set; }
        public int TotalGeralAtividades { get; set; }
        public int[] GraficoAtividades { get; set; } = new int[12];
        public int[] GraficoCategorias { get; set; } = new int[12];
    }
}
