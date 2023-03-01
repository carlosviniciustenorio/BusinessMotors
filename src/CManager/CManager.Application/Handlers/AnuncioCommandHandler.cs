namespace CManager.Application.Handlers
{
    public class AnuncioCommandHandler : IRequestHandler<AddAnuncioCommand.Command, Unit>
    {
        
        public AnuncioCommandHandler()
        {
        }

        public Task<Unit> Handle(AddAnuncioCommand.Command request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}