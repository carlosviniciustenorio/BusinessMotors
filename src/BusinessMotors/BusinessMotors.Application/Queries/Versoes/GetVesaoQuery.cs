namespace BusinessMotors.Application.Queries
{
    public static class GetVesaoQuery
    {
        public sealed record Versao(int id, string nome = "") : IRequest<VersaoResponse>;

        public sealed class Validator : AbstractValidator<Versao>
        {
            public Validator()
            {
                RuleFor(d => d.id)
                .NotEmpty()
                .WithMessage("Informe o id da versão")
                .WithErrorCode(StatusCodes.Status400BadRequest.ToString());
            }
        }
    }
}