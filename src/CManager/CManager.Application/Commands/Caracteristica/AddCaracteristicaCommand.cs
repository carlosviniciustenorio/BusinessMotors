namespace CManager.Application.Commands
{
    public static class AddCaracteristicaCommand
    {
        public sealed record CaracteristicaCommand(string descricao) : IRequest<Unit>;

        public sealed class Validator : AbstractValidator<CaracteristicaCommand>
        {
            public Validator()
            {
                RuleFor(c => c.descricao)
                    .NotEmpty().WithMessage("Descrição não pode ser vazio");
            }
        }
    }
}