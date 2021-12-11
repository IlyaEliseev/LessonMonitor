using LessonMonitor.Api.Models;
using System.Collections.Generic;

namespace LessonMonitor.Api.BusinessLogic
{
    public interface IProductLogic
    {
        void AddProduct(Product product);
        IEnumerable<Product> GetProducts();
        Product CreateProduct(string nameProduct, double volumeProduct);
    }
}