using AtividadesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AtividadesAPI.Context
{
    public class AppDbContext : DbContext
    {
        //Definindo no construtor do classe as configurações utilizadas pelo EntityFrameWork Core
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //Mapeamento das entidades do sistema
        public DbSet<Atividade> Atividades { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<RegistroLog> RegistroLoges { get; set; }
    }
}
