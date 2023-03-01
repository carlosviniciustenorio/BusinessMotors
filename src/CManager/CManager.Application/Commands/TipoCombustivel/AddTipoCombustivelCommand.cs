namespace CManager.Application.Commands
{
    public static class AddTipoCombustivelCommand
    {
        public sealed record Command(string descricao) : IRequest<Unit>;

        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.descricao)
                    .NotEmpty().WithMessage("Descrição não pode ser vazio");
            }
        }
    }
}