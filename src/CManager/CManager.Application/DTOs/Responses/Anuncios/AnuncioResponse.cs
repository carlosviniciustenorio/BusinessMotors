namespace CManager.Application.DTOs.Responses
{
    public class AnuncioResponse
    {
        public Guid Id { get; set; }
        public string Placa { get; set; }
        public ModeloResponse Modelo { get; set; }
        public List<TipoCombustivel> TiposCombustiveis { get; set; }
        public int Portas { get; set; }
        public ECambio Cambio { get; set; }
        public ECor Cor { get; set; }
        public List<OpcionalResponse> Opcionais { get; set; }
        public List<Caracteristica> Caracteristicas {get; set;}
        public string Km { get; set; }
        public string Estado { get; set; }
        public decimal Preco { get; set; }
        public bool ExibirTelefone { get; set; } = false;
        public bool ExibirEmail { get; set; } = false;
        public List<ImagemResponse> Imagens { get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoVeiculo { get; set; }
    }
}