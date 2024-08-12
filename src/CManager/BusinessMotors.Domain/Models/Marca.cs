namespace BusinessMotors.Domain.Models
{
    public class Marca
    {
        public Marca(string descricao) => Descricao = descricao;

        public int Id { get; set; }
        public string Descricao { get; private set; }
        public List<Modelo> Modelos { get; set; }
    }
}