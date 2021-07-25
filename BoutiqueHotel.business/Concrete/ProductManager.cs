using System.Collections.Generic;
using BoutiqueHotel.business.Abstract;
using BoutiqueHotel.data.Abstract;
using BoutiqueHotel.entity;

namespace BoutiqueHotel.business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<Product> GetAll()
        {
            return _unitOfWork.Products.GetAll();
        }

        public Product GetById(int id)
        {
            return _unitOfWork.Products.GetById(id);
        }

        public int GetCountByCategory(string category)
        {
            return _unitOfWork.Products.GetCountByCategory(category);
        }

        public List<Product> GetHomePageProducts()
        {
            return _unitOfWork.Products.GetHomePageProducts();
        }

        public Product GetProductDetails(string url)
        {
            return _unitOfWork.Products.GetProductDetails(url);
        }

        public List<Product> GetProductsByCategory(string name, int page, int pageSize)
        {
            return _unitOfWork.Products.GetProductsByCategory(name, page, pageSize);
        }

        public List<Product> GetSearchResult(string searchString)
        {
            return _unitOfWork.Products.GetSearchResult(searchString);
        }
        public Product GetByIdWithCategories(int id)
        {
            return _unitOfWork.Products.GetByIdWithCategories(id);
        }


        public bool Create(Product entity)
        {
            if (Validation(entity))
            {
                _unitOfWork.Products.Create(entity);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public void Delete(Product entity)
        {
            _unitOfWork.Products.Delete(entity);
            _unitOfWork.Save();
        }
        public void Update(Product entity)
        {
            _unitOfWork.Products.Update(entity);
            _unitOfWork.Save();
        }


        public bool Update(Product entity, int[] categoryId)
        {
            if (Validation(entity))
            {
                _unitOfWork.Products.Update(entity, categoryId);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }
        public string ErrorMessage { get; set; }

        public bool Validation(Product entity)
        {
            var isValid = true;

            if (string.IsNullOrEmpty(entity.Name))
            {
                isValid = false;
            }

            if (entity.Price < 0)
            {
                isValid = false;
            }

            return isValid;
        }
    }


}
