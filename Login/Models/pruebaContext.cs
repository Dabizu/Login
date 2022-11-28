using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Login.Models
{
    public partial class pruebaContext : DbContext
    {
        public pruebaContext(DbContextOptions<pruebaContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Producto> Productos { get; set; }
    }
}
