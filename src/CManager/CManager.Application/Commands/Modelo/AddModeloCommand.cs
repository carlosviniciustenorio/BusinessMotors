namespace CManager.Application.Commands
{
    public static class AddModeloCommand
    {
        public sealed record ModeloCommand(string descricao, int anoModelo, int anoFabricacao, int idMarca) : IRequest<Unit>;

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

                RuleFor(c => c.anoModelo)
                    .NotNull()
                    .NotEmpty()
                    .GreaterThanOrEqualTo(c => c.anoFabricacao)
                    .WithMessage("Ano modelo não pode ser vazio e deve ser igual ou maior que ano de fabricação");

                RuleFor(c => c.anoFabricacao)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Ano fabricação não pode ser vazio");
            }
        }
    }
}