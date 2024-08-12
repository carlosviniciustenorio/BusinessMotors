using Microsoft.AspNetCore.Mvc;

namespace BusinessMotors.Application.Queries
{
    public static class GetTipoCombustivelQuery
    {
        public sealed record TipoCombustivel([FromRoute]int id, [FromQuery]string nome = "") : IRequest<TipoCombustivelResponse>;

        public sealed class Validator : AbstractValidator<TipoCombustivel>
        {
            public Validator()
            {
                RuleFor(d => d.id)
                .NotEmpty()
                .WithMessage("Informe o id do tipo combustivel")
                .WithErrorCode(StatusCodes.Status400BadRequest.ToString());
            }
        }       
    }
}