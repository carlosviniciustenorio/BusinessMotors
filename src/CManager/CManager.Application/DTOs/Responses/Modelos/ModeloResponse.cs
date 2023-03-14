namespace CManager.Application.DTOs.Responses
{
    public class ModeloResponse
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int AnoModelo { get; set; }
        public int AnoFabricacao { get; set; }
        public MarcaResponse Marca { get; set; }
        public VersaoResponse Versao { get; set; }

        public ModeloResponse(Modelo model, Versao versao)
        {
            Id = model.Id;
            Descricao = model.Descricao;
            AnoModelo = model.AnoModelo;
            AnoFabricacao = model.AnoFabricacao;
            Versao = new(versao);
            Marca = new(model.Marca);
        }

        public ModeloResponse(){}
    }
}