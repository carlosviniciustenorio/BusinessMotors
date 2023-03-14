namespace CManager.Application.Handlers
{
    public class TipoCombustivelCommandHandler : IRequestHandler<AddTipoCombustivelCommand.TipoCombustivelCommand, Unit>
    {
        private readonly ITipoCombustivelRepository _tipoCombustivelRepository;

        public TipoCombustivelCommandHandler(ITipoCombustivelRepository tipoCombustivelRepository)
        {
            _tipoCombustivelRepository = tipoCombustivelRepository;
        }

        public async Task<Unit> Handle(AddTipoCombustivelCommand.TipoCombustivelCommand request, CancellationToken cancellationToken)
        {
            await _tipoCombustivelRepository.AddAsync(new TipoCombustivel(request.descricao));
            await _tipoCombustivelRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}