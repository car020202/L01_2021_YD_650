using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace L01_2021_YD_650.Models
{
    public class usuarioContext : DbContext

    {
        public usuarioContext(DbContextOptions<usuarioContext> options) : base(options)
        {

        }
        public DbSet<usuarios> usuarios { get; set; }
        public DbSet<calificaciones> calificaciones {  get; set; }
        public DbSet<comentarios> comentarios { get; set; }
    }
}
