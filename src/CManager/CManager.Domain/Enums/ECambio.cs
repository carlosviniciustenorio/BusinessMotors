using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CManager.Domain.Enums
{
    public enum ECambio
    {
        Manual,
        [Display (Name = "Automático")]
        Automatico,
        Automatizado
    }
}
