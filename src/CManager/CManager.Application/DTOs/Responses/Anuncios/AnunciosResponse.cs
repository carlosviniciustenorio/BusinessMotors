namespace CManager.Application.DTOs.Responses
{
    public class AnunciosResponse
    {
        public Guid Id { get; set; }
        public ModeloResponse Modelo { get; set; }
        public ECambio Cambio { get; set; }
        public ECor Cor { get; set; }
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