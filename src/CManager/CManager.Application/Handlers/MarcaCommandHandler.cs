namespace CManager.Application.Handlers
{
    public class MarcaCommandHandler : IRequestHandler<AddMarcaCommand.MarcaCommand, Unit>
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
    }
}