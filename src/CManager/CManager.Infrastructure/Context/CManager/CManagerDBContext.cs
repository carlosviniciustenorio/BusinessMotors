using CManager.Domain.Models;
using CManager.Infrastructure.Context.CManager.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CManager.Infrastructure.Context.CManager
{
    public class CManagerDBContext : DbContext
    {
        public DbSet<Anuncio> Anuncio { get; set; }

         public CManagerDBContext(DbContextOptions<CManagerDBContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AnuncioMap).Assembly);
        }
    }
}