namespace BusinessMotors.Application.DTOs.Responses
{
    public class AnuncioResponse
    {
        public Guid Id { get; set; }
        public string Placa { get; set; }
        public ModeloResponse Modelo { get; set; }
        public List<TipoCombustivelResponse> TiposCombustiveis { get; set; }
        public int Portas { get; set; }
        public string Cambio { get; set; }
        public string Cor { get; set; }
        public List<OpcionalResponse> Opcionais { get; set; }
        public List<CaracteristicaResponse> Caracteristicas {get; set;}
        public string Km { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public decimal Preco { get; set; }
        public string UsuarioId { get; set; }
        public bool ExibirTelefone { get; set; } = false;
        public bool ExibirEmail { get; set; } = false;
        public List<ImagemResponse> Imagens { get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoVeiculo { get; set; }
    }
}