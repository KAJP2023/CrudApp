using CrudAppApi.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace CrudAppApi.Data
{
    public class CrudAppDbContext : DbContext
    {
        public CrudAppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
        {
            
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Telefono> Telefonos { get; set; }
    }
}
