using System.Collections.Generic;
using BoutiqueHotel.entity;

namespace BoutiqueHotel.business.Abstract
{
    public interface IProductService : IValidationService<Product>
    {
        Product GetById(int id);
        Product GetByIdWithCategories(int id);
        Product GetProductDetails(string url);
        int GetCountByCategory(string category);
        List<Product> GetProductsByCategory(string name, int page, int pageSize);
        List<Product> GetAll();
        List<Product> GetHomePageProducts();
        List<Product> GetSearchResult(string searchString);

        bool Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
        bool Update(Product entity, int[] categoryId);
    }
}