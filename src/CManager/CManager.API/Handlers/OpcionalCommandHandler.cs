namespace CManager.API.Handlers
{
    public class OpcionalCommandHandler : IRequestHandler<AddOpcionalCommand.Command, Unit>
    {
        private readonly CManagerDBContext _dbContext;

        public OpcionalCommandHandler(CManagerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Unit> Handle(AddOpcionalCommand.Command request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}