using Sample_Api.Models;

using Microsoft.EntityFrameworkCore;
namespace Sample_Api.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            
        }

        public DbSet<Character> Characters{get;set;}
        public DbSet<User> Users{get;set;}
    }
}