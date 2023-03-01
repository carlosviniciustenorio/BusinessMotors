
namespace CManager.Application.Commands
{
    public static class AddAnuncioCommand
    {
        public sealed record Command(
            string placa,
            EMarca marca,
            int anoModelo,
            int anoFabricacao,
            string versao,
            List<AddTipoCombustivelCommand.Command>? tipoCombustivelCommand,
            int portas,
            ECambio cambio,
            ECor cor,
            List<AddOpcionalCommand.Command>? opcionalCommand, 
            List<AddCaracteristicaCommand.Command>? caracteristicaCommand, 
            string km,
            string estado,
            Guid usuarioId,
            bool exibirTelefone,
            bool exibirEmail
        ) : IRequest<Unit>;

        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.placa)
                    .NotEmpty()
                    .WithMessage("Placa não pode ser vazio")
                    .Length(7);

                RuleFor(c => c.usuarioId)
                    .NotEmpty()
                    .WithMessage("Id Usuário não pode ser vazio");
                
                RuleFor(c => c.marca)
                    .IsInEnum()
                    .NotEmpty()
                    .WithMessage("Marca não pode ser vazio");
                
                RuleFor(c => c.anoFabricacao).NotEmpty().WithMessage("Ano de fabricação não pode ser vazio")
                           .Must((range, endDate) => endDate > range.anoModelo)
                           .WithMessage("Ano de fabricação não pode ser superior ao ano do modelo");
            }
        }
    }
}