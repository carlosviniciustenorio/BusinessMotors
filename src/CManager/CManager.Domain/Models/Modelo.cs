namespace CManager.Domain.Models
{
    public class Modelo
    {
        public Modelo(){}
        public Modelo(string descricao, Versao versao, int anoModelo, int anoFabricacao) => (Descricao, Versao, AnoModelo, AnoFabricacao) = (descricao, versao, anoModelo, anoFabricacao);

        public int Id { get; init; }
        public string Descricao { get; private set; }
        public Versao Versao { get; private set; }
        public int AnoModelo { get; private set; }
        public int AnoFabricacao { get; private set; }
    }
}