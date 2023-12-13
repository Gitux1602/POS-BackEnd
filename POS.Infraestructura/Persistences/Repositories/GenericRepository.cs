using POS.Infraestructura.Commons.Bases.Request;
using POS.Infraestructura.Helpers;
using POS.Infraestructura.Persistences.Interfaces;
using System.Linq.Dynamic.Core;

namespace POS.Infraestructura.Persistences.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected IQueryable<TDTO> Ordering<TDTO>(BasePaginationRequest request, IQueryable<TDTO> queryable, bool pagination = false) where TDTO : class
        {
            IQueryable<TDTO> queryDto = request.Order == "desc" ? queryable.OrderBy($"{request.Sort} descending") : queryable.OrderBy($"{request.Sort} ascending");

            if (pagination) queryDto = queryDto.Paginate(request);
            return queryDto;
        }
    }
}
