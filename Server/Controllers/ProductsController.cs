using AutoMapper;
using Contracts;
using Entities.Dto.Product;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using Server.ActionFilters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Server.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/products"), Authorize]
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

        /// <summary>
        /// Get available queries 
        /// </summary>
        /// <param></param>
        /// <returns>All avaiable queries in header</returns>
        /// <response code="200">Returns all avaiable queries in header</response>
        /// <response code="401">Not authorized</response>
        [HttpOptions]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public IActionResult GetProductsOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>One product</returns>
        /// <response code="200">Returns one product</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Product with this id not found</response>
        /// <response code="500">Server error</response>
        [HttpGet("{productId}", Name = "GetProductById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ServiceFilter(typeof(ValidateProductExistsAttribute))]
        public IActionResult GetProduct(Guid productId)
        {
            var product = HttpContext.Items["product"] as Product;

            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

        /// <summary>
        /// Gets the list of all products
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns> list of products</returns>
        /// <response code="200">Returns all products</response>
        /// <response code="401">Not authorized</response>
        /// <response code="500">Server error</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetProducts([FromQuery] ProductParameters parameters)
        {
            var products = await _repository.Product
                .GetAllProductsAsync(parameters, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(products.MetaData));

            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);

            return Ok(productsDto);
        }

        /// <summary>
        /// Create new product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>A newly created product</returns>
        /// <response code="201">Returns the newly product</response>
        /// <response code="400">ProductForCreationDto is null</response>
        /// <response code="401">Not authorized</response>
        /// <response code="422">ProductForCreationDto is invalid</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateProduct([FromBody] ProductForCreationDto product)
        {
            var productEntity = _mapper.Map<Product>(product);

            _repository.Product.CreateProduct(productEntity);
            await _repository.SaveAsync();

            var productToReturn = _mapper.Map<ProductDto>(productEntity);

            return CreatedAtRoute(
                "GetProductById",
                new { productId = productToReturn.Id },
                productToReturn);
        }

        /// <summary>
        /// Delete product by id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        /// <response code="204">Product deleted</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Product with this id not found</response>
        /// <response code="500">Server error</response>
        [HttpDelete("{productId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(ValidateProductExistsAttribute))]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            var product = HttpContext.Items["product"] as Product;

            _repository.Product.DeleteProduct(product);
            await _repository.SaveAsync();

            return NoContent();
        }

        /// <summary>
        /// Update product by id
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <response code="204">product updated</response>
        /// <response code="400">ProductForUpdateDto is null</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Product with this id not found</response>
        /// <response code="422">ProductForUpdateDto is invalid</response>
        /// <response code="500">Server error</response>
        [HttpPut("{productId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateProductExistsAttribute))]
        public async Task<IActionResult> UpdateProduct(Guid productId,
            [FromBody] ProductForUpdateDto product)
        {
            var productEntity = HttpContext.Items["product"] as Product;

            _mapper.Map(product, productEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}