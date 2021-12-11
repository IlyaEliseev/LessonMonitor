using LessonMonitor.Api.DAL;
using LessonMonitor.Api.Models;
using System.Collections.Generic;

namespace LessonMonitor.Api.BusinessLogic
{
    public class ProductLogic : IProductLogic
    {
        public ProductLogic()
        {
            UnitOfWork = new UnitOfWork();
        }

        public IUnitOfWork UnitOfWork { get; }

        public Product CreateProduct(string nameProduct, double volumeProduct)
        {
            Product product = new Product()
            {
                Name = nameProduct,
                Volume = volumeProduct
            };
            UnitOfWork.ProductRepository.Add(product);
            product.IdInProductList = UnitOfWork.ProductRepository.GetCount();
            return product;
        }

        public void AddProduct(Product product)
        {
            UnitOfWork.ProductRepository.Add(product);
        }

        public IEnumerable<Product> GetProducts()
        {
            return UnitOfWork.ProductRepository.GetAll();
        }
    }
}
