using Contracts;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using Entities.DTO.Product;
using AutoMapper;
using System.Collections.Generic;
using Entities.Models;
namespace Server.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public ProductsController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repositoryManager = repository;
            _mapper = mapper;

        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var products = _repositoryManager.Product.GetAllProducts(trackChanges: false);
                var productsDTO = _mapper.Map<IEnumerable<ProductDTO>>(products);
                return Ok(productsDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(Get)}action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var product = _repositoryManager.Product.GetProduct(id, trackChanges: false);
            if (product == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var companyDTO = _mapper.Map<ProductDTO>(product);
                return Ok(companyDTO);
            }
        }
    }
}
