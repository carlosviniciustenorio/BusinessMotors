namespace BusinessMotors.Domain.Models
{
    public class Opcional
    {
        public Opcional(string descricao)
        {
            Descricao = descricao;
        }

        public int Id { get; init; }
        public string Descricao { get; set; }
    }
}