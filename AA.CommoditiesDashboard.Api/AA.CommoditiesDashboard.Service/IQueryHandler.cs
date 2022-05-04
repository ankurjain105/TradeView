using System.Threading.Tasks;

namespace AA.CommoditiesDashboard.Service
{
    public interface IQueryHandler<TQuery, TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}