namespace CManager.Application.Handlers
{
    public class OpcionalCommandHandler : IRequestHandler<AddOpcionalCommand.Command, Unit>
    {
        private readonly IOpcionalRepository _opcionalRepository;

        public OpcionalCommandHandler(IOpcionalRepository opcionalRepository)
        {
            _opcionalRepository = opcionalRepository;
        }

        public async Task<Unit> Handle(AddOpcionalCommand.Command request, CancellationToken cancellationToken)
        {
            await _opcionalRepository.AddAsync(new Opcional(request.descricao));
            return Unit.Value;
        }
    }
}