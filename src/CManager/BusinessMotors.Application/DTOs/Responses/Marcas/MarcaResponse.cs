namespace BusinessMotors.Application.DTOs.Responses
{
    public class MarcaResponse
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public MarcaResponse(Marca model)
        {
            Id = model.Id;
            Descricao = model.Descricao;
        }

        public MarcaResponse(){}
    }
}