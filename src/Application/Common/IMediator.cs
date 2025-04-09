using System.Threading.Tasks;

namespace Application.Common
{
    public interface IMediator
    {
        // Synchronous methods
        TResult Send<TQuery, TResult>(TQuery query);
        void Send<TCommand>(TCommand command);

        // Asynchronous methods
        Task<TResult> SendAsync<TQuery, TResult>(TQuery query);
        Task SendAsync<TCommand>(TCommand command);
    }
}
