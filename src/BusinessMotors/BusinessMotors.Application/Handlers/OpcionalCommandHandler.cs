namespace BusinessMotors.Application.Handlers
{
    public class OpcionalCommandHandler : IRequestHandler<AddOpcionalCommand.OpcionalCommand, Unit>,
                                          IRequestHandler<GetOpcionaisQuery.Opcionais, List<OpcionalResponse>>,
                                          IRequestHandler<GetOpcionalQuery.Opcional, OpcionalResponse>
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

        public async Task<List<OpcionalResponse>> Handle(GetOpcionaisQuery.Opcionais request, CancellationToken cancellationToken)
        {
            var opcionais = await _opcionalRepository.GetListByQueryAsync(request);
            
            if(!opcionais.Any())
                return new List<OpcionalResponse>();

            return opcionais.Select(d => new OpcionalResponse(d)).ToList();
        }

        public async Task<OpcionalResponse> Handle(GetOpcionalQuery.Opcional request, CancellationToken cancellationToken)
        {
            var opcional = await _opcionalRepository.GetByQueryAsync(request);
            
            if(opcional is null)
                throw new InvalidDataException();

            return new OpcionalResponse(opcional);
        }
    }
}