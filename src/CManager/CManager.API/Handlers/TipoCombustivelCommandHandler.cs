namespace CManager.API.Handlers
{
    public class TipoCombustivelCommandHandler : IRequestHandler<AddTipoCombustivelCommand.Command, Unit>
    {
        private readonly CManagerDBContext _dbContext;

        public TipoCombustivelCommandHandler(CManagerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Unit> Handle(AddTipoCombustivelCommand.Command request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}