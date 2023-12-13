using Microsoft.EntityFrameworkCore;
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
        public async Task<BaseEntityResponse<Category>> ListCategories(BaseFiltersRequest filters)
        {
           var response = new BaseEntityResponse<Category>();
           //Trae los registros de categoria que no hayan sido eliminados por algun usuario
           var categories = (from c in _context.Categories where c.AuditDeleteUser == null && c.AuditDeleteDate == null
                             select c).AsNoTracking().AsQueryable();
            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter)) 
            {
                switch (filters.NumFilter)
                {
                    case 1: //Filtrando por nombre
                        categories = categories.Where(x => x.Name!.Contains(filters.TextFilter));
                        break;
                    case 2:
                        categories = categories.Where(x => x.Description!.Contains(filters.TextFilter));
                        break;
                }
            }
            if (filters.StateFilter is not null)
            {
                categories = categories.Where(x => x.State.Equals(filters.StateFilter));
            }
            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                categories = categories.Where(x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate) && x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }
            if (filters.Sort is null) filters.Sort = "CategoryId";
            {
                categories = categories.Where(x => x.State.Equals(filters.StateFilter));
            }

            response.TotalRecords = await categories.CountAsync();
            response.Items = await Ordering(filters, categories, !(bool)filters.Download!).ToListAsync();
            return response;
        }


        public  async Task<IEnumerable<Category>> ListSelectCategories()
        {
            var categories = await _context.Categories.Where(x => x.State.Equals(1) && x.AuditDeleteUser == null && x.AuditDeleteDate == null).AsNoTracking().ToListAsync();
            return categories;
        }
        public async Task<Category> CategoryById(int categoryId)
        {
            var category = await _context.Categories!.AsNoTracking().FirstOrDefaultAsync(x => x.State.Equals(categoryId));
            return category!;
        }
        public async Task<bool> RegisterCategory(Category category)
        {
            category.AuditCreateUser = 1;
            category.AuditCreateDate = DateTime.Now;

            await _context.AddAsync(category);

            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }
        public async Task<bool> EditCategory(Category category)
        {
            category.AuditUpdateUser = 1;
            category.AuditUpdateDate = DateTime.Now;

            _context.Update(category);
            _context.Entry(category).Property(x => x.AuditCreateUser).IsModified = false;
            _context.Entry(category).Property(x => x.AuditCreateDate).IsModified = false;

            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }
        public async Task<bool> RemoveCategory(int categoryId)
        {
            var category = await _context.Categories.AsNoTracking().SingleOrDefaultAsync(x => x.CategoryId.Equals(categoryId));
            category!.AuditDeleteUser = 1;
            category.AuditDeleteDate = DateTime.Now;

            _context.Update(category);

            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }
    }
}
