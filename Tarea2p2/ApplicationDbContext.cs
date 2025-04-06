using System.Collections.Generic;
using System.Data.Entity;

namespace Tarea2p2.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection") // Aquí debe coincidir con el nombre de tu connectionString en web.config
        {
        }

        public DbSet<Nota> Notas { get; set; }
    }
}
