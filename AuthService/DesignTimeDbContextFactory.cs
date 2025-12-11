using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using AuthService.Data;

namespace AuthService
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AuthDbContext>
    {
        public AuthDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuthDbContext>();

            optionsBuilder.UseSqlServer(
                "Data Source=.\\sqlexpress;Initial Catalog=AuthDB;Integrated Security=True;TrustServerCertificate=True");

            return new AuthDbContext(optionsBuilder.Options);
        }
    }
}
