using System.Linq;
using BoutiqueHotel.data.Abstract;
using BoutiqueHotel.entity;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueHotel.data.Concrete.EfCore
{
    public class EfCoreCategoryRepository : EfCoreGenericRepository<Category>, ICategoryRepository
    {
        public EfCoreCategoryRepository(ShopContext context) : base(context)
        {

        }
        private ShopContext ShopContext
        {
            get { return context as ShopContext; }
        }
        public void DeleteFromCategory(int categoryId, int productId)
        {
            var cmd = "delete from productcategory where ProductId=@p0 and CategoryId=@p1";
            ShopContext.Database.ExecuteSqlRaw(cmd, productId, categoryId);
        }

        public Category GetByIdWithProducts(int categoryId)
        {
            return ShopContext.Categories
                             .Where(i => i.CategoryId == categoryId)
                             .Include(i => i.ProductCategories)
                             .ThenInclude(i => i.Product)
                             .FirstOrDefault();
        }
    }
}