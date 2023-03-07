namespace CManager.Domain.Models
{
    public class Modelo
    {
        public Modelo(){}
        public Modelo(string descricao, int anoModelo, int anoFabricacao, Marca marca) => (Descricao, AnoModelo, AnoFabricacao, Marca) = (descricao, anoModelo, anoFabricacao, marca);

        public int Id { get; init; }
        public string Descricao { get; private set; }
        public int AnoModelo { get; private set; }
        public int AnoFabricacao { get; private set; }
        public Marca Marca { get; private set; }
        public List<Versao> Versoes { get; private set; }
    }
}