using System;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace Datos
{
    public class TiendaContext : DbContext
    {
        public TiendaContext(DbContextOptions options) : base(options){

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos {get;set;}
    }
}
