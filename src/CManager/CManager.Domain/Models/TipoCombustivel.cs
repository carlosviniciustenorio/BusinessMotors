using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CManager.Domain.Models
{
    public class TipoCombustivel
    {
        public TipoCombustivel(string descricao)
        {
            Descricao = descricao;
        }

        public int Id { get; init; }
        public string Descricao { get; set; }
    }
}