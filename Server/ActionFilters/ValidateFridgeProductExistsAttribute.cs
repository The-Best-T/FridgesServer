using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using System;
using System.Threading.Tasks;
namespace Server.ActionFilters
{
    public class ValidateFridgeProductExistsAttribute : IAsyncActionFilter
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        public ValidateFridgeProductExistsAttribute(ILoggerManager logger, IRepositoryManager repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");

            var fridgeId = (Guid)context.ActionArguments["fridgeId"];
            var fridge = await _repository.Fridge.GetFridgeAsync(fridgeId, trackChanges: false);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {fridgeId} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }

            var productId = (Guid)context.ActionArguments["productId"];
            var fridgeProduct = await _repository.FridgeProduct.GetProductForFridgeAsync(fridgeId, productId, trackChanges);
            if (fridgeProduct == null)
            {
                _logger.LogInfo($"Product with id: {productId} doesn't exist in fridge {fridgeId}.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("fridgeProduct", fridgeProduct);
                await next();
            }
        }
    }
}
