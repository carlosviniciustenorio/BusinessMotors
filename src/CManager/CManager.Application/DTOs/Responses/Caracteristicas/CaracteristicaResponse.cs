namespace CManager.Application.DTOs.Responses
{
    public class CaracteristicaResponse
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public CaracteristicaResponse(Caracteristica model)
        {
            Id = model.Id;
            Descricao = model.Descricao;
        }

        public CaracteristicaResponse(){}
    }
}