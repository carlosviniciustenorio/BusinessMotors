using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CManager.Application.Commands
{
    public static class AddVersaoCommand
    {
        public sealed record VersaoCommand(string descricao, int idModelo) : IRequest<Unit>;

        public sealed class Validator : AbstractValidator<VersaoCommand>
        {
            public Validator()
            {
                RuleFor(c => c.idModelo)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Id Marca não pode ser vazio");

                RuleFor(c => c.descricao)
                    .NotNull().NotEmpty().WithMessage("Descrição não pode ser vazio");
            }
        }
    }
}