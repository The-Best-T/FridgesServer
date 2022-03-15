using AutoMapper;
using Contracts;
using Entities.DTO.Product;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Server.Controllers
{
    [Route("api")]
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
        [Route("products")]
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _repository.Product.GetAllProducts(trackChanges: false);
            var productsDTO = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(productsDTO);
        }
        [Route("products/{id}")]
        [HttpGet]
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
        [Route("models/{modelId}/fridges/{fridgeId}/products")]
        [HttpGet]
        public IActionResult GetProductsForFridge(Guid modelId, Guid fridgeId)
        {
            var model = _repository.FridgeModel.GetFridgeModel(modelId, trackChanges: false);
            if (model == null)
            {
                _logger.LogInfo($"Model with id: {modelId} doesn't exist in the database.");
                return NotFound();
            }
            var fridge = _repository.Fridge.GetFridgeForModel(modelId, fridgeId, trackChanges: true);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {modelId} doesn't exist in the database.");
                return NotFound();
            }
            var fridgeProducts = fridge.FridgeProducts;
            var FridgeProductsDTO = _mapper.Map<IEnumerable<FridgeProductDTO>>(fridgeProducts);
            return Ok(FridgeProductsDTO);
        }
        [Route("models/{modelId}/fridges/{fridgeId}/products/{id}")]
        [HttpGet]
        public IActionResult GetProductForFridge(Guid modelId, Guid fridgeId, Guid id)
        {

            var model = _repository.FridgeModel.GetFridgeModel(modelId, trackChanges: false);
            if (model == null)
            {
                _logger.LogInfo($"Model with id: {modelId} doesn't exist in the database.");
                return NotFound();
            }
            var fridge = _repository.Fridge.GetFridgeForModel(modelId, fridgeId, trackChanges: true);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {modelId} doesn't exist in the database.");
                return NotFound();
            }
            var fridgeProduct=fridge.FridgeProducts
                              .SingleOrDefault(p => p.ProductId.Equals(id));
            if (fridgeProduct == null)
            {
                _logger.LogInfo($"Product with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var fridgeProductDTO = _mapper.Map<FridgeProductDTO>(fridgeProduct);
                return Ok(fridgeProductDTO);
            }
        }
    }
}
