namespace CManager.Application.Handlers
{
    public class TipoCombustivelCommandHandler : IRequestHandler<AddTipoCombustivelCommand.Command, Unit>
    {
        private readonly ITipoCombustivelRepository _tipoCombustivelRepository;

        public TipoCombustivelCommandHandler(ITipoCombustivelRepository tipoCombustivelRepository)
        {
            _tipoCombustivelRepository = tipoCombustivelRepository;
        }

        public async Task<Unit> Handle(AddTipoCombustivelCommand.Command request, CancellationToken cancellationToken)
        {
            await _tipoCombustivelRepository.AddAsync(new TipoCombustivel(request.descricao));
            return Unit.Value;
        }
    }
}