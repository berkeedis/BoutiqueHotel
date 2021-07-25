using System.Collections.Generic;
using BoutiqueHotel.entity;

namespace BoutiqueHotel.data.Abstract
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetByIdWithProducts(int categoryId);

        void DeleteFromCategory(int productId, int categoryId);
    }
}