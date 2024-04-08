using FitByBitService.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FitByBitService.filters
{
    public class ValidationActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(new GenericResponse<object>
                {
                    Data = null,
                    Message = string.Join(Environment.NewLine, GetErrorListFromModelState(context.ModelState))
                });
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //Do nothing
        }

        private static IEnumerable<string> GetErrorListFromModelState
            (ModelStateDictionary modelState)
        {
            var query = from state in modelState.Values.ToList()
                        from error in state.Errors
                        select error.ErrorMessage;

            var errorList = query.ToList();
            return errorList;
        }
    }
}
