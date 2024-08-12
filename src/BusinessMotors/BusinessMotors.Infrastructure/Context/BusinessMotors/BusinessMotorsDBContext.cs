using BusinessMotors.Infrastructure.Context.CManager.Mappings;

namespace BusinessMotors.Infrastructure.Context.CManager
{
    public class BusinessMotorsDBContext : DbContext
    {
        public DbSet<Anuncio> Anuncio { get; set; }
        public DbSet<Opcional> Opcional { get; set; }

        public BusinessMotorsDBContext(DbContextOptions<BusinessMotorsDBContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AnuncioMap).Assembly);
        }
    }
}