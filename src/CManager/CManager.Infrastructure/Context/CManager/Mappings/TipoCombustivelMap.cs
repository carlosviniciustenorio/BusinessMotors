using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CManager.Infrastructure.Context.CManager.Mappings
{
    public class TipoCombustivelMap : IEntityTypeConfiguration<TipoCombustivel> 
    {
        public void Configure(EntityTypeBuilder<TipoCombustivel> entity)
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Descricao).HasMaxLength(100);
        }
    }
}