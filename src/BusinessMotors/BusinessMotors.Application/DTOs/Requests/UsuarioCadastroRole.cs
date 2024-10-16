using System.ComponentModel.DataAnnotations;

namespace ECommerceCT.Application.DTOs.Requests
{
    public class UsuarioCadastroRole
    {
        [Required(ErrorMessage = "E-mail obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} é inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Role obrigatória")]
        public string Role { get; set; }
    }
}
