using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Varejao.Models;

namespace Varejao_api.Mapping
{
    public class TransacaoMapping : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.HasKey(e => e.Id); 
            builder.Property(e => e.ProdutoId).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Quantidade).IsRequired();
            builder.Property(e => e.Categoria).IsRequired();
        }
    }
}