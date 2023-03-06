namespace CManager.Domain.Models
{
    public class Marca
    {
        public Marca(string descricao) => Descricao = descricao;

        public int Id { get; init; }
        public string Descricao { get; private set; }
    }
}