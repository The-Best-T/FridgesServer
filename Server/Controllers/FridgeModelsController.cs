using AutoMapper;
using Contracts;
using Entities.Dto.FridgeModel;
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
    [Route("api/models"),Authorize]
    [ApiController]
    public class FridgeModelsController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public FridgeModelsController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
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
        /// <response code="401">Not authorized</response>
        [HttpOptions]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public IActionResult GetFridgeModelsOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }

        /// <summary>
        /// Get fridge model by id
        /// </summary>
        /// <param name="fridgeModelId"></param>
        /// <returns>One fridge model</returns>
        /// <response code="200">Returns one fridge model</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Fridge model with this id not found</response>
        /// <response code="500">Server error</response>
        [HttpGet("{fridgeModelId}", Name = "GetFridgeModelById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ServiceFilter(typeof(ValidateFridgeModelExistsAttribute))]
        public IActionResult GetFridgeModel(Guid fridgeModelId)
        {
            var fridgeModel = HttpContext.Items["fridgeModel"] as FridgeModel;

            var fridgeModelDto = _mapper.Map<FridgeModelDto>(fridgeModel);
            return Ok(fridgeModelDto);

        }

        /// <summary>
        /// Gets the list of all fridge models
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns> list of fridge models</returns>
        /// <response code="200">Returns all fridge models</response>
        /// <response code="401">Not authorized</response>
        /// <response code="500">Server error</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetFridgeModels([FromQuery] FridgeModelParameters parameters)
        {
            var fridgeModels = await _repository.FridgeModel
                .GetFridgeModelsAsync(parameters, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(fridgeModels.MetaData));

            var fridgeModelsDto = _mapper.Map<IEnumerable<FridgeModelDto>>(fridgeModels);
            return Ok(fridgeModelsDto);
        }

        /// <summary>
        /// Create new fridge model
        /// </summary>
        /// <param name="fridgeModel"></param>
        /// <returns>A newly created fridge model</returns>
        /// <response code="201">Returns the newly fridge model</response>
        /// <response code="400">FridgeModelForCreationDto is null</response>
        /// <response code="401">Not authorized</response>
        /// <response code="422">FridgeModelForCreationDto is invalid</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        [Authorize(Roles ="Administrator")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateFridgeModel([FromBody] FridgeModelForCreationDto fridgeModel)
        {
            var fridgeModelEntity = _mapper.Map<FridgeModel>(fridgeModel);

            _repository.FridgeModel.CreateFridgeModel(fridgeModelEntity);
            await _repository.SaveAsync();

            var fridgeModelToReturn = _mapper.Map<FridgeModelDto>(fridgeModelEntity);

            return CreatedAtRoute(
                "GetFridgeModelById", 
                new { fridgeModelId = fridgeModelToReturn.Id },
                fridgeModelToReturn);
        }

        /// <summary>
        /// Delete frige model by id
        /// </summary>
        /// <param name="fridgeModelId"></param>
        /// <returns></returns>
        /// <response code="204">Fridge model deleted</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Fridge model with this id not found</response>
        /// <response code="500">Server error</response>
        [HttpDelete("{fridgeModelId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(ValidateFridgeModelExistsAttribute))]
        public async Task<IActionResult> DeleteFridgeModel(Guid fridgeModelId)
        {
            var fridgeModel = HttpContext.Items["fridgeModel"] as FridgeModel;

            _repository.FridgeModel.DeleteFridgeModel(fridgeModel);
            await _repository.SaveAsync();

            return NoContent();
        }

        /// <summary>
        /// Update fridge model by id
        /// </summary>
        /// <param name="fridgeModelId"></param>
        /// <param name="fridgeModel"></param>
        /// <returns></returns>
        /// <response code="204">Fridge model updated</response>
        /// <response code="401">Not authorized</response>
        /// <response code="400">FridgeModelForUpdateDto is null</response>
        /// <response code="404">Fridge model with this id not found</response>
        /// <response code="422">FridgeModelForUpdateDto is invalid</response>
        /// <response code="500">Server error</response>
        [HttpPut("{fridgeModelId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateFridgeModelExistsAttribute))]
        public async Task<IActionResult> UpdateFridgeModel(Guid fridgeModelId,
            [FromBody] FridgeModelForUpdateDto fridgeModel)
        {
            var fridgeModelEntity = HttpContext.Items["fridgeModel"] as FridgeModel;

            _mapper.Map(fridgeModel, fridgeModelEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

    }
}