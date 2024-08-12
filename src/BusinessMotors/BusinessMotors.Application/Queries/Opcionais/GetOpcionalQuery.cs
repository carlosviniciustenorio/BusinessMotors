using Microsoft.AspNetCore.Mvc;

namespace BusinessMotors.Application.Queries
{
    public class GetOpcionalQuery
    {
        public sealed record Opcional([FromRoute]int id, [FromQuery]string nome = "") : IRequest<OpcionalResponse>;

        public sealed class Validator : AbstractValidator<Opcional>
        {
            public Validator()
            {
                RuleFor(d => d.id)
                .NotEmpty()
                .WithMessage("Informe o id do opcional")
                .WithErrorCode(StatusCodes.Status400BadRequest.ToString());
            }
        }
    }
}