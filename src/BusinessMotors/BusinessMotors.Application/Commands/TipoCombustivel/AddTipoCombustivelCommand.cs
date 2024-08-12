namespace BusinessMotors.Application.Commands
{
    public static class AddTipoCombustivelCommand
    {
        public sealed record TipoCombustivelCommand(string descricao) : IRequest<Unit>;

        public sealed class Validator : AbstractValidator<TipoCombustivelCommand>
        {
            public Validator()
            {
                RuleFor(c => c.descricao)
                    .NotEmpty().WithMessage("Descrição não pode ser vazio");
            }
        }
    }
}