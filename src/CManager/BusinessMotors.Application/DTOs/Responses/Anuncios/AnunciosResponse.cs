namespace BusinessMotors.Application.DTOs.Responses
{
    public class AnunciosResponse
    {
        public Guid Id { get; set; }
        public ModeloResponse Modelo { get; set; }
        public string Cambio { get; set; }
        public string Cor { get; set; }
        public string Km { get; set; }
        public string Estado { get; set; }
        public decimal Preco { get; set; }
        // public string UsuarioId { get; set; }
        public bool ExibirTelefone { get; set; } = false;
        public bool ExibirEmail { get; set; } = false;
        public ImagemResponse Imagem { get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoVeiculo { get; set; }
    }
}