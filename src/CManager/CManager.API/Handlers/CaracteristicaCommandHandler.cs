namespace CManager.API.Handlers
{
    public class CaracteristicaCommandHandler : IRequestHandler<AddCaracteristicaCommand.Command, Unit>
    {
        private readonly CManagerDBContext _dbContext;

        public CaracteristicaCommandHandler(CManagerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Unit> Handle(AddCaracteristicaCommand.Command request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}