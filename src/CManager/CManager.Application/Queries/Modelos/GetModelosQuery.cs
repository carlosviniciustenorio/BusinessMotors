namespace CManager.Application.Queries
{
    public static class GetModelosQuery
    {
        public sealed record Modelos(int take, string nome = "", int skip = 0, int idMarca = 0) : IRequest<List<ModeloResponse>>;

        public class Validator : AbstractValidator<Modelos>
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