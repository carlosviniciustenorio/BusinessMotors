using Microsoft.EntityFrameworkCore.Design;

namespace BusinessMotors.Infrastructure.Context.CManager
{
    public class BusinessMotorsDbContextFactory : IDesignTimeDbContextFactory<BusinessMotorsDBContext>
    {
        public BusinessMotorsDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BusinessMotorsDBContext>();
            optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=mydatabase;Uid=myuser;Pwd=mypassword", 
                new MySqlServerVersion(new Version(8, 0, 21)));
    
            return new BusinessMotorsDBContext(optionsBuilder.Options);
        }
    }
    
    public class IdentityDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
    {
        public IdentityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>();
            optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=mydatabase;Uid=myuser;Pwd=mypassword", 
                new MySqlServerVersion(new Version(8, 0, 21)));

            return new IdentityDbContext(optionsBuilder.Options);
        }
    }
}