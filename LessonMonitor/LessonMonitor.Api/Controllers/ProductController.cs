using LessonMonitor.Api.BusinessLogic;
using LessonMonitor.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LessonMonitor.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        public ProductController()
        {
            ProductLogic = new ProductLogic();
        }

        public IProductLogic ProductLogic { get; set; }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return ProductLogic.GetProducts().ToArray(); 
        }

        [HttpPost("123")]
        public IActionResult Post(string productName, double productVolume)
        {
            var product = ProductLogic.CreateProduct(productName, productVolume);
            var products = ProductLogic.GetProducts();
            return Ok(products.ToArray());
        }
    }
}
