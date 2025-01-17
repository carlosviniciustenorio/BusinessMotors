using Microsoft.AspNetCore.Mvc;

namespace BusinessMotors.Application.Queries
{
    public static class GetModeloQuery
    {
        public sealed record Modelo([FromRoute]int id, [FromQuery]string nome = "") : IRequest<ModeloResponse>;

        public sealed class Validator : AbstractValidator<Modelo>
        {
            public Validator()
            {
                RuleFor(d => d.id)
                .NotEmpty()
                .WithMessage("Informe o id do modelo")
                .WithErrorCode(StatusCodes.Status400BadRequest.ToString());
            }
        }
    }
}