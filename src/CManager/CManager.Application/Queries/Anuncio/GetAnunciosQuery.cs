namespace CManager.Application.Queries
{
    public static class GetAnunciosQuery
    {
        public sealed record Anuncios(int skip, int take) : IRequest<List<AnunciosResponse>>;

        public class Validator : AbstractValidator<Anuncios>
        {
            public Validator()
            {
                RuleFor(d => d.skip).NotNull().NotEmpty().GreaterThanOrEqualTo(1).WithMessage("Skip deve ser maior ou igual a 1");
                RuleFor(d => d.take).NotNull().NotEmpty().GreaterThanOrEqualTo(1).WithMessage("Take deve ser maior ou igual a 1");
            }
        }
    }
}