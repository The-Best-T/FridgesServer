using AutoMapper;
using Contracts;
using Entities.Dto.FridgeProduct;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api/fridges/{fridgeId}/products"),Authorize]
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

        /// <summary>
        /// Get available queries 
        /// </summary>
        /// <param></param>
        /// <returns>All avaiable queries in header</returns>
        /// <response code="200">Returns all avaiable queries in header</response>
        [HttpOptions]
        [ProducesResponseType(200)]
        public IActionResult GetFridgeProductsOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }

        /// <summary>
        /// Get one product from fridge by fridge id and product id
        /// </summary>
        /// <param name="fridgeId"></param>
        /// <param name="productId"></param>
        /// <returns>One product from the fridge</returns>
        /// <response code="200">Returns one product from the fridge</response>
        /// <response code="404">Fridge or product in this fridge with this id not found</response>
        /// <response code="500">Server error</response>
        [HttpGet("{productId}", Name = "GetProductForFridge")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ServiceFilter(typeof(ValidateFridgeProductExistsAttribute))]
        public IActionResult GetProductForFridge(Guid fridgeId, Guid productId)
        {
            var fridgeProduct = HttpContext.Items["fridgeProduct"] as FridgeProduct;

            var fridgeProductDto = _mapper.Map<FridgeProductDto>(fridgeProduct);
            return Ok(fridgeProductDto);
        }

        /// <summary>
        /// Get all product from fridge 
        /// </summary>
        /// <param name="fridgeId"></param>
        /// <param name="parameters"></param>
        /// <returns>List of products from the fridge </returns>
        /// <response code="200">Returns list of products from the fridge</response>
        /// <response code="404">Fridge with this id not found</response>
        /// <response code="500">Server error</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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

        /// <summary>
        /// Add new product in fridge
        /// </summary>
        /// <param name="fridgeId"></param>
        /// <param name="fridgeProduct"></param>
        /// <returns>The newly product from the fridge</returns>
        /// <response code="201">Returns the newly product from the fridge</response>
        /// <response code="400">FridgeProductForCreationDto is null</response>
        /// <response code="404">Fridge or product with this id not found</response>
        /// <response code="422">FridgeProductForCreationDto is invalid</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator")]
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

        /// <summary>
        /// Delete product from fridge
        /// </summary>
        /// <param name="fridgeId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        /// <response code="204">Product deleted from the fridge</response>
        /// <response code="404">Fridge or product in this fridge with this id not found</response>
        /// <response code="500">Server error</response>
        [HttpDelete("{productId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(ValidateFridgeProductExistsAttribute))]
        public async Task<IActionResult> DeleteProductFromFridge(Guid fridgeId, Guid productId)
        {
            var fridgeProduct = HttpContext.Items["fridgeProduct"] as FridgeProduct;

            _repository.FridgeProduct.DeleteProductFromFridge(fridgeProduct);
            await _repository.SaveAsync();

            return NoContent();
        }


        /// <summary>
        /// Update product in fridge
        /// </summary>
        /// <param name="fridgeId"></param>
        /// <param name="productId"></param>
        /// <param name="fridgeProduct"></param>
        /// <returns></returns>
        /// <response code="204">Product deleted from the fridge</response>
        /// <response code="400">FridgeProductForCreationDto is null</response>
        /// <response code="404">Fridge or product with this id not found</response>
        /// <response code="422">FridgeProductForCreationDto is invalid</response>
        /// <response code="500">Server error</response>
        [HttpPut("{productId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator")]
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