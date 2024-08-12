using System.ComponentModel.DataAnnotations;

namespace BusinessMotors.Domain.Enums
{
    public enum ECambio
    {
        [Display (Name = "Manual")]
        Manual,
        [Display (Name = "Automático")]
        Automatico,
        [Display (Name = "Automatizado")]
        Automatizado
    }
}
