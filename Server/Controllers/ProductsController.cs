using AutoMapper;
using Contracts;
using Entities.DTO.Product;
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
        public IActionResult Get()
        {
            var products = _repository.Product.GetAllProducts(trackChanges: false);
            var productsDTO = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(productsDTO);
        }
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var product = _repository.Product.GetProduct(id, trackChanges: false);
            if (product == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var productDTO = _mapper.Map<ProductDTO>(product);
                return Ok(productDTO);
            }
        }
    }
}
