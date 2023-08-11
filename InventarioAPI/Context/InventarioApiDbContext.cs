using InventarioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioAPI.Context
{
    public class InventarioApiDbContext : DbContext
    {
        public InventarioApiDbContext(DbContextOptions<InventarioApiDbContext> options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
    }
}
