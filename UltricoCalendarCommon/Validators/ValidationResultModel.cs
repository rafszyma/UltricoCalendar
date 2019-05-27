using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Solomio.Api.Infrastructure.Validation;

namespace UltricoCalendarCommon.Validators
{
    public class ValidationResultModel
    {
        public ValidationResultModel(ModelStateDictionary modelState)
        {
            Errors = modelState.Keys.SelectMany(
                    key => modelState[key].Errors
                        .Select(x => new ValidationError(key, x.ErrorMessage)))
                .ToList();
        }

        public List<ValidationError> Errors { get; }
    }
}