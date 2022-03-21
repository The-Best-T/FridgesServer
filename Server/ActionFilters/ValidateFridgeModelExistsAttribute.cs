using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using System;
using System.Threading.Tasks;
namespace Server.ActionFilters
{
    public class ValidateFridgeModelExistsAttribute : IAsyncActionFilter
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        public ValidateFridgeModelExistsAttribute(ILoggerManager logger, IRepositoryManager repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Contains("PUT");
            var id = (Guid)context.ActionArguments["id"];
            var fridgeModel = await _repository.FridgeModel.GetFridgeModelAsync(id, trackChanges);

            if (fridgeModel == null)
            {
                _logger.LogInfo($"FridgeModel with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("fridgeModel", fridgeModel);
                await next();
            }
        }
    }
}
