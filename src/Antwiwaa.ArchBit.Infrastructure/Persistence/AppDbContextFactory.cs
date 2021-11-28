using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Antwiwaa.ArchBit.Infrastructure.Persistence
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            //optionsBuilder.UseSqlServer("Server=localhost;Database=Nyansabepo;User Id=sa;Password=P@ssw0rd2;");
            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-MBU6BF0\\SQLEXPRESS;Database=AntwiwaaDemo;Persist Security Info=True;Integrated Security = True;");
            return AppDbContext.CreateDbContext(optionsBuilder.Options);
        }
    }
}