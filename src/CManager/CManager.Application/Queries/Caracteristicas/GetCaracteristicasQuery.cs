namespace CManager.Application.Queries
{
    public class GetCaracteristicasQuery
    {
        public sealed record Caracteristicas(int take, string nome = "", int skip = 0) : IRequest<List<CaracteristicaResponse>>;

        public class Validator : AbstractValidator<Caracteristicas>
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