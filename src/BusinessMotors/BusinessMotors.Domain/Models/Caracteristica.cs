namespace BusinessMotors.Domain.Models
{
    public class Caracteristica
    {
        public Caracteristica(string descricao)
        {
            Descricao = descricao;
        }

        public int Id { get; init; }
        public string Descricao { get; set; }
    }
}