using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CManager.Infrastructure.Context.CManager.Mappings
{
    public class ModeloMap
    {
        public void Configure(EntityTypeBuilder<Modelo> entity)
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Descricao).IsRequired().HasMaxLength(100);
            entity.HasOne(d => d.Versao).WithMany();
        }
    }
}