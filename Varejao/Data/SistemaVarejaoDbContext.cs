using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using Varejao.Models;
using Varejao_api.Mapping;

namespace Varejao.Data
{
    public class SistemaVarejaoDbContext : DbContext
    {
        public SistemaVarejaoDbContext(DbContextOptions<SistemaVarejaoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Transacao> Transacao { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Produto> Produto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TransacaoMapping());
            modelBuilder.ApplyConfiguration(new ProdutoMapping());
            modelBuilder.ApplyConfiguration(new UsuarioMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
