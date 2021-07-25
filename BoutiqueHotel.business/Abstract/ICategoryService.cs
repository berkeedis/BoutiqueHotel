using System.Collections.Generic;
using BoutiqueHotel.entity;

namespace BoutiqueHotel.business.Abstract
{
    public interface ICategoryService : IValidationService<Category>
    {
        Category GetById(int id);

        Category GetByIdWithProducts(int categoryId);
        List<Category> GetAll();

        void Create(Category entity);

        void Update(Category entity);
        void Delete(Category entity);

        void DeleteFromCategory(int productId, int categoryId);
    }
}