namespace BusinessMotors.Domain.Models
{
    public class Imagem
    {
        public int Id { get; init; }
        public string UrlS3 { get; set; }

        public Imagem(string urlS3)
        {
            UrlS3 = urlS3;
        }
    }
}
