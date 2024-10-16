namespace BusinessMotors.Domain.Models
{
    public class TipoCombustivel
    {
        public TipoCombustivel(string descricao)
        {
            Descricao = descricao;
        }

        public int Id { get; init; }
        public string Descricao { get; set; }
    }
}