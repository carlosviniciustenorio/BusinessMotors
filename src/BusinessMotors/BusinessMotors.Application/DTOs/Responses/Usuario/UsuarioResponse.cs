namespace ECommerceCT.Application.DTOs.Responses;

public class UsuarioResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    
    public UsuarioResponse(string id, string name)
    {
        Id = id;
        Name = name;
    }
}