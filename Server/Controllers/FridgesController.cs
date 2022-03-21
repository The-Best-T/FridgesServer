using AutoMapper;
using Contracts;
using Entities.DTO.Fridge;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Server.ActionFilters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Server.Controllers
{
    [Route("api/models/{fridgeModelId}/fridges")]
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

        [HttpGet("{fridgeId}", Name = "GetFridgeForModel")]
        [ServiceFilter((typeof(ValidateFridgeExistsAttribute)))]
        public IActionResult GetFridgeForModel(Guid fridgeModelId, Guid fridgeId)
        {
            var fridge = HttpContext.Items["fridge"] as FridgeModel;

            var fridgeDTO = _mapper.Map<FridgeDTO>(fridge);
            return Ok(fridgeDTO);
        }

        [HttpGet]
        [ServiceFilter((typeof(ValidateFridgeModelExistsAttribute)))]
        public async Task<IActionResult> GetFridgesForModel(Guid fridgeModelId)
        {
            var fridges = await _repository.Fridge
                                           .GetFridgesForModelAsync(fridgeModelId, trackChanges: true);

            var fridgeDTO = _mapper.Map<IEnumerable<FridgeDTO>>(fridges);

            return Ok(fridgeDTO);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter((typeof(ValidateFridgeModelExistsAttribute)))]
        public async Task<IActionResult> CreateFridgeForModel(Guid fridgeModelId,
                                                  [FromBody] FridgeForCreationDTO fridge)
        {
            var fridgeModel = HttpContext.Items["fridgeModel"] as FridgeModel;

            var fridgeEntity = _mapper.Map<Fridge>(fridge);

            _repository.Fridge.CreateFridgeForModel(fridgeModelId, fridgeEntity);
            await _repository.SaveAsync();

            var fridgeToReturn = _mapper.Map<FridgeDTO>(fridgeEntity);
            fridgeToReturn.ModelName = fridgeModel.Name;

            return CreatedAtRoute("GetFridgeForModel",
                new
                {
                    fridgeModelId,
                    id = fridgeToReturn.Id
                },
                fridgeToReturn);
        }

        [HttpDelete("{fridgeId}")]
        [ServiceFilter(typeof(ValidateFridgeExistsAttribute))]
        public async Task<IActionResult> DeleteFridgeForModel(Guid fridgeModelId, Guid fridgeId)
        {
            var fridge = HttpContext.Items["fridge"] as Fridge;

            _repository.Fridge.DeleteFridge(fridge);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{fridgeId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateFridgeExistsAttribute))]
        public async Task<IActionResult> UpdateFridgeForModel(Guid fridgeModelId, Guid fridgeId,
                                                              [FromBody] FridgeForUpdateDTO fridge)
        {
            var fridgeEntity = HttpContext.Items["fridge"] as Fridge;

            _mapper.Map(fridge, fridgeEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
