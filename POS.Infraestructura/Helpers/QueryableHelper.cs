using POS.Infraestructura.Commons.Bases.Request;

namespace POS.Infraestructura.Helpers
{
    public static class QueryableHelper
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, BasePaginationRequest request)
        { 
            return queryable.Skip((request.NumPage-1) * request.Records).Take(request.Records);
        }
    }
}
