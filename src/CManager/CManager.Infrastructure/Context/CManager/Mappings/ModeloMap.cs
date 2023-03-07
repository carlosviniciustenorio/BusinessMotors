namespace CManager.Infrastructure.Context.CManager.Mappings
{
    public class ModeloMap
    {
        public void Configure(EntityTypeBuilder<Modelo> entity)
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Descricao).IsRequired().HasMaxLength(100);
            entity.Navigation(d => d.Versoes);
        }
    }
}