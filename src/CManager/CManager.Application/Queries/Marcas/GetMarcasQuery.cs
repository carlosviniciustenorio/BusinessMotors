namespace CManager.Application.Queries
{
    public static class GetMarcasQuery
    {
        public sealed record Marcas(int take, string nome = "", int skip = 0) : IRequest<List<MarcaResponse>>;

        public sealed class Validator : AbstractValidator<Marcas>
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