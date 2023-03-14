using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CManager.Infrastructure.Context.CManager.Mappings
{
    public class MarcaMap
    {
        public void Configure(EntityTypeBuilder<Marca> entity)
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Descricao).IsRequired().HasMaxLength(100);
            entity.HasMany(d => d.Modelos).WithOne().OnDelete(DeleteBehavior.NoAction);
        }
    }
}