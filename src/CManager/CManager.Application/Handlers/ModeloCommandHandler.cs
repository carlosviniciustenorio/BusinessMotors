namespace CManager.Application.Handlers
{
    public class ModeloCommandHandler : IRequestHandler<AddModeloCommand.ModeloCommand, Unit>,
                                        IRequestHandler<GetModeloQuery.Modelo, ModeloResponse>,
                                        IRequestHandler<GetModelosQuery.Modelos, List<ModeloResponse>>
    {
        private readonly IModeloRepository _modeloRepository;
        private readonly IMarcaRepository _marcaRepository;

        public ModeloCommandHandler(IModeloRepository modeloRepository, IMarcaRepository marcaRepository)
        {
            _modeloRepository = modeloRepository;
            _marcaRepository = marcaRepository;
        }

        public async Task<Unit> Handle(AddModeloCommand.ModeloCommand request, CancellationToken cancellationToken)
        {
            Marca marca = await _marcaRepository.GetByQueryAsync(new GetMarcaQuery.Marca(request.idMarca));
            if(marca is null)
                throw new InvalidOperationException("Marca informada não localizada");

            await _modeloRepository.AddAsync(new(request.descricao, marca));
            await _modeloRepository.SaveChangesAsync();
            return Unit.Value;
        }

        public async Task<ModeloResponse> Handle(GetModeloQuery.Modelo request, CancellationToken cancellationToken)
        {
            var modelo = await _modeloRepository.GetByQueryAsync(request);
            
            if(modelo is null)
                throw new InvalidDataException();

            return new ModeloResponse(modelo);
        }

        public async Task<List<ModeloResponse>> Handle(GetModelosQuery.Modelos request, CancellationToken cancellationToken)
        {
             var modelos = await _modeloRepository.GetListByQueryAsync(request);
            
            if(!modelos.Any())
                return new List<ModeloResponse>();

            return modelos.Select(d => new ModeloResponse(d)).ToList();
        }
    }
}