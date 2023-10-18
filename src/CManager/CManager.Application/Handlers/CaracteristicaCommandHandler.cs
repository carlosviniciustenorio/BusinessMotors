namespace CManager.Application.Handlers
{
    public class CaracteristicaCommandHandler : IRequestHandler<AddCaracteristicaCommand.CaracteristicaCommand, Unit>,
                                                IRequestHandler<GetCaracteristicaQuery.Caracteristica, CaracteristicaResponse>,
                                                IRequestHandler<GetCaracteristicasQuery.Caracteristicas, List<CaracteristicaResponse>>
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

        public async Task<CaracteristicaResponse> Handle(GetCaracteristicaQuery.Caracteristica request, CancellationToken cancellationToken)
        {
            var caracteristica = await _caracteristicaRepository.GetByQueryAsync(request);
            
            if(caracteristica is null)
                throw new InvalidDataException();

            return new CaracteristicaResponse(caracteristica);
        }

        public async Task<List<CaracteristicaResponse>> Handle(GetCaracteristicasQuery.Caracteristicas request, CancellationToken cancellationToken)
        {
            var caracteristicas = await _caracteristicaRepository.GetListByQueryAsync(request);
            
            if(!caracteristicas.Any())
                return new List<CaracteristicaResponse>();

            return caracteristicas.Select(d => new CaracteristicaResponse(d)).ToList();
        }
    }
}