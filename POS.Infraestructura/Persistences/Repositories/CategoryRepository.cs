using POS.Domain.Entities;
using POS.Infraestructura.Commons.Bases.Request;
using POS.Infraestructura.Commons.Bases.Response;
using POS.Infraestructura.Persistences.Contexts;
using POS.Infraestructura.Persistences.Interfaces;

namespace POS.Infraestructura.Persistences.Repositories
{
    public class CategoryRepository: GenericRepository<Category>, ICategoryRepository
    {
        private readonly POSContext _context;
        public CategoryRepository(POSContext context)
        {
            _context = context;

        }
        public Task<BaseEntityResponse<Category>> ListCategories(BaseFiltersRequest filters)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<Category>> ListSelectCategories()
        {
            throw new NotImplementedException();
        }
        public Task<Category> CategoryById(int categoryId)
        {
            throw new NotImplementedException();
        }
        public Task<bool> RegisterCategory(Category category)
        {
            throw new NotImplementedException();
        }
        public Task<bool> EditCategory(Category category)
        {
            throw new NotImplementedException();
        }
        public Task<bool> RemoveCategory(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
