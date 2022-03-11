using AutoMapper;
using DataAccessLayer.Repositories.Abstract;
using Entities.Models;
using API.Models.InputModels.Products;
using API.Models.OutputModels.Products;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository,IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Get selected product
        /// </summary>
        /// <param name="id">Product identifier</param>
        /// <returns>Product details</returns>
        [SwaggerResponse(HttpStatusCode.OK,"Product Details",typeof(ProductOutputModel))]
        [SwaggerResponse(HttpStatusCode.NotFound,"Empty object")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var product = await _productRepository.GetProductAsync(id);

            if (product == null)
                return NotFound();

            var outputProduct = _mapper.Map<ProductOutputModel>(product);
            return Ok(outputProduct);
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>List of products</returns>
        public async Task<IHttpActionResult> GetAll()
        {
            var products = await _productRepository.GetProductsAsync();

            if (products == null || !products.Any())
                return NotFound();

            var outputProducts = new List<ProductOutputModel>();

            foreach (var product in products)
            {
                outputProducts.Add(new ProductOutputModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    CategoryName = product.Category?.Name
                });

            }
            return Ok(outputProducts);
        }

        /// <summary>
        /// Create new product
        /// </summary>
        /// <param name="model">Product details</param>
        /// <returns>Status</returns>
        public async Task<IHttpActionResult> Post(ProductInputModel model)
        {
            if (model == null)
                return BadRequest("Model can't be a null");

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newProduct = new Product()
            {
                Name = model.Name,
                Price = model.Price,
                CategoryId = model.CategoryId
            };

            var result = await _productRepository.SaveProductAsync(newProduct);

            if (!result)
                return InternalServerError();

            return Ok();
        }

        /// <summary>
        /// Update existing product
        /// </summary>
        /// <param name="id">Product identifier</param>
        /// <param name="model">Product details</param>
        /// <returns>Status</returns>
        public async Task<IHttpActionResult> Put(int id, ProductInputModel model)
        {
            if (model == null)
                return BadRequest("Model can't be a null");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _productRepository.GetProductAsync(id);

            if (product == null)
                return NotFound();

            product.Name = model.Name;
            product.Price = model.Price;
            product.CategoryId = model.CategoryId;

            var result = await _productRepository.SaveProductAsync(product);

            if (!result)
                return InternalServerError();

            return Ok();
        }

        /// <summary>
        /// Delete existing product
        /// </summary>
        /// <param name="id">Product identifier</param>
        /// <returns>Status</returns>
        public async Task<IHttpActionResult> Delete(int id)
        {         
            var product = await _productRepository.GetProductAsync(id);

            if (product == null)
                return NotFound();

            var result = await _productRepository.DeleteProductAsync(product);

            if (!result)
                return InternalServerError();

            return Ok();
        }


    }
}
