using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using System;
using System.Threading.Tasks;
namespace Server.ActionFilters
{
    public class ValidateFridgeExistsAttribute : IAsyncActionFilter
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        public ValidateFridgeExistsAttribute(ILoggerManager logger, IRepositoryManager repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");

            var fridgeModelId = (Guid)context.ActionArguments["fridgeModelId"];
            var fridgeModel = await _repository.FridgeModel.GetFridgeModelAsync(fridgeModelId, trackChanges: false);
            if (fridgeModel == null)
            {
                _logger.LogInfo($"FridgeModel with id: {fridgeModelId} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }

            var fridgeId = (Guid)context.ActionArguments["fridgeId"];
            var fridge = await _repository.Fridge.GetFridgeForModelAsync(fridgeModelId, fridgeId, trackChanges);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {fridgeId} and modelId {fridgeModelId} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("fridge", fridge);
                await next();
            }
        }
    }
}
