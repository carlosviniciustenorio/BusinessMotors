using System.Security.Cryptography.X509Certificates;

namespace CManager.Application.Queries
{
    public static class GetAnunciosQuery
    {
        public sealed record Anuncios(int take, string? estado, decimal? precoInicio, decimal? precoFim, int? kmInicio, int? kmFim, int? anoModeloInicio, int? anoModeloFim, int? idMarca, int? idModelo, int? idVersao, int skip = 0) : IRequest<List<AnunciosResponse>>;

        public sealed class Validator : AbstractValidator<Anuncios>
        {
            public Validator()
            {
                RuleFor(d => d.take).NotNull().NotEmpty().GreaterThanOrEqualTo(1).WithMessage("Take deve ser maior ou igual a 1");
                
                When(x => x.precoInicio.HasValue && x.precoFim.HasValue, () => {
                    RuleFor(x => x.precoInicio).NotNull().LessThanOrEqualTo(x => x.precoFim).WithMessage("precoInicio deve ser menor ou igual ao precoFim");
                });

                When(x => x.anoModeloInicio.HasValue && x.anoModeloFim.HasValue, () => {
                    RuleFor(x => x.anoModeloInicio).NotNull().LessThanOrEqualTo(x => x.anoModeloFim).WithMessage("anoModeloInicio deve ser menor ou igual ao anoModeloFim");
                });

                When(x => x.kmInicio.HasValue && x.kmFim.HasValue, () => {
                    RuleFor(x => x.kmInicio).NotNull().LessThanOrEqualTo(x => x.kmFim).WithMessage("kmInicio deve ser menor ou igual a kmFim");
                });
            }
        }
    }
}