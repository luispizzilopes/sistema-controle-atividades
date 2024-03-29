﻿using AtividadesAPI.Migrations;
using AtividadesAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AtividadesAPI.Context
{
    public class AppDbContext : IdentityDbContext
    {
        //Definindo no construtor do classe as configurações utilizadas pelo EntityFrameWork Core
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //Mapeamento das entidades do sistema
        public DbSet<Atividade> Atividades { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<RegistroLog> RegistroLoges { get; set; }
        public DbSet<AtividadeFutura> AtividadesFuturas { get; set; }
    }
}
