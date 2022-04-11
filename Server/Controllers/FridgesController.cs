using AutoMapper;
using Contracts;
using Entities.Dto.Fridge;
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
    [Route("api/fridges"), Authorize]
    [ApiController]
    public class FridgesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public FridgesController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
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
        public IActionResult GetFridgesOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }

        /// <summary>
        /// Get fridge by id
        /// </summary>
        /// <param name="fridgeId"></param>
        /// <returns>One fridge</returns>
        /// <response code="200">Returns one fridge</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Fridge with this id not found</response>
        /// <response code="500">Server error</response>
        [HttpGet("{fridgeId}", Name = "GetFridgeById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ServiceFilter((typeof(ValidateFridgeExistsAttribute)))]
        public IActionResult GetFridge(Guid fridgeId)
        {
            var fridge = HttpContext.Items["fridge"] as Fridge;

            var FridgeDto = _mapper.Map<FridgeDto>(fridge);
            return Ok(FridgeDto);
        }

        /// <summary>
        /// Gets the list of all fridges
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns> list of fridgs</returns>
        /// <response code="200">Returns all fridgs</response>
        /// <response code="401">Not authorized</response>
        /// <response code="500">Server error</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetFridges([FromQuery] FridgeParameters parameters)
        {
            var fridges = await _repository.Fridge.GetFridgesAsync(parameters, trackChanges: true);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(fridges.MetaData));

            var FridgeDto = _mapper.Map<IEnumerable<FridgeDto>>(fridges);

            return Ok(FridgeDto);
        }

        /// <summary>
        /// Create new fridge
        /// </summary>
        /// <param name="fridge"></param>
        /// <returns>A newly created fridge</returns>
        /// <response code="201">Returns the newly fridge</response>
        /// <response code="400">FridgeForCreationDto is null</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">This fridge model not found</response>
        /// <response code="422">FridgeForCreationDto is invalid</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateFridge([FromBody] FridgeForCreationDto fridge)
        {
            var fridgeModelId = fridge.ModelId;

            var fridgeModel = await _repository.FridgeModel
                .GetFridgeModelAsync(fridgeModelId, trackChanges: false);
            if (fridgeModel == null)
            {
                _logger.LogInfo($"FridgeModel with id {fridgeModelId} doesn't exist in database.");
                return NotFound();
            }
            var fridgeEntity = _mapper.Map<Fridge>(fridge);

            _repository.Fridge.CreateFridge(fridgeEntity);
            await _repository.SaveAsync();

            fridgeEntity.Model = fridgeModel;
            var fridgeToReturn = _mapper.Map<FridgeDto>(fridgeEntity);
            return CreatedAtRoute(
                "GetFridgeById",
                new { fridgeId = fridgeToReturn.Id },
                fridgeToReturn);
        }

        /// <summary>
        /// Delete frige by id
        /// </summary>
        /// <param name="fridgeId"></param>
        /// <returns></returns>
        /// <response code="204">Fridge deleted</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Fridge with this id not found</response>
        /// <response code="500">Server error</response>
        [HttpDelete("{fridgeId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(ValidateFridgeExistsAttribute))]
        public async Task<IActionResult> DeleteFridge(Guid fridgeId)
        {
            var fridge = HttpContext.Items["fridge"] as Fridge;

            _repository.Fridge.DeleteFridge(fridge);
            await _repository.SaveAsync();

            return NoContent();
        }

        /// <summary>
        /// Update fridge by id
        /// </summary>
        /// <param name="fridgeId"></param>
        /// <param name="fridge"></param>
        /// <returns></returns>
        /// <response code="204">Fridge updated</response>
        /// <response code="400">FridgeForUpdateDto is null</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Fridge with this id not found</response>
        /// <response code="422">FridgeForUpdateDto is invalid</response>
        /// <response code="500">Server error</response>
        [HttpPut("{fridgeId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateFridgeExistsAttribute))]
        public async Task<IActionResult> UpdateFridge(Guid fridgeId,
            [FromBody] FridgeForUpdateDto fridge)
        {
            var fridgeEntity = HttpContext.Items["fridge"] as Fridge;

            _mapper.Map(fridge, fridgeEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        /// <summary>
        /// In all fridges set quantity of products to standard quantity if quantity is 0 
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Fridges are filled</response>
        /// <response code="401">Not authorized</response>
        [HttpPost("fill")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> FillFridges()
        {
            await _repository.StoredProcedureWithoutParamasAsync("[dbo].[FillFridges]");
            return Ok();
        }
    }
}