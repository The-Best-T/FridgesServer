using AutoMapper;
using Contracts;
using Entities.DTO.FridgeModel;
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

        [HttpOptions]
        public IActionResult GetFridgeModelsOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }

        [HttpGet("{fridgeModelId}", Name = "GetFridgeModelById")]
        [ServiceFilter(typeof(ValidateFridgeModelExistsAttribute))]
        public IActionResult GetFridgeModel(Guid fridgeModelId)
        {
            var fridgeModel = HttpContext.Items["fridgeModel"] as FridgeModel;

            var fridgeModelDTO = _mapper.Map<FridgeModelDTO>(fridgeModel);
            return Ok(fridgeModelDTO);

        }

        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> GetFridgeModels([FromQuery] FridgeModelParameters parameters)
        {
            var fridgeModels = await _repository.FridgeModel
                .GetFridgeModelsAsync(parameters, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(fridgeModels.MetaData));

            var fridgeModelsDTO = _mapper.Map<IEnumerable<FridgeModelDTO>>(fridgeModels);
            return Ok(fridgeModelsDTO);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateFridgeModel([FromBody] FridgeModelForCreationDTO fridgeModel)
        {
            var fridgeModelEntity = _mapper.Map<FridgeModel>(fridgeModel);

            _repository.FridgeModel.CreateFridgeModel(fridgeModelEntity);
            await _repository.SaveAsync();

            var fridgeModelToReturn = _mapper.Map<FridgeModelDTO>(fridgeModelEntity);

            return CreatedAtRoute("GetFridgeModelById", new { fridgeModelId = fridgeModelToReturn.Id },
                fridgeModelToReturn);
        }

        [HttpDelete("{fridgeModelId}")]
        [ServiceFilter(typeof(ValidateFridgeModelExistsAttribute))]
        public async Task<IActionResult> DeleteFridgeModel(Guid fridgeModelId)
        {
            var fridgeModel = HttpContext.Items["fridgeModel"] as FridgeModel;

            _repository.FridgeModel.DeleteFridgeModel(fridgeModel);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{fridgeModelId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateFridgeModelExistsAttribute))]
        public async Task<IActionResult> UpdateFridgeModel(Guid fridgeModelId,
            [FromBody] FridgeModelForUpdateDTO fridgeModel)
        {
            var fridgeModelEntity = HttpContext.Items["fridgeModel"] as FridgeModel;

            _mapper.Map(fridgeModel, fridgeModelEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

    }
}
