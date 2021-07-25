using System.Collections.Generic;
using BoutiqueHotel.entity;

namespace BoutiqueHotel.data.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        Product GetProductDetails(string url);
        Product GetByIdWithCategories(int id);
        List<Product> GetProductsByCategory(string name, int page, int pageSize);
        List<Product> GetHomePageProducts();
        List<Product> GetSearchResult(string searchString);
        int GetCountByCategory(string category);
        void Update(Product entity, int[] categoryId);

    }
}