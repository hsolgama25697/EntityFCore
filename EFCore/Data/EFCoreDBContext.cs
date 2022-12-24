using EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Data
{
    public class EFCoreDBContext : DbContext
    {
        public EFCoreDBContext(DbContextOptions options) : base(options)
        {
            
        }

        //write command
        //Add-Migration "intial Migration"
        //Update-Database
        public DbSet<Student> Students { get; set; }
    }
}
