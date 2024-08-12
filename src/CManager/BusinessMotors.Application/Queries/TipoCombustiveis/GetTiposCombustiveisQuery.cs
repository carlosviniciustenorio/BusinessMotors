namespace BusinessMotors.Application.Queries
{
    public static class GetTiposCombustiveisQuery
    {
        public sealed record TiposCombustiveis(int take, string nome = "", int skip = 0) : IRequest<List<TipoCombustivelResponse>>;

        public sealed class Validator : AbstractValidator<TiposCombustiveis>
        {
            public Validator()
            {
                RuleFor(d => d.take)
                .NotEmpty()
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(100)
                .WithMessage("Take deve ser maior ou igual a 1 e menor ou igual a 100");
            }
        }
    }
}