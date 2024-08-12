namespace BusinessMotors.Application.Commands
{
    public static class AddModeloCommand
    {
        public sealed record ModeloCommand(string descricao, int idMarca) : IRequest<Unit>;

        public sealed class Validator : AbstractValidator<ModeloCommand>
        {
            public Validator()
            {
                RuleFor(c => c.idMarca)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Id Marca não pode ser vazio");

                RuleFor(c => c.descricao)
                    .NotNull().NotEmpty().WithMessage("Descrição não pode ser vazio");
            }
        }
    }
}