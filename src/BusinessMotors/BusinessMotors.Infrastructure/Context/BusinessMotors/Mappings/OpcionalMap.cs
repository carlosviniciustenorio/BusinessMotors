namespace BusinessMotors.Infrastructure.Context.BusinessMotors.Mappings
{
    public class OpcionalMap : IEntityTypeConfiguration<Opcional> 
    {
        public void Configure(EntityTypeBuilder<Opcional> entity)
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Descricao).HasMaxLength(100);
        }
    }
}