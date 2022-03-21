using AutoMapper;
using Contracts;
using Entities.DTO.Product;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetProducts()
        {
            var products = await _repository.Product.GetAllProductsAsync(trackChanges: false);

            var productsDTO = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productsDTO);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        [ServiceFilter(typeof(ValidateProductExistsAttribute))]
        public IActionResult GetProduct(Guid id)
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

            return CreatedAtRoute("GetProductById", new { id = productToReturn.Id },
                                    productToReturn);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateProductExistsAttribute))]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = HttpContext.Items["product"] as Product;

            _repository.Product.DeleteProduct(product);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateProductExistsAttribute))]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductForUpdateDTO product)
        {
            var productEntity = HttpContext.Items["product"] as Product;

            _mapper.Map(product, productEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
