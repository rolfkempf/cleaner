using System.Threading.Tasks;

namespace Application.Common
{
    /// <summary>
    /// Interface for synchronous query handlers in CQRS pattern
    /// </summary>
    public interface IQueryHandler<in TQuery, TResult>
    {
        TResult Handle(TQuery query);
    }

    /// <summary>
    /// Interface for asynchronous query handlers in CQRS pattern
    /// </summary>
    public interface IAsyncQueryHandler<in TQuery, TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
