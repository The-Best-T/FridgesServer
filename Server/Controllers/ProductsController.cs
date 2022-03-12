using Contracts;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
namespace Server.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repositoryManager;
        public ProductsController(ILoggerManager logger, IRepositoryManager repository)
        {
            _logger = logger;
            _repositoryManager = repository;
            
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var products = _repositoryManager.Product.GetAllProducts(trackChanges: false);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(Get)}action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
