using AutoMapper;
using Contracts;
using Entities.Dto.FridgeProduct;
using Entities.Models;
using Entities.RequestFeatures;
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
    [Route("api/fridges/{fridgeId}/products")]
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

        [HttpOptions]
        public IActionResult GetFridgeProductsOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }

        [HttpGet("{productId}", Name = "GetProductForFridge")]
        [ServiceFilter(typeof(ValidateFridgeProductExistsAttribute))]
        public IActionResult GetProductForFridge(Guid fridgeId, Guid productId)
        {
            var fridgeProduct = HttpContext.Items["fridgeProduct"] as FridgeProduct;

            var fridgeProductDto = _mapper.Map<FridgeProductDto>(fridgeProduct);
            return Ok(fridgeProductDto);
        }

        [HttpGet]
        [HttpHead]
        [ServiceFilter(typeof(ValidateFridgeExistsAttribute))]
        public async Task<IActionResult> GetProductsForFridge(Guid fridgeId,
            [FromQuery] FridgeProductParameters parameters)
        {
            var fridgeProducts = await _repository.FridgeProduct
                .GetProductsForFridgeAsync(fridgeId, parameters, trakChanges: true);

            HttpContext.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(fridgeProducts.MetaData));

            var FridgeProductsDto = _mapper.Map<IEnumerable<FridgeProductDto>>(fridgeProducts);
            return Ok(FridgeProductsDto);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateFridgeExistsAttribute))]
        public async Task<IActionResult> AddProductInFridge(Guid fridgeId,
            [FromBody] FridgeProductForCreationDto fridgeProduct)
        {
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

            var fridgeProductToReturn = _mapper.Map<FridgeProductDto>(fridgeProductEntity);
            fridgeProductToReturn.ProductName = product.Name;

            return CreatedAtRoute(
                "GetProductForFridge",
                 new
                 {
                     fridgeId,
                     productId = fridgeProductToReturn.ProductId
                 },
                 fridgeProductToReturn);
        }

        [HttpDelete("{productId}")]
        [ServiceFilter(typeof(ValidateFridgeProductExistsAttribute))]
        public async Task<IActionResult> DeleteProductFromFridge(Guid fridgeId, Guid productId)
        {
            var fridgeProduct = HttpContext.Items["fridgeProduct"] as FridgeProduct;

            _repository.FridgeProduct.DeleteProductFromFridge(fridgeProduct);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{productId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateFridgeProductExistsAttribute))]
        public async Task<IActionResult> UpdateProductForFridge(Guid fridgeId, Guid productId,
            [FromBody] FridgeProductForUpdateDto fridgeProduct)
        {
            var fridgeProductEntity = HttpContext.Items["fridgeProduct"] as FridgeProduct;

            _mapper.Map(fridgeProduct, fridgeProductEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}