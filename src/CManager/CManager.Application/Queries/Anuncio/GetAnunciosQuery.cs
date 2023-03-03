namespace CManager.Application.Queries
{
    public static class GetAnunciosQuery
    {
        public sealed record Anuncios() : IRequest<List<AnunciosResponse>>;
    }
}