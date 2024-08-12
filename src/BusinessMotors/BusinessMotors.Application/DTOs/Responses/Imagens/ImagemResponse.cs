namespace BusinessMotors.Application.DTOs.Responses
{
    public class ImagemResponse
    {
        public string Arn { get; set; }

        public ImagemResponse(Imagem imagem)
        {
            Arn = imagem.UrlS3;
        }
    }
}