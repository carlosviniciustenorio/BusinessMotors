namespace CManager.Domain.Models
{
    public class Modelo
    {
        public Modelo(){}
        public Modelo(string descricao, int anoModelo, int anoFabricacao) => (Descricao, AnoModelo, AnoFabricacao) = (descricao, anoModelo, anoFabricacao);

        public int Id { get; init; }
        public string Descricao { get; private set; }
        public int AnoModelo { get; private set; }
        public int AnoFabricacao { get; private set; }
        public List<Versao> Versoes { get; private set; }
    }
}