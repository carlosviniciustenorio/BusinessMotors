namespace BusinessMotors.Infrastructure.Context.BusinessMotors.Mappings
{
    public class AnuncioMap : IEntityTypeConfiguration<Anuncio> 
    {
        public void Configure(EntityTypeBuilder<Anuncio> entity)
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Placa).IsRequired();
            entity.Property(d => d.AnoVeiculo).IsRequired().HasMaxLength(4);
            entity.Property(d => d.AnoFabricacao).IsRequired().HasMaxLength(4);
            entity.Property(d => d.UserId).IsRequired().HasMaxLength(36);
            entity.Property(d => d.Preco).IsRequired().HasPrecision(18,2);
            entity.Property(d => d.DataCriacao).IsRequired();
            entity.Property(d => d.DataAtualizacao).IsRequired(false);
            entity.Property(d => d.Estado).IsRequired();
            entity.Property(d => d.Cidade).IsRequired();
            entity.HasMany(d => d.Caracteristicas).WithMany();
            entity.HasMany(d => d.Opcionais).WithMany();
            entity.HasMany(d => d.TiposCombustiveis).WithMany();
            entity.HasMany(d => d.ImagensS3).WithOne();
            entity.HasOne(d => d.Modelo).WithMany().OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(d => d.Versao).WithMany().OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(d => d.User).WithMany().HasForeignKey(d => d.UserId).OnDelete(DeleteBehavior.Cascade);

            entity.Navigation(n => n.Opcionais).AutoInclude();
            entity.Navigation(n => n.Caracteristicas).AutoInclude();
            entity.Navigation(n => n.TiposCombustiveis).AutoInclude();
            entity.Navigation(n => n.Modelo).AutoInclude();
            entity.Navigation(n => n.Versao).AutoInclude();
            entity.Navigation(n => n.ImagensS3).AutoInclude();
        }
    }
}