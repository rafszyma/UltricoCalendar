using Microsoft.AspNetCore.Mvc.Filters;

namespace UltricoApiCommon.Validators
{
    // TODO: GOOD : Nice separation of concerns.
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid) context.Result = new ValidationFailedResult(context.ModelState);
        }
    }
}