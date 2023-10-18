namespace CManager.Application.DTOs.Responses
{
    public class TipoCombustivelResponse
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public TipoCombustivelResponse(TipoCombustivel model)
        {
            Id = model.Id;
            Descricao = model.Descricao;
        }

        public TipoCombustivelResponse(){}
    }
}