using AutoMapper;
using Contracts;
using Entities.DTO.Fridge;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
namespace Server.Controllers
{
    [Route("api/models/{modelId}/fridges")]
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
        [HttpGet("{id}")]
        public IActionResult GetFridgeForModel(Guid modelId,Guid id)
        {
            var model = _repository.FridgeModel.GetFridgeModel(modelId, trackChanges: false);
            if (model == null)
            {
                _logger.LogInfo($"Model with id: {modelId} doesn't exist in the database.");
                return NotFound();
            }
            var fridge = _repository.Fridge.GetFridgeForModel(modelId, id, trackChanges: true);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var fridgeDTO = _mapper.Map<FridgeDTO>(fridge);
                return Ok(fridgeDTO);
            }
        }
        [HttpGet]
        public IActionResult GetFridgesForModel(Guid modelId)
        {
            var model = _repository.FridgeModel.GetFridgeModel(modelId, trackChanges: false);
            if (model==null)
            {
                _logger.LogInfo($"Model with id: {modelId} doesn't exist in the database.");
                return NotFound();
            }
            var fridges=_repository.Fridge.GetFridgesForModel(modelId, trackChanges: true);
            var fridgeDTO= _mapper.Map<IEnumerable<FridgeDTO>>(fridges);
            return Ok(fridgeDTO);
        }

    }
}
