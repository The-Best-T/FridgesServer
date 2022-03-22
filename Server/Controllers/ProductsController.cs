using AutoMapper;
using Contracts;
using Entities.DTO.Product;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using Server.ActionFilters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Server.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public ProductsController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductParameters parameters)
        {
            var products = await _repository.Product.GetAllProductsAsync(parameters, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(products.MetaData));

            var productsDTO = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productsDTO);
        }

        [HttpGet("{productId}", Name = "GetProductById")]
        [ServiceFilter(typeof(ValidateProductExistsAttribute))]
        public IActionResult GetProduct(Guid productId)
        {
            var product = HttpContext.Items["product"] as Product;

            var productDTO = _mapper.Map<ProductDTO>(product);
            return Ok(productDTO);

        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateProduct([FromBody] ProductForCreationDTO product)
        {
            var productEntity = _mapper.Map<Product>(product);

            _repository.Product.CreateProduct(productEntity);
            await _repository.SaveAsync();

            var productToReturn = _mapper.Map<ProductDTO>(productEntity);

            return CreatedAtRoute("GetProductById", new { productId = productToReturn.Id },
                productToReturn);
        }

        [HttpDelete("{productId}")]
        [ServiceFilter(typeof(ValidateProductExistsAttribute))]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            var product = HttpContext.Items["product"] as Product;

            _repository.Product.DeleteProduct(product);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{productId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateProductExistsAttribute))]
        public async Task<IActionResult> UpdateProduct(Guid productId, [FromBody] ProductForUpdateDTO product)
        {
            var productEntity = HttpContext.Items["product"] as Product;

            _mapper.Map(product, productEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
