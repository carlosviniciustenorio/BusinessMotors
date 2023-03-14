namespace CManager.Application.Commands
{
    public static class AddOpcionalCommand
    {
        public sealed record OpcionalCommand(string descricao) : IRequest<Unit>;

        public sealed class Validator : AbstractValidator<OpcionalCommand>
        {
            public Validator()
            {
                RuleFor(c => c.descricao)
                    .NotEmpty().WithMessage("Descrição não pode ser vazio");
            }
        }
    }
}