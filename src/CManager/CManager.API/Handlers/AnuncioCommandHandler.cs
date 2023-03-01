namespace CManager.API.Handlers
{
    public class AnuncioCommandHandler : IRequestHandler<AddAnuncioCommand.Command, Unit>
    {
        private readonly CManagerDBContext _dbContext;

        public AnuncioCommandHandler(CManagerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Unit> Handle(AddAnuncioCommand.Command request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}