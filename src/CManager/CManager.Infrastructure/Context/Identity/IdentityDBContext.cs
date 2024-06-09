namespace CManager.Infrastructure.Context.Identity
{
    public class IdentityDBContext : IdentityDbContext
    {
        public IdentityDBContext(DbContextOptions<IdentityDBContext> options) : base(options) {}
    }
}
