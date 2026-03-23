using Microsoft.EntityFrameworkCore;
using MiWebApp.Models;

namespace MiWebApp.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}