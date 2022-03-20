using AutoMapper;
using Contracts;
using Entities.DTO.FridgeModel;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Server.Controllers
{
    [Route("api/models")]
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

        [HttpGet]
        public async Task<IActionResult> GetFridgeModels()
        {
            var fridgeModels = await _repository.FridgeModel
                                                .GetFridgeModelsAsync(trackChanges: false);
            var fridgeModelsDTO = _mapper.Map<IEnumerable<FridgeModelDTO>>(fridgeModels);
            return Ok(fridgeModelsDTO);
        }

        [HttpGet("{id}", Name = "GetFridgeModelById")]
        public async Task<IActionResult> GetFridgeModel(Guid id)
        {
            var fridgeModel = await _repository.FridgeModel
                                               .GetFridgeModelAsync(id, trackChanges: false);
            if (fridgeModel == null)
            {
                _logger.LogInfo($"FridgeModel with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var fridgeModelDTO = _mapper.Map<FridgeModelDTO>(fridgeModel);
                return Ok(fridgeModelDTO);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateFridgeModel([FromBody] FridgeModelForCreationDTO fridgeModel)
        {
            if (fridgeModel == null)
            {
                _logger.LogError("FridgeModelForCreationDto object sent from client is null.");
                return BadRequest("FridgeModelForCreationDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the FridgeModelForCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var fridgeModelEntity = _mapper.Map<FridgeModel>(fridgeModel);

            _repository.FridgeModel.CreateFridgeModel(fridgeModelEntity);
            await _repository.SaveAsync();

            var fridgeModelToReturn = _mapper.Map<FridgeModelDTO>(fridgeModelEntity);

            return CreatedAtRoute("GetFridgeModelById", new { id = fridgeModelToReturn.Id },
                                  fridgeModelToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFridgeModel(Guid id)
        {
            var fridgeModel = await _repository.FridgeModel
                                               .GetFridgeModelAsync(id, trackChanges: false);
            if (fridgeModel == null)
            {
                _logger.LogInfo($"FridgeModel with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.FridgeModel.DeleteFridgeModel(fridgeModel);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFridgeModel(Guid id,
                                                           [FromBody] FridgeModelForUpdateDTO fridgeModel)
        {
            if (fridgeModel == null)
            {
                _logger.LogError("FridgeModelForUpdateDTO object sent from client is null.");
                return BadRequest("FridgeModelForUpdateDTO object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the FridgeModelForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }

            var fridgeModelEntity = await _repository.FridgeModel
                                                     .GetFridgeModelAsync(id, trackChanges: true);
            if (fridgeModelEntity == null)
            {
                _logger.LogInfo($"FridgeModel with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(fridgeModel, fridgeModelEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

    }
}
