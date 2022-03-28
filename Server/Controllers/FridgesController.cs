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
    [Route("api/fridges"),Authorize]
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

        [HttpOptions]
        public IActionResult GetFridgesOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }

        [HttpGet("{fridgeId}", Name = "GetFridgeById")]
        [ServiceFilter((typeof(ValidateFridgeExistsAttribute)))]
        public IActionResult GetFridge(Guid fridgeId)
        {
            var fridge = HttpContext.Items["fridge"] as Fridge;

            var FridgeDto = _mapper.Map<FridgeDto>(fridge);
            return Ok(FridgeDto);
        }

        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> GetFridges([FromQuery] FridgeParameters parameters)
        {
            var fridges = await _repository.Fridge.GetFridgesAsync(parameters, trackChanges: true);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(fridges.MetaData));

            var FridgeDto = _mapper.Map<IEnumerable<FridgeDto>>(fridges);

            return Ok(FridgeDto);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateFridge([FromBody] FridgeForCreationDto fridge)
        {
            var fridgeModel = await _repository.FridgeModel
                .GetFridgeModelAsync(fridge.ModelId, trackChanges: false);
            if (fridgeModel == null)
            {
                _logger.LogInfo($"FridgeModel with id {fridge.ModelId} doesn't exist in database.");
                return NotFound();
            }
            var fridgeEntity = _mapper.Map<Fridge>(fridge);

            _repository.Fridge.CreateFridge(fridgeEntity);
            await _repository.SaveAsync();

            fridgeEntity.Model = fridgeModel;
            var fridgeToReturn = _mapper.Map<FridgeDto>(fridgeEntity);
            return CreatedAtRoute("GetFridgeById",
                new
                {
                    fridgeId = fridgeToReturn.Id
                },
                fridgeToReturn);
        }

        [HttpDelete("{fridgeId}")]
        [Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(ValidateFridgeExistsAttribute))]
        public async Task<IActionResult> DeleteFridge(Guid fridgeId)
        {
            var fridge = HttpContext.Items["fridge"] as Fridge;

            _repository.Fridge.DeleteFridge(fridge);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{fridgeId}")]
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

        [HttpPost("fill")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> FillFridges()
        {
            await _repository.StoredProcedureWithoutParamasAsync("[dbo].[FillFridges]");
            return Ok();
        }
    }
}
