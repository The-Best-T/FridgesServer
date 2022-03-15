using AutoMapper;
using Contracts;
using Entities.DTO.FridgeModel;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
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
        public IActionResult GetModels()
        {
            var fridgemodels = _repository.FridgeModel.GetAllFridgeModels(trackChanges: false);
            var fridgeModelsDTO = _mapper.Map<IEnumerable<FridgeModelDTO>>(fridgemodels);
            return Ok(fridgeModelsDTO);
        }
        [HttpGet("{id}")]
        public IActionResult GetModel(Guid id)
        {
            var fridgeModel = _repository.FridgeModel.GetFridgeModel(id, trackChanges: false);
            if (fridgeModel == null)
            {
                _logger.LogInfo($"Model with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var fridgeModelDTO = _mapper.Map<FridgeModelDTO>(fridgeModel);
                return Ok(fridgeModelDTO);
            }
        }
    }
}
