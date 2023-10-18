namespace CManager.Domain.Models
{
    public class Modelo
    {
        public Modelo(){}
        public Modelo(string descricao, Marca marca) => (Descricao, Marca) = (descricao, marca);

        public int Id { get; init; }
        public string Descricao { get; private set; }
        public Marca Marca { get; private set; }
        public List<Versao> Versoes { get; private set; }
    }
}