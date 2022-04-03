using AutoMapper;
using Contracts;
using Entities.Dto.FridgeModel;
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
    [Route("api/models"),Authorize]
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

            var fridgeModelDto = _mapper.Map<FridgeModelDto>(fridgeModel);
            return Ok(fridgeModelDto);

        }

        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> GetFridgeModels([FromQuery] FridgeModelParameters parameters)
        {
            var fridgeModels = await _repository.FridgeModel
                .GetFridgeModelsAsync(parameters, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(fridgeModels.MetaData));

            var fridgeModelsDto = _mapper.Map<IEnumerable<FridgeModelDto>>(fridgeModels);
            return Ok(fridgeModelsDto);
        }

        [HttpPost]
        [Authorize(Roles ="Administrator")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateFridgeModel([FromBody] FridgeModelForCreationDto fridgeModel)
        {
            var fridgeModelEntity = _mapper.Map<FridgeModel>(fridgeModel);

            _repository.FridgeModel.CreateFridgeModel(fridgeModelEntity);
            await _repository.SaveAsync();

            var fridgeModelToReturn = _mapper.Map<FridgeModelDto>(fridgeModelEntity);

            return CreatedAtRoute(
                "GetFridgeModelById", 
                new { fridgeModelId = fridgeModelToReturn.Id },
                fridgeModelToReturn);
        }

        [HttpDelete("{fridgeModelId}")]
        [Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(ValidateFridgeModelExistsAttribute))]
        public async Task<IActionResult> DeleteFridgeModel(Guid fridgeModelId)
        {
            var fridgeModel = HttpContext.Items["fridgeModel"] as FridgeModel;

            _repository.FridgeModel.DeleteFridgeModel(fridgeModel);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{fridgeModelId}")]
        [Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateFridgeModelExistsAttribute))]
        public async Task<IActionResult> UpdateFridgeModel(Guid fridgeModelId,
            [FromBody] FridgeModelForUpdateDto fridgeModel)
        {
            var fridgeModelEntity = HttpContext.Items["fridgeModel"] as FridgeModel;

            _mapper.Map(fridgeModel, fridgeModelEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

    }
}