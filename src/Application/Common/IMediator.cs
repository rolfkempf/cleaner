namespace Application.Common
{
    public interface IMediator
    {
        TResult Send<TQuery, TResult>(TQuery query);
    }
}