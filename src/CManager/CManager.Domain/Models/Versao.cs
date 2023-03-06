namespace CManager.Domain.Models
{
    public class Versao
    {
        public Versao(){}
        public Versao(string descricao) => Descricao = descricao;

        public int Id { get; init; }
        public string Descricao { get; private set; }
    }
}