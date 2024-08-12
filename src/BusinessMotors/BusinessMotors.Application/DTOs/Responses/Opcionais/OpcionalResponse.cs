namespace BusinessMotors.Application.DTOs.Responses
{
    public class OpcionalResponse
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public OpcionalResponse(Opcional model)
        {
            Id = model.Id;
            Descricao = model.Descricao;
        }

        public OpcionalResponse(){}
    }
}