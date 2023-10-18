namespace CManager.Application.DTOs.Responses
{
    public class ModeloResponse
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public MarcaResponse Marca { get; set; }
        public List<VersaoResponse> Versoes { get; set; }

        public ModeloResponse(Modelo model)
        {
            Id = model.Id;
            Descricao = model.Descricao;
            Versoes = model.Versoes?.Select(d => new VersaoResponse(d)).ToList() ?? new List<VersaoResponse>();
            Marca = new(model.Marca);
        }

        public ModeloResponse(){}
    }
}