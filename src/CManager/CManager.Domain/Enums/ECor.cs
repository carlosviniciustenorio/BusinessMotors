using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CManager.Domain.Enums
{
    public enum ECor
    {
        Amarelo,
        Vermelho,
        Cinza,
        Preto,
        Verde,
        Roxo,
        [Display (Name = "Lilás")]
        Lilas
    }
}
