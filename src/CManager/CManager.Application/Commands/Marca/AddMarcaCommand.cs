using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CManager.Application.Commands
{
    public static class AddMarcaCommand
    {
        public sealed record MarcaCommand(string descricao) : IRequest<Unit>;

        public sealed class Validator : AbstractValidator<MarcaCommand>
        {
            public Validator()
            {
                RuleFor(c => c.descricao)
                    .NotEmpty().WithMessage("Descrição não pode ser vazio");
            }
        }
    }
}