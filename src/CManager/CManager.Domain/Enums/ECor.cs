using System.ComponentModel.DataAnnotations;

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
