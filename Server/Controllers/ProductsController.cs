using AutoMapper;
using Contracts;
using Entities.DTO.Product;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
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
        public IActionResult GetProducts()
        {
            var products = _repository.Product.GetAllProducts(trackChanges: false);

            var productsDTO = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productsDTO);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public IActionResult GetProduct(Guid id)
        {
            var product = _repository.Product.GetProduct(id, trackChanges: false);
            if (product == null)
            {
                _logger.LogInfo($"Product with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var productDTO = _mapper.Map<ProductDTO>(product);
                return Ok(productDTO);
            }
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductForCreationDTO product)
        {
            if (product == null)
            {
                _logger.LogError("ProductForCreationDTO object sent from client is null.");
                return BadRequest("ProductForCreationDTO object is null");
            }

            var productEntity = _mapper.Map<Product>(product);

            _repository.Product.CreateProduct(productEntity);
            _repository.Save();

            var productToReturn = _mapper.Map<ProductDTO>(productEntity);

            return CreatedAtRoute("GetProductById", new { id = productToReturn.Id },
                                    productToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            var product = _repository.Product.GetProduct(id, trackChanges: false);
            if (product == null)
            {
                _logger.LogInfo($"Product with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.Product.DeleteProduct(product);
            _repository.Save();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(Guid id, [FromBody] ProductForUpdateDTO product)
        {
            if (product == null)
            {
                _logger.LogError("ProductForUpdateDTO object sent from client is null.");
                return BadRequest("ProductForUpdateDTO object is null");
            }

            var productEntity = _repository.Product.GetProduct(id, trackChanges: true);

            _mapper.Map(product, productEntity);
            _repository.Save();

            return NoContent();
        }
    }
}
