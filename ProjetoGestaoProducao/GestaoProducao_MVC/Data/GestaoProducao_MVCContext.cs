using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GestaoProducao_MVC.Models;


namespace GestaoProducao_MVC.Data
{
    public class GestaoProducao_MVCContext : DbContext
    {
        public GestaoProducao_MVCContext (DbContextOptions<GestaoProducao_MVCContext> options)
            : base(options)
        {
        }

        public DbSet<OrdemProduto> OrdemProduto { get; set; } = default!;
        public DbSet<Processo> Processo { get; set; } = default!;

        public DbSet<Apontamento> Apontamento { get; set; } = default!;

        public DbSet<CodigoParada> CodigoParada { get; set; } = default!;

        public DbSet<RegistroParada> RegistroParada { get; set; } = default!;

        public DbSet<Funcionario> Funcionario { get; set; } = default!;

        public DbSet<Maquina> Maquina { get; set; } = default!;



    }
}
