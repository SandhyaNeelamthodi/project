using Microsoft.EntityFrameworkCore;
using LoginRegistration.Models;

namespace LoginRegistration.Data
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        //public DbContextClass(DbContextOptions<DbContextClass> options) : base(options)
        //{
        //}
        public DbSet<Registration> Registration { get; set; }
    }
}