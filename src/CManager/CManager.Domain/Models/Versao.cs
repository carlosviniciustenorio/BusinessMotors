namespace CManager.Domain.Models
{
    public class Versao
    {
        public Versao(){}
        public Versao(string descricao, Modelo modelo) => (Descricao, Modelo) = (descricao, modelo);

        public int Id { get; init; }
        public string Descricao { get; private set; }
        public Modelo Modelo { get; private set; }
    }
}