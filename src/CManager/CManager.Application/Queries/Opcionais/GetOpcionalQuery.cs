namespace CManager.Application.Queries
{
    public class GetOpcionalQuery
    {
        public sealed record Opcional(int id, string nome = "") : IRequest<OpcionalResponse>;

        public sealed class Validator : AbstractValidator<Opcional>
        {
            public Validator()
            {
                RuleFor(d => d.id)
                .NotEmpty()
                .WithMessage("Informe o id do opcional")
                .WithErrorCode(StatusCodes.Status400BadRequest.ToString());
            }
        }
    }
}