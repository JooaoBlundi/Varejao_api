using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Varejao.Models;

namespace Varejao_api.Mapping
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Cpf).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Cargo).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Login).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Senha).IsRequired().HasMaxLength(30);
        }
    }
}