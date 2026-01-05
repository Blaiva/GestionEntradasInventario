using GestionEntradasInventario.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionEntradasInventario.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Productos> Productos { get; set; }

        public DbSet<Entradas> Entradas { get; set; }

        public DbSet<EntradaDetalles> EntradaDetalles { get; set; }
    }
}
