using CManager.Domain.Enums;

namespace CManager.Domain.Models
{
    public class Anuncio
    {
        public Guid Id { get; init; }
        public string Placa { get; private set; }
        public EMarca Marca { get; private set; }
        public int AnoModelo { get; private set; }
        public int AnoFabricacao { get; private set; }
        public string Versao { get; private set; }
        public List<TipoCombustivel> TiposCombustiveis { get; private set; }
        public int Portas { get; private set; }
        public ECambio Cambio { get; private set; }
        public ECor Cor { get; private set; }
        public List<Opcional> Opcionais { get; private set; }
        public List<Caracteristica> Caracteristicas {get; private set;}
        public string Km { get; private set; }
        public string Estado { get; private set; }
        public decimal Preco { get; private set; }
        public Guid UsuarioId { get; private set; }
        public bool ExibirTelefone { get; private set; } = false;
        public bool ExibirEmail { get; private set; } = false;
    }
}
