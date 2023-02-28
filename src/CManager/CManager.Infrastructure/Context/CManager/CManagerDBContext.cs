using CManager.Domain.Models;
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
    }
}