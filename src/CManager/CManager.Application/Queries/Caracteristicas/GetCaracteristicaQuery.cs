using Microsoft.AspNetCore.Mvc;

namespace CManager.Application.Queries
{
    public static class GetCaracteristicaQuery
    {
        public sealed record Caracteristica([FromRoute]int id, [FromQuery] string nome = "") : IRequest<CaracteristicaResponse>;

        public sealed class Validator : AbstractValidator<Caracteristica>
        {
            public Validator()
            {
                RuleFor(d => d.id)
                .NotEmpty()
                .WithMessage("Informe o id da característica")
                .WithErrorCode(StatusCodes.Status400BadRequest.ToString());
            }
        }
    }
}