using Microsoft.AspNetCore.Identity;

namespace BusinessMotors.Domain.Models;

public class Usuario : IdentityUser
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Instagram { get; set; }
    public string Twitter { get; set; }
    public string Facebook { get; set; }
}