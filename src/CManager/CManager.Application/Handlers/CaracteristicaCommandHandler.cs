namespace CManager.Application.Handlers
{
    public class CaracteristicaCommandHandler : IRequestHandler<AddCaracteristicaCommand.Command, Unit>
    {
        private readonly ICaracteristicaRepository _caracteristicaRepository;

        public CaracteristicaCommandHandler(ICaracteristicaRepository caracteristicaRepository)
        {
            _caracteristicaRepository = caracteristicaRepository;
        }

        public async Task<Unit> Handle(AddCaracteristicaCommand.Command request, CancellationToken cancellationToken)
        {
            await _caracteristicaRepository.AddAsync(new Caracteristica(request.descricao));
            return Unit.Value;
        }
    }
}