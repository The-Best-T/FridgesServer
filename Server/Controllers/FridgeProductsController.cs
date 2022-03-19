using AutoMapper;
using Contracts;
using Entities.DTO.FridgeProduct;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
namespace Server.Controllers
{
    [Route("api/models/{fridgeModelId}/fridges/{fridgeId}/products")]
    [ApiController]
    public class FridgeProductsController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public FridgeProductsController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProductsForFridge(Guid fridgeModelId, Guid fridgeId)
        {
            var fridgeModel = _repository.FridgeModel.GetFridgeModel(fridgeModelId, trackChanges: false);
            if (fridgeModel == null)
            {
                _logger.LogInfo($"FridgeModel with id: {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

            var fridge = _repository.Fridge.GetFridgeForModel(fridgeModelId, fridgeId, trackChanges: false);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {fridgeId} and modelId {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

            var fridgeProducts = _repository.FridgeProduct.GetProductsForFridge(fridgeId, trakChanges: true);

            var FridgeProductsDTO = _mapper.Map<IEnumerable<FridgeProductDTO>>(fridgeProducts);

            return Ok(FridgeProductsDTO);
        }

        [HttpGet("{id}", Name = "GetProductForFridge")]
        public IActionResult GetProductForFridge(Guid fridgeModelId, Guid fridgeId, Guid id)
        {
            var fridgeModel = _repository.FridgeModel.GetFridgeModel(fridgeModelId, trackChanges: false);
            if (fridgeModel == null)
            {
                _logger.LogInfo($"FridgeModel with id: {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

            var fridge = _repository.Fridge.GetFridgeForModel(fridgeModelId, fridgeId, trackChanges: true);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {fridgeId} and modelId {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

            var fridgeProduct = _repository.FridgeProduct.GetProductForFridge(fridgeId, id, trackChanges: true);
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

        [HttpPost]
        public IActionResult AddProductInFridge(Guid fridgeModelId, Guid fridgeId,
                                                [FromBody] FridgeProductForCreationDTO fridgeProduct)
        {
            if (fridgeProduct == null)
            {
                _logger.LogError("FridgeProductForCreationDTO object sent from client is null.");
                return BadRequest("FridgeProductForCreationDTO object is null");
            }

            var fridgeModel = _repository.FridgeModel.GetFridgeModel(fridgeModelId, trackChanges: false);
            if (fridgeModel == null)
            {
                _logger.LogInfo($"FridgeModel with id: {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

            var fridge = _repository.Fridge.GetFridgeForModel(fridgeModelId, fridgeId, trackChanges: false);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {fridgeId} and modelId {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

            var product = _repository.Product.GetProduct(fridgeProduct.ProductId, trackChanges: false);
            if (product == null)
            {
                _logger.LogInfo($"Product with id: {fridgeProduct.ProductId} doesn't exist in the database.");
                return NotFound();
            }

            var fridgeProductEntity = _mapper.Map<FridgeProduct>(fridgeProduct);

            _repository.FridgeProduct.AddProductInFridge(fridgeId, fridgeProductEntity);
            _repository.Save();

            var fridgeProductToReturn = _mapper.Map<FridgeProductDTO>(fridgeProductEntity);
            fridgeProductToReturn.ProductName = product.Name;

            return CreatedAtRoute(
                "GetProductForFridge",
                 new
                 {
                     fridgeModelId,
                     fridgeId,
                     id = fridgeProductToReturn.ProductId
                 },
                 fridgeProductToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProductFromFridge(Guid fridgeModelId, Guid fridgeId,
                                                     Guid id)
        {
            var fridgeModel = _repository.FridgeModel.GetFridgeModel(fridgeModelId, trackChanges: false);
            if (fridgeModel == null)
            {
                _logger.LogInfo($"FridgeModel with id: {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

            var fridge = _repository.Fridge.GetFridgeForModel(fridgeModelId, fridgeId, trackChanges: false);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {fridgeId} and modelId {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

            var fridgeProduct = _repository.FridgeProduct.GetProductForFridge(fridgeId, id, trackChanges: true);
            if (fridgeProduct == null)
            {
                _logger.LogInfo($"Product with id: {id} in fridge {fridgeId} doesn't exist in the database.");
                return NotFound();
            }

            _repository.FridgeProduct.DeleteProductFromFridge(fridgeProduct);
            _repository.Save();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProductForFridge(Guid fridgeModelId, Guid fridgeId, Guid id,
                                                    [FromBody] FridgeProductForUpdateDTO fridgeProduct)
        {
            if (fridgeProduct == null)
            {
                _logger.LogError("FridgeProductForUpdateDTO object sent from client is null.");
                return BadRequest("FridgeProductForUpdateDTO object is null");
            }

            var fridgeModel = _repository.FridgeModel.GetFridgeModel(fridgeModelId, trackChanges: false);
            if (fridgeModel == null)
            {
                _logger.LogInfo($"FridgeModel with id: {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

            var fridge = _repository.Fridge.GetFridgeForModel(fridgeModelId, fridgeId, trackChanges: false);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {fridgeId} and modelId {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

            var fridgeProductEntity = _repository.FridgeProduct.GetProductForFridge(fridgeId, id, trackChanges: true);
            if (fridgeProductEntity == null)
            {
                _logger.LogInfo($"Product with id: {id} in fridge {fridgeId} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(fridgeProduct, fridgeProductEntity);
            _repository.Save();

            return NoContent();
        }
    }
}
