namespace CManager.Application.Handlers
{
    public class ModeloCommandHandler : IRequestHandler<AddModeloCommand.ModeloCommand, Unit>
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
    }
}