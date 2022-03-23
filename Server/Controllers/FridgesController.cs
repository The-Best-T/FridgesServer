using AutoMapper;
using Contracts;
using Entities.DTO.Fridge;
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
    [Route("api/fridges")]
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

        [HttpGet("{fridgeId}", Name = "GetFridgeById")]
        [ServiceFilter((typeof(ValidateFridgeExistsAttribute)))]
        public IActionResult GetFridgeForModel(Guid fridgeId)
        {
            var fridge = HttpContext.Items["fridge"] as FridgeModel;

            var fridgeDTO = _mapper.Map<FridgeDTO>(fridge);
            return Ok(fridgeDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetFridges([FromQuery] FridgeParameters parameters)
        {
            var fridges = await _repository.Fridge.GetFridgesAsync(parameters, trackChanges: true);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(fridges.MetaData));

            var fridgeDTO = _mapper.Map<IEnumerable<FridgeDTO>>(fridges);

            return Ok(fridgeDTO);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateFridge([FromBody] FridgeForCreationDTO fridge)
        {
            var fridgeModel = await _repository.FridgeModel
                .GetFridgeModelAsync(fridge.ModelId, trackChanges: false);
            if (fridgeModel==null)
            {
                _logger.LogInfo($"FridgeModel with id {fridge.ModelId} doesn't exist in database.");
                return NotFound();
            }
            var fridgeEntity = _mapper.Map<Fridge>(fridge);
            fridgeEntity.Model = fridgeModel;

            _repository.Fridge.CreateFridge(fridgeEntity);
            await _repository.SaveAsync();

            var fridgeToReturn = _mapper.Map<FridgeDTO>(fridgeEntity);

            return CreatedAtRoute("GetFridgeById",
                new
                {
                    fridgeId = fridgeToReturn.Id
                },
                fridgeToReturn);
        }

        [HttpDelete("{fridgeId}")]
        [ServiceFilter(typeof(ValidateFridgeExistsAttribute))]
        public async Task<IActionResult> DeleteFridge(Guid fridgeId)
        {
            var fridge = HttpContext.Items["fridge"] as Fridge;

            _repository.Fridge.DeleteFridge(fridge);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{fridgeId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateFridgeExistsAttribute))]
        public async Task<IActionResult> UpdateFridge(Guid fridgeId,
            [FromBody] FridgeForUpdateDTO fridge)
        {
            var fridgeEntity = HttpContext.Items["fridge"] as Fridge;

            _mapper.Map(fridge, fridgeEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
