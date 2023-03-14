namespace CManager.Application.Handlers
{
    public class CaracteristicaCommandHandler : IRequestHandler<AddCaracteristicaCommand.CaracteristicaCommand, Unit>
    {
        private readonly ICaracteristicaRepository _caracteristicaRepository;

        public CaracteristicaCommandHandler(ICaracteristicaRepository caracteristicaRepository)
        {
            _caracteristicaRepository = caracteristicaRepository;
        }

        public async Task<Unit> Handle(AddCaracteristicaCommand.CaracteristicaCommand request, CancellationToken cancellationToken)
        {
            await _caracteristicaRepository.AddAsync(new Caracteristica(request.descricao));
            await _caracteristicaRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}