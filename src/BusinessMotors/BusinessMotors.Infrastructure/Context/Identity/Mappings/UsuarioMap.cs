namespace BusinessMotors.Infrastructure.Context.Identity.Mappings;

public class UsuarioMap : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("AspNetUsers");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Twitter).HasColumnName("Twitter").HasMaxLength(300).IsRequired(false);
        builder.Property(d => d.Instagram).HasColumnName("Instragram").HasMaxLength(300).IsRequired(false);
        builder.Property(d => d.Facebook).HasColumnName("Facebook").HasMaxLength(300).IsRequired(false);
    }
}