
namespace CManager.Application.Commands
{
    public static class AddAnuncioCommand
    {
        public sealed record Command(
            string placa,
            int idMarca,
            int idModelo,
            int idVersao,
            List<int>? idTiposCombustiveis,
            int portas,
            ECambio cambio,
            ECor cor,
            List<int>? idOpcionais, 
            List<int>? idCaracteristicas, 
            string km,
            string estado,
            string usuarioId,
            decimal preco,
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
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Id Usuário não pode ser vazio");
                
                RuleFor(c => c.idMarca)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Id Marca não pode ser vazio");
            }
        }
    }
}