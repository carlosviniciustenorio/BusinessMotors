using Microsoft.AspNetCore.Mvc;

namespace CManager.Application.Queries
{
    public static class GetMarcaQuery
    {
        public sealed record Marca([FromRoute]int id, [FromQuery]string nome = "") : IRequest<MarcaResponse>;

        public sealed class Validator : AbstractValidator<Marca>
        {
            public Validator()
            {
                RuleFor(d => d.id)
                .NotEmpty()
                .WithMessage("Informe o id da marca")
                .WithErrorCode(StatusCodes.Status400BadRequest.ToString());
            }
        }
    }
}