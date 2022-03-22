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
        [ServiceFilter(typeof(ValidateFridgeExistsAttribute))]
        public async Task<IActionResult> GetProductsForFridge(Guid fridgeModelId, Guid fridgeId)
        {
            var fridgeProducts = await _repository.FridgeProduct
                                                  .GetProductsForFridgeAsync(fridgeId, trakChanges: true);

            var FridgeProductsDTO = _mapper.Map<IEnumerable<FridgeProductDTO>>(fridgeProducts);

            return Ok(FridgeProductsDTO);
        }

        [HttpGet("{productId}", Name = "GetProductForFridge")]
        [ServiceFilter(typeof(ValidateFridgeProductExistsAttribute))]
        public IActionResult GetProductForFridge(Guid fridgeModelId, Guid fridgeId, Guid productId)
        {
            var fridgeProduct = HttpContext.Items["fridgeProduct"] as FridgeProduct;

            var fridgeProductDTO = _mapper.Map<FridgeProductDTO>(fridgeProduct);
            return Ok(fridgeProductDTO);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateFridgeExistsAttribute))]
        public async Task<IActionResult> AddProductInFridge(Guid fridgeModelId, Guid fridgeId,
                                                           [FromBody] FridgeProductForCreationDTO fridgeProduct)
        {
            var product = await _repository.Product.GetProductAsync(fridgeProduct.ProductId, trackChanges: false);
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
                     productId = fridgeProductToReturn.ProductId
                 },
                 fridgeProductToReturn);
        }

        [HttpDelete("{productId}")]
        [ServiceFilter(typeof(ValidateFridgeProductExistsAttribute))]
        public async Task<IActionResult> DeleteProductFromFridge(Guid fridgeModelId, Guid fridgeId,
                                                                 Guid productId)
        {
            var fridgeProduct = HttpContext.Items["fridgeProduct"] as FridgeProduct;

            _repository.FridgeProduct.DeleteProductFromFridge(fridgeProduct);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{productId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateFridgeProductExistsAttribute))]
        public async Task<IActionResult> UpdateProductForFridge(Guid fridgeModelId, Guid fridgeId, Guid productId,
                                                               [FromBody] FridgeProductForUpdateDTO fridgeProduct)
        {
            var fridgeProductEntity = HttpContext.Items["fridgeProduct"] as FridgeProduct;

            _mapper.Map(fridgeProduct, fridgeProductEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}