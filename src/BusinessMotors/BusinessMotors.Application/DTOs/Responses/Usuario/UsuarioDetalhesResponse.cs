namespace ECommerceCT.Application.DTOs.Responses;

public class UsuarioDetalhesResponse
{
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string Instagram { get; set; }
    public string Twitter { get; set; }
    public string Facebook { get; set; }
    public UsuarioDetalhesResponse(string telefone, string email, string instagram, string twitter, string facebook)
    {
        Telefone = telefone;
        Email = email;
        Instagram = instagram;
        Twitter = twitter;
        Facebook = facebook;
    }
}