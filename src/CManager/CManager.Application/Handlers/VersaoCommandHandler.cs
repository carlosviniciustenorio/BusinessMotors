namespace CManager.Application.Handlers
{
    public class VersaoCommandHandler : IRequestHandler<AddVersaoCommand.VersaoCommand, Unit>
    {
        private readonly IModeloRepository _modeloRepository;
        private readonly IVersaoRepository _versaoRepository;

        public VersaoCommandHandler(IModeloRepository modeloRepository, IVersaoRepository versaoRepository)
        {
            _modeloRepository = modeloRepository;
            _versaoRepository = versaoRepository;
        }

        public async Task<Unit> Handle(AddVersaoCommand.VersaoCommand request, CancellationToken cancellationToken)
        {
            Modelo modelo = await _modeloRepository.GetByIdAsync(request.idModelo);
            if(modelo is null)
                throw new InvalidOperationException("Modelo informado não localizado");

            await _versaoRepository.AddAsync(new(request.descricao, modelo));
            await _versaoRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}