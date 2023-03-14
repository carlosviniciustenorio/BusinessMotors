using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CManager.Infrastructure.Context.CManager.Mappings
{
    public class CaracteristicaMap : IEntityTypeConfiguration<Caracteristica> 
    {
        public void Configure(EntityTypeBuilder<Caracteristica> entity)
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Descricao).IsRequired().HasMaxLength(100);
        }
    }
}