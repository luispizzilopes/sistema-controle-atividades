namespace AtividadesAPI.Dto
{
    public class UsuarioTokenDTO
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public bool Authenticated { get; set; }
        public DateTime Expiration { get; set; }
        public string? Token { get; set; }
        public string? Message { get; set; }
    }
}
