using Microsoft.AspNetCore.Mvc;

namespace BusinessMotors.Application.Queries;
public static class GetAnuncioQuery
{
    public sealed record Anuncio : IRequest<AnuncioResponse>
    {
        [FromRoute(Name = "id")]
        public Guid Id { get; init; }
    }

    public sealed class Validator : AbstractValidator<Anuncio>
    {
        public Validator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Informe o id do anúncio");
        }
    }
}