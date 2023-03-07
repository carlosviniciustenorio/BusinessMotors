namespace CManager.Application.DTOs.Responses
{
    public class VersaoResponse
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public VersaoResponse(Versao model)
        {
            Id = model.Id;
            Descricao = model.Descricao;
        }
    }
}