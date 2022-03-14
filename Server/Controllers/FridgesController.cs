using AutoMapper;
using Contracts;
using Entities.DTO.Fridge;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
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
        [HttpGet]
        public IActionResult GetFridges()
        {

            var fridges = _repository.Fridge.GetAllFridges(trackChanges: true);
            var fridgesDTO = _mapper.Map<IEnumerable<FridgeDTO>>(fridges);
            return Ok(fridgesDTO);

        }
        [HttpGet("{id}")]
        public IActionResult GetFridge(Guid id)
        {
            var fridge = _repository.Fridge.GetFridge(id, trackChanges: false);
            if (fridge == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var fridgeDTO = _mapper.Map<FridgeDTO>(fridge);
                return Ok(fridgeDTO);
            }
        }
    }
}
