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
        }
    }
}