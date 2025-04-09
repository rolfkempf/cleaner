using System.Threading.Tasks;

namespace Application.Common
{
    /// <summary>
    /// Interface for synchronous command handlers in CQRS pattern
    /// </summary>
    public interface ICommandHandler<in TCommand>
    {
        void Handle(TCommand command);
    }

    /// <summary>
    /// Interface for asynchronous command handlers in CQRS pattern
    /// </summary>
    public interface IAsyncCommandHandler<in TCommand>
    {
        Task HandleAsync(TCommand command);
    }
}
