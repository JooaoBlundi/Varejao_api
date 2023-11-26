using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Varejao.Models;

namespace Varejao_api.Mapping
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Categoria).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Quantidade).IsRequired().HasMaxLength(30);
            builder.Property(x => x.PrecoCusto).IsRequired().HasMaxLength(30);
            builder.Property(x => x.PrecoVenda).IsRequired().HasMaxLength(30);
        }
    }
}
