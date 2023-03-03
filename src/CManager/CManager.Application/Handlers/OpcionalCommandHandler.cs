namespace CManager.Application.Handlers
{
    public class OpcionalCommandHandler : IRequestHandler<AddOpcionalCommand.OpcionalCommand, Unit>
    {
        private readonly IOpcionalRepository _opcionalRepository;

        public OpcionalCommandHandler(IOpcionalRepository opcionalRepository)
        {
            _opcionalRepository = opcionalRepository;
        }

        public async Task<Unit> Handle(AddOpcionalCommand.OpcionalCommand request, CancellationToken cancellationToken)
        {
            await _opcionalRepository.AddAsync(new Opcional(request.descricao));
            await _opcionalRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}