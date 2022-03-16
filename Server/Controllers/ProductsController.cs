using AutoMapper;
using Contracts;
using Entities.DTO.Product;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Models;
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
        [HttpGet(Name ="ProductById")]
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
        [Route("fridgemodels/{fridgeModelId}/fridges/{fridgeId}/products")]
        [HttpGet]
        public IActionResult GetProductsForFridge(Guid fridgeModelId, Guid fridgeId)
        {
            var fridgeModel = _repository.FridgeModel.GetFridgeModel(fridgeModelId, trackChanges: false);
            if (fridgeModel == null)
            {
                _logger.LogInfo($"Model with id: {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }
            var fridge = _repository.Fridge.GetFridgeForModel(fridgeModelId, fridgeId, trackChanges: true);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {fridgeId} doesn't exist in the database.");
                return NotFound();
            }
            var fridgeProducts = fridge.FridgeProducts;
            var FridgeProductsDTO = _mapper.Map<IEnumerable<FridgeProductDTO>>(fridgeProducts);
            return Ok(FridgeProductsDTO);
        }
        [Route("fridgemodels/{fridgeModelId}/fridges/{fridgeId}/products/{id}")]
        [HttpGet]
        public IActionResult GetProductForFridge(Guid fridgeModelId, Guid fridgeId, Guid id)
        {

            var fridgeModel = _repository.FridgeModel.GetFridgeModel(fridgeModelId, trackChanges: false);
            if (fridgeModel == null)
            {
                _logger.LogInfo($"Model with id: {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }
            var fridge = _repository.Fridge.GetFridgeForModel(fridgeModelId, fridgeId, trackChanges: true);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {fridgeId} doesn't exist in the database.");
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
        [Route("products")]
        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductForCreationDTO product)
        {
            if (product==null)
            {
                _logger.LogError("ProductForCreationDTO object sent from client is null.");
                return BadRequest("ProductForCreationDTO object is null");
            }
            var productEntity = _mapper.Map<Product>(product);

            _repository.Product.CreateProduct(productEntity);
            _repository.Save();

            var productToReturn=_mapper.Map<ProductDTO>(productEntity);

            return CreatedAtRoute("ProductById", new { id = productToReturn.Id },
                                    productToReturn);
        }
    }
}
