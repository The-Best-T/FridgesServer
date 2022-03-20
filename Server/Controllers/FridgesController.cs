using AutoMapper;
using Contracts;
using Entities.DTO.Fridge;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using NLog;
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
        [HttpGet("{id}", Name = "GetFridgeForModel")]
        public async Task<IActionResult> GetFridgeForModel(Guid fridgeModelId, Guid id)
        {
            var fridgeModel = await _repository.FridgeModel
                                               .GetFridgeModelAsync(fridgeModelId, trackChanges: false);
            if (fridgeModel == null)
            {
                _logger.LogInfo($"FridgeModel with id: {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

            var fridge = await _repository.Fridge
                                          .GetFridgeForModelAsync(fridgeModelId, id, trackChanges: true);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {id} and modelId {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var fridgeDTO = _mapper.Map<FridgeDTO>(fridge);
                return Ok(fridgeDTO);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetFridgesForModel(Guid fridgeModelId)
        {
            var fridgeModel = await _repository.FridgeModel
                                               .GetFridgeModelAsync(fridgeModelId, trackChanges: false);
            if (fridgeModel == null)
            {
                _logger.LogInfo($"FridgeModel with id: {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

            var fridges = await _repository.Fridge
                                           .GetFridgesForModelAsync(fridgeModelId, trackChanges: true);

            var fridgeDTO = _mapper.Map<IEnumerable<FridgeDTO>>(fridges);

            return Ok(fridgeDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFridgeForModel(Guid fridgeModelId,
                                                  [FromBody] FridgeForCreationDTO fridge)
        {
            if (fridge == null)
            {
                _logger.LogError("FridgeForCreationDto object sent from client is null.");
                return BadRequest("FridgeForCreationDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the FridgeForCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var fridgeModel = await _repository.FridgeModel
                                               .GetFridgeModelAsync(fridgeModelId, trackChanges: false);
            if (fridgeModel == null)
            {
                _logger.LogInfo($"FridgeModel with id: {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFridgeForModel(Guid fridgeModelId, Guid id)
        {
            var fridgeModel = await _repository.FridgeModel
                                               .GetFridgeModelAsync(fridgeModelId, trackChanges: false);
            if (fridgeModel == null)
            {
                _logger.LogInfo($"FridgeModel with id: {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

            var fridge = await _repository.Fridge
                                          .GetFridgeForModelAsync(fridgeModelId, id, trackChanges: false);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {id} and modelId {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

            _repository.Fridge.DeleteFridge(fridge);
            await _repository.SaveAsync();

            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFridgeForModel(Guid fridgeModelId, Guid id,
                                                              [FromBody] FridgeForUpdateDTO fridge)
        {
            if (fridge == null)
            {
                _logger.LogError("FridgeForUpdateDTO object sent from client is null.");
                return BadRequest("FridgeForUpdateDTO object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the FridgeForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }

            var fridgeModel = await _repository.FridgeModel
                                               .GetFridgeModelAsync(fridgeModelId, trackChanges: false);
            if (fridgeModel == null)
            {
                _logger.LogInfo($"FridgeModel with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var fridgeEntity = await _repository.Fridge
                                                .GetFridgeForModelAsync(fridgeModelId, id, trackChanges: true);
            if (fridgeEntity == null)
            {
                _logger.LogInfo($"Fridge with id: {id} and modelId {fridgeModelId} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(fridge, fridgeEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
