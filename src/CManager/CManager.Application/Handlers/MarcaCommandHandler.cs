namespace CManager.Application.Handlers
{
    public class MarcaCommandHandler : IRequestHandler<AddMarcaCommand.MarcaCommand, Unit>,
                                       IRequestHandler<GetMarcasQuery.Marcas, List<MarcaResponse>>,
                                       IRequestHandler<GetMarcaQuery.Marca, MarcaResponse>
    {
        private readonly IMarcaRepository _marcaRepository;
        
        public MarcaCommandHandler(IMarcaRepository marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }

        public async Task<Unit> Handle(AddMarcaCommand.MarcaCommand request, CancellationToken cancellationToken)
        {
            await _marcaRepository.AddAsync(new Marca(request.descricao));
            await _marcaRepository.SaveChangesAsync();
            return Unit.Value;
        }

        public async Task<List<MarcaResponse>> Handle(GetMarcasQuery.Marcas request, CancellationToken cancellationToken)
        {
            var marcas = await _marcaRepository.GetListByByQueryAsync(request);
            
            if(!marcas.Any())
                return new List<MarcaResponse>();

            return marcas.Select(d => new MarcaResponse(d)).ToList();
        }

        public async Task<MarcaResponse> Handle(GetMarcaQuery.Marca request, CancellationToken cancellationToken)
        {
            var marca = await _marcaRepository.GetByQueryAsync(request);
            
            if(marca is null)
                throw new InvalidDataException();

            return new MarcaResponse(marca);
        }
    }
}