namespace CManager.Application.Queries
{
    public static class GetAnunciosQuery
    {
        public sealed record Anuncios(string? estado, decimal? precoInicio, decimal? precoFim, int? kmInicio, int? kmFim, int? anoModeloInicio, int? anoModeloFim, int? idMarca, int? idModelo, int? idVersao) : IRequest<List<AnunciosResponse>>;

        public class Validator : AbstractValidator<Anuncios>
        {
            public Validator()
            {
                When(x => x.precoInicio.HasValue && x.precoFim.HasValue, () => {
                    RuleFor(x => x.precoInicio).NotNull().LessThan(x => x.precoFim);
                });

                When(x => x.anoModeloInicio.HasValue && x.anoModeloFim.HasValue, () => {
                    RuleFor(x => x.anoModeloInicio).NotNull().LessThan(x => x.anoModeloFim);
                });

                When(x => x.kmInicio.HasValue && x.kmFim.HasValue, () => {
                    RuleFor(x => x.kmInicio).NotNull().LessThan(x => x.kmFim);
                });
            }
        }
    }
}