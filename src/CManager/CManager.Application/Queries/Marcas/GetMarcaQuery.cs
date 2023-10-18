namespace CManager.Application.Queries
{
    public static class GetMarcaQuery
    {
        public sealed record Marca(int id, string nome = "") : IRequest<MarcaResponse>;

        public sealed class Validator : AbstractValidator<Marca>
        {
            public Validator()
            {
                RuleFor(d => d.id)
                .NotEmpty()
                .WithMessage("Informe o id da marca")
                .WithErrorCode(StatusCodes.Status400BadRequest.ToString());
            }
        }
    }
}