namespace CManager.Application.Queries
{
    public static class GetAnuncioQuery
    {
        public sealed record Anuncio(Guid id) : IRequest<AnuncioResponse>;

        public sealed class Validator : AbstractValidator<Anuncio>
        {
            public Validator()
            {
                RuleFor(c => c.id).NotEmpty().WithMessage("Informe o id do anúncio");
            }
        }
    }
}