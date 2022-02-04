using Microsoft.EntityFrameworkCore;
using RestWith.NET.Model;

namespace RestWith.NET.Context {
    public class DataContext : DbContext {
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base (options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
        }
        public DbSet<Person> People { get; set; }
        public DbSet<Book> Books {get;set;}
        public DbSet <User> Users { get; set; }
    }
}