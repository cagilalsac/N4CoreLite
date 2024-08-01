using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess.Factories
{
    public class DbFactory : IDesignTimeDbContextFactory<Db>
    {
        public Db CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Db>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=N4CoreLiteDemoDB;trusted_connection=true;trustservercertificate=true;");
            return new Db(optionsBuilder.Options);
        }
    }
}
