namespace CManager.Application.Queries
{
    public static class GetOpcionaisQuery
    {
        public sealed record Opcionais(int take, string nome = "", int skip = 0) : IRequest<List<OpcionalResponse>>;

        public class Validator : AbstractValidator<Opcionais>
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