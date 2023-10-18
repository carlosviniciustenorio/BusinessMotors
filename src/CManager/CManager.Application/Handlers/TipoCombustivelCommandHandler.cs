namespace CManager.Application.Handlers
{
    public class TipoCombustivelCommandHandler : IRequestHandler<AddTipoCombustivelCommand.TipoCombustivelCommand, Unit>,
                                                 IRequestHandler<GetTiposCombustiveisQuery.TiposCombustiveis, List<TipoCombustivelResponse>>,
                                                 IRequestHandler<GetTipoCombustivelQuery.TipoCombustivel, TipoCombustivelResponse>
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

        public async Task<TipoCombustivelResponse> Handle(GetTipoCombustivelQuery.TipoCombustivel request, CancellationToken cancellationToken)
        {
            var tipoCombustivel = await _tipoCombustivelRepository.GetByQueryAsync(request);
            
            if(tipoCombustivel is null)
                throw new InvalidDataException();

            return new TipoCombustivelResponse(tipoCombustivel);
        }

        public async Task<List<TipoCombustivelResponse>> Handle(GetTiposCombustiveisQuery.TiposCombustiveis request, CancellationToken cancellationToken)
        {
            var tiposCombustiveis = await _tipoCombustivelRepository.GetListByQueryAsync(request);
            
            if(!tiposCombustiveis.Any())
                return new List<TipoCombustivelResponse>();

            return tiposCombustiveis.Select(d => new TipoCombustivelResponse(d)).ToList();
        }
    }
}