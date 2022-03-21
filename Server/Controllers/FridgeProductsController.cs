using AutoMapper;
using Contracts;
using Entities.DTO.FridgeProduct;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Server.ActionFilters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetProductsForFridge(Guid fridgeModelId, Guid fridgeId)
        {
            var fridgeModel = await _repository.FridgeModel
                                               .GetFridgeModelAsync(fridgeModelId, trackChanges: false);
            if (fridgeModel == null)
            {
                _logger.LogInfo($"FridgeModel with id: {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

            var fridge = await _repository.Fridge
                                          .GetFridgeForModelAsync(fridgeModelId, fridgeId, trackChanges: false);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {fridgeId} and modelId {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

            var fridgeProducts = await _repository.FridgeProduct
                                                  .GetProductsForFridgeAsync(fridgeId, trakChanges: true);

            var FridgeProductsDTO = _mapper.Map<IEnumerable<FridgeProductDTO>>(fridgeProducts);

            return Ok(FridgeProductsDTO);
        }

        [HttpGet("{id}", Name = "GetProductForFridge")]
        public async Task<IActionResult> GetProductForFridge(Guid fridgeModelId, Guid fridgeId, Guid id)
        {
            var fridgeModel = await _repository.FridgeModel
                                               .GetFridgeModelAsync(fridgeModelId, trackChanges: false);
            if (fridgeModel == null)
            {
                _logger.LogInfo($"FridgeModel with id: {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

            var fridge = await _repository.Fridge
                                          .GetFridgeForModelAsync(fridgeModelId, fridgeId, trackChanges: true);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {fridgeId} and modelId {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

            var fridgeProduct = await _repository.FridgeProduct
                                                 .GetProductForFridgeAsync(fridgeId, id, trackChanges: true);
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
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddProductInFridge(Guid fridgeModelId, Guid fridgeId,
                                                [FromBody] FridgeProductForCreationDTO fridgeProduct)
        {
            var fridgeModel = await _repository.FridgeModel
                                               .GetFridgeModelAsync(fridgeModelId, trackChanges: false);
            if (fridgeModel == null)
            {
                _logger.LogInfo($"FridgeModel with id: {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

            var fridge = await _repository.Fridge
                                          .GetFridgeForModelAsync(fridgeModelId, fridgeId, trackChanges: false);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {fridgeId} and modelId {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

            var product = await _repository.Product
                                           .GetProductAsync(fridgeProduct.ProductId, trackChanges: false);
            if (product == null)
            {
                _logger.LogInfo($"Product with id: {fridgeProduct.ProductId} doesn't exist in the database.");
                return NotFound();
            }

            var fridgeProductEntity = _mapper.Map<FridgeProduct>(fridgeProduct);

            _repository.FridgeProduct.AddProductInFridge(fridgeId, fridgeProductEntity);
            await _repository.SaveAsync();

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
        public async Task<IActionResult> DeleteProductFromFridge(Guid fridgeModelId, Guid fridgeId,
                                                     Guid id)
        {
            var fridgeModel = await _repository.FridgeModel
                                               .GetFridgeModelAsync(fridgeModelId, trackChanges: false);
            if (fridgeModel == null)
            {
                _logger.LogInfo($"FridgeModel with id: {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

            var fridge = await _repository.Fridge
                                          .GetFridgeForModelAsync(fridgeModelId, fridgeId, trackChanges: false);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {fridgeId} and modelId {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

            var fridgeProduct = await _repository.FridgeProduct
                                                 .GetProductForFridgeAsync(fridgeId, id, trackChanges: true);
            if (fridgeProduct == null)
            {
                _logger.LogInfo($"Product with id: {id} in fridge {fridgeId} doesn't exist in the database.");
                return NotFound();
            }

            _repository.FridgeProduct.DeleteProductFromFridge(fridgeProduct);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateProductForFridge(Guid fridgeModelId, Guid fridgeId, Guid id,
                                                                [FromBody] FridgeProductForUpdateDTO fridgeProduct)
        {
            var fridgeModel = await _repository.FridgeModel
                                               .GetFridgeModelAsync(fridgeModelId, trackChanges: false);
            if (fridgeModel == null)
            {
                _logger.LogInfo($"FridgeModel with id: {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

            var fridge = await _repository.Fridge
                                          .GetFridgeForModelAsync(fridgeModelId, fridgeId, trackChanges: false);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {fridgeId} and modelId {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

            var fridgeProductEntity = await _repository.FridgeProduct
                                                       .GetProductForFridgeAsync(fridgeId, id, trackChanges: true);
            if (fridgeProductEntity == null)
            {
                _logger.LogInfo($"Product with id: {id} in fridge {fridgeId} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(fridgeProduct, fridgeProductEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
