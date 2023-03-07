namespace CManager.Infrastructure.Context.CManager.Mappings
{
    public class AnuncioMap : IEntityTypeConfiguration<Anuncio> 
    {
        public void Configure(EntityTypeBuilder<Anuncio> entity)
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Placa).IsRequired();
            entity.Property(d => d.UsuarioId).IsRequired().HasMaxLength(100);
            entity.HasOne(d => d.Marca).WithMany().IsRequired();
            entity.HasMany(d => d.Caracteristicas).WithMany();
            entity.HasMany(d => d.Opcionais).WithMany();
            entity.HasMany(d => d.TiposCombustiveis).WithMany();
            entity.HasOne(d => d.Versao).WithMany().OnDelete(DeleteBehavior.NoAction);
            
            entity.Navigation(n => n.Opcionais).AutoInclude();
            entity.Navigation(n => n.Caracteristicas).AutoInclude();
            entity.Navigation(n => n.Marca).AutoInclude();
            entity.Navigation(n => n.TiposCombustiveis).AutoInclude();
            entity.Navigation(n => n.Modelo).AutoInclude();
            entity.Navigation(n => n.Versao).AutoInclude();
        }
    }
}